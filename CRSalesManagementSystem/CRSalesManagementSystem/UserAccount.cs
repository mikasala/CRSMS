using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;

namespace CRSalesManagementSystem
{
    public sealed class UserAccount : DatabaseConnection
    {
        private string _username;
        private string _password;
        private string _firstName;
        private string _lastName;

        #region Constructor

        public UserAccount(string username, string password)
        {
            _username = username;
            _password = GetMD5Hash(password);
        }

        #endregion

        #region Properties

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        #endregion

        #region Methods

        public bool IsAdmin()
        {
            bool admin = false;

            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter =
                    new MySqlDataAdapter("SELECT * FROM users WHERE username=?username AND password=?password;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?username", _username);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?password", _password);

                DataAdapter.Fill(DTable);

                if (DTable.Rows.Count != 0)
                {
                    admin = true;
                    _firstName = DTable.Rows[0]["fname"].ToString();
                    _lastName = DTable.Rows[0]["lname"].ToString();
                }

            }

            catch (Exception e)
            {
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }

            return admin;
        }

        public string GetMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString().ToLower();
        }


        #endregion
    }
}
