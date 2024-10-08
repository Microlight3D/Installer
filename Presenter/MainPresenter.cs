﻿using System;
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
        private bool BYPASS_INSTALL = false;
        
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

        private async void UserControlMain_InstallSoftware(string outputPath, bool bypass)
        {
            CancelInstall = new CancellationTokenSource();
            var CancelToken = CancelInstall.Token;
            BYPASS_INSTALL = bypass;


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
            string sftConverterPath = "SFTConverter";
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
                Updater.ExtractZip(outputZip);
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                SourceCopy = new CancellationTokenSource();
                ConfigCopy = new CancellationTokenSource();
                DocCopy = new CancellationTokenSource();
                // Copy software to destination directory
                userControlMain.UpdateInfo("Install software to chosen destination");
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                Updater.CopyFolderAsync(applicationPath, outputPath, SourceCopy).Wait();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                // if Phaos, also copy SFTConverter
                if (Software.Equals("Phaos"))
                {
                    userControlMain.UpdateInfo("Install SFTConverter to chosen destination");
                    string basedir = Directory.GetParent(Directory.GetParent(outputPath).FullName).FullName;
                    string sftPath = Path.Combine(basedir, sftConverterPath);
                    Updater.CopyFolderAsync(sftConverterPath, sftPath, SourceCopy).Wait();
                    string exePath = Path.Combine(sftPath, "SFTConverter.exe");
                    Updater.CreateShortcut(exePath, sftConverterPath,"", "desktop");
                    Updater.CreateShortcut(exePath, sftConverterPath, "", "startmenu");

                    string calibrationDesignOutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Microlight3D\Calibration Designs");

                    try
                    {
                        // Copy Calbiration Design
                        // Create Calibration design Folder
                        Updater.CreateFolder(calibrationDesignOutputPath);
                        Updater.CopyFolderAsync("Calibration Designs", calibrationDesignOutputPath, SourceCopy).Wait();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occured with the creation of the Calibration Designs directory. Continuing installation ...");
                    }
                    
                } else if (Software.Equals("Luminis"))
                {
                    // Copy configuration in luminis 
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
                }

                // Copy user manuals 

                userControlMain.UpdateInfo("Copy configuration and documentation to Documents\\Microlight3D");
                Updater.CopyFolderByFileTypeAsync("", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"Microlight3D"), ".pdf", SourceCopy).Wait();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                

                // Create Shortcut
                Updater.CreateShortcut(executablePath, Software, Version, "desktop");
                Updater.CreateShortcut(executablePath, Software, Version, "startmenu");

                // Refresh desktop to remove old shortcuts 
                RefreshDesktop();
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
