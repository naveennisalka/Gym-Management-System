using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System.Forms
{
    public partial class formAddStoreItem : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;

        bool isChecked = false;
        private string originalImagePath = "";

        public formAddStoreItem()
        {
            
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());

            
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            lblID.Visible = false;
            LoadCategories();

        }

        
        public formAddStoreItem(string id, string name, string desc, string price, string stock, string category, string expiry, string minStock)
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            LoadCategories();

           
            
            lblID.Text = id;
            txtItemName.Text = name;
            txtDescription.Text = desc;
            txtPrice.Text = price;
            txtStock.Text = stock;
            cbCategory.Text = category;
            txtMinStock.Text = minStock;

       
            if (!string.IsNullOrEmpty(expiry) && expiry != "N/A" && DateTime.TryParse(expiry, out DateTime dt))
            {
                dtpExpiryDate.Value = dt;
            }

       
            try
            {
                string imgQuery = "SELECT image_path FROM store_items WHERE id = @id";
                using (SqlCommand imgCmd = new SqlCommand(imgQuery, con))
                {
                    imgCmd.Parameters.AddWithValue("@id", id);
                    if (con.State == ConnectionState.Closed) con.Open();
                    var result = imgCmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        originalImagePath = result.ToString();
                        txtImagePath.Text = originalImagePath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading item image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                con.Close();
            }

          
            if (!string.IsNullOrEmpty(originalImagePath) && File.Exists(originalImagePath))
            {
                try
                {
                    using (var stream = new FileStream(originalImagePath, FileMode.Open, FileAccess.Read))
                    {
                        pbItemImage.Image = Image.FromStream(stream);
                    }
                }
                catch
                {
                    pbItemImage.Image = Properties.Resources.no_image;
                }
            }
            else
            {
                pbItemImage.Image = Properties.Resources.no_image;
            }

          
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        private void LoadCategories()
        {
            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(new string[] { "GymItem", "Memberships" });
        }


        public void clearForm()
        {
            txtItemName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtMinStock.Clear();
            txtImagePath.Clear();
            cbCategory.SelectedIndex = -1;
            dtpExpiryDate.Value = DateTime.Now;
            pbItemImage.Image = Properties.Resources.no_image;
        }

        public void checkForm()
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text) ||
                cbCategory.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                string.IsNullOrWhiteSpace(txtMinStock.Text))
            {
                MessageBox.Show("Please complete all required item specifications fields before executing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isChecked = false;
            }
            else
            {
                isChecked = true;
            }
        }


        private void pbClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            checkForm();
            if (isChecked)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to update this item's structural specifications?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string portableImagePath = SaveImageToProjectFolder(txtImagePath.Text.Trim(), lblID.Text);

                        string query = @"UPDATE store_items 
                                         SET name = @name, description = @desc, price = @price, stock = @stock, 
                                             category = @category, expiry_date = @expiry, image_path = @img, min_stock_level = @min 
                                         WHERE id = @id";

                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", lblID.Text);
                        cmd.Parameters.AddWithValue("@name", txtItemName.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text.Trim()));
                        cmd.Parameters.AddWithValue("@category", cbCategory.Text);
                        cmd.Parameters.AddWithValue("@expiry", cbCategory.Text == "Memberships" ? (object)DBNull.Value : dtpExpiryDate.Value.Date);
                        cmd.Parameters.AddWithValue("@img", portableImagePath);
                        cmd.Parameters.AddWithValue("@min", Convert.ToInt32(txtMinStock.Text.Trim()));

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Inventory record successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    MessageBox.Show("Update Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            checkForm();
            if (isChecked)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to save this product item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Generate dynamic tracking ID: ITM + timestamp
                        string newId = "ITM" + DateTime.Now.ToString("yyMMddHHmmss");
                        string portableImagePath = SaveImageToProjectFolder(txtImagePath.Text.Trim(), newId);

                        string query = @"INSERT INTO store_items (id, name, description, price, stock, category, expiry_date, image_path, min_stock_level) 
                                         VALUES (@id, @name, @desc, @price, @stock, @category, @expiry, @img, @min)";

                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", newId);
                        cmd.Parameters.AddWithValue("@name", txtItemName.Text.Trim());
                        cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
                        cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtPrice.Text.Trim()));
                        cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text.Trim()));
                        cmd.Parameters.AddWithValue("@category", cbCategory.SelectedItem?.ToString() ?? "GymItem");
                        cmd.Parameters.AddWithValue("@expiry", cbCategory.Text == "Memberships" ? (object)DBNull.Value : dtpExpiryDate.Value.Date);
                        cmd.Parameters.AddWithValue("@img", portableImagePath);
                        cmd.Parameters.AddWithValue("@min", Convert.ToInt32(txtMinStock.Text.Trim()));

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("Store item successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    MessageBox.Show("Save Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnChooseFile_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // Filter out non-graphic file format extensions
                ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                ofd.Title = "Select Product Image Asset";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    
                    txtImagePath.Text = ofd.FileName;
                    pbItemImage.Image = Image.FromFile(ofd.FileName);                    
                    pbItemImage.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private string SaveImageToProjectFolder(string sourceFilePath, string itemId)
        {
            try
            {
                // 1. If no image was chosen, or the file doesn't exist, return empty string
                if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
                    return "";

                // 2. Define target directory path ('Item_Images' folder inside your app installation path)
                string targetDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Item_Images");

                // Create the directory if it doesn't exist yet
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                // 3. Keep existing path if it's already inside our target folder (Prevents redundant copies on Edit)
                if (sourceFilePath.StartsWith(targetDirectory, StringComparison.OrdinalIgnoreCase))
                {
                    return sourceFilePath;
                }

                // 4. Generate a clean file extension and unique file name based on the Item ID
                string extension = Path.GetExtension(sourceFilePath); // e.g., ".png"
                string newFileName = $"{itemId}{extension}";          // e.g., "ITM260621.png"
                string destinationPath = Path.Combine(targetDirectory, newFileName);

                // 5. Copy the physical file to the local directory (Overwrite if it already exists)
                File.Copy(sourceFilePath, destinationPath, true);

                return destinationPath; // This returns the local portable file path to save in the DB
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to locally archive image asset: " + ex.Message, "IO Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return sourceFilePath; // Fallback to original if file operation fails
            }
        }
    }
}
