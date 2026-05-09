using Gym_Management_System.Components;
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

namespace Gym_Management_System
{
    public partial class formStore : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        public formStore()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            LoadStoreItems();
        }

        //search function
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (storeItem item in flpStore.Controls.OfType<storeItem>())
            {
                if (item.ItemName.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    item.Visible = true;
                }
                else
                {
                    item.Visible = false;
                }
            }
        }

        private void LoadStoreItems()
        {
            flpStore.Controls.Clear();

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                
                string query = "SELECT name, price, image_path, category FROM store_items";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    storeItem item = new storeItem();
                                        
                    item.ItemName = reader["name"].ToString();
                    
                    double price = 0;
                    if (reader["price"] != DBNull.Value)
                    {
                        price = Convert.ToDouble(reader["price"]);
                    }
                    item.ItemPrice = price.ToString("N2");
                    item.ItemPrice = string.Format("{0:N2}", reader["price"]);
                   
                    string categoryName = reader["category"].ToString();
                    item.Category = (Categories)Enum.Parse(typeof(Categories), categoryName);
                    
                    string imgPath = reader["image_path"].ToString();

                    if (!string.IsNullOrEmpty(imgPath) && File.Exists(imgPath))
                    {
                        item.ItemImage = Image.FromFile(imgPath);
                    }
                    else
                    {
                       item.ItemImage = Properties.Resources.no_image;
                    }

                    flpStore.Controls.Add(item);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void flpStore_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }


}
