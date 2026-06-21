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
            txtPassword.UseSystemPasswordChar = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputEmail = txtEmail.Text.Trim();
            string inputPassword = txtPassword.Text;

            if (VerifyUserLogin(inputEmail, inputPassword, out string currentID, out string currentName, out int currentRole))
            {
                UserSession.UserID = currentID;
                UserSession.UserName = currentName;
                UserSession.UserRole = currentRole;

                this.Hide();
                mainFrame mainDash = new mainFrame();
                mainDash.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Email or Password.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public bool VerifyUserLogin(string email, string password, out string userId, out string userName, out int userRole)
        {
            userId = "";
            userName = "";
            userRole = -1;

            string query = "SELECT id, name, role FROM users WHERE email = @Email AND password = @Password";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(query, tempCon))
                {

                    cmd.Parameters.AddWithValue("@Email", email.Trim());
                    cmd.Parameters.AddWithValue("@Password", password); 

                    tempCon.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userId = reader["id"].ToString();
                            userName = reader["name"].ToString();
                            userRole = Convert.ToInt32(reader["role"]);
                            return true; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Authentication Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false; 
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
            Application.Exit();
        }



        private void pbShowHide_Click_1(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                pbShowHide.Image = Properties.Resources.eye_open; 
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                pbShowHide.Image = Properties.Resources.eye_closed; 
            }
        }
    }
}
