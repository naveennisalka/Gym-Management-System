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
            this.itemImg = new System.Windows.Forms.PictureBox();
            this.itemName = new System.Windows.Forms.Label();
            this.itemPrice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.itemImg)).BeginInit();
            this.SuspendLayout();
            // 
            // itemImg
            // 
            this.itemImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.itemImg.Image = ((System.Drawing.Image)(resources.GetObject("itemImg.Image")));
            this.itemImg.Location = new System.Drawing.Point(12, 13);
            this.itemImg.Name = "itemImg";
            this.itemImg.Size = new System.Drawing.Size(140, 119);
            this.itemImg.TabIndex = 0;
            this.itemImg.TabStop = false;
            // 
            // itemName
            // 
            this.itemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemName.Location = new System.Drawing.Point(8, 144);
            this.itemName.Name = "itemName";
            this.itemName.Size = new System.Drawing.Size(144, 45);
            this.itemName.TabIndex = 1;
            this.itemName.Text = "Item Name";
            this.itemName.Click += new System.EventHandler(this.label1_Click);
            // 
            // itemPrice
            // 
            this.itemPrice.AutoSize = true;
            this.itemPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemPrice.ForeColor = System.Drawing.Color.DarkGreen;
            this.itemPrice.Location = new System.Drawing.Point(8, 189);
            this.itemPrice.Name = "itemPrice";
            this.itemPrice.Size = new System.Drawing.Size(82, 24);
            this.itemPrice.TabIndex = 2;
            this.itemPrice.Text = "1000.00";
            // 
            // storeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.itemPrice);
            this.Controls.Add(this.itemName);
            this.Controls.Add(this.itemImg);
            this.Name = "storeItem";
            this.Size = new System.Drawing.Size(166, 224);
            this.Load += new System.EventHandler(this.storeItem_Load);
            this.Click += new System.EventHandler(this.storeItem_Click);
            ((System.ComponentModel.ISupportInitialize)(this.itemImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox itemImg;
        public System.Windows.Forms.Label itemName;
        public System.Windows.Forms.Label itemPrice;
    }
}
