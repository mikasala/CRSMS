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
    public abstract class Transaction : DatabaseConnection
    {
        private int _transactionId;
        private string _referenceNo;
        private int _customerId;
        private double _totalAmount;
        private double _downpayment;
        private string _notes;

        #region Constructors

        public Transaction()
        {
            _transactionId = 1;
            _referenceNo = "";
            _customerId = 1;
            _totalAmount = 1.00;
            _downpayment = 0.00;
            _notes = "";
        }

        #endregion

        #region Properties

        public int TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }


        public string ReferenceNo
        {
            get { return _referenceNo; }
            set { _referenceNo = value; }
        }

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }


        public Double TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public Double Downpayment
        {
            get { return _downpayment; }
            set { _downpayment = value; }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        #endregion

        #region Methods

        public abstract void GetInfo();
        public abstract void GetInfoByRefNum();
        public abstract void GetCustomer();
        public abstract void GetProducts();
        public abstract void SaveTransaction();
        public abstract void UpdateTransaction();

        #endregion
    }
}
