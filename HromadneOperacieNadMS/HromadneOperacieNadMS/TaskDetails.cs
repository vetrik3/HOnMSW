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
    public partial class TaskDetails : Form
    {

        public string taskDetailsName;
        public TaskDetails()
        {
            InitializeComponent();
        }

        private void TaskDetails_Load(object sender, EventArgs e)
        {
            string fileName = @".\TaskDetails\" + taskDetailsName;
            if (File.Exists(fileName))
            {
                List<string> textLines = File.ReadAllLines(fileName).ToList();
                foreach (string line in textLines)
                {
                    if (line.Contains("STEP||"))
                    {
                        ListViewItem computerStepDetails = new ListViewItem();
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        computerStepDetails.ImageIndex = Convert.ToInt16(splitter[4]);
                        computerStepDetails.SubItems.Add(splitter[1]);
                        computerStepDetails.SubItems.Add(splitter[2]);
                        computerStepDetails.SubItems.Add(splitter[3]);
                        listViewTaskDetails.Items.Add(computerStepDetails);
                    }
                }
            }
            else
            {
                MessageBox.Show("Task Details not find");
                this.Close();
            }
        }
    }
}
