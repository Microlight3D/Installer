using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ML3DInstaller.Presenter
{
    public static class Utils
    {
        public static DialogResult ErrorBox(string message, string title)
        {
            return MessageBox.Show(
                "Error : "+message, 
                title, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error
            );
        }

        public static DialogResult WarningBox(string message, string title, MessageBoxButtons buttons=MessageBoxButtons.OK)
        {
            return MessageBox.Show(
                "Warning : " + message, 
                title, 
                buttons, 
                MessageBoxIcon.Warning
            );
        }

        public static DialogResult QuestionBox(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.YesNo, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(
                message, 
                title, 
                buttons, 
                MessageBoxIcon.Question, 
                defaultBtn
            );
        }
        public static DialogResult InfoBox(string message, string title)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
