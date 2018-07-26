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
    public sealed class Product : DatabaseConnection
    {
        private int _productId;
        private string _name;
        private string _clothType;
        private int _quantity;
        private string _description;
        private string _sizeFit;
        private double _rentPrice;
        private double _amount;
        private string _category;
        private string _status;


        #region Constructor

        public Product()
        {
            _productId = 1;
            _name = "";
            _clothType = "";
            _quantity = 1;
            _description = "";
            _sizeFit = "";
            _rentPrice = 1.00;
            _amount = 1.00;
            _category = "";
            _status = "";

        }

        #endregion

        #region Properties

        public int ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ClothType
        {
            get { return _clothType; }
            set { _clothType = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string SizeFit
        {
            get { return _sizeFit; }
            set { _sizeFit = value; }
        }

        public double RentPrice
        {
            get { return _rentPrice; }
            set { _rentPrice = value; }
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }


        #endregion

        #region Methods

        public void GetProductInfo()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT productid as 'ID', name as 'Name', clothtype as 'Cloth', quantity as 'Quantity', description as 'Description', size_fit as 'Size', rentprice as 'Rent Price', amount as 'Price', category as 'Category', status as 'Status' FROM products WHERE productid=?productid;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?productid", _productId);

                DataAdapter.Fill(DTable);

                //_productId = Int32.Parse(DTable.Rows[0]["productid"].ToString());
                _name = DTable.Rows[0]["Name"].ToString();
                _clothType = DTable.Rows[0]["Cloth"].ToString();
                _quantity = Int32.Parse(DTable.Rows[0]["Quantity"].ToString());
                _description = DTable.Rows[0]["Description"].ToString();
                _sizeFit = DTable.Rows[0]["Size"].ToString();
                _rentPrice = Double.Parse(DTable.Rows[0]["Rent Price"].ToString());
                _amount = Double.Parse(DTable.Rows[0]["Price"].ToString());
                _category = DTable.Rows[0]["Category"].ToString();
                _status = DTable.Rows[0]["Status"].ToString();

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

        public void GetRentals()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT referenceno as 'Reference No.',date_of_rental as 'Date of Rent',date_of_pickup as 'Pick-up Date',total_amount as 'Amount' FROM rentals WHERE rentid in (SELECT rentid FROM rentedproducts WHERE productid=?productid);", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?productid", _productId);

                DataAdapter.Fill(DTable);

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

        public void GetSales()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT salesreference as 'Reference No.',date_of_purchase as 'Date of Purchase',amount as 'Amount' FROM sales WHERE salesid in (SELECT salesid FROM soldproducts WHERE productid=?productid);", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?productid", _productId);

                DataAdapter.Fill(DTable);

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

                string stmt = "INSERT INTO products(name,clothtype,quantity,description,size_fit,rentprice,amount,category,status) ";
                stmt += "VALUES(?name,?clothtype,?quantity,?description,?sizefit,?rentprice,?amount,?category,?status);";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?name", _name);
                cmd.Parameters.AddWithValue("?clothtype", _clothType);
                cmd.Parameters.AddWithValue("?quantity", _quantity);
                cmd.Parameters.AddWithValue("?description", _description);
                cmd.Parameters.AddWithValue("?sizefit", _sizeFit);
                cmd.Parameters.AddWithValue("?rentprice", _rentPrice);
                cmd.Parameters.AddWithValue("?amount", _amount);
                cmd.Parameters.AddWithValue("?category", _category);
                cmd.Parameters.AddWithValue("?status", _status);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Product Added!");

            }
            catch (MySqlException ex)
            {
                Trace.WriteLine("Error: {0}", ex.ToString());
                MessageBox.Show("Error occurred in saving the product.");
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

                string stmt = "UPDATE products SET ";
                stmt += "name=?name,";
                stmt += "clothtype=?clothtype,";
                stmt += "quantity=?quantity,";
                stmt += "description=?description,";
                stmt += "size_fit=?sizefit,";
                stmt += "rentprice=?rentprice,";
                stmt += "amount=?amount,";
                stmt += "category=?category,";
                stmt += "status=?status ";
                stmt += "WHERE productid=?productid;";

                cmd.CommandText = stmt;
                cmd.Prepare();

                //binding parameters
                cmd.Parameters.AddWithValue("?productid", _productId);
                cmd.Parameters.AddWithValue("?name", _name);
                cmd.Parameters.AddWithValue("?clothtype", _clothType);
                cmd.Parameters.AddWithValue("?quantity", _quantity);
                cmd.Parameters.AddWithValue("?description", _description);
                cmd.Parameters.AddWithValue("?sizefit", _sizeFit);
                cmd.Parameters.AddWithValue("?rentprice", _rentPrice);
                cmd.Parameters.AddWithValue("?amount", _amount);
                cmd.Parameters.AddWithValue("?category", _category);
                cmd.Parameters.AddWithValue("?status", _status);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Updated Successfully!");

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
