using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Text;
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Forms;
using ML3DInstaller.View;
using System.Reflection;
using ML3DInstaller.View.Forms;

namespace ML3DInstaller
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            ApplicationConfiguration.Initialize();
            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string inner = ex.InnerException != null ? ex.InnerException.Message : "";
                MessageBox.Show("An error occured : \n" + ex.Message +"\n"+inner +"\n" + ex.StackTrace);
            }
            
        }

        public static string GetVersion()
        {
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            string versionString = fileVersionInfo.FileVersion;
            while (versionString.EndsWith(".0"))
            {
                versionString = versionString.Substring(0, versionString.Length - 2);
            }
            return versionString;
        }

        public static void RestartSoftware()
        {
            //Start new process as administrator. Environment.ProcessPath is the path of what we are currently running.
            Process.Start(new ProcessStartInfo { FileName = Environment.ProcessPath, UseShellExecute = true, Verb = "runas" });

            //Exit current process
            Environment.Exit(0);
            
        }
    }
}