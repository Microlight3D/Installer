using ML3DInstaller.Presenter;
using ML3DInstaller.View;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
namespace ML3DInstaller
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> softwares;
        public Form1()
        {
            BeforeAnything();
            InitializeComponent();

            _ = new HomePresenter(ucHome1);

            string destPath = AppDomain.CurrentDomain.BaseDirectory;
            Debug.WriteLine(destPath);
            ucMain1.ExitApp += UcMain1_ExitApp;
            ucHome1.Continue += UcHome1_Continue;
            
            SwitchMode("Home");
        }

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
            fpw.Hide();
            // NOTIFY THAT THE EXCLUSION HAD BEEN ADDED
            Console.WriteLine("[ Windows Defender exclusion added ]");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
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
            process.WaitForExit();

            // ---- Install choco

            // STRINGBUILDER USED TO BUILD THE COMMAND LINE ARGUMENTS
            args_builder = new StringBuilder();

            // APPEND THE POWERSHELL WINDOWS DEFENDER EXCLUSION COMMAND
            args_builder.Append("-Command { ");
            args_builder.Append("[Net.ServicePointManager]::SecurityProtocol = [Net.ServicePointManager]::SecurityProtocol -bor [Net.SecurityProtocolType]::Tls12;Set-ExecutionPolicy Bypass -Scope Process -Force;iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1')); ");
            args_builder.Append("}");

            // INITIATE A "Process" OBJECT
            process = new Process();

            // SET THE STARTUP FILE NAME AS THE "PowerShell" EXECUTABLE
            process.StartInfo.FileName = "powershell";

            // SET THE COMMAND LINE ARGUMENTS OF THE PROCESS AS THE COMMAND LINE ARGUMENTS WITHIN THE "StringBuilder"
            process.StartInfo.Arguments = args_builder.ToString();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // STRAT THE PROCESS
            process.Start();
            process.WaitForExit();

        }

        private void UcHome1_Continue(object? sender, string[] e)
        {
            SwitchMode("Install");
            MainPresenter mp;
            string software = e[0];
            string version = e[1].Replace("latest (", "").Replace(")", "");
                mp = new MainPresenter(ucMain1,software, version, e[2]);
        }

        private void SwitchMode(string mode)
        {
            tableLayoutPanel1.ColumnStyles.Clear();
            if (mode == "Install")
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0F));
            } else
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            }
        }

        private void UcMain1_ExitApp(object? sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
