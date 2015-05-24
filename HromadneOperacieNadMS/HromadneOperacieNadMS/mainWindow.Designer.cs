namespace HromadneOperacieNadMS
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Úlohy"}, 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "Počítače"}, 1);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewComputers = new System.Windows.Forms.ListView();
            this.computerStatusColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.computerNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.computerMacAdressColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListStatusComputer = new System.Windows.Forms.ImageList(this.components);
            this.listViewOptions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListOptions = new System.Windows.Forms.ImageList(this.components);
            this.listViewTasks = new System.Windows.Forms.ListView();
            this.TaskNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskExecutedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TaskComputersHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nováToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upraviťToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmazaťToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewTaskExecutions = new System.Windows.Forms.ListView();
            this.ExecutionTaskStatusHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutionTaskNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutionTaskStartedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutionTaskFinishedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutionTaskClientsHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutionTaskClientsOKHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripExecutedTasks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.podrobnostiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zmazaťToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zmazaťVšetkyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListStatus = new System.Windows.Forms.ImageList(this.components);
            this.timerComputersStatusReload = new System.Windows.Forms.Timer(this.components);
            this.timerReloadTaskDetails = new System.Windows.Forms.Timer(this.components);
            this.timerReloadTasks = new System.Windows.Forms.Timer(this.components);
            this.pomocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStripTask.SuspendLayout();
            this.contextMenuStripExecutedTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(577, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateClientsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.pomocToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.helpToolStripMenuItem.Text = "Pomoc";
            // 
            // updateClientsToolStripMenuItem
            // 
            this.updateClientsToolStripMenuItem.Name = "updateClientsToolStripMenuItem";
            this.updateClientsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.updateClientsToolStripMenuItem.Text = "Aktualizovať klientov";
            this.updateClientsToolStripMenuItem.Click += new System.EventHandler(this.updateClientsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.aboutToolStripMenuItem.Text = "O autorovi";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // listViewComputers
            // 
            this.listViewComputers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.computerStatusColumn,
            this.computerNameColumn,
            this.computerMacAdressColumn});
            this.listViewComputers.FullRowSelect = true;
            this.listViewComputers.Location = new System.Drawing.Point(148, 27);
            this.listViewComputers.Name = "listViewComputers";
            this.listViewComputers.Size = new System.Drawing.Size(417, 284);
            this.listViewComputers.SmallImageList = this.imageListStatusComputer;
            this.listViewComputers.TabIndex = 4;
            this.listViewComputers.UseCompatibleStateImageBehavior = false;
            this.listViewComputers.View = System.Windows.Forms.View.Details;
            this.listViewComputers.Visible = false;
            this.listViewComputers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewComputers_MouseDoubleClick);
            // 
            // computerStatusColumn
            // 
            this.computerStatusColumn.DisplayIndex = 2;
            this.computerStatusColumn.Text = "Status";
            this.computerStatusColumn.Width = 82;
            // 
            // computerNameColumn
            // 
            this.computerNameColumn.DisplayIndex = 0;
            this.computerNameColumn.Text = "Názov";
            this.computerNameColumn.Width = 147;
            // 
            // computerMacAdressColumn
            // 
            this.computerMacAdressColumn.DisplayIndex = 1;
            this.computerMacAdressColumn.Text = "Mac Adresa";
            this.computerMacAdressColumn.Width = 184;
            // 
            // imageListStatusComputer
            // 
            this.imageListStatusComputer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatusComputer.ImageStream")));
            this.imageListStatusComputer.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListStatusComputer.Images.SetKeyName(0, "Connected.ico");
            this.imageListStatusComputer.Images.SetKeyName(1, "Disconected.ico");
            this.imageListStatusComputer.Images.SetKeyName(2, "Working.ico");
            // 
            // listViewOptions
            // 
            this.listViewOptions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewOptions.FullRowSelect = true;
            this.listViewOptions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listViewOptions.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewOptions.Location = new System.Drawing.Point(12, 27);
            this.listViewOptions.MultiSelect = false;
            this.listViewOptions.Name = "listViewOptions";
            this.listViewOptions.Size = new System.Drawing.Size(129, 284);
            this.listViewOptions.SmallImageList = this.imageListOptions;
            this.listViewOptions.TabIndex = 5;
            this.listViewOptions.UseCompatibleStateImageBehavior = false;
            this.listViewOptions.View = System.Windows.Forms.View.Details;
            this.listViewOptions.SelectedIndexChanged += new System.EventHandler(this.listViewOptions_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ikonka";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Možnosť";
            this.columnHeader2.Width = 75;
            // 
            // imageListOptions
            // 
            this.imageListOptions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListOptions.ImageStream")));
            this.imageListOptions.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListOptions.Images.SetKeyName(0, "Delacro-Id-Scheduled-Tasks.ico");
            this.imageListOptions.Images.SetKeyName(1, "Icons-Land-Vista-Hardware-Devices-Computer.ico");
            // 
            // listViewTasks
            // 
            this.listViewTasks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TaskNameHeader,
            this.TaskExecutedHeader,
            this.TaskComputersHeader});
            this.listViewTasks.ContextMenuStrip = this.contextMenuStripTask;
            this.listViewTasks.FullRowSelect = true;
            this.listViewTasks.Location = new System.Drawing.Point(147, 27);
            this.listViewTasks.MultiSelect = false;
            this.listViewTasks.Name = "listViewTasks";
            this.listViewTasks.Size = new System.Drawing.Size(417, 284);
            this.listViewTasks.TabIndex = 6;
            this.listViewTasks.UseCompatibleStateImageBehavior = false;
            this.listViewTasks.View = System.Windows.Forms.View.Details;
            this.listViewTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewTasks_MouseDoubleClick);
            this.listViewTasks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewTasks_MouseDown);
            // 
            // TaskNameHeader
            // 
            this.TaskNameHeader.Text = "Názov";
            this.TaskNameHeader.Width = 146;
            // 
            // TaskExecutedHeader
            // 
            this.TaskExecutedHeader.Text = "Vykonané";
            this.TaskExecutedHeader.Width = 140;
            // 
            // TaskComputersHeader
            // 
            this.TaskComputersHeader.Text = "Počítače";
            this.TaskComputersHeader.Width = 126;
            // 
            // contextMenuStripTask
            // 
            this.contextMenuStripTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nováToolStripMenuItem,
            this.upraviťToolStripMenuItem,
            this.zmazaťToolStripMenuItem});
            this.contextMenuStripTask.Name = "contextMenuStripTask";
            this.contextMenuStripTask.Size = new System.Drawing.Size(115, 70);
            // 
            // nováToolStripMenuItem
            // 
            this.nováToolStripMenuItem.Name = "nováToolStripMenuItem";
            this.nováToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.nováToolStripMenuItem.Text = "Nová";
            this.nováToolStripMenuItem.Click += new System.EventHandler(this.nováToolStripMenuItem_Click);
            // 
            // upraviťToolStripMenuItem
            // 
            this.upraviťToolStripMenuItem.Name = "upraviťToolStripMenuItem";
            this.upraviťToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.upraviťToolStripMenuItem.Text = "Upraviť";
            this.upraviťToolStripMenuItem.Visible = false;
            this.upraviťToolStripMenuItem.Click += new System.EventHandler(this.upraviťToolStripMenuItem_Click);
            // 
            // zmazaťToolStripMenuItem
            // 
            this.zmazaťToolStripMenuItem.Name = "zmazaťToolStripMenuItem";
            this.zmazaťToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.zmazaťToolStripMenuItem.Text = "Zmazať";
            this.zmazaťToolStripMenuItem.Visible = false;
            this.zmazaťToolStripMenuItem.Click += new System.EventHandler(this.zmazaťToolStripMenuItem_Click);
            // 
            // listViewTaskExecutions
            // 
            this.listViewTaskExecutions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ExecutionTaskStatusHeader,
            this.ExecutionTaskNameHeader,
            this.ExecutionTaskStartedHeader,
            this.ExecutionTaskFinishedHeader,
            this.ExecutionTaskClientsHeader,
            this.ExecutionTaskClientsOKHeader});
            this.listViewTaskExecutions.ContextMenuStrip = this.contextMenuStripExecutedTasks;
            this.listViewTaskExecutions.FullRowSelect = true;
            this.listViewTaskExecutions.Location = new System.Drawing.Point(12, 317);
            this.listViewTaskExecutions.Name = "listViewTaskExecutions";
            this.listViewTaskExecutions.Size = new System.Drawing.Size(552, 284);
            this.listViewTaskExecutions.SmallImageList = this.imageListStatus;
            this.listViewTaskExecutions.TabIndex = 7;
            this.listViewTaskExecutions.UseCompatibleStateImageBehavior = false;
            this.listViewTaskExecutions.View = System.Windows.Forms.View.Details;
            this.listViewTaskExecutions.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewTaskExecutions_ColumnClick);
            this.listViewTaskExecutions.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewTaskExecutions_MouseDoubleClick);
            // 
            // ExecutionTaskStatusHeader
            // 
            this.ExecutionTaskStatusHeader.DisplayIndex = 1;
            this.ExecutionTaskStatusHeader.Text = "Status";
            this.ExecutionTaskStatusHeader.Width = 47;
            // 
            // ExecutionTaskNameHeader
            // 
            this.ExecutionTaskNameHeader.DisplayIndex = 0;
            this.ExecutionTaskNameHeader.Text = "Názov";
            this.ExecutionTaskNameHeader.Width = 89;
            // 
            // ExecutionTaskStartedHeader
            // 
            this.ExecutionTaskStartedHeader.Text = "Spustené";
            this.ExecutionTaskStartedHeader.Width = 115;
            // 
            // ExecutionTaskFinishedHeader
            // 
            this.ExecutionTaskFinishedHeader.Text = "Ukončené";
            this.ExecutionTaskFinishedHeader.Width = 109;
            // 
            // ExecutionTaskClientsHeader
            // 
            this.ExecutionTaskClientsHeader.Text = "Počet Počítačov";
            this.ExecutionTaskClientsHeader.Width = 97;
            // 
            // ExecutionTaskClientsOKHeader
            // 
            this.ExecutionTaskClientsOKHeader.Text = "Ukončených";
            this.ExecutionTaskClientsOKHeader.Width = 91;
            // 
            // contextMenuStripExecutedTasks
            // 
            this.contextMenuStripExecutedTasks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.podrobnostiToolStripMenuItem,
            this.zmazaťToolStripMenuItem1,
            this.zmazaťVšetkyToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.contextMenuStripExecutedTasks.Name = "contextMenuStripExecutedTasks";
            this.contextMenuStripExecutedTasks.Size = new System.Drawing.Size(151, 92);
            // 
            // podrobnostiToolStripMenuItem
            // 
            this.podrobnostiToolStripMenuItem.Name = "podrobnostiToolStripMenuItem";
            this.podrobnostiToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.podrobnostiToolStripMenuItem.Text = "Podrobnosti";
            this.podrobnostiToolStripMenuItem.Click += new System.EventHandler(this.podrobnostiToolStripMenuItem_Click);
            // 
            // zmazaťToolStripMenuItem1
            // 
            this.zmazaťToolStripMenuItem1.Name = "zmazaťToolStripMenuItem1";
            this.zmazaťToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.zmazaťToolStripMenuItem1.Text = "Zmazať";
            this.zmazaťToolStripMenuItem1.Click += new System.EventHandler(this.zmazaťToolStripMenuItem1_Click);
            // 
            // zmazaťVšetkyToolStripMenuItem
            // 
            this.zmazaťVšetkyToolStripMenuItem.Name = "zmazaťVšetkyToolStripMenuItem";
            this.zmazaťVšetkyToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.zmazaťVšetkyToolStripMenuItem.Text = "Zmazať všetky";
            this.zmazaťVšetkyToolStripMenuItem.Click += new System.EventHandler(this.zmazaťVšetkyToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // imageListStatus
            // 
            this.imageListStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatus.ImageStream")));
            this.imageListStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListStatus.Images.SetKeyName(0, "Done.ico");
            this.imageListStatus.Images.SetKeyName(1, "Error.ico");
            this.imageListStatus.Images.SetKeyName(2, "Warning.ico");
            this.imageListStatus.Images.SetKeyName(3, "In_progress_icon.svg.png");
            // 
            // timerComputersStatusReload
            // 
            this.timerComputersStatusReload.Enabled = true;
            this.timerComputersStatusReload.Interval = 10000;
            this.timerComputersStatusReload.Tick += new System.EventHandler(this.timerComputersStatusReload_Tick);
            // 
            // timerReloadTaskDetails
            // 
            this.timerReloadTaskDetails.Enabled = true;
            this.timerReloadTaskDetails.Tick += new System.EventHandler(this.timerReloadTaskDetails_Tick);
            // 
            // timerReloadTasks
            // 
            this.timerReloadTasks.Enabled = true;
            this.timerReloadTasks.Tick += new System.EventHandler(this.timerRealoadTasks_Tick);
            // 
            // pomocToolStripMenuItem
            // 
            this.pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            this.pomocToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.pomocToolStripMenuItem.Text = "Pomoc";
            this.pomocToolStripMenuItem.Click += new System.EventHandler(this.pomocToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 613);
            this.Controls.Add(this.listViewTaskExecutions);
            this.Controls.Add(this.listViewTasks);
            this.Controls.Add(this.listViewOptions);
            this.Controls.Add(this.listViewComputers);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hlavná Stránka";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStripTask.ResumeLayout(false);
            this.contextMenuStripExecutedTasks.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ListView listViewComputers;
        private System.Windows.Forms.ListView listViewOptions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageListOptions;
        private System.Windows.Forms.ColumnHeader computerNameColumn;
        private System.Windows.Forms.ColumnHeader computerMacAdressColumn;
        private System.Windows.Forms.ColumnHeader computerStatusColumn;
        private System.Windows.Forms.ListView listViewTasks;
        private System.Windows.Forms.ColumnHeader TaskNameHeader;
        private System.Windows.Forms.ColumnHeader TaskExecutedHeader;
        private System.Windows.Forms.ColumnHeader TaskComputersHeader;
        private System.Windows.Forms.ListView listViewTaskExecutions;
        private System.Windows.Forms.ColumnHeader ExecutionTaskNameHeader;
        private System.Windows.Forms.ColumnHeader ExecutionTaskStatusHeader;
        private System.Windows.Forms.ColumnHeader ExecutionTaskStartedHeader;
        private System.Windows.Forms.ColumnHeader ExecutionTaskFinishedHeader;
        private System.Windows.Forms.ColumnHeader ExecutionTaskClientsHeader;
        private System.Windows.Forms.ColumnHeader ExecutionTaskClientsOKHeader;
        private System.Windows.Forms.ImageList imageListStatus;
        private System.Windows.Forms.ImageList imageListStatusComputer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTask;
        private System.Windows.Forms.ToolStripMenuItem nováToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upraviťToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zmazaťToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripExecutedTasks;
        private System.Windows.Forms.ToolStripMenuItem podrobnostiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zmazaťToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem zmazaťVšetkyToolStripMenuItem;
        private System.Windows.Forms.Timer timerComputersStatusReload;
        private System.Windows.Forms.Timer timerReloadTasks;
        public System.Windows.Forms.Timer timerReloadTaskDetails;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;

    }
}

