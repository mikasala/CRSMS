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
    public sealed class MadeToOrder : Transaction
    {
        private DateTime _dateOfOrder;
        private DateTime _dateOfUse;
        private DateTime _dateOfPickUp;
        private string _purpose;
        

        #region Constructors

        public MadeToOrder()
        {
            _dateOfOrder = System.DateTime.MinValue;
            _dateOfUse = System.DateTime.MinValue;
            _dateOfPickUp = System.DateTime.MinValue;
            _purpose = "";  
        }

        #endregion

        #region Properties

        public DateTime DateOfOrder
        {
            get { return _dateOfOrder; }
            set { _dateOfOrder = value; }
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
                DataAdapter = new MySqlDataAdapter("SELECT * FROM orders WHERE ordernum=?ordernum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?ordernum", TransactionId);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                //TransactionId = Int32.Parse(DTable.Rows[0]["rentid"].ToString());
                ReferenceNo = DTable.Rows[0]["refnum"].ToString();
                CustomerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _dateOfOrder = DateTime.Parse(DTable.Rows[0]["date_of_order"].ToString());
                _dateOfUse = DateTime.Parse(DTable.Rows[0]["date_of_use"].ToString());
                _dateOfPickUp = DateTime.Parse(DTable.Rows[0]["date_of_pickup"].ToString());
                _purpose = DTable.Rows[0]["purpose"].ToString();
                TotalAmount = Double.Parse(DTable.Rows[0]["total_amount"].ToString());
                Downpayment = Double.Parse(DTable.Rows[0]["downpayment"].ToString());
                Notes = DTable.Rows[0]["notes"].ToString();

            }

            catch (Exception e)
            {
                MessageBox.Show("Order Transaction does not exist.");
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
                DataAdapter = new MySqlDataAdapter("SELECT * from orders where refnum=?refnum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?refnum", ReferenceNo);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                TransactionId = Int32.Parse(DTable.Rows[0]["ordernum"].ToString());
                ReferenceNo = DTable.Rows[0]["refnum"].ToString();
                CustomerId = Int32.Parse(DTable.Rows[0]["customerid"].ToString());
                _dateOfOrder = DateTime.Parse(DTable.Rows[0]["date_of_order"].ToString());
                _dateOfUse = DateTime.Parse(DTable.Rows[0]["date_of_use"].ToString());
                _dateOfPickUp = DateTime.Parse(DTable.Rows[0]["date_of_pickup"].ToString());
                _purpose = DTable.Rows[0]["purpose"].ToString();
                TotalAmount = Double.Parse(DTable.Rows[0]["total_amount"].ToString());
                Downpayment = Double.Parse(DTable.Rows[0]["downpayment"].ToString());
                Notes = DTable.Rows[0]["notes"].ToString();

            }


            catch (Exception e)
            {
                MessageBox.Show("Order Transaction does not exist.");
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
                DataAdapter = new MySqlDataAdapter("SELECT customerid as 'ID',fname as 'First Name',lname as 'Last Name',address as 'Address',contactno as 'Contact No.' FROM customers WHERE customerid = (SELECT customerid FROM orders WHERE ordernum=?ordernum);", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?ordernum", TransactionId);

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

        public override void GetProducts()
        {
            try
            {
                Conn.Open();
                string stmt = "SELECT clothtype as 'Cloth', details as 'Details', chest as 'Chest', waist as 'Waist', overall_length as 'Length' ";
                stmt += "FROM orderedproducts WHERE ordernum=?ordernum;";
                DataAdapter = new MySqlDataAdapter(stmt, Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?ordernum", TransactionId);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Order Transaction not found.");
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

                string stmt = "INSERT INTO orders(refnum,customerid,date_of_order,date_of_use,date_of_pickup,purpose,total_amount,downpayment,notes) ";
                stmt += "VALUES(?referenceno,?customerid,?dateoforder,?dateofuse,?dateofpickup,?purpose,?amount,?downpayment,?notes);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?referenceno", ReferenceNo);
                cmd.Parameters.AddWithValue("?customerid", CustomerId);
                cmd.Parameters.AddWithValue("?dateoforder", _dateOfOrder);
                cmd.Parameters.AddWithValue("?dateofuse", _dateOfUse);
                cmd.Parameters.AddWithValue("?dateofpickup", _dateOfPickUp);
                cmd.Parameters.AddWithValue("?purpose", _purpose);
                cmd.Parameters.AddWithValue("?amount", TotalAmount);
                cmd.Parameters.AddWithValue("?downpayment", Downpayment);
                cmd.Parameters.AddWithValue("?notes", Notes);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Order Transaction Saved!");

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
                    
                string stmt = "UPDATE orders SET ";
                stmt += "customerid=?customerid,";
                //stmt += "date_of_order=?dateoforder,";
                stmt += "date_of_use=?dateofuse,";
                stmt += "date_of_pickup=?dateofpickup,";
                stmt += "purpose=?purpose,";
                stmt += "total_amount=?amount,";
                stmt += "downpayment=?downpayment,";
                stmt += "notes=?notes ";
                stmt += "WHERE ordernum=?ordernum;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?ordernum", TransactionId);
                cmd.Parameters.AddWithValue("?customerid", CustomerId);
                //cmd.Parameters.AddWithValue("?dateoforder", _dateOfOrder);
                cmd.Parameters.AddWithValue("?dateofuse", _dateOfUse);
                cmd.Parameters.AddWithValue("?dateofpickup", _dateOfPickUp);
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
