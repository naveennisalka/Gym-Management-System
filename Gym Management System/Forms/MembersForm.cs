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
                cmd = new SqlCommand("SELECT * FROM Users WHERE CONCAT(userId,FullName, DOB, Address, Gender,Phone ,Status,UserRole,StoreCreditBalance) LIKE @search", con);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i++;
                    dgvUser.Rows.Add(i, reader["userId"].ToString(), reader["FullName"].ToString(), Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd"), reader["Address"].ToString(), reader["Gender"].ToString(), reader["Phone"].ToString(), reader["Status"].ToString(), reader["UserRole"].ToString(), reader["StoreCreditBalance"].ToString());
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
                userRegForm.dtDob.Value = Convert.ToDateTime(dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString());
                userRegForm.txtAddress.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();
                userRegForm.cbGender.Text = dgvUser.Rows[e.RowIndex].Cells[5].Value.ToString();
                userRegForm.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[6].Value.ToString();

                userRegForm.btnSave.Enabled = false;
                userRegForm.btnUpdate.Enabled = true;
                userRegForm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM Users WHERE userId = @userId", con);
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
    }
}
