using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace Mind_care
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }

        public static string hash(string username)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(username));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        Login l = new Login();

        private void button1_Click(object sender, EventArgs e)
        {
            Function f = new Function();

            string firstEmail = Email.Text.Substring(0, 1);
            string thirdPass = Email.Text.Substring(2, 1);
            string username = firstEmail + Email.TextLength + thirdPass;

            string hashedPwd = hash(Password.Text);



            if (f.verifyEmail(Email.Text))
            {
                MessageBox.Show("User Already exist");
            }
            else if (f.registerUser(Email.Text, hashedPwd, username))
            {
                MessageBox.Show(username);
                MessageBox.Show("user register");


                l.Show();
                this.Hide();
            }
        }




        private void Register_Load(object sender, EventArgs e)
        {
            Email.Focus();
        }
    }
        }
