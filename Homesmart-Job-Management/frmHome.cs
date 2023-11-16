using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
            search();
        }

        private void search()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            if (dbConnection.OpenConnection() == true)
            {
                string query = "";

                if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @CustomerName AND CustomerAddress = @CustomerAddress";
                }
                else if (boxCustomerName.Text.Length > 0)
                {
                    query = "SELECT JobID, CustomerName, CustomerAddress FROM Job WHERE CustomerName = @CustomerName";
                }
                else if (boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT JobID, CustomerName, CustomerAddress FROM Job WHERE CustomerAddress = @CustomerAddress";
                }
                else if (boxCustomerName.Text.Length == 0 && boxCustomerAddress.Text.Length == 0)
                {
                    query = "SELECT JobID, CustomerName, CustomerAddress FROM Job";
                }

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@CustomerName", boxCustomerName.Text);
                cmd.Parameters.AddWithValue("@CustomerAddress", boxCustomerAddress.Text);

                DataTable dt = new DataTable();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                dataGrid.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void boxCustomerName_TextChanged_1(object sender, EventArgs e)
        {
            if (boxCustomerName.Text.Length > 0 || boxCustomerAddress.Text.Length > 0)
            {
                btnSearch.Enabled = true;
            }
            else
            {
                btnSearch.Enabled = false;
            }

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
            if (boxCustomerName.Text.Length > 0 || boxCustomerAddress.Text.Length > 0)
            {
                btnSearch.Enabled = true;
            }
            else
            {
                btnSearch.Enabled = false;
            }

            if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
            {
                btnSubmitNew.Enabled = true;
            }
            else
            {
                btnSubmitNew.Enabled = false;
            }
        }

        private void dataGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // If the 'Edit' button was clicked.
            if (dataGrid.Columns[e.ColumnIndex].Name == "btnEdit" && e.RowIndex >= 0)
            {
                // Get the job ID from the selected row.
                int jobId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["JobID"].Value);

                frmEditEntry newEditJob = new frmEditEntry(jobId);
                newEditJob.Show();
            }
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
                    string query = "INSERT INTO Job (CustomerName, CustomerAddress) VALUES (@CustomerName, @CustomerAddress)";

                    MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                    cmd.Parameters.AddWithValue("@CustomerName", boxCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CustomerAddress", boxCustomerAddress.Text);

                    cmd.ExecuteNonQuery();

                    dbConnection.CloseConnection();
                }
            }
        }
    }
}
