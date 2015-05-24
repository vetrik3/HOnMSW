using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HromadneOperacieNadMS
{
    public partial class Help : Form
    {
        int position;
        int MAXposition;
        string NameOfHelp;
        int MINposition;

        public Help()
        {
            InitializeComponent();
        }

        private void UpdateImage()
        {
            object O = Properties.Resources.ResourceManager.GetObject(NameOfHelp+position.ToString());
            pictureBoxHelp.Image = (Image)O;
        }

        private void HideButtons()
        {
            buttonUpdateClients.Visible = false;
            buttonComputerDetails.Visible = false;
            buttonDeleteTask.Visible = false;
            buttonEditTask.Visible = false;
            buttonCreateTask.Visible = false;
            buttonEnd.Visible = true;
            buttonPause.Visible = true;
            buttonNext.Visible = true;
            buttonBack.Visible = false;
            pictureBoxHelp.Visible = true;
            timer1.Enabled = true;
        }

        private void ShowButtons()
        {
            buttonUpdateClients.Visible = true;
            buttonComputerDetails.Visible = true;
            buttonDeleteTask.Visible = true;
            buttonEditTask.Visible = true;
            buttonCreateTask.Visible = true;
            buttonEnd.Visible = false;
            buttonPause.Visible = false;
            buttonNext.Visible = false;
            buttonBack.Visible = false;
            pictureBoxHelp.Visible = false; 
            timer1.Enabled = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            MINposition = 1;
            MAXposition = 45;
            position = 1;
            NameOfHelp = "CreateTask";
            HideButtons();
            UpdateImage();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            position++;
            UpdateImage();
            if(position == MAXposition)
            {
                buttonNext.Visible = false;
            }
            else
            {
                buttonBack.Visible = true;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            position--;
            UpdateImage();
            if (position == MINposition)
            {
                buttonBack.Visible = false;
            }
            else
            {
                buttonNext.Visible = true;
            }
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            ShowButtons();            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MINposition = 1;
            MAXposition = 6;
            position = 1;
            NameOfHelp = "EditTask";
            HideButtons();
            UpdateImage();
        }

        private void buttonDeleteTask_Click(object sender, EventArgs e)
        {
            MINposition = 1;
            MAXposition = 5;
            position = 1;
            NameOfHelp = "DeleteTask";
            HideButtons();
            UpdateImage();
        }

        private void buttonComputerDetails_Click(object sender, EventArgs e)
        {
            MINposition = 1;
            MAXposition = 9;
            position = 1;
            NameOfHelp = "ComputerDetails";
            HideButtons();
            UpdateImage();
        }

        private void buttonUpdateClients_Click(object sender, EventArgs e)
        {
            MINposition = 1;
            MAXposition = 51;
            position = 1;
            NameOfHelp = "UpdateClients";
            HideButtons();
            UpdateImage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            position++;
            if(position == MAXposition)
            {
                buttonNext.Visible = false;                
            }
            else if(position > MAXposition)
            {
                position = MINposition;
                buttonBack.Visible = false;
            }
            else if(position < MAXposition && position > MINposition)
            {
                buttonNext.Visible = true; 
                buttonBack.Visible = true;
            }
            UpdateImage();
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
    }
}
