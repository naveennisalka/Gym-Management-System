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
    public partial class formPasswordReset : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        public formPasswordReset()
        {
            InitializeComponent();
            LoadAllUsersIntoComboBox();
        }
        private void LoadAllUsersIntoComboBox()
        {
            cbUsers.Items.Clear(); 

            string query = "SELECT id, name FROM users ORDER BY role ASC, name ASC";

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["id"].ToString();
                            string name = reader["name"].ToString();


                            cbUsers.Items.Add($"{id} - {name}");
                        }
                    }
                }


                cbUsers.DropDownStyle = ComboBoxStyle.DropDown;
                cbUsers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbUsers.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbUsers.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user drop-down: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {

            if (cbUsers.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cbUsers.Text))
            {
                MessageBox.Show("Please select a valid user account to perform a password reset.", "Selection Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedItem = cbUsers.Text; 
            string targetUserId = selectedItem.Split('-')[0].Trim(); 

            DialogResult confirm = MessageBox.Show($"Are you sure you want to reset the password for account '{targetUserId}' to '11111111'?",
                                                   "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            
            string query = "UPDATE users SET password = @DefaultPassword WHERE id = @UserID";

            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    
                    cmd.Parameters.AddWithValue("@DefaultPassword", "11111111");
                    cmd.Parameters.AddWithValue("@UserID", targetUserId);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Password for '{selectedItem}' has been successfully reset to '11111111'.",
                                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cbUsers.SelectedIndex = -1; 
                    }
                    else
                    {
                        MessageBox.Show("Unable to find the specified user record inside the system database.", "Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: Failed to alter password record. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
