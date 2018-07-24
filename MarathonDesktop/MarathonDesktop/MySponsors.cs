using MarathonDesktop.marathonDataSetTableAdapters;
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
            var ds = new marathonDataSet();
            new UserTableAdapter().Fill(ds.User);
            new RunnerTableAdapter().Fill(ds.Runner);
            new RegistrationTableAdapter().Fill(ds.Registration);
            new CharityTableAdapter().Fill(ds.Charity);
            new SponsorshipTableAdapter().Fill(ds.Sponsorship);
            var registration = (from reg in ds.Registration
                                from runner in ds.Runner
                                where runner.Email == mail && reg.RunnerId == runner.RunnerId
                                select reg).SingleOrDefault();
            var charity = (from ch in ds.Charity where ch.CharityId == registration.CharityId select ch).SingleOrDefault();
            var sponsors = (from sponsor in ds.Sponsorship where sponsor.RegistrationId == registration.RegistrationId select sponsor);
            foreach (var d in sponsors)
            {
                ListViewItem lvi = new ListViewItem(d.SponsorName);
                lvi.SubItems.Add(d.Amount.ToString());
                listView1.Items.Add(lvi);
            }
            //string desc = "", logo = "", name = "";      
            //int sum = 0;
            //while (reader.Read())
            //{

            //    // pictureBox1.Image = Image.FromFile(reader["CharityLogo"].ToString());
            //    // label4.Text = reader["CharityDescription"].ToString();
            //    ListViewItem lvi = new ListViewItem(reader["SponsorName"].ToString());
            //    lvi.SubItems.Add(reader["Amount"].ToString());
            //    listView1.Items.Add(lvi);
            //    sum += Convert.ToInt32(reader["Amount"]);
            //}

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
            if (charity != null)
            {
                label4.Text = charity.CharityName;
                label5.Text = charity.CharityDescription;
                pictureBox1.Image = Image.FromFile(charity.CharityLogo);
                label5.Height = 150 + label5.Text.Length / 2;
            }

            //conn.Close();
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
