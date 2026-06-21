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
    public partial class formAddService : Form
    {
        string trackingEquipmentId = "";
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        public formAddService( string EquipmentId)
        {
            InitializeComponent();
            trackingEquipmentId = EquipmentId;
            InitializeServiceTypeDropDown();
            InitializeServiceStatusDropDown();
        }

        private void formAddService_Load(object sender, EventArgs e)
        {

        }

        private void InitializeServiceStatusDropDown()
        {
            cbServiceStatus.Items.Clear();

            // Add transaction status flags
            cbServiceStatus.Items.AddRange(new string[] {
        "Completed",
        "In Progress",
        "Pending Parts",
        "Cancelled"
    });

            cbServiceStatus.DropDownStyle = ComboBoxStyle.DropDown;
            cbServiceStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbServiceStatus.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbServiceStatus.SelectedIndex = -1;
        }

        private void InitializeServiceTypeDropDown()
        {
            cbServiceType.Items.Clear();

            // Add typical service operation classifications
            cbServiceType.Items.AddRange(new string[] {
        "Routine Maintenance",
        "Repair",
        "Inspection",
        "Part Replacement",
        "Calibration",
        "Emergency Fix"
    });

            // Configure AutoComplete for quick search filtering
            cbServiceType.DropDownStyle = ComboBoxStyle.DropDown;
            cbServiceType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbServiceType.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbServiceType.SelectedIndex = -1;
        }
        private string GetNextServiceID()
        {
            string nextId = "SRV-101"; // Fallback default if table is empty
            string query = "SELECT TOP 1 service_id FROM service_log ORDER BY service_id DESC";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(query, tempCon))
                {
                    tempCon.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string lastId = result.ToString(); //  "SRV-103"
                        int currentNum = int.Parse(lastId.Replace("SRV-", ""));
                        nextId = "SRV-" + (currentNum + 1).ToString("D3"); // Becomes "SRV-104"
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating ID: " + ex.Message);
            }
            return nextId;
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            //primary transaction fields are filled out
            if (cbServiceType.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtTechnician.Text) ||
                string.IsNullOrWhiteSpace(txtCost.Text) ||
                cbServiceStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill out all required service fields.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure the numerical cost format is clean
            if (!decimal.TryParse(txtCost.Text.Trim(), out decimal serviceCost))
            {
                MessageBox.Show("Please enter a valid numeric calculation for the cost field.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generate the next auto-incremented primary key
            string newServiceId = GetNextServiceID();

           
            string query = @"INSERT INTO service_log 
                     (service_id, equipment_id, service_date, service_type, technician, description, parts_replaced, cost, next_service, status) 
                     VALUES 
                     (@service_id, @equipment_id, @service_date, @service_type, @technician, @description, @parts_replaced, @cost, @next_service, @status)";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand insertCmd = new SqlCommand(query, tempCon))
                {
                  
                    insertCmd.Parameters.AddWithValue("@service_id", newServiceId);
                    insertCmd.Parameters.AddWithValue("@equipment_id", trackingEquipmentId);
                    insertCmd.Parameters.AddWithValue("@service_date", dtpServiceDate.Value.Date);
                    insertCmd.Parameters.AddWithValue("@service_type", cbServiceType.Text);
                    insertCmd.Parameters.AddWithValue("@technician", txtTechnician.Text.Trim());

                    
                    insertCmd.Parameters.AddWithValue("@description", string.IsNullOrWhiteSpace(txtDescription.Text) ? (object)DBNull.Value : txtDescription.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@parts_replaced", string.IsNullOrWhiteSpace(txtPartsReplaced.Text) ? "None" : txtPartsReplaced.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@cost", serviceCost);

                    
                    if (cbServiceStatus.Text == "In Progress")
                    {
                        insertCmd.Parameters.AddWithValue("@next_service", DBNull.Value);
                    }
                    else
                    {
                        insertCmd.Parameters.AddWithValue("@next_service", dtpNextServiceDate.Value.Date);
                    }

                    insertCmd.Parameters.AddWithValue("@status", cbServiceStatus.Text);

                    
                    tempCon.Open();
                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Service log entry '{newServiceId}' committed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: Failed to log service transaction. " + ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
