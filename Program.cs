using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Text;
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Forms;
using ML3DInstaller.View;

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

        

        
    }
}