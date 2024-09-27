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
    public partial class FormPleaseWait : Form
    {
        public FormPleaseWait()
        {
            InitializeComponent();
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

        public void UpdateProgress(int progress, long bytesRead, long totalBytes)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    progressBar1.Value = progress;
                    label1.Text = $"Downloaded {bytesRead} of {totalBytes} bytes.";
                }));
            }
            else
            {
                progressBar1.Value = progress;
                label1.Text = $"Downloaded {bytesRead} of {totalBytes} bytes.";
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

        private void RefreshNow()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    label1.Refresh();
                    progressBar1.Refresh();
                    this.Refresh();
                    Application.DoEvents();
                }));
            }
            else
            {
                label1.Refresh();
                progressBar1.Refresh();
                this.Refresh();
                Application.DoEvents();
            }
        }
    }
}
