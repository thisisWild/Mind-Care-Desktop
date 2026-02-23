using MySql.Data.MySqlClient;
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
    public partial class Analytics: Form
    {
        private Conn db;
        public Analytics()
        {
            InitializeComponent();
            db = new Conn();
        }

        private void Lohout_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }


        private void LoadMoodData(string username)
        {
            string query = "SELECT `datetime`, `mood` FROM `analytics` WHERE `username` = @username ORDER BY `datetime` ASC";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection);

            cmd.Parameters.AddWithValue("@username", username);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            try
            {
                db.openConnect();
                dataAdapter.Fill(dataTable);
                db.closeConnect();

                chart1.Series.Clear(); // Clear existing series
                chart1.Series.Add("MoodData");
                chart1.Series["MoodData"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                // Clear previous labels
                chart1.ChartAreas[0].AxisY.CustomLabels.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["datetime"]);
                    string mood = row["mood"].ToString();
                    int moodValue = MapMoodToValue(mood);

                    chart1.Series["MoodData"].Points.AddXY(date, moodValue);
                }

                // Format X-axis as date
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MM/dd/yyyy";
                chart1.ChartAreas[0].AxisY.Title = "Mood Level";
                chart1.ChartAreas[0].AxisX.Title = "Date";

                // Set mood labels on Y-axis
                chart1.ChartAreas[0].AxisY.CustomLabels.Add(0.5, 1.5, "Happy");
                chart1.ChartAreas[0].AxisY.CustomLabels.Add(1.5, 2.5, "Neutral");
                chart1.ChartAreas[0].AxisY.CustomLabels.Add(2.5, 3.5, "Sad");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading mood data: " + ex.Message);
            }
        }

        private int MapMoodToValue(string mood)
        {
            switch (mood.ToLower())
            {
                case "happy": return 1;
                case "neutral": return 2;
                case "sad": return 3;
                default: return 0; // Default to neutral
            }
        }


        private void Analytics_Load(object sender, EventArgs e)
        {
            username.Text = Login.loggedUser;
            if (string.IsNullOrEmpty(Login.loggedUser))
            {
                MessageBox.Show("Error: No user is logged in.");
                return; // Stop execution if no user is logged in
            }

            LoadMoodData(Login.loggedUser);
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
    }
}
