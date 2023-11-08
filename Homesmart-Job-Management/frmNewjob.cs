using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Connections;
using MySql.Data.MySqlClient;

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
            DatabaseConnection dbConnection = new DatabaseConnection();

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
        }
    }
}
