using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ML3DInstaller
{
    public partial class UCHome : UserControl
    {
        Dictionary<string, List<string>> softwares;
        public event EventHandler<Tuple<string, string, bool, bool>> Continue;
        public UCHome()
        {
            InitializeComponent();
        }

        public void SetSoftwares(Dictionary<string, List<string>> softwares)
        {
            this.softwares = softwares;
            foreach (string key in softwares.Keys)
            {
                cbSoftware.Items.Add(key);
            }
            cbSoftware.SelectedIndex = 0;
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
            List<string> versions = softwares[this.cbSoftware.GetItemText(this.cbSoftware.SelectedItem)];
            cbVersion.Items.Clear();
            cbVersion.Items.Add("latest (" + versions[0] + ")");
            for (int i=1; i<versions.Count; i++)
            {
                cbVersion.Items.Add(versions[i]);
            }
            cbVersion.SelectedIndex = 0;
        }

        private void cbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVersions();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tuple<string, string, bool, bool> args = new Tuple<string, string, bool, bool>(
                cbSoftware.GetItemText(cbSoftware.SelectedItem),
                cbVersion.GetItemText(cbVersion.SelectedItem),
                checkBox1.Checked,
                cbVerbose.Checked
            );
            Continue?.Invoke(this, args);
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
