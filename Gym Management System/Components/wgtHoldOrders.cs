using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gym_Management_System.Components
{
    public partial class wgtHoldOrders : UserControl
    {
        public wgtHoldOrders()
        {
            InitializeComponent();
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
        // public properties to set from parent
        public string HoldId { get; set; }

        public string Title
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        public string Total
        {
            get => lblTotal.Text;
            set => lblTotal.Text = value;
        }

        // events
        public event EventHandler OnSelect;
        public event EventHandler OnDeleteRequested;

        private void roundedPannel1_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, EventArgs.Empty);
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            OnSelect?.Invoke(this, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OnDeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
