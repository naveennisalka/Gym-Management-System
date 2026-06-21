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
    public partial class formManageStoreItems : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        public formManageStoreItems()
        {
            InitializeComponent();
            LoadStoreItemsGrid();
        }



        public void LoadStoreItemsGrid()
        {
            try
            {
                dgvStoreItems.Rows.Clear(); // Make sure this matches your DataGridView name

                string query = @"SELECT id, name, description, price, stock, category, expiry_date, min_stock_level 
                         FROM store_items 
                         WHERE (id LIKE @search OR name LIKE @search OR category LIKE @search)";

                if (chkExpiredItems.Checked)
                {
                    // Filters items where expiry_date has passed AND ignores category = 'Memberships' (which are NULL)
                    query += " AND expiry_date IS NOT NULL AND expiry_date < CAST(GETDATE() AS DATE)";
                }

                using (SqlConnection con = new SqlConnection(dbcon.connection()))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text.Trim() + "%");
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;

                            string expiryStr = "N/A";
                            if (reader["expiry_date"] != DBNull.Value && reader["expiry_date"] != null)
                            {
                                expiryStr = Convert.ToDateTime(reader["expiry_date"]).ToString("yyyy-MM-dd");
                            }

                            decimal price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0.00m;

                            dgvStoreItems.Rows.Add(
                                
                                reader["id"].ToString(),
                                reader["name"].ToString(),
                                reader["description"]?.ToString() ?? "",
                                price.ToString("F2"),
                                reader["stock"].ToString(),
                                reader["category"].ToString(),
                                expiryStr,
                                reader["min_stock_level"].ToString()
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to populate store items inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadStoreItemsGrid();
        }

        private void dgvStoreItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Skip clicking headers
            if (e.RowIndex < 0) return;

            string colName = dgvStoreItems.Columns[e.ColumnIndex].Name;
            string itemId = dgvStoreItems.Rows[e.RowIndex].Cells[1].Value.ToString(); // Index 1 = Item ID Column
            string itemName = dgvStoreItems.Rows[e.RowIndex].Cells[2].Value.ToString(); // Index 2 = Name Column

            if (colName == "Edit")
            {
                DataGridViewRow row = dgvStoreItems.Rows[e.RowIndex];

                // Extract the data variables sequentially matching your grid layout mapping
                string id = row.Cells[1].Value.ToString();          // Index 1 = ID
                string name = row.Cells[2].Value.ToString();        // Index 2 = Name
                string description = row.Cells[3].Value?.ToString() ?? "";
                string price = row.Cells[4].Value.ToString();       // Index 4 = Price
                string stock = row.Cells[5].Value.ToString();       // Index 5 = Stock
                string category = row.Cells[6].Value.ToString();    // Index 6 = Category
                string expiryDate = row.Cells[7].Value?.ToString() ?? "";
                string minStock = row.Cells[8].Value.ToString();    // Index 8 = Min Stock Level

                // Initialize the specialized input form passing ALL parameters through Constructor 2
                formAddStoreItem editForm = new formAddStoreItem(id, name, description, price, stock, category, expiryDate, minStock);

                // Hide the primary standard save executor button, displaying update option layout instead
                editForm.btnSave.Visible = false;
                editForm.btnUpdate.Visible = true;

                editForm.ShowDialog();

                // Refresh the local inventory dashboard grid instantly once the dialog window wraps up
                LoadStoreItemsGrid();
            }
            else if (colName == "Delete")
            {
                DialogResult dialogResult = MessageBox.Show(
                    $"Are you sure you want to permanently delete '{itemName}' ({itemId}) from the inventory registry?",
                    "Confirm Delete Action",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM store_items WHERE id = @id";

                        using (SqlConnection con = new SqlConnection(dbcon.connection()))
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@id", itemId);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Inventory item removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStoreItemsGrid(); // Refresh grid layout context
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database Cascading Error: Could not drop record. " + ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            formAddStoreItem FormAddStoreItem = new formAddStoreItem();
            FormAddStoreItem.ShowDialog();
            LoadStoreItemsGrid();
        }

        private void chkExpiredItems_CheckedChanged(object sender, EventArgs e)
        {
            LoadStoreItemsGrid();
        }
    }
}