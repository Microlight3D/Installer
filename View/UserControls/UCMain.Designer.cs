namespace ML3DInstaller
{
    partial class UCMain
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
            tableLayoutPanel2 = new TableLayoutPanel();
            btnCancelLeft = new Button();
            label1 = new Label();
            defaultPathCb = new CheckBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            PathTB = new TextBox();
            selectFolderBtn = new Button();
            btnInstall = new Button();
            progressBar = new ProgressBar();
            lblTitle = new Label();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(btnCancelLeft, 0, 5);
            tableLayoutPanel2.Controls.Add(label1, 0, 1);
            tableLayoutPanel2.Controls.Add(defaultPathCb, 2, 2);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel2.Controls.Add(btnInstall, 1, 5);
            tableLayoutPanel2.Controls.Add(progressBar, 0, 3);
            tableLayoutPanel2.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(345, 135);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // btnCancelLeft
            // 
            btnCancelLeft.BackColor = Color.FromArgb(255, 192, 192);
            btnCancelLeft.Dock = DockStyle.Fill;
            btnCancelLeft.Location = new Point(3, 101);
            btnCancelLeft.Name = "btnCancelLeft";
            btnCancelLeft.Size = new Size(118, 33);
            btnCancelLeft.TabIndex = 7;
            btnCancelLeft.Text = "Cancel";
            btnCancelLeft.UseVisualStyleBackColor = false;
            btnCancelLeft.Click += btnCancelLeft_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(label1, 3);
            label1.Location = new Point(3, 20);
            label1.Name = "label1";
            label1.Size = new Size(196, 15);
            label1.TabIndex = 1;
            label1.Text = "Please chose the destination folder :";
            // 
            // defaultPathCb
            // 
            defaultPathCb.AutoSize = true;
            defaultPathCb.Checked = true;
            defaultPathCb.CheckState = CheckState.Checked;
            defaultPathCb.Dock = DockStyle.Left;
            defaultPathCb.Location = new Point(251, 43);
            defaultPathCb.Name = "defaultPathCb";
            defaultPathCb.Size = new Size(91, 23);
            defaultPathCb.TabIndex = 2;
            defaultPathCb.Text = "Default Path";
            defaultPathCb.UseVisualStyleBackColor = true;
            defaultPathCb.CheckedChanged += defaultPathCb_CheckedChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel2.SetColumnSpan(tableLayoutPanel3, 2);
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(PathTB, 0, 0);
            tableLayoutPanel3.Controls.Add(selectFolderBtn, 1, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 40);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(248, 29);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // PathTB
            // 
            PathTB.Dock = DockStyle.Top;
            PathTB.Location = new Point(3, 3);
            PathTB.Name = "PathTB";
            PathTB.ReadOnly = true;
            PathTB.Size = new Size(206, 23);
            PathTB.TabIndex = 4;
            PathTB.Text = "C:\\Program Files (x86)\\Microlight3D\\";
            // 
            // selectFolderBtn
            // 
            selectFolderBtn.Dock = DockStyle.Top;
            selectFolderBtn.Location = new Point(215, 3);
            selectFolderBtn.MaximumSize = new Size(30, 0);
            selectFolderBtn.Name = "selectFolderBtn";
            selectFolderBtn.Size = new Size(30, 23);
            selectFolderBtn.TabIndex = 5;
            selectFolderBtn.Text = "...";
            selectFolderBtn.UseVisualStyleBackColor = true;
            selectFolderBtn.Click += selectFolderBtn_Click;
            // 
            // btnInstall
            // 
            tableLayoutPanel2.SetColumnSpan(btnInstall, 2);
            btnInstall.Dock = DockStyle.Fill;
            btnInstall.Location = new Point(127, 101);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(215, 33);
            btnInstall.TabIndex = 4;
            btnInstall.Text = "Install Phaos";
            btnInstall.UseVisualStyleBackColor = true;
            btnInstall.Click += btnInstall_Click;
            // 
            // progressBar
            // 
            tableLayoutPanel2.SetColumnSpan(progressBar, 3);
            progressBar.Dock = DockStyle.Fill;
            progressBar.ForeColor = Color.Lime;
            progressBar.Location = new Point(3, 72);
            progressBar.MarqueeAnimationSpeed = 75;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(339, 23);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 8;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(lblTitle, 3);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(339, 20);
            lblTitle.TabIndex = 10;
            lblTitle.Text = "Installing XXX version XXX";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UCMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel2);
            MinimumSize = new Size(345, 135);
            Name = "UCMain";
            Size = new Size(345, 135);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel2;
        private Button btnCancelLeft;
        private Label label1;
        private CheckBox defaultPathCb;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox PathTB;
        private Button selectFolderBtn;
        private Button btnInstall;
        private ProgressBar progressBar;
        private Label lblTitle;
    }
}
