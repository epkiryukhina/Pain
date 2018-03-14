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
    public partial class Form3 : Form
    {
        Model1Container db = new Model1Container();
        public Employee currentEmployee;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = currentEmployee.Name;
            comboBox2.Items.AddRange((from a in db.JobSet select a.Name).ToArray());
            checkedListBox1.Items.AddRange((from a in db.TypeOfServiceSet select a.Name).ToArray());
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if ((currentEmployee.Job.Name == "Администратор") && (currentEmployee.TypeOfService.Any(b => b.Name == "Администрация")))
            {
                Hide();
                Form4 f = new Form4();
                f.currentEmployee = currentEmployee;
                f.Show();
            }
            else
            {
                Hide();
                Form1 f = new Form1();
                f.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from a in db.ServiceSet where ((a.Date >= monthCalendar1.SelectionStart) && (a.Date <= monthCalendar1.SelectionEnd) && (currentEmployee.TypeOfService.Any(b => b.Name == a.TypeOfService.Name))) select new { a.TypeOfService.Name, a.Date, a.TypeOfService.TypeOfPrice, a.TypeOfService.Price, a.NumberOfHours, a.NumberOfPeople,}).ToArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
