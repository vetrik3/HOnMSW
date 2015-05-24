namespace HromadneOperacieNadMS
{
    partial class Help
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
            this.pictureBoxHelp = new System.Windows.Forms.PictureBox();
            this.buttonCreateTask = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonEditTask = new System.Windows.Forms.Button();
            this.buttonComputerDetails = new System.Windows.Forms.Button();
            this.buttonDeleteTask = new System.Windows.Forms.Button();
            this.buttonUpdateClients = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonPause = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxHelp
            // 
            this.pictureBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxHelp.Location = new System.Drawing.Point(12, 41);
            this.pictureBoxHelp.Name = "pictureBoxHelp";
            this.pictureBoxHelp.Size = new System.Drawing.Size(930, 557);
            this.pictureBoxHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHelp.TabIndex = 0;
            this.pictureBoxHelp.TabStop = false;
            this.pictureBoxHelp.Visible = false;
            // 
            // buttonCreateTask
            // 
            this.buttonCreateTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateTask.Location = new System.Drawing.Point(12, 3);
            this.buttonCreateTask.Name = "buttonCreateTask";
            this.buttonCreateTask.Size = new System.Drawing.Size(930, 40);
            this.buttonCreateTask.TabIndex = 1;
            this.buttonCreateTask.Text = "Ako vytvoriť úlohu";
            this.buttonCreateTask.UseVisualStyleBackColor = true;
            this.buttonCreateTask.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(867, 12);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "Ďalej";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Visible = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(12, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 3;
            this.buttonBack.Text = "Dozadu";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonEnd.Location = new System.Drawing.Point(401, 12);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(75, 23);
            this.buttonEnd.TabIndex = 4;
            this.buttonEnd.Text = "Koniec";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Visible = false;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonEditTask
            // 
            this.buttonEditTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditTask.Location = new System.Drawing.Point(12, 49);
            this.buttonEditTask.Name = "buttonEditTask";
            this.buttonEditTask.Size = new System.Drawing.Size(930, 40);
            this.buttonEditTask.TabIndex = 5;
            this.buttonEditTask.Text = "Ako upraviť úlohu";
            this.buttonEditTask.UseVisualStyleBackColor = true;
            this.buttonEditTask.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // buttonComputerDetails
            // 
            this.buttonComputerDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonComputerDetails.Location = new System.Drawing.Point(12, 141);
            this.buttonComputerDetails.Name = "buttonComputerDetails";
            this.buttonComputerDetails.Size = new System.Drawing.Size(930, 40);
            this.buttonComputerDetails.TabIndex = 6;
            this.buttonComputerDetails.Text = "Ako zobraziť podrobnosti o počítači";
            this.buttonComputerDetails.UseVisualStyleBackColor = true;
            this.buttonComputerDetails.Click += new System.EventHandler(this.buttonComputerDetails_Click);
            // 
            // buttonDeleteTask
            // 
            this.buttonDeleteTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteTask.Location = new System.Drawing.Point(12, 95);
            this.buttonDeleteTask.Name = "buttonDeleteTask";
            this.buttonDeleteTask.Size = new System.Drawing.Size(930, 40);
            this.buttonDeleteTask.TabIndex = 7;
            this.buttonDeleteTask.Text = "Ako zmazať úlohu";
            this.buttonDeleteTask.UseVisualStyleBackColor = true;
            this.buttonDeleteTask.Click += new System.EventHandler(this.buttonDeleteTask_Click);
            // 
            // buttonUpdateClients
            // 
            this.buttonUpdateClients.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdateClients.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonUpdateClients.Location = new System.Drawing.Point(12, 187);
            this.buttonUpdateClients.Name = "buttonUpdateClients";
            this.buttonUpdateClients.Size = new System.Drawing.Size(930, 40);
            this.buttonUpdateClients.TabIndex = 8;
            this.buttonUpdateClients.Text = "Ako updatovat počítače";
            this.buttonUpdateClients.UseVisualStyleBackColor = true;
            this.buttonUpdateClients.Click += new System.EventHandler(this.buttonUpdateClients_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonPause.Location = new System.Drawing.Point(482, 12);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(152, 23);
            this.buttonPause.TabIndex = 9;
            this.buttonPause.Text = "Pozastaviť/Pokračovať";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Visible = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 610);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonUpdateClients);
            this.Controls.Add(this.buttonDeleteTask);
            this.Controls.Add(this.buttonComputerDetails);
            this.Controls.Add(this.buttonEditTask);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonCreateTask);
            this.Controls.Add(this.pictureBoxHelp);
            this.Name = "Help";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHelp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxHelp;
        private System.Windows.Forms.Button buttonCreateTask;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonEditTask;
        private System.Windows.Forms.Button buttonComputerDetails;
        private System.Windows.Forms.Button buttonDeleteTask;
        private System.Windows.Forms.Button buttonUpdateClients;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonPause;
    }
}