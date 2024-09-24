using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML3DInstaller.View
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        public void SetVersion(string version)
        {
            this.lblVersion.Text = version;
        }

        private void About_Load(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2, Owner.Location.Y + Owner.Height / 2 - Height / 2);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMail(true);
        }

        private void SendMail(bool mailOnly)
        {
            string mailTo = "mailto:support@microlight.fr";
            if (!mailOnly)
            {
                string title = "[Bug Report] Issue with " + lblName.Text + " version " + lblVersion.Text;

                string body = Uri.EscapeDataString(
                    "Software name: " + lblName.Text + Environment.NewLine +
                    "Software version: " + lblVersion.Text + Environment.NewLine +
                    "Date of the encounter: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm") + Environment.NewLine + Environment.NewLine
                );

                mailTo += $"?subject={title}&body={body}";
            }
            Process.Start(new ProcessStartInfo(mailTo) { UseShellExecute = true });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMail(false);
        }
    }
}
