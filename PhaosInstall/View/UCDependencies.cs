using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML3DInstaller
{
    public partial class UCDependencies : UserControl
    {
        private Dictionary<string, string> nameToPath;
        public UCDependencies()
        {
            InitializeComponent();
            nameToPath = new Dictionary<string, string>();
        }

        #region public api
        public event EventHandler<string[]> Continue;
        public event EventHandler Cancel;

        public void SetItems(string[] items)
        {
            foreach (string item in items)
            {
                nameToPath[Path.GetFileName(item)] = item;
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Add(Path.GetFileName(item)), true);
            }
        }

        public void SetChoco(List<string> chocoItems)
        {
            foreach (string item in chocoItems)
            {
                string rItem = item.Replace("!", "");
                checkedListBox1.SetItemChecked(checkedListBox1.Items.Add(rItem), !item.StartsWith("!"));
                nameToPath[rItem] = "choco install -y " + rItem;
            }
        }
        #endregion

        #region event forwarding
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        private void btnContinue_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            foreach (string item in this.checkedListBox1.CheckedItems)
            {
                items.Add(nameToPath[item]);
            }
            Continue?.Invoke(this, items.ToArray());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            // if anything is now unchecked, and the select all was checked, uncheck it
            if (checkedListBox1.Items.Count != checkedListBox1.CheckedItems.Count)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, !checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex));
        }
    }
}
