using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mind_care
{
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string loggedUser;
       
        private void button1_Click(object sender, EventArgs e)
        {
            Function f = new Function(); // Instantiate Function class
            string username = Username.Text;
            string password = Password.Text;

            if (f.loginUser(username, password))
            {
                loggedUser = username;
                MessageBox.Show("Login Successful!");
                this.Hide();
                Mood_tracking m = new Mood_tracking();
                m.Show();
                
            }
            else
            {
                MessageBox.Show("Invalid Email or Password.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register r = new Register();
            r.Show();
        }
    }
}
