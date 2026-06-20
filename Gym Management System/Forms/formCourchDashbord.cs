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


    public partial class formCourchDashbord : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        public formCourchDashbord()
        {
            InitializeComponent();
        }



        public void LoadCoachDashboardKPIs(string loggedInCoachID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                {
                    con.Open();

                    // 1. Assigned Members count
                    string qAssigned = "SELECT COUNT(DISTINCT member_id) FROM schedules WHERE coach_id = @CoachID;";
                    using (SqlCommand cmd = new SqlCommand(qAssigned, con))
                    {
                        cmd.Parameters.AddWithValue("@CoachID", loggedInCoachID);
                        lblAssignedMembers.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // 2. Today's Assigned Routines
                    string qTodayRoutines = @"SELECT COUNT(DISTINCT s.id) 
                                      FROM schedules s
                                      INNER JOIN schedule_exercises se ON s.id = se.schedule_id
                                      WHERE s.coach_id = @CoachID 
                                        AND se.day_of_week = DATENAME(WEEKDAY, GETDATE());";
                    using (SqlCommand cmd = new SqlCommand(qTodayRoutines, con))
                    {
                        cmd.Parameters.AddWithValue("@CoachID", loggedInCoachID);
                        lblTodaySessions.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // 3. Pending Configurations (Schedules with no active exercises setup yet)
                    string qPending = @"SELECT COUNT(*) FROM schedules s
                                LEFT JOIN schedule_exercises se ON s.id = se.schedule_id
                                WHERE s.coach_id = @CoachID AND se.id IS NULL;";
                    using (SqlCommand cmd = new SqlCommand(qPending, con))
                    {
                        cmd.Parameters.AddWithValue("@CoachID", loggedInCoachID);
                        lblPendingSchedules.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Coach KPIs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}