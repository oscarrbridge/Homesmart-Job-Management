using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Homesmart_Job_Management;

namespace Homesmart_Job_Management
{
    public partial class frmEditEntry : Form
    {
        private int uJobID;

        private int countQ = 0;
        private int countC = 0;
        private int countI = 0;

        private int countQL = 0;
        private int countCL = 0;
        private int countIL = 0;

        Dictionary<string, Control> QuoteControls = new Dictionary<string, Control>();
        Dictionary<string, Control> ChargeControls = new Dictionary<string, Control>();
        Dictionary<string, Control> InvoiceControls = new Dictionary<string, Control>();

        public frmEditEntry(int JobID)
        {
            uJobID = JobID;

            InitializeComponent();
            getInfo(JobID);
        }

        private void getInfo(int JobID)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            if (dbConnection.OpenConnection() == true)
            {
                int Quote = 0;
                int Charge = 0;
                int Invoice = 0;

                string QuoteQuery = $"SELECT SupplierContractor, QuoteDate, uReference, QuoteValue FROM ExpenseQuote WHERE JobID = @JobID";
                string ChargeQuery = $"SELECT Company, SupplierContractor, uValue FROM InternalCharge WHERE JobID = @JobID";
                string InvoiceQuery = $"SELECT SupplierContractor, InvoiceDate, uReference, InvoiceNo, uValue FROM ExpenseInvoice WHERE JobID = @JobID";

                string QuoteCount = $"SELECT COUNT(*) FROM ExpenseQuote WHERE JobID = @JobID";
                string ChargeCount = $"SELECT COUNT(*) FROM InternalCharge WHERE JobID = @JobID";
                string InvoiceCount = $"SELECT COUNT(*) FROM ExpenseInvoice WHERE JobID = @JobID";

                MySqlCommand QuoteCountCmd = new MySqlCommand(QuoteCount, dbConnection.GetConnection());
                MySqlCommand ChargeCountCmd = new MySqlCommand(ChargeCount, dbConnection.GetConnection());
                MySqlCommand InvoiceCountCmd = new MySqlCommand(InvoiceCount, dbConnection.GetConnection());

                QuoteCountCmd.Parameters.AddWithValue("@JobID", JobID);
                ChargeCountCmd.Parameters.AddWithValue("@JobID", JobID);
                InvoiceCountCmd.Parameters.AddWithValue("@JobID", JobID);

                MySqlDataReader reader = QuoteCountCmd.ExecuteReader();
                while (reader.Read())
                {
                    Quote = reader.GetInt32(0);
                }
                reader.Close();

                reader = ChargeCountCmd.ExecuteReader();
                while (reader.Read())
                {
                    Charge = reader.GetInt32(0);
                }
                reader.Close();

                reader = InvoiceCountCmd.ExecuteReader();
                while (reader.Read())
                {
                    Invoice = reader.GetInt32(0);
                }
                reader.Close();

                // Execute the QuoteQuery and use the result to set the value of the field
                MySqlCommand QuoteQueryCmd = new MySqlCommand(QuoteQuery, dbConnection.GetConnection());
                QuoteQueryCmd.Parameters.AddWithValue("@JobID", JobID);

                reader = QuoteQueryCmd.ExecuteReader();
                for (int i = 0; i < Quote; i++)
                {
                    if (reader.Read())
                    {
                        AddQuote();

                        (QuoteControls["QSupplier" + $"{i}"] as TextBox).Text = reader["SupplierContractor"].ToString();
                        try
                        {
                            (QuoteControls["QDate" + $"{i}"] as DateTimePicker).Value = DateTime.Parse(reader["QuoteDate"].ToString());
                        }
                        catch { }
                        (QuoteControls["QReference" + $"{i}"] as TextBox).Text = reader["uReference"].ToString();
                        (QuoteControls["QValue" + $"{i}"] as TextBox).Text = reader["QuoteValue"].ToString();
                    }
                }
                reader.Close();

                MySqlCommand ChargeQueryCmd = new MySqlCommand(ChargeQuery, dbConnection.GetConnection());
                ChargeQueryCmd.Parameters.AddWithValue("@JobID", JobID);

                reader = ChargeQueryCmd.ExecuteReader();
                for (int i = 0; i < Charge; i++)
                {
                    if (reader.Read())
                    {
                        AddCharge();

                        (ChargeControls["CCompany" + $"{i}"] as TextBox).Text = reader["Company"].ToString();
                        (ChargeControls["CSupplier" + $"{i}"] as TextBox).Text = reader["SupplierContractor"].ToString();
                        (ChargeControls["CValue" + $"{i}"] as TextBox).Text = reader["uValue"].ToString();
                    }
                }
                reader.Close();


                MySqlCommand InvoiceQueryCmd = new MySqlCommand(InvoiceQuery, dbConnection.GetConnection());
                InvoiceQueryCmd.Parameters.AddWithValue("@JobID", JobID);

                reader = InvoiceQueryCmd.ExecuteReader();
                for (int i = 0; i < Invoice; i++)
                {
                    if (reader.Read())
                    {
                        AddInv();

                        (InvoiceControls["ISupplier" + $"{i}"] as TextBox).Text = reader["SupplierContractor"].ToString();
                        try
                        {
                            (InvoiceControls["IDate" + $"{i}"] as DateTimePicker).Value = DateTime.Parse(reader["InvoiceDate"].ToString());
                        }
                        catch (Exception ex){}
                        (InvoiceControls["IReference" + $"{i}"] as TextBox).Text = reader["uReference"].ToString();
                        (InvoiceControls["IInvNumber" + $"{i}"] as TextBox).Text = reader["InvoiceNo"].ToString();
                        (InvoiceControls["IValue"     + $"{i}"] as TextBox).Text = reader["uValue"].ToString();
                    }
                }
                reader.Close();

                string JobQuery = $"SELECT CustomerName, CustomerAddress, QuoteValue, TotalCost, Profit, Margin FROM Job WHERE JobID = @JobID";

                MySqlCommand JobQueryCmd = new MySqlCommand(JobQuery, dbConnection.GetConnection());

                JobQueryCmd.Parameters.AddWithValue("@JobID", JobID);

                reader = JobQueryCmd.ExecuteReader();

                while (reader.Read())
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Please double check the information before submiting",
                "Confirmation", MessageBoxButtons.OKCancel);


