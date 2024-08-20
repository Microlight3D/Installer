using ML3DInstaller.Presenter;
using System.Configuration;
using System.Diagnostics;
namespace ML3DInstaller
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> softwares;
        public Form1()
        {
            InitializeComponent();

            _ = new HomePresenter(ucHome1);

            string destPath = AppDomain.CurrentDomain.BaseDirectory;
            Debug.WriteLine(destPath);
            ucMain1.ExitApp += UcMain1_ExitApp;
            ucHome1.Continue += UcHome1_Continue;
            
            SwitchMode("Home");
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
