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
                string _name = "";
                int _role = 0;

                if (con.State == ConnectionState.Closed) con.Open();

                
                string query = "SELECT id,name, role FROM users WHERE email = @Email AND password = @Password";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", txtUser.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPwd.Text);

                reader = cmd.ExecuteReader();

              
                if (reader.Read())
                {

                    

                    _name = reader["name"].ToString();
                    _role = Convert.ToInt32(reader["role"]);

                    MessageBox.Show("Login successful!\nWelcome " + _name, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    UserSession.UserID = reader.GetValue(reader.GetOrdinal("id")).ToString();
                    UserSession.UserName = reader["name"].ToString();
                    UserSession.UserRole = Convert.ToInt32(reader["role"]);
                    


                    this.Hide();     
                    mainFrame mainForm = new mainFrame();

                    
                    //mainForm.lblUserName.Text = _name;

                    
                    // 1: Admin, 2: Staff, 3: Coach, 4: Member
                    if (_role == 1)
                    {
                        //mainForm.lblRole.Text = "Admin";
                        //mainForm.adminToolStripMenuItem.Enabled = true;
                        //mainForm.userManagementToolStripMenuItem.Enabled = true;
                    }
                    else if (_role == 2)
                    {
                        //mainForm.lblRole.Text = "Staff";
                        //mainForm.adminToolStripMenuItem.Enabled = false;
                        //mainForm.userManagementToolStripMenuItem.Enabled = true;
                    }
                    else if (_role == 3)
                    {
                        //mainForm.lblRole.Text = "Coach";
                        //mainForm.adminToolStripMenuItem.Enabled = false;
                        //mainForm.userManagementToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        //mainForm.lblRole.Text = "Member";
                        //mainForm.adminToolStripMenuItem.Enabled = false;
                        //mainForm.userManagementToolStripMenuItem.Enabled = false;
                    }

                    
                    mainForm.ShowDialog();
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Invalid email or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }        
            finally
            {
                if (reader != null) reader.Close();
                if (con.State == ConnectionState.Open) con.Close();
            }
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

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

        

        private void label7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
