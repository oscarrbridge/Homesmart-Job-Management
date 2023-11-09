
namespace Homesmart_Job_Management
{
    partial class frmHome
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
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Controls.Add(this.btnNew);
            this.pnlButtons.Location = new System.Drawing.Point(321, 185);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(121, 92);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(3, 49);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(115, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = true;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(115, 40);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New Entry";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Homesmart_Job_Management.Properties.Resources.logo_plaster_home_specialists_w_;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 50);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlButtons);
            this.Name = "frmHome";
            this.Text = "Form1";
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

