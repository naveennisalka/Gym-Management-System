namespace Gym_Management_System
{
    partial class storeItem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(storeItem));
            this.roundedPannel1 = new Gym_Management_System.RoundedPannel();
            this.itemImg = new System.Windows.Forms.PictureBox();
            this.itemPrice = new System.Windows.Forms.Label();
            this.itemName = new System.Windows.Forms.Label();
            this.roundedPannel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemImg)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedPannel1
            // 
            this.roundedPannel1.BackColor = System.Drawing.Color.White;
            this.roundedPannel1.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPannel1.BorderRadius = 12;
            this.roundedPannel1.BorderThickness = 0F;
            this.roundedPannel1.Controls.Add(this.itemImg);
            this.roundedPannel1.Controls.Add(this.itemPrice);
            this.roundedPannel1.Controls.Add(this.itemName);
            this.roundedPannel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundedPannel1.Location = new System.Drawing.Point(3, 6);
            this.roundedPannel1.Name = "roundedPannel1";
            this.roundedPannel1.Size = new System.Drawing.Size(132, 179);
            this.roundedPannel1.TabIndex = 3;
            // 
            // itemImg
            // 
            this.itemImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.itemImg.Image = ((System.Drawing.Image)(resources.GetObject("itemImg.Image")));
            this.itemImg.Location = new System.Drawing.Point(16, 15);
            this.itemImg.Name = "itemImg";
            this.itemImg.Size = new System.Drawing.Size(109, 89);
            this.itemImg.TabIndex = 0;
            this.itemImg.TabStop = false;
            this.itemImg.Click += new System.EventHandler(this.itemImg_Click);
            // 
            // itemPrice
            // 
            this.itemPrice.AutoSize = true;
            this.itemPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemPrice.ForeColor = System.Drawing.Color.DarkGreen;
            this.itemPrice.Location = new System.Drawing.Point(13, 152);
            this.itemPrice.Name = "itemPrice";
            this.itemPrice.Size = new System.Drawing.Size(59, 16);
            this.itemPrice.TabIndex = 2;
            this.itemPrice.Text = "1000.00";
            // 
            // itemName
            // 
            this.itemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemName.Location = new System.Drawing.Point(12, 107);
            this.itemName.Name = "itemName";
            this.itemName.Size = new System.Drawing.Size(129, 45);
            this.itemName.TabIndex = 1;
            this.itemName.Text = "Item Name";
            this.itemName.Click += new System.EventHandler(this.label1_Click);
            // 
            // storeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.roundedPannel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "storeItem";
            this.Size = new System.Drawing.Size(142, 195);
            this.Load += new System.EventHandler(this.storeItem_Load);
            this.Click += new System.EventHandler(this.storeItem_Click);
            this.roundedPannel1.ResumeLayout(false);
            this.roundedPannel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox itemImg;
        public System.Windows.Forms.Label itemName;
        public System.Windows.Forms.Label itemPrice;
        private RoundedPannel roundedPannel1;
    }
}
