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
                    { Name: var n } when n.StartsWith("fa") => // Iran
                        "http://www.aparat.com",
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

        /// <summary>
        /// Open all executable files in the list of files
        /// </summary>
        /// <param name="executableList"></param>
        public void RunExecutablesList(string[] executableList)
        {
            foreach (string executable in executableList)
            {
                try
                {
                    if (executable.StartsWith("choco"))
                    {
                        RunChocoInstall(executable);
                    } else
                    {
                        ExeInstall(executable);
                    }
                } catch { continue; } // if something fails, just continue installing stuff
            }
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

        private void RunChocoInstall(string package)
        {
            StringBuilder args_builder = new StringBuilder();
            args_builder.Append(package);

            Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = args_builder.ToString();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
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

        

        private void DependenciesView_Continue(object? sender, List<string> dependenciesToInstall)
        {
            foreach (string dependency in dependenciesToInstall)
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = dependency
                    }
                };
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
