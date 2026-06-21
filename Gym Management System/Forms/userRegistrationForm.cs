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

        public userRegistrationForm()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            membersForm = null; // MembersForm එකක් දැනට නැත
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        public userRegistrationForm(MembersForm member)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            membersForm = member;
            // default for new registration: show Save, hide Update
            btnSave.Visible = true;
            btnUpdate.Visible = false;
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

                        // generate a new id for user
                        string roleText = cbRole.SelectedItem?.ToString() ?? "Member";
                        string prefix = "MEM";
                        int roleId = 4;
                        if (roleText.Equals("Admin", StringComparison.OrdinalIgnoreCase)) { prefix = "ADM"; roleId = 1; }
                        else if (roleText.Equals("Cashier", StringComparison.OrdinalIgnoreCase) || roleText.Equals("Staff", StringComparison.OrdinalIgnoreCase)) { prefix = "STF"; roleId = 2; }
                        else if (roleText.Equals("Coache", StringComparison.OrdinalIgnoreCase) || roleText.Equals("Coach", StringComparison.OrdinalIgnoreCase)) { prefix = "COA"; roleId = 3; }

                        string newId = prefix + DateTime.Now.ToString("yyMMddHHmmss");

                        cmd = new SqlCommand("INSERT INTO users (id, name, email, password, role, phone, address, salary, status, dob, gender) VALUES (@id, @name, @email, @password, @role, @phone, @address, @salary, @status, @dob, @gender)", con);
                        cmd.Parameters.AddWithValue("@id", newId);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@email", txtMail.Text);
                        cmd.Parameters.AddWithValue("@password", "");
                        cmd.Parameters.AddWithValue("@role", roleId);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@salary", 0);
                        cmd.Parameters.AddWithValue("@status", cbStatus.SelectedIndex >= 0 ? cbStatus.SelectedIndex : 0);
                        cmd.Parameters.AddWithValue("@dob", dtDob.Value.Date);
                        cmd.Parameters.AddWithValue("@gender", cbGender.SelectedItem?.ToString() ?? "");

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        MessageBox.Show("User saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearForm();
                        if (membersForm != null)
                        {
                            membersForm.loadMembers(); 
                        }
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

                        string updateQuery = "UPDATE users SET name = @name, email = @email, role = @role, dob = @dob, address = @address, phone = @phone, status = @status, gender = @gender WHERE id = @id";
                        cmd = new SqlCommand(updateQuery, con);

                        // map role text back to numeric role
                        int roleId = 4;
                        var roleText = cbRole.SelectedItem?.ToString() ?? cbRole.Text;
                        if (roleText.Equals("Admin", StringComparison.OrdinalIgnoreCase)) roleId = 1;
                        else if (roleText.Equals("Cashier", StringComparison.OrdinalIgnoreCase) || roleText.Equals("Staff", StringComparison.OrdinalIgnoreCase)) roleId = 2;
                        else if (roleText.Equals("Coache", StringComparison.OrdinalIgnoreCase) || roleText.Equals("Coach", StringComparison.OrdinalIgnoreCase)) roleId = 3;

                        cmd.Parameters.AddWithValue("@id", selectedUserID);
                        cmd.Parameters.AddWithValue("@name", txtName.Text);
                        cmd.Parameters.AddWithValue("@email", txtMail.Text);
                        cmd.Parameters.AddWithValue("@gender", cbGender.SelectedItem?.ToString() ?? cbGender.Text);
                        cmd.Parameters.AddWithValue("@role", roleId);
                        cmd.Parameters.AddWithValue("@dob", dtDob.Value.Date);
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@status", cbStatus.SelectedIndex >= 0 ? cbStatus.SelectedIndex : 0);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // 4. Changed success message
                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                        if (membersForm != null)
                        {
                            membersForm.loadMembers(); 
                        }
                        ;
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

        private void userRegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
