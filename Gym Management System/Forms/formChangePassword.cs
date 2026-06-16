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

namespace Gym_Management_System.Forms
{
    public partial class formChangePassword : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        public formChangePassword()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // basic validation
            if (string.IsNullOrWhiteSpace(txtOldPwd.Text))
            {
                MessageBox.Show("Please enter your current password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNewPwd.Text))
            {
                MessageBox.Show("Please enter a new password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNewPwd.Text != txtNewPwdRe.Text)
            {
                MessageBox.Show("New password and re-entered password do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(UserSession.UserID))
            {
                MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                // get current password
                cmd = new SqlCommand("SELECT password FROM users WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", UserSession.UserID);
                var obj = cmd.ExecuteScalar();
                string currentPwd = obj != null && obj != DBNull.Value ? obj.ToString() : string.Empty;

                if (currentPwd != txtOldPwd.Text)
                {
                    MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // update password
                var upd = new SqlCommand("UPDATE users SET password = @pwd WHERE id = @id", con);
                upd.Parameters.AddWithValue("@pwd", txtNewPwd.Text);
                upd.Parameters.AddWithValue("@id", UserSession.UserID);
                upd.ExecuteNonQuery();

                MessageBox.Show("Password changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null) { try { reader.Close(); } catch { } }
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
