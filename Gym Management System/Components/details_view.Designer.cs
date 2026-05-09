namespace Gym_Management_System.Components
{
    partial class details_view
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(details_view));
            this.roundedPannel1 = new Gym_Management_System.RoundedPannel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.roundedPannel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundedPannel1
            // 
            this.roundedPannel1.BackColor = System.Drawing.Color.White;
            this.roundedPannel1.BorderColor = System.Drawing.Color.Transparent;
            this.roundedPannel1.BorderRadius = 8;
            this.roundedPannel1.BorderThickness = 0F;
            this.roundedPannel1.Controls.Add(this.button1);
            this.roundedPannel1.Controls.Add(this.lblName);
            this.roundedPannel1.Controls.Add(this.button3);
            this.roundedPannel1.Controls.Add(this.button2);
            this.roundedPannel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPannel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPannel1.Name = "roundedPannel1";
            this.roundedPannel1.Size = new System.Drawing.Size(1130, 53);
            this.roundedPannel1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(1006, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 33);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(6, 17);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 18);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Item Name";
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.BackColor = System.Drawing.Color.MistyRose;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(1086, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 33);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(1046, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 33);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // details_view
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.roundedPannel1);
            this.Name = "details_view";
            this.Size = new System.Drawing.Size(1130, 53);
            this.roundedPannel1.ResumeLayout(false);
            this.roundedPannel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPannel roundedPannel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}
