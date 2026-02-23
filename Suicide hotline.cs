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
    public partial class Suicide_hotline: Form
    {
        public Suicide_hotline()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Lohout_Click(object sender, EventArgs e)
        {
            Login L = new Login();
            L.Show();
            this.Hide();
        }

      

        private void Suicide_hotline_Load(object sender, EventArgs e)
        {
            username.Text = Login.loggedUser;
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
