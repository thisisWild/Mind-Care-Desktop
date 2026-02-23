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
    public partial class Chat: Form
    {
        private Conn db;
        public Chat()
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


        private void Chat_Load(object sender, EventArgs e)
        {
            username.Text = Login.loggedUser;
            LoadQuestions();
            LoadQuestionsWithResponses();

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

        private void LoadQuestionsWithResponses()
        {
            string query = @"SELECT 
        q.question_id,  
        q.question_text,  
        r.username,  
        r.response_text  
    FROM questions q
    LEFT JOIN responses r ON q.question_id = r.question_id 
    ORDER BY q.question_id ASC, r.created_at ASC;"; 

            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                db.openConnect();
                adapter.Fill(dt);
                db.closeConnect();

                chatListView.Items.Clear();
                chatListView.View = View.Details;  
                chatListView.Columns.Clear();
                chatListView.Columns.Add("Conversation", 600);  

                int previousQuestionId = -1;

                foreach (DataRow row in dt.Rows)
                {
                    int questionId = Convert.ToInt32(row["question_id"]);
                    string questionText = row["question_text"].ToString();
                    string username = row["username"]?.ToString() ?? "No responses yet";
                    string responseText = row["response_text"]?.ToString() ?? "";

                    if (questionId != previousQuestionId)
                    {
                        
                        ListViewItem questionItem = new ListViewItem($"Q: {questionText}");
                        questionItem.Font = new Font(chatListView.Font, FontStyle.Bold);
                        questionItem.ForeColor = Color.DarkBlue;
                        chatListView.Items.Add(questionItem);
                    }

                    if (!string.IsNullOrEmpty(responseText))
                    {
                        
                        ListViewItem responseItem = new ListViewItem($"   → {username}: {responseText}");
                        responseItem.ForeColor = Color.DarkGreen;
                        chatListView.Items.Add(responseItem);
                    }

                    previousQuestionId = questionId;
                }

               
                chatListView.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chat: " + ex.Message);
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(responseInput.Text))
            {
                MessageBox.Show("Please enter a response.");
                return;
            }

            if (questionDropdown.SelectedValue == null)
            {
                MessageBox.Show("Please select a question.");
                return;
            }

            try
            {
                int questionId = Convert.ToInt32(questionDropdown.SelectedValue);
                string responseText = responseInput.Text;
                string username = Login.loggedUser;

                string query = "INSERT INTO responses (question_id, username, response_text, created_at) " +
                              "VALUES (@qid, @user, @response, NOW())";

                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection);
                cmd.Parameters.AddWithValue("@qid", questionId);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@response", responseText);

                db.openConnect();
                cmd.ExecuteNonQuery();
                db.closeConnect();

                responseInput.Clear();
                LoadQuestionsWithResponses();  // Refresh the chat display
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding response: " + ex.Message);
            }
        }

        private void LoadQuestions()
        {
            string query = "SELECT question_id, question_text FROM questions ORDER BY created_at DESC";
            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                db.openConnect();
                adapter.Fill(dt);
                db.closeConnect();

                // Add a default empty option
                DataRow emptyRow = dt.NewRow();
                emptyRow["question_id"] = 0;
                emptyRow["question_text"] = "-- Select a question --";
                dt.Rows.InsertAt(emptyRow, 0);

                questionDropdown.DataSource = dt;
                questionDropdown.DisplayMember = "question_text";
                questionDropdown.ValueMember = "question_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message);
            }
        }

        private void chatListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

