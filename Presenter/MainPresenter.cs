using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML3DInstaller.Presenter
{
    internal class MainPresenter
    {
        private bool BYPASS_INSTALL = true;
        
        private UCMain userControlMain;
        private Update Updater;

        private string Software;
        private string Version;

        private bool InstallDependencies;

        private CancellationTokenSource CancelInstall;

        private CancellationTokenSource SourceCopy;
        private CancellationTokenSource ConfigCopy;
        private CancellationTokenSource DocCopy;

        private UCDependencies dependenciesView;

        private bool InstallBlender = false;
        public MainPresenter(UCMain uCMain, string software, string version, bool installDependencies, bool IsVerbose) {

            userControlMain = uCMain;
            userControlMain.Init(software, version);

            userControlMain.ExitApp += UserControlMain_ExitApp;
            userControlMain.InstallSoftware += UserControlMain_InstallSoftware;
            userControlMain.CancelInstall += UserControlMain_CancelInstall;

            this.Software = software;
            this.Version = version;
            this.InstallDependencies = installDependencies;

            Updater = new Update(software, version, installDependencies, IsVerbose);
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

        bool InstalledCancel = false;

        private async void UserControlMain_InstallSoftware(object? sender, string outputPath)
        {
            CancelInstall = new CancellationTokenSource();
            var CancelToken = CancelInstall.Token;

            
            userControlMain.SetMode("Loading");
            try
            {
                await Task.Run(() => InstallSoftware(outputPath, CancelToken), CancelToken);
            }
            catch (OperationCanceledException)
            {
                InstalledCancel = true;
            }
            if (Updater.OperationCancelled || InstalledCancel)
            {
                MessageBox.Show("Installation Cancelled.");
            } else
            {
                MessageBox.Show("Installation Complete");
            }
            Application.Exit();
        }

        private async void InstallSoftware(string outputPath, CancellationToken cancellationToken)
        {
            // Check for internet connection
            if (!CheckInternet())
            {
                MessageBox.Show("An internet connection is required to update this software.\nPlease connect to the internet before re-trying", "No Connection detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            string githubLink = "https://github.com/Microlight3D/" + Software + "Redistribuable/releases/download/Release-" + Version + "/" + Software + ".zip";
            string outputZip = Software + "_" + Version + ".zip";

            string applicationPath =  Software + "-" + Version;
            string configurationPath = "Configuration";
            string documentationPath = "Documentation";
            string dependenciesPath = "Dependencies";

            string configurationDestinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Microlight3D\Configuration");
            string documentationDestinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Microlight3D\Documentation");

            string executablePath = Path.Combine(outputPath, Software + ".exe");

            bool downloadGit = true;
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            // bypass install when debugging
            if (!BYPASS_INSTALL)
            { 
                userControlMain.UpdateInfo("Downloading the software");
                if (!Updater.DownloadZip(githubLink, outputZip))
                {
                    MessageBox.Show("An error occured during the downloading of the software.\nPlease restart the installer and try again. If nothing changes, contact the Microlight 3D Support at support@microlight.fr", "Downloading error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    InstalledCancel = true;
                    return;
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Extract to current directory
                userControlMain.UpdateInfo("UnZipping the software");
                Updater.ExtractZip();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                SourceCopy = new CancellationTokenSource();
                ConfigCopy = new CancellationTokenSource();
                DocCopy = new CancellationTokenSource();
                // Copy software to destination directory
                userControlMain.UpdateInfo("Copy software to chosen destination");
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                Updater.CopyFolderAsync(applicationPath, outputPath, SourceCopy).Wait();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                userControlMain.UpdateInfo("Copy configuration and documentation to Documents\\Microlight3D");
                Updater.CopyFolderAsync(configurationPath, configurationDestinationPath, ConfigCopy).Wait();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                Updater.CopyFolderAsync(documentationPath, documentationDestinationPath, DocCopy).Wait();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // Create Calibration design Folder
                Updater.CreateFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Calibration design"));

                // Create Shortcut
                Updater.CreateShortcut(executablePath);
                Updater.AddShortcutToStart(executablePath);
            }
            else
            {
                MessageBox.Show("Sucessfully bypassed the software's installation");
            }

            if (InstallDependencies)
            {
                // Get the list of dependencies
                var listOfExe = Updater.GetAllExeInFolder(dependenciesPath, BYPASS_INSTALL);
                this.SelectDependencies(listOfExe);
            }
            Updater.DeleteDownloaded();
            this.userControlMain.End();
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
                return;
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
