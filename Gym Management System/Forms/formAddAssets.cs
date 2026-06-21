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
    public partial class formAddAssets : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        public formAddAssets()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            loadcbStatus();
            loadcbCategoryy();
        }

        public formAddAssets(string id, string name, string brand, string category, string status, string model, string serial, string price, string purchaseDate, string warrantyDate)
        {
            InitializeComponent();
             // Populates dropdowns first!

            string targetAssetId = id;  
            txtName.Text = name;
            txtBrand.Text = brand;
            cbCategory.Text = category;
            cbStatus.Text = status;
            txtModelNum.Text = model;
            txtSerialNum.Text = serial;
            txtPrice.Text = price;

            
            if (DateTime.TryParse(purchaseDate, out DateTime pDate)) dtpPurchaseDate.Value = pDate;
            if (DateTime.TryParse(warrantyDate, out DateTime wDate)) dtpWarrantyExpiry.Value = wDate;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formAddAssets_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Validation Check: Ensure critical fields are filled
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                cbCategory.SelectedIndex == -1 ||
                cbStatus.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtBrand.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Please fill in all required hardware fields before saving.", "Validation Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Validate that the price input is a true numeric value
            if (!decimal.TryParse(txtPrice.Text, out decimal purchasePrice))
            {
                MessageBox.Show("Please enter a valid numeric value for the purchase price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newEquipmentId = GetNextEquipmentID();

            // Define the parameterized SQL INSERT query
            string query = @"INSERT INTO equipment 
                     (id,name, category, status, brand, model_number, serial_number, purchase_date, purchase_price, warranty_expiry_date) 
                     VALUES 
                     (@id,@name, @category, @status, @brand, @model_number, @serial_number, @purchase_date, @purchase_price, @warranty_expiry_date)";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand insertCmd = new SqlCommand(query, tempCon))
                {
                    //Add parameters to prevent any SQL injection attacks
                    insertCmd.Parameters.AddWithValue("@id", newEquipmentId);
                    insertCmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@category", cbCategory.Text);
                    insertCmd.Parameters.AddWithValue("@status", cbStatus.Text);
                    insertCmd.Parameters.AddWithValue("@brand", txtBrand.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@model_number", string.IsNullOrWhiteSpace(txtModelNum.Text) ? (object)DBNull.Value : txtModelNum.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@serial_number", string.IsNullOrWhiteSpace(txtSerialNum.Text) ? (object)DBNull.Value : txtSerialNum.Text.Trim());

                    //Extract pure Date values from your DateTimePickers
                    insertCmd.Parameters.AddWithValue("@purchase_date", dtpPurchaseDate.Value.Date);
                    insertCmd.Parameters.AddWithValue("@purchase_price", purchasePrice);
                    insertCmd.Parameters.AddWithValue("@warranty_expiry_date", dtpWarrantyExpiry.Value.Date);

                    
                    tempCon.Open();
                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("New equipment asset registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        ClearFormInputs();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: Could not save the asset record. " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void ClearFormInputs()
        {
            txtName.Clear();
            cbCategory.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            txtBrand.Clear();
            txtModelNum.Clear();
            txtSerialNum.Clear();
            txtPrice.Clear();
            dtpPurchaseDate.Value = DateTime.Now;
            dtpWarrantyExpiry.Value = DateTime.Now;
        }

        private void loadcbCategoryy()
        {
            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(new string[] {
        "Treadmill",
        "Bench Press",
        "Dumbbell",
        "Squat Rack",
        "Cable Machine",
        "Stationary Bike",
        "Rowing Machine"
        });

            cbCategory.DropDownStyle = ComboBoxStyle.DropDown;
            cbCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbCategory.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbCategory.SelectedIndex = -1;
        }

        private void loadcbStatus()
        {
            cbStatus.Items.Clear();
            cbStatus.Items.AddRange(new string[] {
        "Active",
        "Under Repair",
        "Out of Service",
        "New",  
        "In Storage",       
        "Under Warranty",    
        "Retired " 
    });
            cbStatus.DropDownStyle = ComboBoxStyle.DropDown;
            cbStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbStatus.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbStatus.SelectedIndex = -1;

        }

        private string GetNextEquipmentID()
        {
            string nextId = "EQ-001"; 
            string query = "SELECT TOP 1 id FROM equipment ORDER BY id DESC";

            using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
            using (SqlCommand cmd = new SqlCommand(query, tempCon))
            {
                tempCon.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastId = result.ToString();
                    int currentNum = int.Parse(lastId.Replace("EQ-", ""));
                    nextId = "EQ-" + (currentNum + 1).ToString("D3"); 
                }
            }
            return nextId;
        }
    }
}
