using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class adminDashboardForm : Form
    {
        public adminDashboardForm()
        {
            InitializeComponent();
            lblgreeting.Text = GetGreeting();
        }



        private void formAdmindashboardLoad(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblgreeting_Click(object sender, EventArgs e)
        {

        }

        private string GetGreeting() {

            int hour = DateTime.Now.Hour;
            if (hour >= 5 && hour < 12)
            {
                return "Good Morning";
            }
            else if (hour >= 12 && hour < 17)
            {
                return "Good Afternoon"; // Optional, but keeps it natural
            }
            else if (hour >= 17 && hour < 21)
            {
                return "Good Evening";
            }
            else
            {
                return "Good Night";
            }
        }
    }
}
