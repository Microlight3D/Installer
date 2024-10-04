using ML3DInstaller.Presenter;
using ML3DInstaller.View;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using static ML3DInstaller.Presenter.GithubAPI;
namespace ML3DInstaller
{
    public partial class MainForm : Form
    {
        Dictionary<string, List<string>> softwares;
        public MainForm()
        {
            BeforeAnything();
            InitializeComponent();

            _ = new HomePresenter(ucHome1);

            string destPath = AppDomain.CurrentDomain.BaseDirectory;
            Debug.WriteLine(destPath);
            ucMain1.ExitApp += UcMain1_ExitApp;
            ucMain1.BackToHome += UcMain1_BackToHome;
            ucMain1.InstallSoftware += UcMain1_InstallSoftware;
            ucHome1.Continue += UcHome1_Continue;

            lblDevMode.Visible = Properties.Settings.Default.DeveloperMode;

            SwitchMode("Home");

            this.SizeChanged += Form1_SizeChanged;
        }

        /// <summary>
        /// Execute powershell functions before anything else in the execution
        /// </summary>
        private void BeforeAnything()
        {
            FormPleaseWait fpw = new FormPleaseWait();
            fpw.StartPosition = FormStartPosition.CenterScreen;
            fpw.Show();
            // Check for admin privileges
            try
            {
                //check if we are running as administrator currently
                if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    Program.RestartSoftware();
                }
            }
            catch
            {
                MessageBox.Show("Administrative rights are required for installing this application.\nRestart using Right click -> run as Administrator", "Error: Admin Privilege", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            

            // Check for access to github
            if (!GithubAPI.IsGithubAccessible())
            {
                Utils.ErrorBox("No access to github was detected. Please chez your internet connection or contact the support. \n\nNote: www.github.com's access is restricted in certain countries. If that is the case at your location, this installer is not a suitable way to install this software.", "No access to server");
                Environment.Exit(0);
            }

            // Check for updates
            Updater.AutoUpdate(GithubAPI.VersionToInt(Program.GetVersion()));

            OnStartup();
            fpw.Hide();
            // NOTIFY THAT THE EXCLUSION HAD BEEN ADDED
            Console.WriteLine("[ Windows Defender exclusion added ]");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // Delete installer if it is required
            if (Properties.Settings.Default.DeleteInstaller)
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), @"Microlight3D_TempVars\Installer\ML3DInstallerSetup.exe");
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
                Properties.Settings.Default.DeleteInstaller = false;
                Properties.Settings.Default.Save();
            }

            // Check if a download was currently ongoing
            if (Properties.Settings.Default.CurrentlyDownloadingURL != null && Properties.Settings.Default.CurrentlyDownloadingURL != "")
            {
                if (Utils.QuestionBox("An interrupted download has been detected.\n" +
                    "Press 'yes' and restart the download with the same settings and the process will continue, or press 'No' to cancel onging download\n" +
                    "Continue the ongoing download ?", "Download pending ..") == DialogResult.No)
                {
                    Updater.DeleteTempDir();
                    Updater.ResetDownloadSettings();
                }
            
            } else
            {
                try
                {
                    Updater.DeleteTempDir();
                }
                catch {
                    // The only reason for an error is if the Installer setup is running, and so the installer setup.exe can't be removed
                    Utils.ErrorBox("Error: The temporary files' folder couldn't be cleared.\nThis is likely due to the ML3DInstallerSetup.exe already running. Please finish the installation and close it before restarting this software.\nIf it's not running, try re-booting this computer.", "Can't delete previous installer");
                    Application.Exit();
                }
                
            }
        }

        

        /// <summary>
        /// Execute command(s) on startup
        /// </summary>
        static void OnStartup()
        {
            DoCommand(new List<string>()
            {
                "-inputformat none -outputformat none -NonInteractive -Command Add-MpPreference -ExclusionPath \"",
                Environment.CurrentDirectory,
                "\""
            });
        }

        /// <summary>
        ///  Execute a powershell command in a process, and waits for it to exit
        /// </summary>
        /// <param name="commands"></param>
        private static void DoCommand(List<string> commands)
        {
            // STRINGBUILDER USED TO BUILD THE COMMAND LINE ARGUMENTS
            StringBuilder args_builder = new StringBuilder();

            foreach (string s in commands)
            {
                args_builder.Append(s);
            }
            Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = args_builder.ToString();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();
            process.WaitForExit();
        }
        /// <summary>
        /// Continue to installation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arguments">software name, software version, installDependencies, isVerbose</param>
        private void UcHome1_Continue(object? sender, Tuple<Release, bool, bool> arguments)
        {
            SwitchMode("Install");

            MainPresenter mp = new MainPresenter(ucMain1, arguments.Item1, arguments.Item2, arguments.Item3);
        }
        /// <summary>
        /// Move from install mode to home mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcMain1_BackToHome(object? sender, EventArgs e)
        {
            SwitchMode("Home");

        }
        /// <summary>
        /// Switch the visible elements in this form
        /// </summary>
        /// <param name="mode"></param>
        private void SwitchMode(string mode)
        {
            tableLayoutPanel1.ColumnStyles.Clear();
            if (mode == "Install")
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0F));
                this.Size = new Size(427, 288);
            }
            else
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                this.Size = new Size(427, 520);
            }
            if (!Properties.Settings.Default.DeveloperMode)
            {
                this.Size = new Size(this.Width, this.Height - 17); // remove the height associated with the "developer mode" banner
            }
        }
        /// <summary>
        /// Exit the software
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcMain1_ExitApp(object? sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Open settings by clicking on the menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settingsForm = Utils.FormSettings();

            settingsForm.ShowDialog(this);
            
        }

        /// <summary>
        /// Open about by clicking on the menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.StartPosition = FormStartPosition.CenterParent;
            about.SetVersion(Program.GetVersion());
            about.Show(this);
        }


        private void UcMain1_InstallSoftware(List<string> zipsToProcess, string software, bool bypass)
        {
            this.Size = new Size(427, 120);
            if (!Properties.Settings.Default.DeveloperMode)
            {
                this.Size = new Size(427, 103);
            }
        }

        private void Form1_SizeChanged(object? sender, EventArgs e)
        {
            Debug.WriteLine("New size : " + this.Size.ToString());
        }
    }
}
