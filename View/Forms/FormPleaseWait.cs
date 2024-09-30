using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ML3DInstaller.View
{
    public partial class FormPleaseWait : Form, ProgressBarAPI
    {
        public FormPleaseWait()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public void SetMaximum(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar1.Maximum = value;
                }));
            }
            else
            {
                progressBar1.Maximum = value;
            }
            RefreshNow();
        }

        public void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar1.Value = progress;
                }));
            }
            else
            {
                progressBar1.Value = progress;
            }
            RefreshNow();
        }

        public void UpdateProgress(float progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar1.Value = (int)progress;
                }));
            }
            else
            {
                progressBar1.Value = (int)progress;
            }
            RefreshNow();
        }

        public void SetLoadingMode(bool loadingMode)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    if (loadingMode)
                    {
                        progressBar1.Style = ProgressBarStyle.Continuous;
                    }
                    else
                    {
                        progressBar1.Style = ProgressBarStyle.Marquee;
                    }
                }));
            }
            else
            {
                if (loadingMode)
                {
                    progressBar1.Style = ProgressBarStyle.Continuous;
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                }
            }
            RefreshNow();
        }

        public void RefreshNow()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    label1.Refresh();
                    progressBar1.Update();
                    progressBar1.Refresh();
                    this.Refresh();
                    Application.DoEvents();
                }));
            }
            else
            {
                label1.Refresh();
                progressBar1.Update();
                progressBar1.Refresh();
                this.Refresh();
                Application.DoEvents();
            }
        }

        public void UpdateProgress(double progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar1.Value = (int)progress;
                }));
            }
            else
            {
                progressBar1.Value = (int)progress;
            }
            RefreshNow();
        }

        public void EndProgress()
        {
            this.Close();
        }

        public void StartProgress()
        {
            this.ShowDialog();
        }
    }
}
