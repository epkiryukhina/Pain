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
    public partial class Form2 : Form
    {
        Model1Container db = new Model1Container();

        public Form2()
        {
            InitializeComponent();
        }

        private void AddEmployee(ref Employee currentEmployee)
        {

            string str = comboBox2.SelectedItem.ToString();
            Job j = (from a in db.JobSet where (a.Name == str) select a).ToList()[0];
            List<TypeOfService> ts = new List<TypeOfService>();
            DateTime d = DateTime.Parse(comboBox3.SelectedItem + "." + comboBox4.SelectedItem + "." + comboBox5.SelectedItem);

            foreach (string curS in checkedListBox1.SelectedItems)
                ts.Add((from a in db.TypeOfServiceSet where (a.Name == curS) select a).ToList()[0]);

            currentEmployee = new Employee { Name = textBox1.Text, DateBirth = d, Sex = comboBox1.SelectedItem.ToString(), Passport = textBox2.Text, Password = textBox3.Text, Job = j, TypeOfService = ts };
            db.PersonSet.Add(currentEmployee);
            db.SaveChanges();
        }

       private void Form2_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange((from a in db.JobSet select a.Name).ToArray());
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange((from a in db.TypeOfServiceSet select a.Name).ToArray());
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsDigit(e.KeyChar)) || (e.KeyChar == 8)) return;
            else
                e.Handled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if ((textBox1.Text != null) && (textBox2.Text != null) && (textBox3.Text != null) && (comboBox1.SelectedIndex != -1) && (comboBox3.SelectedIndex != -1) && (comboBox4.SelectedIndex != -1) && (comboBox5.SelectedIndex != -1))
            {
                if (!(db.PersonSet.Any(b => b.Passport == textBox2.Text)))
                {
                    DateTime d = DateTime.Parse(comboBox3.SelectedItem + "." + comboBox4.SelectedItem + "." + comboBox5.SelectedItem);
                    Client currentClient = new Client { Name = textBox1.Text, DateBirth = d, Sex = comboBox1.SelectedItem.ToString(), Passport = textBox2.Text, Password = textBox3.Text, Debt = 0 };
                    db.PersonSet.Add(currentClient);
                    db.SaveChanges();
                    Hide();
                    Form5 f = new Form5();
                    f.currentClient = currentClient;
                    f.ShowDialog();
                }
                else
                    MessageBox.Show("Пользователь с таким логином (паспортом) уже существует!");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((textBox1.Text != null) && (textBox2.Text != null) && (textBox3.Text != null) && (comboBox1.SelectedIndex != -1) && (comboBox2.SelectedIndex != -1) && (comboBox3.SelectedIndex != -1) && (comboBox4.SelectedIndex != -1) && (comboBox5.SelectedIndex != -1) && (checkedListBox1.SelectedIndex != -1))
            {
                if (!(db.PersonSet.Any(b => b.Passport == textBox2.Text)))
                {
                    Employee currentEmployee = new Employee();

                    if ((checkedListBox1.SelectedItem.ToString() == "Администрация") && (comboBox2.SelectedItem.ToString() == "Администратор"))
                    {
                        AddEmployee(ref currentEmployee);
                        Hide();
                        Form4 f = new Form4();
                        f.currentEmployee = currentEmployee;
                        f.ShowDialog();
                    }
                    else
                    {
                        AddEmployee(ref currentEmployee);
                        Hide();
                        Form3 f = new Form3();
                        f.currentEmployee = currentEmployee;
                        f.ShowDialog();
                    }
                }
                else
                    MessageBox.Show("Пользователь с таким логином (паспортом) уже существует!");
            }
            else
                MessageBox.Show("Пожалуйста, заполните все поля!");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Hide();
            Form1 f = new Form1();
            f.ShowDialog();
        }
    }
}

