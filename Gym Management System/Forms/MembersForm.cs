 using Gym_Management_System.Forms;
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
    public partial class MembersForm : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        public MembersForm()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            loadMembers();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                loadMembers();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userRegistrationForm userRegForm = new userRegistrationForm(this);
            userRegForm.ShowDialog();
        }

        public void loadMembers()
        {
            try
            {
                int i = 0;
                dgvUser.Rows.Clear();
                string q = @"
SELECT 
  id AS userId,
  name AS FullName,
  dob AS DOB,
  address AS Address,
  gender AS Gender,
  phone AS Phone,
  status AS Status,
  role AS UserRole,
  email AS Email
FROM users
WHERE CONCAT(ISNULL(id, ''), ISNULL(name, ''), ISNULL(dob, ''), ISNULL(address, ''), ISNULL(gender, ''), ISNULL(phone, ''), ISNULL(status, ''), ISNULL(role, ''), ISNULL(email, '')) LIKE @search
";
                cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i++;
                    string dobStr = "";
                    if (reader["DOB"] != DBNull.Value && !string.IsNullOrEmpty(reader["DOB"].ToString()))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(reader["DOB"].ToString(), out dt)) dobStr = dt.ToString("yyyy-MM-dd");
                    }

                    dgvUser.Rows.Add(
                        i,
                        reader["userId"].ToString(),
                        reader["FullName"].ToString(),
                        dobStr,
                        reader["Address"].ToString(),
                        reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : string.Empty,
                        reader["Phone"].ToString(),
                        reader["Status"].ToString(),
                        reader["UserRole"].ToString(),
                        reader["Email"].ToString()
                    );
                    try
                    {
                        var addedRow = dgvUser.Rows[dgvUser.Rows.Count - 1];
                        var emailVal = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty;
                        int emailColIndex = -1;
                        for (int ci = 0; ci < dgvUser.Columns.Count; ci++)
                        {
                            if (dgvUser.Columns[ci].HeaderText.Equals("Email", StringComparison.OrdinalIgnoreCase))
                            {
                                emailColIndex = ci;
                                break;
                            }
                        }
                        if (emailColIndex >= 0)
                        {
                            addedRow.Cells[emailColIndex].Value = emailVal;
                        }
                    }
                    catch { }
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;

            if (colName == "Edit")
            {
                userRegistrationForm userRegForm = new userRegistrationForm(this);
                userRegForm.selectedUserID = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userRegForm.txtName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                DateTime dt;
                if (DateTime.TryParse(dgvUser.Rows[e.RowIndex].Cells[3].Value?.ToString(), out dt)) userRegForm.dtDob.Value = dt;
                userRegForm.txtAddress.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? string.Empty;
                userRegForm.cbGender.Text = dgvUser.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? string.Empty;
                userRegForm.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[6].Value?.ToString() ?? string.Empty;
                userRegForm.txtMail.Text = dgvUser.Rows[e.RowIndex].Cells[9].Value?.ToString() ?? string.Empty;
                var roleVal = dgvUser.Rows[e.RowIndex].Cells[8].Value?.ToString();
                int roleNum;
                if (int.TryParse(roleVal, out roleNum))
                {
                    switch (roleNum)
                    {
                        case 1: userRegForm.cbRole.Text = "Admin"; break;
                        case 2: userRegForm.cbRole.Text = "Cashier"; break;
                        case 3: userRegForm.cbRole.Text = "Coache"; break;
                        default: userRegForm.cbRole.Text = "Member"; break;
                    }
                }
                var statusVal = dgvUser.Rows[e.RowIndex].Cells[7].Value?.ToString();
                int st;
                if (int.TryParse(statusVal, out st) && st >= 0 && st < userRegForm.cbStatus.Items.Count)
                {
                    userRegForm.cbStatus.SelectedIndex = st;
                }

                userRegForm.btnSave.Visible = false;
                userRegForm.btnUpdate.Visible = true;
                userRegForm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM users WHERE id = @userId", con);
                        cmd.Parameters.AddWithValue("@userId", dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString());
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Member deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            loadMembers();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userRegistrationForm userRegForm = new userRegistrationForm(this);
            userRegForm.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
