using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class loginForm : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        
        public loginForm()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _name = "", _role = "";
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Users WHERE username = @Username AND password = @Password", con);
                cmd.Parameters.AddWithValue("@Username", txtUser.Text);
                cmd.Parameters.AddWithValue("@Password", txtPwd.Text);
                reader = cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    _name = reader["FullName"].ToString();
                    _role = reader["UserRole"].ToString();
                    MessageBox.Show("Login successful!\n" + "Welcome " + _name, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    mainFrame mainForm = new mainFrame();
                    //mainForm.lblUser.Text = _name;
                    //mainForm.lnlRole.Text = _role;
                    //if (_role == "Admin")
                    //{
                    //    mainForm.adminToolStripMenuItem.Enabled = true;
                    //    mainForm.userManagementToolStripMenuItem.Enabled = true;
                    //}
                    //else
                    //{
                    //    mainForm.adminToolStripMenuItem.Enabled = false;
                    //    mainForm.userManagementToolStripMenuItem.Enabled = false;
                    //}

                    mainForm.ShowDialog(); 

                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            };
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Plz contact the admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
