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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tableLayoutPanel1 = new TableLayoutPanel();
            ucMain1 = new UCMain();
            ucHome1 = new UCHome();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(ucMain1, 0, 0);
            tableLayoutPanel1.Controls.Add(ucHome1, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(415, 153);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // ucMain1
            // 
            ucMain1.Dock = DockStyle.Fill;
            ucMain1.Location = new Point(3, 3);
            ucMain1.MinimumSize = new Size(0, 135);
            ucMain1.Name = "ucMain1";
            ucMain1.Size = new Size(201, 147);
            ucMain1.TabIndex = 1;
            // 
            // ucHome1
            // 
            ucHome1.Dock = DockStyle.Fill;
            ucHome1.Location = new Point(210, 3);
            ucHome1.MinimumSize = new Size(343, 132);
            ucHome1.Name = "ucHome1";
            ucHome1.Size = new Size(343, 147);
            ucHome1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 153);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(427, 192);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ML3D Installer";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private UCMain ucMain1;
        private UCHome ucHome1;
    }
}
