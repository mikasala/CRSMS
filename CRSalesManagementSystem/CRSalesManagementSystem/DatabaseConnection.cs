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
    public class DatabaseConnection
    {
        private MySqlConnection _conn;
        private DataTable _dTable;
        private MySqlDataAdapter _dataAdapter;

        # region Constructor

        public DatabaseConnection()
        {
            //connection to database
            _conn = new MySqlConnection("Server=localhost;port=3306;database=crsms_db;username=root;password=;pooling = false; convert zero datetime=True");
        }

        #endregion

        #region Properties

        public MySqlConnection Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }


        public DataTable DTable
        {
            get { return _dTable; }
            set { _dTable = value; }
        }

        public MySqlDataAdapter DataAdapter
        {
            get { return _dataAdapter; }
            set { _dataAdapter = value; }
        }

        #endregion

        # region Methods

        public bool IsConnected()
        {
            bool connected = false;

            try
            {
                _conn.Open();
                connected = true;
            }

            catch (Exception e)
            {

                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                _conn.Close();
            }

            return connected;
        }

        
        #endregion


    }
}
