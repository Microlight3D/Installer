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
            tableLayoutPanel5 = new TableLayoutPanel();
            label5 = new Label();
            cbChunkSize = new ComboBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnLaunchUpdate = new Button();
            label4 = new Label();
            tbCurrentVersion = new TextBox();
            cbShowTest = new CheckBox();
            label3 = new Label();
            tlpGitPat = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            cbUsePAT = new CheckBox();
            label7 = new Label();
            btnPATHelp = new Button();
            lblGitPat = new Label();
            tbGitPAT = new TextBox();
            cbSupportOptions = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            btnSave = new Button();
            button1 = new Button();
            label6 = new Label();
            tlpReloadSources = new TableLayoutPanel();
            btnReloadSources = new Button();
            lblLastReload = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tlpDevMode.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tlpGitPat.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tlpReloadSources.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(cbDevMode, 0, 3);
            tableLayoutPanel1.Controls.Add(tlpDevMode, 0, 6);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 8);
            tableLayoutPanel1.Controls.Add(label6, 0, 4);
            tableLayoutPanel1.Controls.Add(tlpReloadSources, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 1F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(372, 385);
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
            cbDevMode.Location = new Point(8, 104);
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
            tlpDevMode.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpDevMode.ColumnCount = 1;
            tlpDevMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDevMode.Controls.Add(tableLayoutPanel5, 0, 6);
            tlpDevMode.Controls.Add(tableLayoutPanel3, 0, 3);
            tlpDevMode.Controls.Add(cbShowTest, 0, 1);
            tlpDevMode.Controls.Add(label3, 0, 0);
            tlpDevMode.Controls.Add(tlpGitPat, 0, 5);
            tlpDevMode.Controls.Add(cbSupportOptions, 0, 7);
            tlpDevMode.Dock = DockStyle.Top;
            tlpDevMode.Location = new Point(3, 130);
            tlpDevMode.Name = "tlpDevMode";
            tlpDevMode.RowCount = 9;
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle());
            tlpDevMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDevMode.Size = new Size(366, 217);
            tlpDevMode.TabIndex = 13;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label5, 0, 0);
            tableLayoutPanel5.Controls.Add(cbChunkSize, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Top;
            tableLayoutPanel5.Location = new Point(9, 163);
            tableLayoutPanel5.Margin = new Padding(8, 0, 0, 0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Size = new Size(356, 26);
            tableLayoutPanel5.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Top;
            label5.Location = new Point(3, 5);
            label5.Margin = new Padding(3, 5, 3, 0);
            label5.Name = "label5";
            label5.Size = new Size(172, 15);
            label5.TabIndex = 0;
            label5.Text = "Size of chunks";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbChunkSize
            // 
            cbChunkSize.Dock = DockStyle.Top;
            cbChunkSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChunkSize.FormattingEnabled = true;
            cbChunkSize.Items.AddRange(new object[] { "8,192", "16,384", "32,768", "65,536", "131,072", "262,144", "524,288" });
            cbChunkSize.Location = new Point(181, 3);
            cbChunkSize.Margin = new Padding(3, 3, 3, 0);
            cbChunkSize.Name = "cbChunkSize";
            cbChunkSize.Size = new Size(172, 23);
            cbChunkSize.TabIndex = 1;
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
            tableLayoutPanel3.Location = new Point(9, 56);
            tableLayoutPanel3.Margin = new Padding(8, 5, 0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(356, 44);
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
            label4.Size = new Size(230, 15);
            label4.TabIndex = 1;
            label4.Text = "Set Current Version";
            // 
            // tbCurrentVersion
            // 
            tbCurrentVersion.Dock = DockStyle.Top;
            tbCurrentVersion.Location = new Point(123, 18);
            tbCurrentVersion.Name = "tbCurrentVersion";
            tbCurrentVersion.Size = new Size(230, 23);
            tbCurrentVersion.TabIndex = 2;
            tbCurrentVersion.Text = "0.0";
            // 
            // cbShowTest
            // 
            cbShowTest.AutoSize = true;
            cbShowTest.Dock = DockStyle.Top;
            cbShowTest.Location = new Point(9, 27);
            cbShowTest.Margin = new Padding(8, 5, 3, 3);
            cbShowTest.Name = "cbShowTest";
            cbShowTest.Size = new Size(353, 19);
            cbShowTest.TabIndex = 5;
            cbShowTest.Text = "Show \"Test\" Project";
            cbShowTest.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(4, 1);
            label3.Name = "label3";
            label3.Size = new Size(358, 20);
            label3.TabIndex = 4;
            label3.Text = "Developer Options";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // tlpGitPat
            // 
            tlpGitPat.AutoSize = true;
            tlpGitPat.ColumnCount = 2;
            tlpGitPat.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGitPat.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpGitPat.Controls.Add(tableLayoutPanel6, 0, 0);
            tlpGitPat.Controls.Add(lblGitPat, 0, 1);
            tlpGitPat.Controls.Add(tbGitPAT, 1, 1);
            tlpGitPat.Dock = DockStyle.Fill;
            tlpGitPat.Location = new Point(9, 107);
            tlpGitPat.Margin = new Padding(8, 5, 0, 0);
            tlpGitPat.Name = "tlpGitPat";
            tlpGitPat.RowCount = 2;
            tlpGitPat.RowStyles.Add(new RowStyle());
            tlpGitPat.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpGitPat.Size = new Size(356, 55);
            tlpGitPat.TabIndex = 14;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.ColumnCount = 3;
            tlpGitPat.SetColumnSpan(tableLayoutPanel6, 2);
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(cbUsePAT, 0, 0);
            tableLayoutPanel6.Controls.Add(label7, 1, 0);
            tableLayoutPanel6.Controls.Add(btnPATHelp, 2, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 0);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(356, 25);
            tableLayoutPanel6.TabIndex = 19;
            // 
            // cbUsePAT
            // 
            cbUsePAT.AutoSize = true;
            cbUsePAT.Dock = DockStyle.Top;
            cbUsePAT.Location = new Point(0, 3);
            cbUsePAT.Margin = new Padding(0, 3, 3, 3);
            cbUsePAT.Name = "cbUsePAT";
            cbUsePAT.Size = new Size(106, 19);
            cbUsePAT.TabIndex = 16;
            cbUsePAT.Text = "Use Github PAT";
            cbUsePAT.UseVisualStyleBackColor = true;
            cbUsePAT.CheckedChanged += cbUsePAT_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.ForeColor = Color.Gray;
            label7.Location = new Point(112, 0);
            label7.Name = "label7";
            label7.Size = new Size(96, 25);
            label7.TabIndex = 17;
            label7.Text = "(Recommended)";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnPATHelp
            // 
            btnPATHelp.Dock = DockStyle.Fill;
            btnPATHelp.Location = new Point(211, 0);
            btnPATHelp.Margin = new Padding(0);
            btnPATHelp.Name = "btnPATHelp";
            btnPATHelp.Size = new Size(145, 25);
            btnPATHelp.TabIndex = 18;
            btnPATHelp.Text = "What's that ?";
            btnPATHelp.UseVisualStyleBackColor = true;
            btnPATHelp.Click += btnPATHelp_Click;
            // 
            // lblGitPat
            // 
            lblGitPat.AutoSize = true;
            lblGitPat.Dock = DockStyle.Fill;
            lblGitPat.Location = new Point(3, 25);
            lblGitPat.Margin = new Padding(3, 0, 0, 0);
            lblGitPat.Name = "lblGitPat";
            lblGitPat.Size = new Size(175, 30);
            lblGitPat.TabIndex = 0;
            lblGitPat.Text = "Github Private Access Token";
            lblGitPat.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tbGitPAT
            // 
            tbGitPAT.Dock = DockStyle.Top;
            tbGitPAT.Location = new Point(181, 28);
            tbGitPAT.Name = "tbGitPAT";
            tbGitPAT.PasswordChar = '*';
            tbGitPAT.Size = new Size(172, 23);
            tbGitPAT.TabIndex = 1;
            tbGitPAT.UseSystemPasswordChar = true;
            // 
            // cbSupportOptions
            // 
            cbSupportOptions.AutoSize = true;
            tlpDevMode.SetColumnSpan(cbSupportOptions, 2);
            cbSupportOptions.Dock = DockStyle.Top;
            cbSupportOptions.Location = new Point(9, 193);
            cbSupportOptions.Margin = new Padding(8, 3, 3, 3);
            cbSupportOptions.Name = "cbSupportOptions";
            cbSupportOptions.Size = new Size(353, 19);
            cbSupportOptions.TabIndex = 17;
            cbSupportOptions.Text = "View \"Support Only\" options";
            cbSupportOptions.UseVisualStyleBackColor = true;
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
            tableLayoutPanel4.Location = new Point(0, 350);
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
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Silver;
            label6.Dock = DockStyle.Top;
            label6.Location = new Point(8, 126);
            label6.Margin = new Padding(8, 0, 8, 0);
            label6.Name = "label6";
            label6.Size = new Size(356, 1);
            label6.TabIndex = 15;
            // 
            // tlpReloadSources
            // 
            tlpReloadSources.AutoSize = true;
            tlpReloadSources.ColumnCount = 2;
            tlpReloadSources.ColumnStyles.Add(new ColumnStyle());
            tlpReloadSources.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpReloadSources.Controls.Add(btnReloadSources, 0, 0);
            tlpReloadSources.Controls.Add(lblLastReload, 1, 0);
            tlpReloadSources.Dock = DockStyle.Top;
            tlpReloadSources.Location = new Point(0, 70);
            tlpReloadSources.Margin = new Padding(0);
            tlpReloadSources.Name = "tlpReloadSources";
            tlpReloadSources.RowCount = 1;
            tlpReloadSources.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpReloadSources.Size = new Size(372, 31);
            tlpReloadSources.TabIndex = 16;
            // 
            // btnReloadSources
            // 
            btnReloadSources.AutoSize = true;
            btnReloadSources.Dock = DockStyle.Top;
            btnReloadSources.Location = new Point(3, 3);
            btnReloadSources.Name = "btnReloadSources";
            btnReloadSources.Size = new Size(97, 25);
            btnReloadSources.TabIndex = 0;
            btnReloadSources.Text = "Reload Sources";
            btnReloadSources.UseVisualStyleBackColor = true;
            btnReloadSources.Click += btnReloadSources_Click;
            // 
            // lblLastReload
            // 
            lblLastReload.AutoSize = true;
            lblLastReload.Dock = DockStyle.Fill;
            lblLastReload.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastReload.ForeColor = Color.FromArgb(64, 64, 64);
            lblLastReload.Location = new Point(106, 0);
            lblLastReload.Name = "lblLastReload";
            lblLastReload.RightToLeft = RightToLeft.No;
            lblLastReload.Size = new Size(263, 31);
            lblLastReload.TabIndex = 1;
            lblLastReload.Text = "Last update : xxx ago";
            lblLastReload.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // UCSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "UCSettings";
            Size = new Size(372, 385);
            Load += UCSettings_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tlpDevMode.ResumeLayout(false);
            tlpDevMode.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tlpGitPat.ResumeLayout(false);
            tlpGitPat.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tlpReloadSources.ResumeLayout(false);
            tlpReloadSources.PerformLayout();
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
        private CheckBox cbShowTest;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btnSave;
        private Button button1;
        private TableLayoutPanel tlpGitPat;
        private Label lblGitPat;
        private TextBox tbGitPAT;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label5;
        private ComboBox cbChunkSize;
        private CheckBox cbSupportOptions;
        private Label label6;
        private TableLayoutPanel tableLayoutPanel6;
        private CheckBox cbUsePAT;
        private Label label7;
        private Button btnPATHelp;
        private TableLayoutPanel tlpReloadSources;
        private Button btnReloadSources;
        private Label lblLastReload;
    }
}
