namespace ML3DInstaller
{
    partial class UCHome
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
            lblInfo = new Label();
            label1 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            label3 = new Label();
            cbVersion = new ComboBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblSoftware = new Label();
            cbSoftware = new ComboBox();
            button1 = new Button();
            checkBox1 = new CheckBox();
            cbVerbose = new CheckBox();
            markdownRichTextBox1 = new View.CustomControls.MarkdownRichTextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.6666641F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel1.Controls.Add(markdownRichTextBox1, 0, 5);
            tableLayoutPanel1.Controls.Add(lblInfo, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(button1, 1, 3);
            tableLayoutPanel1.Controls.Add(checkBox1, 0, 3);
            tableLayoutPanel1.Controls.Add(cbVerbose, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(417, 286);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(lblInfo, 2);
            lblInfo.Dock = DockStyle.Top;
            lblInfo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfo.Location = new Point(3, 24);
            lblInfo.Margin = new Padding(3, 3, 3, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(411, 19);
            lblInfo.TabIndex = 1;
            lblInfo.Text = "Please select a software and version and continue.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(411, 21);
            label1.TabIndex = 0;
            label1.Text = "Welcome to Microlight3D Software Installer";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 46);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(411, 48);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(label3, 0, 0);
            tableLayoutPanel4.Controls.Add(cbVersion, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(205, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(206, 48);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 10F);
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(200, 19);
            label3.TabIndex = 0;
            label3.Text = "Version :";
            // 
            // cbVersion
            // 
            cbVersion.Dock = DockStyle.Top;
            cbVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVersion.FormattingEnabled = true;
            cbVersion.Location = new Point(3, 22);
            cbVersion.MinimumSize = new Size(163, 0);
            cbVersion.Name = "cbVersion";
            cbVersion.Size = new Size(200, 23);
            cbVersion.TabIndex = 1;
            cbVersion.SelectedIndexChanged += cbVersion_SelectedIndexChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(lblSoftware, 0, 0);
            tableLayoutPanel3.Controls.Add(cbSoftware, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(205, 48);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // lblSoftware
            // 
            lblSoftware.AutoSize = true;
            lblSoftware.Dock = DockStyle.Top;
            lblSoftware.Font = new Font("Segoe UI", 10F);
            lblSoftware.Location = new Point(3, 0);
            lblSoftware.Name = "lblSoftware";
            lblSoftware.Size = new Size(199, 19);
            lblSoftware.TabIndex = 0;
            lblSoftware.Text = "Software : ";
            // 
            // cbSoftware
            // 
            cbSoftware.Dock = DockStyle.Top;
            cbSoftware.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSoftware.FormattingEnabled = true;
            cbSoftware.Location = new Point(3, 22);
            cbSoftware.Name = "cbSoftware";
            cbSoftware.Size = new Size(199, 23);
            cbSoftware.TabIndex = 1;
            cbSoftware.SelectedIndexChanged += cbSoftware_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Right;
            button1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(296, 100);
            button1.MinimumSize = new Size(0, 30);
            button1.Name = "button1";
            tableLayoutPanel1.SetRowSpan(button1, 2);
            button1.Size = new Size(118, 44);
            button1.TabIndex = 3;
            button1.Text = "Continue";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Dock = DockStyle.Top;
            checkBox1.Location = new Point(3, 100);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(271, 19);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Run Full Installation";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // cbVerbose
            // 
            cbVerbose.AutoSize = true;
            cbVerbose.Location = new Point(3, 125);
            cbVerbose.Name = "cbVerbose";
            cbVerbose.Size = new Size(101, 19);
            cbVerbose.TabIndex = 5;
            cbVerbose.Text = "Verbose Install";
            cbVerbose.UseVisualStyleBackColor = true;
            cbVerbose.Visible = false;
            // 
            // markdownRichTextBox1
            // 
            markdownRichTextBox1.BackColor = Color.White;
            markdownRichTextBox1.BorderStyle = BorderStyle.None;
            markdownRichTextBox1.Dock = DockStyle.Fill;
            markdownRichTextBox1.ForeColor = Color.FromArgb(33, 33, 33);
            markdownRichTextBox1.Location = new Point(3, 150);
            markdownRichTextBox1.Name = "markdownRichTextBox1";
            markdownRichTextBox1.ReadOnly = true;
            markdownRichTextBox1.Size = new Size(271, 133);
            markdownRichTextBox1.TabIndex = 8;
            markdownRichTextBox1.Text = "";
            // 
            // UCHome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(343, 148);
            Name = "UCHome";
            Size = new Size(417, 286);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label lblInfo;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private Label lblSoftware;
        private ComboBox cbSoftware;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label3;
        private ComboBox cbVersion;
        private Button button1;
        private CheckBox checkBox1;
        private CheckBox cbVerbose;
        private View.CustomControls.MarkdownRichTextBox markdownRichTextBox1;
    }
}
