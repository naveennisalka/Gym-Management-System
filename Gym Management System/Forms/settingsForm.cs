using Gym_Management_System.Forms;
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
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();
        }
        

        private void button7_Click(object sender, EventArgs e)
        {
            formAddAssets formAddAssets = new formAddAssets();
            formAddAssets.btnUpdate.Hide();
            formAddAssets.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            formEquipment formEquipment = new formEquipment();
            formEquipment.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formChangePassword formChangePassword = new formChangePassword();

            formChangePassword.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            userRegistrationForm profileEditForm = new userRegistrationForm(true);
            profileEditForm.label12.Text = "Edit My Profile";
            profileEditForm.ShowDialog();
        }
    }
}
