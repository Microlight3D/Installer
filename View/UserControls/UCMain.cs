using IWshRuntimeLibrary;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.IO.Compression;
using System.Windows.Forms;
using System;
using ML3DInstaller.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ML3DInstaller.Presenter;
using System.Security.Policy;


namespace ML3DInstaller
{
    public partial class UCMain : UserControl, ProgressBarAPI
    {
        private string DefaultPath = @"C:\Program Files (x86)\Microlight3D\Phaos\";
        private string ChosenPath = @"C:\Program Files (x86)\Microlight3D\Phaos\";

        private string software;
        private string version;
        private Release SoftwareRelease;
        private string ZipBaseURL = "";

        private string Mode = "Init";

        private List<CheckBox> checkBoxes = new List<CheckBox>();

        ManualResetEventSlim progressEvent = new ManualResetEventSlim(false);

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
        public event EventHandler BackToHome;

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
        public void Init(Release release)
        {
            this.software = release.Software;
            this.version = release.Version;
            this.SoftwareRelease = release;

            DefaultPath = @"C:\Program Files (x86)\Microlight3D\" + software + @"\";
            ChosenPath = DefaultPath;

            lblTitle.Text = "Installing " + software + " version " + version;
            PathTB.Text = DefaultPath;
            btnInstall.Text = "Install " + software;
            btnCancelLeft.Visible = true;

            if (version == "Support")
            {
                btnInstall.Visible = false;
                btnCancelLeft.Enabled = true;
            }

            // Fill downloadable options
            var zips = release.Zips;
            if (zips != null && zips.Count > 0)
            {
                var uri = new Uri(zips[0]);
                ZipBaseURL = uri.GetLeftPart(UriPartial.Authority);

                int nrows = zips.Count / 2;
                this.tlpDownloadableContent.RowCount = nrows + 2;
                int col = 0;
                int row = 0;
                int iteration = 0;
                foreach (var zip in zips)
                {
                    if (iteration % 2 == 0)
                    {
                        col = 0;
                        row++;
                    }
                    string zipNoExtension = zip.Split("/")[^1].Replace(".zip", "");

                    if (!Properties.Settings.Default.ShowSupportOptions && zipNoExtension == "Dependencies")
                    {
                        // hide this one
                    }
                    else
                    {
                        CheckBox cb = new CheckBox();
                        cb.Dock = DockStyle.Top;
                        cb.Checked = true;
                        cb.Text = zipNoExtension;
                        cb.FlatStyle = FlatStyle.Popup;
                        checkBoxes.Add(cb);
                        tlpDownloadableContent.Controls.Add(cb, col, row);
                        iteration++;
                        col++;
                    }
                }
                tlpDownloadableContent.RowStyles.Clear();
                for (int i = 0; i < tlpDownloadableContent.RowCount; i++)
                {
                    if (i < tlpDownloadableContent.RowCount - 1)
                    {
                        tlpDownloadableContent.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    }
                    else
                    {
                        tlpDownloadableContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                    }
                }
            }



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

            btnCancelLeft.Text = (mode == "Init" ? "Back" : "Cancel");
            btnCancelLeft.Visible = mode == "Init";
            progressBar.Visible = mode == "Loading";

            this.Mode = mode;

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
            if (Mode == "Init")
            {
                BackToHome?.Invoke(this, null);
            }
        }


        /// <summary>
        /// Launch installation process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInstall_Click(object sender, EventArgs e)
        {
            bool bypass = false;
            btnInstall.Visible = false;
            btnCancelLeft.Enabled = true;
            InstallSoftware?.Invoke(PathTB.Text, bypass);
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

        private float MaxProgressValue = 0;
        #region progress bar api
        public void SetMaximum(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar.Maximum = value;
                    MaxProgressValue = (float)value;
                }));
            }
            else
            {
                progressBar.Maximum = value;
                MaxProgressValue = (float)value;
            }
            RefreshNow();
        }

        public void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar.Value = progress;
                    label1.Text = "Downloading ... (" + progress + "%)";
                }));
            }
            else
            {
                progressBar.Value = progress;
                label1.Text = "Downloading ... (" + progress + "%)";
            }
            RefreshNow();
        }

        public void UpdateProgress(float progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar.Value = (int)progress;
                    label1.Text = "Downloading ... (" + progress + "%)";
                }));
            }
            else
            {
                progressBar.Value = (int)progress;
                label1.Text = "Downloading ... (" + progress + "%)";
            }
            RefreshNow();
        }

        public void SetLoadingMode(bool loadingMode)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    if (loadingMode)
                    {
                        progressBar.Style = ProgressBarStyle.Continuous;
                    }
                    else
                    {
                        progressBar.Style = ProgressBarStyle.Marquee;
                    }
                }));
            }
            else
            {
                if (loadingMode)
                {
                    progressBar.Style = ProgressBarStyle.Continuous;
                }
                else
                {
                    progressBar.Style = ProgressBarStyle.Marquee;
                }
            }
            RefreshNow();
        }

        public void RefreshNow()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    label1.Refresh();
                    progressBar.Update();
                    progressBar.Refresh();
                    this.Refresh();
                    Application.DoEvents();
                }));
            }
            else
            {
                label1.Refresh();
                progressBar.Update();
                progressBar.Refresh();
                this.Refresh();
                Application.DoEvents();
            }

        }

        public void UpdateProgress(double progress)
        {
            if (progress != progressBar.Value)
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        progressBar.Value = (int)progress;
                        label1.Text = "Downloading ... (" + progress + "%)";
                    }));
                }
                else
                {
                    progressBar.Value = (int)progress;
                    label1.Text = "Downloading ... (" + progress + "%)";
                }
                RefreshNow();
            }

        }

        public void EndProgress()
        {
            progressEvent.Set();
        }

        public void StartProgress()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar.Value = 0;
                }));
            }
            else
            {
                progressBar.Value = 0;
            }
            RefreshNow();
            progressEvent.Wait();
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in checkBoxes)
            {
                checkBox.Checked = checkBox1.Checked;
            }
            tlpDownloadableContent.Enabled = !checkBox1.Checked;
        }
    }
}
