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
            this.SuspendLayout();
            this.boardPanel.BackColor = System.Drawing.Color.Black;
            this.boardPanel.Location = new System.Drawing.Point(12, 12);
            this.boardPanel.Margin = new System.Windows.Forms.Padding(0);
            this.boardPanel.Padding = new System.Windows.Forms.Padding(0);
            this.boardPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.boardPanel.AutoSize = false;
            this.boardPanel.MinimumSize = new System.Drawing.Size(240, 480);
            this.boardPanel.MaximumSize = new System.Drawing.Size(240, 480);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.Size = new System.Drawing.Size(240, 480); 
            this.boardPanel.TabIndex = 0;
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            this.lblScoreTitle.AutoSize = true;
            this.lblScoreTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblScoreTitle.Location = new System.Drawing.Point(268, 20);
            this.lblScoreTitle.Name = "lblScoreTitle";
            this.lblScoreTitle.Size = new System.Drawing.Size(58, 19);
            this.lblScoreTitle.TabIndex = 1;
            this.lblScoreTitle.Text = "Score :";
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblScore.Location = new System.Drawing.Point(332, 20);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(17, 19);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "0";
            this.lblLinesTitle.AutoSize = true;
            this.lblLinesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLinesTitle.Location = new System.Drawing.Point(268, 50);
            this.lblLinesTitle.Name = "lblLinesTitle";
            this.lblLinesTitle.Size = new System.Drawing.Size(55, 19);
            this.lblLinesTitle.TabIndex = 3;
            this.lblLinesTitle.Text = "Lines :";
            this.lblLines.AutoSize = true;
            this.lblLines.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLines.Location = new System.Drawing.Point(332, 50);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(17, 19);
            this.lblLines.TabIndex = 4;
            this.lblLines.Text = "0";
            this.lblHelp.AutoSize = true;
            this.lblHelp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHelp.Location = new System.Drawing.Point(268, 90);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(210, 150);
            this.lblHelp.TabIndex = 5;
            this.lblHelp.Text = "←/→: Move   ↑: Rotate   ↓: Soft drop\r\nSpace: Hard drop\r\nP: Pause/Resume\r\nR: Rest" + "art";
            this.gameTimer.Enabled = false;
            this.gameTimer.Interval = 500;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(492, 504);
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
    }
}
