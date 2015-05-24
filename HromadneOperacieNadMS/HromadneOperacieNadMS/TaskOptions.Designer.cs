namespace HromadneOperacieNadMS
{
    partial class TaskOptions
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBrowseComputers = new System.Windows.Forms.Button();
            this.textBoxTargetComputers = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTargetDirectory = new System.Windows.Forms.TextBox();
            this.buttonClearListBoxFiles = new System.Windows.Forms.Button();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.buttonFilesDialog = new System.Windows.Forms.Button();
            this.buttonFolderDialog = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarWaitingTime = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.radioButtonUDP = new System.Windows.Forms.RadioButton();
            this.checkBoxTurnOff = new System.Windows.Forms.CheckBox();
            this.checkBoxWOL = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBoxRunCommand = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWaitingTime)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxName.Location = new System.Drawing.Point(9, 29);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(404, 23);
            this.textBoxName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Názov úlohy:";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonHelp.Location = new System.Drawing.Point(312, 656);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(95, 31);
            this.buttonHelp.TabIndex = 2;
            this.buttonHelp.Text = "Pomoc";
            this.buttonHelp.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonClose.Location = new System.Drawing.Point(211, 656);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(95, 31);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Zatvoriť";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSave.Location = new System.Drawing.Point(110, 656);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(95, 31);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Uložiť";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExecute.Location = new System.Drawing.Point(9, 656);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(95, 31);
            this.buttonExecute.TabIndex = 5;
            this.buttonExecute.Text = "Vykonať";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBrowseComputers);
            this.groupBox1.Controls.Add(this.textBoxTargetComputers);
            this.groupBox1.Location = new System.Drawing.Point(9, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 94);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cieľové počítače";
            // 
            // buttonBrowseComputers
            // 
            this.buttonBrowseComputers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonBrowseComputers.Location = new System.Drawing.Point(297, 48);
            this.buttonBrowseComputers.Name = "buttonBrowseComputers";
            this.buttonBrowseComputers.Size = new System.Drawing.Size(95, 31);
            this.buttonBrowseComputers.TabIndex = 7;
            this.buttonBrowseComputers.Text = "Vybrať";
            this.buttonBrowseComputers.UseVisualStyleBackColor = true;
            this.buttonBrowseComputers.Click += new System.EventHandler(this.buttonBrowseComputers_Click);
            // 
            // textBoxTargetComputers
            // 
            this.textBoxTargetComputers.Enabled = false;
            this.textBoxTargetComputers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTargetComputers.Location = new System.Drawing.Point(6, 19);
            this.textBoxTargetComputers.Name = "textBoxTargetComputers";
            this.textBoxTargetComputers.Size = new System.Drawing.Size(386, 23);
            this.textBoxTargetComputers.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxTargetDirectory);
            this.groupBox2.Controls.Add(this.buttonClearListBoxFiles);
            this.groupBox2.Controls.Add(this.listBoxFiles);
            this.groupBox2.Controls.Add(this.buttonFilesDialog);
            this.groupBox2.Controls.Add(this.buttonFolderDialog);
            this.groupBox2.Location = new System.Drawing.Point(9, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 218);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Súbory na kopírovanie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(6, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Cieľový priečinok:";
            // 
            // textBoxTargetDirectory
            // 
            this.textBoxTargetDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxTargetDirectory.Location = new System.Drawing.Point(6, 150);
            this.textBoxTargetDirectory.Name = "textBoxTargetDirectory";
            this.textBoxTargetDirectory.Size = new System.Drawing.Size(386, 23);
            this.textBoxTargetDirectory.TabIndex = 8;
            // 
            // buttonClearListBoxFiles
            // 
            this.buttonClearListBoxFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonClearListBoxFiles.Location = new System.Drawing.Point(95, 179);
            this.buttonClearListBoxFiles.Name = "buttonClearListBoxFiles";
            this.buttonClearListBoxFiles.Size = new System.Drawing.Size(95, 31);
            this.buttonClearListBoxFiles.TabIndex = 10;
            this.buttonClearListBoxFiles.Text = "Vyčistiť";
            this.buttonClearListBoxFiles.UseVisualStyleBackColor = true;
            this.buttonClearListBoxFiles.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.Location = new System.Drawing.Point(9, 19);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(383, 108);
            this.listBoxFiles.TabIndex = 9;
            // 
            // buttonFilesDialog
            // 
            this.buttonFilesDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonFilesDialog.Location = new System.Drawing.Point(196, 179);
            this.buttonFilesDialog.Name = "buttonFilesDialog";
            this.buttonFilesDialog.Size = new System.Drawing.Size(95, 31);
            this.buttonFilesDialog.TabIndex = 8;
            this.buttonFilesDialog.Text = "Súbory";
            this.buttonFilesDialog.UseVisualStyleBackColor = true;
            this.buttonFilesDialog.Click += new System.EventHandler(this.buttonFilesDialog_Click);
            // 
            // buttonFolderDialog
            // 
            this.buttonFolderDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonFolderDialog.Location = new System.Drawing.Point(297, 179);
            this.buttonFolderDialog.Name = "buttonFolderDialog";
            this.buttonFolderDialog.Size = new System.Drawing.Size(95, 31);
            this.buttonFolderDialog.TabIndex = 7;
            this.buttonFolderDialog.Text = "Priečinok";
            this.buttonFolderDialog.UseVisualStyleBackColor = true;
            this.buttonFolderDialog.Click += new System.EventHandler(this.buttonFolderDialog_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.trackBarWaitingTime);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.radioButtonTCP);
            this.groupBox3.Controls.Add(this.radioButtonUDP);
            this.groupBox3.Controls.Add(this.checkBoxTurnOff);
            this.groupBox3.Controls.Add(this.checkBoxWOL);
            this.groupBox3.Location = new System.Drawing.Point(9, 508);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(404, 142);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Iné nastavenia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "\"\"";
            // 
            // trackBarWaitingTime
            // 
            this.trackBarWaitingTime.Location = new System.Drawing.Point(120, 97);
            this.trackBarWaitingTime.Maximum = 30;
            this.trackBarWaitingTime.Minimum = 5;
            this.trackBarWaitingTime.Name = "trackBarWaitingTime";
            this.trackBarWaitingTime.Size = new System.Drawing.Size(104, 45);
            this.trackBarWaitingTime.SmallChange = 5;
            this.trackBarWaitingTime.TabIndex = 7;
            this.trackBarWaitingTime.TickFrequency = 5;
            this.trackBarWaitingTime.Value = 5;
            this.trackBarWaitingTime.ValueChanged += new System.EventHandler(this.trackBarWaitingTime_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(3, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Doba čakania:";
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.AutoSize = true;
            this.radioButtonTCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonTCP.Location = new System.Drawing.Point(133, 47);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(108, 21);
            this.radioButtonTCP.TabIndex = 4;
            this.radioButtonTCP.TabStop = true;
            this.radioButtonTCP.Text = "TCP(unicast)";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            // 
            // radioButtonUDP
            // 
            this.radioButtonUDP.AutoSize = true;
            this.radioButtonUDP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radioButtonUDP.Location = new System.Drawing.Point(7, 47);
            this.radioButtonUDP.Name = "radioButtonUDP";
            this.radioButtonUDP.Size = new System.Drawing.Size(120, 21);
            this.radioButtonUDP.TabIndex = 3;
            this.radioButtonUDP.TabStop = true;
            this.radioButtonUDP.Text = "UDP(Multicast)";
            this.radioButtonUDP.UseVisualStyleBackColor = true;
            // 
            // checkBoxTurnOff
            // 
            this.checkBoxTurnOff.AutoSize = true;
            this.checkBoxTurnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxTurnOff.Location = new System.Drawing.Point(7, 74);
            this.checkBoxTurnOff.Name = "checkBoxTurnOff";
            this.checkBoxTurnOff.Size = new System.Drawing.Size(129, 21);
            this.checkBoxTurnOff.TabIndex = 2;
            this.checkBoxTurnOff.Text = "Vypnúť počítače";
            this.checkBoxTurnOff.UseVisualStyleBackColor = true;
            // 
            // checkBoxWOL
            // 
            this.checkBoxWOL.AutoSize = true;
            this.checkBoxWOL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxWOL.Location = new System.Drawing.Point(7, 20);
            this.checkBoxWOL.Name = "checkBoxWOL";
            this.checkBoxWOL.Size = new System.Drawing.Size(106, 21);
            this.checkBoxWOL.TabIndex = 0;
            this.checkBoxWOL.Text = "WakeOnLan";
            this.checkBoxWOL.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBoxRunCommand);
            this.groupBox4.Location = new System.Drawing.Point(9, 392);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(404, 110);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Spustiť príkazy";
            // 
            // richTextBoxRunCommand
            // 
            this.richTextBoxRunCommand.Location = new System.Drawing.Point(7, 20);
            this.richTextBoxRunCommand.Name = "richTextBoxRunCommand";
            this.richTextBoxRunCommand.Size = new System.Drawing.Size(385, 84);
            this.richTextBoxRunCommand.TabIndex = 0;
            this.richTextBoxRunCommand.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // TaskOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 699);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TaskOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskOptions";
            this.Load += new System.EventHandler(this.TaskOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWaitingTime)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowseComputers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonFolderDialog;
        private System.Windows.Forms.Button buttonFilesDialog;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Button buttonClearListBoxFiles;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxWOL;
        private System.Windows.Forms.CheckBox checkBoxTurnOff;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox richTextBoxRunCommand;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBoxTargetDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonTCP;
        private System.Windows.Forms.RadioButton radioButtonUDP;
        public System.Windows.Forms.TextBox textBoxTargetComputers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarWaitingTime;
        private System.Windows.Forms.Label label4;
    }
}