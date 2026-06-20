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
        string currentAssetId = "";

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        public formAssetsInfo()
        {
            InitializeComponent();
        }

        public formAssetsInfo(string id, string name, string category, string status, string brand, string model, string serial, string purchaseDate, string price, string warrantyDate)
        {
            InitializeComponent();
            


            currentAssetId = id;

            //Map the data strings directly to your UI Labels
            lblID.Text = id;             
            lblAssetName.Text = name;    
            lblCategory.Text = category; 
            lblStatus.Text = status;     
            lblBrand.Text = brand;       
            lblModel.Text = model;       
            lblSerial.Text = serial;     

            // Format dates or clear if empty strings
            lblPurchaseDate.Text = purchaseDate; 
            lblPurchasePrice.Text = price;       
            lblWarranty.Text = warrantyDate;

            LoadServiceHistoryGrid();
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

        private void LoadServiceHistoryGrid()
        {
            try
            {
                dgvServices.Rows.Clear();

                // FIXED: Changed table name from 'services' to 'service_log'
                string q = @"SELECT service_id, service_date, service_type, technician, description, parts_replaced, cost, next_service, status 
                     FROM service_log 
                     WHERE equipment_id = @AssetID 
                     ORDER BY service_date DESC";

                int counter = 0;

                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(q, tempCon))
                {
                    cmd.Parameters.AddWithValue("@AssetID", currentAssetId);
                    tempCon.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            counter++;

                            string nextServiceDate = reader["next_service"] == DBNull.Value
                                ? "N/A"
                                : Convert.ToDateTime(reader["next_service"]).ToString("yyyy-MM-dd");

                            dgvServices.Rows.Add(
                                counter,
                                reader["service_id"].ToString(),
                                Convert.ToDateTime(reader["service_date"]).ToString("yyyy-MM-dd"),
                                reader["service_type"].ToString(),
                                reader["technician"].ToString(),
                                reader["description"].ToString(),
                                reader["parts_replaced"]?.ToString() ?? "None",
                                Convert.ToDecimal(reader["cost"]).ToString("F2"),
                                nextServiceDate,
                                reader["status"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load service transaction logs: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            formAddService addServiceWindow = new formAddService(currentAssetId);

            if (addServiceWindow.ShowDialog() == DialogResult.OK)
            {
                
                LoadServiceHistoryGrid();
            }
        }
    }
}
