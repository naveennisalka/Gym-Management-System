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
    public partial class formAssetsInfo : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        private string selectedEquipmentId = "";

        public formAssetsInfo()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
        }

        public void SetEquipmentData(string equipmentId)
        {
            selectedEquipmentId = equipmentId;
            LoadEquipmentDetails();
            LoadServices();
        }

        private void LoadEquipmentDetails()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string q = @"
SELECT 
    id,
    name,
    brand,
    category,
    purchase_date,
    status,
    model_number,
    serial_number,
    purchase_price,
    warranty_expiry_date
FROM equipment
WHERE id = @id
";
                cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@id", selectedEquipmentId);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Update label3 with equipment name
                    label3.Text = reader["name"].ToString();

                    // Set all the detail labels
                    label7.Text = reader["id"].ToString();
                    label24.Text = reader["status"].ToString();
                    label10.Text = reader["category"].ToString();
                    label12.Text = reader["brand"].ToString();
                    label14.Text = reader["model_number"].ToString();
                    label16.Text = reader["serial_number"].ToString();
                    label20.Text = reader["purchase_price"].ToString();

                    // Format dates
                    string purchaseDateStr = "";
                    if (reader["purchase_date"] != DBNull.Value && !string.IsNullOrEmpty(reader["purchase_date"].ToString()))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(reader["purchase_date"].ToString(), out dt))
                            purchaseDateStr = dt.ToString("yyyy-MM-dd");
                    }
                    label22.Text = purchaseDateStr;

                    string warrantyDateStr = "";
                    if (reader["warranty_expiry_date"] != DBNull.Value && !string.IsNullOrEmpty(reader["warranty_expiry_date"].ToString()))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(reader["warranty_expiry_date"].ToString(), out dt))
                            warrantyDateStr = dt.ToString("yyyy-MM-dd");
                    }
                    label18.Text = warrantyDateStr;
                }
                reader.Close();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServices()
        {
            try
            {
                int i = 0;
                dgvUser.Rows.Clear();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                // First, check if the service_history table exists
                string checkTableQuery = @"
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'service_history')
    SELECT 1
ELSE
    SELECT 0
";
                SqlCommand checkCmd = new SqlCommand(checkTableQuery, con);
                object tableExists = checkCmd.ExecuteScalar();
                checkCmd.Dispose();

                if (tableExists == null || tableExists.ToString() == "0")
                {
                    // Table doesn't exist - show empty message
                    return;
                }

                string q = @"
SELECT 
    id,
    service_date,
    service_type,
    technician,
    description,
    parts_replaced,
    cost,
    next_service_date,
    status
FROM service_history
WHERE equipment_id = @equipmentId
ORDER BY service_date DESC
";
                // Create a new SqlCommand instance for this query
                SqlCommand serviceCmd = new SqlCommand(q, con);
                serviceCmd.Parameters.AddWithValue("@equipmentId", selectedEquipmentId);
                SqlDataReader serviceReader = serviceCmd.ExecuteReader();

                while (serviceReader.Read())
                {
                    i++;

                    string serviceDateStr = "";
                    if (serviceReader["service_date"] != DBNull.Value && !string.IsNullOrEmpty(serviceReader["service_date"].ToString()))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(serviceReader["service_date"].ToString(), out dt))
                            serviceDateStr = dt.ToString("yyyy-MM-dd");
                    }

                    string nextServiceStr = "";
                    if (serviceReader["next_service_date"] != DBNull.Value && !string.IsNullOrEmpty(serviceReader["next_service_date"].ToString()))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(serviceReader["next_service_date"].ToString(), out dt))
                            nextServiceStr = dt.ToString("yyyy-MM-dd");
                    }

                    dgvUser.Rows.Add(
                        i,
                        serviceReader["id"].ToString(),
                        serviceDateStr,
                        serviceReader["service_type"].ToString(),
                        serviceReader["technician"].ToString(),
                        serviceReader["description"].ToString(),
                        serviceReader["parts_replaced"].ToString(),
                        serviceReader["cost"].ToString(),
                        nextServiceStr,
                        serviceReader["status"].ToString()
                    );
                }
                serviceReader.Close();
                serviceCmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                // If table doesn't exist or query fails, just continue without showing services
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
