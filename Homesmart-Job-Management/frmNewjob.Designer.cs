
namespace Homesmart_Job_Management
{
    partial class frmNewjob
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
            this.pnlNewEntry = new System.Windows.Forms.Panel();
            this.btnSubmitNew = new System.Windows.Forms.Button();
            this.txtCustomerAddress = new System.Windows.Forms.Label();
            this.boxCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.Label();
            this.boxCustomerName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlNewEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNewEntry
            // 
            this.pnlNewEntry.Controls.Add(this.btnSubmitNew);
            this.pnlNewEntry.Controls.Add(this.txtCustomerAddress);
            this.pnlNewEntry.Controls.Add(this.boxCustomerAddress);
            this.pnlNewEntry.Controls.Add(this.txtCustomerName);
            this.pnlNewEntry.Controls.Add(this.boxCustomerName);
            this.pnlNewEntry.Location = new System.Drawing.Point(186, 181);
            this.pnlNewEntry.Name = "pnlNewEntry";
            this.pnlNewEntry.Size = new System.Drawing.Size(426, 70);
            this.pnlNewEntry.TabIndex = 2;
            // 
            // btnSubmitNew
            // 
            this.btnSubmitNew.Location = new System.Drawing.Point(292, 11);
            this.btnSubmitNew.Name = "btnSubmitNew";
            this.btnSubmitNew.Size = new System.Drawing.Size(115, 40);
            this.btnSubmitNew.TabIndex = 4;
            this.btnSubmitNew.Text = "Submit";
            this.btnSubmitNew.UseVisualStyleBackColor = true;
            this.btnSubmitNew.Click += new System.EventHandler(this.btnSubmitNew_Click);
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.AutoSize = true;
            this.txtCustomerAddress.Location = new System.Drawing.Point(3, 46);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(92, 13);
            this.txtCustomerAddress.TabIndex = 3;
            this.txtCustomerAddress.Text = "Customer Address";
            // 
            // boxCustomerAddress
            // 
            this.boxCustomerAddress.Location = new System.Drawing.Point(101, 43);
            this.boxCustomerAddress.Name = "boxCustomerAddress";
            this.boxCustomerAddress.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerAddress.TabIndex = 2;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.AutoSize = true;
            this.txtCustomerName.Location = new System.Drawing.Point(3, 11);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(82, 13);
            this.txtCustomerName.TabIndex = 1;
            this.txtCustomerName.Text = "Customer Name";
            // 
            // boxCustomerName
            // 
            this.boxCustomerName.Location = new System.Drawing.Point(101, 8);
            this.boxCustomerName.Name = "boxCustomerName";
            this.boxCustomerName.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerName.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Homesmart_Job_Management.Properties.Resources.logo_plaster_home_specialists_w_;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 50);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmNewjob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlNewEntry);
            this.Name = "frmNewjob";
            this.Text = "newjob";
            this.pnlNewEntry.ResumeLayout(false);
            this.pnlNewEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNewEntry;
        private System.Windows.Forms.Button btnSubmitNew;
        private System.Windows.Forms.Label txtCustomerAddress;
        private System.Windows.Forms.TextBox boxCustomerAddress;
        private System.Windows.Forms.Label txtCustomerName;
        private System.Windows.Forms.TextBox boxCustomerName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}