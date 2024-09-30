using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML3DInstaller.Presenter
{
    public static class Utils
    {
        public static DialogResult ErrorBox(string message, string title)
        {
            return MessageBox.Show(
                "Error : "+message, 
                title, 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Error
            );
        }

        public static DialogResult WarningBox(string message, string title, MessageBoxButtons buttons=MessageBoxButtons.OK)
        {
            return MessageBox.Show(
                "Warning : " + message, 
                title, 
                buttons, 
                MessageBoxIcon.Warning
            );
        }

        public static DialogResult QuestionBox(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.YesNo, MessageBoxDefaultButton defaultBtn = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(
                message, 
                title, 
                buttons, 
                MessageBoxIcon.Question, 
                defaultBtn
            );
        }
        public static DialogResult InfoBox(string message, string title)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
