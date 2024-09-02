using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ML3DInstaller.Presenter
{
    /// <summary>
    /// Evertyhing needed to update a software
    /// </summary>
    internal class Update
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

        /// <summary>
        /// Initialize the Updater
        /// </summary>
        /// <param name="software">Software name eg: "Phaos"</param>
        /// <param name="version">Version number eg: "2.2.6.5"</param>
        public Update(string software, string version) 
        { 
            this.Software = software;
            this.Version = version;

            string softwarePath = software + "Redistribuable-" + version + "\\" + software + "-" + version;
            SourceSoftwarePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, softwarePath);

            SourceConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, software + "Redistribuable-" + version + "\\Configuration");
            SourceDocumentationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, software + "Redistribuable-" + version + "\\Documentation");

            CopyCancellationTokenSource = new CancellationTokenSource();
            ZipDownloadCancellationTokenSourceTask = new TaskCompletionSource<bool>();

            TempDirectory = GetTemporaryDirectory();
        }

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
        public bool CheckForInternetConnection(int timeoutMs = 10000, string url = null)
        {
            try
            {
                url ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };

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

        public bool DownloadZip(string zipUrl, string zipName)
        {
            try
            {
                DownloadedZip = TempDirectory + "\\" + zipName;
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Accept: text/html, application/xhtml+xml, */*");
                webClient.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                webClient.DownloadFile(new Uri(zipUrl), DownloadedZip);
            } catch
            {
                return false;
            }
            return true;
            
        }

        private string ExtractedZip = "";

        public void ExtractZip()
        {
            ExtractedZip = Path.Combine(TempDirectory, Software + "-" + Version);
            ZipFile.ExtractToDirectory(DownloadedZip, ExtractedZip);
        }

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
        }

        public void CreateShortcut(string exeFilePath, string outputFolderPath="desktop")
        {
            if (!OperationCancelled)
            {
                string destPath = outputFolderPath;
                if (outputFolderPath == "desktop")
                {
                    destPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                }

                string shortcutLocation = Path.Combine(destPath, Software + " " + Version + ".lnk");

                // Create a new WshShell object
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
                shortcut.TargetPath = exeFilePath;
                shortcut.WorkingDirectory = exeFilePath;
                shortcut.Description = Software + " " + Version + "\nLocation: " + exeFilePath;
                shortcut.Save();
            }
        }

        public void AddShortcutToStart(string pathToExe, string pathToIcon="")
        {
            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Microlight 3D");

            if (!Directory.Exists(appStartMenuPath))
                Directory.CreateDirectory(appStartMenuPath);

            string shortcutLocation = Path.Combine(appStartMenuPath, Software + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Microlight 3D Micro-Printing software\n"+Software+" v"+Version;
            if (!pathToIcon.Equals(""))
            {
                shortcut.IconLocation = pathToIcon;
            }
             //uncomment to set the icon of the shortcut
            shortcut.TargetPath = pathToExe;
            shortcut.Save();
        }

        /// <summary>
        /// Get the list of all Executables in a folder, recursively
        /// </summary>
        /// <param name="rootDependenciesFolder"></param>
        /// <returns></returns>
        public string[] GetAllExeInFolder(string rootDependenciesFolder)
        {
            string path = Path.Combine(ExtractedZip, rootDependenciesFolder);
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

        private void RunChocoInstallList(List<string> packagesList, bool verbose =true)
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

            if (verbose)
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
        private void InstallChoco(bool verbose=true)
        {
            StringBuilder argsBuilder = new StringBuilder();
            argsBuilder.Append("Set-ExecutionPolicy Bypass -Scope Process -Force;");
            argsBuilder.Append("[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; ");
            argsBuilder.Append("iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1')) ");

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
            else if(verbose)
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
            argsBuilder.Append("Set-ExecutionPolicy Bypass -Scope Process -Force;");
            argsBuilder.Append(package);

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
            
            
            return new Tuple<Process, StringBuilder, StringBuilder>(process,standardOutput,errorOutput);
        }

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
    }
}
