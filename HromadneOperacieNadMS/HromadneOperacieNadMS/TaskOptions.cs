using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HromadneOperacieNadMS
{
    public partial class TaskOptions : Form
    {
        public string taskName;
        public string taskComputers;
        public string SourceDirectory;
        public string TargetDirectory;
        public List<String> TempFiles = new List<String>();
        private MainWindow parent;
        private string LastExecution;
        private bool saveDONE;
        string srcDirName;

        public void WriteToFile(List<string> Writer)
        {           
            if (!(Directory.Exists(@".\Tasks\")))
            {
                Directory.CreateDirectory(@".\Tasks\");
            }
            File.WriteAllLines(@".\Tasks\" + Writer[0], Writer.ToArray());
        }

        private void LoadfilesToListBox(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            Thread.Sleep(10);
            listBoxFiles.Invoke(new Action(() => listBoxFiles.Items.Clear()));
            listBoxFiles.Invoke(new Action(() => listBoxFiles.Items.AddRange(TempFiles.ToArray())));
        }

        private void LoadFilesToTemp(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            //treba vymazat temp aby sme predosli posielaniu suborov z predchadzajuceho vyberu
            //TempFiles.Clear();
            bool first_time = true;
            foreach (String file in openFileDialog1.FileNames)
            {
                if (!(TempFiles.Contains(file)))
                {
                    TempFiles.Add(file);
                }
                if (first_time)
                {
                    // ak sme nemali zadanu cestu kam sa to ma posielat, tak ju tam dopise
                    if (textBoxTargetDirectory.Text == "")
                    {
                        first_time = false;
                        string[] splitter = file.Split('\\');
                        string filename = splitter[splitter.Length - 1];
                        textBoxTargetDirectory.Invoke(new Action(() => textBoxTargetDirectory.Text = file.Replace('\\' + filename, "")));
                    }
                    //potrebujeme zistit zdrojovy priecinok odkial sme si vybrali subory
                    first_time = false;
                    string[] splitter2 = file.Split('\\');
                    string filename2 = splitter2[splitter2.Length - 1];
                    SourceDirectory = file.Replace('\\' + filename2, "");
                }
            }
        }

        private void LoadFilesToTempFromDirectory(object sender, Jacksonsoft.WaitWindowEventArgs e)
        {
            TempFiles.Clear();
            SourceDirectory = folderBrowserDialog1.SelectedPath;
            if (textBoxTargetDirectory.Text == "")
            {
                // ak sme nemali zadanu cestu kam sa to ma posielat, tak ju tam dopis   
                textBoxTargetDirectory.Invoke(new Action(() => textBoxTargetDirectory.Text = SourceDirectory));
            }
            NaciatanieSuborov(SourceDirectory, "");
        }

        //Nacitava rekurzivne subory a uklada ich do tempu - TempFiles
        private void NaciatanieSuborov(string odkial, string kam)
        {
            DirectoryInfo priecinok = new DirectoryInfo(odkial);


            FileInfo[] subory = priecinok.GetFiles();
            foreach (FileInfo subor in subory)
            {
                string temppath = Path.Combine(odkial, subor.Name);
                TempFiles.Add(temppath);
            }
            try
            {
                foreach (string folder in Directory.GetDirectories(odkial))
                {
                    string temppath = Path.Combine(SourceDirectory, folder);
                    NaciatanieSuborov(folder, temppath);
                }
            }
            catch (UnauthorizedAccessException) { }
        }

        public TaskOptions(MainWindow parent2)
        {
            parent = parent2;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            parent.RunTaskName = "";
            this.Close();
        }

        private void TaskOptions_Load(object sender, EventArgs e)
        {
            parent.RunTaskName = "";
            textBoxName.Text = taskName;
            textBoxTargetComputers.Text = "";
            trackBarWaitingTime.Value = 10;
            string fileName = @".\Tasks\" + taskName;
            if (File.Exists(fileName))
            {
                List<string> textLines = File.ReadAllLines(fileName).ToList();
                bool commands = false;
                bool targetDirectory = false;
                foreach (string line in textLines)
                {
                    if (line.Contains("WOL||"))
                    {
                        commands = false;
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        checkBoxWOL.Checked = Convert.ToBoolean(splitter[1]);
                    }
                    if (commands)
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        richTextBoxRunCommand.Text = richTextBoxRunCommand.Text + splitter[1];
                    }
                    if (line.Contains("TargetComputers||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        textBoxTargetComputers.Text = splitter[1];
                        if (textBoxTargetComputers.Text == "")
                        {
                            buttonExecute.Enabled = false;
                        }
                        else
                        {
                            buttonExecute.Enabled = true;
                        }
                    }
                    if (line.Contains("WaitingTime||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        trackBarWaitingTime.Value = Convert.ToInt16(splitter[1]);
                    }
                    if (line.Contains("LastExecution||"))
                    {
                        LastExecution = "";
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        if(splitter[1] != "")
                        {
                            LastExecution = splitter[1];
                        }
                    }
                    if (line.Contains("Commands||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        richTextBoxRunCommand.Text = splitter[1];
                        commands = true;
                    }                    
                    if (line.Contains("UDP||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        if (Convert.ToBoolean(splitter[1]))
                        {
                            radioButtonUDP.Checked = true;
                        }
                        else
                        {
                            radioButtonTCP.Checked = true;
                        }

                    }
                    if (line.Contains("TurnOff||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        checkBoxTurnOff.Checked = Convert.ToBoolean(splitter[1]);
                    }
                    if (line != "")
                    {
                        if (targetDirectory)
                        {
                            TempFiles.Add(line);
                        }
                    }
                    if (line.Contains("TargetDirectory||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        textBoxTargetDirectory.Text = splitter[1];
                        targetDirectory = true;
                    }
                    if (line.Contains("SourceDirectory||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        SourceDirectory = splitter[1];
                    }                   
                }
                if (TempFiles.Count != 0)
                {
                    object result = Jacksonsoft.WaitWindow.Show(this.LoadfilesToListBox, "Načítavam súbory do listu, Prosím počkajte.");
                }
            }
            else
            {
                radioButtonUDP.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
            TempFiles.Clear();
        }

        private void buttonFolderDialog_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (folderBrowserDialog1.SelectedPath != "")
                {
                    object result = Jacksonsoft.WaitWindow.Show(this.LoadFilesToTempFromDirectory, "Načítavam súbory do tempu, Prosím počkajte.");
                    // prida do listu cesty suborov
                    //kontroluje ci sme oznacili aspon jeden subor
                    if (TempFiles.Count != 0)
                    {
                        result = Jacksonsoft.WaitWindow.Show(this.LoadfilesToListBox, "Načítavam súbory do listu, Prosím počkajte.");
                    }
                }
            }
        }

        private void buttonFilesDialog_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                object result = Jacksonsoft.WaitWindow.Show(this.LoadFilesToTemp, "Načítavam súbory do tempu, Prosím počkajte.");
                if (TempFiles.Count != 0)
                {
                    result = Jacksonsoft.WaitWindow.Show(this.LoadfilesToListBox, "Načítavam súbory do listu, Prosím počkajte.");
                }
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Ste si istí, že chcete úlohu spustiť?",
                                     "Pozor!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (SaveTask(DateTime.Now.ToString()))
                {
                    parent.RunTaskName = textBoxName.Text;
                    this.Close();
                }
            }            
        }

        private bool SaveTask(string lastExecution)
        {
            List<string> Writer = new List<string>();
            if (textBoxName.Text == "")
            {
                ToolTip tt = new ToolTip();
                int VisibleTime = 2000; //in milliseconds
                tt.Show("Zadajte názov úlohy", textBoxName, 0, 0, VisibleTime);
                return false;
            }
            else
            {
                Writer.Add(textBoxName.Text);
                if (textBoxTargetComputers.Text != "")
                {
                    buttonExecute.Enabled = true;
                    Writer.Add("TargetComputers||" + textBoxTargetComputers.Text);
                    List<string> Computer_List = new List<string>();
                    if (textBoxTargetComputers.Text.Contains(','))
                    {
                        string[] split = textBoxTargetComputers.Text.Split(',');
                        foreach (string ComputerName in split)
                        {
                            Computer_List.Add(ComputerName);
                        }
                    }
                    else
                    {
                        Computer_List.Add(textBoxTargetComputers.Text);
                    }

                    if (Directory.Exists(@".\TaskDetails\"))
                    {
                        string[] TaskDetailInfoFiles = Directory.GetFiles(@".\TaskDetails\");
                        foreach (string taskDetail in TaskDetailInfoFiles)
                        {
                            List<string> textLines = File.ReadAllLines(taskDetail).ToList();
                            string Computers = "NONE";
                            foreach (string line in textLines)
                            {
                                if (line.Contains("TargetComputers||"))
                                {
                                    string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                                    if (splitter[1] != "")
                                    {
                                        Computers = splitter[1];
                                    }
                                }
                                if (line.Contains("DETAILS||"))
                                {
                                    string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                                    if (splitter[1] == "3")
                                    {
                                        foreach (string ComputerName in Computer_List)
                                        {
                                            if (Computers.Contains(','))
                                            {
                                                string[] split = Computers.Split(',');
                                                foreach (string ComputerName2 in split)
                                                {
                                                    if (ComputerName2 == ComputerName)
                                                    {
                                                        ToolTip tt = new ToolTip();
                                                        int VisibleTime = 2000;
                                                        tt.Show("Pocitac" + ComputerName + " je uz v inej vykonavajucej ulohe.", textBoxName, 0, 0, VisibleTime);
                                                        return false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (Computers == ComputerName)
                                                {
                                                    ToolTip tt = new ToolTip();
                                                    int VisibleTime = 2000;
                                                    tt.Show("Pocitac" + ComputerName + " je uz v inej vykonavajucej ulohe.", textBoxName, 0, 0, VisibleTime);
                                                    return false;
                                                }
                                            }

                                        }
                                    }
                                }
                            }

                        }
                    }
                    Writer.Add("Commands||" + richTextBoxRunCommand.Text);
                    Writer.Add("WOL||" + checkBoxWOL.Checked);
                    Writer.Add("UDP||" + radioButtonUDP.Checked);
                    Writer.Add("TurnOff||" + checkBoxTurnOff.Checked);
                    Writer.Add("LastExecution||" + lastExecution);
                    Writer.Add("WaitingTime||" + trackBarWaitingTime.Value.ToString());
                    if (TempFiles.Count != 0 && textBoxTargetDirectory.Text == "")
                    {
                        ToolTip tt = new ToolTip();
                        int VisibleTime = 2000; //in milliseconds
                        tt.Show("Musí byť zadaný cieľový priečinok", textBoxTargetDirectory, 0, 0, VisibleTime);
                        return false;
                    }
                    else
                    {
                        Writer.Add("SourceDirectory||" + SourceDirectory);
                        Writer.Add("TargetDirectory||" + textBoxTargetDirectory.Text);
                        for (int i = 0; i < TempFiles.Count; i++)
                        {
                            Writer.Add(TempFiles[i]);
                        }                        
                        WriteToFile(Writer);
                    }
                }
                else
                {
                    buttonExecute.Enabled = false;
                    ToolTip tt = new ToolTip();
                    int VisibleTime = 2000; //in milliseconds
                    tt.Show("Musia byť vybrané počítače, na ktoré sa má úloha vykonať.", textBoxTargetComputers, 0, 0, VisibleTime);
                    return false;
                }
            } 
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveTask(LastExecution);
        }

        private void buttonBrowseComputers_Click(object sender, EventArgs e)
        {
            computersSelectDialog newDialog = new computersSelectDialog(this);           
            newDialog.ShowDialog();   
            if(textBoxTargetComputers.Text != "")
            {
                buttonExecute.Enabled = true;
            }
            else
            {
                buttonExecute.Enabled = false;
            }
        }

        private void trackBarWaitingTime_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = trackBarWaitingTime.Value.ToString();
        }
    }
}
