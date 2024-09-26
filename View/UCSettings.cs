using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML3DInstaller.View
{
    public partial class UCSettings : UserControl
    {
        private bool init = false;
        public UCSettings()
        {
            InitializeComponent();
            rbShowProd.Checked = Properties.Settings.Default.ReleaseOnly;
            rbShowall.Checked = !Properties.Settings.Default.ReleaseOnly;
            cbShowTest.Checked = Properties.Settings.Default.ViewTestProject;
            init = true;
        }

        private void rbShowall_CheckedChanged(object sender, EventArgs e)
        {
            if (init)
            {
                if (rbShowall.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show("Warning: Versions that you install that are Test, Develop or PreRelease are installed at your own risk. Microlight 3D is not responsible for any misuse or problem caused by using these versions.\n Unless Microlight 3D's team advises you to use a specific version, do not use this option.\nDo you still wish to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult != DialogResult.Yes)
                    {
                        rbShowall.Checked = false;
                        rbShowProd.Checked = true;
                    }
                }
            }
        }

        private bool SaveSettings()
        {
            Properties.Settings.Default.ReleaseOnly = rbShowProd.Checked;
            Properties.Settings.Default.ViewTestProject = cbShowTest.Checked;   

            Properties.Settings.Default.Save();
            return true;
        }

        public event EventHandler Exit;
        private void button1_Click(object sender, EventArgs e)
        {
            Exit?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
            {
                MessageBox.Show("Restart software to apply changes.");
                Exit?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }
}
