namespace Homesmart_Job_Management
{
    partial class frmSearch
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.txtCustomerAddress = new System.Windows.Forms.Label();
            this.boxCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.Label();
            this.boxCustomerName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
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
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 103);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(298, 324);
            this.dataGrid.TabIndex = 6;
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.AutoSize = true;
            this.txtCustomerAddress.Location = new System.Drawing.Point(332, 49);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(92, 13);
            this.txtCustomerAddress.TabIndex = 10;
            this.txtCustomerAddress.Text = "Customer Address";
            // 
            // boxCustomerAddress
            // 
            this.boxCustomerAddress.Location = new System.Drawing.Point(430, 46);
            this.boxCustomerAddress.Name = "boxCustomerAddress";
            this.boxCustomerAddress.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerAddress.TabIndex = 9;
            this.boxCustomerAddress.TextChanged += new System.EventHandler(this.boxCustomerAddress_TextChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.AutoSize = true;
            this.txtCustomerName.Location = new System.Drawing.Point(332, 22);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(82, 13);
            this.txtCustomerName.TabIndex = 8;
            this.txtCustomerName.Text = "Customer Name";
            // 
            // boxCustomerName
            // 
            this.boxCustomerName.Location = new System.Drawing.Point(430, 19);
            this.boxCustomerName.Name = "boxCustomerName";
            this.boxCustomerName.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerName.TabIndex = 7;
            this.boxCustomerName.TextChanged += new System.EventHandler(this.boxCustomerName_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(636, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(115, 40);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCustomerAddress);
            this.Controls.Add(this.boxCustomerAddress);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.boxCustomerName);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmSearch";
            this.Text = "frmSearch";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label txtCustomerAddress;
        private System.Windows.Forms.TextBox boxCustomerAddress;
        private System.Windows.Forms.Label txtCustomerName;
        private System.Windows.Forms.TextBox boxCustomerName;
        private System.Windows.Forms.Button btnSearch;
    }
}