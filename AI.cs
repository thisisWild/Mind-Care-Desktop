using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mind_care
{
    public partial class AI : Form
    {
        private HttpContent content;

        public AI()
        {
            InitializeComponent();
        }

        private async void btnAsk_Click_1(object sender, EventArgs e)
        {
            btnAsk.Enabled = false;
            richTextRes.Text = "Generating Results, Please Wait...";

            string userQuestion = txtQuestion.Text;

            if (string.IsNullOrWhiteSpace(userQuestion))
            {
                richTextRes.Text = "Please enter your health question first!";
                btnAsk.Enabled = true;
                return;
            }

            try
            {
                string result = await GetAIResponseAsync(userQuestion);
                richTextRes.Text = result;
            }
            catch (Exception ex)
            {
                richTextRes.Text = $"Error: {ex.Message}";
            }
            finally
            {
                btnAsk.Enabled = true;
            }
        }

        // Method to call Google Gemini API and get the AI response
        public async Task<string> GetAIResponseAsync(string question)
        {
            // Preparing JSON payload 
            var requestData = new
            {
                contents = new[]
                {
            new
            {
                role = "user",
                parts = new[]
                {
                    new { text = $"You are a healthcare assistant expert only. Answer: \"{question}\"" }
                }
            }
        }
            };

            string json = JsonConvert.SerializeObject(requestData);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
                client.DefaultRequestHeaders.Add("x-goog-api-key", "AIzaSyBwgETerHkz9_f2EYeSN6caUbVGFCqT2zA"); //added new key

                using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    // Send POST request to the Gemini API
                    HttpResponseMessage response = await client.PostAsync(
                        "v1beta/models/gemini-2.5-flash:generateContent",
                        content
                    );

                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var parsed = JObject.Parse(responseBody);
                        return parsed["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString()
                               ?? "No response received.";
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}\n{responseBody}";
                    }
                }
            }
        }

        private void AI_Load(object sender, EventArgs e)
        {
            username.Text = Login.loggedUser;
        }

        private void Homebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mood_tracking mo = new Mood_tracking();
            mo.Show();
        }

        private void chatbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Chat c = new Chat();
            c.Show();
        }

        private void hotlinebtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Suicide_hotline h = new Suicide_hotline();
            h.Show();
        }

        private void AIbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            AI A = new AI();
            A.Show();
        }

        private void Reportbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analytics a = new Analytics();
            a.Show();
        }

        private void Projectbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            projectspace p = new projectspace();
            p.Show();
        }

        private void Lohout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }
    }


    //private void txtQuestion_Enter(object sender, EventArgs e)
    //{
    //    //operationFalse
    //    if (txtQuestion.Text == "Type your question here...")
    //    {
    //        txtQuestion.Text = "";
    //        txtQuestion.ForeColor = Color.Black;
    //    }
    //}

    //private void txtQuestion_Leave(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrWhiteSpace(txtQuestion.Text))
    //    {
    //        txtQuestion.Text = "Type your question here...";
    //        txtQuestion.ForeColor = Color.Gray;
    //    }
    //}
}
