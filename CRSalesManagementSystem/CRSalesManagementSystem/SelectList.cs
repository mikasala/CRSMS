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
    public sealed class SelectList : DatabaseConnection
    {
        #region Constructors

        public SelectList()
        {
        }

        #endregion

        #region Properties


        #endregion

        # region Methods

        #region Methods for Users

        public void GetAllUsers()
        {
            try
            {
                Conn.Open();
                DTable = new DataTable();
                DataAdapter = new MySqlDataAdapter("SELECT * FROM users;", Conn);
                DataAdapter.Fill(DTable);
            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        #endregion

        #region Methods for Customers

        public void GetAllCustomers()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT customerid as 'ID', fname as 'First Name', lname as 'Last Name', address as 'Address', contactno as 'Contact No' FROM customers;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetCustomersByName(string name)
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT customerid as 'ID', fname as 'First Name', lname as 'Last Name', address as 'Address', contactno as 'Contact No' FROM customers WHERE fname LIKE ?fname;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?fname", "%" + name + "%");

                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist." + e.Message);
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetCustomersCount()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT count(customerid) FROM customers;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        


        #endregion

        #region Methods for Products

        public void GetAllProducts()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT productid as 'ID', name as 'Name', clothtype as 'Cloth', quantity as 'Quantity', description as 'Description', size_fit as 'Size', rentprice as 'Rent Price', amount as 'Price', category as 'Category', status as 'Status' FROM products ;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetAllProductsAvailable()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT productid as 'ID', name as 'Name', clothtype as 'Cloth', quantity as 'Quantity', description as 'Description', size_fit as 'Size', rentprice as 'Rent Price', amount as 'Price', category as 'Category', status as 'Status' FROM products WHERE status='Available';", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetProductsByName(string name)
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT productid as 'ID', name as 'Name', clothtype as 'Cloth', quantity as 'Quantity', description as 'Description', size_fit as 'Size', rentprice as 'Rent Price', amount as 'Price', category as 'Category', status as 'Status' FROM products WHERE name LIKE ?name;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?name", "%" + name + "%");

                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist." + e.Message);
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetProductsAvailableByName(string name)
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT productid as 'ID', name as 'Name', clothtype as 'Cloth', quantity as 'Quantity', description as 'Description', size_fit as 'Size', rentprice as 'Rent Price', amount as 'Price', category as 'Category', status as 'Status' FROM products WHERE name LIKE ?name and status = 'Available';", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?name", "%" + name + "%");

                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Customer does not exist." + e.Message);
                Trace.WriteLine("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }


        #endregion

        #region Methods for Rentals

        public void GetAllRentals()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT referenceno as 'Reference No.', customerid as 'Customer',date_of_rental as 'Rent Date',date_of_use as 'Date of Use',date_of_pickup as 'Pick-up Date', date_of_return as 'Return Date', purpose as 'Purpose', total_amount as 'Amount', downpayment as 'Downpayment', notes as 'Notes' from rentals;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetAllRentalsID()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT rentid from rentals;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetMaxRentalsID()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT max(rentid) as 'Max' from rentals;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetRentalsByRefnum(string refnum)
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT referenceno as 'Reference No.', customerid as 'Customer',date_of_rental as 'Rent Date',date_of_use as 'Date of Use',date_of_pickup as 'Pick-up Date', date_of_return as 'Return Date', purpose as 'Purpose', total_amount as 'Amount', downpayment as 'Downpayment', notes as 'Notes' FROM rentals WHERE referenceno LIKE ?refnum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?refnum", "%" + refnum + "%");
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetRentalsCount()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT count(rentid) as 'Count' FROM rentals;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        #endregion

        #region Methods for Sales


        public void GetAllSales()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT salesreference as 'Reference No.', customerid as 'Customer',date_of_purchase as 'Purchase Date', amount as 'Amount', notes as 'Notes' FROM sales;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetAllSalesID()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT salesid FROM sales;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }


        public void GetMaxSalesID()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT max(salesid) as 'Max' from sales;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }


        public void GetSalesByRefnum(string refnum)
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT salesreference as 'Reference No.', customerid as 'Customer',date_of_purchase as 'Purchase Date', amount as 'Amount', notes as 'Notes' FROM sales WHERE salesreference LIKE ?refnum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?refnum", "%" + refnum + "%");
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }


        public void GetSalesCount()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT count(salesid) as 'Count' FROM sales;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        #endregion

        #region Methods for Orders


        public void GetAllOrders()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT refnum as 'Reference No.', customerid as 'Customer',date_of_order as 'Order Date',date_of_use as 'Date of Use',date_of_pickup as 'Pick-up Date',purpose as 'Purpose', total_amount as 'Amount', downpayment as 'Downpayment', notes as 'Notes' FROM orders;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }


        }

        public void GetAllOrdersID()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT ordernum FROM orders;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }


        public void GetMaxOrdersID()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT max(ordernum) as 'Max' from orders;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }


        public void GetOrdersByRefnum(string refnum)
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT refnum as 'Reference No.', customerid as 'Customer',date_of_order as 'Order Date',date_of_use as 'Date of Use',date_of_pickup as 'Pick-up Date',purpose as 'Purpose', total_amount as 'Amount', downpayment as 'Downpayment', notes as 'Notes' FROM orders WHERE refnum LIKE ?refnum;", Conn);
                DataAdapter.SelectCommand.Parameters.AddWithValue("?refnum", "%" + refnum + "%");
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        public void GetOrdersCount()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT count(ordernum) as 'Count' FROM orders;", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }
        #endregion


        public void GetTransCount()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT count(rentid) as 'Rent Count',count(salesid) as 'Sales Count',count(ordernum) as 'Orders Count',count(rentid)+count(salesid)+count(ordernum) as 'Total Count' FROM rentals,sales,orders where year(date_of_rental)=year(now()) and month(date_of_rental)=month(now()) and year(date_of_purchase)=year(now()) and month(date_of_purchase)=month(now()) and year(date_of_order)=year(now()) and month(date_of_order)=month(now());", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }
        public void GetTransCountByDate()
        {
            try
            {
                Conn.Open();
                DataAdapter = new MySqlDataAdapter("SELECT count(rentid) as 'Rent Count',count(salesid) as 'Sales Count',count(ordernum) as 'Orders Count',count(rentid)+count(salesid)+count(ordernum) as 'Total Count' FROM rentals,sales,orders where year(date_of_rental)=year(now()) and month(date_of_rental)=month(now()) and year(date_of_purchase)=year(now()) and month(date_of_purchase)=month(now()) and year(date_of_order)=year(now()) and month(date_of_order)=month(now());", Conn);
                DTable = new DataTable();
                DataAdapter.Fill(DTable);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error:" + e.Message);
            }

            finally
            {
                Conn.Close();
            }
        }

        #endregion

    }
}
