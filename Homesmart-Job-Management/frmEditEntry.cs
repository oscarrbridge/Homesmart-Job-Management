using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connections;
using System.Data.Odbc;
using System.Security.Cryptography;

namespace Homesmart_Job_Management
{
    public partial class frmEditEntry : Form
    {
        public int JobID;

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
            btnSaveCustomer.Enabled = true;
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
    }
}

//"SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName AND CustomerAddress = @customerAddress";
