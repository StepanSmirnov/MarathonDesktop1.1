using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MarathonDesktop
{
    public partial class ProfilEdit : Form
    {
        public ProfilEdit(string email)
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=marathon;connection timeout=30");
            conn.Open();
            SqlCommand comm1 = new SqlCommand("select * from [country]", conn);
            SqlDataReader reader1 = comm1.ExecuteReader();

            while (reader1.Read())
            {
                string countryCode = reader1["CountryCode"].ToString();
                comboBox2.Items.Add(countryCode);
            }
            reader1.Close();
            SqlCommand comm2 = new SqlCommand("select * from [gender]", conn);
            SqlDataReader reader2 = comm2.ExecuteReader();
            while (reader2.Read())
            {
                string male = reader2["Gender"].ToString();
                comboBox1.Items.Add(male);
            }
            reader2.Close();


            SqlCommand comm = new SqlCommand("select [User].[Email],[User].[FirstName],[User].[LastName],[Runner].Gender,[Runner].CountryCode,[Runner].DateOfBirth " +
                " FROM [User] inner join [Runner] ON [User].Email=[Runner].Email WHERE [User].Email='" + email + "'", conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                string fName = reader["FirstName"].ToString();
                string lName = reader["LastName"].ToString();
                string gender = reader["Gender"].ToString();
                string country = reader["CountryCode"].ToString();
                string dr = reader["DateOfBirth"].ToString();

                textBox1.Text = fName;
                textBox2.Text = lName;
                comboBox1.Text = gender;
                comboBox2.Text = country;
                dateTimePicker1.Value = Convert.ToDateTime(dr);
            }
            label11.Text = email;
          
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dNow = DateTime.Now;
            DateTime dStart = new DateTime(2018, 11, 24);
            TimeSpan f = dStart - dNow;
            int days = f.Days;
            int hours = f.Hours;
            int min = f.Minutes;


            label1.Text = days.ToString() + " дней " + hours.ToString() + " часов " + min.ToString() + " минут до начала гонки";

        }

        private void button3_Click(object sender, EventArgs e)
        {

            RunnerMenu f = new RunnerMenu("a");
            f.Show();

            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == ""))
            {
                MessageBox.Show("Заполните все поля"); return;
            }

            SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=marathon;connection timeout=30");
            conn.Open();
            SqlCommand comm = new SqlCommand("UPDATE [User] SET FirstName='" + textBox1.Text + "',LastName='" + textBox2.Text + "' where Email='" + label11.Text + "'", conn);
            comm.ExecuteNonQuery();
            SqlCommand comm2 = new SqlCommand("UPDATE [Runner] SET Gender='" + comboBox1.Text + "',CountryCode='" + comboBox2.Text + "',DateOfBirth='" +
                dateTimePicker1.Value.ToString() + "' where Email='" + label11.Text + "'", conn);
            comm2.ExecuteNonQuery();


            if (textBox3.Text != "")
            {
                if (textBox4.Text == "") { MessageBox.Show("Повторите пароль"); return; }

                string pass1 = textBox3.Text;
                string pass2 = textBox4.Text;
                if (pass1.Length < 6) { MessageBox.Show("Длина пароля мин. 6 символов"); return; }

                bool digit = false, simv = false, password = false;


                for (int i = 0; i < pass1.Length; i++)
                {
                    if (Char.IsDigit(pass1[i])) digit = true;
                    if ((pass1[i] == '!') || (pass1[i] == '@') || (pass1[i] == '#')) simv = true;
                }

                if (simv && digit)
                {
                    if (pass1.Length == pass2.Length)
                    {
                        int k = 0;
                        for (int i = 0; i < pass1.Length; i++)
                            if (pass1[i] == pass2[i]) k++;
                        if (k == pass1.Length) password = true;
                        else { MessageBox.Show("Пароли не совпадают"); return; }



                    }
                    else { MessageBox.Show("Пароли не совпадают"); return; }
                }
                else { MessageBox.Show("Пароль должен содержать мин. 1 цифру и один из символов !@# "); return; }


                if (password)
                {
                    SqlCommand comm3 = new SqlCommand("UPDATE [User] SET Password='" + textBox3.Text + "' where email='" + label11.Text + "'", conn);
                    comm3.ExecuteNonQuery();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}