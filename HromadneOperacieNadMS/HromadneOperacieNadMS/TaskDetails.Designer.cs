namespace HromadneOperacieNadMS
{
    partial class TaskDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskDetails));
            this.listViewTaskDetails = new System.Windows.Forms.ListView();
            this.ExecutedTaskStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutedTaskComputerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutedTaskStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExecutedTaskTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListStatus = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewTaskDetails
            // 
            this.listViewTaskDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ExecutedTaskStatus,
            this.ExecutedTaskComputerName,
            this.ExecutedTaskStep,
            this.ExecutedTaskTime});
            this.listViewTaskDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTaskDetails.Location = new System.Drawing.Point(0, 0);
            this.listViewTaskDetails.Name = "listViewTaskDetails";
            this.listViewTaskDetails.Size = new System.Drawing.Size(459, 677);
            this.listViewTaskDetails.SmallImageList = this.imageListStatus;
            this.listViewTaskDetails.TabIndex = 0;
            this.listViewTaskDetails.UseCompatibleStateImageBehavior = false;
            this.listViewTaskDetails.View = System.Windows.Forms.View.Details;
            // 
            // ExecutedTaskStatus
            // 
            this.ExecutedTaskStatus.DisplayIndex = 3;
            this.ExecutedTaskStatus.Text = "Status";
            this.ExecutedTaskStatus.Width = 105;
            // 
            // ExecutedTaskComputerName
            // 
            this.ExecutedTaskComputerName.DisplayIndex = 0;
            this.ExecutedTaskComputerName.Text = "Počítač";
            this.ExecutedTaskComputerName.Width = 117;
            // 
            // ExecutedTaskStep
            // 
            this.ExecutedTaskStep.DisplayIndex = 1;
            this.ExecutedTaskStep.Text = "Krok";
            this.ExecutedTaskStep.Width = 96;
            // 
            // ExecutedTaskTime
            // 
            this.ExecutedTaskTime.DisplayIndex = 2;
            this.ExecutedTaskTime.Text = "Čas";
            this.ExecutedTaskTime.Width = 137;
            // 
            // imageListStatus
            // 
            this.imageListStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatus.ImageStream")));
            this.imageListStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListStatus.Images.SetKeyName(0, "Done.ico");
            this.imageListStatus.Images.SetKeyName(1, "Error.ico");
            this.imageListStatus.Images.SetKeyName(2, "Warning.ico");
            // 
            // TaskDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 677);
            this.Controls.Add(this.listViewTaskDetails);
            this.Name = "TaskDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detail Úlohy";
            this.Load += new System.EventHandler(this.TaskDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewTaskDetails;
        private System.Windows.Forms.ColumnHeader ExecutedTaskStatus;
        private System.Windows.Forms.ColumnHeader ExecutedTaskComputerName;
        private System.Windows.Forms.ColumnHeader ExecutedTaskStep;
        private System.Windows.Forms.ColumnHeader ExecutedTaskTime;
        private System.Windows.Forms.ImageList imageListStatus;
    }
}