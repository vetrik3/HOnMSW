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
    public partial class ComputerDetails : Form
    {
        public string ComputerName;
        public ComputerDetails()
        {
            InitializeComponent();
        }

        private void ComputerDetails_Load(object sender, EventArgs e)
        {
            string fileName = @".\Computers\" + ComputerName + ".my";
            if (File.Exists(fileName))
            {
                 List<string> textLines = File.ReadAllLines(fileName).ToList();
                 foreach (string line in textLines)
                 {
                     if(line != "")
                     {
                         if(line.Contains("||"))
                         {
                             string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                             ListViewItem computerItem = new ListViewItem(splitter[0]);
                             computerItem.SubItems.Add(splitter[1]);
                             lstDisplayHardware.Items.Add(computerItem);
                         }
                     }
                 }
            }
            else
            {
                this.Close();
            }
        }
    }
}
