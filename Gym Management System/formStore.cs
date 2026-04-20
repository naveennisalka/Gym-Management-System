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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                    item.ItemPrice = string.Format("{0:N2}", reader["price"]); 

                    
                    int catValue = Convert.ToInt32(reader["category"]);
                    item.Category = (Categories)catValue;

                    
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
                MessageBox.Show("දෝෂයකි: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }


}
