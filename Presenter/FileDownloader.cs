using Microsoft.VisualBasic.Logging;
using ML3DInstaller.View;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ML3DInstaller.Presenter
{
    /// <summary>
    /// Download files with progress reporting and support for resuming unfinished downloads.
    /// </summary>
    public class FileDownloader
    {
        private BackgroundWorker worker;
        public event EventHandler WorkerCompleted;

        /// <summary>
        /// Bufferized download of a file, defined by its url, to a certain path.
        /// 
        /// Progress updated in real time in a FormPleaseWait.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tempFilePath"></param>
        /// <param name="formPleaseWait"></param>
        public void DownloadFileWithProgress(string url, string tempFilePath, ProgressBarAPI formPleaseWait)
        {
            // use worker to update the ui while working
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;

            worker.DoWork += (sender, e) =>
            {
                try
                {
                    long existingFileSize = 0;

                    // Check if part of the file already exists
                    if (File.Exists(tempFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(tempFilePath);
                        existingFileSize = fileInfo.Length;
                        Debug.WriteLine("Existing size: " + existingFileSize);
                    }

                    // Prepare the request
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";

                    if (existingFileSize > 0)
                    {
                        request.AddRange(existingFileSize);
                    }

                    // Download the file, taking into consideration what has already been downloaded
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        long totalBytes = response.ContentLength + existingFileSize;
                        long totalBytesRead = existingFileSize;
                        int oldProgress = 0;

                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                // Might need to upgrade this size later, it makes a lot of updates, no need for that much. 
                                byte[] buffer = new byte[8192]; // Update downloaded file after 8kb
                                int bytesRead;

                                while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    fileStream.Write(buffer, 0, bytesRead);
                                    totalBytesRead += bytesRead;

                                    int progress = (int)((totalBytesRead * 100) / totalBytes);
                                    oldProgress = progress;
                                    worker.ReportProgress(progress, new Tuple<long, long>(totalBytesRead, totalBytes));
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Report the error via RunWorkerCompleted event
                    e.Result = ex;
                }
            };
            formPleaseWait.SetMaximum(100);
            int previousPercentage = 0;
            worker.ProgressChanged += (sender, e) =>
            {
                int progressPercentage = e.ProgressPercentage;
                Tuple<long, long> bytesInfo = (Tuple<long, long>)e.UserState;
                long bytesReceived = bytesInfo.Item1;
                long totalBytes = bytesInfo.Item2;

                double value = ((double)bytesReceived / (double)totalBytes) * 100;

                int toIntPercentage = (int)value;

                if (toIntPercentage > previousPercentage)
                {
                    formPleaseWait.UpdateProgress(toIntPercentage);
                    Debug.WriteLine("Progress : " + bytesReceived+" / "+totalBytes + " - "+ value + "%");
                }
                previousPercentage = progressPercentage;
            };

            worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Result is Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                formPleaseWait.EndProgress();

                // Trigger an event or callback if needed
                WorkerCompleted?.Invoke(this, e);
            };

            // Start the download
            worker.RunWorkerAsync();
        }
    }
}
