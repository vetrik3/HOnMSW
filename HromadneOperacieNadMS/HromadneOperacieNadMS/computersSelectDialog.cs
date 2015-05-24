using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HromadneOperacieNadMS
{
    public partial class computersSelectDialog : Form
    {
        private TaskOptions parentTaskOptions;
        private MainWindow parentMainWindow;
        private const int Online = 0;
        private const int Offline = 1;

        public computersSelectDialog(TaskOptions parent2)
        {
            InitializeComponent();
            parentTaskOptions = parent2;
        }

        public computersSelectDialog(MainWindow parent2)
        {
            InitializeComponent();
            parentMainWindow = parent2;
        }

        private void computersSelectDialog_Load(object sender, EventArgs e)
        {
            string[] computersInfoFiles = Directory.GetFiles(@".\Computers\", "*.my");
            foreach (string computerFile in computersInfoFiles)
            {
                DateTime LastModifik = new System.IO.FileInfo(computerFile).LastWriteTime;
                DateTime Date_Now = DateTime.Now;
                TimeSpan duration = Date_Now - LastModifik;
                List<string> textLines = File.ReadAllLines(computerFile).ToList();
                ListViewItem computerItem = new ListViewItem();
                if (duration.TotalSeconds < 120)
                {
                    computerItem.ImageIndex = Online;
                }
                else
                {
                    computerItem.ImageIndex = Offline;
                }
                foreach (string line in textLines)
                {
                    if (line != "")
                    {
                        if (line.Contains("Computer Name||"))
                        {
                            string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                            computerItem.SubItems.Add(splitter[1]);
                        }
                        if (line.Contains("MacAddress||"))
                        {
                            string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                            computerItem.SubItems.Add(splitter[1]);
                            break;
                        }
                    }
                }
                listViewComputers.Items.Add(computerItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> TargetComputers = new List<string>();
            foreach (ListViewItem selectedComputer in listViewComputers.SelectedItems)
            {
                TargetComputers.Add(selectedComputer.SubItems[1].Text);
            }
            if (parentTaskOptions != null)
            {
                parentTaskOptions.textBoxTargetComputers.Text = String.Join(",", TargetComputers.ToArray());
                this.Close();
            }
            else
            {
                parentMainWindow.clientsForUpdate = String.Join(",", TargetComputers.ToArray());
                this.Close();
            }
        }
    }
}
