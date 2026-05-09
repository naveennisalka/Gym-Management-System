using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System
{
    public partial class orderItem : UserControl
    {
        public event EventHandler OnQuantityChanged = null;
        public event EventHandler OnRemoved = null;

        private string _name;
        private decimal _price;
        private int _qty = 1;

        public orderItem()
        {
            InitializeComponent();

            // wire up buttons
            button1.Click += Button1_Click; // decrement
            button2.Click += Button2_Click; // increment
            button3.Click += Button3_Click; // delete
            UpdateDisplay();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OnRemoved?.Invoke(this, EventArgs.Empty);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Qty += 1;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Qty > 1)
                Qty -= 1;
            else
                OnRemoved?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateDisplay()
        {
            lblName.Text = _name;
            lblPrice.Text = _price.ToString("N2");
            lblQty.Text = _qty.ToString();
            lblTotal.Text = (_price * _qty).ToString("N2");
        }

        public string ItemName
        {
            get => _name;
            set { _name = value; UpdateDisplay(); }
        }

        public decimal ItemPrice
        {
            get => _price;
            set { _price = value; UpdateDisplay(); }
        }

        public int Qty
        {
            get => _qty;
            set { _qty = value; UpdateDisplay(); OnQuantityChanged?.Invoke(this, EventArgs.Empty); }
        }

        // associated id from DB (store item id or plan id)
        public string ItemId { get; set; }
    }
}
