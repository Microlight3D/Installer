using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML3DInstaller.View.Forms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult dr = ofd.ShowDialog();
            if (dr != DialogResult.OK)
            {
                return;
            }
            string filePath = ofd.FileName;
            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine(fileContent);
            markdownLabel1.MarkdownText = fileContent;
            //FillTB(fileContent);
            markdownRichTextBox1.SetText(fileContent);
        }

       
    }
}
