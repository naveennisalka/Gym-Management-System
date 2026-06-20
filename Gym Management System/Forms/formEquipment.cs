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
    public partial class formEquipment : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;


        public formEquipment()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            loadEquipment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            formAddAssets formAddAssets = new formAddAssets();
            formAddAssets.btnUpdate.Visible = false;
            formAddAssets.ShowDialog();

            loadEquipment();
        }

        private void loadEquipment()
        {
            try
            {
                int i = 0;
                dgvAsset.Rows.Clear();
                string q = @"SELECT 
    id AS ID,
    name AS Name,
    brand AS Brand,
    category AS Category,
    purchase_date AS PurchaseDate,
    status AS Status,
    model_number AS ModelNumber,
    serial_number AS SerialNumber,
    purchase_price AS PurchasePrice,
    warranty_expiry_date AS WarrantyExpiryDate
FROM equipment
WHERE CONCAT(
    ISNULL(id, ''),
    ISNULL(name, ''),
    ISNULL(brand, ''),
    ISNULL(category, ''),
    ISNULL(CONVERT(VARCHAR, purchase_date, 120), ''),
    ISNULL(status, ''),
    ISNULL(model_number, ''),
    ISNULL(serial_number, ''),
    ISNULL(CAST(purchase_price AS VARCHAR), ''),
    ISNULL(CONVERT(VARCHAR, warranty_expiry_date, 120), '')
) LIKE @search";

                cmd = new SqlCommand(q, con);
                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");
                cmd.Parameters.AddWithValue("@LoggedInCoachID", "COH001");
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    i++;
                    dgvAsset.Rows.Add(
                        
                        reader["ID"].ToString(),
                        reader["Name"].ToString(),
                        reader["Brand"].ToString(),
                        reader["Category"].ToString(),
                        reader["PurchaseDate"].ToString(),
                        reader["Status"].ToString(),
                        reader["ModelNumber"].ToString(),
                        reader["SerialNumber"].ToString(),
                        reader["PurchasePrice"].ToString(),
                        reader["WarrantyExpiryDate"].ToString()                   

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

        private void dgvAsset_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvAsset.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                DataGridViewRow row = dgvAsset.Rows[e.RowIndex];

                // Extract the strings sequentially matching your column mapping positions
                string id = row.Cells[0].Value.ToString(); // Index 0 = ID
                string name = row.Cells[1].Value.ToString(); // Index 1 = Name
                string brand = row.Cells[2].Value.ToString(); // Index 2 = Brand
                string category = row.Cells[3].Value.ToString(); // Index 3 = Category

                // Format handles dates safely if raw column values exist
                string purchaseDate = row.Cells[4].Value?.ToString() ?? "";
                string status = row.Cells[5].Value.ToString(); // Index 5 = Status
                string model = row.Cells[6].Value?.ToString() ?? "";
                string serial = row.Cells[7].Value?.ToString() ?? "";
                string price = row.Cells[8].Value.ToString();
                string warrantyDate = row.Cells[9].Value?.ToString() ?? "";

                // Initialize the form with Constructor 2 (Passes ALL details)
                formAddAssets editForm = new formAddAssets(id, name, brand, category, status, model, serial, price, purchaseDate, warrantyDate);
                editForm.btnSave.Hide();
                editForm.ShowDialog();

                // Refresh the grid to show updates after the edit form closes
                loadEquipment();

            }
            else if (colName == "Delete")
            {
                DataGridViewRow row = dgvAsset.Rows[e.RowIndex];
                string assetId = row.Cells[0].Value.ToString();   // Index 0 = ID (e.g., 'EQ-001')
                string assetName = row.Cells[1].Value.ToString(); // Index 1 = Name

                // 1. Double check with the user before deleting permanent records
                DialogResult dialogResult = MessageBox.Show(
                    $"Are you sure you want to permanently delete the asset '{assetName}' ({assetId})?\nThis action cannot be undone and may remove associated maintenance logs.",
                    "Confirm Asset Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    // 2. Run the deletion method
                    DeleteAssetFromDB(assetId);

                    // 3. Refresh the grid instantly to reflect changes
                    loadEquipment(); // Replace with your exact grid loading method name
                }
            }
            else if (colName == "view")
            {
                DataGridViewRow row = dgvAsset.Rows[e.RowIndex];

                // 1. Extract values sequentially from your main list row columns
                string id = row.Cells[0].Value.ToString();
                string name = row.Cells[1].Value.ToString();
                string brand = row.Cells[2].Value.ToString();
                string category = row.Cells[3].Value.ToString();
                string purchaseDate = row.Cells[4].Value?.ToString() ?? "";
                string status = row.Cells[5].Value.ToString();
                string model = row.Cells[6].Value?.ToString() ?? "";
                string serial = row.Cells[7].Value?.ToString() ?? "";
                string price = row.Cells[8].Value.ToString();
                string warrantyDate = row.Cells[9].Value?.ToString() ?? "";

                // 2. Initialize the info preview form using the new custom constructor parameters
                formAssetsInfo infoForm = new formAssetsInfo(id, name, category, status, brand, model, serial, purchaseDate, price, warrantyDate);

                // 3. Display the dialog window on top
                infoForm.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            loadEquipment();
        }

        private void DeleteAssetFromDB(string assetId)
        {
            string query = "DELETE FROM equipment WHERE id = @AssetID";

            try
            {
                using (SqlConnection tempCon = new SqlConnection(dbcon.connection()))
                using (SqlCommand deleteCmd = new SqlCommand(query, tempCon))
                {
                    // Use precise parameterized assignments to secure the query execution
                    deleteCmd.Parameters.AddWithValue("@AssetID", assetId);

                    tempCon.Open();
                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Equipment asset was successfully removed from the registry.", "Asset Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The asset record could not be found or was already deleted.", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error during deletion execution: " + ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
