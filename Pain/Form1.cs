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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Model1Container db = new Model1Container();
            List<Person> list = (from d in db.PersonSet where d.Passport == textBox1.Text && d.Password == textBox2.Text select d).ToList();
            if (list.Capacity != 0)
            {
                Person p = list[0];
                if ((p is Employee) && (p as Employee).Job == db.JobSet.Find(1))
                {
                    Hide();
                    Form4 f = new Form4();
                    f.currentEmployee = (Employee)p;
                    f.ShowDialog();
                }
                else if (p is Employee)
                {
                    Hide();
                    Form3 f = new Form3();
                    f.currentEmployee = (Employee)p;
                    f.ShowDialog();
                }
                else
                {
                    Hide();
                    Form5 f = new Form5();
                    f.currentClient = (Client)p;
                    f.ShowDialog();
                }
            }
            else
                MessageBox.Show("Неверный логин или пароль");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Hide();
            Form2 f = new Form2();
            f.ShowDialog();
        }
    }
}
