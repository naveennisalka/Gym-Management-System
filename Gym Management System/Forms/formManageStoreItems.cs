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
                dgvStoreItems.Rows.Clear(); 

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
     
            if (e.RowIndex < 0) return;

            string colName = dgvStoreItems.Columns[e.ColumnIndex].Name;
            string itemId = dgvStoreItems.Rows[e.RowIndex].Cells[1].Value.ToString(); 
            string itemName = dgvStoreItems.Rows[e.RowIndex].Cells[2].Value.ToString(); 

            if (colName == "Edit")
            {
                DataGridViewRow row = dgvStoreItems.Rows[e.RowIndex];


                //string id = row.Cells[1].Value.ToString();         
                //string name = row.Cells[2].Value.ToString();       
                //string description = row.Cells[3].Value?.ToString() ?? "";
                //string price = row.Cells[4].Value.ToString();       
                //string stock = row.Cells[5].Value.ToString();       
                //string category = row.Cells[6].Value.ToString();   
                //string expiryDate = row.Cells[7].Value?.ToString() ?? "";
                //string minStock = row.Cells[8].Value.ToString();    

                string id = row.Cells[0].Value?.ToString() ?? "";
                string name = row.Cells[1].Value?.ToString() ?? "";
                string description = row.Cells[2].Value?.ToString() ?? "";
                string price = row.Cells[3].Value?.ToString() ?? "0.00";
                string stock = row.Cells[4].Value?.ToString() ?? "0";
                string category = row.Cells[5].Value?.ToString() ?? "GymItem";
                string expiryDate = row.Cells[6].Value?.ToString() ?? "N/A";
                string minStock = row.Cells[7].Value?.ToString() ?? "0";

                formAddStoreItem editForm = new formAddStoreItem(id, name, description, price, stock, category, expiryDate, minStock);

   
                editForm.btnSave.Visible = false;
                editForm.btnUpdate.Visible = true;
                editForm.label1.Text = "Edit Store Item";
                editForm.ShowDialog();

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
                        LoadStoreItemsGrid(); 
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