namespace GameGK
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel boardPanel;
        private System.Windows.Forms.Label lblScoreTitle;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblLinesTitle;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblHighScoreTitle;
        private System.Windows.Forms.Label lblHighScore;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.boardPanel = new System.Windows.Forms.Panel();
            this.lblScoreTitle = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblLinesTitle = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblHighScoreTitle = new System.Windows.Forms.Label();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.nextPiecePanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // boardPanel
            // 
            this.boardPanel.BackColor = System.Drawing.Color.Black;
            this.boardPanel.Location = new System.Drawing.Point(12, 12);
            this.boardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.boardPanel.MaximumSize = new System.Drawing.Size(240, 480);
            this.boardPanel.MinimumSize = new System.Drawing.Size(240, 480);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(240, 480);
            this.boardPanel.TabIndex = 0;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            // 
            // lblScoreTitle
            // 
            this.lblScoreTitle.AutoSize = true;
            this.lblScoreTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblScoreTitle.Location = new System.Drawing.Point(255, 12);
            this.lblScoreTitle.Name = "lblScoreTitle";
            this.lblScoreTitle.Size = new System.Drawing.Size(64, 23);
            this.lblScoreTitle.TabIndex = 1;
            this.lblScoreTitle.Text = "Score :";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblScore.Location = new System.Drawing.Point(319, 12);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(19, 23);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "0";
            // 
            // lblLinesTitle
            // 
            this.lblLinesTitle.AutoSize = true;
            this.lblLinesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLinesTitle.Location = new System.Drawing.Point(255, 42);
            this.lblLinesTitle.Name = "lblLinesTitle";
            this.lblLinesTitle.Size = new System.Drawing.Size(60, 23);
            this.lblLinesTitle.TabIndex = 3;
            this.lblLinesTitle.Text = "Lines :";
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLines.Location = new System.Drawing.Point(319, 42);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(19, 23);
            this.lblLines.TabIndex = 4;
            this.lblLines.Text = "0";
            // 
            // lblHelp
            // 
            this.lblHelp.AutoSize = true;
            this.lblHelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHelp.Location = new System.Drawing.Point(257, 267);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(244, 80);
            this.lblHelp.TabIndex = 5;
            this.lblHelp.Text = "←/→: Move   ↑: Rotate   ↓: Soft drop\r\nSpace: Hard drop\r\nP: Pause/Resume\r\nR: Resta" +
    "rt";
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 500;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblHighScoreTitle
            // 
            this.lblHighScoreTitle.AutoSize = true;
            this.lblHighScoreTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHighScoreTitle.Location = new System.Drawing.Point(255, 72);
            this.lblHighScoreTitle.Name = "lblHighScoreTitle";
            this.lblHighScoreTitle.Size = new System.Drawing.Size(103, 23);
            this.lblHighScoreTitle.TabIndex = 6;
            this.lblHighScoreTitle.Text = "High Score:";
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHighScore.Location = new System.Drawing.Point(355, 72);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(19, 23);
            this.lblHighScore.TabIndex = 7;
            this.lblHighScore.Text = "0";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(257, 469);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "Cài đặt ⚙️";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(259, 232);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 9;
            this.btnNewGame.Text = "Chơi mới";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(340, 232);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(78, 23);
            this.btnPause.TabIndex = 10;
            this.btnPause.Text = "Tạm dừng";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(424, 232);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // nextPiecePanel
            // 
            this.nextPiecePanel.BackColor = System.Drawing.Color.Black;
            this.nextPiecePanel.Location = new System.Drawing.Point(259, 98);
            this.nextPiecePanel.Name = "nextPiecePanel";
            this.nextPiecePanel.Size = new System.Drawing.Size(100, 100);
            this.nextPiecePanel.TabIndex = 12;
            this.nextPiecePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.nextPiecePanel_Paint);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(580, 504);
            this.Controls.Add(this.nextPiecePanel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblHighScore);
            this.Controls.Add(this.lblHighScoreTitle);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.lblLines);
            this.Controls.Add(this.lblLinesTitle);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblScoreTitle);
            this.Controls.Add(this.boardPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel nextPiecePanel;
    }
}
