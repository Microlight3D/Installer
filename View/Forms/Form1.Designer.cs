namespace ML3DInstaller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tableLayoutPanel1 = new TableLayoutPanel();
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            ucMain1 = new UCMain();
            ucHome1 = new UCHome();
            lblDevMode = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            tableLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel1.Controls.Add(ucMain1, 0, 2);
            tableLayoutPanel1.Controls.Add(ucHome1, 1, 2);
            tableLayoutPanel1.Controls.Add(lblDevMode, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(411, 191);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ButtonHighlight;
            tableLayoutPanel1.SetColumnSpan(menuStrip1, 2);
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(411, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // ucMain1
            // 
            ucMain1.Dock = DockStyle.Fill;
            ucMain1.Location = new Point(3, 44);
            ucMain1.MinimumSize = new Size(0, 135);
            ucMain1.Name = "ucMain1";
            ucMain1.Size = new Size(199, 144);
            ucMain1.TabIndex = 1;
            // 
            // ucHome1
            // 
            ucHome1.Dock = DockStyle.Fill;
            ucHome1.Location = new Point(208, 44);
            ucHome1.MinimumSize = new Size(343, 132);
            ucHome1.Name = "ucHome1";
            ucHome1.Size = new Size(343, 144);
            ucHome1.TabIndex = 2;
            // 
            // lblDevMode
            // 
            lblDevMode.AutoSize = true;
            lblDevMode.BackColor = Color.FromArgb(192, 255, 255);
            tableLayoutPanel1.SetColumnSpan(lblDevMode, 2);
            lblDevMode.Dock = DockStyle.Top;
            lblDevMode.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDevMode.ForeColor = Color.Red;
            lblDevMode.Location = new Point(0, 24);
            lblDevMode.Margin = new Padding(0);
            lblDevMode.Name = "lblDevMode";
            lblDevMode.Size = new Size(411, 17);
            lblDevMode.TabIndex = 3;
            lblDevMode.Text = "Developer Mode Activated";
            lblDevMode.TextAlign = ContentAlignment.TopCenter;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 191);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MinimumSize = new Size(427, 192);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ML3D Installer";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private UCMain ucMain1;
        private UCHome ucHome1;
        private ContextMenuStrip contextMenuStrip1;
        private Label lblDevMode;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}
