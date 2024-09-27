namespace ML3DInstaller.View
{
    partial class UCSettings
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            rbShowProd = new RadioButton();
            rbShowall = new RadioButton();
            label2 = new Label();
            cbDevMode = new CheckBox();
            tlpDevMode = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnLaunchUpdate = new Button();
            label4 = new Label();
            tbCurrentVersion = new TextBox();
            btnClumsy = new Button();
            cbShowTest = new CheckBox();
            label3 = new Label();
            tlpGitPat = new TableLayoutPanel();
            lblGitPat = new Label();
            tbGitPAT = new TextBox();
            cbUsePAT = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            btnSave = new Button();
            button1 = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tlpDevMode.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tlpGitPat.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(cbDevMode, 0, 2);
            tableLayoutPanel1.Controls.Add(tlpDevMode, 0, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 5);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(372, 337);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(366, 20);
            label1.TabIndex = 0;
            label1.Text = "Releases";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(rbShowProd, 0, 0);
            tableLayoutPanel2.Controls.Add(rbShowall, 0, 1);
            tableLayoutPanel2.Controls.Add(label2, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 20);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(372, 50);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // rbShowProd
            // 
            rbShowProd.AutoSize = true;
            rbShowProd.Checked = true;
            rbShowProd.Location = new Point(8, 3);
            rbShowProd.Margin = new Padding(8, 3, 3, 3);
            rbShowProd.Name = "rbShowProd";
            rbShowProd.Size = new Size(176, 19);
            rbShowProd.TabIndex = 0;
            rbShowProd.TabStop = true;
            rbShowProd.Text = "Show only production-ready";
            rbShowProd.UseVisualStyleBackColor = true;
            // 
            // rbShowall
            // 
            rbShowall.AutoSize = true;
            rbShowall.Location = new Point(8, 28);
            rbShowall.Margin = new Padding(8, 3, 3, 3);
            rbShowall.Name = "rbShowall";
            rbShowall.Size = new Size(69, 19);
            rbShowall.TabIndex = 1;
            rbShowall.Text = "Show all";
            rbShowall.UseVisualStyleBackColor = true;
            rbShowall.CheckedChanged += rbShowall_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.ForeColor = Color.Gray;
            label2.Location = new Point(190, 3);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(179, 19);
            label2.TabIndex = 2;
            label2.Text = "(Recommended)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbDevMode
            // 
            cbDevMode.AutoSize = true;
            cbDevMode.Location = new Point(8, 73);
            cbDevMode.Margin = new Padding(8, 3, 3, 3);
            cbDevMode.Name = "cbDevMode";
            cbDevMode.Size = new Size(113, 19);
            cbDevMode.TabIndex = 10;
            cbDevMode.Text = "Developer Mode";
            cbDevMode.UseVisualStyleBackColor = true;
            cbDevMode.CheckedChanged += cbDevMode_CheckedChanged;
            // 
            // tlpDevMode
            // 
            tlpDevMode.AutoSize = true;
            tlpDevMode.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpDevMode.ColumnCount = 1;
            tlpDevMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDevMode.Controls.Add(tableLayoutPanel3, 0, 3);
            tlpDevMode.Controls.Add(btnClumsy, 0, 2);
            tlpDevMode.Controls.Add(cbShowTest, 0, 1);
            tlpDevMode.Controls.Add(label3, 0, 0);
            tlpDevMode.Controls.Add(tlpGitPat, 0, 5);
            tlpDevMode.Controls.Add(cbUsePAT, 0, 4);
            tlpDevMode.Dock = DockStyle.Fill;
            tlpDevMode.Location = new Point(3, 98);
            tlpDevMode.Name = "tlpDevMode";
            tlpDevMode.RowCount = 6;
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.Size = new Size(366, 188);
            tlpDevMode.TabIndex = 13;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(btnLaunchUpdate, 0, 0);
            tableLayoutPanel3.Controls.Add(label4, 1, 0);
            tableLayoutPanel3.Controls.Add(tbCurrentVersion, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(8, 85);
            tableLayoutPanel3.Margin = new Padding(8, 5, 0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(358, 44);
            tableLayoutPanel3.TabIndex = 13;
            // 
            // btnLaunchUpdate
            // 
            btnLaunchUpdate.Dock = DockStyle.Top;
            btnLaunchUpdate.Location = new Point(3, 3);
            btnLaunchUpdate.MinimumSize = new Size(114, 0);
            btnLaunchUpdate.Name = "btnLaunchUpdate";
            tableLayoutPanel3.SetRowSpan(btnLaunchUpdate, 2);
            btnLaunchUpdate.Size = new Size(114, 38);
            btnLaunchUpdate.TabIndex = 0;
            btnLaunchUpdate.Text = "Launch Update";
            btnLaunchUpdate.UseVisualStyleBackColor = true;
            btnLaunchUpdate.Click += btnLaunchUpdate_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Top;
            label4.Location = new Point(123, 0);
            label4.Name = "label4";
            label4.Size = new Size(232, 15);
            label4.TabIndex = 1;
            label4.Text = "Set Current Version";
            // 
            // tbCurrentVersion
            // 
            tbCurrentVersion.Dock = DockStyle.Top;
            tbCurrentVersion.Location = new Point(123, 18);
            tbCurrentVersion.Name = "tbCurrentVersion";
            tbCurrentVersion.Size = new Size(232, 23);
            tbCurrentVersion.TabIndex = 2;
            tbCurrentVersion.Text = "0.0";
            // 
            // btnClumsy
            // 
            btnClumsy.AutoSize = true;
            btnClumsy.Dock = DockStyle.Left;
            btnClumsy.Location = new Point(11, 52);
            btnClumsy.Margin = new Padding(11, 5, 3, 3);
            btnClumsy.Name = "btnClumsy";
            btnClumsy.Size = new Size(114, 25);
            btnClumsy.TabIndex = 12;
            btnClumsy.Text = "Download Clumsy";
            btnClumsy.UseVisualStyleBackColor = true;
            // 
            // cbShowTest
            // 
            cbShowTest.AutoSize = true;
            cbShowTest.Dock = DockStyle.Top;
            cbShowTest.Location = new Point(8, 25);
            cbShowTest.Margin = new Padding(8, 5, 3, 3);
            cbShowTest.Name = "cbShowTest";
            cbShowTest.Size = new Size(355, 19);
            cbShowTest.TabIndex = 5;
            cbShowTest.Text = "Show \"Test\" Project";
            cbShowTest.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(360, 20);
            label3.TabIndex = 4;
            label3.Text = "Developer Options";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // tlpGitPat
            // 
            tlpGitPat.AutoSize = true;
            tlpGitPat.ColumnCount = 2;
            tlpGitPat.ColumnStyles.Add(new ColumnStyle());
            tlpGitPat.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpGitPat.Controls.Add(lblGitPat, 0, 0);
            tlpGitPat.Controls.Add(tbGitPAT, 1, 0);
            tlpGitPat.Dock = DockStyle.Fill;
            tlpGitPat.Location = new Point(11, 159);
            tlpGitPat.Margin = new Padding(11, 5, 0, 0);
            tlpGitPat.Name = "tlpGitPat";
            tlpGitPat.RowCount = 1;
            tlpGitPat.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpGitPat.Size = new Size(355, 29);
            tlpGitPat.TabIndex = 14;
            // 
            // lblGitPat
            // 
            lblGitPat.AutoSize = true;
            lblGitPat.Dock = DockStyle.Fill;
            lblGitPat.Location = new Point(0, 0);
            lblGitPat.Margin = new Padding(0);
            lblGitPat.Name = "lblGitPat";
            lblGitPat.Size = new Size(155, 29);
            lblGitPat.TabIndex = 0;
            lblGitPat.Text = "Github Private Access Token";
            lblGitPat.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbGitPAT
            // 
            tbGitPAT.Dock = DockStyle.Top;
            tbGitPAT.Location = new Point(158, 3);
            tbGitPAT.Name = "tbGitPAT";
            tbGitPAT.PasswordChar = '*';
            tbGitPAT.Size = new Size(194, 23);
            tbGitPAT.TabIndex = 1;
            tbGitPAT.UseSystemPasswordChar = true;
            // 
            // cbUsePAT
            // 
            cbUsePAT.AutoSize = true;
            cbUsePAT.Location = new Point(3, 132);
            cbUsePAT.Name = "cbUsePAT";
            cbUsePAT.Size = new Size(198, 19);
            cbUsePAT.TabIndex = 15;
            cbUsePAT.Text = "Use Github PAT (Recommended)";
            cbUsePAT.UseVisualStyleBackColor = true;
            cbUsePAT.CheckedChanged += cbUsePAT_CheckedChanged;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(btnSave, 0, 0);
            tableLayoutPanel4.Controls.Add(button1, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Top;
            tableLayoutPanel4.Location = new Point(0, 302);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(372, 31);
            tableLayoutPanel4.TabIndex = 14;
            // 
            // btnSave
            // 
            btnSave.AutoSize = true;
            btnSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSave.Dock = DockStyle.Right;
            btnSave.Location = new Point(269, 3);
            btnSave.MinimumSize = new Size(100, 0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 25);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Dock = DockStyle.Left;
            button1.Location = new Point(3, 3);
            button1.MinimumSize = new Size(100, 0);
            button1.Name = "button1";
            button1.Size = new Size(100, 25);
            button1.TabIndex = 10;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // UCSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UCSettings";
            Size = new Size(372, 337);
            Load += UCSettings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tlpDevMode.ResumeLayout(false);
            tlpDevMode.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tlpGitPat.ResumeLayout(false);
            tlpGitPat.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton rbShowProd;
        private RadioButton rbShowall;
        private Label label2;
        private CheckBox cbDevMode;
        private TableLayoutPanel tlpDevMode;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnLaunchUpdate;
        private Label label4;
        private TextBox tbCurrentVersion;
        private Button btnClumsy;
        private CheckBox cbShowTest;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btnSave;
        private Button button1;
        private TableLayoutPanel tlpGitPat;
        private Label lblGitPat;
        private TextBox tbGitPAT;
        private CheckBox cbUsePAT;
    }
}
