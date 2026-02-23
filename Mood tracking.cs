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
    public partial class Mood_tracking: Form
    {
        public Mood_tracking()
        {
            InitializeComponent();
        }
        string mood;
        private void Mood_tracking_Load(object sender, EventArgs e)
        {
            username.Text = Login.loggedUser;
        }

        private void moodSet(object sender, EventArgs e)
        {
            RadioButton btn = (RadioButton)sender;

            mood= btn.Text;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            Function f = new Function();
            string username = Login.loggedUser; 
            string mood = GetSelectedMood(); 

            if (f.IsDataSubmittedToday(username))
            {
                MessageBox.Show("You have already submitted your mood for today. Please try again tomorrow.");
            }
            else
            {
                if (f.SubmitMoodData(username, mood))
                {
                    MessageBox.Show("Your mood has been successfully recorded.");
                }
                else
                {
                    MessageBox.Show("There was an error submitting your mood.");
                }
            }
        }

        private string GetSelectedMood()
        {
            return mood;  
        }

        private void Lohout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
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
    }
}
