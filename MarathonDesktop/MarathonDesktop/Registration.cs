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
    public partial class Registration : Form
    {
       
        public Registration()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection("server=DESKTOP-FHVMNUE;Trusted_Connection=yes;database=marathon;connection timeout=30");
            conn.Open();
            SqlCommand comm = new SqlCommand("select * from [country]",conn);
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                string countryCode = reader["CountryCode"].ToString();
                comboBox2.Items.Add(countryCode);
            }
            reader.Close();
            SqlCommand comm2 = new SqlCommand("select * from [gender]", conn);
            SqlDataReader reader2 = comm2.ExecuteReader();
            while (reader2.Read())
            {
                string male = reader2["Gender"].ToString();
                comboBox1.Items.Add(male);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
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
            Choose f = new Choose();
            f.Show();
            this.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((textBox1.Text=="")|| (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (textBox5.Text == ""))
            {
                MessageBox.Show("Заполните все поля");return;
            }

            DateTime dnow = DateTime.Now;
            TimeSpan checkAge = dnow - dateTimePicker1.Value;
            int age = checkAge.Days;
            int check = age / 365;
      
            if (check < 10) { MessageBox.Show("Возраст бегуна - не менее 10 лет");return; }


            string pass1 = textBox3.Text;
            string pass2 = textBox4.Text;
            if (pass1.Length<6) { MessageBox.Show("Длина пароля мин. 6 символов"); return; }

            bool digit = false,simv=false,password=false;


            for (int i = 0; i < pass1.Length; i++)
            {
                if (Char.IsDigit(pass1[i])) digit = true;
                if ((pass1[i] == '!') || (pass1[i] == '@') || (pass1[i] == '#')) simv = true;
            }

            if (simv && digit )
            {
                if (pass1.Length==pass2.Length)
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
                SqlConnection conn = new SqlConnection("server=DESKTOP-FHVMNUE;Trusted_Connection=yes;database=marathon;connection timeout=30");
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO [user]([Email],[Password],[FirstName],[LastName],[RoleId]) VALUES ('"+textBox5.Text+"','" +
                    pass1+"','"+textBox1.Text+"','"+textBox2.Text+"','R')",conn);
                comm.ExecuteNonQuery();

                SqlCommand comm2 = new SqlCommand("INSERT INTO [runner]([Email],[Gender],[DateOfBirth],[CountryCode]) VALUES('" + textBox5.Text + "','" +
                    comboBox1.Text + "','" + dateTimePicker1.Value.ToString() + "','" + comboBox2.Text + "')",conn);
                comm2.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Вы успешно зарегестрировались!");
            }
          
        }
    }
}
