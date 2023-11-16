using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmEditEntry : Form
    {
        public int JobID;

        public int lastTextBoxBottomQ = 206;
        public int lastTextBoxBottomC = 304;
        public int lastTextBoxBottomI = 205;

        List<Control> quoteControls = new List<Control>();
        List<Control> internalChargeControls = new List<Control>();


        public frmEditEntry(int JobID)
        {
            InitializeComponent();
            getInfo(JobID);

            boxCustomerName.TextChanged += new EventHandler(TextBox_TextChanged);
            boxCustomerAddress.TextChanged += new EventHandler(TextBox_TextChanged);
            boxQuoteValue.TextChanged += new EventHandler(TextBox_TextChanged);
            boxTotalCost.TextChanged += new EventHandler(TextBox_TextChanged);
            boxProfit.TextChanged += new EventHandler(TextBox_TextChanged);
            boxMargin.TextChanged += new EventHandler(TextBox_TextChanged);
        }

        private void getInfo(int JobID)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            if (dbConnection.OpenConnection() == true)
            {
                string query = $"SELECT CustomerName, CustomerAddress, QuoteValue, TotalCost, Profit, Margin FROM Job WHERE JobID = @JobID";

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@JobID", JobID);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    boxCustomerName.Text = reader["CustomerName"].ToString();
                    boxCustomerAddress.Text = reader["CustomerAddress"].ToString();
                    boxQuoteValue.Text = reader["QuoteValue"].ToString();
                    boxTotalCost.Text = reader["TotalCost"].ToString();
                    boxProfit.Text = reader["Profit"].ToString();
                    boxMargin.Text = reader["Margin"].ToString();
                }

                reader.Close();
                dbConnection.CloseConnection();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox source = (TextBox)sender;
            txtWarning.Visible = true;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Is the information correct: " +
                $"\nCustomer Name: {boxCustomerName.Text}" +
                $"\nCustomer Address: {boxCustomerAddress.Text}" +
                $"\nQuote Value: {boxQuoteValue.Text}" +
                $"\nTotal Cost: {boxTotalCost.Text}",
                "Confirmation", MessageBoxButtons.OKCancel);


            if (dialogResult == DialogResult.OK)
            {
                int quoteValue = int.Parse(boxQuoteValue.Text);
                int totalCost = int.Parse(boxTotalCost.Text);

                int profit = quoteValue - totalCost;

                int margin = profit / quoteValue * 100;


                DatabaseConnection dbConnection = new DatabaseConnection();

                if (dbConnection.OpenConnection() == true)
                {
                    string query = "UPDATE Job SET CustomerName = @CustomerName, CustomerAddress = @CustomerAddress, QuoteValue = @QuoteValue, TotalCost = @TotalCost, Profit = @Profit, Margin = @Margin WHERE JobID = @JobID";

                    MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                    cmd.Parameters.AddWithValue("@CustomerName", boxCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CustomerAddress", boxCustomerAddress.Text);
                    cmd.Parameters.AddWithValue("@QuoteValue", boxQuoteValue.Text);
                    cmd.Parameters.AddWithValue("@TotalCost", boxTotalCost.Text);
                    cmd.Parameters.AddWithValue("@Profit", profit);
                    cmd.Parameters.AddWithValue("@Margin", margin);
                    cmd.Parameters.AddWithValue("@JobID", 1); //fix

                    cmd.ExecuteNonQuery();

                    dbConnection.CloseConnection();

                    Close();
                }
            }
        }

        private void btnAddQuote_Click(object sender, EventArgs e)
        {
            // Create new input fields
            TextBox newSupplierBox = new TextBox();
            DateTimePicker newDateBox = new DateTimePicker();
            TextBox newRefrenceBox = new TextBox();
            TextBox newValueBox = new TextBox();
            Button removeButton = new Button();

            // Set properties for the new input fields
            newSupplierBox.Location = new Point(18, lastTextBoxBottomQ + 10);
            newSupplierBox.Size = new Size(170, 20);

            newDateBox.Location = new Point(195, lastTextBoxBottomQ + 10);
            newDateBox.Size = new Size(80, 20);
            newDateBox.Format = DateTimePickerFormat.Short;

            newRefrenceBox.Location = new Point(285, lastTextBoxBottomQ + 10);
            newRefrenceBox.Size = new Size(170, 20);

            newValueBox.Location = new Point(645, lastTextBoxBottomQ + 10);
            newValueBox.Size = new Size(80, 20);

            removeButton.Location = new Point(730, lastTextBoxBottomQ + 10); // Adjust this based on your needs
            removeButton.Size = new Size(20, 20); // Adjust this based on your needs
            removeButton.Text = "X";
            removeButton.Click += (s, ev) =>
            {
                // Remove the input fields and the button
                this.Controls.Remove(newSupplierBox);
                this.Controls.Remove(newDateBox);
                this.Controls.Remove(newRefrenceBox);
                this.Controls.Remove(newValueBox);
                this.Controls.Remove(removeButton);

                // Move all controls that are below the removed fields up
                foreach (Control control in this.Controls)
                {
                    if (control.Top > newSupplierBox.Top)
                    {
                        control.Top -= newSupplierBox.Height + 10; // Adjust this based on your needs
                    }
                }

                if (this.Controls.OfType<TextBox>().Any())
                {
                    lastTextBoxBottomQ = this.Controls.OfType<TextBox>().Max(txt => txt.Bottom);
                }
                else
                {
                    lastTextBoxBottomQ = 206; // Reset to the bottom of your last static control
                }

            };

            // Add the new input fields and the button to the form
            this.Controls.Add(newSupplierBox);
            this.Controls.Add(newDateBox);
            this.Controls.Add(newRefrenceBox);
            this.Controls.Add(newValueBox);
            this.Controls.Add(removeButton);

            quoteControls.Add(newSupplierBox);
            quoteControls.Add(newDateBox);
            quoteControls.Add(newRefrenceBox);
            quoteControls.Add(newValueBox);
            quoteControls.Add(removeButton);

            // Update the bottom position of the last TextBox and Label
            lastTextBoxBottomQ = newSupplierBox.Bottom;

            // Move all controls that are below the new fields down
            foreach (Control control in this.Controls)
            {
                if (control.Top > lastTextBoxBottomQ)
                {
                    control.Top += newSupplierBox.Height + 10;
                }
            }
        }

        private void btnAddCharge_Click(object sender, EventArgs e)
        {
            // Create new input fields
            TextBox newCompanyBox = new TextBox();
            TextBox newSupplierBoxQ = new TextBox();
            TextBox newValueBoxQ = new TextBox();
            Button removeButtonQ = new Button();

            // Set properties for the new input fields
            newCompanyBox.Location = new Point(18, lastTextBoxBottomC + 10);
            newCompanyBox.Size = new Size(170, 20);

            newSupplierBoxQ.Location = new Point(285, lastTextBoxBottomC + 10);
            newSupplierBoxQ.Size = new Size(170, 20);

            newValueBoxQ.Location = new Point(645, lastTextBoxBottomC + 10);
            newValueBoxQ.Size = new Size(80, 20);

            removeButtonQ.Location = new Point(730, lastTextBoxBottomC + 10); // Adjust this based on your needs
            removeButtonQ.Size = new Size(20, 20); // Adjust this based on your needs
            removeButtonQ.Text = "X";
            removeButtonQ.Click += (s, ev) =>
            {
                // Remove the input fields and the button
                this.Controls.Remove(newCompanyBox);
                this.Controls.Remove(newSupplierBoxQ);
                this.Controls.Remove(newValueBoxQ);
                this.Controls.Remove(removeButtonQ);

                // Move all controls that are below the removed fields up
                foreach (Control control in this.Controls)
                {
                    if (control.Top > newCompanyBox.Top)
                    {
                        control.Top -= newCompanyBox.Height + 10; // Adjust this based on your needs
                    }
                }

                if (this.Controls.OfType<TextBox>().Any())
                {
                    lastTextBoxBottomC = this.Controls.OfType<TextBox>().Max(txt => txt.Bottom);
                }
                else
                {
                    lastTextBoxBottomC = 304; // Reset to the bottom of your last static control
                }
            };

            // Add the new input fields and the button to the form
            this.Controls.Add(newCompanyBox);
            this.Controls.Add(newSupplierBoxQ);
            this.Controls.Add(newValueBoxQ);
            this.Controls.Add(removeButtonQ);

            internalChargeControls.Add(newCompanyBox);
            internalChargeControls.Add(newSupplierBoxQ);
            internalChargeControls.Add(newValueBoxQ);
            internalChargeControls.Add(removeButtonQ);

            // Update the bottom position of the last TextBox and Label
            lastTextBoxBottomC = newCompanyBox.Bottom;

            // Move all controls that are below the new fields down
            foreach (Control control in this.Controls)
            {
                if (control.Top > lastTextBoxBottomC)
                {
                    control.Top += newCompanyBox.Height + 10;
                }
            }
        }
    }
}

//"SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName AND CustomerAddress = @customerAddress";
