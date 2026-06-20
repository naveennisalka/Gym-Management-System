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
    public partial class formSchedule : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;


        public formSchedule()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            loadMembers();
        }

        public void loadMembers()
        {
            try
            {
                int i = 0;
                dgvSchedule.Rows.Clear();
                // select and alias columns to match existing UI expectations
                string q = @"SELECT 
                s.id AS SID,
                s.coach_id AS CoachID,
                s.member_id AS MemID,
                m.name AS MemName,
                s.title AS Title,
                s.days_per_week AS dpw
             FROM schedules s
             INNER JOIN users m ON s.member_id = m.id
             WHERE s.coach_id = @LoggedInCoachID
               AND CONCAT(
                   ISNULL(CAST(s.id AS VARCHAR), ''),
                   ISNULL(s.coach_id, ''),
                   ISNULL(s.member_id, ''),
                   ISNULL(m.name, ''),
                   ISNULL(s.title, ''),
                   ISNULL(CAST(s.days_per_week AS VARCHAR), '')
               ) LIKE @search";
                cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
                cmd.Parameters.AddWithValue("@LoggedInCoachID", "COH001");
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i++;
                    dgvSchedule.Rows.Add(
                        i,
                        reader["SID"].ToString(),
                        reader["CoachID"].ToString(),
                        reader["MemID"].ToString(),
                        reader["MemName"].ToString(),
                        reader["Title"].ToString(),
                        reader["dpw"].ToString()
                   
                    );
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvSchedule.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                DataGridViewRow row = dgvSchedule.Rows[e.RowIndex];

                // Extract all parameters exactly from your row columns mapping
                string sid = row.Cells["SID"].Value.ToString();
                string coachId = row.Cells["CoachID"].Value.ToString();
                string memberId = row.Cells["MemID"].Value.ToString();
                string title = row.Cells["Title"].Value.ToString();
                string dpw = row.Cells["dpw"].Value.ToString();

                // Assuming you have access to or want to pass the coach name (or fetch via ID)
                string coachName = GetCoachNameFromDB(coachId);

                // Initialize form using Constructor 2 (Passes ALL data fields)
                formAddSchedule updateForm = new formAddSchedule(sid, coachId, coachName, memberId, title, dpw);
                updateForm.btnSave.Enabled = false;
                updateForm.btnSave.Hide();
                updateForm.ShowDialog();

                // Refresh main list after form closes
                loadMembers();
            }
            else if (colName == "Delete")
            {
                DataGridViewRow row = dgvSchedule.Rows[e.RowIndex];
                string sid = row.Cells[1].Value.ToString(); // Index 1 is your Schedule ID (SID)
                string title = row.Cells[5].Value.ToString(); // Index 5 is the Title text

                // 1. Ask the coach for confirmation before deleting
                DialogResult dialogResult = MessageBox.Show(
                    $"Are you sure you want to permanently delete the schedule '{title}' (ID: {sid})? This will remove all assigned exercises.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // 2. Execute the deletion routine
                    DeleteScheduleFromDB(sid);

                    // 3. Refresh the main DataGridView list immediately
                    loadMembers();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadMembers();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string activeCoachId = "COH001";
            string activeCoachName = "Coach Kamal";

            
            formAddSchedule addScheduleForm = new formAddSchedule(activeCoachId, activeCoachName);
            addScheduleForm.btnUpdate.Enabled = false;
            addScheduleForm.btnUpdate.Hide();

            addScheduleForm.ShowDialog();

            loadMembers();
        }

        private string GetCoachNameFromDB(string coachId)
        {
            string coachName = "Unknown Coach"; // Default 
            string query = "SELECT name FROM users WHERE id = @CoachID";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand selectCmd = new SqlCommand(query, tempCon))
                {
                    selectCmd.Parameters.AddWithValue("@CoachID", coachId);

                    tempCon.Open();
                    object result = selectCmd.ExecuteScalar();

                    if (result != null)
                    {
                        coachName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching coach name: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return coachName;
        }

        private void DeleteScheduleFromDB(string scheduleId)
        {
            string query = "DELETE FROM schedules WHERE id = @ScheduleID";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand deleteCmd = new SqlCommand(query, tempCon))
                {
                    deleteCmd.Parameters.AddWithValue("@ScheduleID", Convert.ToInt32(scheduleId));

                    tempCon.Open();
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Schedule and all its exercises were successfully deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Schedule record could not be found or was already deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error during deletion: " + ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
