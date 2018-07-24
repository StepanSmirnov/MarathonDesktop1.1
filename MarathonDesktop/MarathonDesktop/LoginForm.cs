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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("server=(localdb)\\MSSQLLocalDB;Trusted_Connection=yes;database=marathon;connection timeout=30");
                conn.Open();
                string mail = textBox1.Text;
                SqlCommand comm = new SqlCommand("SELECT * FROM [dbo].[User] where Email='" + mail+"'", conn);
                SqlDataReader reader = comm.ExecuteReader();

                bool check = false;
                string role="";

                while (reader.Read())
                {

                    string pass = reader["Password"].ToString();
                    if (pass == textBox2.Text) check = true;
                    role = reader["RoleId"].ToString();
                }
                
                if (check)
                {
                    switch (role)
                    {
                        case "R": RunnerMenu f = new RunnerMenu(textBox1.Text);f.Show();this.Close(); f.label2.Text = mail; break;
                        case "A": AdminMenu f1 = new AdminMenu(); f1.Show(); this.Close();break;
                        case "C": CoordinatorMenu f2= new CoordinatorMenu(); f2.Show(); this.Close(); break;
                        default:MessageBox.Show("Ошибка");
                            break;
                    }
                }

                conn.Close();
            }
            catch (Exception err)
            {

                MessageBox.Show(err.ToString());
            }
        
        }
    }
}
