using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO; 
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Media;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace GameGK
{
    public partial class Form1 : Form
    {
        private IWavePlayer waveOut;
        private MixingSampleProvider mixer;

        private const int COLS = 10;
        private const int ROWS = 20;
        private const int CELL = 24;

        private int[,] board = new int[ROWS, COLS];
        private Random rng = new Random();
        private Tetromino current, next;
        private int curRow, curCol, curRot;
        private bool running, gameOver;
        private int score, lines;
        private int tickMs = 500;
        private int highScore;
        private const string highScoreFile = "highscore.txt";
        private byte[] moveSoundData;
        private byte[] lockSoundData;
        private byte[] clearSoundData;
        private byte[] gameOverSoundData;
        private Color[] palette = new Color[]
        {
            Color.Black,
            Color.Cyan,          // I
            Color.Yellow,        // O
            Color.MediumPurple,  // T
            Color.Lime,          // S
            Color.Red,           // Z
            Color.RoyalBlue,     // J
            Color.Orange         // L
        };

        private static readonly Dictionary<Tetromino, int[][,]> Shapes = new Dictionary<Tetromino, int[][,]>
        {
            { Tetromino.I, new int[][,]
                {
                    new int[,] { {0,0,0,0},{1,1,1,1},{0,0,0,0},{0,0,0,0} },
                    new int[,] { {0,0,1,0},{0,0,1,0},{0,0,1,0},{0,0,1,0} }
                }
            },
            { Tetromino.O, new int[][,]
                {
                    new int[,] { {0,1,1,0},{0,1,1,0},{0,0,0,0},{0,0,0,0} }
                }
            },
            { Tetromino.T, new int[][,]
                {
                    new int[,] { {0,1,0,0},{1,1,1,0},{0,0,0,0},{0,0,0,0} },
                    new int[,] { {0,1,0,0},{0,1,1,0},{0,1,0,0},{0,0,0,0} },
                    new int[,] { {0,0,0,0},{1,1,1,0},{0,1,0,0},{0,0,0,0} },
                    new int[,] { {0,1,0,0},{1,1,0,0},{0,1,0,0},{0,0,0,0} }
                }
            },
            { Tetromino.S, new int[][,]
                {
                    new int[,] { {0,1,1,0},{1,1,0,0},{0,0,0,0},{0,0,0,0} },
                    new int[,] { {1,0,0,0},{1,1,0,0},{0,1,0,0},{0,0,0,0} }
                }
            },
            { Tetromino.Z, new int[][,]
                {
                    new int[,] { {1,1,0,0},{0,1,1,0},{0,0,0,0},{0,0,0,0} },
                    new int[,] { {0,1,0,0},{1,1,0,0},{1,0,0,0},{0,0,0,0} }
                }
            },
            { Tetromino.J, new int[][,]
                {
                    new int[,] { {1,0,0,0},{1,1,1,0},{0,0,0,0},{0,0,0,0} },
                    new int[,] { {0,1,1,0},{0,1,0,0},{0,1,0,0},{0,0,0,0} },
                    new int[,] { {0,0,0,0},{1,1,1,0},{0,0,1,0},{0,0,0,0} },
                    new int[,] { {0,1,0,0},{0,1,0,0},{1,1,0,0},{0,0,0,0} }
                }
            },
            { Tetromino.L, new int[][,]
                {
                    new int[,] { {0,0,1,0},{1,1,1,0},{0,0,0,0},{0,0,0,0} },
                    new int[,] { {0,1,0,0},{0,1,0,0},{0,1,1,0},{0,0,0,0} },
                    new int[,] { {0,0,0,0},{1,1,1,0},{1,0,0,0},{0,0,0,0} },
                    new int[,] { {1,1,0,0},{0,1,0,0},{0,1,0,0},{0,0,0,0} }
                }
            }
        };

        public Form1()
        {
            InitializeComponent();
            waveOut = new WaveOutEvent();            
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));
            mixer.ReadFully = true; 
            waveOut.Init(mixer);
            waveOut.Play();
            LoadSounds();
            LoadHighScore(); 
            lblHighScore.Text = highScore.ToString(); 
            ForceBoardSize();
            boardPanel.Margin = Padding.Empty;
            boardPanel.Padding = Padding.Empty;
            boardPanel.BorderStyle = BorderStyle.None;
            boardPanel.AutoSize = false;
            this.DoubleBuffered = true;
            EnableDoubleBuffer(boardPanel);
            this.Load += new EventHandler(Form1_Load);
            this.Resize += new EventHandler(Form1_Resize);

            ResetGame();
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            ForceBoardSize();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ForceBoardSize();
        }

        private void LoadSounds()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                Properties.Resources._move.CopyTo(stream);
                moveSoundData = stream.ToArray();
                stream.SetLength(0); // Reset stream cho file tiếp theo

                Properties.Resources._lock.CopyTo(stream);
                lockSoundData = stream.ToArray();
                stream.SetLength(0);

                Properties.Resources._clear.CopyTo(stream);
                clearSoundData = stream.ToArray();
                stream.SetLength(0);

                Properties.Resources._gameover.CopyTo(stream);
                gameOverSoundData = stream.ToArray();
            }
        }

        private void PlaySound(byte[] soundData)
        {
            // Tạo một luồng đọc trong bộ nhớ từ dữ liệu đã nạp sẵn
            var stream = new System.IO.MemoryStream(soundData);
            var waveReader = new WaveFileReader(stream);

            // Chuyển đổi và thêm vào bộ trộn
            ISampleProvider soundToPlay = waveReader.ToSampleProvider();

            if (soundToPlay.WaveFormat.SampleRate != mixer.WaveFormat.SampleRate ||
                soundToPlay.WaveFormat.Channels != mixer.WaveFormat.Channels)
            {
                soundToPlay = new WdlResamplingSampleProvider(soundToPlay, mixer.WaveFormat.SampleRate);
                if (soundToPlay.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
                {
                    soundToPlay = new MonoToStereoSampleProvider(soundToPlay);
                }
            }

            mixer.AddMixerInput(soundToPlay);
        }

        private void LoadHighScore()
        {
            if (File.Exists(highScoreFile))
            {
                string content = File.ReadAllText(highScoreFile);
                int.TryParse(content, out highScore);
            }
            else
            {
                highScore = 0;
            }
        }

        private void SaveHighScore()
        {
            File.WriteAllText(highScoreFile, highScore.ToString());
        }

        private void ForceBoardSize()
        {
            int w = COLS * CELL; 
            int h = ROWS * CELL;
            if (boardPanel.Width != w || boardPanel.Height != h)
            {
                boardPanel.MinimumSize = new Size(w, h);
                boardPanel.MaximumSize = new Size(w, h);
                boardPanel.Size = new Size(w, h);
            }
        }

        private void EnableDoubleBuffer(Control c)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi != null) pi.SetValue(c, true, null);
        }

        private void ResetGame()
        {
            Array.Clear(board, 0, board.Length);
            score = 0; lines = 0; tickMs = 500;
            lblScore.Text = "0";
            lblLines.Text = "0";
            lblHighScore.Text = highScore.ToString();
            lblHelp.Text = "←/→: Move   ↑: Rotate   ↓: Soft drop   Space: Hard drop\nP: Pause/Resume   R: Restart";

            next = RandomPiece();
            SpawnNew();

            gameOver = false; running = true;
            gameTimer.Interval = tickMs;
            gameTimer.Start();
            boardPanel.Invalidate();
        }

        private Tetromino RandomPiece()
        {
            return (Tetromino)rng.Next(1, 8); 
        }

        private void SpawnNew()
        {
            
            current = next;
            next = RandomPiece();
            curRot = 0; curRow = 0; curCol = 3;

            if (!CanPlace(current, curRow, curCol, curRot))
            {
                PlaySound(gameOverSoundData);
                gameOver = true; running = false;
                gameTimer.Stop();
                if (score > highScore)
                {
                    highScore = score;
                    lblHighScore.Text = highScore.ToString();
                    SaveHighScore();
                    lblHelp.Text = "New High Score! Nhấn R để chơi lại.";
                }
                else
                {
                    lblHelp.Text = "Game Over! Nhấn R để chơi lại.";
                }
            }
        }

        private static int[,] GetMatrix(Tetromino t, int rot)
        {
            int[][,] mats = Shapes[t];
            int len = mats.Length;
            int idx = rot % len;
            if (idx < 0) idx += len;
            return mats[idx];
        }

        private bool CanPlace(Tetromino t, int r, int c, int rot)
        {
            int[,] mat = GetMatrix(t, rot);
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (mat[y, x] == 0) continue;
                    int nr = r + y, nc = c + x;
                    if (nc < 0 || nc >= COLS || nr >= ROWS) return false;
                    if (nr >= 0 && board[nr, nc] != 0) return false;
                }
            }
            return true;
        }

        private void LockPiece()
        {
            PlaySound(lockSoundData);
            int[,] mat = GetMatrix(current, curRot);
            int id = (int)current;

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (mat[y, x] == 0) continue;
                    int nr = curRow + y, nc = curCol + x;
                    if (nr >= 0 && nr < ROWS && nc >= 0 && nc < COLS)
                        board[nr, nc] = id;
                }
            }

            ClearLines();
            SpawnNew();
        }
        private void ClearLines()
        {
            int cleared = 0;

            for (int r = ROWS - 1; r >= 0; r--)
            {
                bool full = true;
                for (int c = 0; c < COLS; c++)
                {
                    if (board[r, c] == 0) { full = false; break; }
                }

                if (full)
                {
                    cleared++;
                    int rr, cc;
                    for (rr = r; rr > 0; rr--)
                        for (cc = 0; cc < COLS; cc++)
                            board[rr, cc] = board[rr - 1, cc];
                    for (cc = 0; cc < COLS; cc++) board[0, cc] = 0;
                    r++; 
                }
            }

            if (cleared > 0)
            {
                PlaySound(clearSoundData);
                int add = 0;
                switch (cleared)
                {
                    case 1: add = 100; break;
                    case 2: add = 300; break;
                    case 3: add = 500; break;
                    default: add = 800; break;
                }
                score += add; lines += cleared;
                lblScore.Text = score.ToString();
                lblLines.Text = lines.ToString();

                if (lines % 10 == 0 && tickMs > 110)
                {
                    tickMs = (int)(tickMs * 0.95);
                    if (tickMs < 110) tickMs = 110;
                    gameTimer.Interval = tickMs;
                }
            }
        }

        private bool TryMove(int dr, int dc)
        {
            int nr = curRow + dr, nc = curCol + dc;
            if (!CanPlace(current, nr, nc, curRot)) return false;
            curRow = nr; curCol = nc;
            PlaySound(moveSoundData);
            boardPanel.Invalidate();
            return true;
        }

        private void TryRotate()
        {
            int newRot = curRot + 1;
            int[] kicks = new int[] { 0, -1, 1, -2, 2 };
            for (int i = 0; i < kicks.Length; i++)
            {
                int k = kicks[i];
                if (CanPlace(current, curRow, curCol + k, newRot))
                {
                    curRot = newRot;
                    curCol += k;
                    PlaySound(moveSoundData); 
                    boardPanel.Invalidate();
                    return;
                }
            }
        }

        private void SoftDrop()
        {
            if (TryMove(1, 0))
            {
                lblScore.Text = score.ToString();
            }
            else LockPiece();
        }

        private void HardDrop()
        {
            int drop = 0;
            while (TryMove(1, 0)) drop++;
            score += drop * 2;
            lblScore.Text = score.ToString();
            LockPiece();
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (!running || gameOver) return;
            if (!TryMove(1, 0)) LockPiece();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameOver)
            {
                if (e.KeyCode == Keys.R) ResetGame();
                return;
            }

            if (!running)
            {
                if (e.KeyCode == Keys.P) 
                { 
                    running = true; 
                    gameTimer.Start(); 
                    lblHelp.Text = "←/→: Move   ↑: Rotate   ↓: Soft drop   Space: Hard drop\nP: Pause/Resume   R: Restart"; 
                }
                else if (e.KeyCode == Keys.R) ResetGame();
                return;
            }

            if (e.KeyCode == Keys.Left) TryMove(0, -1);
            else if (e.KeyCode == Keys.Right) TryMove(0, 1);
            else if (e.KeyCode == Keys.Down) SoftDrop();
            else if (e.KeyCode == Keys.Up) TryRotate();
            else if (e.KeyCode == Keys.Space) HardDrop();
            else if (e.KeyCode == Keys.P)
            {
                running = false;
                gameTimer.Stop();
                lblHelp.Text = "Đã tạm dừng (nhấn P để tiếp tục)";
            }
            else if (e.KeyCode == Keys.R) ResetGame();
        }
        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            ForceBoardSize();
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.None;
            int W = boardPanel.ClientSize.Width;
            int H = boardPanel.ClientSize.Height;
            Rectangle boardRect = new Rectangle(0, 0, W, H);
            using (LinearGradientBrush bg = new LinearGradientBrush(
                boardRect, Color.FromArgb(18, 18, 28), Color.FromArgb(45, 45, 66), 90f))
            {
                g.FillRectangle(bg, boardRect);
            }
            using (Pen gridPen = new Pen(Color.FromArgb(40, 255, 255, 255)))
            {
                int maxX = W - 1;
                int maxY = H - 1;

                for (int r = 0; r <= ROWS; r++)
                {
                    int y = r * CELL;
                    if (y > maxY) y = maxY;
                    g.DrawLine(gridPen, 0, y, maxX, y);
                }

                for (int c = 0; c <= COLS; c++)
                {
                    int x = c * CELL;
                    if (x > maxX) x = maxX;
                    g.DrawLine(gridPen, x, 0, x, maxY);
                }
            }
            for (int rr = 0; rr < ROWS; rr++)
                for (int cc = 0; cc < COLS; cc++)
                {
                    int id = board[rr, cc];
                    if (id != 0) DrawCell(g, cc, rr, palette[id], false);
                }
            if (!gameOver)
            {
                int ghostRow = curRow;
                while (CanPlace(current, ghostRow + 1, curCol, curRot)) ghostRow++;
                DrawPiece(g, current, ghostRow, curCol, curRot, palette[(int)current], true);
                DrawPiece(g, current, curRow, curCol, curRot, palette[(int)current], false);
            }
            using (Pen pen = new Pen(Color.FromArgb(70, Color.White), 2))
            {
                g.DrawRectangle(pen, 0, 0, W - 1, H - 1);
            }
        }
        private void DrawPiece(Graphics g, Tetromino t, int r, int c, int rot, Color color, bool ghost)
        {
            int[,] mat = GetMatrix(t, rot);
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    if (mat[y, x] == 0) continue;
                    int rr = r + y, cc = c + x;
                    if (rr < 0 || rr >= ROWS || cc < 0 || cc >= COLS) continue;
                    DrawCell(g, cc, rr, color, ghost);
                }
        }
        private void DrawCell(Graphics g, int col, int row, Color color, bool ghost)
        {
            int x = col * CELL, y = row * CELL;
            Rectangle rect = new Rectangle(x + 1, y + 1, CELL - 2, CELL - 2);

            Color cTop = ghost ? Color.FromArgb(60, color.R, color.G, color.B) : color;
            Color cBottom = ghost ? Color.FromArgb(40, (int)(color.R * 0.7), (int)(color.G * 0.7), (int)(color.B * 0.7))
                                  : ControlPaint.Dark(color);

            using (LinearGradientBrush br = new LinearGradientBrush(rect, cTop, cBottom, 90f))
                g.FillRectangle(br, rect);
            using (Pen border = new Pen(Color.FromArgb(90, Color.Black)))
                g.DrawRectangle(border, rect);
            using (Pen topLine = new Pen(Color.FromArgb(140, Color.White)))
                g.DrawLine(topLine, rect.Left + 1, rect.Top + 1, rect.Right - 1, rect.Top + 1);
        }
    }
    public enum Tetromino { I = 1, O, T, S, Z, J, L }
}
