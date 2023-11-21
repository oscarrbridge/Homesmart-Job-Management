using Connections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Homesmart_Job_Management
{
    public partial class frmEditEntry : Form
    {
        private int uJobID;

        private int init = 0;

        private int countQ = 0;
        private int countC = 0;
        private int countI = 0;

        private int countQL = 0;
        private int countCL = 0;
        private int countIL = 0;

        Dictionary<string, Control> QuoteControls = new Dictionary<string, Control>();
        Dictionary<string, Control> OrigQuoteControls = new Dictionary<string, Control>();
        Dictionary<string, Control> RemQuoteControls = new Dictionary<string, Control>();
        Dictionary<string, Control> NewQuoteControls = new Dictionary<string, Control>();

        Dictionary<string, Control> ChargeControls = new Dictionary<string, Control>();
        Dictionary<string, Control> OrigChargeControls = new Dictionary<string, Control>();
        Dictionary<string, Control> RemChargeControls = new Dictionary<string, Control>();
        Dictionary<string, Control> NewChargeControls = new Dictionary<string, Control>();

        Dictionary<string, Control> InvoiceControls = new Dictionary<string, Control>();
        Dictionary<string, Control> OrigInvoiceControls = new Dictionary<string, Control>();
        Dictionary<string, Control> RemInvoiceControls = new Dictionary<string, Control>();
        Dictionary<string, Control> NewInvoiceControls = new Dictionary<string, Control>();

        public frmEditEntry(int JobID)
        {
            uJobID = JobID;

            InitializeComponent();
            getInfo(JobID);
        }

        private void IValue_TextChanged(object sender, EventArgs e)
        {
            decimal TotalCost = 0;

            for (int i = 0; i < InvoiceControls.Count / 6; i++)
            {
                TotalCost += (InvoiceControls["IValue" + $"{i}"] as NumericUpDown).Value;
            }
            boxTotalCost.Value = TotalCost;

            // Calculate Profit
            decimal QuoteValue = boxQuoteValue.Value;
            decimal Profit = QuoteValue - TotalCost;
            boxProfit.Value = Profit;

            // Calculate Margin
            if (QuoteValue != 0)
            {
                decimal Margin = Profit / QuoteValue * 100;
                boxMargin.Value = Margin;
            }
            else
            {
                boxMargin.Value = 0;
            }
        }

        private void copyDict()
        {
            OrigQuoteControls = new Dictionary<string, Control>(QuoteControls);
            OrigChargeControls = new Dictionary<string, Control>(ChargeControls);
            OrigInvoiceControls = new Dictionary<string, Control>(InvoiceControls);
        }

        private void getInfo(int JobID)
        {
            DatabaseConnection dbConnection = new DatabaseConnection();

            if (dbConnection.OpenConnection() == true)
            {
                int Quote = 0;
                int Charge = 0;
                int Invoice = 0;

                string QuoteQuery = $"SELECT SupplierContractor, QuoteDate, uReference, QuoteValue, QuoteID FROM ExpenseQuote WHERE JobID = @JobID";
                string ChargeQuery = $"SELECT Company, SupplierContractor, uValue, ChargeID FROM InternalCharge WHERE JobID = @JobID";
                string InvoiceQuery = $"SELECT SupplierContractor, InvoiceDate, uReference, InvoiceNo, uValue, InvoiceID FROM ExpenseInvoice WHERE JobID = @JobID";

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
                        (QuoteControls["QValue" + $"{i}"] as NumericUpDown).Text = reader["QuoteValue"].ToString();
                        (QuoteControls["QID" + $"{i}"] as TextBox).Text = reader["QuoteID"].ToString();
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
                        (ChargeControls["CValue" + $"{i}"] as NumericUpDown).Text = reader["uValue"].ToString();
                        (ChargeControls["CID" + $"{i}"] as TextBox).Text = reader["ChargeID"].ToString();
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
                        catch (Exception ex) { }
                        (InvoiceControls["IReference" + $"{i}"] as TextBox).Text = reader["uReference"].ToString();
                        (InvoiceControls["IInvNumber" + $"{i}"] as TextBox).Text = reader["InvoiceNo"].ToString();
                        (InvoiceControls["IValue" + $"{i}"] as NumericUpDown).Text = reader["uValue"].ToString();
                        (InvoiceControls["IID" + $"{i}"] as TextBox).Text = reader["InvoiceID"].ToString();
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

            IValue_TextChanged(this, null);
            copyDict();

            init = 1;
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
                    string Query = "UPDATE Job " +
                                    "SET CustomerName = @CustomerName, CustomerAddress = @CustomerAddress, QuoteValue = @QuoteValue, TotalCost = @TotalCost, Profit = @Profit, Margin = @Margin " +
                                    "WHERE JobID = @JobID";
                    MySqlCommand cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                    cmd.Parameters.AddWithValue("@CustomerName", boxCustomerName.Text);
                    cmd.Parameters.AddWithValue("@CustomerAddress", boxCustomerAddress.Text);
                    cmd.Parameters.AddWithValue("@QuoteValue", boxQuoteValue.Text);
                    cmd.Parameters.AddWithValue("@TotalCost", boxTotalCost.Text);
                    cmd.Parameters.AddWithValue("@Profit", profit);
                    cmd.Parameters.AddWithValue("@Margin", margin);
                    cmd.Parameters.AddWithValue("@JobID", uJobID);

                    cmd.ExecuteNonQuery();

                    for (int i = 0; i < QuoteControls.Count / 5; i++)
                    {
                        Query = "UPDATE ExpenseQuote " +
                                "SET SupplierContractor = @SupplierContractor, QuoteDate = @QuoteDate, uReference = @uReference, QuoteValue = @QuoteValue " +
                                "WHERE QuoteID = @QuoteID";
                        cmd = new MySqlCommand(Query, dbConnection.GetConnection()); //remove from the global control dict

                        cmd.Parameters.AddWithValue("@SupplierContractor", (QuoteControls["QSupplier" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@QuoteDate", (QuoteControls["QDate" + $"{i}"] as DateTimePicker).Value);
                        cmd.Parameters.AddWithValue("@uReference", (QuoteControls["QReference" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@QuoteValue", (QuoteControls["QValue" + $"{i}"] as NumericUpDown).Text);
                        cmd.Parameters.AddWithValue("@QuoteID", (QuoteControls["QID" + $"{i}"] as TextBox).Text);

                        cmd.ExecuteNonQuery();
                    }
                    int k = 0;
                    foreach (Control control in NewQuoteControls.Values)
                    {
                        if (k % 5 == 0) // Start of a new group of controls
                        {
                            Query = "INSERT INTO ExpenseQuote (SupplierContractor, QuoteDate, uReference, QuoteValue, JobID) " +
                                    "VALUES (@SupplierContractor, @QuoteDate, @uReference, @QuoteValue, @JobID)";

                            cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                            cmd.Parameters.AddWithValue("@JobID", uJobID);
                        }

                        switch (k % 5)
                        {
                            case 0: // SupplierContractor
                                cmd.Parameters.AddWithValue("@SupplierContractor", (control as TextBox).Text);
                                break;
                            case 1: // QuoteDate
                                cmd.Parameters.AddWithValue("@QuoteDate", (control as DateTimePicker).Value);
                                break;
                            case 2: // uReference
                                cmd.Parameters.AddWithValue("@uReference", (control as TextBox).Text);
                                break;
                            case 3: // QuoteValue
                                cmd.Parameters.AddWithValue("@QuoteValue", (control as NumericUpDown).Text);
                                break;
                            case 4: // QuoteID
                                cmd.Parameters.AddWithValue("@QuoteID", (control as TextBox).Text);
                                cmd.ExecuteNonQuery(); // Execute the query after the last control of the group
                                break;
                        }

                        k++;
                    }


                    for (int i = 0; i < ChargeControls.Count / 4; i++)
                    {
                        Query = "UPDATE InternalCharge " +
                                "SET Company = @Company, SupplierContractor = @SupplierContractor, uValue = @uValue " +
                                "WHERE ChargeID = @ChargeID";
                        cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                        cmd.Parameters.AddWithValue("@Company", (ChargeControls["CCompany" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@SupplierContractor", (ChargeControls["CSupplier" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@uValue", (ChargeControls["CValue" + $"{i}"] as NumericUpDown).Text);
                        cmd.Parameters.AddWithValue("@ChargeID", (ChargeControls["CID" + $"{i}"] as TextBox).Text);

                        cmd.ExecuteNonQuery();
                    }
                    k = 0;
                    foreach (Control control in NewChargeControls.Values)
                    {
                        if (k % 4 == 0) // Start of a new group of controls
                        {
                            Query = "INSERT INTO InternalCharge (Company, SupplierContractor, uValue, JobID) " +
                                    "VALUES (@Company, @SupplierContractor, @uValue, @JobID)";

                            cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                            cmd.Parameters.AddWithValue("@JobID", uJobID);
                        }

                        switch (k % 4)
                        {
                            case 0: // Company
                                cmd.Parameters.AddWithValue("@Company", (control as TextBox).Text);
                                break;
                            case 1: // SupplierContractor
                                cmd.Parameters.AddWithValue("@SupplierContractor", (control as TextBox).Text); // Adjusted control type
                                break;
                            case 2: // uValue
                                cmd.Parameters.AddWithValue("@uValue", (control as NumericUpDown).Text); // Adjusted control type
                                break;
                            case 3: // Execute the query after the last control of the group
                                cmd.ExecuteNonQuery();
                                break;
                        }

                        k++;
                    }


                    for (int i = 0; i < InvoiceControls.Count / 6; i++)
                    {
                        Query = "UPDATE ExpenseInvoice " +
                                "SET SupplierContractor = @SupplierContractor, InvoiceDate = @InvoiceDate, uReference = @uReference, InvoiceNo = @InvoiceNo, uValue = @uValue " +
                                "WHERE InvoiceID = @InvoiceID";
                        cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                        cmd.Parameters.AddWithValue("@SupplierContractor", (InvoiceControls["ISupplier" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@InvoiceDate", (InvoiceControls["IDate" + $"{i}"] as DateTimePicker).Value);
                        cmd.Parameters.AddWithValue("@uReference", (InvoiceControls["IReference" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@InvoiceNo", (InvoiceControls["IInvNumber" + $"{i}"] as TextBox).Text);
                        cmd.Parameters.AddWithValue("@uValue", (InvoiceControls["IValue" + $"{i}"] as NumericUpDown).Text);
                        cmd.Parameters.AddWithValue("@InvoiceID", (InvoiceControls["IID" + $"{i}"] as TextBox).Text);

                        cmd.ExecuteNonQuery();
                    }
                    k = 0;
                    foreach (Control control in NewInvoiceControls.Values)
                    {
                        if (k % 6 == 0) // Start of a new group of controls
                        {
                            Query = "INSERT INTO ExpenseInvoice (SupplierContractor, InvoiceDate, uReference, InvoiceNo, uValue, JobID) " +
                                    "VALUES (@SupplierContractor, @InvoiceDate, @uReference, @InvoiceNo, @uValue, @JobID)";
                            
                            cmd = new MySqlCommand(Query, dbConnection.GetConnection());
                    
                            cmd.Parameters.AddWithValue("@JobID", uJobID);
                        }
                    
                        switch (k % 6)
                        {
                            case 0: // SupplierContractor
                                cmd.Parameters.AddWithValue("@SupplierContractor", (control as TextBox).Text);
                                break;
                            case 1: // InvoiceDate
                                cmd.Parameters.AddWithValue("@InvoiceDate", (control as DateTimePicker).Value);
                                break;
                            case 2: // uReference
                                cmd.Parameters.AddWithValue("@uReference", (control as TextBox).Text);
                                break;
                            case 3: // InvoiceNo
                                cmd.Parameters.AddWithValue("@InvoiceNo", (control as TextBox).Text);
                                break;
                            case 4: // uValue
                                cmd.Parameters.AddWithValue("@uValue", (control as NumericUpDown).Text);
                                break;
                            case 5:
                                cmd.ExecuteNonQuery(); // Execute the query after the last control of the group
                                break;
                        }
                    
                        k++;
                    }

                    if (RemQuoteControls.Count > 0)
                    {
                        foreach (KeyValuePair<string, Control> item in RemQuoteControls)
                        {
                            Query = "DELETE FROM ExpenseQuote WHERE QuoteID = @QuoteID;";

                            cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                            cmd.Parameters.AddWithValue("@QuoteID", ((TextBox)item.Value).Text);
                            cmd.ExecuteNonQuery();

                        }
                    }

                    if (RemChargeControls.Count > 0)
                    {
                        foreach (KeyValuePair<string, Control> item in RemChargeControls)
                        {
                            Query = "DELETE FROM InternalCharge WHERE ChargeID = @ChargeID;";

                            cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                            cmd.Parameters.AddWithValue("@ChargeID", ((TextBox)item.Value).Text);
                            cmd.ExecuteNonQuery();

                        }
                    }

                    if (RemInvoiceControls.Count > 0)
                    {
                        foreach (KeyValuePair<string, Control> item in RemInvoiceControls)
                        {
                            Query = "DELETE FROM ExpenseInvoice WHERE InvoiceId = @InvoiceId;";

                            cmd = new MySqlCommand(Query, dbConnection.GetConnection());

                            cmd.Parameters.AddWithValue("@InvoiceId", ((TextBox)item.Value).Text);
                            cmd.ExecuteNonQuery();

                        }
                    }

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
            NumericUpDown QValue = new NumericUpDown();
            Button button = new Button();
            TextBox QID = new TextBox();


            //Set properties
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
            QValue.Maximum = 1000000;
            QValue.ThousandsSeparator = true;

            button.Text = "X";
            button.Name = "button" + countQL;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            button.Size = new Size(20, 20);

            QID.Name = "QID" + countQL;
            QID.Location = new Point(startPosX + 743, this.AutoScrollPosition.Y + (30 * countQ) + startPosY);
            QID.Size = new Size(30, 20);
            QID.Visible = false;

            // Add Click event to the remove Button
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
                    Controls.Remove(QID);

                    if (QuoteControls.ContainsKey(QID.Name))
                    {
                        QuoteControls.Remove(QSupplier.Name);
                        QuoteControls.Remove(QDate.Name);
                        QuoteControls.Remove(QReference.Name);
                        QuoteControls.Remove(QValue.Name);
                        QuoteControls.Remove(QID.Name);
                    }
                    else if (NewQuoteControls.ContainsKey(QID.Name))
                    {
                        NewQuoteControls.Remove(QSupplier.Name);
                        NewQuoteControls.Remove(QDate.Name);
                        NewQuoteControls.Remove(QReference.Name);
                        NewQuoteControls.Remove(QValue.Name);
                        NewQuoteControls.Remove(QID.Name);
                    }

                    if (OrigQuoteControls.ContainsKey(QID.Name))
                    {
                        RemQuoteControls.Add(QID.Name, QID);
                    }

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

            //Add the new control to the dict
            if (init == 0)
            {
                QuoteControls.Add(QSupplier.Name, QSupplier);
                QuoteControls.Add(QDate.Name, QDate);
                QuoteControls.Add(QReference.Name, QReference);
                QuoteControls.Add(QValue.Name, QValue);
                QuoteControls.Add(QID.Name, QID);
            }
            else if (init == 1)
            {
                NewQuoteControls.Add(QSupplier.Name, QSupplier);
                NewQuoteControls.Add(QDate.Name, QDate);
                NewQuoteControls.Add(QReference.Name, QReference);
                NewQuoteControls.Add(QValue.Name, QValue);
                NewQuoteControls.Add(QID.Name, QID);
            }


            // Add the new controls to the form
            Controls.Add(QSupplier);
            Controls.Add(QDate);
            Controls.Add(QReference);
            Controls.Add(QValue);
            Controls.Add(button);
            Controls.Add(QID);

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
            NumericUpDown CValue = new NumericUpDown();
            Button button = new Button();
            TextBox CID = new TextBox();

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
            CValue.Maximum = 1000000;
            CValue.ThousandsSeparator = true;

            button.Text = "X";
            button.Name = "button" + countCL;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            button.Size = new Size(20, 20);

            CID.Name = "CID" + countCL;
            CID.Location = new Point(startPosX + 743, this.AutoScrollPosition.Y + (30 * countC) + startPosY);
            CID.Size = new Size(30, 20);
            CID.Visible = false;

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
                    Controls.Remove(CID);

                    if (ChargeControls.ContainsKey(CID.Name))
                    {
                        ChargeControls.Remove(CCompany.Name);
                        ChargeControls.Remove(CSupplier.Name);
                        ChargeControls.Remove(CValue.Name);
                        ChargeControls.Remove(CID.Name);
                    }
                    else if (NewChargeControls.ContainsKey(CID.Name))
                    {
                        NewChargeControls.Remove(CCompany.Name);
                        NewChargeControls.Remove(CSupplier.Name);
                        NewChargeControls.Remove(CValue.Name);
                        NewChargeControls.Remove(CID.Name);
                    }

                    if (OrigChargeControls.ContainsKey(CID.Name))
                    {
                        RemChargeControls.Add(CID.Name, CID);
                    }

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

            if (init == 0)
            {
                ChargeControls.Add(CCompany.Name, CCompany);
                ChargeControls.Add(CSupplier.Name, CSupplier);
                ChargeControls.Add(CValue.Name, CValue);
                ChargeControls.Add(CID.Name, CID);
            }
            else if (init == 1)
            {
                NewChargeControls.Add(CCompany.Name, CCompany);
                NewChargeControls.Add(CSupplier.Name, CSupplier);
                NewChargeControls.Add(CValue.Name, CValue);
                NewChargeControls.Add(CID.Name, CID);
            }

            // Add the new controls to the form
            Controls.Add(CCompany);
            Controls.Add(CSupplier);
            Controls.Add(CValue);
            Controls.Add(button);
            Controls.Add(CID);

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
            NumericUpDown IValue = new NumericUpDown();
            Button button = new Button();
            TextBox IID = new TextBox();

            IValue.TextChanged += new EventHandler(IValue_TextChanged);

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
            IValue.Maximum = 1000000;
            IValue.ThousandsSeparator = true;

            button.Text = "X";
            button.Name = "button" + countIL;
            button.Location = new Point(startPosX + 713, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            button.Size = new Size(20, 20);

            IID.Name = "IID" + countIL;
            IID.Location = new Point(startPosX + 743, this.AutoScrollPosition.Y + (30 * countI) + startPosY);
            IID.Size = new Size(30, 20);
            IID.Visible = false;

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
                    Controls.Remove(IID);

                    if (InvoiceControls.ContainsKey(IID.Name))
                    {
                        InvoiceControls.Remove(ISupplier.Name);
                        InvoiceControls.Remove(IDate.Name);
                        InvoiceControls.Remove(IReference.Name);
                        InvoiceControls.Remove(IInvNumber.Name);
                        InvoiceControls.Remove(IValue.Name);
                        InvoiceControls.Remove(IID.Name);
                    }
                    else if (NewInvoiceControls.ContainsKey(IID.Name))
                    {
                        NewInvoiceControls.Remove(ISupplier.Name);
                        NewInvoiceControls.Remove(IDate.Name);
                        NewInvoiceControls.Remove(IReference.Name);
                        NewInvoiceControls.Remove(IInvNumber.Name);
                        NewInvoiceControls.Remove(IValue.Name);
                        NewInvoiceControls.Remove(IID.Name);
                    }

                    if (OrigInvoiceControls.ContainsKey(IID.Name))
                    {
                        RemInvoiceControls.Add(IID.Name, IID);
                    }

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

            if (init == 0)
            {
                InvoiceControls.Add(ISupplier.Name, ISupplier);
                InvoiceControls.Add(IDate.Name, IDate);
                InvoiceControls.Add(IReference.Name, IReference);
                InvoiceControls.Add(IInvNumber.Name, IInvNumber);
                InvoiceControls.Add(IValue.Name, IValue);
                InvoiceControls.Add(IID.Name, IID);
            }
            else if (init == 1)
            {
                NewInvoiceControls.Add(ISupplier.Name, ISupplier);
                NewInvoiceControls.Add(IDate.Name, IDate);
                NewInvoiceControls.Add(IReference.Name, IReference);
                NewInvoiceControls.Add(IInvNumber.Name, IInvNumber);
                NewInvoiceControls.Add(IValue.Name, IValue);
                NewInvoiceControls.Add(IID.Name, IID);
            }

            // Add the new controls to the form
            Controls.Add(ISupplier);
            Controls.Add(IDate);
            Controls.Add(IReference);
            Controls.Add(IInvNumber);
            Controls.Add(IValue);
            Controls.Add(button);
            Controls.Add(IID);

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
