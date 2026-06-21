using Gym_Management_System.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Globalization;
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
        // if a held order is loaded into the current order, track its id
        private string loadedHeldOrderId = null;
        public formStore()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
            // remove any design-time placeholder order items
            flpCurrentOrder.Controls.Clear();

            LoadStoreItems();

            // wire clear button
            button8.Click += (s, e) => {
                flpCurrentOrder.Controls.Clear();
                UpdateTotal();
            };

            // show store items by default
            button3.Click += (s, e) => { ShowStoreItems(); };
            // show memberships
            button4.Click += (s, e) => { LoadMemberships(); };

            // show held orders (left panel button)
            button7.Click += (s, e) => { LoadHeldOrders(); };

            // hold current order (top right)
            button9.Click += (s, e) => { HoldCurrentOrder(); };

            // place order
            button1.Click += Button1_Click;
        }

        private void CancelHeldOrder()
        {
            if (string.IsNullOrEmpty(loadedHeldOrderId)) return;
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var cmdDelItems = new SqlCommand("DELETE FROM order_items WHERE order_id = @o", con);
                cmdDelItems.Parameters.AddWithValue("@o", loadedHeldOrderId);
                cmdDelItems.ExecuteNonQuery();
                var cmdDel = new SqlCommand("DELETE FROM orders WHERE id = @o", con);
                cmdDel.Parameters.AddWithValue("@o", loadedHeldOrderId);
                cmdDel.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }

            loadedHeldOrderId = null;
        }

        private string GenerateOrderId()
        {
            // readable: ORD-YYYYMMDD-XXXX where XXXX is a short random number
            var dt = DateTime.Now;
            var rnd = new Random();
            return $"ORD-{dt:yyyyMMdd}-{rnd.Next(1000, 9999)}";
        }

        private void ShowStoreItems()
        {
            // reload store items
            LoadStoreItems();
        }

        private void LoadMemberships()
        {
            // For simplicity, reuse flpStore to show membership plans
            flpStore.Controls.Clear();

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                string q = "SELECT id, name, price, description FROM gym_plans";
                cmd = new SqlCommand(q, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    storeItem item = new storeItem();
                    var planId = reader["id"]?.ToString();
                    var planName = reader["name"]?.ToString();
                    var planPrice = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0m;
                    item.ItemId = planId;
                    item.ItemName = planName;
                    item.ItemPrice = planPrice.ToString("N2");
                    item.Category = Categories.Memberships;
                    item.OnItemSelect += StoreItem_OnItemSelect;
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

        //private void Button1_Click(object sender, EventArgs e)
        //{
        //    // Place order: prompt for user id or name
        //    string userInput;
        //    using (var d = new Forms.InputDialog("Place Order", "Enter Member ID or Name:"))
        //    {
        //        if (d.ShowDialog() != DialogResult.OK) return;
        //        if (string.IsNullOrWhiteSpace(d.Value)) return;
        //        userInput = d.Value;
        //    }

        //    // try to find user by id or name
        //    string userId = null;
        //    try
        //    {
        //        if (con.State == ConnectionState.Closed) con.Open();
        //        cmd = new SqlCommand("SELECT id FROM users WHERE id = @q OR name = @q", con);
        //        cmd.Parameters.AddWithValue("@q", userInput);
        //        var r = cmd.ExecuteScalar();
        //        if (r != null) userId = r.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally { con.Close(); }

        //    if (userId == null)
        //    {
        //        MessageBox.Show("User not found.");
        //        return;
        //    }

        //    // determine order id: if we loaded a held order, update it instead of creating a new one
        //    string orderId = loadedHeldOrderId ?? GenerateOrderId();
        //    decimal total = 0m;
        //    foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>()) total += oi.ItemPrice * oi.Qty;

        //    try
        //    {
        //        if (con.State == ConnectionState.Closed) con.Open();
        //        if (loadedHeldOrderId == null)
        //        {
        //            // create new order
        //            cmd = new SqlCommand("INSERT INTO orders (id, user_id, staff_id, total_amount, order_date, status) VALUES (@id, @user, @staff, @total, GETDATE(), 1)", con);
        //            cmd.Parameters.AddWithValue("@id", orderId);
        //            cmd.Parameters.AddWithValue("@user", userId);
        //            cmd.Parameters.AddWithValue("@staff", DBNull.Value);
        //            cmd.Parameters.AddWithValue("@total", total);
        //            cmd.ExecuteNonQuery();
        //        }
        //        else
        //        {
        //            // update existing held order to completed
        //            var cmdUp = new SqlCommand("UPDATE orders SET user_id = @user, staff_id = @staff, total_amount = @total, order_date = GETDATE(), status = 1 WHERE id = @id", con);
        //            cmdUp.Parameters.AddWithValue("@id", orderId);
        //            cmdUp.Parameters.AddWithValue("@user", userId);
        //            cmdUp.Parameters.AddWithValue("@staff", DBNull.Value);
        //            cmdUp.Parameters.AddWithValue("@total", total);
        //            cmdUp.ExecuteNonQuery();

        //            // remove previous order items for this held order; we'll insert current items below
        //            var del = new SqlCommand("DELETE FROM order_items WHERE order_id = @o", con);
        //            del.Parameters.AddWithValue("@o", orderId);
        //            del.ExecuteNonQuery();
        //        }

        //        // insert order items
        //        foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>())
        //        {
        //            // ensure item_id refers to an existing store_items id; if not, insert NULL to avoid FK violations
        //            object itemIdParam = DBNull.Value;
        //            if (!string.IsNullOrEmpty(oi.ItemId))
        //            {
        //                var chk = new SqlCommand("SELECT COUNT(1) FROM store_items WHERE id = @id", con);
        //                chk.Parameters.AddWithValue("@id", oi.ItemId);
        //                var exists = Convert.ToInt32(chk.ExecuteScalar()) > 0;
        //                if (exists) itemIdParam = oi.ItemId;
        //            }

        //            var cmd2 = new SqlCommand("INSERT INTO order_items (order_id, item_id, quantity, unit_price) VALUES (@o, @it, @q, @p)", con);
        //            cmd2.Parameters.AddWithValue("@o", orderId);
        //            cmd2.Parameters.AddWithValue("@it", itemIdParam);
        //            cmd2.Parameters.AddWithValue("@q", oi.Qty);
        //            cmd2.Parameters.AddWithValue("@p", oi.ItemPrice);
        //            cmd2.ExecuteNonQuery();

        //            // if it was a membership plan, update membership for user
        //            if (!string.IsNullOrEmpty(oi.ItemId))
        //            {
        //                // check if ItemId exists in gym_plans
        //                var cmd3 = new SqlCommand("SELECT duration_months FROM gym_plans WHERE id = @pid", con);
        //                cmd3.Parameters.AddWithValue("@pid", oi.ItemId);
        //                var dur = cmd3.ExecuteScalar();
        //                if (dur != null && dur != DBNull.Value)
        //                {
        //                    int months = Convert.ToInt32(dur);
        //                    // extend or create membership
        //                    var cmd4 = new SqlCommand("SELECT id, end_date FROM memberships WHERE user_id = @u", con);
        //                    cmd4.Parameters.AddWithValue("@u", userId);
        //                    var rdr = cmd4.ExecuteReader();
        //                    if (rdr.Read())
        //                    {
        //                        var mid = rdr["id"]?.ToString();
        //                        DateTime curEnd = rdr["end_date"] != DBNull.Value ? Convert.ToDateTime(rdr["end_date"]) : DateTime.Now;
        //                        rdr.Close();
        //                        var cmd5 = new SqlCommand("UPDATE memberships SET end_date = @end, payment_status = 1 WHERE id = @mid", con);
        //                        cmd5.Parameters.AddWithValue("@end", curEnd.AddMonths(months));
        //                        cmd5.Parameters.AddWithValue("@mid", mid);
        //                        cmd5.ExecuteNonQuery();
        //                    }
        //                    else
        //                    {
        //                        rdr.Close();
        //                        var cmd6 = new SqlCommand("INSERT INTO memberships (id, user_id, plan_id, start_date, end_date, payment_status) VALUES (@id, @u, @p, @s, @e, 1)", con);
        //                        cmd6.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
        //                        cmd6.Parameters.AddWithValue("@u", userId);
        //                        cmd6.Parameters.AddWithValue("@p", oi.ItemId);
        //                        cmd6.Parameters.AddWithValue("@s", DateTime.Now.Date);
        //                        cmd6.Parameters.AddWithValue("@e", DateTime.Now.Date.AddMonths(months));
        //                        cmd6.ExecuteNonQuery();
        //                    }
        //                }
        //            }
        //        }

        //        MessageBox.Show("Order placed successfully.");
        //        // if this order originated from a held order, remove the held record
        //        if (!string.IsNullOrEmpty(loadedHeldOrderId))
        //        {
        //            try
        //            {
        //                var cmdDelItems = new SqlCommand("DELETE FROM order_items WHERE order_id = @o", con);
        //                cmdDelItems.Parameters.AddWithValue("@o", loadedHeldOrderId);
        //                cmdDelItems.ExecuteNonQuery();
        //                var cmdDel = new SqlCommand("DELETE FROM orders WHERE id = @o", con);
        //                cmdDel.Parameters.AddWithValue("@o", loadedHeldOrderId);
        //                cmdDel.ExecuteNonQuery();
        //            }
        //            catch { }
        //            loadedHeldOrderId = null;
        //        }

        //        flpCurrentOrder.Controls.Clear();
        //        UpdateTotal();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally { con.Close(); }
        //}

        //search function

        private void Button1_Click(object sender, EventArgs e)
        {
            // Place order: prompt for user id or name
            string userInput;
            using (var d = new Forms.InputDialog("Place Order", "Enter Member ID or Name:"))
            {
                if (d.ShowDialog() != DialogResult.OK) return;
                if (string.IsNullOrWhiteSpace(d.Value)) return;
                userInput = d.Value;
            }

            // try to find user by id or name
            string userId = null;
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                cmd = new SqlCommand("SELECT id FROM users WHERE id = @q OR name = @q", con);
                cmd.Parameters.AddWithValue("@q", userInput);
                var r = cmd.ExecuteScalar();
                if (r != null) userId = r.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }

            if (userId == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            string orderId = loadedHeldOrderId ?? GenerateOrderId();
            decimal total = 0m;
            foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>()) total += oi.ItemPrice * oi.Qty;

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                if (loadedHeldOrderId == null)
                {
                    cmd = new SqlCommand("INSERT INTO orders (id, user_id, staff_id, total_amount, order_date, status) VALUES (@id, @user, @staff, @total, GETDATE(), 1)", con);
                    cmd.Parameters.AddWithValue("@id", orderId);
                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@staff", DBNull.Value);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    var cmdUp = new SqlCommand("UPDATE orders SET user_id = @user, staff_id = @staff, total_amount = @total, order_date = GETDATE(), status = 1 WHERE id = @id", con);
                    cmdUp.Parameters.AddWithValue("@id", orderId);
                    cmdUp.Parameters.AddWithValue("@user", userId);
                    cmdUp.Parameters.AddWithValue("@staff", DBNull.Value);
                    cmdUp.Parameters.AddWithValue("@total", total);
                    cmdUp.ExecuteNonQuery();

                    var del = new SqlCommand("DELETE FROM order_items WHERE order_id = @o", con);
                    del.Parameters.AddWithValue("@o", orderId);
                    del.ExecuteNonQuery();
                }

                // insert order items
                foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>())
                {
                    object itemIdParam = DBNull.Value;
                    if (!string.IsNullOrEmpty(oi.ItemId))
                    {
                        var chk = new SqlCommand("SELECT COUNT(1) FROM store_items WHERE id = @id", con);
                        chk.Parameters.AddWithValue("@id", oi.ItemId);
                        var exists = Convert.ToInt32(chk.ExecuteScalar()) > 0;
                        if (exists) itemIdParam = oi.ItemId;
                    }

                    var cmd2 = new SqlCommand("INSERT INTO order_items (order_id, item_id, quantity, unit_price) VALUES (@o, @it, @q, @p)", con);
                    cmd2.Parameters.AddWithValue("@o", orderId);
                    cmd2.Parameters.AddWithValue("@it", itemIdParam);
                    cmd2.Parameters.AddWithValue("@q", oi.Qty);
                    cmd2.Parameters.AddWithValue("@p", oi.ItemPrice);
                    cmd2.ExecuteNonQuery();

                    // Handle Membership Plans Extension/Creation
                    if (!string.IsNullOrEmpty(oi.ItemId))
                    {
                        var cmd3 = new SqlCommand("SELECT duration_months FROM gym_plans WHERE id = @pid", con);
                        cmd3.Parameters.AddWithValue("@pid", oi.ItemId);
                        var dur = cmd3.ExecuteScalar();

                        if (dur != null && dur != DBNull.Value)
                        {
                            int months = Convert.ToInt32(dur);

                            var cmd4 = new SqlCommand("SELECT id, end_date FROM memberships WHERE user_id = @u", con);
                            cmd4.Parameters.AddWithValue("@u", userId);

                            var rdr = cmd4.ExecuteReader();
                            if (rdr.Read())
                            {
                                var mid = rdr["id"]?.ToString();
                                DateTime curEnd = rdr["end_date"] != DBNull.Value ? Convert.ToDateTime(rdr["end_date"]) : DateTime.Now;
                                rdr.Close();

                                // FIXED LOGIC: If membership has already expired, renewal extends starting from TODAY instead of the past date
                                DateTime baseDate = (curEnd < DateTime.Now) ? DateTime.Now : curEnd;
                                DateTime newEndDate = baseDate.AddMonths(months);

                                var cmd5 = new SqlCommand("UPDATE memberships SET plan_id = @pid, end_date = @end, payment_status = 1 WHERE id = @mid", con);
                                cmd5.Parameters.AddWithValue("@pid", oi.ItemId);
                                cmd5.Parameters.AddWithValue("@end", newEndDate);
                                cmd5.Parameters.AddWithValue("@mid", mid);
                                cmd5.ExecuteNonQuery();
                            }
                            else
                            {
                                rdr.Close();
                                var cmd6 = new SqlCommand("INSERT INTO memberships (id, user_id, plan_id, start_date, end_date, payment_status) VALUES (@id, @u, @p, @s, @e, 1)", con);
                                cmd6.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                                cmd6.Parameters.AddWithValue("@u", userId);
                                cmd6.Parameters.AddWithValue("@p", oi.ItemId);
                                cmd6.Parameters.AddWithValue("@s", DateTime.Now.Date);
                                cmd6.Parameters.AddWithValue("@e", DateTime.Now.Date.AddMonths(months));
                                cmd6.ExecuteNonQuery();
                            }
                        }
                    }
                }

                MessageBox.Show("Order placed successfully.");

                if (!string.IsNullOrEmpty(loadedHeldOrderId))
                {
                    try
                    {
                        var cmdDelItems = new SqlCommand("DELETE FROM order_items WHERE order_id = @o", con);
                        cmdDelItems.Parameters.AddWithValue("@o", loadedHeldOrderId);
                        cmdDelItems.ExecuteNonQuery();
                        var cmdDel = new SqlCommand("DELETE FROM orders WHERE id = @o", con);
                        cmdDel.Parameters.AddWithValue("@o", loadedHeldOrderId);
                        cmdDel.ExecuteNonQuery();
                    }
                    catch { }
                    loadedHeldOrderId = null;
                }

                flpCurrentOrder.Controls.Clear();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }
        }
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
                
                string query = "SELECT id, name, price, image_path, category FROM store_items";
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    storeItem item = new storeItem();
                                        
                    item.ItemName = reader["name"].ToString();
                    item.ItemId = reader["id"]?.ToString();
                    item.Tag = item.ItemId;
                    
                    double price = 0;
                    if (reader["price"] != DBNull.Value)
                    {
                        price = Convert.ToDouble(reader["price"]);
                    }
                    item.ItemPrice = price.ToString("N2");
                   
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

                    // subscribe to selection event so clicking the storeItem adds it to the current order
                    item.OnItemSelect += StoreItem_OnItemSelect;

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
        private void StoreItem_OnItemSelect(object sender, EventArgs e)
        {
            var si = sender as storeItem;
            if (si == null) return;
            AddOrUpdateOrderItem(si);
        }

        private void AddOrUpdateOrderItem(storeItem si)
        {
            // check existing
            var existing = flpCurrentOrder.Controls.OfType<orderItem>().FirstOrDefault(x => x.ItemName == si.ItemName);
            if (existing != null)
            {
                existing.Qty += 1;
            }
            else
            {
                var oi = new orderItem();
                oi.ItemId = si.ItemId;
                oi.ItemName = si.ItemName;
                // parse price string
                decimal p = 0m;
                if (!string.IsNullOrEmpty(si.ItemPrice))
                {
                    decimal.TryParse(si.ItemPrice, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.CurrentCulture, out p);
                }
                oi.ItemPrice = p;

                oi.OnQuantityChanged += (s, e) => UpdateTotal();
                oi.OnRemoved += (s, e) => {
                    flpCurrentOrder.Controls.Remove(oi);
                    UpdateTotal();
                    oi.Dispose();
                };

                flpCurrentOrder.Controls.Add(oi);
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = 0m;
            foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>())
            {
                total += oi.ItemPrice * oi.Qty;
            }
            label4.Text = total.ToString("N2");
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void HoldCurrentOrder()
        {
            // save current order with status = 0 (hold)
            if (!flpCurrentOrder.Controls.OfType<orderItem>().Any())
            {
                MessageBox.Show("No items to hold.");
                return;
            }

            string orderId = GenerateOrderId();
            decimal total = 0m;
            foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>()) total += oi.ItemPrice * oi.Qty;

            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var cmdHold = new SqlCommand("INSERT INTO orders (id, user_id, staff_id, total_amount, status) VALUES (@id, @user, @staff, @total, 0)", con);
                cmdHold.Parameters.AddWithValue("@id", orderId);
                cmdHold.Parameters.AddWithValue("@user", DBNull.Value);
                cmdHold.Parameters.AddWithValue("@staff", DBNull.Value);
                cmdHold.Parameters.AddWithValue("@total", total);
                cmdHold.ExecuteNonQuery();

                foreach (var oi in flpCurrentOrder.Controls.OfType<orderItem>())
                {
                    object itemIdParam = DBNull.Value;
                    if (!string.IsNullOrEmpty(oi.ItemId))
                    {
                        var chk = new SqlCommand("SELECT COUNT(1) FROM store_items WHERE id = @id", con);
                        chk.Parameters.AddWithValue("@id", oi.ItemId);
                        var exists = Convert.ToInt32(chk.ExecuteScalar()) > 0;
                        if (exists) itemIdParam = oi.ItemId;
                    }

                    var cmd2 = new SqlCommand("INSERT INTO order_items (order_id, item_id, quantity, unit_price) VALUES (@o, @it, @q, @p)", con);
                    cmd2.Parameters.AddWithValue("@o", orderId);
                    cmd2.Parameters.AddWithValue("@it", itemIdParam);
                    cmd2.Parameters.AddWithValue("@q", oi.Qty);
                    cmd2.Parameters.AddWithValue("@p", oi.ItemPrice);
                    cmd2.ExecuteNonQuery();
                }

                MessageBox.Show("Order held.");
                flpCurrentOrder.Controls.Clear();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }
        }

        private void LoadHeldOrders()
        {
            flpStore.Controls.Clear();
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var cmdH = new SqlCommand("SELECT id, total_amount, order_date FROM orders WHERE status = 0 ORDER BY order_date DESC", con);
                var r = cmdH.ExecuteReader();
                while (r.Read())
                {
                    var id = r["id"].ToString();
                    var total = r["total_amount"] != DBNull.Value ? Convert.ToDecimal(r["total_amount"]) : 0m;
                    var od = r["order_date"] != DBNull.Value ? Convert.ToDateTime(r["order_date"]) : DateTime.Now;

                    var w = new wgtHoldOrders();
                    w.HoldId = id;
                    w.Title = $"{id} - {od:g}";
                    w.Total = total.ToString("N2");
                    w.OnSelect += (s2, e2) => {
                        // if current has items, ask what to do
                        if (flpCurrentOrder.Controls.OfType<orderItem>().Any())
                        {
                            var res = MessageBox.Show("Current order has items.\nYes = Hold current and load selected held order.\nNo = Discard current and load selected held order.\nCancel = do nothing.", "Current Order", MessageBoxButtons.YesNoCancel);
                            if (res == DialogResult.Cancel) return;
                            if (res == DialogResult.Yes)
                            {
                                HoldCurrentOrder();
                            }
                            else if (res == DialogResult.No)
                            {
                                flpCurrentOrder.Controls.Clear();
                                UpdateTotal();
                            }
                        }

                        // load held order into current (do not delete yet)
                        LoadHeldOrderIntoCurrent(id);
                        loadedHeldOrderId = id;
                        // refresh held list to reflect selection state
                        LoadHeldOrders();
                    };

                    w.OnDeleteRequested += (s2, e2) => {
                        // delete held order from DB
                        try
                        {
                            if (con.State == ConnectionState.Closed) con.Open();
                            var cmdDelItems = new SqlCommand("DELETE FROM order_items WHERE order_id = @o", con);
                            cmdDelItems.Parameters.AddWithValue("@o", id);
                            cmdDelItems.ExecuteNonQuery();
                            var cmdDel = new SqlCommand("DELETE FROM orders WHERE id = @o", con);
                            cmdDel.Parameters.AddWithValue("@o", id);
                            cmdDel.ExecuteNonQuery();
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show(ex2.ToString());
                        }
                        finally { con.Close(); }

                        // if this was the loaded held order, clear current
                        if (loadedHeldOrderId == id)
                        {
                            loadedHeldOrderId = null;
                            flpCurrentOrder.Controls.Clear();
                            UpdateTotal();
                        }

                        LoadHeldOrders();
                    };

                    w.Width = 485;
                    flpStore.Controls.Add(w);
                }
                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }
        }

        private void LoadHeldOrderIntoCurrent(string heldId)
        {
            flpCurrentOrder.Controls.Clear();
            try
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var cmd = new SqlCommand(@"SELECT oi.item_id, oi.quantity, oi.unit_price, si.name FROM order_items oi LEFT JOIN store_items si ON oi.item_id = si.id WHERE oi.order_id = @o", con);
                cmd.Parameters.AddWithValue("@o", heldId);
                var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    var itemId = r["item_id"] != DBNull.Value ? r["item_id"].ToString() : null;
                    var qty = r["quantity"] != DBNull.Value ? Convert.ToInt32(r["quantity"]) : 1;
                    var price = r["unit_price"] != DBNull.Value ? Convert.ToDecimal(r["unit_price"]) : 0m;
                    var name = r["name"] != DBNull.Value ? r["name"].ToString() : "Item";

                    var oi = new orderItem();
                    oi.ItemId = itemId;
                    oi.ItemName = name;
                    oi.ItemPrice = price;
                    oi.Qty = qty;
                    oi.OnQuantityChanged += (s, e) => UpdateTotal();
                    oi.OnRemoved += (s, e) => { flpCurrentOrder.Controls.Remove(oi); UpdateTotal(); oi.Dispose(); };
                    flpCurrentOrder.Controls.Add(oi);
                }
                r.Close();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { con.Close(); }
        }

        private void flpStore_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }


}
