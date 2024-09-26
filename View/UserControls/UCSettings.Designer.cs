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
            lblSeparator = new Label();
            label3 = new Label();
            cbShowTest = new CheckBox();
            btnSave = new Button();
            button1 = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(lblSeparator, 0, 2);
            tableLayoutPanel1.Controls.Add(label3, 0, 3);
            tableLayoutPanel1.Controls.Add(cbShowTest, 0, 4);
            tableLayoutPanel1.Controls.Add(btnSave, 1, 7);
            tableLayoutPanel1.Controls.Add(button1, 0, 7);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(335, 150);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(329, 20);
            label1.TabIndex = 0;
            label1.Text = "Releases";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
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
            tableLayoutPanel2.Size = new Size(335, 50);
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
            label2.Size = new Size(142, 19);
            label2.TabIndex = 2;
            label2.Text = "(Recommended)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblSeparator
            // 
            lblSeparator.BackColor = Color.Silver;
            tableLayoutPanel1.SetColumnSpan(lblSeparator, 2);
            lblSeparator.Dock = DockStyle.Top;
            lblSeparator.Location = new Point(10, 70);
            lblSeparator.Margin = new Padding(10, 0, 10, 0);
            lblSeparator.Name = "lblSeparator";
            lblSeparator.Size = new Size(315, 1);
            lblSeparator.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label3, 2);
            label3.Dock = DockStyle.Top;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(3, 71);
            label3.Name = "label3";
            label3.Size = new Size(329, 20);
            label3.TabIndex = 3;
            label3.Text = "Developer Options";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // cbShowTest
            // 
            cbShowTest.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(cbShowTest, 2);
            cbShowTest.Dock = DockStyle.Top;
            cbShowTest.Location = new Point(3, 94);
            cbShowTest.Name = "cbShowTest";
            cbShowTest.Size = new Size(329, 19);
            cbShowTest.TabIndex = 4;
            cbShowTest.Text = "Show \"Test\" Project";
            cbShowTest.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.AutoSize = true;
            btnSave.Dock = DockStyle.Right;
            btnSave.Location = new Point(232, 118);
            btnSave.MinimumSize = new Size(100, 0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Dock = DockStyle.Left;
            button1.Location = new Point(3, 118);
            button1.MinimumSize = new Size(100, 0);
            button1.Name = "button1";
            button1.Size = new Size(100, 29);
            button1.TabIndex = 9;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // UCSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            MaximumSize = new Size(371, 193);
            Name = "UCSettings";
            Size = new Size(335, 150);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton rbShowProd;
        private RadioButton rbShowall;
        private Label label2;
        private Label lblSeparator;
        private Label label3;
        private CheckBox cbShowTest;
        private Button btnSave;
        private Button button1;
    }
}
