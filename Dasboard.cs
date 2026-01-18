using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsManagementSystem
{
    public partial class Dasboard : Form
    {
        public Dasboard()
        {
            InitializeComponent();
            studentpage();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Dasboard_Load(object sender, EventArgs e)
        {


        }

        private void Studentbtn_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.Show();
            this.Hide();

        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {


        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();

            login.Show();
            


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            studentpage();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(0);
            }

            var course = new Course();
            course.TopLevel = false;
            panel3.Controls.Add(course);
            course.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls.RemoveAt(0);
            }

            var subject = new Subjects();
            subject.TopLevel = false;
            panel3.Controls.Add(subject);
            subject.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void studentpage() {
            try
            {
                if (panel3.Controls.Count > 0)
                {
                    panel3.Controls.RemoveAt(0);
                }

                var student = new Student();
                student.TopLevel = false;
                panel3.Controls.Add(student);

                student.Show();
            }
            catch (Exception ex)


            {
                MessageBox.Show(ex.Message);
            }
        }
    }   }
