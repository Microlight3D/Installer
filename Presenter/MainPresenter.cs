using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ML3DInstaller.Presenter
{
    internal class MainPresenter
    {
        private bool BYPASS_INSTALL = false;
        
        private UCMain userControlMain;
        private Updater Updater;

        private string Software;
        private string Version;

        private Release CurrentRelease;

        private bool InstallDependencies;

        private CancellationTokenSource CancelInstall;

        private CancellationTokenSource SourceCopy;
        private CancellationTokenSource ConfigCopy;
        private CancellationTokenSource DocCopy;

        private UCDependencies dependenciesView;

        private bool InstallBlender = false;
        public MainPresenter(UCMain uCMain, Release release, bool installDependencies, bool IsVerbose) {

            userControlMain = uCMain;
            userControlMain.Init(release);

            userControlMain.ExitApp += UserControlMain_ExitApp;
            userControlMain.InstallSoftware += UserControlMain_InstallSoftware;
            userControlMain.CancelInstall += UserControlMain_CancelInstall;

            this.Software = release.Software;
            this.Version = release.Version;
            this.InstallDependencies = installDependencies;

            Updater = new Updater(release, installDependencies, IsVerbose);
        }

        

        private void UserControlMain_CancelInstall(object? sender, EventArgs e)
        {
            SourceCopy?.Cancel();
            ConfigCopy?.Cancel();
            DocCopy?.Cancel();
            CancelInstall?.Cancel();
        }

        private bool CheckInternet()
        {
            userControlMain.UpdateInfo("Checking for internet connection ...");
            bool internetAvailable = Updater.CheckForInternetConnection();
            if (!internetAvailable)
            {
                MessageBox.Show("No internet connection was found.\nPlease Connect to the internet before restarting the installation process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            userControlMain.SetCursor(Cursors.WaitCursor);
            return true;
        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            if (File.Exists(tempDirectory))
            {
                return GetTemporaryDirectory();
            }
            else
            {
                Directory.CreateDirectory(tempDirectory);
                return tempDirectory;
            }
        }

        bool InstalledCancel = false;

        private async void UserControlMain_InstallSoftware(List<string> zipsToProcess, string outputPath, bool bypass)
        {
            CancelInstall = new CancellationTokenSource();
            var CancelToken = CancelInstall.Token;
            BYPASS_INSTALL = bypass;

            int totalZip = zipsToProcess.Count;
            int current = 1;

            foreach (string zip in zipsToProcess)
            {
                string softwareName = zip.Split('/')[^1].Replace(".zip", "");
                userControlMain.SetMode("Loading");
                userControlMain.SetIteration(current, totalZip);
                string thisZipOutputPath = Path.Combine(outputPath, softwareName);
                if (softwareName == "Documentation")
                {
                    thisZipOutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Microlight3D");
                }
                try
                {
                    await Task.Run(() => OneZipProcess(zip, softwareName, thisZipOutputPath,CancelToken), CancelToken);
                }
                catch (OperationCanceledException)
                {
                    InstalledCancel = true;
                }
                if (Updater.OperationCancelled || InstalledCancel)
                {
                    MessageBox.Show("Installation Cancelled.");
                    Application.Exit();
                }
                current += 1;
            }
            userControlMain.SetIteration(-1, totalZip);
            Updater.DeleteZips();
            Application.Exit();
        }

        /// <summary>
        /// Download a zip, unzip it, copy to correct path, create shortcut if necessary, and delete temp files
        /// </summary>
        /// <param name="downloadURL">url to download the zip</param>
        /// <param name="softwareName">name of the software for GUI usages</param>
        /// <param name="outputPath">location to copy content of the zip</param>
        private async void OneZipProcess(string downloadURL, string softwareName, string outputPath, CancellationToken cancellationToken, string version="")
        {
            Debug.WriteLine("One zip process");
            if (!CheckInternet())
            {
                MessageBox.Show("An internet connection is required to install "+softwareName+"\nPlease connect to the internet before re-trying", "No Connection detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string zipName = softwareName + ".zip";

            // if outputpath is "", find one in temp
            if (outputPath == "")
            {
                outputPath = GetTemporaryDirectory();
            }

            // Download the zip
            userControlMain.UpdateInfo("Downloading "+softwareName+" ...");
            Debug.WriteLine("Download zip "+softwareName);

            var ZipDownloadResult = Updater.DownloadZip(downloadURL, softwareName+".zip", userControlMain);
            if (!ZipDownloadResult.Item1)
            {
                MessageBox.Show("An error occured during the downloading of the software.\nPlease restart the installer and try again. If nothing changes, contact the Microlight 3D Support at support@microlight.fr", "Downloading error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InstalledCancel = true;
                return;
            }
            Debug.WriteLine("Extract zip " + softwareName);

            // Unzip the zip
            userControlMain.UpdateInfo("UnZipping "+zipName+" ...");
            if (!Updater.ExtractZip(ZipDownloadResult.Item2, outputPath))
            {
                return;
            }
            Debug.WriteLine("grant access" + softwareName);

            // Give all the rights to the destination folder
            userControlMain.UpdateInfo("Grant access for execution");
            Updater.GrantFullControl(outputPath);

            // If software is "Dependencies", we need to launch all the processes
            if (softwareName == "Dependencies")
            {
                var listOfExe = Updater.GetAllExeInFolder(outputPath);
                this.SelectDependencies(listOfExe);
            }
            Debug.WriteLine("create shprtcuts " + softwareName);

            // Create Shortcuts
            userControlMain.UpdateInfo("Create shortcuts ...");
            string exePath = outputPath + "\\" + softwareName + ".exe";
            if (File.Exists(exePath))
            {
                Updater.CreateShortcut(exePath, softwareName, version, "desktop");
                Updater.CreateShortcut(exePath, softwareName, version, "startmenu");
            }

            // Refresh desktop to add shortcuts
            RefreshDesktop();
        }

        [System.Runtime.InteropServices.DllImport("Shell32.dll")]
        private static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);
        /// <summary>
        /// Same as a Desktop -> Right click -> Refresh 
        /// </summary>
        private void RefreshDesktop()
        {
            SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
        }

        private Form DependenciesForm;

        public void SelectDependencies(string[] availableExes)
        {
            List<string> chocoList = new List<string>();
            chocoList.AddRange(new string[]
            {
                "dotnetcore-runtime --version 3.1.32 ",
                "netfx-4.8",
                "vcredist-all",
                "windirstat",
                "klayout",
                "teamviewer",
                "termite",
                "imagej",
                "!blender"
            });
            this.dependenciesView = new UCDependencies();
            dependenciesView.SetItems(availableExes);
            dependenciesView.SetChoco(chocoList);
            dependenciesView.Cancel += (sender, e) =>
            {
                dependenciesView.Hide();
                dependenciesView.Dispose();
                Application.Exit();
            };
            dependenciesView.Continue += DependenciesView_Continue;

            DependenciesForm = new Form();
            DependenciesForm.Text = "Select Dependencies";
            DependenciesForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            DependenciesForm.StartPosition = FormStartPosition.CenterScreen;
            DependenciesForm.MinimizeBox = false;
            DependenciesForm.MaximizeBox = false;
            DependenciesForm.ClientSize = new Size(300, 600);

            DependenciesForm.Controls.Add(dependenciesView);
            dependenciesView.Dock = DockStyle.Fill;
            DependenciesForm.ShowDialog();
        }
        private void DependenciesView_Continue(object? sender, string[] dependenciesToInstall)
        {
            DependenciesForm?.Hide();
            DependenciesForm?.Dispose();
            userControlMain.UpdateInfo("Installing Dependencies ...");
            Updater.RunChocoInstalls += Updater_RunChocoInstalls;
            Updater.RunExecutablesList(dependenciesToInstall);
        }

        private void Updater_RunChocoInstalls(object? sender, EventArgs e)
        {
            userControlMain.UpdateInfo("The installation process isn't over, please wait ...");
        }

        private void UserControlMain_ExitApp(object? sender, EventArgs e)
        {
            userControlMain.Hide();
            
        }
    }
}
