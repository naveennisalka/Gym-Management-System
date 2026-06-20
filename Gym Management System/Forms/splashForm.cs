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
    public partial class splashForm : Form
    {
        public splashForm()
        {
            InitializeComponent();
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        // Define this at the top of your Splash Form class level variables
        int startPoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 5;

            if (startPoint >= 100)
            {
                progressBar1.Value = 100;
                timer1.Stop();

                // 1. Initialize the new login system instance
                loginForm login = new loginForm();

                // 2. Display the login screen on the screen surface
                login.Show();

                // 3. Change form ownership so the Splash Form can close without killing the thread
                this.Hide();
            }
            else
            {
                progressBar1.Value = startPoint;
            }
        }


        private void splashForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            
        }
    }
}
