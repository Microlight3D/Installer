using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ML3DInstaller.Presenter
{
    /// <summary>
    /// Download Files, makes it possible to see progress and continue unfinished download
    /// </summary>
    public class FileDownloader
    {
        public async Task DownloadFileWithProgress(string url, string tempFilePath, View.FormPleaseWait formPleaseWait)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    long existingFileSize = 0;

                    // Check if part of the file already exists
                    if (File.Exists(tempFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(tempFilePath);
                        existingFileSize = fileInfo.Length;
                        Debug.WriteLine("Existing size : " + existingFileSize);
                    }

                    // Set up the range request if file is partially downloaded
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    if (existingFileSize > 0)
                    {
                        request.Headers.Range = new System.Net.Http.Headers.RangeHeaderValue(existingFileSize, null);
                    }

                    using (HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();

                        long? totalBytes = response.Content.Headers.ContentLength;
                        if (totalBytes.HasValue)
                        {
                            totalBytes += existingFileSize;
                        }

                        // Stream the content to the file
                        using (var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            SaveFileWithProgress(tempFilePath, contentStream, totalBytes, existingFileSize, formPleaseWait).Wait() ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }


        private async Task SaveFileWithProgress(string filePath, Stream contentStream, long? totalBytes, long existingFileSize, View.FormPleaseWait formPleaseWait)
        {
            byte[] buffer = new byte[8192]; // should make this settable in settings (must be 2 power)
            long totalBytesRead = existingFileSize;
            int bytesRead;

            formPleaseWait.SetLoadingMode(true);
            formPleaseWait.SetMaximum(totalBytes.HasValue ? (int)totalBytes.Value : 100);

            // Open the file in append mode to continue downloading
            using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, 8192, true))
            {
                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
                    totalBytesRead += bytesRead;

                    if (totalBytes.HasValue)
                    {
                        int progress = (int)((totalBytesRead * 100) / totalBytes.Value);
                        formPleaseWait.UpdateProgress(progress, totalBytesRead, totalBytes.Value);
                    }
                    else
                    {
                        formPleaseWait.UpdateProgress((int)totalBytesRead, totalBytesRead, totalBytes.Value);
                    }

                    Debug.WriteLine($"Bytes read: {totalBytesRead}/{totalBytes}");
                }
            }

            Debug.WriteLine("File download completed.");
        }

    }
}
