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
        public UCSettings()
        {
            InitializeComponent();
            checkBox1_CheckedChanged(this, EventArgs.Empty);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            lblGitPAT.Visible = cbShowTest.Checked;
            lblGitPATRequired.Visible = cbShowTest.Checked;
            tbGitPAT.Visible = cbShowTest.Checked;
        }

        private void rbShowall_CheckedChanged(object sender, EventArgs e)
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

        private bool SaveSettings()
        {
            Properties.Settings.Default.ReleaseOnly = rbShowProd.Checked;
            if (cbShowTest.Checked && cbShowTest.Checked != Properties.Settings.Default.ViewTestProject)
            {
                // Show test value changed and is checked
                DialogResult dialogresult = MessageBox.Show("Warning: The github private access token (PAT) needs to be correct, and to have access to the Microlight 3D private repositories. Otherwise, the installer will not display the Test project, and unpredictable behavior might ensue.\n Are you sure your github PAT is correct ?", "Warning about PAT", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogresult.Equals(DialogResult.No))
                {
                    return false;
                }
                Properties.Settings.Default.ViewTestProject = cbShowTest.Checked;
                Properties.Settings.Default.GithubApiToken = tbGitPAT.Text;
            }
            

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
