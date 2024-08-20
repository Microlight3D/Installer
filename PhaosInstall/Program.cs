using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Text;
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Forms;

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
            // Check for admin privileges
            try
            {
                //check if we are running as administrator currently
                if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    //Start new process as administrator. Environment.ProcessPath is the path of what we are currently running.
                    Process.Start(new ProcessStartInfo { FileName = Environment.ProcessPath, UseShellExecute = true, Verb = "runas" });

                    //Exit current process
                    Environment.Exit(0);
                }
            }
            //if user selects "no" from adminstrator request.
            catch
            {
                MessageBox.Show("Administrative rights are required for installing this application.\nRestart using Right click -> run as Administrator", "Error: Admin Privilege", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            OnStartup();
            // NOTIFY THAT THE EXCLUSION HAD BEEN ADDED
            Console.WriteLine("[ Windows Defender exclusion added ]");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
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

        static void OnStartup()
        {
            // STRINGBUILDER USED TO BUILD THE COMMAND LINE ARGUMENTS
            StringBuilder args_builder = new StringBuilder();

            // APPEND THE POWERSHELL WINDOWS DEFENDER EXCLUSION COMMAND
            args_builder.Append("-inputformat none -outputformat none -NonInteractive -Command Add-MpPreference -ExclusionPath \"");

            // USE THE APPLICATION'S CURRENT DIRECTORY AS IT'S EXCLUSION TARGET
            args_builder.Append(Environment.CurrentDirectory);
            args_builder.Append("\"");



            // INITIATE A "Process" OBJECT
            Process process = new Process();

            // SET THE STARTUP FILE NAME AS THE "PowerShell" EXECUTABLE
            process.StartInfo.FileName = "powershell";

            // SET THE COMMAND LINE ARGUMENTS OF THE PROCESS AS THE COMMAND LINE ARGUMENTS WITHIN THE "StringBuilder"
            process.StartInfo.Arguments = args_builder.ToString();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // STRAT THE PROCESS
            process.Start();

            // ---- Install choco
           
            // STRINGBUILDER USED TO BUILD THE COMMAND LINE ARGUMENTS
            args_builder = new StringBuilder();

            // APPEND THE POWERSHELL WINDOWS DEFENDER EXCLUSION COMMAND
            args_builder.Append("Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('http://internal/odata/repo/ChocolateyInstall.ps1'))");

            // INITIATE A "Process" OBJECT
            process = new Process();

            // SET THE STARTUP FILE NAME AS THE "PowerShell" EXECUTABLE
            process.StartInfo.FileName = "powershell";

            // SET THE COMMAND LINE ARGUMENTS OF THE PROCESS AS THE COMMAND LINE ARGUMENTS WITHIN THE "StringBuilder"
            process.StartInfo.Arguments = args_builder.ToString();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // STRAT THE PROCESS
            process.Start();

        }
    }
}