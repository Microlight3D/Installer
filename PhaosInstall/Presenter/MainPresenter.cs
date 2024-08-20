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
        private UCMain userControlMain;
        private Update Updater;

        private string Software;
        private string Version;

        private bool installDependencies;

        private CancellationTokenSource CancelInstall;

        private CancellationTokenSource SourceCopy;
        private CancellationTokenSource ConfigCopy;
        private CancellationTokenSource DocCopy;

        private UCDependencies dependenciesView;
        public MainPresenter(UCMain uCMain, string software, string version, string installDependencies) {

            userControlMain = uCMain;
            userControlMain.Init(software, version);

            userControlMain.ExitApp += UserControlMain_ExitApp;
            userControlMain.InstallSoftware += UserControlMain_InstallSoftware;
            userControlMain.CancelInstall += UserControlMain_CancelInstall;

            this.Software = software;
            this.Version = version;
            this.installDependencies = installDependencies == "True";

            Updater = new Update(software, version);
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

        private async void UserControlMain_InstallSoftware(object? sender, string outputPath)
        {
            CancelInstall = new CancellationTokenSource();
            var CancelToken = CancelInstall.Token;

            bool installedCancel = false;
            userControlMain.SetMode("Loading");
            try
            {
                await Task.Run(() => InstallSoftware(outputPath, CancelToken), CancelToken);
            }
            catch (OperationCanceledException)
            {
                installedCancel = true;
            }
            if (Updater.OperationCancelled || installedCancel)
            {
                MessageBox.Show("Installation Cancelled.");
            } else
            {
                MessageBox.Show("Installation Complete");
                Application.Exit();
            }
        }

        private async void InstallSoftware(string outputPath, CancellationToken cancellationToken)
        {
            // Check for internet connection
            if (!CheckInternet())
            {
                MessageBox.Show("An internet connection is necessary to update this software.\nPlease connect to the internet before re-trying", "No Connection detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            userControlMain.UpdateInfo("Downloading the software from Github");
            Updater.DownloadZip(githubLink, outputZip);
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
            // Copy Configuration and Documentation to Documents
            // Create Shortcut
            Updater.CreateShortcut(executablePath);

            if (installDependencies)
            {
                // Get the list of dependencies
                var listOfExe = Updater.GetAllExeInFolder(dependenciesPath);
                this.SelectDependencies(listOfExe);
            }
            Updater.DeleteDownloaded();
        }

        public void SelectDependencies(string[] availableExes)
        {
            this.dependenciesView = new UCDependencies();
            dependenciesView.SetItems(availableExes);
            dependenciesView.Cancel += (sender, e) =>
            {
                dependenciesView.Hide();
                dependenciesView.Dispose();
                return;
            };
            dependenciesView.Continue += DependenciesView_Continue;

            using (var dialogForm = new Form())
            {
                dialogForm.Text = "Select Dependencies";
                dialogForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialogForm.StartPosition = FormStartPosition.CenterScreen;
                dialogForm.MinimizeBox = false;
                dialogForm.MaximizeBox = false;
                dialogForm.ClientSize = new Size(300, 600);

                dialogForm.Controls.Add(dependenciesView);
                dependenciesView.Dock = DockStyle.Fill;

                dialogForm.ShowDialog();
            }
        }
        private void DependenciesView_Continue(object? sender, string[] dependenciesToInstall)
        {
            Updater.RunExecutablesList(dependenciesToInstall);
            dependenciesView.Hide();
            dependenciesView.Dispose();
            this.userControlMain.Hide();
            this.userControlMain.Dispose();
            Application.Exit();
            
        }
        private void UserControlMain_ExitApp(object? sender, EventArgs e)
        {
            userControlMain.Hide();
            
        }

    }
}
