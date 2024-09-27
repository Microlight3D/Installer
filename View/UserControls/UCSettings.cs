using ML3DInstaller.Presenter;
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
            cbDevMode.Checked = Properties.Settings.Default.DeveloperMode;
            cbUsePAT.Checked = Properties.Settings.Default.UseGitPAT;
            tbGitPAT.Text = Properties.Settings.Default.GithubApiToken;

            tlpDevMode.Visible = Properties.Settings.Default.DeveloperMode;
            init = true;
        }

        private void rbShowall_CheckedChanged(object sender, EventArgs e)
        {
            if (init)
            {
                if (rbShowall.Checked)
                {
                    DialogResult dialogResult = MessageBox.Show("Warning: Versions that you install that are Test, Develop or PreRelease are installed at your own risk. Microlight 3D is not responsible for any misuse or problem caused by using these versions.\n Unless Microlight 3D's team advises you to use a specific version, do not use this option.\nDo you still wish to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
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
            Properties.Settings.Default.DeveloperMode = cbDevMode.Checked;
            Properties.Settings.Default.UseGitPAT = cbUsePAT.Checked;
            Properties.Settings.Default.GithubApiToken = tbGitPAT.Text;

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

        public event EventHandler<bool> DevMode;

        private void cbDevMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDevMode.Checked && init)
            {
                DialogResult dr = MessageBox.Show("Developer mode gives access to internal testing functions, that are not suitable for anything else than testing.\nIf you are not part of the Microlight3D Testing Team, it is strongly advised to not use this mode.\nMicrolight 3D is not responsible for any misuse or bug caused by modifications of these settings.\n\nActivate Developer Mode ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    cbDevMode.Checked = false;
                }
            }
            this.tlpDevMode.Visible = cbDevMode.Checked;
            DevMode?.Invoke(this, cbDevMode.Checked);
        }

        private void UCSettings_Load(object sender, EventArgs e)
        {
            //cbDevMode_CheckedChanged(this, EventArgs.Empty);
            cbUsePAT_CheckedChanged(this, EventArgs.Empty);
        }

        private void btnLaunchUpdate_Click(object sender, EventArgs e)
        {
            Updater.AutoUpdate(GithubAPI.VersionToInt(tbCurrentVersion.Text));
        }

        private void cbUsePAT_CheckedChanged(object sender, EventArgs e)
        {
            tlpGitPat.Enabled = cbUsePAT.Checked;
        }
    }
}
