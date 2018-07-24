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
    public partial class RunnerMenu : Form
    {
        public RunnerMenu(string email)
        {
            InitializeComponent();

            

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
            LoginForm f = new LoginForm();
            f.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProfilEdit f = new ProfilEdit(label2.Text);
            f.ShowDialog();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySponsors f = new MySponsors(label2.Text);
            f.label2.Text = this.label2.Text;
            f.Show();
           
        }
    }
}
