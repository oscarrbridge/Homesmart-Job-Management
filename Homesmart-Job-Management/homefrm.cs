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
    public partial class homefrm : Form
    {
        int index = 0;

        public homefrm()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            index += 1;
            pnlButtons.Visible = false;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            index += 1;
            pnlButtons.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            index -= 1;
            if(index == 0)
            {
                pnlButtons.Visible = true;
                pnlNewEntry.Visible = false;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            index += 1;
            pnlButtons.Visible = false;
            pnlNewEntry.Visible = true;
        }

        private void btnSubmitNew_Click(object sender, EventArgs e)
        {

            string query = "SELECT * FROM Job";

            DatabaseConnection dbConnection = new DatabaseConnection();

            //open connection
            if (dbConnection.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection());

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //Access data column by column. For example, if you have a column named 'JobName'
                    string jobName = dataReader["JobID"].ToString();

                    //Do something with jobName
                    Console.WriteLine(jobName);
                }

                //close Data Reader
                dataReader.Close();

                //close connection
                dbConnection.CloseConnection();
            }
        }
    }
}
