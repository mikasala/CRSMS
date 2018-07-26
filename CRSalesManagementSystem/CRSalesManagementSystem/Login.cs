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
    public partial class Login : Form
    {

        DatabaseConnection dbConnection = new DatabaseConnection();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

            if (!dbConnection.IsConnected())
            {
                MessageBox.Show("Can't access the server. System will shut down.");
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserAccount ua = new UserAccount(txtUsername.Text, txtPassword.Text);

            if (!ua.IsAdmin())
            {
                MessageBox.Show("Username or Password is incorrect");
                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Welcome " + ua.FirstName + " " + ua.LastName + "!");
                this.Hide();

                Dashboard db = new Dashboard();
                db.ShowDialog();

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
