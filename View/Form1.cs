using ML3DInstaller.Presenter;
using ML3DInstaller.View;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
            DoCommand(new List<string>()
            {
                "-inputformat none -outputformat none -NonInteractive -Command Add-MpPreference -ExclusionPath \"",
                Environment.CurrentDirectory,
                "\""
            });
            
            /*DoCommand(new List<string>()
            {
                "-Command { ",
                "Start-Process powershell -ArgumentList \"-NoProfile -ExecutionPolicy Bypass -Command \"\"[Net.ServicePointManager]::SecurityProtocol = [Net.ServicePointManager]::SecurityProtocol -bor [Net.SecurityProtocolType]::Tls12; Try { Invoke-WebRequest -Uri 'https://community.chocolatey.org/install.ps1' -UseBasicParsing | Invoke-Expression } Catch { Write-Host 'Error occurred: ' $_.Exception.Message; Exit 1 }\"\"\" -Verb RunAs\n",
                "}"
            });*/
        }

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

        private void UcHome1_Continue(object? sender, Tuple<string, string, bool, bool> arguments)
        {
            SwitchMode("Install");
            MainPresenter mp;
            string software = arguments.Item1;
            string version = arguments.Item2.Replace("latest (", "").Replace(")", "");
                mp = new MainPresenter(ucMain1,software, version, arguments.Item3, arguments.Item4);
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
