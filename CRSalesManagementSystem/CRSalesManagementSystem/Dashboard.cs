using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CRSalesManagementSystem
{
    public partial class Dashboard : Form
    {
        DataSet dt = new DataSet();
        Customer cust;
        Product prod;
        Rental rent;
        Sales sales;
        MadeToOrder order;

        public Dashboard()
        {
            InitializeComponent();

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            //displays all the list of each tab
            DisplayAllCustomers();
            DisplayAllProducts();
            DisplayAllRentals();
            DisplayAllSales();
            DisplayAllOrders();

            //buttons for customer
            ResetCustButton();

            //buttons for products
            ResetProdButton();

            //buttons for rentals
            ResetRentButton();

            //buttons for sales
            ResetSalesButton();

            //buttons for sales
            ResetOrdersButton();
            
        }

        
        private void Dashboard_FormClosed(object sender, EventArgs e)
        {

            System.Windows.Forms.Application.Exit();
        }

        #region Customers Functions

        //resets customer buttons
        public void ResetCustButton()
        {
            //toggle buttons
            btnCAdd.Visible = true;
            btnCAdd.Enabled = true;
            btnCUpdate.Visible = false;
            btnCUpdate.Enabled = false;

        }
        
        //clears customer textboxes
        public void ClearCustText()
        {
            //clear the text boxes
            txtCSearch.Clear();
            txtCID.Clear();
            txtCFirst.Clear();
            txtCLast.Clear();
            txtCadd.Clear();
            txtCCont.Clear();

        }

        //displays rentals, orders, and sales list
        public void DisplayCustLists()
        {

            cust.GetRentals();
            dtCRentals.DataSource = cust.DTable;
            lblCRentals.Text = "Rentals (" + cust.DTable.Rows.Count.ToString() + ")";

            cust.GetSales();
            dtCSales.DataSource = cust.DTable;
            lblCSales.Text = "Sales (" + cust.DTable.Rows.Count.ToString() + ")";

            cust.GetOrders();
            dtCOrders.DataSource = cust.DTable;
            lblCOrders.Text = "Orders (" + cust.DTable.Rows.Count.ToString() + ")";
        }

        //clears rentals, orders, and sales list
        public void ClearCustLists()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { 
            new DataColumn("No Customer selected.", typeof(String)),
            });

            dtCRentals.DataSource = dt;
            lblCRentals.Text = "Rentals";
            dtCSales.DataSource = dt;
            lblCSales.Text = "Sales";
            dtCOrders.DataSource = dt;
            lblCOrders.Text = "Orders";
        }

        //display the list of customers in the datagrid
        public void DisplayAllCustomers()
        {
            SelectList slist = new SelectList();
            slist.GetAllCustomers();
            if (slist.DTable.Rows != null)
                dtCustoInfo.DataSource = slist.DTable;
            ClearCustLists();

            //updates drop down customer list of each transaction
            LoadRentalsCustList();
            LoadSalesCustList();
            LoadOrdersCustList();

            cust = null;
        }

        public void DisplayAllCustomersByName(string name)
        {
            SelectList slist = new SelectList();
            slist.GetCustomersByName(name);
            if (slist.DTable.Rows != null)
                dtCustoInfo.DataSource = slist.DTable;

            //updates drop down customer list of each transaction
            LoadRentalsCustList();
            LoadSalesCustList();
            LoadOrdersCustList();
        }

        private void dtCustoInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow custRow = dtCustoInfo.Rows[e.RowIndex];

                //instantiating class
                cust = new Customer();
                txtCID.Text = custRow.Cells["ID"].Value.ToString();
                cust.CustomerId = Int32.Parse(txtCID.Text);
                cust.GetCustomerInfo();

                txtCLast.Text = cust.LastName;
                txtCFirst.Text = cust.FirstName;
                txtCadd.Text = cust.Address;
                txtCCont.Text = cust.ContactNo;

                DisplayCustLists();

                //toggle buttons
                btnCAdd.Visible = false;
                btnCAdd.Enabled = false;
                btnCUpdate.Visible = true;
                btnCUpdate.Enabled = true;
            }

        }

        private void btnCNew_Click(object sender, EventArgs e)
        {
            ClearCustText();

            ResetCustButton();

            DisplayAllCustomers();
        
        }

        private void btnCRefresh_Click(object sender, EventArgs e)
        {
            ClearCustText();

            ResetCustButton();

            DisplayAllCustomers();

        }

        private void txtCSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayAllCustomersByName(txtCSearch.Text);
        }


        private void btnCAdd_Click(object sender, EventArgs e)
        {
            bool proceed = true;

            if (txtCFirst.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide first name of the customer.");
            }
            else if (txtCLast.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide last name of the customer.");
            }
            else if (txtCadd.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide the address of the customer.");
            }
            else if (txtCCont.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide the contact number of the customer.");
            }

            if (proceed)
            {
                cust = new Customer();
                cust.FirstName = txtCFirst.Text;
                cust.LastName = txtCLast.Text;
                cust.Address = txtCadd.Text;
                cust.ContactNo = txtCCont.Text;
                cust.SaveInfo();

                ClearCustText();

                DisplayAllCustomers();

            }

        }


        private void btnCUpdate_Click(object sender, EventArgs e)
        {
            bool proceed = true;

            if (txtCFirst.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide first name of the customer.");
            }
            else if (txtCLast.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide last name of the customer.");
            }
            else if (txtCadd.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide the address of the customer.");
            }
            else if (txtCCont.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide the contact number of the customer.");
            }
            else
            {
                //getting values from the text boxes
                cust.FirstName = txtCFirst.Text;
                cust.LastName = txtCLast.Text;
                cust.Address = txtCadd.Text;
                cust.ContactNo = txtCCont.Text;
            }

            if (proceed)
            {
                cust.UpdateInfo();

                DisplayAllCustomersByName(txtCSearch.Text);
            }
        }

        private void btnCForRental_Click(object sender, EventArgs e)
        {
            if (cust == null)
            {
                bool proceed = true;

                if (txtCFirst.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide first name of the customer.");
                }
                else if (txtCLast.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide last name of the customer.");
                }
                else if (txtCadd.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide the address of the customer.");
                }
                else if (txtCCont.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide the contact number of the customer.");
                }

                if (proceed)
                {
                    cust = new Customer();
                    cust.FirstName = txtCFirst.Text;
                    cust.LastName = txtCLast.Text;
                    cust.Address = txtCadd.Text;
                    cust.ContactNo = txtCCont.Text;
                    cust.SaveInfo();

                    ClearCustText();

                    //gets the customer id
                    SelectList slist = new SelectList();
                    slist.GetAllCustomers();
                    int rowindex = slist.DTable.Rows.Count-1;
                    DataRow cRow = slist.DTable.Rows[rowindex];

                    int custid = Int32.Parse(cRow["ID"].ToString());

                    DisplayAllCustomers();
                    
                    //refreshes rentals page first: clears everything
                    ClearRentText();
                    ClearRentLists();
                    ResetRentButton();

                    //loads cstomer to rentals page as the chosen customer
                    LoadRentalsCustSelected(custid);

                    tabTransactions.SelectedIndex = 1;
                    
                }
            }
            else
            {
                //refreshes rentals page first: clears everything
                ClearRentText();
                ClearRentLists();
                ResetRentButton();

                //loads cstomer to rentals page as the chosen customer
                LoadRentalsCustSelected(cust.CustomerId);
                
                tabTransactions.SelectedIndex = 1;
            }
        }

        private void btnCForSales_Click(object sender, EventArgs e)
        {
            if (cust == null)
            {
                bool proceed = true;

                if (txtCFirst.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide first name of the customer.");
                }
                else if (txtCLast.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide last name of the customer.");
                }
                else if (txtCadd.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide the address of the customer.");
                }
                else if (txtCCont.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide the contact number of the customer.");
                }

                if (proceed)
                {
                    cust = new Customer();
                    cust.FirstName = txtCFirst.Text;
                    cust.LastName = txtCLast.Text;
                    cust.Address = txtCadd.Text;
                    cust.ContactNo = txtCCont.Text;
                    cust.SaveInfo();

                    ClearCustText();

                    //gets the customer id
                    SelectList slist = new SelectList();
                    slist.GetAllCustomers();
                    int rowindex = slist.DTable.Rows.Count - 1;
                    DataRow cRow = slist.DTable.Rows[rowindex];

                    int custid = Int32.Parse(cRow["ID"].ToString());

                    DisplayAllCustomers();

                    //refreshes Sales page first: clears everything
                    ClearSalesText();
                    ClearSalesLists();
                    ResetSalesButton();

                    //loads cstomer to sales page as the chosen customer
                    LoadSalesCustSelected(custid);

                    tabTransactions.SelectedIndex = 2;

                }
            }
            else
            {
                //refreshes Sales page first: clears everything
                ClearSalesText();
                ClearSalesLists();
                ResetSalesButton();

                //loads cstomer to rentals page as the chosen customer
                LoadSalesCustSelected(cust.CustomerId);

                tabTransactions.SelectedIndex = 2;
            }
        }

        private void btnCForOrder_Click(object sender, EventArgs e)
        {
            if (cust == null)
            {
                bool proceed = true;

                if (txtCFirst.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide first name of the customer.");
                }
                else if (txtCLast.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide last name of the customer.");
                }
                else if (txtCadd.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide the address of the customer.");
                }
                else if (txtCCont.Text == "")
                {
                    proceed = false;
                    MessageBox.Show("Please provide the contact number of the customer.");
                }

                if (proceed)
                {
                    cust = new Customer();
                    cust.FirstName = txtCFirst.Text;
                    cust.LastName = txtCLast.Text;
                    cust.Address = txtCadd.Text;
                    cust.ContactNo = txtCCont.Text;
                    cust.SaveInfo();

                    ClearCustText();

                    //gets the customer id
                    SelectList slist = new SelectList();
                    slist.GetAllCustomers();
                    int rowindex = slist.DTable.Rows.Count - 1;
                    DataRow cRow = slist.DTable.Rows[rowindex];

                    int custid = Int32.Parse(cRow["ID"].ToString());

                    DisplayAllCustomers();

                    //refreshes rentals page first: clears everything
                    ClearOrdersText();
                    ClearOrdersLists();
                    ResetOrdersButton();

                    //loads cstomer to rentals page as the chosen customer
                    LoadOrdersCustSelected(custid);

                    tabTransactions.SelectedIndex = 4;

                }
            }
            else
            {
                //refreshes rentals page first: clears everything
                ClearOrdersText();
                ClearOrdersLists();
                ResetOrdersButton();

                //loads cstomer to rentals page as the chosen customer
                LoadOrdersCustSelected(cust.CustomerId);

                tabTransactions.SelectedIndex = 4;
            }
        }

        #endregion

        #region Products Functions

        //resets product buttons
        public void ResetProdButton()
        {
            //toggle buttons
            btnProdAdd.Visible = true;
            btnProdAdd.Enabled = true;
            btnProdUpdate.Visible = false;
            btnProdUpdate.Enabled = false;

        }

        //clears product textboxes
        public void ClearProdText()
        {
            //clear the text boxes
            txtPSearch.Clear();
            txtProdID.Clear();
            txtPName.Clear();
            txtPCloth.Clear();
            txtPQuantity.Value = txtPQuantity.Minimum;
            txtPDesc.Clear();
            dropPSize.SelectedIndex = -1;
            txtPRPrice.Value = txtPRPrice.Minimum;
            txtPAmount.Value = txtPAmount.Minimum;
            dropPCateg.SelectedIndex = -1;
            dropPStatus.SelectedIndex = -1;
        }

        //displays rentals, orders, and sales list
        public void DisplayProdLists()
        {
            prod.GetRentals();
            dtProdRentals.DataSource = prod.DTable;

            prod.GetSales();
            dtProdSales.DataSource = prod.DTable;

        }

        //clears rentals, orders, and sales list
        public void ClearProdLists()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { 
            new DataColumn("No Product selected.", typeof(String)),
            });

            dtProdRentals.DataSource = dt;
            dtProdSales.DataSource = dt;
        }

        //displays all products
        public void DisplayAllProducts()
        {
            SelectList slist = new SelectList();
            slist.GetAllProducts();
            if (slist.DTable.Rows != null)
                dtProdInfo.DataSource = slist.DTable;
            ClearProdLists();
        }


        public void DisplayAllProductsByName(string name)
        {
            SelectList slist = new SelectList();
            slist.GetProductsByName(name);
            if (slist.DTable.Rows != null)
                dtProdInfo.DataSource = slist.DTable;

        }


        private void txtPSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayAllProductsByName(txtPSearch.Text);
        }

        private void btnProdNew_Click(object sender, EventArgs e)
        {
            ResetProdButton();

            ClearProdText();

            DisplayAllProducts();

        }

        private void btnProdRefr_Click(object sender, EventArgs e)
        {
            ResetProdButton();

            ClearProdText();

            DisplayAllProducts();
        }

        private void btnProdAdd_Click(object sender, EventArgs e)
        {
            bool proceed = true;

            if (txtPName.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide name of the product.");
            }
            else if (txtPCloth.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide cloth type of the product.");
            }
            else if (txtPDesc.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide any description of the product.");
            }
            else if (txtPQuantity.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide quantity of the product.");
            }
            else if (dropPSize.SelectedIndex < 0)
            {
                proceed = false;
                MessageBox.Show("Please choose a size.");
            }
            else if (txtPRPrice.Text=="0.00")
            {
                proceed = false;
                MessageBox.Show("Please provide rent price.");
            }
            else if (txtPAmount.Text == "0.00")
            {
                proceed = false;
                MessageBox.Show("Please provide the amount.");
            }
            else if (dropPCateg.SelectedIndex < 0)
            {
                proceed = false;
                MessageBox.Show("Please choose a category.");
            }
            else if (dropPStatus.SelectedIndex < 0)
            {
                proceed = false;
                MessageBox.Show("Please choose a status.");
            }

            if (proceed)
            {
                prod = new Product();

                prod.Name = txtPName.Text;
                prod.ClothType = txtPCloth.Text;
                prod.Quantity = Int32.Parse(txtPQuantity.Text);
                prod.Description = txtPDesc.Text;
                prod.SizeFit = dropPSize.SelectedItem.ToString();
                prod.RentPrice = Double.Parse(txtPRPrice.Text);
                prod.Amount = Double.Parse(txtPAmount.Text);
                prod.Category = dropPCateg.SelectedItem.ToString();
                prod.Status = dropPStatus.SelectedItem.ToString();
                prod.SaveInfo();

                ClearProdText();

                DisplayAllProducts();

            }
        }

        private void btnProdUpdate_Click(object sender, EventArgs e)
        {
            bool proceed = true;
            
            if (txtPName.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide name of the product.");
            }
            else if (txtPCloth.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide cloth type of the product.");
            }
            else if (txtPDesc.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide any description of the product.");
            }
            else if (txtPQuantity.Text == "")
            {
                proceed = false;
                MessageBox.Show("Please provide quantity of the product.");
            }
            else if (dropPSize.SelectedIndex < 0)
            {
                proceed = false;
                MessageBox.Show("Please choose a size.");
            }
            else if (txtPRPrice.Text == "0.00")
            {
                proceed = false;
                MessageBox.Show("Please provide rent price.");
            }
            else if (txtPAmount.Text == "0.00")
            {
                proceed = false;
                MessageBox.Show("Please provide the amount.");
            }
            else if (dropPCateg.SelectedIndex < 0)
            {
                proceed = false;
                MessageBox.Show("Please choose a category.");
            }
            else if (dropPStatus.SelectedIndex < 0)
            {
                proceed = false;
                MessageBox.Show("Please choose a status.");
            }

            if (proceed)
            {
                //getting values from the text boxes
                prod.Name = txtPName.Text;
                prod.ClothType = txtPCloth.Text;
                prod.Quantity = Int32.Parse(txtPQuantity.Text);
                prod.Description = txtPDesc.Text;
                prod.SizeFit = dropPSize.SelectedItem.ToString();
                prod.RentPrice = Double.Parse(txtPRPrice.Text);
                prod.Amount = Double.Parse(txtPAmount.Text);
                prod.Category = dropPCateg.SelectedItem.ToString();
                prod.Status = dropPStatus.SelectedItem.ToString();
                
            }


            if (proceed)
            {
                prod.UpdateInfo();

                DisplayAllProductsByName(txtPSearch.Text);
            }
        }


        private void dtProdInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow myProdRow = this.dtProdInfo.Rows[e.RowIndex];

                prod = new Product();
                txtProdID.Text = myProdRow.Cells["ID"].Value.ToString();
                prod.ProductId = Int32.Parse(txtProdID.Text);
                prod.GetProductInfo();
                
                txtPName.Text = prod.Name;
                txtPCloth.Text = prod.ClothType; 
                txtPQuantity.Text = prod.Quantity.ToString();
                txtPDesc.Text = prod.Description;
                dropPSize.SelectedItem = prod.SizeFit;
                txtPRPrice.Text = prod.RentPrice.ToString();
                txtPAmount.Text = prod.Amount.ToString();
                dropPCateg.SelectedItem = prod.Category;
                dropPStatus.SelectedItem = prod.Status;
                
                DisplayProdLists();

                //toggle buttons for update
                btnProdAdd.Visible = false;
                btnProdAdd.Enabled = false;
                btnProdUpdate.Visible = true;
                btnProdUpdate.Enabled = true;

            }

        }

        
        #endregion

        #region Rentals Functions

        //display list of rentals
        public void DisplayAllRentals()
        {
            SelectList slist = new SelectList();
            slist.GetAllRentals();
            if (slist.DTable.Rows != null)
                dtRList.DataSource = slist.DTable;
            ClearRentLists();

            rent = null;
        }
        //display list of rentals by reference number
        public void DisplayAllRentalsByRefnum(string refnum)
        {
            SelectList slist = new SelectList();
            slist.GetRentalsByRefnum(refnum);
            if (slist.DTable.Rows != null)
                dtRList.DataSource = slist.DTable;

            //rent = null;
        }

        public void LoadRentalsCustList()
        {
            SelectList slist = new SelectList();
            slist.GetAllCustomers();

            slist.DTable.Columns.Add("Full Name", typeof(string));

            foreach (DataRow dr in slist.DTable.Rows)
            {
                dr["Full Name"] = dr["First Name"] + " " + dr["Last Name"];   // or set it to some other value
            }
            if (slist.DTable.Rows != null)
            {
                dropRCust.DataSource = slist.DTable;
                dropRCust.ValueMember = "ID";
                dropRCust.DisplayMember = "Full Name";

            }
            //dropRCust.SelectedIndex = -1;
        }

        //load customer details in rentals tab
        public void LoadRentalsCustSelected(int cid)
        {
            Customer rcust = new Customer();
            rcust.CustomerId = cid;
            rcust.GetCustomerInfo();
            rcust.DTable.Columns["customerid"].ColumnName = "ID";
            rcust.DTable.Columns["fname"].ColumnName = "First Name";
            rcust.DTable.Columns["lname"].ColumnName = "Last Name";
            rcust.DTable.Columns["address"].ColumnName = "Address";
            rcust.DTable.Columns["contactno"].ColumnName = "Contact No.";
            dtRCust.DataSource = rcust.DTable;

            dropRCust.SelectedValue = cid;
        }

        //displays customer details and products rented
        public void DisplayRentLists()
        {

            rent.GetCustomer();
            dtRCust.DataSource = rent.DTable;

            rent.GetProducts();
            dtRProd.DataSource = rent.DTable;
            lblProdRent.Text = "PRODUCTS RENTED (" + rent.DTable.Rows.Count.ToString() + ") :";

        }


        //clears customer details and products Sold
        public void ClearRentLists()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { 
            new DataColumn("No Transaction selected.", typeof(String)),
            });

            dtRCust.DataSource = dt;
            dtRProd.DataSource = dt;
            lblProdRent.Text = "PRODUCTS RENTED:";
        }

        //resets rentals buttons
        public void ResetRentButton()
        {
            //toggle buttons
            btnRAdd.Visible = true;
            btnRAdd.Enabled = true;
            btnRUpdate.Visible = false;
            btnRUpdate.Enabled = false;
            btnREditProd.Enabled = false;
            btnREditProd.Visible = false;

        }
        //clears rentals textboxes
        public void ClearRentText()
        {
            //clear the text boxes
            txtRSearch.Clear();
            txtRRefNum.Clear();
            //dropRCust.SelectedIndex = -1;
            dateRRentDate.ResetText();
            dateRUseDate.ResetText();
            dateRPickupDate.ResetText();
            dateRReturnDate.ResetText();
            txtRPurpose.Clear();
            txtRAmount.Value = txtRAmount.Minimum;
            txtRDown.Value = txtRDown.Minimum;
            txtRNotes.Clear();
        }

        private void txtRSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayAllRentalsByRefnum(txtRSearch.Text);
        }

        private void btnRNew_Click(object sender, EventArgs e)
        {
            ClearRentText();

            ResetRentButton();

            DisplayAllRentals();
        }

        private void btnRRefresh_Click(object sender, EventArgs e)
        {
            ClearRentText();

            ResetRentButton();

            DisplayAllRentals();
        }

        private void dropRCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRentalsCustSelected(Int32.Parse(dropRCust.SelectedValue.ToString()));
        }

        private void btnRUpdate_Click(object sender, EventArgs e)
        {
            if (dtRCust.RowCount == 1)
            {
                MessageBox.Show("Please choose a customer.");
            }
            else if (txtRPurpose.Text == "")
            {
                MessageBox.Show("Please provide purpose of the transaction.");
            }
            else if (txtRNotes.Text == "")
            {
                MessageBox.Show("Please provide any notes for this transaction.");
            }
            else
            {
                rent = new Rental();
                rent.ReferenceNo = txtRRefNum.Text;
                rent.GetInfoByRefNum();
                rent.CustomerId = Int32.Parse(dropRCust.SelectedValue.ToString());
                rent.DateOfRental = dateRRentDate.Value.Date;

                rent.DateOfUse = dateRUseDate.Value.Date;
                rent.DateOfPickUp = dateRPickupDate.Value.Date;
                rent.DateOfReturn = dateRReturnDate.Value.Date;
                rent.Purpose = txtRPurpose.Text;
                rent.TotalAmount = Double.Parse(txtRAmount.Text);
                rent.Downpayment = Double.Parse(txtRDown.Text);
                rent.Notes = txtRNotes.Text;
                rent.UpdateTransaction();

                MessageBox.Show("Transaction Updated!");

                DisplayAllRentalsByRefnum(txtRSearch.Text);
            }
        }


        private void btnREditProd_Click(object sender, EventArgs e)
        {
            //edit products to be rented
            SelectProducts sp = new SelectProducts(rent, "edit");
            sp.ShowDialog();
            bool done = sp.isDone;
            if (done)
            {
                //updates the total amount view and balance
                txtRAmount.Text = rent.TotalAmount.ToString();
                double balance = Double.Parse(txtRAmount.Text) - Double.Parse(txtRDown.Text);
                txtRBalance.Text = balance.ToString();

                //refresh the products rented list
                DisplayRentLists();
            }
        }

        private void btnRAdd_Click(object sender, EventArgs e)
        {
            if (dtRCust.RowCount == 1)
            {
                MessageBox.Show("Please choose a customer.");
            }
            else if (txtRPurpose.Text == "")
            {
                MessageBox.Show("Please provide purpose of the transaction.");
            }
            else if (txtRNotes.Text == "")
            {
                MessageBox.Show("Please provide any notes for this transaction.");
            }
            else
            {
                rent = new Rental();
                rent.CustomerId = Int32.Parse(dropRCust.SelectedValue.ToString());
                rent.DateOfRental = dateRRentDate.Value.Date;

                //gets max rentals id for creating rentals reference no.
                SelectList slist = new SelectList();
                slist.GetMaxRentalsID();

                DataRow rRow = slist.DTable.Rows[0];
                int count = 0;
                if (rRow.IsNull("Max"))
                    count = 4001;
                else
                {

                    count = Int32.Parse(rRow["Max"].ToString()) + 1;
                }

                rent.ReferenceNo = "R" + "-" + rent.DateOfRental.Year + "-" + rent.CustomerId + "-" + count;

                rent.DateOfUse = dateRUseDate.Value.Date;
                rent.DateOfPickUp = dateRPickupDate.Value.Date;
                rent.DateOfReturn = dateRReturnDate.Value.Date;
                rent.Purpose = txtRPurpose.Text;
                //rent.TotalAmount = Double.Parse(txtRAmount.Text);
                rent.Downpayment = Double.Parse(txtRDown.Text);
                rent.Notes = txtRNotes.Text;

                //selects products to be rented
                SelectProducts sp = new SelectProducts(rent, "add");
                sp.ShowDialog();
                bool done = sp.isDone;
                if (done)
                {
                    //refresh the rentals page
                    ClearRentText();
                    ClearRentLists();
                    DisplayAllRentals();
                }
            }

        }


        private void txtRDown_ValueChanged(object sender, EventArgs e)
        {
            double balance = Double.Parse(txtRAmount.Text) - Double.Parse(txtRDown.Text);
            txtRBalance.Text = balance.ToString();
        }

        private void dtRList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow rRow = dtRList.Rows[e.RowIndex];

                //instantiating class
                rent = new Rental();
                txtRRefNum.Text = rRow.Cells["Reference No."].Value.ToString();
                rent.ReferenceNo = txtRRefNum.Text;
                rent.GetInfoByRefNum();

                dropRCust.SelectedValue = rent.CustomerId;
                dateRRentDate.Text = rent.DateOfRental.ToShortDateString();
                dateRUseDate.Text = rent.DateOfUse.ToShortDateString();
                dateRPickupDate.Text = rent.DateOfPickUp.ToShortDateString();
                dateRReturnDate.Text = rent.DateOfReturn.ToShortDateString();
                txtRPurpose.Text = rent.Purpose;
                txtRAmount.Text = rent.TotalAmount.ToString();
                txtRDown.Text = rent.Downpayment.ToString();
                txtRNotes.Text = rent.Notes;

                DisplayRentLists();

                //toggle buttons
                btnRAdd.Visible = false;
                btnRAdd.Enabled = false;
                btnRUpdate.Visible = true;
                btnRUpdate.Enabled = true;
                btnREditProd.Enabled = true;
                btnREditProd.Visible = true;

            }
        }

        #endregion

        #region Sales Functions

        //display list of sales
        public void DisplayAllSales()
        {
            SelectList slist = new SelectList();
            slist.GetAllSales();
            if (slist.DTable.Rows != null)
                dtSList.DataSource = slist.DTable;
            ClearSalesLists();

            sales = null;
        }
        //display list of sales by reference number
        public void DisplayAllSalesByRefnum(string refnum)
        {
            SelectList slist = new SelectList();
            slist.GetSalesByRefnum(refnum);
            if (slist.DTable.Rows != null)
                dtSList.DataSource = slist.DTable;

            //sales = null;
        }

        //loads drop down customer list
        public void LoadSalesCustList()
        {
            SelectList slist = new SelectList();
            slist.GetAllCustomers();

            slist.DTable.Columns.Add("Full Name", typeof(string));

            foreach (DataRow dr in slist.DTable.Rows)
            {
                dr["Full Name"] = dr["First Name"] + " " + dr["Last Name"];   // or set it to some other value
            }
            if (slist.DTable.Rows != null)
            {
                dropSCust.DataSource = slist.DTable;
                dropSCust.ValueMember = "ID";
                dropSCust.DisplayMember = "Full Name";

            }
            //dropRCust.SelectedIndex = -1;
        }

        //load customer details in sales tab
        public void LoadSalesCustSelected(int cid)
        {
            Customer scust = new Customer();
            scust.CustomerId = cid;
            scust.GetCustomerInfo();
            scust.DTable.Columns["customerid"].ColumnName = "ID";
            scust.DTable.Columns["fname"].ColumnName = "First Name";
            scust.DTable.Columns["lname"].ColumnName = "Last Name";
            scust.DTable.Columns["address"].ColumnName = "Address";
            scust.DTable.Columns["contactno"].ColumnName = "Contact No.";
            dtSCust.DataSource = scust.DTable;

            dropSCust.SelectedValue = cid;
        }

        //displays customer details and products sold list
        public void DisplaySalesLists()
        {

            sales.GetCustomer();
            dtSCust.DataSource = sales.DTable;

            sales.GetProducts();
            dtSProd.DataSource = sales.DTable;
            lblSProdSold.Text = "PRODUCTS SOLD (" + sales.DTable.Rows.Count.ToString() + ") :";

        }


        //clears customer details and products sold
        public void ClearSalesLists()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { 
            new DataColumn("No Transaction selected.", typeof(String)),
            });

            dtSCust.DataSource = dt;
            dtSProd.DataSource = dt;
            lblSProdSold.Text = "PRODUCTS SOLD:";
        }

        //resets sales buttons
        public void ResetSalesButton()
        {
            //toggle buttons
            btnSAdd.Visible = true;
            btnSAdd.Enabled = true;
            btnSUpdate.Visible = false;
            btnSUpdate.Enabled = false;
            btnSEditProd.Enabled = false;
            btnSEditProd.Visible = false;

        }
        //clears sales textboxes
        public void ClearSalesText()
        {
            //clear the text boxes
            txtSSearch.Clear();
            txtSRefNum.Clear();
            //dropRCust.SelectedIndex = -1;
            dateSPurchase.ResetText();
            txtSAmount.Value = txtSAmount.Minimum;
            txtSNotes.Clear();
        }

        private void btnSNew_Click(object sender, EventArgs e)
        {
            ClearSalesText();

            ResetSalesButton();

            DisplayAllSales();
        }

        private void btnSRefresh_Click(object sender, EventArgs e)
        {
            ClearSalesText();

            ResetSalesButton();

            DisplayAllSales();
        }

        private void txtSSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayAllSalesByRefnum(txtSSearch.Text);
        }

        private void dropSCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesCustSelected(Int32.Parse(dropSCust.SelectedValue.ToString()));
        }

        private void btnSUpdate_Click(object sender, EventArgs e)
        {
            if (dtSCust.RowCount == 1)
            {
                MessageBox.Show("Please choose a customer.");
            }
            else if (txtSNotes.Text == "")
            {
                MessageBox.Show("Please provide any notes for this transaction.");
            }
            else
            {
                sales = new Sales();
                sales.ReferenceNo = txtSRefNum.Text;
                sales.GetInfoByRefNum();
                sales.CustomerId = Int32.Parse(dropSCust.SelectedValue.ToString());
                sales.DateOfPurchase = dateSPurchase.Value.Date;
                sales.TotalAmount = Double.Parse(txtSAmount.Text);
                sales.Notes = txtSNotes.Text;
                sales.UpdateTransaction();

                MessageBox.Show("Transaction Updated!");

                DisplayAllSalesByRefnum(txtSSearch.Text);
            }
        }

        private void btnSEditProd_Click(object sender, EventArgs e)
        {
            //edit products that were sold
            SelectProducts sp = new SelectProducts(sales, "edit");
            sp.ShowDialog();
            bool done = sp.isDone;
            if (done)
            {
                //updates the total amount view
                txtSAmount.Text = sales.TotalAmount.ToString();

                //refresh the products sold list
                DisplaySalesLists();
            }
        }

        private void btnSAdd_Click(object sender, EventArgs e)
        {
            if (dtSCust.RowCount == 1)
            {
                MessageBox.Show("Please choose a customer.");
            }
            else if (txtSNotes.Text == "")
            {
                MessageBox.Show("Please provide any notes for this transaction.");
            }
            else
            {
                sales = new Sales();
                sales.CustomerId = Int32.Parse(dropSCust.SelectedValue.ToString());
                sales.DateOfPurchase = dateSPurchase.Value.Date;

                //gets max sales id for creating sales reference no.
                SelectList slist = new SelectList();
                slist.GetMaxSalesID();

                DataRow sRow = slist.DTable.Rows[0];
                int count = 0;
                if (sRow.IsNull("Max"))
                {
                    count = 5001;
                }
                else
                {
                    count = Int32.Parse(sRow["Max"].ToString()) + 1;
                }

                sales.ReferenceNo = "S" + "-" + sales.DateOfPurchase.Year + "-" + sales.CustomerId + "-" + count;
                sales.Notes = txtSNotes.Text;

                //selects products to be sold
                SelectProducts sp = new SelectProducts(sales, "add");
                sp.ShowDialog();
                bool done = sp.isDone;
                if (done)
                {
                    //refresh the sales page
                    ClearSalesText();
                    ClearSalesLists();
                    DisplayAllSales();
                }

            }
        }

        private void dtSList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow sRow = dtSList.Rows[e.RowIndex];

                //instantiating class
                sales = new Sales();
                txtSRefNum.Text = sRow.Cells["Reference No."].Value.ToString();
                sales.ReferenceNo = txtSRefNum.Text;
                sales.GetInfoByRefNum();

                dropSCust.SelectedValue = sales.CustomerId;
                dateSPurchase.Text = sales.DateOfPurchase.ToShortDateString();
                txtSAmount.Text = sales.TotalAmount.ToString();
                txtSNotes.Text = sales.Notes;

                DisplaySalesLists();

                //toggle buttons
                btnSAdd.Visible = false;
                btnSAdd.Enabled = false;
                btnSUpdate.Visible = true;
                btnSUpdate.Enabled = true;
                btnSEditProd.Enabled = true;
                btnSEditProd.Visible = true;

            }
        }



        #endregion

        #region Orders Functions


        //display list of orders
        public void DisplayAllOrders()
        {
            SelectList slist = new SelectList();
            slist.GetAllOrders();
            if (slist.DTable.Rows != null)
                dtOList.DataSource = slist.DTable;
            ClearOrdersLists();

            order = null;
        }
        //display list of orders by reference number
        public void DisplayAllOrdersByRefnum(string refnum)
        {
            SelectList slist = new SelectList();
            slist.GetOrdersByRefnum(refnum);
            if (slist.DTable.Rows != null)
                dtOList.DataSource = slist.DTable;

            //order = null;
        }

        public void LoadOrdersCustList()
        {
            SelectList slist = new SelectList();
            slist.GetAllCustomers();

            slist.DTable.Columns.Add("Full Name", typeof(string));

            foreach (DataRow dr in slist.DTable.Rows)
            {
                dr["Full Name"] = dr["First Name"] + " " + dr["Last Name"];   // or set it to some other value
            }
            if (slist.DTable.Rows != null)
            {
                dropOCust.DataSource = slist.DTable;
                dropOCust.ValueMember = "ID";
                dropOCust.DisplayMember = "Full Name";

            }
            //dropRCust.SelectedIndex = -1;
        }

        //load customer details in orders tab
        public void LoadOrdersCustSelected(int cid)
        {
            Customer ocust = new Customer();
            ocust.CustomerId = cid;
            ocust.GetCustomerInfo();
            ocust.DTable.Columns["customerid"].ColumnName = "ID";
            ocust.DTable.Columns["fname"].ColumnName = "First Name";
            ocust.DTable.Columns["lname"].ColumnName = "Last Name";
            ocust.DTable.Columns["address"].ColumnName = "Address";
            ocust.DTable.Columns["contactno"].ColumnName = "Contact No.";
            dtOCust.DataSource = ocust.DTable;

            dropOCust.SelectedValue = cid;
        }

        //displays customer details and product details
        public void DisplayOrdersLists()
        {

            order.GetCustomer();
            dtOCust.DataSource = order.DTable;

            order.GetProducts();
            dtOProduct.DataSource = order.DTable;
            
        }


        //clears customer details and product details
        public void ClearOrdersLists()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { 
            new DataColumn("No Transaction selected.", typeof(String)),
            });

            dtOCust.DataSource = dt;
            dtOProduct.DataSource = dt;
        }

        //resets orders buttons
        public void ResetOrdersButton()
        {
            //toggle buttons
            btnOAdd.Visible = true;
            btnOAdd.Enabled = true;
            btnOUpdate.Visible = false;
            btnOUpdate.Enabled = false;
            
        }
        //clears orders textboxes
        public void ClearOrdersText()
        {
            //clear the text boxes
            txtOSearch.Clear();
            txtORefNum.Clear();
            //dropRCust.SelectedIndex = -1;
            dateOrderDate.ResetText();
            dateOUseDate.ResetText();
            dateOPickup.ResetText();
            txtOPurpose.Clear();
            txtOAmount.Value = txtRAmount.Minimum;
            txtODown.Value = txtRDown.Minimum;
            txtONotes.Clear();
        }

        private void btnONew_Click(object sender, EventArgs e)
        {
            ClearOrdersText();

            ResetOrdersButton();

            DisplayAllOrders();
        }

        private void btnORefresh_Click(object sender, EventArgs e)
        {
            ClearOrdersText();

            ResetOrdersButton();

            DisplayAllOrders();
        }

        private void txtOSearch_TextChanged(object sender, EventArgs e)
        {
            DisplayAllOrdersByRefnum(txtOSearch.Text);
        }

        private void dropOCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrdersCustSelected(Int32.Parse(dropOCust.SelectedValue.ToString()));
        }

        private void txtODown_ValueChanged(object sender, EventArgs e)
        {
            double balance = Double.Parse(txtOAmount.Text) - Double.Parse(txtODown.Text);
            txtOBalance.Text = balance.ToString();
        }

        private void btnOUpdate_Click(object sender, EventArgs e)
        {
            if (dtOCust.RowCount == 1)
            {
                MessageBox.Show("Please choose a customer.");
            }
            else if (txtOPurpose.Text == "")
            {
                MessageBox.Show("Please provide purpose of the transaction.");
            }
            else if (txtONotes.Text == "")
            {
                MessageBox.Show("Please provide any notes for this transaction.");
            }
            else
            {
                order = new MadeToOrder();
                order.ReferenceNo = txtORefNum.Text;
                order.GetInfoByRefNum();
                order.CustomerId = Int32.Parse(dropOCust.SelectedValue.ToString());
                order.DateOfOrder = dateOrderDate.Value.Date;

                order.DateOfUse = dateOUseDate.Value.Date;
                order.DateOfPickUp = dateOPickup.Value.Date;
                order.Purpose = txtOPurpose.Text;
                order.TotalAmount = Double.Parse(txtOAmount.Text);
                order.Downpayment = Double.Parse(txtODown.Text);
                order.Notes = txtONotes.Text;
                order.UpdateTransaction();

                MessageBox.Show("Transaction Updated!");

                DisplayAllOrdersByRefnum(txtRSearch.Text);
            }
        }

        private void btnOAdd_Click(object sender, EventArgs e)
        {
            if (dtOCust.RowCount == 1)
            {
                MessageBox.Show("Please choose a customer.");
            }
            else if (txtOPurpose.Text == "")
            {
                MessageBox.Show("Please provide purpose of the transaction.");
            }
            else if (txtONotes.Text == "")
            {
                MessageBox.Show("Please provide any notes for this transaction.");
            }
            else
            {
                order = new MadeToOrder();
                order.CustomerId = Int32.Parse(dropOCust.SelectedValue.ToString());
                order.DateOfOrder = dateOrderDate.Value.Date;

                //gets max order number for creating order reference no.
                SelectList slist = new SelectList();
                slist.GetMaxOrdersID();

                DataRow oRow = slist.DTable.Rows[0];
                int count = 0;
                if (oRow.IsNull("Max"))
                    count = 6001;
                else
                {

                    count = Int32.Parse(oRow["Max"].ToString()) + 1;
                }

                order.ReferenceNo = "R" + "-" + order.DateOfOrder.Year + "-" + order.CustomerId + "-" + count;

                order.DateOfUse = dateOUseDate.Value.Date;
                order.DateOfPickUp = dateOPickup.Value.Date;
                order.Purpose = txtOPurpose.Text;
                order.TotalAmount = Double.Parse(txtOAmount.Text);
                order.Downpayment = Double.Parse(txtODown.Text);
                order.Notes = txtONotes.Text;

                //proceed to the product details
                ProductOrdered po = new ProductOrdered(order,"add");
                po.ShowDialog();
                bool done = po.isDone;
                if (done)
                {
                    //refresh the rentals page
                    ClearOrdersText();
                    ClearOrdersLists();
                    DisplayAllOrders();
                }
            }
        }

        private void dtOList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow oRow = dtOList.Rows[e.RowIndex];

                //instantiating class
                order = new MadeToOrder();
                txtORefNum.Text = oRow.Cells["Reference No."].Value.ToString();
                order.ReferenceNo = txtORefNum.Text;
                order.GetInfoByRefNum();

                dropOCust.SelectedValue = order.CustomerId;
                dateOrderDate.Text = order.DateOfOrder.ToShortDateString();
                dateOUseDate.Text = order.DateOfUse.ToShortDateString();
                dateOPickup.Text = order.DateOfPickUp.ToShortDateString();
                txtOPurpose.Text = order.Purpose;
                txtOAmount.Text = order.TotalAmount.ToString();
                txtODown.Text = order.Downpayment.ToString();
                txtONotes.Text = order.Notes;

                DisplayOrdersLists();

                //toggle buttons
                btnOAdd.Visible = false;
                btnOAdd.Enabled = false;
                btnOUpdate.Visible = true;
                btnOUpdate.Enabled = true;
                
            }
        }

        private void dtOProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //edit product ordered
                ProductOrdered po = new ProductOrdered(order, "edit");
                po.ShowDialog();
                bool done = po.isDone;
                if (done)
                {
                    //refresh the product details
                    DisplayOrdersLists();
                }
            }
        }

        #endregion

    }
}
