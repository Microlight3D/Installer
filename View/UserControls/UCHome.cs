using ML3DInstaller.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ML3DInstaller.Presenter.GithubAPI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ML3DInstaller
{
    public partial class UCHome : UserControl
    {
        Dictionary<string, List<Release>> Softwares;
        public event EventHandler<Tuple<Release, bool, bool>> Continue;
        public UCHome()
        {
            InitializeComponent();
        }

        public void SetSoftwares(Dictionary<string, List<Release>> softwares)
        {
            cbSoftware.Items.Clear();
            this.Softwares = softwares;
            int selectedIndex = 0;
            foreach (var key in softwares.Keys.Select((key, i) => new { i, key })) 
            {
                cbSoftware.Items.Add(key.key);
                if (key.key == Properties.Settings.Default.LastUsedSoftware)
                {
                    selectedIndex = key.i;
                }
            }
            cbSoftware.SelectedIndex = selectedIndex;
            UpdateVersions();
        }

        private void searchForUpdates()
        {
            string urlLuminis = "https://api.github.com/repos/Microlight3D/LuminisRedistribuable/releases";
            string urlPhaos = "https://api.github.com/repos/Microlight3D/PhaosRedistribuable/releases";


            SetSoftwares(null);
            lblInfo.Text = "Please select a software and version and continue.";
            cbSoftware.Enabled = true;
            cbVersion.Enabled = true;
        }


        private void UpdateVersions()
        {
            List<Release> versions = Softwares[this.cbSoftware.GetItemText(this.cbSoftware.SelectedItem)];
            cbVersion.Items.Clear();
            int latestIndex = 0;
            for (int i = 0; i < versions.Count; i++)
            {
                cbVersion.Items.Add(versions[i].StringVersion);
                if (versions[i].IsLatest)
                {
                    latestIndex = i;
                }
            }
            cbVersion.SelectedIndex = latestIndex;
        }

        private void cbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVersions();
        }

        /// <summary>
        /// Change readme info when changing version of the release
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Release> versions = Softwares[this.cbSoftware.GetItemText(this.cbSoftware.SelectedItem)];
            string version = this.cbVersion.GetItemText(this.cbVersion.SelectedItem);
            foreach (Release release in versions)
            {
                if (release.StringVersion.Equals(version))
                {
                    markdownRichTextBox1.SetText(release.Body);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] versionSplit = cbVersion.GetItemText(cbVersion.SelectedItem).Split(" ");
            string version = versionSplit[0];

            List<Release> currentSoftwareReleases = Softwares[cbSoftware.GetItemText(cbSoftware.SelectedItem)];
            // get release by version
            Release currentRelease = GithubAPI.GetReleaseByVersion(currentSoftwareReleases, version);
            if (currentRelease.Type != ReleaseType.None)
            {
                Tuple<Release, bool, bool> args = new Tuple<Release, bool, bool>(
                    currentRelease,
                    checkBox1.Checked,
                    cbVerbose.Checked
                );
                Properties.Settings.Default.LastUsedSoftware = cbSoftware.GetItemText(cbSoftware.SelectedItem);
                Properties.Settings.Default.Save();
                Continue?.Invoke(this, args);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("A full installation will re-install all the dependencies. \nDo you wish to continue ?", "Dependencies (Re-)installation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    checkBox1.Checked = false;
                }
            }
            cbVerbose.Visible = checkBox1.Checked;
        }

        
    }
}
