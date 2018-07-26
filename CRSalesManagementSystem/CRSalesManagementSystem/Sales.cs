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
    public sealed class Sales : Transaction
    {
        private DateTime _dateOfPurchase;

        #region Constructor

        public Sales()
        {
            _dateOfPurchase = System.DateTime.MinValue;
        }

        #endregion

        #region Properties

        public DateTime DateOfPurchase
        {
            get { return _dateOfPurchase; }
            set { _dateOfPurchase = value; }
        }

        #endregion

        #region Methods

        public override void GetInfo()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT * FROM sales WHERE salesid=?salesid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?salesid", TransactionId);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                //TransactionId = Int32.Parse(DTable.Rows[0]["rentid"].ToString());
                ReferenceNo = DTable.Rows[0]["salesreference"].ToString();
                CustomerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _dateOfPurchase = DateTime.Parse(DTable.Rows[0]["date_of_purchase"].ToString());
                TotalAmount = Double.Parse(DTable.Rows[0]["amount"].ToString());
                Notes = DTable.Rows[0]["notes"].ToString();

            }

            catch (Exception e)
            {
                MessageBox.Show("Sales Transaction does not exist.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public override void GetInfoByRefNum()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT * from sales where salesreference=?refnum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?refnum", ReferenceNo);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                TransactionId = Int32.Parse(DTable.Rows[0]["salesid"].ToString());
                ReferenceNo = DTable.Rows[0]["salesreference"].ToString();
                CustomerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _dateOfPurchase = DateTime.Parse(DTable.Rows[0]["date_of_purchase"].ToString());
                TotalAmount = Double.Parse(DTable.Rows[0]["amount"].ToString());
                Notes = DTable.Rows[0]["notes"].ToString();

            }


            catch (Exception e)
            {
                MessageBox.Show("Sales Transaction does not exist.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public override void GetCustomer()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT customerid as 'ID',fname as 'First Name',lname as 'Last Name',address as 'Address',contactno as 'Contact No.' FROM customers WHERE customerid = (SELECT customerid FROM sales WHERE salesid=?salesid);", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?salesid", TransactionId);

                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Sales Transaction not found.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public override void GetProducts()
        {
            try
            {
                Conn.Open();
                string stmt = "SELECT productid as 'ID',name as 'Name',category as 'Category',size_fit as 'Size',amount as 'Amount' ";
                stmt += "FROM products WHERE productid in (SELECT productid FROM soldproducts WHERE salesid=?salesid);";
                DataAdapter = new MySqlDataAdapter(stmt, Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?salesid", TransactionId);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Sales Transaction not found.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public override void SaveTransaction()
        {
            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "INSERT INTO sales(salesreference,customerid,date_of_purchase,amount,notes) ";
                stmt += "VALUES(?referenceno,?customerid,?dateofpurchase,?amount,?notes);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?referenceno", ReferenceNo);
                cmd.Parameters.AddWithValue("?customerid", CustomerId);
                cmd.Parameters.AddWithValue("?dateofpurchase", _dateOfPurchase);
                cmd.Parameters.AddWithValue("?amount", TotalAmount);
                cmd.Parameters.AddWithValue("?notes", Notes);

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

        public override void UpdateTransaction()
        {
            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "UPDATE sales SET ";
                stmt += "customerid=?customerid,";
                //stmt += "date_of_purchase=?dateofpurchase,";
                stmt += "amount=?amount,";
                stmt += "notes=?notes ";
                stmt += "WHERE salesid=?salesid;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?salesid", TransactionId);
                cmd.Parameters.AddWithValue("?customerid", CustomerId);
                //cmd.Parameters.AddWithValue("?dateofpurchase", _dateOfPurchase);
                cmd.Parameters.AddWithValue("?amount", TotalAmount);
                cmd.Parameters.AddWithValue("?notes", Notes);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Updated Successfully!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in updating the transaction.");
            }
            finally
            {
                Conn.Close();
            }
        }

        #endregion
    }
}
