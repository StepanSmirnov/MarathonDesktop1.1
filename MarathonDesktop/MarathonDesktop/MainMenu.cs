using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarathonDesktop
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MoreInfo f = new MoreInfo();
            f.Show();
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
            label3.Text = dNow.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Choose form = new Choose();
            form.Show();

        }
    }
}
