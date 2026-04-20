using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gym_Management_System;

namespace Gym_Management_System
{

    public partial class storeItem : UserControl
    {
        public event EventHandler OnItemSelect = null;
        private Categories _category;
        public storeItem()
        {
            InitializeComponent();
        }

        private void storeItem_Load(object sender, EventArgs e)
        {
            OnItemSelect?.Invoke(this, EventArgs.Empty);
        }

        private void storeItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        


        //getters and setters for the item name, price and image
        public string ItemName
        {
            get { return itemName.Text; }
            set { itemName.Text = value; }
        }
        public string ItemPrice
        {
            get { return itemPrice.Text; }
            set { itemPrice.Text = value; }
        }
        public Image ItemImage
        {
            get { return itemImg.Image; }
            set { itemImg.Image = value; }
            
        }
        
        public Categories Category
        {
            get => _category;
            set => _category = value;
        }
    }
}
