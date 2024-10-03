namespace ML3DInstaller.View.Forms
{
    partial class Form2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            button1 = new Button();
            panel1 = new Panel();
            markdownLabel1 = new CustomControls.MarkdownLabel();
            markdownRichTextBox1 = new CustomControls.MarkdownRichTextBox();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(button1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(markdownRichTextBox1, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Load File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.AutoSize = true;
            panel1.Controls.Add(markdownLabel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 32);
            panel1.Name = "panel1";
            panel1.Size = new Size(394, 415);
            panel1.TabIndex = 2;
            // 
            // markdownLabel1
            // 
            markdownLabel1.Dock = DockStyle.Fill;
            markdownLabel1.Location = new Point(0, 0);
            markdownLabel1.MarkdownText = null;
            markdownLabel1.Name = "markdownLabel1";
            markdownLabel1.Size = new Size(394, 415);
            markdownLabel1.TabIndex = 0;
            // 
            // markdownRichTextBox1
            // 
            markdownRichTextBox1.BorderStyle = BorderStyle.FixedSingle;
            markdownRichTextBox1.Dock = DockStyle.Fill;
            markdownRichTextBox1.Location = new Point(403, 32);
            markdownRichTextBox1.Name = "markdownRichTextBox1";
            markdownRichTextBox1.ReadOnly = true;
            markdownRichTextBox1.Size = new Size(394, 415);
            markdownRichTextBox1.TabIndex = 3;
            markdownRichTextBox1.Text = "";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form2";
            Text = "Form2";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Panel panel1;
        private CustomControls.MarkdownLabel markdownLabel1;
        private CustomControls.MarkdownRichTextBox markdownRichTextBox1;
    }
}