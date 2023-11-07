
namespace Homesmart_Job_Management
{
    partial class homefrm
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
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlNewEntry = new System.Windows.Forms.Panel();
            this.txtQuote = new System.Windows.Forms.Label();
            this.boxQuote = new System.Windows.Forms.TextBox();
            this.txtBudget2 = new System.Windows.Forms.Label();
            this.boxBudget2 = new System.Windows.Forms.TextBox();
            this.txtBudget = new System.Windows.Forms.Label();
            this.boxBudget = new System.Windows.Forms.TextBox();
            this.txtCompanyName2 = new System.Windows.Forms.Label();
            this.boxCompanyName2 = new System.Windows.Forms.TextBox();
            this.txtExpenseName2 = new System.Windows.Forms.Label();
            this.ExpenseName2 = new System.Windows.Forms.TextBox();
            this.boxCompanyName = new System.Windows.Forms.Label();
            this.CompanyName = new System.Windows.Forms.TextBox();
            this.txtExpenseName = new System.Windows.Forms.Label();
            this.boxExpenseName = new System.Windows.Forms.TextBox();
            this.txtCustomerEmail = new System.Windows.Forms.Label();
            this.boxCustomerEmail = new System.Windows.Forms.TextBox();
            this.txtCustomerAddress = new System.Windows.Forms.Label();
            this.boxCustomerAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.Label();
            this.boxCustomerName = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnSubmitNew = new System.Windows.Forms.Button();
            this.pnlButtons.SuspendLayout();
            this.pnlNewEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.AutoSize = true;
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Controls.Add(this.btnNew);
            this.pnlButtons.Location = new System.Drawing.Point(12, 39);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(129, 138);
            this.pnlButtons.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Location = new System.Drawing.Point(3, 49);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(115, 40);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit Entry";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(3, 95);
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
            // pnlNewEntry
            // 
            this.pnlNewEntry.Controls.Add(this.btnSubmitNew);
            this.pnlNewEntry.Controls.Add(this.txtQuote);
            this.pnlNewEntry.Controls.Add(this.boxQuote);
            this.pnlNewEntry.Controls.Add(this.txtBudget2);
            this.pnlNewEntry.Controls.Add(this.boxBudget2);
            this.pnlNewEntry.Controls.Add(this.txtBudget);
            this.pnlNewEntry.Controls.Add(this.boxBudget);
            this.pnlNewEntry.Controls.Add(this.txtCompanyName2);
            this.pnlNewEntry.Controls.Add(this.boxCompanyName2);
            this.pnlNewEntry.Controls.Add(this.txtExpenseName2);
            this.pnlNewEntry.Controls.Add(this.ExpenseName2);
            this.pnlNewEntry.Controls.Add(this.boxCompanyName);
            this.pnlNewEntry.Controls.Add(this.CompanyName);
            this.pnlNewEntry.Controls.Add(this.txtExpenseName);
            this.pnlNewEntry.Controls.Add(this.boxExpenseName);
            this.pnlNewEntry.Controls.Add(this.txtCustomerEmail);
            this.pnlNewEntry.Controls.Add(this.boxCustomerEmail);
            this.pnlNewEntry.Controls.Add(this.txtCustomerAddress);
            this.pnlNewEntry.Controls.Add(this.boxCustomerAddress);
            this.pnlNewEntry.Controls.Add(this.txtCustomerName);
            this.pnlNewEntry.Controls.Add(this.boxCustomerName);
            this.pnlNewEntry.Location = new System.Drawing.Point(362, 31);
            this.pnlNewEntry.Name = "pnlNewEntry";
            this.pnlNewEntry.Size = new System.Drawing.Size(426, 407);
            this.pnlNewEntry.TabIndex = 1;
            this.pnlNewEntry.Visible = false;
            // 
            // txtQuote
            // 
            this.txtQuote.AutoSize = true;
            this.txtQuote.Location = new System.Drawing.Point(3, 373);
            this.txtQuote.Name = "txtQuote";
            this.txtQuote.Size = new System.Drawing.Size(66, 13);
            this.txtQuote.TabIndex = 19;
            this.txtQuote.Text = "Quote Value";
            // 
            // boxQuote
            // 
            this.boxQuote.Location = new System.Drawing.Point(101, 370);
            this.boxQuote.Name = "boxQuote";
            this.boxQuote.Size = new System.Drawing.Size(171, 20);
            this.boxQuote.TabIndex = 18;
            // 
            // txtBudget2
            // 
            this.txtBudget2.AutoSize = true;
            this.txtBudget2.Location = new System.Drawing.Point(3, 320);
            this.txtBudget2.Name = "txtBudget2";
            this.txtBudget2.Size = new System.Drawing.Size(41, 13);
            this.txtBudget2.TabIndex = 17;
            this.txtBudget2.Text = "Budget";
            // 
            // boxBudget2
            // 
            this.boxBudget2.Location = new System.Drawing.Point(101, 317);
            this.boxBudget2.Name = "boxBudget2";
            this.boxBudget2.Size = new System.Drawing.Size(171, 20);
            this.boxBudget2.TabIndex = 16;
            // 
            // txtBudget
            // 
            this.txtBudget.AutoSize = true;
            this.txtBudget.Location = new System.Drawing.Point(3, 202);
            this.txtBudget.Name = "txtBudget";
            this.txtBudget.Size = new System.Drawing.Size(41, 13);
            this.txtBudget.TabIndex = 15;
            this.txtBudget.Text = "Budget";
            // 
            // boxBudget
            // 
            this.boxBudget.Location = new System.Drawing.Point(101, 199);
            this.boxBudget.Name = "boxBudget";
            this.boxBudget.Size = new System.Drawing.Size(171, 20);
            this.boxBudget.TabIndex = 14;
            // 
            // txtCompanyName2
            // 
            this.txtCompanyName2.AutoSize = true;
            this.txtCompanyName2.Location = new System.Drawing.Point(3, 288);
            this.txtCompanyName2.Name = "txtCompanyName2";
            this.txtCompanyName2.Size = new System.Drawing.Size(82, 13);
            this.txtCompanyName2.TabIndex = 13;
            this.txtCompanyName2.Text = "Company Name";
            // 
            // boxCompanyName2
            // 
            this.boxCompanyName2.Location = new System.Drawing.Point(101, 285);
            this.boxCompanyName2.Name = "boxCompanyName2";
            this.boxCompanyName2.Size = new System.Drawing.Size(171, 20);
            this.boxCompanyName2.TabIndex = 12;
            // 
            // txtExpenseName2
            // 
            this.txtExpenseName2.AutoSize = true;
            this.txtExpenseName2.Location = new System.Drawing.Point(3, 256);
            this.txtExpenseName2.Name = "txtExpenseName2";
            this.txtExpenseName2.Size = new System.Drawing.Size(79, 13);
            this.txtExpenseName2.TabIndex = 11;
            this.txtExpenseName2.Text = "Expense Name";
            // 
            // ExpenseName2
            // 
            this.ExpenseName2.Location = new System.Drawing.Point(101, 253);
            this.ExpenseName2.Name = "ExpenseName2";
            this.ExpenseName2.Size = new System.Drawing.Size(171, 20);
            this.ExpenseName2.TabIndex = 10;
            // 
            // boxCompanyName
            // 
            this.boxCompanyName.AutoSize = true;
            this.boxCompanyName.Location = new System.Drawing.Point(3, 172);
            this.boxCompanyName.Name = "boxCompanyName";
            this.boxCompanyName.Size = new System.Drawing.Size(82, 13);
            this.boxCompanyName.TabIndex = 9;
            this.boxCompanyName.Text = "Company Name";
            // 
            // CompanyName
            // 
            this.CompanyName.Location = new System.Drawing.Point(101, 169);
            this.CompanyName.Name = "CompanyName";
            this.CompanyName.Size = new System.Drawing.Size(171, 20);
            this.CompanyName.TabIndex = 8;
            // 
            // txtExpenseName
            // 
            this.txtExpenseName.AutoSize = true;
            this.txtExpenseName.Location = new System.Drawing.Point(3, 140);
            this.txtExpenseName.Name = "txtExpenseName";
            this.txtExpenseName.Size = new System.Drawing.Size(79, 13);
            this.txtExpenseName.TabIndex = 7;
            this.txtExpenseName.Text = "Expense Name";
            // 
            // boxExpenseName
            // 
            this.boxExpenseName.Location = new System.Drawing.Point(101, 137);
            this.boxExpenseName.Name = "boxExpenseName";
            this.boxExpenseName.Size = new System.Drawing.Size(171, 20);
            this.boxExpenseName.TabIndex = 6;
            // 
            // txtCustomerEmail
            // 
            this.txtCustomerEmail.AutoSize = true;
            this.txtCustomerEmail.Location = new System.Drawing.Point(3, 44);
            this.txtCustomerEmail.Name = "txtCustomerEmail";
            this.txtCustomerEmail.Size = new System.Drawing.Size(79, 13);
            this.txtCustomerEmail.TabIndex = 5;
            this.txtCustomerEmail.Text = "Customer Email";
            // 
            // boxCustomerEmail
            // 
            this.boxCustomerEmail.Location = new System.Drawing.Point(101, 41);
            this.boxCustomerEmail.Name = "boxCustomerEmail";
            this.boxCustomerEmail.Size = new System.Drawing.Size(171, 20);
            this.boxCustomerEmail.TabIndex = 4;
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.AutoSize = true;
            this.txtCustomerAddress.Location = new System.Drawing.Point(3, 76);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(92, 13);
            this.txtCustomerAddress.TabIndex = 3;
            this.txtCustomerAddress.Text = "Customer Address";
            // 
            // boxCustomerAddress
            // 
            this.boxCustomerAddress.Location = new System.Drawing.Point(101, 73);
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
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(15, 381);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(115, 40);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnSubmitNew
            // 
            this.btnSubmitNew.Location = new System.Drawing.Point(296, 350);
            this.btnSubmitNew.Name = "btnSubmitNew";
            this.btnSubmitNew.Size = new System.Drawing.Size(115, 40);
            this.btnSubmitNew.TabIndex = 4;
            this.btnSubmitNew.Text = "Submit";
            this.btnSubmitNew.UseVisualStyleBackColor = true;
            this.btnSubmitNew.Click += new System.EventHandler(this.btnSubmitNew_Click);
            // 
            // homefrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pnlNewEntry);
            this.Controls.Add(this.pnlButtons);
            this.Name = "homefrm";
            this.Text = "Form1";
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.pnlNewEntry.ResumeLayout(false);
            this.pnlNewEntry.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel pnlNewEntry;
        private System.Windows.Forms.Label txtCustomerEmail;
        private System.Windows.Forms.TextBox boxCustomerEmail;
        private System.Windows.Forms.Label txtCustomerAddress;
        private System.Windows.Forms.TextBox boxCustomerAddress;
        private System.Windows.Forms.Label txtCustomerName;
        private System.Windows.Forms.TextBox boxCustomerName;
        private System.Windows.Forms.Label txtExpenseName;
        private System.Windows.Forms.TextBox boxExpenseName;
        private System.Windows.Forms.Label boxCompanyName;
        private System.Windows.Forms.TextBox CompanyName;
        private System.Windows.Forms.Label txtQuote;
        private System.Windows.Forms.TextBox boxQuote;
        private System.Windows.Forms.Label txtBudget2;
        private System.Windows.Forms.TextBox boxBudget2;
        private System.Windows.Forms.Label txtBudget;
        private System.Windows.Forms.TextBox boxBudget;
        private System.Windows.Forms.Label txtCompanyName2;
        private System.Windows.Forms.TextBox boxCompanyName2;
        private System.Windows.Forms.Label txtExpenseName2;
        private System.Windows.Forms.TextBox ExpenseName2;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnSubmitNew;
    }
}

