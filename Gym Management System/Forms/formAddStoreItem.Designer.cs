namespace Gym_Management_System.Forms
{
    partial class formAddStoreItem
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formAddStoreItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.pbClose = new System.Windows.Forms.Button();
            this.txtMinStock = new System.Windows.Forms.TextBox();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.pbItemImage = new System.Windows.Forms.PictureBox();
            this.lblID = new System.Windows.Forms.Label();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbItemImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 112);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 37);
            this.label1.TabIndex = 14;
            this.label1.Text = "Add New Item to Store";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnUpdate);
            this.flowLayoutPanel1.Controls.Add(this.pbClose);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 835);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(469, 42);
            this.flowLayoutPanel1.TabIndex = 28;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.PaleGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 37);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.PaleGreen;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(138, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(139, 36);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.FlatAppearance.BorderColor = System.Drawing.Color.ForestGreen;
            this.pbClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pbClose.ForeColor = System.Drawing.Color.DarkGreen;
            this.pbClose.Location = new System.Drawing.Point(283, 3);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(139, 36);
            this.pbClose.TabIndex = 24;
            this.pbClose.Text = "Cancel";
            this.pbClose.UseVisualStyleBackColor = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click_1);
            // 
            // txtMinStock
            // 
            this.txtMinStock.Location = new System.Drawing.Point(422, 339);
            this.txtMinStock.Name = "txtMinStock";
            this.txtMinStock.Size = new System.Drawing.Size(103, 27);
            this.txtMinStock.TabIndex = 84;
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Location = new System.Drawing.Point(202, 403);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(323, 27);
            this.dtpExpiryDate.TabIndex = 82;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(116, 339);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(106, 27);
            this.txtStock.TabIndex = 80;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(116, 286);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(409, 27);
            this.txtPrice.TabIndex = 79;
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(202, 216);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(323, 31);
            this.cbCategory.TabIndex = 77;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(202, 167);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(323, 27);
            this.txtItemName.TabIndex = 76;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 28);
            this.label3.TabIndex = 74;
            this.label3.Text = "Name :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(32, 459);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(109, 28);
            this.label25.TabIndex = 73;
            this.label25.Text = "Description :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(263, 339);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(138, 28);
            this.label21.TabIndex = 72;
            this.label21.Text = "Min Stock Level :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(32, 402);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(107, 28);
            this.label23.TabIndex = 71;
            this.label23.Text = "Expiry Date :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(32, 338);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 28);
            this.label15.TabIndex = 69;
            this.label15.Text = "Stock :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(32, 286);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 28);
            this.label13.TabIndex = 68;
            this.label13.Text = "Price :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(32, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 28);
            this.label11.TabIndex = 67;
            this.label11.Text = "Category :";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(37, 501);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(488, 78);
            this.txtDescription.TabIndex = 85;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 610);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 28);
            this.label4.TabIndex = 86;
            this.label4.Text = "Image :";
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new System.Drawing.Point(116, 611);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(285, 27);
            this.txtImagePath.TabIndex = 87;
            // 
            // pbItemImage
            // 
            this.pbItemImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbItemImage.Location = new System.Drawing.Point(116, 653);
            this.pbItemImage.Name = "pbItemImage";
            this.pbItemImage.Size = new System.Drawing.Size(173, 146);
            this.pbItemImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbItemImage.TabIndex = 88;
            this.pbItemImage.TabStop = false;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblID.Location = new System.Drawing.Point(32, 126);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(27, 28);
            this.lblID.TabIndex = 89;
            this.lblID.Text = "ID";
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(407, 611);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(118, 28);
            this.btnChooseFile.TabIndex = 90;
            this.btnChooseFile.Text = "Choose Image";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click_1);
            // 
            // formAddStoreItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 889);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.pbItemImage);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtMinStock);
            this.Controls.Add(this.dtpExpiryDate);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAddStoreItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "formAddStoreItem";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbItemImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button pbClose;
        private System.Windows.Forms.TextBox txtMinStock;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.PictureBox pbItemImage;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnChooseFile;
        public System.Windows.Forms.Label label1;
    }
}