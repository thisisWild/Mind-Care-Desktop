using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Mind_care
{
 
    class Function
    {
        Conn db = new Conn();


        public bool registerUser( string email,string pwd, string user)
        {

            MySqlCommand cmd = new MySqlCommand("insert into `register`(`email`,`password`,`username`) values (@email,@pwd,@user)", db.GetConnection);


            cmd.Parameters.Add("@user", MySqlDbType.VarChar).Value= user;
            cmd.Parameters.Add("@pwd", MySqlDbType.VarChar).Value =pwd;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

            db.openConnect();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnect();
                return true;
            }
            else
            {
                db.openConnect();
                return false;
            }
        }
        


        public bool verifyEmail(string email)
        {

            MySqlCommand cmd = new MySqlCommand("select * from `register` where `email`=@email", db.GetConnection);


            
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

            db.openConnect();

            if (cmd.ExecuteNonQuery() == 1)
            {
                db.closeConnect();
                return true;
            }
            else
            {
                db.openConnect();
                return false;
            }
        }
        //login function
        public bool loginUser(string username, string pwd)
        {
            string hashedPwd = hash(pwd);

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `register` WHERE `username`=@username AND `password`=@pwd", db.GetConnection);

            cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@pwd", MySqlDbType.VarChar).Value = hashedPwd;

            db.openConnect();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) // If a record exists, login is successful
            {
                db.closeConnect();
                return true;
            }
            else
            {
                db.closeConnect();
                return false;
            }
        }

        public static string hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        //mood tracking
        public bool IsDataSubmittedToday(string username)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM `analytics` WHERE `username`=@username AND Datetime = CURDATE()", db.GetConnection);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;

            db.openConnect();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            db.closeConnect();

            return count > 0;  // If count > 0, the user has submitted data today
        }

        // Method to insert mood data into the database
        public bool SubmitMoodData(string username, string mood)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `analytics` (`username`, `mood`) VALUES (@username, @mood)", db.GetConnection);
            cmd.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
            cmd.Parameters.Add("@mood", MySqlDbType.VarChar).Value = mood;

            db.openConnect();

            if (cmd.ExecuteNonQuery() == 1) // Check if insertion was successful
            {
                db.closeConnect();
                return true;
            }
            else
            {
                db.closeConnect();
                return false;
            }
        }


    }
}
