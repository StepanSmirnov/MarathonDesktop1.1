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
    public partial class MySponsors : Form
    {
        public MySponsors(string mail)
        {
            InitializeComponent();

        
            
            listView1.Columns.Add("Спонсор");
            listView1.Columns.Add("Сумма");
            listView1.Columns[0].Width = -1;
            listView1.Columns[1].Width = -1;

            listView1.View = View.Details;

            SqlConnection conn = new SqlConnection("server=DESKTOP-FHVMNUE;database=marathon;Trusted_Connection=yes;");
            conn.Open();
            SqlCommand comm = new SqlCommand("select [Registration].[CharityId],[Charity].[CharityDescription],[Charity].CharityName,[Sponsorship].SponsorName, " +
                "[Charity].CharityLogo,[Sponsorship].Amount,[Sponsorship].RegistrationId,[Registration].[RegistrationId],[Runner].RunnerId,[User].Email " +
                "FROM [User] inner join [Runner] ON [User].Email=[Runner].Email inner join [Registration] on [Registration].RunnerId=[Runner].RunnerId " +
                "inner join [Sponsorship] on [Sponsorship].RegistrationId = [Registration].RegistrationId inner join [Charity] " +
                " on [Charity].CharityId=[Registration].CharityId WHERE [User].Email='" + mail+"'",conn);
            SqlDataReader reader = comm.ExecuteReader();

            string desc = "", logo = "", name = "";      
            int sum = 0;
            while (reader.Read())
            {

                // pictureBox1.Image = Image.FromFile(reader["CharityLogo"].ToString());
                // label4.Text = reader["CharityDescription"].ToString();
                name = reader["CharityName"].ToString();
                desc= reader["CharityDescription"].ToString();
                logo= reader["CharityLogo"].ToString();
                ListViewItem lvi = new ListViewItem(reader["SponsorName"].ToString());
                lvi.SubItems.Add(reader["Amount"].ToString());
                listView1.Items.Add(lvi);
                sum += Convert.ToInt32(reader["Amount"]);
            }

            /* Image img = Image.FromFile(logo);
             listView2.View = View.LargeIcon;
             ImageList imageList = new ImageList();
           listView2.Columns.Add("all");listView2.Columns.Add("2");
               imageList.Images.Add(img);
            // listView2.Columns[0].TextAlign = HorizontalAlignment.Center;




             ListViewItem item = new ListViewItem(name);
            item.ImageIndex = imageList.Images.Count - 1;
             item.SubItems.Add(desc);
             listView2.Items.Add(item);
             listView2.LargeImageList = imageList;

             ListViewItem listItem = new ListViewItem("ВСЕГО");
             listItem.SubItems.Add(sum.ToString());
             listView1.Items.Add(listItem);*/

            label4.Text = name;
            label5.Text = desc;
            pictureBox1.Image = Image.FromFile(logo);
            label5.Height = 150+ desc.Length / 2;

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

            this.Close();
        }
    }
}
