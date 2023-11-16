using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmSearch : Form
    {
        public frmSearch()
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
                    query = "SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName AND CustomerAddress = @customerAddress";

                }
                else if (boxCustomerName.Text.Length > 0)
                {
                    query = "SELECT JobID, CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName";
                }
                else if (boxCustomerAddress.Text.Length > 0)
                {
                    query = "SELECT JobID, CustomerName, CustomerAddress FROM Job WHERE CustomerAddress = @customerAddress";
                }
                else if (boxCustomerName.Text.Length == 0 && boxCustomerAddress.Text.Length == 0)
                {
                    query = "SELECT JobID, CustomerName, CustomerAddress FROM Job";
                }

                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                cmd.Parameters.AddWithValue("@customerName", boxCustomerName.Text);
                cmd.Parameters.AddWithValue("@customerAddress", boxCustomerAddress.Text);

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

        private void boxCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (boxCustomerName.Text.Length > 0 || boxCustomerAddress.Text.Length > 0)
            {
                btnSearch.Enabled = true;
            }
            else
            {
                btnSearch.Enabled = false;
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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void dataGrid_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // If the 'Edit' button was clicked.
            if (dataGrid.Columns[e.ColumnIndex].Name == "btnEdit" && e.RowIndex >= 0)
            {
                // Get the job ID from the selected row.
                int jobId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["JobID"].Value);

                frmSearch newSearch = new frmSearch();

                frmEditEntry newEditJob = new frmEditEntry(jobId);
            }
        }
    }
}
