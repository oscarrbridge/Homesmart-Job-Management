using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();

            search();

            dataGrid.Columns[1].Width = 40;
            dataGrid.Columns[4].Width = 83;
            
            dataGrid1.Columns[2].Width = 50;
            dataGrid1.Columns[3].Width = 70;
            dataGrid1.Columns[4].Width = 102;

        }

        private void search()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            if (dbConnection.OpenConnection() == true)
            {
                string query = "";

                if (boxCustomerName.Text.Length > 0 && boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT Job.JobID, Job.CustomerName, Job.CustomerAddress, AddCustQuote.QuoteOwner FROM Job LEFT JOIN (SELECT JobID, MIN(CustQuoteID) as MinCustQuoteID FROM AddCustQuote GROUP BY JobID) as MinQuotes ON Job.JobID = MinQuotes.JobID LEFT JOIN AddCustQuote ON MinQuotes.JobID = AddCustQuote.JobID AND MinQuotes.MinCustQuoteID = AddCustQuote.CustQuoteID WHERE Job.CustomerName LIKE @CustomerName AND Job.CustomerAddress LIKE @CustomerAddress";
                }
                else if (boxCustomerName.Text.Length > 0)
                {
                    query = "SELECT Job.JobID, Job.CustomerName, Job.CustomerAddress, AddCustQuote.QuoteOwner FROM Job LEFT JOIN (SELECT JobID, MIN(CustQuoteID) as MinCustQuoteID FROM AddCustQuote GROUP BY JobID) as MinQuotes ON Job.JobID = MinQuotes.JobID LEFT JOIN AddCustQuote ON MinQuotes.JobID = AddCustQuote.JobID AND MinQuotes.MinCustQuoteID = AddCustQuote.CustQuoteID WHERE Job.CustomerName LIKE @CustomerName";
                }
                else if (boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT Job.JobID, Job.CustomerName, Job.CustomerAddress, AddCustQuote.QuoteOwner FROM Job LEFT JOIN (SELECT JobID, MIN(CustQuoteID) as MinCustQuoteID FROM AddCustQuote GROUP BY JobID) as MinQuotes ON Job.JobID = MinQuotes.JobID LEFT JOIN AddCustQuote ON MinQuotes.JobID = AddCustQuote.JobID AND MinQuotes.MinCustQuoteID = AddCustQuote.CustQuoteID WHERE Job.CustomerAddress LIKE @CustomerAddress";
                }
                else if (boxCustomerName.Text.Length == 0 && boxCustomerAddress.Text.Length == 0)
                {
                    query = "SELECT Job.JobID, Job.CustomerName, Job.CustomerAddress, AddCustQuote.QuoteOwner FROM Job LEFT JOIN (SELECT JobID, MIN(CustQuoteID) as MinCustQuoteID FROM AddCustQuote GROUP BY JobID) as MinQuotes ON Job.JobID = MinQuotes.JobID LEFT JOIN AddCustQuote ON MinQuotes.JobID = AddCustQuote.JobID AND MinQuotes.MinCustQuoteID = AddCustQuote.CustQuoteID";
                }

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@CustomerName", "%" + boxCustomerName.Text + "%");
                cmd.Parameters.AddWithValue("@CustomerAddress", "%" + boxCustomerAddress.Text + "%");

                DataTable dt = new DataTable();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                dataGrid.DataSource = dt;

                string houseWashQuery = "SELECT Job.CustomerAddress, Job.JobID, FollowUp.HouseWashDate as Date, 'House Wash' as Description FROM FollowUp INNER JOIN Job ON FollowUp.JobID = Job.JobID WHERE FollowUp.HouseWash = 1";
                string mossTreatmentQuery = "SELECT Job.CustomerAddress, Job.JobID, FollowUp.MossTreatmentDate as Date, 'Moss Treatment' as Description FROM FollowUp INNER JOIN Job ON FollowUp.JobID = Job.JobID WHERE FollowUp.MossTreatment = 1";
                string otherQuery = "SELECT Job.CustomerAddress, Job.JobID, FollowUp.OtherDate as Date, FollowUp.OtherDesc as Description FROM FollowUp INNER JOIN Job ON FollowUp.JobID = Job.JobID WHERE FollowUp.Other = 1";

                MySqlCommand houseWashCmd = new MySqlCommand(houseWashQuery, dbConnection.GetConnection());
                MySqlCommand mossTreatmentCmd = new MySqlCommand(mossTreatmentQuery, dbConnection.GetConnection());
                MySqlCommand otherCmd = new MySqlCommand(otherQuery, dbConnection.GetConnection());

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                MySqlDataAdapter da1 = new MySqlDataAdapter(houseWashCmd);
                MySqlDataAdapter da2 = new MySqlDataAdapter(mossTreatmentCmd);
                MySqlDataAdapter da3 = new MySqlDataAdapter(otherCmd);

                da1.Fill(dt1);
                da2.Fill(dt2);
                da3.Fill(dt3);

                // Combine all DataTables into one
                dt1.Merge(dt2);
                dt1.Merge(dt3);

                // Convert the DataTable to an enumerable collection and sort by date
                var orderedRows = from row in dt1.AsEnumerable()
                                  orderby row.Field<DateTime>("Date")
                                  select row;

                // Convert the sorted collection back to a DataTable
                DataTable sortedTable = orderedRows.CopyToDataTable();

                dataGrid1.DataSource = sortedTable;
            }
            else
            {
                DialogResult result = MessageBox.Show("Server not found. Contact Admin", "Error", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    search();
                }
                else
                {
                    this.Close();
                }
            }
            dbConnection.CloseConnection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void boxCustomerName_TextChanged_1(object sender, EventArgs e)
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

        private void dataGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1);

            // If the 'Edit' button was clicked.
            else if (dataGrid.Columns[e.ColumnIndex].Name == "btnEdit" && e.RowIndex >= 0)
            {
                // Get the job ID from the selected row.
                int jobId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["JobID"].Value);

                frmEditEntry newEditJob = new frmEditEntry(jobId);
                newEditJob.Show();
            }
        }
        private void dataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1);

            // If the 'Edit' button was clicked.
            else if (dataGrid1.Columns[e.ColumnIndex].Name == "btnEdit1" && e.RowIndex >= 0)
            {
                // Get the job ID from the selected row.
                int jobId = Convert.ToInt32(dataGrid1.Rows[e.RowIndex].Cells["JobID"].Value);

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
                search();
            }
        }

        private void frmHome_Enter(object sender, EventArgs e)
        {
            search();
        }

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            boxCustomerName.Text = "";
            boxCustomerAddress.Text = "";
            search();
        }

    }
}
