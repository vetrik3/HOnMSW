namespace HromadneOperacieNadMS
{
    partial class computersSelectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(computersSelectDialog));
            this.listViewComputers = new System.Windows.Forms.ListView();
            this.computerStatusColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.computerNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.computerMacAdressColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.imageListStatusComputer = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewComputers
            // 
            this.listViewComputers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.computerStatusColumn,
            this.computerNameColumn,
            this.computerMacAdressColumn});
            this.listViewComputers.FullRowSelect = true;
            this.listViewComputers.Location = new System.Drawing.Point(12, 12);
            this.listViewComputers.Name = "listViewComputers";
            this.listViewComputers.Size = new System.Drawing.Size(417, 284);
            this.listViewComputers.SmallImageList = this.imageListStatusComputer;
            this.listViewComputers.TabIndex = 5;
            this.listViewComputers.UseCompatibleStateImageBehavior = false;
            this.listViewComputers.View = System.Windows.Forms.View.Details;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(273, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imageListStatusComputer
            // 
            this.imageListStatusComputer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatusComputer.ImageStream")));
            this.imageListStatusComputer.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListStatusComputer.Images.SetKeyName(0, "Connected.ico");
            this.imageListStatusComputer.Images.SetKeyName(1, "Disconected.ico");
            this.imageListStatusComputer.Images.SetKeyName(2, "Working.ico");
            // 
            // computersSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 335);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewComputers);
            this.Name = "computersSelectDialog";
            this.Text = "computersSelectDialog";
            this.Load += new System.EventHandler(this.computersSelectDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewComputers;
        private System.Windows.Forms.ColumnHeader computerStatusColumn;
        private System.Windows.Forms.ColumnHeader computerNameColumn;
        private System.Windows.Forms.ColumnHeader computerMacAdressColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageListStatusComputer;
    }
}