            if (dialogResult == DialogResult.OK)
            {
                int profit = 0;
                int margin = 0;


                DatabaseConnection dbConnection = new DatabaseConnection();

                if (dbConnection.OpenConnection() == true)
                {
                    string Query = "UPDATE Job SET CustomerName = @CustomerName, CustomerAddress = @CustomerAddress, QuoteValue = @QuoteValue, TotalCost = @TotalCost, Profit = @Profit, Margin = @Margin WHERE JobID = @JobID";
                    MySqlCommand cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                    cmd.Parameters.AddWithValue("@CustomerName", boxCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CustomerAddress", boxCustomerAddress.Text);
                    cmd.Parameters.AddWithValue("@QuoteValue", boxQuoteValue.Text);
                    cmd.Parameters.AddWithValue("@TotalCost", boxTotalCost.Text);
                    cmd.Parameters.AddWithValue("@Profit", profit);
                    cmd.Parameters.AddWithValue("@Margin", margin);
                    cmd.Parameters.AddWithValue("@JobID", uJobID);

                    cmd.ExecuteNonQuery();


                    for(int i = 0; i <= QuoteControls.Count / 4; i++)
                    {
                        Query = "UPDATE ExpenseQuote SET SupplierContractor = @SupplierContractor, QuoteDate = @QuoteDate, uReference = @uReference, QuoteValue = @QuoteValue WHERE JobID = @JobID";
                        cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                        cmd.Parameters.AddWithValue("@SupplierContractor", (QuoteControls["QSupplier" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@QuoteDate", (QuoteControls["QDate" + $"{i}"] as DateTimePicker).Value);
                        cmd.Parameters.AddWithValue("@uReference", (QuoteControls["QReference" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@QuoteValue", (QuoteControls["QValue" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@JobID", uJobID);
                    }

                    cmd.ExecuteNonQuery();



                    dbConnection.CloseConnection();
                    Close();
                }
            }
        }

        private void AddQuote()
        {
            int startPosX = 18;
            int startPosY = 183;

            // Create new TextBox and Button
            TextBox QSupplier = new TextBox();
            DateTimePicker QDate = new DateTimePicker();
            TextBox QReference = new TextBox();
            TextBox QValue = new TextBox();
            Button button = new Button();

            // Set properties
            QSupplier.Name = "QSupplier" + countQL;
            QSupplier.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QSupplier.Size = new Size(170, 20);

            QDate.Name = "QDate" + countQL;
            QDate.Location = new Point(startPosX + 177, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QDate.Size = new Size(80, 20);
            QDate.Format = DateTimePickerFormat.Short;

            QReference.Name = "QReference" + countQL;
            QReference.Location = new Point(startPosX + 267, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QReference.Size = new Size(170, 20);

            QValue.Name = "QValue" + countQL;
            QValue.Location = new Point(startPosX + 627, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QValue.Size = new Size(80, 20);

            button.Text = "X";
            button.Name = "button" + countQL;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            button.Size = new Size(20, 20);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                DialogResult result = MessageBox.Show("Do you want to remove this item?", "Confirmation", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
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

                    countQL--;
                }
            };

            // Move down all existing controls that are below the new one
            foreach (Control control in Controls)
            {
                if (control.Location.Y >= QSupplier.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            QuoteControls.Add(QSupplier.Name, QSupplier);
            QuoteControls.Add(QDate.Name, QDate);
            QuoteControls.Add(QReference.Name, QReference);
            QuoteControls.Add(QValue.Name, QValue);

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

            countQL++;
        }

        private void AddCharge()
        {
            int startPosX = 18;
            int startPosY = 281;

            // Create new TextBox and Button
            TextBox CCompany = new TextBox();
            TextBox CSupplier = new TextBox();
            TextBox CValue = new TextBox();
            Button button = new Button();

            // Set properties
            CCompany.Name = "CCompany" + countCL;
            CCompany.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CCompany.Size = new Size(170, 20);

            CSupplier.Name = "CSupplier" + countCL;
            CSupplier.Location = new Point(startPosX + 177, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CSupplier.Size = new Size(170, 20);

            CValue.Name = "CValue" + countCL;
            CValue.Location = new Point(startPosX + 627, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CValue.Size = new Size(80, 20);

            button.Text = "X";
            button.Name = "button" + countCL;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            button.Size = new Size(20, 20);


            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                DialogResult result = MessageBox.Show("Do you want to remove this item?", "Confirmation", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
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

                    countCL--;
                }
            };

            // Move down all existing controls that are below the new one
            foreach (Control control in Controls)
            {
                if (control.Location.Y >= CCompany.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            ChargeControls.Add(CCompany.Name, CCompany);
            ChargeControls.Add(CSupplier.Name, CSupplier);
            ChargeControls.Add(CValue.Name, CValue);

            // Add the new controls to the form
            Controls.Add(CCompany);
            Controls.Add(CSupplier);
            Controls.Add(CValue);

            Controls.Add(button);

            // Increment the count
            countC++;
            countI++;

            countCL++;
        }

        private void AddInv()
        {
            int startPosX = 18;
            int startPosY = 393;

            // Create new TextBox and Button
            TextBox ISupplier = new TextBox();
            DateTimePicker IDate = new DateTimePicker();
            TextBox IReference = new TextBox();
            TextBox IInvNumber = new TextBox();
            TextBox IValue = new TextBox();
            Button button = new Button();

            // Set properties
            ISupplier.Name = "ISupplier" + countIL;
            ISupplier.Location = new Point(startPosX, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            ISupplier.Size = new Size(170, 20);

            IDate.Name = "IDate" + countIL;
            IDate.Location = new Point(startPosX + 177, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IDate.Size = new Size(80, 20);
            IDate.Format = DateTimePickerFormat.Short;

            IReference.Name = "IReference" + countIL;
            IReference.Location = new Point(startPosX + 267, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IReference.Size = new Size(170, 20);

            IInvNumber.Name = "IInvNumber" + countIL;
            IInvNumber.Location = new Point(startPosX + 443, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IInvNumber.Size = new Size(170, 20);

            IValue.Name = "IValue" + countIL;
            IValue.Location = new Point(startPosX + 627, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IValue.Size = new Size(80, 20);

            button.Text = "X";
            button.Name = "button" + countIL;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            button.Size = new Size(20, 20);

            // Add Click event to the Button
            button.Click += (s, ev) =>
            {
                DialogResult result = MessageBox.Show("Do you want to remove this item?", "Confirmation", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
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

                    countIL--;
                }
            };

            // Move down all existing controls that are below the new one
            foreach (Control control in Controls)
            {
                if (control.Location.Y >= ISupplier.Location.Y)
                {
                    control.Location = new Point(control.Location.X, control.Location.Y + 30);
                }
            }

            InvoiceControls.Add(ISupplier.Name, ISupplier);
            InvoiceControls.Add(IDate.Name, IDate);
            InvoiceControls.Add(IReference.Name, IReference);
            InvoiceControls.Add(IInvNumber.Name, IInvNumber);
            InvoiceControls.Add(IValue.Name, IValue);

            // Add the new controls to the form
            Controls.Add(ISupplier);
            Controls.Add(IDate);
            Controls.Add(IReference);
            Controls.Add(IInvNumber);
            Controls.Add(IValue);

            Controls.Add(button);

            // Increment the count
            countI++;

            countIL++;
        }

        private void btnAddQuote_Click(object sender, EventArgs e)
        {
            AddQuote();
        }

        private void btnAddCharge_Click(object sender, EventArgs e)
        {
            AddCharge();
        }

        private void btnAddInv_Click(object sender, EventArgs e)
        {
            AddInv();
        }
    }
}

//"SELECT JobID CustomerName, CustomerAddress FROM Job WHERE CustomerName = @customerName AND CustomerAddress = @customerAddress";
