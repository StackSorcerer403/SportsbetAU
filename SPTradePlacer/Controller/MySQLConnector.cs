using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace BettingBot.Controller
{
    public class MySQLConnector
    {
        string connectionString = "server=95.179.244.192;port=3306;database=betflash;user=willow;password=KYMis199$109;";

        MySqlConnection connection = null;
        private static MySQLConnector _instance = null;
        public MySQLConnector()
        {
            doConnect();
        }

        public static MySQLConnector instance
        {
            get
            {
                return _instance;
            }
        }

        public bool IsLogged { get; internal set; }

        static public void CreateInstance()
        {
            _instance = new MySQLConnector();
        }

        private void doConnect()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                // Open the connection
                connection.Open();
                Console.WriteLine("Connection successful!");

                // Create a SQL query
                string query = $"SELECT COUNT(*) FROM tbl_account WHERE status=1 AND bookmaker='winamax' AND license='{Setting.instance.fingerPrint}';";

                // Create a command object
                MySqlCommand command = new MySqlCommand(query, connection);
                int rowCount = Convert.ToInt32(command.ExecuteScalar());
                if(rowCount == 0 )
                {
                    LogMng.instance.PrintLog("It is new account. Please contact support!");
                    query = $"INSERT INTO tbl_account(username, password, bookmaker, license) VALUES ('{Setting.instance.betUser}', '{Setting.instance.betPassword}', 'winamax', '{Utils.Base64Encode($"{Setting.instance.betUser}:{Setting.instance.betPassword}")}');";
                    command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    return;
                }
                IsLogged = true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

    }
}
