namespace Gym_Management_System.Forms
{
    partial class userRegistrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userRegistrationForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCdtBalance = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cbGender = new System.Windows.Forms.ComboBox();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtDob = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(36, 78);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(275, 31);
            this.txtName.TabIndex = 1;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(36, 160);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(275, 31);
            this.txtPhone.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Phone Number";
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(36, 242);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(275, 31);
            this.txtMail.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "eMail";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "Gender";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 385);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "Status";
            // 
            // txtCdtBalance
            // 
            this.txtCdtBalance.Location = new System.Drawing.Point(378, 326);
            this.txtCdtBalance.Name = "txtCdtBalance";
            this.txtCdtBalance.Size = new System.Drawing.Size(275, 31);
            this.txtCdtBalance.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(373, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 28);
            this.label6.TabIndex = 10;
            this.label6.Text = "StoreCreditBalance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(373, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 28);
            this.label7.TabIndex = 12;
            this.label7.Text = "DOB";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(378, 160);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(275, 31);
            this.txtAddress.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(373, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 28);
            this.label8.TabIndex = 14;
            this.label8.Text = "Address";
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive",
            "Freezed"});
            this.cbStatus.Location = new System.Drawing.Point(36, 416);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(275, 36);
            this.cbStatus.TabIndex = 16;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // cbGender
            // 
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.cbGender.Location = new System.Drawing.Point(36, 326);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(275, 36);
            this.cbGender.TabIndex = 17;
            this.cbGender.SelectedIndexChanged += new System.EventHandler(this.cbGender_SelectedIndexChanged);
            // 
            // cbRole
            // 
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Items.AddRange(new object[] {
            "Member",
            "Admin",
            "Cashier",
            "Coache"});
            this.cbRole.Location = new System.Drawing.Point(378, 237);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(275, 36);
            this.cbRole.TabIndex = 19;
            this.cbRole.SelectedIndexChanged += new System.EventHandler(this.cbRole_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(373, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 28);
            this.label9.TabIndex = 18;
            this.label9.Text = "Role";
            // 
            // dtDob
            // 
            this.dtDob.Location = new System.Drawing.Point(378, 78);
            this.dtDob.Name = "dtDob";
            this.dtDob.Size = new System.Drawing.Size(275, 31);
            this.dtDob.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(917, 515);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 49);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(772, 515);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(139, 49);
            this.btnUpdate.TabIndex = 22;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(627, 515);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 49);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pbClose
            // 
            this.pbClose.Image = ((System.Drawing.Image)(resources.GetObject("pbClose.Image")));
            this.pbClose.Location = new System.Drawing.Point(1026, 12);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(30, 31);
            this.pbClose.TabIndex = 24;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // userRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 581);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dtDob);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCdtBalance);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "userRegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Registration Form";
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbClose;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtPhone;
        public System.Windows.Forms.TextBox txtMail;
        public System.Windows.Forms.TextBox txtCdtBalance;
        public System.Windows.Forms.TextBox txtAddress;
        public System.Windows.Forms.ComboBox cbStatus;
        public System.Windows.Forms.ComboBox cbGender;
        public System.Windows.Forms.ComboBox cbRole;
        public System.Windows.Forms.DateTimePicker dtDob;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button btnSave;
    }
}