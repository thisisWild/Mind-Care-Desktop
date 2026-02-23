using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Mind_care
{
    public partial class projectspace: Form
    {
        private Conn db;
        public projectspace()
        {
            InitializeComponent();
            db = new Conn();
        }

    
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Donate_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://mindcare-brown.vercel.app/");
        }

        private void Lohout_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();

        }

        private void projectspace_Load(object sender, EventArgs e)
        {
            FillDataGridView();
            username.Text = Login.loggedUser;
        }

        public void FillDataGridView()
        {
            // Create a connection to the database
            string query = "SELECT * FROM `projects`"; // Modify this to your query based on the data you want
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, db.GetConnection);
            DataTable dataTable = new DataTable(); // DataTable to hold the fetched data

            try
            {
                db.openConnect(); // Open the database connection
                dataAdapter.Fill(dataTable); // Fill the DataTable with data from the query
                db.closeConnect(); // Close the connection

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mood_tracking m = new Mood_tracking();
            m.Show();
        }

        private void chatbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Chat C = new Chat();
            C.Show();
        }

        private void hotlinebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Suicide_hotline sh = new Suicide_hotline();
            sh.Show();
        }

        private void AIbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            AI a = new AI();
            a.Show();
        }

        private void Reportbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analytics r = new Analytics();
            r.Show();
        }

        private void Projectbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            projectspace p = new projectspace();
            p.Show();
        }

        private void username_Click(object sender, EventArgs e)
        {

        }
    }
}
