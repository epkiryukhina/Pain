using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pain
{
    public partial class Form5 : Form
    {
        public Client currentClient;
        int typeOfSearch = 1;
        Model1Container db = new Model1Container();

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label2.Text = currentClient.Name;
            if ((from a in db.VisitSet where a.Client.Passport == currentClient.Passport select a).ToList().Capacity != 0)
                dataGridView2.DataSource = (from a in db.VisitSet where (a.Client.Id == currentClient.Id) select new { a.Id, a.FirstDate, a.SecondDate, a.Room.Number }).ToList();
            dataGridView1.DataSource = (from b in db.RoomSet select new { b.Number, b.TypeOfRoom.Type, b.TypeOfRoom.Price, b.TypeOfRoom.Capacity }).ToList();
            comboBox2.Items.AddRange((from a in db.TypeOfRoomSet select a.Type).ToArray());
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            typeOfSearch = 1;
            List<Room> first = new List<Room>();
            List<Room> second = new List<Room>();
            List<int> res = new List<int>();
            if ((dateTimePicker1.Value <= dateTimePicker2.Value) && (dateTimePicker1.Value > DateTime.Now))
            {
                first = (from a in db.RoomSet where (!(db.VisitSet.Any(b => (b.FirstDate <= dateTimePicker2.Value) && (b.SecondDate >= dateTimePicker1.Value) || (b.SecondDate <= dateTimePicker2.Value) && (b.SecondDate >= dateTimePicker1.Value)))) select a).ToList();
                if (comboBox2.SelectedIndex >= 0)
                {
                    foreach (Room x in first)
                        if (x.TypeOfRoom.Type == comboBox2.Text)
                            second.Add(x);

                    first = second;
                    second = new List<Room>();
                }
                if (comboBox1.SelectedIndex >= 0)
                {
                    foreach (Room x in first)
                        if (x.TypeOfRoom.Capacity >= Int32.Parse(comboBox1.Text))
                            second.Add(x);

                    first = second;
                    second = new List<Room>();
                }
                if ((textBox1.Text != "От") && (textBox1.Text != ""))
                {
                    foreach (Room x in first)
                        if (x.TypeOfRoom.Price >= Int32.Parse(textBox1.Text))
                            second.Add(x);

                    first = second;
                    second = new List<Room>();
                }
                if ((textBox2.Text != "До") && (textBox2.Text != ""))
                {
                    foreach (Room x in first)
                        if (x.TypeOfRoom.Price <= Int32.Parse(textBox2.Text))
                            second.Add(x);

                    first = second;
                }

                foreach (Room x in first)
                    res.Add(x.Id);

                //dataGridView2.DataSource = (from a in db.VisitSet where (a.Client.Id == currentClient.Id) select new { a.FirstDate, a.SecondDate, a.Room.Number }).ToArray();
                dataGridView1.DataSource = (from b in db.RoomSet where (res.Any(a => a == b.Id)) select new { b.Number, b.TypeOfRoom.Type, b.TypeOfRoom.Price, b.TypeOfRoom.Capacity }).ToArray();
            }
            else
                MessageBox.Show("Вы не можете забранировать номер на эти даты!");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            typeOfSearch = 2;
            List<TypeOfService> first = new List<TypeOfService>();
            List<TypeOfService> second = new List<TypeOfService>();
            List<int> res = new List<int>();
            if (dateTimePicker4.Value > DateTime.Now)
            {
                first = (from a in db.TypeOfServiceSet select a).ToList();
                if ((textBox5.Text != "От") && (textBox5.Text != ""))
                {
                    foreach (TypeOfService x in first)
                        if (x.Price >= Int32.Parse(textBox5.Text))
                            second.Add(x);

                    first = second;
                    second = new List<TypeOfService>();
                }
                if ((textBox4.Text != "До") && (textBox4.Text != ""))
                {
                    foreach (TypeOfService x in first)
                        if (x.Price <= Int32.Parse(textBox4.Text))
                            second.Add(x);

                    first = second;
                    second = new List<TypeOfService>();
                }
                foreach (TypeOfService x in first)
                    res.Add(x.Id);

                dataGridView1.DataSource = (from b in db.TypeOfServiceSet where (res.Any(a => a == b.Id)) select new { b.Name, b.Price, b.TypeOfPrice.Type }).ToArray();
            }
            else
                MessageBox.Show("Вы не можете забранировать услугу на эту дату!");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index >= 0)
            {
                if (typeOfSearch == 1)
                {
                    string str = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    Room r = (from a in db.RoomSet where (a.Number == str) select a).ToList()[0];
                    ((from a in db.PersonSet where a.Id == currentClient.Id select a).ToList()[0] as Client).Debt += r.TypeOfRoom.Price * (dateTimePicker2.Value.Day - dateTimePicker1.Value.Day + 1);
                    
                    db.VisitSet.Add(new Visit { Client = (Client)(from a in db.PersonSet where a.Id == currentClient.Id select a).ToList()[0], FirstDate = dateTimePicker1.Value, SecondDate = dateTimePicker2.Value, Room = r });

                    db.SaveChanges();
                    dataGridView2.DataSource = (from a in db.VisitSet where (a.Client.Id == currentClient.Id) select new {a.Id, a.FirstDate, a.SecondDate, a.Room.Number, a.Room.TypeOfRoom.Type, a.Room.TypeOfRoom.Price }).ToArray();
                }
                else
                {
                    string str = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    TypeOfService ts = (from a in db.TypeOfServiceSet where (a.Name == str) select a).ToList()[0];
                    int price, kolPeople = 0, kolHours = 0;

                    if ((ts.TypeOfPrice.Type == "По часам") && (textBox7.Text != "Часы") && (textBox7.Text != ""))
                    {
                        kolHours = Int32.Parse(textBox7.Text);
                        price = ts.Price * Int32.Parse(textBox7.Text);
                    }
                    else if (((ts.TypeOfPrice.Type == "По колличеству") && (textBox6.Text != "Люди") && (textBox6.Text != "")))
                    {
                        kolPeople = Int32.Parse(textBox6.Text);
                        price = ts.Price * Int32.Parse(textBox6.Text);
                    }
                    else if (ts.TypeOfPrice.Type == "Бесплатно")
                        price = 0;
                    else
                        price = ts.Price;
                    
                    ((from a in db.PersonSet where a.Id == currentClient.Id select a).ToList()[0] as Client).Debt += price;
                    db.ServiceSet.Add(new Service { Date = dateTimePicker4.Value, TypeOfService = ts, Client = new Client[] { (Client)(from a in db.PersonSet where a.Id == currentClient.Id select a).ToList()[0] }, NumberOfHours = kolHours, NumberOfPeople = kolPeople });

                    db.SaveChanges();
                    dataGridView2.DataSource = (from a in db.ServiceSet where (a.Client.Any(b => b.Id == currentClient.Id)) select new {a.Id, a.Date, a.TypeOfService.Name, a.TypeOfService.Price, a.TypeOfService.TypeOfPrice.Type, a.NumberOfHours, a.NumberOfPeople}).ToArray();
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Hide();
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
            {
                (from a in db.PersonSet where a.Id == currentClient.Id select a).ToList()[0].Password = textBox3.Text;
                //db.PersonSet.Remove((from a in db.PersonSet where a.Id == currentClient.Id select a).ToList()[0]);
                //currentClient.Password = textBox3.Text;
                //db.PersonSet.Add(new Client { Name = currentClient.Name, DateBirth = currentClient.DateBirth, Debt = currentClient.Debt, History = currentClient.History, Passport = currentClient.Passport, Password = currentClient.Password, Sex = currentClient.Sex });
                textBox3.Clear();
                db.SaveChanges();
                MessageBox.Show("Пароль успешно изменен.");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (typeOfSearch == 1)
            {
                int i = Int32.Parse(dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString());
                Visit v = (from a in db.VisitSet where (a.Id == i) select a).ToList()[0];

                foreach (Room x in (from a in db.RoomSet where (a.Visit.Any(b => b.Id == v.Id)) select a).ToList())
                    x.Visit.Remove(v);

                foreach (Client x in (from a in db.PersonSet where ((a is Client) && ((a as Client).Visit.Any(b => b.Id == v.Id))) select a).ToList())
                    x.Visit.Remove(v);

                db.VisitSet.Remove(v);

                db.SaveChanges();
                dataGridView2.DataSource = (from a in db.VisitSet where (a.Client.Id == currentClient.Id) select new { a.Id, a.FirstDate, a.SecondDate, a.Room.Number, a.Room.TypeOfRoom.Type, a.Room.TypeOfRoom.Price }).ToArray();
            }
            else
            {
                int i = Int32.Parse(dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString());
                Service s = (from a in db.ServiceSet where (a.Id == i) select a).ToList()[0];

                foreach (TypeOfService x in (from a in db.TypeOfServiceSet where (a.Service.Any(b => b.Id == s.Id)) select a).ToList())
                    x.Service.Remove(s);

                foreach (Client x in (from a in db.PersonSet where ((a is Client) && ((a as Client).Service.Any(b => b.Id == s.Id))) select a).ToList())
                    x.Service.Remove(s);

                db.ServiceSet.Remove(s);

                db.SaveChanges();
                dataGridView2.DataSource = (from a in db.ServiceSet where (a.Client.Any(b => b.Id == currentClient.Id)) select new { a.Id, a.Date, a.TypeOfService.Name, a.TypeOfService.Price, a.TypeOfService.TypeOfPrice.Type, a.NumberOfHours, a.NumberOfPeople }).ToArray();
            }
        }
    }
}