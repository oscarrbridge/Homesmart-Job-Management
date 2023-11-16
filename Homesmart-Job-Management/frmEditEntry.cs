using Connections;
using Google.Protobuf.WellKnownTypes;
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
            TextBox QSupplier = new TextBox();
            DateTimePicker QDate= new DateTimePicker();
            TextBox QReference = new TextBox();
            TextBox QValue = new TextBox();
            Button button = new Button();

            // Set properties
            QSupplier.Name = "QSupplier" + countQ;
            QSupplier.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QSupplier.Size = new Size(170, 20);

            QDate.Name = "QDate" + countQ;
            QDate.Location = new Point(startPosX + 177, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QDate.Size = new Size(80, 20);
            QDate.Format = DateTimePickerFormat.Short;

            QReference.Name = "QReference" + countQ;
            QReference.Location = new Point(startPosX + 267, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QReference.Size = new Size(170, 20);

            QValue.Name = "QValue" + countQ;
            QValue.Location = new Point(startPosX + 627, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QValue.Size = new Size(80, 20);

            button.Text = "X";
            button.Name = "button" + countQ;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            button.Size = new Size(20, 20);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                // Remove the TextBox and Button
                Controls.Remove(QSupplier);
                Controls.Remove(QDate);
                Controls.Remove(QReference);
                Controls.Remove(QValue);
                Controls.Remove(button);

                // Move up all controls that are below the removed one
                foreach (Control control in Controls)
                {
                    if (control.Location.Y > QSupplier.Location.Y)
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
                if (control.Location.Y >= QSupplier.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            // Add the new controls to the form
            Controls.Add(QSupplier);
            Controls.Add(QDate);
            Controls.Add(QReference);
            Controls.Add(QValue);

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
            TextBox CCompany = new TextBox();
            TextBox CSupplier = new TextBox();
            TextBox CValue = new TextBox();
            Button button = new Button();

            // Set properties
            CCompany.Name = "CCompany" + countC;
            CCompany.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CCompany.Size = new Size(170, 20);

            CSupplier.Name = "CSupplier" + countC;
            CSupplier.Location = new Point(startPosX + 177, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CSupplier.Size = new Size(170, 20);

            CValue.Name = "CValue" + countC;
            CValue.Location = new Point(startPosX + 627, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CValue.Size = new Size(80, 20);

            button.Text = "X";
            button.Name = "button" + countC;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            button.Size = new Size(20, 20);


            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                // Remove the TextBox and Button
                Controls.Remove(CCompany);
                Controls.Remove(CSupplier);
                Controls.Remove(CValue);

                Controls.Remove(button);

                // Move up all controls that are below the removed one
                foreach (Control control in Controls)
                {
                    if (control.Location.Y > CCompany.Location.Y)
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
                if (control.Location.Y >= CCompany.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            // Add the new controls to the form
            Controls.Add(CCompany);
            Controls.Add(CSupplier);
            Controls.Add(CValue);

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
            TextBox ISupplier = new TextBox();
            DateTimePicker IDate = new DateTimePicker();
            TextBox IReference = new TextBox();
            TextBox IInvNumber = new TextBox();
            TextBox IValue = new TextBox();
            Button button = new Button();

            // Set properties
            ISupplier.Name = "ISupplier" + countI;
            ISupplier.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            ISupplier.Size = new Size(170, 20);

            IDate.Name = "QDate" + countI;
            IDate.Location = new Point(startPosX + 177, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IDate.Size = new Size(80, 20);
            IDate.Format = DateTimePickerFormat.Short;

            IReference.Name = "QReference" + countI;
            IReference.Location = new Point(startPosX + 267, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IReference.Size = new Size(170, 20);

            IInvNumber.Name = "QReference" + countI;
            IInvNumber.Location = new Point(startPosX + 443, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IInvNumber.Size = new Size(170, 20);

            IValue.Name = "QValue" + countI;
            IValue.Location = new Point(startPosX + 627, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IValue.Size = new Size(80, 20);

            button.Text = "X";
            button.Name = "button" + countI;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            button.Size = new Size(20, 20);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                // Remove the TextBox and Button
                Controls.Remove(ISupplier);
                Controls.Remove(IDate);
                Controls.Remove(IReference);
                Controls.Remove(IInvNumber);
                Controls.Remove(IValue);

                Controls.Remove(button);

                // Move up all controls that are below the removed one
                foreach (Control control in Controls)
                {
                    if (control.Location.Y > ISupplier.Location.Y)
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
                if (control.Location.Y >= ISupplier.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            // Add the new controls to the form
            Controls.Add(ISupplier);
            Controls.Add(IDate);
            Controls.Add(IReference);
            Controls.Add(IInvNumber);
            Controls.Add(IValue);

            Controls.Add(button);

            // Increment the count
            countI++;
        }
    }
}

//"SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName AND CustomerAddress = @customerAddress";
