using SERVER_MULTICAST;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HromadneOperacieNadMS
{
    public partial class MainWindow : Form
    {

#region Deklaracia_Premennych
        UdpClient SendingSocket;
        IPEndPoint SendingIEP;
        IPEndPoint ReceivingIEP;
        private class TemplateForExecutedTask
        {
            public Thread thread { get; set; }
            public Task2 Task { get; set; }
            public TemplateForExecutedTask()
            {
            }
        }
        List<TemplateForExecutedTask> ExecutedTasks_List = new List<TemplateForExecutedTask>();

        public ColumnClickEventArgs ColumnE;

        public string clientsForUpdate;
        public string RunTaskName;
        public string ReceiveMacAddress;
        public int WAITINGTIME;

        const int EMPTY = 0;
        const int Online = 0;
        const int Offline = 1;

        const int ERROR_MESSAGE = -4;
        const int INFO_MESSAGE = -1;
        const int ACK_FLAG = 1;
        const int SYN_FLAG = 2;
        const int CONNECT_MESSAGE = 3;
        const int ACK_CONNECT_MESSAGE = 4;
        const int TURN_OFF_MESSAGE = 5;
        const int ACK_TURN_OFF = 6; 
        const int FINISH_MESSAGE = 7;
        const int ACK_FINISH = 8;
        const int CONNECTION_FLAG = 10;
        const int RUN_COMMAND = 15;
        const int ACK_RUN_COMMAND = 16;
        const int FINISH_RUN_COMMAND = 17;
        const int ACK_FINISH_RUN_COMMAND = 18;
        
#endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UDP_IncomingData(IAsyncResult ar)
        {
            try
            {
                byte[] bResp = SendingSocket.EndReceive(ar, ref ReceivingIEP);

                FilePiece piece = FilePiece.parse_packet(bResp);

                switch (piece.state)
                {
                    case SYN_FLAG:
                        {
                            string paket_string_data = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            List<String> computerDetails_list = new List<string>(paket_string_data.Split(new string[] { "|..|" }, StringSplitOptions.None));
                            string PCName = "";
                            string MacAddress = "";
                            if (computerDetails_list.Count != 0)
                            {
                                if (computerDetails_list[0].Contains("Computer Name||"))
                                {
                                    string[] splitter = computerDetails_list[0].Split(new string[] { "||" }, StringSplitOptions.None);
                                    PCName = splitter[1];
                                }
                                if (computerDetails_list[1].Contains("MacAddress||"))
                                {
                                    string[] splitter = computerDetails_list[1].Split(new string[] { "||" }, StringSplitOptions.None);
                                    MacAddress = splitter[1];
                                }
                            }
                            ReceiveMacAddress = MacAddress;
                            System.IO.File.WriteAllLines(@".\Computers\" + PCName+ ".my", computerDetails_list);

                            string hostName = Dns.GetHostName();
                            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                            string Info_data = myIP;
                            Info_data += "#" + MacAddress;
                            byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(Info_data);

                            SendMessage(ACK_FLAG, INFO_MESSAGE, Sending_Info_Data);
                            break;
                        }
                }

                SendingSocket.BeginReceive(new AsyncCallback(UDP_IncomingData), ReceivingIEP);
            }
            catch
            {
                Console.WriteLine("SOMETHING WRONG WITH SOCKET IN MAIN");
                Create_Connections();
            }


        }

        private void Create_Connections()
        {
            try
            {
                try
                {
                    Destroy_Connections();
                }
                catch
                {

                }
                IPAddress multicastaddress = IPAddress.Parse("225.100.0.1");
                ReceivingIEP = new IPEndPoint(IPAddress.Any, 0);

                SendingIEP = new IPEndPoint(multicastaddress, 9050);

                SendingSocket = new UdpClient(ReceivingIEP);

                SendingSocket.BeginReceive(new AsyncCallback(UDP_IncomingData), ReceivingIEP);
            }
            catch
            {
                Console.WriteLine("CANNOT CREATE CONNECTION");
            }
        }

        private void Destroy_Connections()
        {
            try
            {
                SendingSocket.Close();
            }
            catch
            {

            }
        }

        private void CheckDirectories()
        {
            if (!(Directory.Exists(@".\Computers\")))
            {
                Directory.CreateDirectory(@".\Computers\");
            }
            if (!(Directory.Exists(@".\Tasks\")))
            {
                Directory.CreateDirectory(@".\Tasks\");
            }
            if (!(Directory.Exists(@".\TaskDetails\")))
            {
                Directory.CreateDirectory(@".\TaskDetails\");
            }
        }
       
        private void MainWindow_Load(object sender, EventArgs e)
        {
            listViewOptions.Items[0].Selected = true;
            listViewOptions.Select();
            CheckDirectories();
            Create_Connections();
        }

        private void listViewOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewOptions.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewOptions.SelectedIndices[0];
                int selectedTasksOption = 0;
                int selectedComputersOption = 1;
                if (intselectedindex == selectedTasksOption)
                {
                    listViewTasks.Visible = true;
                    listViewComputers.Visible = false;
                }
                if (intselectedindex == selectedComputersOption)
                {
                    listViewTasks.Visible = false;
                    listViewComputers.Visible = true;
                    timerComputersStatusReload.Interval = 100;
                }
            }
        }

        private void UpdateTask()
        {
            if (listViewTasks.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTasks.SelectedIndices[0];

                if (intselectedindex >= 0)
                {
                    string taskName = listViewTasks.Items[intselectedindex].SubItems[0].Text;
                    if (taskName != "UpdateClients")
                    {
                        TaskOptions newDialog = new TaskOptions(this);
                        string targetComputers = listViewTasks.Items[intselectedindex].SubItems[2].Text;
                        newDialog.taskName = taskName;
                        newDialog.taskComputers = targetComputers;
                        newDialog.ShowDialog();
                        if (RunTaskName != "")
                        {
                            Thread thread = new Thread(new ThreadStart(CreateThreadForTask));
                            ExecutedTasks_List.Add(new TemplateForExecutedTask());
                            ExecutedTasks_List[ExecutedTasks_List.Count - 1].thread = thread;
                            thread.Start();
                            Thread.Sleep(1000);
                            RefreshTaskDetails();
                            RefreshTasks();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Update klientov sa nedá editovať.");
                    }
                }
            }
        }

        private void listViewTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateTask();
        }

        private bool RunWithTimeout(ThreadStart threadStart, TimeSpan timeout)
        {
            Thread workerThread = new Thread(threadStart);

            workerThread.Start();

            bool finished = workerThread.Join(timeout);
            if (!finished)
                workerThread.Abort();

            return finished;
        }

        public static bool runWakeOnLan(string MacAddress)
        {
            if (MacAddress != "")
            {
                try
                {
                    MacAddress = MacAddress.Replace("-", "");
                    MacAddress = MacAddress.Replace(":", "");
                    MacAddress = MacAddress.Replace(".", "");
                    MacAddress = MacAddress.Replace("_", "");
                    MacAddress = MacAddress.Replace(" ", "");
                    MacAddress = MacAddress.Replace(",", "");
                    MacAddress = MacAddress.Replace(";", "");
                    if (MacAddress.Length != 12)
                    {
                        return false;
                    }
                    byte[] mac = new byte[6];

                    for (int k = 0; k < 6; k++)
                    {
                        mac[k] = Byte.Parse(MacAddress.Substring(k * 2, 2), System.Globalization.NumberStyles.HexNumber);
                    }

                    System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient();
                    client.Connect(IPAddress.Parse("255.255.255.255"), 40000);

                    byte[] packet = new byte[17 * 6];

                    for (int i = 0; i < 6; i++)
                        packet[i] = 0xFF;

                    for (int i = 1; i <= 16; i++)
                        for (int j = 0; j < 6; j++)
                            packet[i * 6 + j] = mac[j];

                    client.Send(packet, packet.Length);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void WakeOnLan(Task2 ExecutedTask2)
        {
            foreach(String MacAddress in ExecutedTask2.Global_TaskOptions.MacAddresses)
            {
                runWakeOnLan(MacAddress);
            }
        }

        private void WaitForClientsAtStart(Task2 ExecutedTask_Temp)
        {
            ExecutedTask_Temp.WaitingClientsMacs = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
            while (ExecutedTask_Temp.WaitingClientsMacs.Count != EMPTY)
            {
                if (ReceiveMacAddress != "")
                {
                    if (ExecutedTask_Temp.WaitingClientsMacs.Contains(ReceiveMacAddress))
                    {
                        ExecutedTask_Temp.WaitingClientsMacs.Remove(ReceiveMacAddress);
                        if (ExecutedTask_Temp.Global_TaskOptions.WakeOnLan)
                        {
                            ExecutedTask_Temp.Global_TaskOptions.AddStep(ReceiveMacAddress, "WOL", DateTime.Now.ToString(), "0");
                        }
                        else
                        {
                            ExecutedTask_Temp.Global_TaskOptions.AddStep(ReceiveMacAddress, "SYNCH", DateTime.Now.ToString(), "0");
                        }
                    }
                }
                ReceiveMacAddress = "";
                Thread.Sleep(100);
            }                      
        }

        private void WaitForClientsAtFinish(Task2 ExecutedTask_Temp)
        {
            ExecutedTask_Temp.WaitingClientsMacs = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
            while (ExecutedTask_Temp.WaitingClientsMacs.Count != EMPTY)
            {              
                var MacAddresses = String.Join(",", ExecutedTask_Temp.Global_TaskOptions.MacAddresses.ToArray());
                byte[] Sending_Data = Encoding.ASCII.GetBytes(MacAddresses);
                ExecutedTask_Temp.SendMessage(FINISH_MESSAGE, INFO_MESSAGE, Sending_Data);
                ReceiveMacAddress = "";
                Thread.Sleep(100);
            }
            for(int i = 0; i < 6; i++)
            {
                var MacAddresses = "";
                byte[] Sending_Data = Encoding.ASCII.GetBytes(MacAddresses);
                ExecutedTask_Temp.SendMessage(FINISH_MESSAGE, INFO_MESSAGE, Sending_Data);
                Thread.Sleep(100);
            }
        }

        private void WakeOnLan_Task(Task2 ExecutedTask_Temp)
        {
            if (ExecutedTask_Temp.Global_TaskOptions.MacAddresses.Count != EMPTY)
            {
                if (ExecutedTask_Temp.Global_TaskOptions.WakeOnLan)
                {
                    WakeOnLan(ExecutedTask_Temp);
                }
            }
        }

        private Task2 CreateTaskConnection(Task2 ExecutedTask_Temp, string MulticastAddress)
        {
            if (ExecutedTask_Temp.Global_TaskOptions.MacAddresses.Count != EMPTY)
            {
                ExecutedTask_Temp.Create_Connections(MulticastAddress);
            }
            return ExecutedTask_Temp;
        }

        private void TurnOffComputers(Task2 ExecutedTask_Temp)
        {
            ExecutedTask_Temp.WaitingClientsMacs = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
            while (ExecutedTask_Temp.WaitingClientsMacs.Count != EMPTY)
            {
                var MacAddresses = String.Join(",", ExecutedTask_Temp.Global_TaskOptions.MacAddresses.ToArray());
                byte[] Sending_Data = Encoding.ASCII.GetBytes(MacAddresses);
                ExecutedTask_Temp.SendMessage(TURN_OFF_MESSAGE, INFO_MESSAGE, Sending_Data);
                Thread.Sleep(1000);
            }
        }

        private void FinishTask(Task2 ExecutedTask_Temp, int NumberOfPCs)
        {
            if (NumberOfPCs > -1)
            {
                if (NumberOfPCs == ExecutedTask_Temp.Global_TaskOptions.MacAddresses.Count)
                {
                    ExecutedTask_Temp.Global_TaskOptions.EditDetailsLine("0", DateTime.Now.ToString(), NumberOfPCs.ToString());
                }
                else
                {
                    ExecutedTask_Temp.Global_TaskOptions.EditDetailsLine("1", DateTime.Now.ToString(), ExecutedTask_Temp.Global_TaskOptions.MacAddresses.Count.ToString());
                }
            }
            else
            {
                ExecutedTask_Temp.Global_TaskOptions.EditDetailsLine("1", DateTime.Now.ToString(), "0");
            }
        }

        private void SendMessageToClientsToJoin(Task2 ExecutedTask_Temp, string MulticastAddress)
        {
           ExecutedTask_Temp.WaitingClientsMacs = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
           while (ExecutedTask_Temp.WaitingClientsMacs.Count != EMPTY)
           {
               var MacAddresses = String.Join(",", ExecutedTask_Temp.Global_TaskOptions.MacAddresses.ToArray());
               byte[] Sending_Data = Encoding.ASCII.GetBytes(MacAddresses + "||" + MulticastAddress);
               SendMessage(CONNECT_MESSAGE, INFO_MESSAGE, Sending_Data);
               Sending_Data = Encoding.ASCII.GetBytes(MacAddresses);
               ExecutedTask_Temp.SendMessage(SYN_FLAG, INFO_MESSAGE, Sending_Data);
               Thread.Sleep(1000);
           }
        }

        private Task2 TurnOFF(Task2 ExecutedTask_Temp)
        {
            if (ExecutedTask_Temp.Global_TaskOptions.TurnOff)
            {
                Task.Factory.StartNew(() => TurnOffComputers(ExecutedTask_Temp)).Wait(ExecutedTask_Temp.Global_TaskOptions.WAITING_TIME);
                for (int position = 0; position < ExecutedTask_Temp.WaitingClientsMacs.Count; position++)
                {
                    ExecutedTask_Temp.Global_TaskOptions.RemoveMacAddress(ExecutedTask_Temp.WaitingClientsMacs[position]);
                }
            }
            return ExecutedTask_Temp;
        }

        private Task2 UDP_Multicast_Copy(Task2 ExecutedTask_Temp)
        {
            if(ExecutedTask_Temp.Global_TaskOptions.UDP_multicast)
            {
                if(ExecutedTask_Temp.Global_TaskOptions.TempFiles.Count != 0)
                {
                    ExecutedTask_Temp.WaitingClientsMacs = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
                    ExecutedTask_Temp.UDP_Multicast();
                    List<String> difference_MACS = ExecutedTask_Temp.WaitingClientsMacs.Except(ExecutedTask_Temp.Global_TaskOptions.MacAddresses).ToList();
                    if(difference_MACS.Count != 0)
                    {
                        for (int position = 0; position < difference_MACS.Count; position++)
                        {
                            ExecutedTask_Temp.Global_TaskOptions.RemoveMacAddress(difference_MACS[position]);
                        }
                    }
                    foreach (String MacAddress in ExecutedTask_Temp.Global_TaskOptions.MacAddresses)
                    {
                        ExecutedTask_Temp.Global_TaskOptions.AddStep(MacAddress, "UDP", DateTime.Now.ToString(), "0");
                    }                    
                }
            }
            return ExecutedTask_Temp;
        }

        private Task2 TCP_Unicast_Copy(Task2 ExecutedTask_Temp)
        {
            if(ExecutedTask_Temp.Global_TaskOptions.TCP_unicast)
            {
                if (ExecutedTask_Temp.Global_TaskOptions.TempFiles.Count != 0)
                {
                    ExecutedTask_Temp.TCP_Unicast();
                }
            }
            return ExecutedTask_Temp;
        }

        private void WaitForFinishOfRunningCommand(Task2 ExecutedTask_Temp)
        {
            while (ExecutedTask_Temp.WaitingClientsMacs_ForFinishCMD.Count != EMPTY)
            {                
                Thread.Sleep(300);
            }
        }

        private void WaitForRunningCommand(Task2 ExecutedTask_Temp, String Command)
        {
            ExecutedTask_Temp.WaitingClientsMacs = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
            while (ExecutedTask_Temp.WaitingClientsMacs.Count != EMPTY)
            {                
                var MacAddresses = String.Join(",", ExecutedTask_Temp.Global_TaskOptions.MacAddresses.ToArray());
                byte[] Sending_Data = Encoding.ASCII.GetBytes(MacAddresses + "||" + Command);
                ExecutedTask_Temp.SendMessage(RUN_COMMAND, INFO_MESSAGE, Sending_Data);
                ReceiveMacAddress = "";
                Thread.Sleep(300);
            }
        }

        private Task2 RunCommands(Task2 ExecutedTask_Temp)
        {
            if (ExecutedTask_Temp.Global_TaskOptions.Commands.Count != EMPTY)
            {
                foreach (String Command in ExecutedTask_Temp.Global_TaskOptions.Commands)
                {
                    ExecutedTask_Temp.WaitingClientsMacs_ForFinishCMD = new List<string>(ExecutedTask_Temp.Global_TaskOptions.MacAddresses);
                    Task.Factory.StartNew(() => WaitForRunningCommand(ExecutedTask_Temp, Command)).Wait(ExecutedTask_Temp.Global_TaskOptions.WAITING_TIME);
                    for (int position = 0; position < ExecutedTask_Temp.WaitingClientsMacs.Count; position++)
                    {
                        ExecutedTask_Temp.Global_TaskOptions.RemoveMacAddress(ExecutedTask_Temp.WaitingClientsMacs[position]);
                    }
                    if (!(ExecutedTask_Temp.Global_TaskOptions.update))
                    {
                        Task.Factory.StartNew(() => WaitForFinishOfRunningCommand(ExecutedTask_Temp)).Wait(ExecutedTask_Temp.Global_TaskOptions.WAITING_TIME);
                        for (int position = 0; position < ExecutedTask_Temp.WaitingClientsMacs_ForFinishCMD.Count; position++)
                        {
                            ExecutedTask_Temp.Global_TaskOptions.RemoveMacAddress(ExecutedTask_Temp.WaitingClientsMacs_ForFinishCMD[position]);
                        }
                    }
                }
            }
            return ExecutedTask_Temp;
        }

        private void StopTask(string TaskName)
        {
            for (int i = 0; i < ExecutedTasks_List.Count; i++)
            {
                string executedTaskName = ExecutedTasks_List[i].Task.Global_TaskOptions.Name + "." + ExecutedTasks_List[i].Task.Global_TaskOptions.NumberOFTask.ToString();
                if (executedTaskName == TaskName)
                {
                    if (!(ExecutedTasks_List[i].Task.STOPPED))
                    {
                        var MacAddresses = String.Join(",", ExecutedTasks_List[i].Task.Global_TaskOptions.MacAddresses.ToArray());
                        byte[] Sending_Data = Encoding.ASCII.GetBytes(MacAddresses);

                        for (int x = 0; x < 10; x++)
                        {
                            Console.WriteLine("ERROR MEESAGE");
                            ExecutedTasks_List[i].Task.SendMessage(ERROR_MESSAGE, INFO_MESSAGE, Sending_Data);
                            SendMessage(ERROR_MESSAGE, INFO_MESSAGE, Sending_Data);
                            Thread.Sleep(100);
                        }
                        FinishTask(ExecutedTasks_List[i].Task, -1);
                        ExecutedTasks_List[i].Task.Stop();
                        ExecutedTasks_List[i].Task.Destroy_Connections();
                        if (ExecutedTasks_List[i].thread.IsAlive)
                        {
                            ExecutedTasks_List[i].thread.Abort();
                        } 
                        var w = new Form() { Size = new Size(0, 0) };
                        Task.Delay(TimeSpan.FromSeconds(5))
                            .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                        MessageBox.Show(w, ExecutedTasks_List[i].Task.Global_TaskOptions.Name + " STOPPED", "STOPPED");
                        RefreshTaskDetails();
                        break;
                    }                    
                }
            }
        }

        private void CreateThreadForTask()
        {
            string taskName = "";
            try
            {
                WAITINGTIME = 300000;
                Task2 ExecutedTask = new Task2();
                ExecutedTasks_List[ExecutedTasks_List.Count - 1].Task = ExecutedTask;
                ExecutedTask.PrepareTask(RunTaskName);
                taskName = ExecutedTask.Global_TaskOptions.Name;
                ExecutedTask.Global_TaskOptions.WAITING_TIME = WAITINGTIME;
                int NumberOfPCs = ExecutedTask.Global_TaskOptions.TargetComputers_List.Count;
                string MulticastAddress = "225.100.0." + ExecutedTask.Global_TaskOptions.NumberOFTask.ToString();

                WakeOnLan_Task(ExecutedTask);
                Task.Factory.StartNew(() => WaitForClientsAtStart(ExecutedTask)).Wait(ExecutedTask.Global_TaskOptions.WAITING_TIME);
                for (int position = 0; position < ExecutedTask.WaitingClientsMacs.Count; position++)
                {
                    ExecutedTask.Global_TaskOptions.RemoveMacAddress(ExecutedTask.WaitingClientsMacs[position]);
                }
                ExecutedTask.WaitingClientsMacs = new List<string>();
                ExecutedTask = CreateTaskConnection(ExecutedTask, MulticastAddress);

                Task.Factory.StartNew(() => SendMessageToClientsToJoin(ExecutedTask, MulticastAddress)).Wait(ExecutedTask.Global_TaskOptions.WAITING_TIME);
                for (int position = 0; position < ExecutedTask.WaitingClientsMacs.Count; position++)
                {
                    ExecutedTask.Global_TaskOptions.RemoveMacAddress(ExecutedTask.WaitingClientsMacs[position]);
                }

                ExecutedTask = UDP_Multicast_Copy(ExecutedTask);

                ExecutedTask = TCP_Unicast_Copy(ExecutedTask);

                ExecutedTask = RunCommands(ExecutedTask);
                if(!(ExecutedTask.Global_TaskOptions.update))
                {
                    ExecutedTask = TurnOFF(ExecutedTask); 
                    Task.Factory.StartNew(() => WaitForClientsAtFinish(ExecutedTask)).Wait(ExecutedTask.Global_TaskOptions.WAITING_TIME);
                    for (int position = 0; position < ExecutedTask.WaitingClientsMacs.Count; position++)
                    {
                        ExecutedTask.Global_TaskOptions.RemoveMacAddress(ExecutedTask.WaitingClientsMacs[position]);
                    }
                }
                FinishTask(ExecutedTask, NumberOfPCs);
                ExecutedTask.Destroy_Connections();
                BeginInvoke(new Action(() => RefreshTaskDetails()));
                var w = new Form() { Size = new Size(0, 0) };
                Task.Delay(TimeSpan.FromSeconds(10))
                    .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                MessageBox.Show(w, ExecutedTask.Global_TaskOptions.Name + " DONE", "DONE");
            }
            catch (Exception ex)
            {
                StopTask(taskName);
                Console.WriteLine("SOMETHING WRONG WITH TASK " + ex.ToString());
            }
        }

        private void nováToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunTaskName = "";
            TaskOptions newDialog = new TaskOptions(this);            
            newDialog.ShowDialog();
            if(RunTaskName != "")
            {
                Thread thread = new Thread(new ThreadStart(CreateThreadForTask));
                ExecutedTasks_List.Add(new TemplateForExecutedTask());
                ExecutedTasks_List[ExecutedTasks_List.Count - 1].thread = thread;
                thread.Start();
                Thread.Sleep(1000);
                RefreshTaskDetails();
                RefreshTasks();
            }
        }

        private void upraviťToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTask();
        }

        private void zmazaťToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTasks.SelectedIndices[0];
                if (intselectedindex >= 0)
                {
                    string taskName = listViewTasks.Items[intselectedindex].SubItems[0].Text;
                    if(File.Exists(@".\Tasks\"+taskName))
                    {
                        File.Delete(@".\Tasks\" + taskName);
                    }
                    listViewTasks.Items[intselectedindex].Remove(); 
                }
            }
        }

        private void listViewTasks_MouseDown(object sender, MouseEventArgs e)
        {
            if (listViewTasks.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTasks.SelectedIndices[0];
                if (intselectedindex >= 0)
                {
                    upraviťToolStripMenuItem.Visible = true;
                    zmazaťToolStripMenuItem.Visible = true;
                }                
            }
            else
            {
                upraviťToolStripMenuItem.Visible = false;
                zmazaťToolStripMenuItem.Visible = false;
            }
        }

        private void podrobnostiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTaskExecutions.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTaskExecutions.SelectedIndices[0];

                if (intselectedindex >= 0)
                {
                    TaskDetails newDialog = new TaskDetails();
                    string taskDetailsName = listViewTaskExecutions.Items[intselectedindex].SubItems[1].Text;
                    newDialog.taskDetailsName = taskDetailsName;
                    newDialog.ShowDialog();
                }
            }
        }

        private void SendMessage(short state, Int64 position, byte[]data)
        {
            FilePiece resend_piece = new FilePiece(state, position, data);
            byte[] bytes = resend_piece.get_packet();
            SendingSocket.Send(bytes, bytes.Length, SendingIEP);
        }

        private void timerComputersStatusReload_Tick(object sender, EventArgs e)
        {
            SendMessage(CONNECTION_FLAG, INFO_MESSAGE, new byte[0]);
            listViewComputers.Items.Clear();
            if(Directory.Exists(@".\Computers\"))
            {
                string[] computersInfoFiles = Directory.GetFiles(@".\Computers\", "*.my");
                foreach (string computerFile in computersInfoFiles)
                {
                    DateTime LastModifik = new System.IO.FileInfo(computerFile).LastWriteTime;
                    DateTime Date_Now =  DateTime.Now;
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
            timerComputersStatusReload.Interval = 10000;
        }

        private void listViewTaskExecutions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewTaskExecutions.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTaskExecutions.SelectedIndices[0];

                if (intselectedindex >= 0)
                {
                    TaskDetails newDialog = new TaskDetails();
                    string taskDetailsName = listViewTaskExecutions.Items[intselectedindex].SubItems[1].Text;
                    newDialog.taskDetailsName = taskDetailsName;
                    newDialog.ShowDialog();
                }
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (TemplateForExecutedTask tasks in ExecutedTasks_List)
            {
                tasks.thread.Abort();
            }
            Destroy_Connections();
        }

        private void RefreshTaskDetails()
        {
            listViewTaskExecutions.Items.Clear();
            if (Directory.Exists(@".\TaskDetails\"))
            {
                string[] TaskDetailsInfoFiles = Directory.GetFiles(@".\TaskDetails\");
                foreach (string taskDetails in TaskDetailsInfoFiles)
                {
                    try
                    {
                        List<string> textLines = File.ReadAllLines(taskDetails).ToList();
                        foreach (string line in textLines)
                        {
                            if (line.Contains("DETAILS||"))
                            {
                                ListViewItem Task_DETAILS = new ListViewItem();
                                string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                                Task_DETAILS.ImageIndex = Convert.ToInt16(splitter[1]);
                                Task_DETAILS.SubItems.Add(Path.GetFileName(taskDetails));
                                Task_DETAILS.SubItems.Add(splitter[2]);
                                if (splitter[3] != "NONE")
                                {
                                    Task_DETAILS.SubItems.Add(splitter[3]);
                                }
                                else
                                {
                                    Task_DETAILS.SubItems.Add("WORKING");
                                }
                                Task_DETAILS.SubItems.Add(splitter[4]);
                                Task_DETAILS.SubItems.Add(splitter[5]);

                                listViewTaskExecutions.Items.Add(Task_DETAILS);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            if (ColumnE != null)
            {
                SortListView(listViewTaskExecutions, ColumnE);
            }
        }

        private void timerReloadTaskDetails_Tick(object sender, EventArgs e)
        {
            RefreshTaskDetails();
            timerReloadTaskDetails.Interval = 30000;
        }

        private void RefreshTasks()
        {
            listViewTasks.Items.Clear();
            if (Directory.Exists(@".\Tasks\"))
            {
                string[] TaskDetailInfoFiles = Directory.GetFiles(@".\Tasks\");
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
                        if (line.Contains("LastExecution||"))
                        {
                            string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                            ListViewItem Task = new ListViewItem(Path.GetFileName(taskDetail));
                            if (splitter[1] != "")
                            {
                                Task.SubItems.Add(splitter[1]);
                            }
                            else
                            {
                                Task.SubItems.Add("NONE");
                            }
                            Task.SubItems.Add(Computers);
                            listViewTasks.Items.Add(Task);
                        }
                    }
                }
            }
        }

        private void timerRealoadTasks_Tick(object sender, EventArgs e)
        {
            RefreshTasks();
            timerReloadTasks.Interval = 30000;
        }

        private void SortListView(ListView list, ColumnClickEventArgs e)
        {
            int total = list.Items.Count;
            list.BeginUpdate();
            ListViewItem[] items = new ListViewItem[total];
            for (int i = 0; i < total; i++)
            {
                int count = list.Items.Count;
                int minIdx = 0;
                for (int j = 1; j < count; j++)
                    if (list.Items[j].SubItems[e.Column].Text.CompareTo(list.Items[minIdx].SubItems[e.Column].Text) > 0)
                        minIdx = j;
                items[i] = list.Items[minIdx];
                list.Items.RemoveAt(minIdx);
            }
            list.Items.AddRange(items);
            list.EndUpdate();
        }

        private void listViewTaskExecutions_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnE = e;
            SortListView((ListView)sender, e);
        }

        private void zmazaťToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listViewTaskExecutions.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTaskExecutions.SelectedIndices[0];
                if (intselectedindex >= 0)
                {
                    
                    string TaskName = listViewTaskExecutions.Items[intselectedindex].SubItems[1].Text;
                    bool runningTask = false;
                    if (listViewTaskExecutions.Items[intselectedindex].ImageIndex == 3)
                    {
                        for (int i = 0; i < ExecutedTasks_List.Count; i++)
                        {
                            string ExecutedTaskName = ExecutedTasks_List[i].Task.Global_TaskOptions.Name + "." + ExecutedTasks_List[i].Task.Global_TaskOptions.NumberOFTask.ToString();
                            if (ExecutedTaskName == TaskName)
                            {
                                runningTask = true;
                            }
                        }
                    }
                    if (!(runningTask))
                    {
                        string fileNameToDelete = @".\TaskDetails\" + TaskName;
                        if (File.Exists(fileNameToDelete))
                        {
                            File.Delete(fileNameToDelete);
                            RefreshTaskDetails();
                        }
                    }
                }
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTaskExecutions.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewTaskExecutions.SelectedIndices[0];
                if (intselectedindex >= 0 && listViewTaskExecutions.Items[intselectedindex].ImageIndex == 3)
                {
                    string TaskNameToStop = listViewTaskExecutions.Items[intselectedindex].SubItems[1].Text;
                    StopTask(TaskNameToStop);
                }
            }
        }

        private void zmazaťVšetkyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewTaskExecutions.Items.Count > EMPTY)
            {
                foreach (ListViewItem Item in listViewTaskExecutions.Items)
                {
                    string TaskName = Item.SubItems[1].Text;
                    bool runningTask = false;
                    if (Item.ImageIndex == 3)
                    {
                        for (int i = 0; i < ExecutedTasks_List.Count; i++)
                        {
                            string ExecutedTaskName = ExecutedTasks_List[i].Task.Global_TaskOptions.Name + "." + ExecutedTasks_List[i].Task.Global_TaskOptions.NumberOFTask.ToString();
                            if (ExecutedTaskName == TaskName)
                            {
                                runningTask = true;
                            }
                        }
                    }
                    if (!(runningTask))
                    {
                        string fileNameToDelete = @".\TaskDetails\" + TaskName;
                        if (File.Exists(fileNameToDelete))
                        {
                            File.Delete(fileNameToDelete);
                        }
                    }
                } 
                RefreshTaskDetails();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(TemplateForExecutedTask executedTask in ExecutedTasks_List)
            {
                if (!(executedTask.Task.STOPPED))
                {
                    executedTask.Task.Stop();
                    executedTask.Task.Global_TaskOptions.EditDetailsLine("1", DateTime.Now.ToString(), "0");
                }
            }            
        }

        private void listViewComputers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewComputers.SelectedItems.Count > EMPTY)
            {
                int intselectedindex = listViewComputers.SelectedIndices[0];
                if (intselectedindex >= 0)
                {
                    string ComputerName = listViewComputers.Items[intselectedindex].SubItems[1].Text;
                    ComputerDetails newdialog = new ComputerDetails();
                    newdialog.ComputerName = ComputerName;
                    newdialog.ShowDialog();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientsForUpdate = "";
            computersSelectDialog newDialog = new computersSelectDialog(this);
            newDialog.ShowDialog();
            
            if(clientsForUpdate != "")
            {
                List<string> Writer = new List<string>(); 
                Writer.Add("UpdateClients");
                Writer.Add("TargetComputers||" + clientsForUpdate);
                List<string> Computer_List = new List<string>();
                if (clientsForUpdate.Contains(','))
                {
                    string[] split = clientsForUpdate.Split(',');
                    foreach (string ComputerName in split)
                    {
                        Computer_List.Add(ComputerName);
                    }
                }
                else
                {
                    Computer_List.Add(clientsForUpdate);
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
                                                    MessageBox.Show("Pocitac" + ComputerName + " je uz v inej vykonavajucej ulohe.");
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Computers == ComputerName)
                                            {
                                                MessageBox.Show("Pocitac" + ComputerName + " je uz v inej vykonavajucej ulohe.");
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Writer.Add(@"Commands||.\UPDATE\UPDATE.bat");
                Writer.Add("WOL||True");
                Writer.Add("UDP||True");
                Writer.Add("TurnOff||False");
                Writer.Add("LastExecution||" + DateTime.Now);
                Writer.Add("WaitingTime||30"); 
                Writer.Add("SourceDirectory||UPDATE");
                Writer.Add("TargetDirectory||UPDATE");
                Writer.Add(@".\UPDATE\HONMSW_CLIENT.exe");
                Writer.Add(@".\UPDATE\UPDATE.bat"); 
                WriteToFile(Writer);
                RunTaskName = "UpdateClients";
                Thread thread = new Thread(new ThreadStart(CreateThreadForTask));
                ExecutedTasks_List.Add(new TemplateForExecutedTask());
                ExecutedTasks_List[ExecutedTasks_List.Count - 1].thread = thread;
                thread.Start();
                Thread.Sleep(1000);
                ExecutedTasks_List[ExecutedTasks_List.Count - 1].Task.Global_TaskOptions.update = true;
                RefreshTaskDetails();
                RefreshTasks();
            }
        }

        public void WriteToFile(List<string> Writer)
        {
            if (!(Directory.Exists(@".\Tasks\")))
            {
                Directory.CreateDirectory(@".\Tasks\");
            }
            File.WriteAllLines(@".\Tasks\" + Writer[0], Writer.ToArray());
        }
    }
}
