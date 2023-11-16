using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmNewjob : Form
    {
        public frmNewjob()
        {
            InitializeComponent();
        }

        private void btnSubmitNew_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Is the information correct: " +
                $"\nCustomer Name: {boxCustomerName.Text}" +
                $"\nCustomer Address: {boxCustomerAddress.Text}", "Confirmation", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                if (dbConnection.OpenConnection() == true)
                {
                    string query = "INSERT INTO Job (CustomerName, CustomerAddress) VALUES (@customerName, @customerAddress)";

                    MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                    cmd.Parameters.AddWithValue("@customerName", boxCustomerName.Text);
                    cmd.Parameters.AddWithValue("@customerAddress", boxCustomerAddress.Text);

                    cmd.ExecuteNonQuery();

                    dbConnection.CloseConnection();

                    Close();
                }
            }
        }

        private void boxCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
            {
                btnSubmitNew.Enabled = true;
            }
            else
            {
                btnSubmitNew.Enabled = false;
            }
        }

        private void boxCustomerAddress_TextChanged(object sender, EventArgs e)
        {
            if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
            {
                btnSubmitNew.Enabled = true;
            }
            else
            {
                btnSubmitNew.Enabled = false;
            }
        }
    }
}


/*
   if (dbConnection.OpenConnection() == true)
            {
                string query = "SELECT * FROM Job";

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader["JobID"] + ": " + dataReader["QuoteValue"]);
                }

                dataReader.Close();

                dbConnection.CloseConnection();
            }
*/