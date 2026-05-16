using System;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class mainFrame : Form
    {

        adminDashboardForm Dashboard;
        MembersForm Members;
        //storeForm Store;
        settingsForm Settings;

        private Form currentChildForm;


        //public static object lblUsername;
        //public static object lblUserRole;

        public mainFrame()
        {
            InitializeComponent();

        }


        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        public void OpenChildForm(Form childForm)
        {
            // close and remove previous child
            if (currentChildForm != null)
            {
                try { currentChildForm.Close(); } catch { }
                panelDesctop.Controls.Clear();
                currentChildForm = null;
            }

            currentChildForm = childForm;
            // embed the form into the panel so it fits exactly
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopMost = false;
            childForm.Margin = new Padding(0);
            childForm.Padding = new Padding(0);
            childForm.WindowState = FormWindowState.Normal;
            childForm.Dock = DockStyle.Fill;

            panelDesctop.SuspendLayout();
            panelDesctop.Controls.Add(childForm);
            panelDesctop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            panelDesctop.ResumeLayout();
        }

        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new adminDashboardForm());
        }



        private void btnMembers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MembersForm());
        }



        private void btnStore_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new storeForm());
            OpenChildForm(new formStore());
        }



        private void btnSettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new settingsForm());
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {

        }

        private void mainFrame_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            currentChildForm?.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
