using Gym_Management_System.Forms;
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
            ConfigureDashboardView();
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


        // No parameters needed anymore! It reads directly from the static session class
        public void ConfigureDashboardView()
        {
           
            

            
            HideAllDashboardElements();

            // 3. Render modules based on the static class role variable
            switch (UserSession.UserRole)
            {
                case 1: // ---- SUPER ADMIN VIEW ----
                    btnMembers.Visible = true;
                    btnSettings.Visible = true;
                    btnStore.Visible = true;
                    btnSchedules.Visible = true;
                    btnEquipment.Visible = true;
                    break;

                case 3: // ---- COACH VIEW ----
                    btnSchedules.Visible = true;
                    btnSettings.Visible = true;
                    break;

                case 4: // ---- GYM MEMBER VIEW ----
                    btnSettings.Visible = true;
                    break;

                default:
                    MessageBox.Show("Unknown security clearance role level detected.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                    break;
            }
        }

        private void HideAllDashboardElements()
        {

            btnMembers.Visible = false;
            btnSettings.Visible = false;
            btnStore.Visible = false;
            btnSchedules.Visible = false;
            btnEquipment.Visible = false;
            

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
            UserSession.ClearSession();

            this.Hide();
            loginForm loginWindow = new loginForm();
            loginWindow.ShowDialog();
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formSchedule());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new formEquipment());
        }
    }
}
