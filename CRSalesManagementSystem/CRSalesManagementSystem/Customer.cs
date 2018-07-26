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
    public sealed class Customer : DatabaseConnection
    {

        private int _customerId;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _contactNo;


        #region Constructor

        //initiating without defined value
        public Customer()
        {
            
            _customerId = 1;
            _firstName = "";
            _lastName = "";
            _address = "";
            _contactNo = "";

        }

        #endregion

        #region Properties

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
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
		
		public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string ContactNo
        {
            get { return _contactNo; }
            set { _contactNo = value; }
        }

        #endregion

        #region Methods


        public void GetCustomerInfo()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT * FROM customers WHERE customerid=?customerid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?customerid", _customerId);

                DataAdapter.Fill(DTable);

                //_customerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _firstName = DTable.Rows[0]["fname"].ToString();
                _lastName = DTable.Rows[0]["lname"].ToString();
                _address = DTable.Rows[0]["address"].ToString();
                _contactNo = DTable.Rows[0]["contactno"].ToString();
            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetRentals()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT referenceno as 'Reference No.',date_of_rental as 'Date of Rent',date_of_pickup as 'Pick-up Date',total_amount as 'Amount' FROM rentals WHERE customerid=?customerid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?customerid", _customerId);

                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetSales()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT salesreference as 'Reference No.',date_of_purchase as 'Date of Purchase',amount as 'Amount' FROM sales WHERE customerid=?customerid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?customerid", _customerId);

                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetOrders()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT refnum as 'Reference No.',date_of_order as 'Date Ordered',date_of_pickup as 'Pick-up Date',total_amount as 'Amount' FROM orders WHERE customerid=?customerid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?customerid", _customerId);

                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist.");
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void SaveInfo()
        {

            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = "INSERT INTO customers(fname,lname,address,contactno) " +
				"VALUES(?fname,?lname,?address,?contactno);";
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?fname", _firstName);
                cmd.Parameters.AddWithValue("?lname", _lastName);
                cmd.Parameters.AddWithValue("?address", _address);
                cmd.Parameters.AddWithValue("?contactno", _contactNo);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Added!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in saving the customer.");
            }
            finally
            {
                Conn.Close();
            }
        }

        public void UpdateInfo()
        {

            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText =
                    "UPDATE customers SET fname=?fname,lname=?lname,address=?address,contactno=?contactno WHERE customerid=?customerid;";
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?customerid", _customerId);
                cmd.Parameters.AddWithValue("?fname", _firstName);
                cmd.Parameters.AddWithValue("?lname", _lastName);
                cmd.Parameters.AddWithValue("?address", _address);
                cmd.Parameters.AddWithValue("?contactno", _contactNo);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Updated Successfully!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in updating the customer.");
            }
            finally
            {
                Conn.Close();
            }
        }

        #endregion

    }
}
