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
    public partial class adminDashboardForm : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        public adminDashboardForm()
        {
            InitializeComponent();
            ConfigureDashboardView();


        }

        public void ConfigureDashboardView()
        {
            // 1. Assign global session header labels (if applicable)
            lblCurrentUserName.Text = UserSession.UserName;
            lblCurrentUserRole.Text = GetRoleName(UserSession.UserRole);

            // 2. Reset everything to hidden first for maximum security
            HideAllDashboardElements();

            // 3. Selectively display components depending on the role integer mapping
            switch (UserSession.UserRole)
            {
                case 1: // ---- SUPER ADMIN VIEW ----
                        // Show Admin Sidebar Control Buttons
                    pnlActiveCoaches.Visible = true;
                    pnlTotalActiveMember.Visible = true;
                    pnlMonthlyReveneue.Visible = true;
                    pnlFaultyEqupment.Visible = true;
                    // Execute Database Content Loading for Admin
                    LoadAdminDashboardKPIs();
                    break;

                case 3: // ---- COACH VIEW ----
                        // Show Coach Sidebar Control Buttons
                    pnlPendingSchedules.Visible = true;
                    pnlTodaySessions.Visible = true;
                    pnlAssignedMembers.Visible = true;

                    // Execute Database Content Loading for this specific coach
                    LoadCoachDashboardKPIs(UserSession.UserID);
                    break;

                case 4: // ---- GYM MEMBER VIEW ----
                        // Show Member Sidebar Control Buttons
                    pnlTotalStoreSpend.Visible = true;
                    pnlGymPlan.Visible = true;

                    // Execute Database Content Loading for this specific member
                    LoadMemberDashboardKPIs(UserSession.UserID);
                    break;

                default:
                    MessageBox.Show("Unknown security clearance role level detected.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                    break;
            }
        }

        // Helper routine to sweep clean the screen before rendering the targeted interface views
        private void HideAllDashboardElements()
        {
            
            pnlActiveCoaches.Visible = false;
            pnlTotalActiveMember.Visible = false;
            pnlMonthlyReveneue.Visible = false;
            pnlFaultyEqupment.Visible = false;

            pnlTotalStoreSpend.Visible = false;
            pnlGymPlan.Visible = false;
            
            pnlPendingSchedules.Visible = false;
            pnlTodaySessions.Visible = false;
            pnlAssignedMembers.Visible = false;

        }

        // Simple helper to format raw role indices into clean display strings
        private string GetRoleName(int role)
        {
            if (role == 1) return "System Administrator";
            if (role == 3) return "Fitness Coach";
            if (role == 4) return "Gym Member";
            return "Guest";
        }

        private void formAdmindashboardLoad(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblgreeting_Click(object sender, EventArgs e)
        {

        }

        public void LoadAdminDashboardKPIs()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                {
                    con.Open();

                    // 1. Total Active Members
                    string qActive = "SELECT COUNT(*) FROM memberships WHERE end_date >= GETDATE();";
                    using (SqlCommand cmd = new SqlCommand(qActive, con))
                    {
                        lblActiveMembers.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // 2. Monthly Revenue (Memberships + Retail Store Orders)
                    string qRevenue = @"SELECT 
                (SELECT ISNULL(SUM(total_amount), 0.00) FROM orders WHERE MONTH(order_date) = MONTH(GETDATE()) AND YEAR(order_date) = YEAR(GETDATE())) +
                (SELECT ISNULL(SUM(gp.price), 0.00) FROM memberships m INNER JOIN gym_plans gp ON m.plan_id = gp.id WHERE MONTH(m.start_date) = MONTH(GETDATE()) AND YEAR(m.start_date) = YEAR(GETDATE()))";
                    using (SqlCommand cmd = new SqlCommand(qRevenue, con))
                    {
                        decimal revenue = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblMonthlyRevenue.Text = "Rs. " + revenue.ToString("N2");
                    }

                    // 3. Active Coaches (Role = 3, Status = 1)
                    string qCoaches = "SELECT COUNT(*) FROM users WHERE role = 3 AND status = 1;";
                    using (SqlCommand cmd = new SqlCommand(qCoaches, con))
                    {
                        lblActiveCoaches.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }

                    // 4. Equipment Breakdown Alert (Under Repair + Out of Service)
                    string qEquipment = "SELECT COUNT(*) FROM equipment WHERE status IN ('Under Repair', 'Out of Service');";
                    using (SqlCommand cmd = new SqlCommand(qEquipment, con))
                    {
                        lblFaultyEquipment.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Admin KPIs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetGreeting() {

            int hour = DateTime.Now.Hour;
            if (hour >= 5 && hour < 12)
            {
                return "Good Morning";
            }
            else if (hour >= 12 && hour < 17)
            {
                return "Good Afternoon";
            }
            else if (hour >= 17 && hour < 21)
            {
                return "Good Evening";
            }
            else
            {
                return "Good Night";
            }
        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

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

        public void LoadMemberDashboardKPIs(string loggedInMemberID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                {
                    con.Open();

                    // 1. Current Membership Plan & Countdown
                    string qExpiry = @"SELECT TOP 1 gp.name, DATEDIFF(DAY, GETDATE(), m.end_date) AS DaysRemaining
                               FROM memberships m
                               INNER JOIN gym_plans gp ON m.plan_id = gp.id
                               WHERE m.user_id = @MemberID
                               ORDER BY m.end_date DESC;";
                    using (SqlCommand cmd = new SqlCommand(qExpiry, con))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", loggedInMemberID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int daysLeft = Convert.ToInt32(reader["DaysRemaining"]);
                                lblPlanName.Text = reader["name"].ToString();
                                lblMembershipCountdown.Text = daysLeft >= 0 ? $"{daysLeft} Days Left" : "Expired";
                            }
                            else
                            {
                                lblPlanName.Text = "No Active Plan";
                                lblMembershipCountdown.Text = "N/A";
                            }
                        }
                    }

                    // 2. Total Retail Purchases Summary Metric
                    string qStoreTotal = "SELECT ISNULL(SUM(total_amount), 0.00) FROM orders WHERE user_id = @MemberID;";
                    using (SqlCommand cmd = new SqlCommand(qStoreTotal, con))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", loggedInMemberID);
                        decimal totalSpent = Convert.ToDecimal(cmd.ExecuteScalar());
                        lblTotalStoreSpend.Text = "Rs. " + totalSpent.ToString("N2");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Member KPIs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            formChangePassword passwordResetForm = new formChangePassword();
            passwordResetForm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            formAddSchedule addScheduleForm = new formAddSchedule();
            addScheduleForm.btnUpdate.Enabled = false;
            addScheduleForm.btnUpdate.Hide();

            addScheduleForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            formAddAssets formAddAssets = new formAddAssets();
            formAddAssets.btnUpdate.Visible = false;
            formAddAssets.ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userRegistrationForm userRegForm = new userRegistrationForm();
            userRegForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formManageStoreItems manageStoreItems = new formManageStoreItems();
            manageStoreItems.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formManageStoreItems ExpiredStoreItems = new formManageStoreItems();
            ExpiredStoreItems.chkExpiredItems.Checked = true;
            ExpiredStoreItems.ShowDialog();
        }
    }
}
