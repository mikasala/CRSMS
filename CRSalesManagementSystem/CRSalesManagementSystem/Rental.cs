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
    public sealed class Rental : Transaction
    {
        private DateTime _dateOfRental;
        private DateTime _dateOfUse;
        private DateTime _dateOfPickUp;
        private DateTime _dateOfReturn;
        private string _purpose;
        
        #region Constructor

        public Rental()
        {
            _dateOfRental = System.DateTime.MinValue;
            _dateOfUse = System.DateTime.MinValue;
            _dateOfPickUp = System.DateTime.MinValue;
            _dateOfReturn = System.DateTime.MinValue;
            _purpose = "";

        }

        #endregion

        #region Properties

        public DateTime DateOfRental
        {
            get { return _dateOfRental; }
            set { _dateOfRental = value; }
        }

        public DateTime DateOfUse
        {
            get { return _dateOfUse; }
            set { _dateOfUse = value; }
        }

        public DateTime DateOfPickUp
        {
            get { return _dateOfPickUp; }
            set { _dateOfPickUp = value; }
        }

        public DateTime DateOfReturn
        {
            get { return _dateOfReturn; }
            set { _dateOfReturn = value; }
        }

        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }


        #endregion

        #region Methods

        public override void GetInfo()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT * from rentals where rentid=?rentid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?rentid", TransactionId);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                //TransactionId = Int32.Parse(DTable.Rows[0]["rentid"].ToString());
                ReferenceNo = DTable.Rows[0]["referenceno"].ToString();
                CustomerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _dateOfRental = DateTime.Parse(DTable.Rows[0]["date_of_rental"].ToString());
                _dateOfUse = DateTime.Parse(DTable.Rows[0]["date_of_use"].ToString());
                _dateOfPickUp = DateTime.Parse(DTable.Rows[0]["date_of_pickup"].ToString());
                _dateOfReturn = DateTime.Parse(DTable.Rows[0]["date_of_return"].ToString());
                _purpose = DTable.Rows[0]["purpose"].ToString();
                TotalAmount = Double.Parse(DTable.Rows[0]["total_amount"].ToString());
                Downpayment = Double.Parse(DTable.Rows[0]["downpayment"].ToString());
                Notes = DTable.Rows[0]["notes"].ToString();

            }


            catch (Exception e)
            {
                MessageBox.Show("Rent Transaction does not exist.");
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
                DataAdapter = new MySqlDataAdapter("SELECT * from rentals where referenceno=?refnum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?refnum", ReferenceNo);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                TransactionId = Int32.Parse(DTable.Rows[0]["rentid"].ToString());
                ReferenceNo = DTable.Rows[0]["referenceno"].ToString();
                CustomerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _dateOfRental = DateTime.Parse(DTable.Rows[0]["date_of_rental"].ToString());
                _dateOfUse = DateTime.Parse(DTable.Rows[0]["date_of_use"].ToString());
                _dateOfPickUp = DateTime.Parse(DTable.Rows[0]["date_of_pickup"].ToString());
                _dateOfReturn = DateTime.Parse(DTable.Rows[0]["date_of_return"].ToString());
                _purpose = DTable.Rows[0]["purpose"].ToString();
                TotalAmount = Double.Parse(DTable.Rows[0]["total_amount"].ToString());
                Downpayment = Double.Parse(DTable.Rows[0]["downpayment"].ToString());
                Notes = DTable.Rows[0]["notes"].ToString();

            }


            catch (Exception e)
            {
                MessageBox.Show("Rent Transaction does not exist.");
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
                DataAdapter = new MySqlDataAdapter("SELECT customerid as 'ID',fname as 'First Name',lname as 'Last Name',address as 'Address',contactno as 'Contact No.' FROM customers WHERE customerid = (SELECT customerid FROM rentals WHERE rentid=?rentid);", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?rentid", TransactionId);

                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Rent Transaction not found."+e.Message);
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
                string stmt = "SELECT productid as 'ID',name as 'Name',category as 'Category',size_fit as 'Size',rentprice as 'Amount' ";
                stmt += "FROM products WHERE productid in (SELECT productid FROM rentedproducts WHERE rentid=?rentid);";
                DataAdapter = new MySqlDataAdapter(stmt, Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?rentid", TransactionId);
                
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Rent Transaction not found.");
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

                string stmt = "INSERT INTO rentals(referenceno,customerid,date_of_rental,date_of_use,date_of_pickup,date_of_return,purpose,total_amount,downpayment,notes) ";
                stmt += "VALUES(?referenceno,?customerid,?dateofrental,?dateofuse,?dateofpickup,?dateofreturn,?purpose,?amount,?downpayment,?notes);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?referenceno", ReferenceNo);
                cmd.Parameters.AddWithValue("?customerid", CustomerId);
                cmd.Parameters.AddWithValue("?dateofrental", _dateOfRental);
                cmd.Parameters.AddWithValue("?dateofuse", _dateOfUse);
                cmd.Parameters.AddWithValue("?dateofpickup", _dateOfPickUp);
                cmd.Parameters.AddWithValue("?dateofreturn", _dateOfReturn);
                cmd.Parameters.AddWithValue("?purpose", _purpose);
                cmd.Parameters.AddWithValue("?amount", TotalAmount);
                cmd.Parameters.AddWithValue("?downpayment", Downpayment);
                cmd.Parameters.AddWithValue("?notes", Notes);

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

        public override void UpdateTransaction()
        {

            try
            {
                Conn.Open();

                //preparing statement
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conn;

                string stmt = "UPDATE rentals SET ";
                stmt += "customerid=?customerid,";
                //stmt += "date_of_rental=?dateofrental,";
                stmt += "date_of_use=?dateofuse,";
                stmt += "date_of_pickup=?dateofpickup,";
                stmt += "date_of_return=?dateofreturn,";
                stmt += "purpose=?purpose,";
                stmt += "total_amount=?amount,";
                stmt += "downpayment=?downpayment,";
                stmt += "notes=?notes ";
                stmt += "WHERE rentid=?rentid;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?rentid", TransactionId);
                cmd.Parameters.AddWithValue("?customerid", CustomerId);
                //cmd.Parameters.AddWithValue("?dateofrental", _dateOfRental);
                cmd.Parameters.AddWithValue("?dateofuse", _dateOfUse);
                cmd.Parameters.AddWithValue("?dateofpickup", _dateOfPickUp);
                cmd.Parameters.AddWithValue("?dateofreturn", _dateOfReturn);
                cmd.Parameters.AddWithValue("?purpose", _purpose);
                cmd.Parameters.AddWithValue("?amount", TotalAmount);
                cmd.Parameters.AddWithValue("?downpayment", Downpayment);
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
