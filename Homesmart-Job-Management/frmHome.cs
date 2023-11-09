using System;
using System.Windows.Forms;
using Connections;
using MySql.Data.MySqlClient;

namespace Homesmart_Job_Management
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewjob newjob = new frmNewjob();
            newjob.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearch newSearch = new frmSearch();
            newSearch.Show();
        }


    }
}
