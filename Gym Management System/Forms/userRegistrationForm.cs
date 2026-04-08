using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gym_Management_System.Forms
{
    public partial class userRegistrationForm : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();



        MembersForm membersForm;

        Boolean isChecked = false;
        
        public string selectedUserID { get; set; }
        public userRegistrationForm(MembersForm member)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            membersForm = member;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            checkForm();
            if (isChecked == true)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to save this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        cmd = new SqlCommand("INSERT INTO Users (FullName, email, UserRole, DOB, Address, Phone, Status, Gender) VALUES (@FullName, @Email, @UserRole, @DOB, @Address, @Phone, @Status, @Gender)", con);

                        cmd.Parameters.AddWithValue("@FullName", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtMail.Text);


                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text);

                        cmd.Parameters.AddWithValue("@UserRole", cbRole.Text);
                        cmd.Parameters.AddWithValue("@DOB", dtDob.Value);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Status", cbStatus.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        MessageBox.Show("User saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearForm();
                        membersForm.loadMembers();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            checkForm();
            if (isChecked == true)
            {
                try
                {
                    
                    if (MessageBox.Show("Are you sure you want to update this user's details?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        string updateQuery = "UPDATE Users SET FullName = @FullName, email = @Email, UserRole = @UserRole, DOB = @DOB, Address = @Address, Phone = @Phone, Status = @Status, Gender = @Gender WHERE userID = @userID";
                        cmd = new SqlCommand(updateQuery, con);

                        cmd.Parameters.AddWithValue("@userID", selectedUserID);
                        cmd.Parameters.AddWithValue("@FullName", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtMail.Text);
                        cmd.Parameters.AddWithValue("@Gender", cbGender.Text);
                        cmd.Parameters.AddWithValue("@UserRole", cbRole.Text);
                        cmd.Parameters.AddWithValue("@DOB", dtDob.Value);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Status", cbStatus.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // 4. Changed success message
                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                        membersForm.loadMembers();
                    }
                }
                catch (Exception ex)
                {
                    // Ensures the connection is closed even if the update crashes
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void clearForm()
        {
            txtName.Clear();
            txtMail.Clear();
            cbGender.SelectedIndex = -1;
            cbRole.SelectedIndex = -1;
            dtDob.Value = DateTime.Now;
            txtAddress.Clear();
            txtPhone.Clear();
            cbStatus.SelectedIndex = -1;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        public void checkForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtMail.Text) || cbGender.SelectedIndex == -1 || cbRole.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || cbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                isChecked = true;
            }
        }
    }
}
