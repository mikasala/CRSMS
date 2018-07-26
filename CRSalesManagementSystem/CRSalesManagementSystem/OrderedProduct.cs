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
    public sealed class OrderedProduct : TransactionProduct
    {
        private int _orderProdId;
        private string _clothType;
        private string _details;
        private double _chest;
        private double _waist;
        private double _length;

        #region Constructors

        public OrderedProduct()
        {
            _orderProdId = 1;
            _clothType = "";
            _details = "";
            _chest = 0.00;
            _waist = 0.00;
            _length = 10.00;
        }

        #endregion

        #region Properties

        public int OrderProdId
        {
            get { return _orderProdId; }
            set { _orderProdId = value; }
        }

        public string ClothType
        {
            get { return _clothType; }
            set { _clothType = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public double Chest
        {
            get { return _chest; }
            set { _chest = value; }
        }


        public double Waist
        {
            get { return _waist; }
            set { _waist = value; }
        }

        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }

        #endregion

        #region Methods

        public void GetProductInfo()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT * FROM orderedproducts WHERE ordernum=?ordernum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?ordernum", TransactionId);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

                _orderProdId = Int32.Parse(DTable.Rows[0]["orderprodid"].ToString());
                TransactionId = Int32.Parse(DTable.Rows[0]["ordernum"].ToString());
                _clothType = DTable.Rows[0]["clothtype"].ToString();
                _details = DTable.Rows[0]["details"].ToString();
                _chest = Double.Parse(DTable.Rows[0]["chest"].ToString());
                _waist = Double.Parse(DTable.Rows[0]["waist"].ToString());
                _length = Double.Parse(DTable.Rows[0]["overall_length"].ToString());
            }

            catch (Exception e)
            {
                MessageBox.Show("Product does not exist.");
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

                string stmt = "INSERT INTO orderedproducts(ordernum,clothtype,details,chest,waist,overall_length) ";
                stmt += "VALUES(?ordernum,?clothtype,?details,?chest,?waist,?length);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?ordernum", TransactionId);
                cmd.Parameters.AddWithValue("?clothtype", _clothType);
                cmd.Parameters.AddWithValue("?details", _details);
                cmd.Parameters.AddWithValue("?chest", _chest);
                cmd.Parameters.AddWithValue("?waist", _waist);
                cmd.Parameters.AddWithValue("?length", _length);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Product Added!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in processing the order.");
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

                string stmt = "UPDATE orderedproducts SET ";
                stmt += "clothtype=?clothtype,";
                stmt += "details=?details,";
                stmt += "chest=?chest,";
                stmt += "waist=?waist,";
                stmt += "overall_length=?length ";
                stmt += "WHERE ordernum=?ordernum;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?ordernum", TransactionId);
                cmd.Parameters.AddWithValue("?clothtype", _clothType);
                cmd.Parameters.AddWithValue("?details", _details);
                cmd.Parameters.AddWithValue("?chest", _chest);
                cmd.Parameters.AddWithValue("?waist", _waist);
                cmd.Parameters.AddWithValue("?length", _length);

                cmd.ExecuteNonQuery();

                //MessageBox.Show("Updated Successfully!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in updating the product.");
            }
            finally
            {
                Conn.Close();
            }
        }

        #endregion
    }
}
