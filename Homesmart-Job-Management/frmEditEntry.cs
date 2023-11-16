using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmEditEntry : Form
    {
        public int JobID;

        private int countQ = 0;
        private int countC = 0;
        private int countI = 0;


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
            int startPosX = 18;
            int startPosY = 216;

            // Create new TextBox and Button
            TextBox textBox = new TextBox();
            Button button = new Button();

            // Set properties
            textBox.Name = "textBox" + countQ;
            textBox.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            button.Text = "X";
            button.Name = "button" + countQ;
            button.Location = new Point(startPosX + 200, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                // Remove the TextBox and Button
                Controls.Remove(textBox);
                Controls.Remove(button);

                // Move up all controls that are below the removed one
                foreach (Control control in Controls)
                {
                    if (control.Location.Y > textBox.Location.Y)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y - 30);
                    }
                }

                // Decrement the count
                countQ--;
                countC--;
                countI--;
            };

            // Move down all existing controls that are below the new one
            foreach (Control control in Controls)
            {
                if (control.Location.Y >= textBox.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            // Add the new controls to the form
            Controls.Add(textBox);
            Controls.Add(button);

            // Increment the count
            countQ++;
            countC++;
            countI++;
        }


        private void btnAddCharge_Click(object sender, EventArgs e)
        {
            int startPosX = 18;
            int startPosY = 314;

            // Create new TextBox and Button
            TextBox textBox = new TextBox();
            Button button = new Button();

            // Set properties
            textBox.Name = "textBox" + countC;
            textBox.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            button.Text = "X";
            button.Name = "button" + countC;
            button.Location = new Point(startPosX + 200, this.AutoScrollPosition.Y + (30 * countC) + startPosY);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                // Remove the TextBox and Button
                Controls.Remove(textBox);
                Controls.Remove(button);

                // Move up all controls that are below the removed one
                foreach (Control control in Controls)
                {
                    if (control.Location.Y > textBox.Location.Y)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y - 30);
                    }
                }

                // Decrement the count
                countC--;
                countI--;
            };

            // Move down all existing controls that are below the new one
            foreach (Control control in Controls)
            {
                if (control.Location.Y >= textBox.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            // Add the new controls to the form
            Controls.Add(textBox);
            Controls.Add(button);

            // Increment the count
            countC++;
            countI++;
        }

        private void btnAddInv_Click(object sender, EventArgs e)
        {
            int startPosX = 18;
            int startPosY = 426;

            // Create new TextBox and Button
            TextBox textBox = new TextBox();
            Button button = new Button();

            // Set properties
            textBox.Name = "textBox" + countI;
            textBox.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            button.Text = "X";
            button.Name = "button" + countI;
            button.Location = new Point(startPosX + 200, this.AutoScrollPosition.Y + (30 * countI) + startPosY);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                // Remove the TextBox and Button
                Controls.Remove(textBox);
                Controls.Remove(button);

                // Move up all controls that are below the removed one
                foreach (Control control in Controls)
                {
                    if (control.Location.Y > textBox.Location.Y)
                    {
                        control.Location = new Point(control.Location.X, control.Location.Y - 30);
                    }
                }

                // Decrement the count
                countI--;
            };

            // Move down all existing controls that are below the new one
            foreach (Control control in Controls)
            {
                if (control.Location.Y >= textBox.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            // Add the new controls to the form
            Controls.Add(textBox);
            Controls.Add(button);

            // Increment the count
            countI++;
        }
    }
}

//"SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName AND CustomerAddress = @customerAddress";
