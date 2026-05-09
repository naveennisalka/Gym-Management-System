namespace Gym_Management_System.Components
{
    partial class wgtHoldOrders
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(wgtHoldOrders));
            this.roundedPannel1 = new Gym_Management_System.RoundedPannel();
            this.lblName = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.roundedPannel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundedPannel1
            // 
            this.roundedPannel1.BackColor = System.Drawing.Color.White;
            this.roundedPannel1.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPannel1.BorderRadius = 16;
            this.roundedPannel1.BorderThickness = 0F;
            this.roundedPannel1.Controls.Add(this.lblName);
            this.roundedPannel1.Controls.Add(this.button3);
            this.roundedPannel1.Controls.Add(this.lblTotal);
            this.roundedPannel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPannel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPannel1.Name = "roundedPannel1";
            this.roundedPannel1.Size = new System.Drawing.Size(485, 53);
            this.roundedPannel1.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(18, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 18);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Hold name";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            this.lblName.Click += new System.EventHandler(this.roundedPannel1_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.BackColor = System.Drawing.Color.MistyRose;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(437, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 33);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(335, 17);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(86, 18);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "20000.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // gymMembership
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.roundedPannel1);
            this.Name = "gymMembership";
            this.Size = new System.Drawing.Size(485, 78);
            this.roundedPannel1.ResumeLayout(false);
            this.roundedPannel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPannel roundedPannel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblTotal;
    }
}
