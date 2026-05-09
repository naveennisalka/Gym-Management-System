namespace Gym_Management_System
{
    partial class formStore
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formStore));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.flpStore = new System.Windows.Forms.FlowLayoutPanel();
            this.flpCurrentOrder = new System.Windows.Forms.FlowLayoutPanel();
            this.orderItem10 = new Gym_Management_System.orderItem();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.flpCurrentOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.flpCurrentOrder);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(911, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 776);
            this.panel1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.button8);
            this.panel5.Controls.Add(this.button9);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(615, 64);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current Order";
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(513, 18);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(91, 27);
            this.button8.TabIndex = 8;
            this.button8.Text = "Clear";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(416, 18);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(91, 27);
            this.button9.TabIndex = 9;
            this.button9.Text = "Hold";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 653);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(615, 123);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(498, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 29);
            this.label4.TabIndex = 13;
            this.label4.Text = "32000.00";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(567, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Rs";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Total";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(13, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(592, 44);
            this.button1.TabIndex = 1;
            this.button1.Text = "Place Order";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(21, 16);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(20);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(751, 29);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(6, 682);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(109, 83);
            this.button7.TabIndex = 4;
            this.button7.Text = "\r\nHold Orders";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(122, 776);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.flpStore);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(122, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(789, 776);
            this.panel4.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtSearch);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(789, 64);
            this.panel6.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(6, 62);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 83);
            this.button3.TabIndex = 5;
            this.button3.Text = "\r\nStore Items";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(6, 151);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 83);
            this.button4.TabIndex = 6;
            this.button4.Text = "\r\nMemberships";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // flpStore
            // 
            this.flpStore.AutoScroll = true;
            this.flpStore.AutoSize = true;
            this.flpStore.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpStore.Location = new System.Drawing.Point(0, 64);
            this.flpStore.Name = "flpStore";
            this.flpStore.Padding = new System.Windows.Forms.Padding(16);
            this.flpStore.Size = new System.Drawing.Size(789, 712);
            this.flpStore.TabIndex = 2;
            // 
            // flpCurrentOrder
            // 
            this.flpCurrentOrder.AutoScroll = true;
            this.flpCurrentOrder.Controls.Add(this.orderItem10);
            this.flpCurrentOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCurrentOrder.Location = new System.Drawing.Point(0, 64);
            this.flpCurrentOrder.Name = "flpCurrentOrder";
            this.flpCurrentOrder.Padding = new System.Windows.Forms.Padding(20);
            this.flpCurrentOrder.Size = new System.Drawing.Size(615, 589);
            this.flpCurrentOrder.TabIndex = 2;
            // 
            // orderItem10
            // 
            this.orderItem10.BackColor = System.Drawing.Color.Transparent;
            this.orderItem10.Location = new System.Drawing.Point(23, 23);
            this.orderItem10.Name = "orderItem10";
            this.orderItem10.Size = new System.Drawing.Size(549, 61);
            this.orderItem10.TabIndex = 9;
            // 
            // formStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1526, 776);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "formStore";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.flpCurrentOrder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FlowLayoutPanel flpStore;
        private System.Windows.Forms.FlowLayoutPanel flpCurrentOrder;
        private orderItem orderItem10;
    }
}