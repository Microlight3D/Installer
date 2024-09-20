using IWshRuntimeLibrary;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.IO.Compression;
using System.Windows.Forms;
using System;


namespace ML3DInstaller
{
    public partial class UCMain : UserControl
    {
        private string DefaultPath = @"C:\Program Files (x86)\Microlight3D\Phaos\";
        private string ChosenPath = @"C:\Program Files (x86)\Microlight3D\Phaos\";

        private string software;
        private string version;

        /// <summary>
        /// App needs to be exited
        /// </summary>
        public event EventHandler ExitApp;
        public delegate void InstallSoftwareEvent(string software, bool bypass);
        /// <summary>
        /// Install the software at path \<string\>
        /// </summary>
        public event InstallSoftwareEvent InstallSoftware;
        /// <summary>
        /// Cancel the installation (copy)
        /// </summary>
        public event EventHandler CancelInstall;

        public UCMain()
        {
            InitializeComponent();

            PathTB.Text = DefaultPath;
            PathTB.ReadOnly = defaultPathCb.Checked;
            selectFolderBtn.Enabled = !defaultPathCb.Checked;

            SetMode("Init");
        }

        /// <summary>
        /// Initialize the UserControl
        /// </summary>
        /// <param name="software"></param>
        /// <param name="version"></param>
        public void Init(string software, string version)
        {
            this.software = software;
            this.version = version;

            DefaultPath = @"C:\Program Files (x86)\Microlight3D\" + software + @"\";
            ChosenPath = DefaultPath;

            lblTitle.Text = "Installing " + software + " version " + version;
            PathTB.Text = DefaultPath;
            btnInstall.Text = "Install " + software;
            btnCancelLeft.Visible = false;
        }
        /// <summary>
        /// Set mode to initialization or loading view
        /// </summary>
        /// <param name="mode"></param>
        public void SetMode(string mode)
        {
            PathTB.Visible = mode == "Init";
            selectFolderBtn.Visible = mode == "Init";
            defaultPathCb.Visible = mode == "Init";
            btnInstall.Enabled = mode == "Init";

            btnCancelLeft.Enabled = mode == "Loading";
            progressBar.Visible = mode == "Loading";

        }

        #region events handling
        /// <summary>
        /// Select the destination folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectFolderBtn_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.InitialDirectory = ChosenPath;
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    PathTB.Text = folderDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Enable or disable the modifications of path depending on the value of DefaultPathCheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultPathCb_CheckedChanged(object sender, EventArgs e)
        {
            PathTB.ReadOnly = defaultPathCb.Checked;
            selectFolderBtn.Enabled = !defaultPathCb.Checked;
            if (PathTB.ReadOnly)
            {
                ChosenPath = PathTB.Text;
                PathTB.Text = DefaultPath;
            }
            else
            {
                PathTB.Text = ChosenPath;
            }
        }

        /// <summary>
        /// Cancel installation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelLeft_Click(object sender, EventArgs e)
        {
            // CancelInstall?.Invoke(this, EventArgs.Empty);
        }


        /// <summary>
        /// Launch installation process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInstall_Click(object sender, EventArgs e)
        {
            bool bypass = false;
            if (ctrlPressed && shiftPressed)
            {
                // bypass mode
                DialogResult bypassInstall = MessageBox.Show("Bypass Installation ?\nWarning: the software won't be downloaded. SFTConverter won't be downloaded. Any .exe dependency will be ignored.\nThe installation process will go directly to dependencies.", "Bypass Installation?", MessageBoxButtons.YesNo);
                if (bypassInstall == DialogResult.Yes)
                {
                    bypass = true;
                }
            }

            btnInstall.Visible = false;
            btnCancelLeft.Enabled = true;
            InstallSoftware?.Invoke(PathTB.Text, bypass);
        }
        private bool ctrlPressed = false;
        private bool shiftPressed = false;
        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey)
            {
                Debug.WriteLine("Ctrl ..");
                ctrlPressed = true;
            }
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                Debug.WriteLine("Shift ..");
                shiftPressed = true;
            }
            
        }

        public void KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.ControlKey)
            {
                ctrlPressed = false;
            }
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                shiftPressed = false;
            }
        }

        public void UpdateInfo(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateInfo(text)));
            }
            else
            {
                label1.Text = text;
                RefreshUI();
            }

        }

        private void RefreshUI()
        {
            this.Refresh();
            Application.DoEvents();
        }
        #endregion
        #region Processing Functions


        public void UpdateProgressBar(int percent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgressBar(percent)));
            }
            else
            {
                progressBar.Value = percent;
            }
        }



        public void SetCursor(Cursor cursor)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetCursor(cursor)));
            }
            else
            {
                this.Cursor = cursor;
            }

        }

        public void End()
        {
            if (InvokeRequired && !IsDisposed)
            {
                Invoke(new Action(() => End()));
            }
            else
            {
                this.Hide();
                this.Dispose();
            }

            #endregion

        }

        
    }
}
