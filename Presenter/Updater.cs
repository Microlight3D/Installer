using IWshRuntimeLibrary;
using ML3DInstaller.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static ML3DInstaller.Presenter.GithubAPI;

namespace ML3DInstaller.Presenter
{
    /// <summary>
    /// Evertyhing needed to update a software
    /// </summary>
    public class Updater
    {
        private readonly string Software;
        private readonly string Version;

        private readonly string SourceSoftwarePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PhaosRedistribuable\\Phaos-2.2.6.5");
        private readonly string SourceConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PhaosRedistribuable\\Configuration");
        private readonly string SourceDocumentationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PhaosRedistribuable\\Documentation");
        private readonly string DestinationFolderConfig = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Microlight3D");

        private readonly string TempDirectory;

        private readonly CancellationTokenSource CopyCancellationTokenSource;
        private readonly TaskCompletionSource<bool> ZipDownloadCancellationTokenSourceTask;

        public bool OperationCancelled { get; private set; } = false;

        private readonly bool installDependencies;
        private readonly bool installVerbose;

        /// <summary>
        /// Initialize the Updater
        /// </summary>
        /// <param name="software">Software name eg: "Phaos"</param>
        /// <param name="version">Version number eg: "2.2.6.5"</param>
        public Updater(string software, string version, bool installDependencies, bool verboeInstall) 
        { 
            this.Software = software;
            this.Version = version;
            this.installDependencies = installDependencies;
            if (installDependencies)
            {
                this.installVerbose = verboeInstall;
            }
            else
            {
                this.installVerbose = false;
            }
            
            string softwarePath = software + "Redistribuable-" + version + "\\" + software + "-" + version;
            SourceSoftwarePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, softwarePath);

            SourceConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, software + "Redistribuable-" + version + "\\Configuration");
            SourceDocumentationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, software + "Redistribuable-" + version + "\\Documentation");

            CopyCancellationTokenSource = new CancellationTokenSource();
            ZipDownloadCancellationTokenSourceTask = new TaskCompletionSource<bool>();

            TempDirectory = GetTemporaryDirectory();
        }

        /// <summary>
        /// Get a temporary directory in C:\Users\USERNAME\AppData\Local\Temp\
        /// </summary>
        /// <returns></returns>
        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            if (System.IO.File.Exists(tempDirectory))
            {
                return GetTemporaryDirectory();
            }
            else
            {
                Directory.CreateDirectory(tempDirectory);
                return tempDirectory;
            }
        }

        /// <summary>
        /// Cancel the installation using the installation token
        /// </summary>
        public void CancelInstall()
        {
            CopyCancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Detect if internet connection is available 
        /// https://stackoverflow.com/a/2031831/13812144
        /// </summary>
        /// <param name="timeoutMs"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url = "https://github.com/Microlight3D/PhaosRedistribuable"; // always check github anyways

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                return false;
            }
        }

        private string DownloadedZip = "";
        /// <summary>
        /// Download a .zip file from the internet.
        /// The zip file will be written inside the TempDirectory 
        /// </summary>
        /// <param name="zipUrl">url to download</param>
        /// <param name="zipName">output file name (without path)</param>
        /// <returns></returns>
        public bool DownloadZip(string zipUrl, string zipName)
        {
            try
            {
                DownloadedZip = TempDirectory + "\\" + zipName;

                // Create an instance of HttpClient
                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromMinutes(30) })
                {
                    // Header to reduce risk of being flagged as a bot
                    client.DefaultRequestHeaders.Add("Accept", "text/html, application/xhtml+xml, */*");
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");

                    // Download using HttpClient
                    HttpResponseMessage response = client.GetAsync(zipUrl).Result;
                    response.EnsureSuccessStatusCode();
                    byte[] fileBytes = response.Content.ReadAsByteArrayAsync().Result;

                    // Save the file to the specified path
                    System.IO.File.WriteAllBytes(DownloadedZip, fileBytes);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private string ExtractedZip = "";

        /// <summary>
        /// Extract zip file (name without a path, the path is the tempdirectory)
        /// </summary>
        public void ExtractZip(string zipName)
        {
            ExtractedZip = Path.Combine(TempDirectory, "");
            ZipFile.ExtractToDirectory(DownloadedZip, ExtractedZip);
        }

        /// <summary>
        /// Launches a Task to copy full directory to destination
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task CopyFolderAsync(string sourceFolder, string destinationFolder, CancellationTokenSource cancellationTokenSource)
        {
            if (!OperationCancelled)
            {
                Task copyTask = CopyFolder(Path.Combine(ExtractedZip, sourceFolder), destinationFolder, cancellationTokenSource.Token);
                try
                {
                    await copyTask;
                }
                catch (OperationCanceledException)
                {
                    OperationCancelled = true;
                }
                catch (Exception ex)
                {
                    OperationCancelled = true;
                }
            }
        }

        /// <summary>
        /// Launches a Task to copy only certain files, non-recursively, to a folder
        /// </summary>
        /// <param name="sourceFolderPath"></param>
        /// <param name="destinationFolderPath"></param>
        /// <param name="fileType"></param>
        /// <param name="cancellationTokenSource"></param>
        /// <returns></returns>
        public async Task CopyFolderByFileTypeAsync(string sourceFolderPath, string destinationFolderPath, string fileType, CancellationTokenSource cancellationTokenSource)
        {
            if (!OperationCancelled)
            {
                Task copyTask = CopyFolderByFileType(Path.Combine(ExtractedZip, sourceFolderPath), destinationFolderPath, fileType, cancellationTokenSource.Token);
                try
                {
                    await copyTask;
                }
                catch (OperationCanceledException)
                {
                    OperationCancelled = true;
                }
                catch (Exception ex)
                {
                    OperationCancelled = true;
                }
            }
        }

        /// <summary>
        /// Grants everyone access to all files in directory
        /// </summary>
        /// <param name="directoryPath">directory to fully authorize</param>
        void GrantFullControl(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

            // Add the "Everyone" group with full control permissions
            directorySecurity.AddAccessRule(new FileSystemAccessRule(
                "Everyone",
                FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow));

            directoryInfo.SetAccessControl(directorySecurity);
        }
        /// <summary>
        /// Create folder at certain destination
        /// </summary>
        /// <param name="destinationFolder"></param>
        public void CreateFolder(string destinationFolder)
        {
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
        }

        /// <summary>
        /// Copy Folder to different folder, then grants everyone access to all files in folder. 
        /// </summary>
        /// <param name="sourceFolderPath"></param>
        /// <param name="destinationFolderPath"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task CopyFolder(string sourceFolderPath, string destinationFolderPath, CancellationToken token)
        {
            var allFiles = Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories);
            int totalFiles = allFiles.Length;
            int copiedFiles = 0;
            string destinationPath = destinationFolderPath;

            foreach (var filePath in allFiles)
            {
                token.ThrowIfCancellationRequested();

                var relativePath = Path.GetRelativePath(sourceFolderPath, filePath);
                var destFilePath = Path.Combine(destinationPath, relativePath);

                var destDir = Path.GetDirectoryName(destFilePath);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                System.IO.File.Copy(filePath, destFilePath, true);

                copiedFiles++;
            }
            GrantFullControl(destinationPath);
        }
        /// <summary>
        /// Copies files to different folder, then grants everyone access to all files in folder. 
        /// </summary>
        /// <param name="sourceFolderPath"></param>
        /// <param name="destinationFolderPath"></param>
        /// <param name="fileType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task CopyFolderByFileType(string sourceFolderPath, string destinationFolderPath, string fileType, CancellationToken token)
        {
            var filesToCopy = Directory.GetFiles(sourceFolderPath, $"*{fileType}", SearchOption.TopDirectoryOnly);
            int totalFiles = filesToCopy.Length;
            int copiedFiles = 0;

            foreach (var filePath in filesToCopy)
            {
                token.ThrowIfCancellationRequested(); // Check if cancellation is requested

                var fileName = Path.GetFileName(filePath);
                var destFilePath = Path.Combine(destinationFolderPath, fileName);

                var destDir = Path.GetDirectoryName(destFilePath);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                System.IO.File.Copy(filePath, destFilePath, true);

                copiedFiles++;
            }
            GrantFullControl(destinationFolderPath);
        }

        /// <summary>
        /// Create a shortcut to either the Desktop or Start Menu
        /// </summary>
        /// <param name="exeFilePath">Path to the executable</param>
        /// <param name="softwareName">Name of the software</param>
        /// <param name="version">Version of the software (optional)</param>
        /// <param name="outputLocation">Location for the shortcut: "desktop" or "startmenu" (desktop by default)</param>
        /// <param name="iconPath">Path to the icon (optional)</param>
        public void CreateShortcut(string exeFilePath, string softwareName, string version = "", string outputLocation = "desktop", string iconPath = "")
        {
            if (OperationCancelled)
            {
                return;
            }

            string shortcutFolderPath;

            // Determine the destination folder based on the outputLocation parameter
            if (outputLocation == "desktop")
            {
                shortcutFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            else if (outputLocation == "startmenu")
            {
                string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
                shortcutFolderPath = Path.Combine(commonStartMenuPath, "Programs", "Microlight 3D");

                if (!Directory.Exists(shortcutFolderPath))
                {
                    Directory.CreateDirectory(shortcutFolderPath);
                }
            }
            else
            {
                // invalid output location
                return;
            }

            // Build the shortcut name
            string shortcutName = softwareName;
            if (!string.IsNullOrEmpty(version))
            {
                shortcutName += " " + version;
            }
            shortcutName += ".lnk";

            // Create the full shortcut path
            string shortcutLocation = Path.Combine(shortcutFolderPath, shortcutName);

            // Delete any existing shortcut with same name
            string shortcutPattern = softwareName + " *.lnk";
            string[] existingShortcuts = Directory.GetFiles(shortcutFolderPath, shortcutPattern);

            foreach (string shortcutToRm in existingShortcuts)
            {
                System.IO.File.Delete(shortcutToRm);
            }

            // Create the shortcut
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.TargetPath = exeFilePath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(exeFilePath);
            shortcut.Description = softwareName + " " + version + "\nLocation: " + exeFilePath;

            if (!string.IsNullOrEmpty(iconPath))
            {
                shortcut.IconLocation = iconPath;
            }

            shortcut.Save();
        }


        /// <summary>
        /// Get the list of all Executables in a folder, recursively
        /// </summary>
        /// <param name="rootDependenciesFolder"></param>
        /// <returns></returns>
        public string[] GetAllExeInFolder(string rootDependenciesFolder, bool bypass= false)
        {
            string path = Path.Combine(ExtractedZip, rootDependenciesFolder);
            if (bypass)
            {
                return new string[] { };
            }
            return Directory.GetFiles(path, "*.exe", SearchOption.AllDirectories);
        }

        public event EventHandler RunChocoInstalls; 

        /// <summary>
        /// Open all executable files in the list of files
        /// </summary>
        /// <param name="executableList"></param>
        public void RunExecutablesList(string[] executableList)
        {
            List<string> chocoInstalls = new List<string>();
            foreach (string executable in executableList)
            {
                try
                {
                    if (executable.StartsWith("choco"))
                    {
                        chocoInstalls.Add(executable);
                    } else
                    {
                        ExeInstall(executable);
                    }
                } catch { continue; } // if something fails, just continue installing stuff
            }
            RunChocoInstalls?.Invoke(this, null);
            RunChocoInstallList(chocoInstalls);
        }

        /// <summary>
        /// Launch the executable at the given path
        /// </summary>
        /// <param name="exeFilePath"></param>
        private void ExeInstall(string exeFilePath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = exeFilePath
                }
            };
            process.Start();
            process.WaitForExit();
        }

        /// <summary>
        /// Install all chocos in list of choco
        /// </summary>
        /// <param name="packagesList"></param>
        private void RunChocoInstallList(List<string> packagesList)
        {
            StringBuilder errorOutput = new StringBuilder();
            StringBuilder stdOutput = new StringBuilder();
            List<string> failedPackages = new List<string>();
            if (packagesList != null && packagesList.Count > 0)
            {
                InstallChoco();
            }
            foreach(string package in packagesList)
            {
                Tuple<Process,StringBuilder,StringBuilder> p = RunChocoInstall(package);
                if (p.Item1.ExitCode != 0)
                {
                    failedPackages.Add(package);
                    errorOutput.Append(p.Item2);
                }
                stdOutput.Append(p.Item1);
            }
            if (failedPackages.Count > 0)
            {
                string failedPackagesString = String.Join("\n", failedPackages);
                MessageBox.Show("The following packages' installation failed : \n" + failedPackagesString
                                +"\n\n. Message log :"+stdOutput.ToString()
                                +"\n\n Error log :"+errorOutput.ToString(),
                         "Install failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (installVerbose)
            {
                MessageBox.Show("The choocolatey packages installation succeed : \n"
                    +"\n\n. Message log :"+stdOutput.ToString()
                    +"\n\n Error log :"+errorOutput.ToString(),
                    "Install Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Install chocolatey repo onto the machine for future usage
        /// </summary>
        /// <param name="verbose"> Verbose mode </param>
        private void InstallChoco()
        {
            StringBuilder argsBuilder = new StringBuilder();
            argsBuilder.Append("-NoProfile -ExecutionPolicy Bypass -Command ");
            argsBuilder.Append("\"[Net.ServicePointManager]::SecurityProtocol = [Net.ServicePointManager]::SecurityProtocol -bor [Net.SecurityProtocolType]::Tls12; ");
            argsBuilder.Append("If (-Not (Get-Command choco -ErrorAction SilentlyContinue)) { ");
            argsBuilder.Append("Try { Invoke-WebRequest -Uri 'https://community.chocolatey.org/install.ps1' -UseBasicParsing | Invoke-Expression; ");
            argsBuilder.Append("$env:PATH += ';C:\\ProgramData\\chocolatey\\bin'; [Environment]::SetEnvironmentVariable('PATH', $env:PATH, [System.EnvironmentVariableTarget]::User); ");
            argsBuilder.Append("Write-Host 'Chocolatey installed and PATH updated.' } ");
            argsBuilder.Append("Catch { Write-Host 'Error occurred: ' $_.Exception.Message; Exit 1 } ");
            argsBuilder.Append("} else { Write-Host 'Chocolatey is already installed.' }\"");
            // ^---- overly complicated code to avoid right issues

            Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = argsBuilder.ToString();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.Verb = "runas";  // 'runas' to run as administrator
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            StringBuilder standardOutput = new StringBuilder();
            process.OutputDataReceived += new DataReceivedEventHandler(delegate (Object sendingProcess, DataReceivedEventArgs outLine) 
            {
                standardOutput.Append(outLine.Data);
            });

            StringBuilder errorOutput = new StringBuilder();
            process.ErrorDataReceived += new DataReceivedEventHandler(delegate (Object sendingProcess, DataReceivedEventArgs outLine) 
            {
                errorOutput.Append(outLine.Data);
            });

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();


            if (process.ExitCode != 0)
            {
                MessageBox.Show(errorOutput.ToString(), "Error in chocolatey installation :", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if(installVerbose)
            {
                MessageBox.Show(standardOutput.ToString(), "Chocolatey installation completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Everything went well, proceeding to install chocolatey packages.", "Chocolatey installation completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            System.Environment.SetEnvironmentVariable("choco", @"C:\ProgramData\chocolatey\choco.exe");
        }

        /// <summary>
        /// Method to call for an install choco package. It generate an output report with process status and the contained output and error
        /// </summary>
        /// <param name="package"> package name to install using choco</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>
        /// <description>Process : Process status uppon exiting</description>
        /// </item>
        /// <item>
        /// <description>Standard Output : output given by the process, if verbose mode is wanted</description>
        /// </item>
        /// <item>
        /// <description>Error Output :  output logged in the process when errors occured</description>
        /// </item>
        /// </list>
        /// </returns>
        private Tuple<Process,StringBuilder,StringBuilder> RunChocoInstall(string package)
        {
            StringBuilder argsBuilder = new StringBuilder();
            argsBuilder.Append("-NoProfile -ExecutionPolicy Bypass -Command ");
            argsBuilder.Append("\"" + package + "\"");

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = argsBuilder.ToString(),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Verb = "runas",  // 'runas' to run as administrator
                    WindowStyle = ProcessWindowStyle.Hidden
                }
            };

            StringBuilder standardOutput = new StringBuilder();
            process.OutputDataReceived += (sendingProcess, outLine) => standardOutput.Append(outLine.Data);

            StringBuilder errorOutput = new StringBuilder();
            process.ErrorDataReceived += (sendingProcess, outLine) =>
            {
                errorOutput.Append(outLine.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            return new Tuple<Process, StringBuilder, StringBuilder>(process,standardOutput,errorOutput);
        }

        /// <summary>
        /// Delete downloaded zip and extracted files 
        /// </summary>
        public void DeleteDownloaded()
        {
            string zipPath = Path.Combine(TempDirectory, Software + "_" + Version + ".zip");
            string folderPath = Path.Combine(TempDirectory, Software + "-" + Version);
            if (System.IO.File.Exists(zipPath)) 
            {
                System.IO.File.Delete(zipPath);
            }
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        public static void AutoUpdate(int currentVersion)
        {
            // Check for updates
            Release latestRelease = GithubAPI.GetML3DLatest("Installer");
            if (latestRelease.Type == ReleaseType.Release)
            {
                int version = latestRelease.VersionInt;
                int current = currentVersion;
                if (current < version)
                {
                    // an update is available
                    DialogResult dialogResult = MessageBox.Show(
                        "A new version of the installer is available. Installing it is recommended to fix bugs and properly install latest versions. \nDo you wish to install it ?",
                        "Update Available",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                    );
                    if (dialogResult == DialogResult.Yes)
                    {
                        string tempFilePath = Path.Combine(Path.GetTempPath(), @"Microlight3D_TempVars\ML3DInstallerSetup.exe");
                        string tempDirPath = Path.Combine(Path.GetTempPath(), @"Microlight3D_TempVars\");
                        if (!Directory.Exists(tempDirPath))
                        {
                            Directory.CreateDirectory(tempDirPath);
                        }
                        FormPleaseWait progressForm = new FormPleaseWait();
                        progressForm.StartPosition = FormStartPosition.CenterScreen;
                        progressForm.Show();

                        FileDownloader downloader = new FileDownloader();
                        downloader.DownloadFileWithProgress("https://github.com/Microlight3D/Installer/releases/latest/download/ML3DInstallerSetup.exe", tempFilePath, progressForm).Wait();

                        Properties.Settings.Default.DeleteInstaller = true; // delete installer on next launch
                        Properties.Settings.Default.Save();

                        progressForm.Close();

                        // Launch the installer in the background
                        Process installerProcess = new Process
                        {
                            StartInfo =
                            {
                                FileName = tempFilePath,
                                UseShellExecute = true,
                                WindowStyle = ProcessWindowStyle.Hidden
                            }
                        };
                        installerProcess.Start();
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}
