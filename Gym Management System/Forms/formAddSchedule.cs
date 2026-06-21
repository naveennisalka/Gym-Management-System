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
    public partial class formAddSchedule : Form
    {
        int editingRowIndex = -1;
        int targetScheduleId = -1;
        bool isUpdateMode = false;


        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;


        public formAddSchedule()
        {
            con = new SqlConnection(dbcon.connection());
            InitializeComponent();
            LoadExercisesToComboBox();
            LoadMembersToComboBox();
            loadcbDay();         

        }

        // Constructor 1:  adding a BRAND NEW schedule
        public formAddSchedule(string coachId, string coachName)
        {
            InitializeComponent();
            InitializeFormDefaults();

            lblCID.Text = UserSession.UserID;
            lblCName.Text = UserSession.UserName;

            isUpdateMode = false;
            
        }

        // Constructor 2:  UPDATING an existing schedule from the grid
        public formAddSchedule(string scheduleId, string memberId, string title, string daysPerWeek)
        {
            InitializeComponent();
            InitializeFormDefaults();

            // Store keys internally for the database UPDATE query later
            targetScheduleId = Convert.ToInt32(scheduleId);
            isUpdateMode = true;

            lblCID.Text = UserSession.UserID;
            lblCName.Text = UserSession.UserName;

            txtScheduleTitle.Text = title;
            txtDPW.Text = daysPerWeek;

            
            cbMember.SelectedValue = memberId;

            LoadSavedExercisesToGrid(scheduleId);
        }

        private void InitializeFormDefaults()
        {
            con = new SqlConnection(dbcon.connection());
            LoadExercisesToComboBox();
            LoadMembersToComboBox();
            loadcbDay();
        }

        private void LoadSavedExercisesToGrid(string scheduleId)
        {
            try
            {
                dgvExecise.Rows.Clear();
                string q = "SELECT exercise_id, (SELECT name FROM exercises WHERE id=exercise_id), sets, reps, day_of_week, notes FROM schedule_exercises WHERE schedule_id = @sid";
                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    cmd.Parameters.AddWithValue("@sid", scheduleId);
                    if (con.State == ConnectionState.Closed) con.Open();
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        dgvExecise.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5]);
                    }
                    r.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading details: " + ex.Message); }
            finally { con.Close(); }
        }


        private void formAddSchedule_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormAddExecise addExerciseForm = new FormAddExecise();
            addExerciseForm.btnUpdate.Enabled = false;
            addExerciseForm.txtExID.Visible = false;
            addExerciseForm.label2.Visible = false;
            addExerciseForm.ShowDialog();
        }

        private void loadcbDay()
        {
            cbDay.Items.Clear();
            cbDay.Items.AddRange(new string[] { "Day 01", "Day 02", "Day 03", "Day 04", "Day 05", "Day 06", "Day 07" });

            cbDay.DropDownStyle = ComboBoxStyle.DropDown;
            cbDay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbDay.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbDay.SelectedIndex = -1;
        }

        private void LoadMembersToComboBox()
        {
            try
            {
                
                string q = "SELECT id, CONCAT(id, ' - ', name) AS DisplayMemberText FROM users WHERE role = 4 ORDER BY name ASC";

                
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(q, tempCon))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cbMember.DataSource = dt;
                        cbMember.DisplayMember = "DisplayMemberText"; // Shows: "MEM001 - Nimal Perera"
                        cbMember.ValueMember = "id";               
                    }
                }

                
                cbMember.DropDownStyle = ComboBoxStyle.DropDown;
                cbMember.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbMember.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbMember.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load members into dropdown: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExercisesToComboBox()
        {
            try
            {
                // Concatenate ID and Name with a clean separator for the UI display
                string q = "SELECT id, CONCAT(id, ' - ', name) AS DisplayText FROM exercises ORDER BY name ASC";

                using (SqlCommand cmd = new SqlCommand(q, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        cbExercise.DataSource = dt;
                        cbExercise.DisplayMember = "DisplayText";
                        cbExercise.ValueMember = "id";           
                    }
                }

                cbExercise.DropDownStyle = ComboBoxStyle.DropDown;
                cbExercise.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbExercise.AutoCompleteSource = AutoCompleteSource.ListItems;

                cbExercise.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load exercises into dropdown: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbExercise_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (cbExercise.SelectedValue == null || cbExercise.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cbExercise.Text) || string.IsNullOrWhiteSpace(txtSet.Text) || string.IsNullOrWhiteSpace(txtRep.Text) || string.IsNullOrWhiteSpace(cbDay.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 1. Collect inputs from form controls
            string exId = cbExercise.SelectedValue.ToString(); 

            string fullText = cbExercise.Text;
            string exName = fullText;           
            if (fullText.Contains("-"))
            {                
                exName = fullText.Substring(fullText.IndexOf('-') + 1).Trim();
            }

            int sets = int.Parse(txtSet.Text);
            int reps = int.Parse(txtRep.Text);
            string dayOfWeek = cbDay.Text;
            string notes = txtNote.Text;

            if (editingRowIndex > -1)
            {
                // Update existing row
                DataGridViewRow row = dgvExecise.Rows[editingRowIndex];
                row.Cells[0].Value = exId;
                row.Cells[1].Value = exName;
                row.Cells[2].Value = sets;
                row.Cells[3].Value = reps;
                row.Cells[4].Value = dayOfWeek;
                row.Cells[5].Value = notes;
            }
            else
            {
                // Add brand new row
                dgvExecise.Rows.Add(exId, exName, sets, reps, dayOfWeek, notes);
            }                    

            editingRowIndex = -1;
            button3.Text = "+ Add";

            cbDay.SelectedIndex = -1;
            cbExercise.SelectedIndex = -1;
            txtSet.Clear();
            txtRep.Clear();
            txtNote.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cbDay.SelectedIndex = -1;
            cbExercise.SelectedIndex = -1;
            txtSet.Clear();
            txtRep.Clear();
            txtNote.Clear();
        }

        private void dgvExecise_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvExecise.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                editingRowIndex = e.RowIndex;
         
                DataGridViewRow row = dgvExecise.Rows[editingRowIndex];
                
                cbExercise.SelectedValue = row.Cells[0].Value.ToString(); // Index 0 = exId
                txtSet.Text = row.Cells[2].Value.ToString(); // Index 2 = sets
                txtRep.Text = row.Cells[3].Value.ToString(); // Index 3 = reps
                cbDay.Text = row.Cells[4].Value.ToString(); // Index 4 = dayOfWeek
                txtNote.Text = row.Cells[5].Value?.ToString() ?? ""; // Index 5 = notes

                button3.Text = "✓ Update";
                
            }
            else if (colName == "Delete")
            {
                string exName = dgvExecise.Rows[e.RowIndex].Cells[1].Value.ToString(); // Index 1 = Exercise Name

                DialogResult dialogResult = MessageBox.Show(
                    $"Are you sure you want to remove '{exName}' from this schedule list?",
                    "Remove Exercise",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // Remove the row directly from the DataGridView collections in memory
                    dgvExecise.Rows.RemoveAt(e.RowIndex);

                    // Safety: If the coach was editing this exact row and decided to delete it, reset the input form fields
                    if (editingRowIndex == e.RowIndex)
                    {
                        editingRowIndex = -1;
                        button3.Text = "+ Add";

                        cbDay.SelectedIndex = -1;
                        cbExercise.SelectedIndex = -1;
                        txtSet.Clear();
                        txtRep.Clear();
                        txtNote.Clear();
                    }
                    else if (editingRowIndex > e.RowIndex)
                    {
                        // Shifting our pointer back by 1 if a row preceding our active editing row index is dropped
                        editingRowIndex--;
                    }
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvExecise.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one exercise to the schedule.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            if (cbMember.SelectedValue == null || string.IsNullOrWhiteSpace(txtScheduleTitle.Text) || string.IsNullOrWhiteSpace(txtDPW.Text))
            {
                MessageBox.Show("Please complete all upper schedule fields before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (con.State == ConnectionState.Closed) con.Open();
            SqlTransaction transaction = con.BeginTransaction(); // Start transaction to prevent partial saves

            try
            {
                // Step 1: Insert the high-level Schedule details
                string insertScheduleQuery = @"INSERT INTO schedules (title, member_id, coach_id, days_per_week) 
                                       VALUES (@title, @member_id, @coach_id, @days_per_week);
                                       SELECT SCOPE_IDENTITY();";

                SqlCommand scheduleCmd = new SqlCommand(insertScheduleQuery, con, transaction);
                scheduleCmd.Parameters.AddWithValue("@title", txtScheduleTitle.Text);
                scheduleCmd.Parameters.AddWithValue("@member_id", cbMember.SelectedValue.ToString()); // Fixed: Pulls 'MEM001' from ComboBox Value
                scheduleCmd.Parameters.AddWithValue("@coach_id", UserSession.UserID);                          // Active logged-in coach
                scheduleCmd.Parameters.AddWithValue("@days_per_week", int.Parse(txtDPW.Text));

                // Execute and get the newly generated schedule_id
                int newScheduleId = Convert.ToInt32(scheduleCmd.ExecuteScalar());

                // Step 2: Loop through every row in your temporary GridView and insert into schedule_exercises
                string insertExerciseQuery = @"INSERT INTO schedule_exercises (schedule_id, exercise_id, sets, reps, day_of_week, notes)
                                       VALUES (@schedule_id, @exercise_id, @sets, @reps, @day_of_week, @notes);";

                foreach (DataGridViewRow row in dgvExecise.Rows) // Fixed: Name changed to match dgvExecise
                {
                    // Skip the new uncommitted empty row at the bottom of the grid if there is one
                    if (row.IsNewRow) continue;

                    SqlCommand exerciseCmd = new SqlCommand(insertExerciseQuery, con, transaction);
                    exerciseCmd.Parameters.AddWithValue("@schedule_id", newScheduleId);

                    // Fixed: Accessing cells via exact zero-based cell indexes matching your add layout map
                    exerciseCmd.Parameters.AddWithValue("@exercise_id", row.Cells[0].Value.ToString());
                    exerciseCmd.Parameters.AddWithValue("@sets", Convert.ToInt32(row.Cells[2].Value));
                    exerciseCmd.Parameters.AddWithValue("@reps", Convert.ToInt32(row.Cells[3].Value));
                    exerciseCmd.Parameters.AddWithValue("@day_of_week", row.Cells[4].Value.ToString());
                    exerciseCmd.Parameters.AddWithValue("@notes", row.Cells[5].Value?.ToString() ?? "");

                    exerciseCmd.ExecuteNonQuery();
                }

                // Commit everything if no errors occurred
                transaction.Commit();
                MessageBox.Show("Schedule saved successfully to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear grid and inputs after successful save
                dgvExecise.Rows.Clear();
                txtScheduleTitle.Clear();
                txtDPW.Clear();
                cbMember.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                // Roll back everything to keep database clean if an exercise row fails
                transaction.Rollback();
                MessageBox.Show("Database Error: Save operation cancelled. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
