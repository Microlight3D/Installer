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

namespace ML3DInstaller
{
    internal static class Program
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
                Application.Run(new Form1());
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
    }
}