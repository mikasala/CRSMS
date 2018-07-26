using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRSalesManagementSystem
{
    public partial class SelectProducts : Form
    {
        Rental rent;
        int[] productsSelected;

        Sales sales;
        string type;
        //contructors
        public SelectProducts(Rental r, string toDo)
        {
            InitializeComponent();
            rent = r;
            type = toDo;
            sales = null;
            
        }
        public SelectProducts(Sales s, string toDo)
        {
            InitializeComponent();
            sales = s;
            type = toDo;
            rent = null;
        }

        //Property
        //will tell Dashboard that transaction has been completed
        public bool isDone { get; private set; }

        public void LoadSelProdList()
        {
            SelectList slist = new SelectList();
            slist.GetAllProductsAvailable();
            dtSelProdList.DataSource = slist.DTable;

            //inserting checkbox control
            DataGridViewCheckBoxColumn chkbox = new DataGridViewCheckBoxColumn()
            {
                Name = "Check",
                FalseValue = 0,
                TrueValue = 1,                
                Visible = true
            };
            dtSelProdList.Columns.Insert(0,chkbox);

            //initializing checkboxes be unchecked
            foreach (DataGridViewRow row in dtSelProdList.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = chk.FalseValue;
                
            }
        }

        public void LoadEditProdList()
        {
            int i;
            
            //getting first already included products in a transaction
            if(rent != null){
                rent.GetProducts();

                //productsSelected = new int[rent.DTable.Rows.Count];
            
                DataRow rRow;
                for (i = 0; i < rent.DTable.Rows.Count; i++)
                {
                    rRow = rent.DTable.Rows[i];
                    productsSelected[i] = Int32.Parse(rRow["ID"].ToString());
                }
            }

            if (sales != null)
            {
                sales.GetProducts();

                //productsSelected = new int[sales.DTable.Rows.Count];

                DataRow sRow;
                for (i = 0; i < sales.DTable.Rows.Count; i++)
                {
                    sRow = sales.DTable.Rows[i];
                    productsSelected[i] = Int32.Parse(sRow["ID"].ToString());
                }
            }

            SelectList slist = new SelectList();
            slist.GetAllProductsAvailable();
            dtSelProdList.DataSource = slist.DTable;

            //inserting checkbox control
            DataGridViewCheckBoxColumn chkbox = new DataGridViewCheckBoxColumn()
            {
                Name = "Check",
                FalseValue = 0,
                TrueValue = 1,
                Visible = true
            };
            dtSelProdList.Columns.Insert(0, chkbox);
            
            
            //initializing checkboxes be checked
            foreach (DataGridViewRow row in dtSelProdList.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (row.Cells["ID"].Value != null)
                {
                    
                    for (i = 0; i < productsSelected.Length; i++)
                    {
                        if (Int32.Parse(row.Cells[1].Value.ToString()) == productsSelected[i])
                        {
                            chk.Value = chk.TrueValue;
                            break;
                        }
                        else chk.Value = chk.FalseValue;
                    }
                }
            }
        }

        private void SelectProducts_Load(object sender, EventArgs e)
        {
            SelectList slist = new SelectList();
            slist.GetAllProductsAvailable();
            
            productsSelected = new int[slist.DTable.Rows.Count];

            for (int i = 0; i < slist.DTable.Rows.Count; i++ )
                productsSelected[i] = 0;

            if (type == "add")
                LoadSelProdList();
            else LoadEditProdList();
        }


        private void txtPSearch_TextChanged(object sender, EventArgs e)
        {
            int i;

            SelectList slist = new SelectList();
            slist.GetProductsAvailableByName(txtPSearch.Text);
            if (slist.DTable.Rows != null)
                dtSelProdList.DataSource = slist.DTable;

            //initializing checkboxes be checked
            foreach (DataGridViewRow row in dtSelProdList.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (row.Cells["ID"].Value != null)
                {
                    for (i = 0; i < productsSelected.Length; i++)
                    {
                        if (Int32.Parse(row.Cells[1].Value.ToString()) == productsSelected[i])
                        {
                            chk.Value = chk.TrueValue;
                            break;
                        }
                        else chk.Value = chk.FalseValue;
                    }
                }
            }
            
        }
        
        private void dtSelProdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int count = dtSelProdList.RowCount;

            if (e.RowIndex >= 0 && e.RowIndex < count - 1)
            {
                //toggles the checkbox
                DataGridViewRow row = dtSelProdList.Rows[e.RowIndex];
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Check"];

                if (chk.Value == chk.TrueValue)
                {
                    for (int i = 0; i < productsSelected.Length; i++)
                    {
                        if (Int32.Parse(row.Cells[1].Value.ToString()) == productsSelected[i])
                        {
                            productsSelected[i] = 0;
                            break;
                        }
                    }
                    chk.Value = chk.FalseValue;
                }
                else
                {
                    for (int i = 0; i < productsSelected.Length; i++)
                    {
                        if (productsSelected[i] == 0)
                        {
                            productsSelected[i] = Int32.Parse(row.Cells[1].Value.ToString());
                            break;
                        }
                    }
                    chk.Value = chk.TrueValue;
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //calculate first total amount
            Product p;
            double total = 0.00;
            

            if (rent != null)
            {
                foreach (DataGridViewRow row in dtSelProdList.Rows)
                {
                    if (row.Cells["ID"].Value != null)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (chk.Value == chk.TrueValue)
                        {
                            int prodid = Int32.Parse(row.Cells["ID"].Value.ToString());
                            p = new Product();
                            p.ProductId = prodid;
                            p.GetProductInfo();
                            total += p.RentPrice;
                        }
                    }

                }
                rent.TotalAmount = total;

                //if EDITting products rented
                if (type == "edit")
                {
                    //updates the total amount of the selected rent
                    rent.UpdateTransaction();

                    //unlinks all products to the rent id
                    TransactionProduct transprod = new TransactionProduct();
                    transprod.TransactionId = rent.TransactionId;
                    transprod.UnLinkRentProducts();

                }
                else //adding NEW rent transaction
                {
                    //saves the transaction of new rental
                    rent.SaveTransaction();

                    //gets the rent id 
                    SelectList slist = new SelectList();
                    slist.GetMaxRentalsID();
                    DataRow rRow = slist.DTable.Rows[0];

                    rent.TransactionId = Int32.Parse(rRow["Max"].ToString());
                }

                //then links the products selected
                TransactionProduct tp;
                foreach (DataGridViewRow row in dtSelProdList.Rows)
                {
                    if (row.Cells["ID"].Value != null)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (chk.Value == chk.TrueValue)
                        {
                            tp = new TransactionProduct();
                            tp.TransactionId = rent.TransactionId;

                            tp.ProductId = Int32.Parse(row.Cells["ID"].Value.ToString());

                            tp.LinkRentProduct();
                        }
                    }

                }
                if(type=="add")
                    MessageBox.Show("Transaction Complete!");
                else MessageBox.Show("Transaction Updated!");
                isDone = true;
                this.Close();
                
            }

            if (sales != null)
            {
                foreach (DataGridViewRow row in dtSelProdList.Rows)
                {
                    if (row.Cells["ID"].Value != null)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (chk.Value == chk.TrueValue)
                        {
                            int prodid = Int32.Parse(row.Cells["ID"].Value.ToString());
                            p = new Product();
                            p.ProductId = prodid;
                            p.GetProductInfo();
                            total += p.Amount;
                        }
                    }

                }
                sales.TotalAmount = total;

                //if EDITting products sold
                if (type == "edit")
                {
                    //updates the total amount of the selected rent
                    sales.UpdateTransaction();

                    //unlinks all products to the sales id
                    TransactionProduct transprod = new TransactionProduct();
                    transprod.TransactionId = sales.TransactionId;
                    transprod.UnLinkSalesProducts();

                }
                else //adding NEW sales transaction
                {
                    //saves the transaction of new sales
                    sales.SaveTransaction();

                    //gets the rent id 
                    SelectList slist = new SelectList();
                    slist.GetMaxSalesID();
                    DataRow rRow = slist.DTable.Rows[0];

                    sales.TransactionId = Int32.Parse(rRow["Max"].ToString());
                }

                //then links the products selected
                TransactionProduct tp;
                foreach (DataGridViewRow row in dtSelProdList.Rows)
                {
                    if (row.Cells["ID"].Value != null)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (chk.Value == chk.TrueValue)
                        {
                            tp = new TransactionProduct();
                            tp.TransactionId = sales.TransactionId;

                            tp.ProductId = Int32.Parse(row.Cells["ID"].Value.ToString());

                            tp.LinkSalesProduct();
                        }
                    }

                }
                if (type == "add")
                    MessageBox.Show("Transaction Complete!");
                else MessageBox.Show("Transaction Updated!");
                isDone = true;
                this.Close();
                
            }
        }

    }
}
