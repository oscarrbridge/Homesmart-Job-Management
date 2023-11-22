using MySql.Data.MySqlClient;
using System;

namespace Connections
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;
        private string server;
        private string port;
        private string database;
        private string uid;
        private string password;

        // Constructor
        public DatabaseConnection()
        {
            //dotnet add package MySql.Data --version 8.2.0
            Initialize();
        }

        // Initialize values
        private void Initialize()
        {
            server = "shieldcoat.ddns.net";
            port = "3306";
            database = "homesmart-job-database";
            uid = "kk";
            password = "shieldCOAT!";
            string connectionString = $"SERVER={server};PORT={port};DATABASE={database};USER={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        // Open connection to the database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // When handling errors, you can your application's response based on the error number.
                // The two most common error numbers when connecting are as follows:
                // 0: Cannot connect to server.
                // 1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        // Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}