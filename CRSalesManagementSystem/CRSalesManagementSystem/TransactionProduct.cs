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
    public class TransactionProduct : DatabaseConnection
    {
        private int _transactionId;
        private int _productId;

        #region Constructors

        public TransactionProduct()
        {
            _transactionId = 1;
            _productId = 1;
        }

        #endregion

        #region Properties

        public int TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }


        #endregion

        #region Methods

        public void LinkRentProduct()
        {
            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "INSERT INTO rentedproducts(rentid,productid) ";
                stmt += "VALUES(?transid,?productid);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?transid", _transactionId);
                cmd.Parameters.AddWithValue("?productid", _productId);
                
                cmd.ExecuteNonQuery();

                //MessageBox.Show("Rent Transaction Saved!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in saving the transaction.");
            }
            finally
            {
                Conn.Close();
            }
        }


        public void UnLinkRentProducts()
        {
            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "DELETE FROM rentedproducts ";
                stmt += "WHERE rentid=?transid;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?transid", _transactionId);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Rent Transaction Saved!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in saving the transaction.");
            }
            finally
            {
                Conn.Close();
            }
        }

        public void LinkSalesProduct()
        {
            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "INSERT INTO soldproducts(salesid,productid) ";
                stmt += "VALUES(?transid,?productid);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?transid", _transactionId);
                cmd.Parameters.AddWithValue("?productid", _productId);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Sales Transaction Saved!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in saving the transaction.");
            }
            finally
            {
                Conn.Close();
            }
        }

        public void UnLinkSalesProducts()
        {
            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "DELETE FROM soldproducts ";
                stmt += "WHERE salesid=?transid;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?transid", _transactionId);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Sales Transaction Saved!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in saving the transaction.");
            }
            finally
            {
                Conn.Close();
            }
        }

        #endregion
    }
}
