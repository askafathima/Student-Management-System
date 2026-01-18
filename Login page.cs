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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Studentlbl_Click(object sender, EventArgs e)
        {

        }

        private void Resetbtn_Click(object sender, EventArgs e)
        {
            Usernametxt.Clear();
            Passwordtxt.Clear();

        }

        private void Signinbtn_Click(object sender, EventArgs e)
        {
            /*  if(Usernametxt.Text=="Aska" && Passwordtxt.Text == "1234")
              {
                  Dasboard dasboard = new Dasboard();
                  dasboard.Show();
                  this.Hide();


              }
              else
              {
                  MessageBox.Show("Wrong Username And Password");
              }
            */
            Dasboard dasboard = new Dasboard();
            dasboard.Show();
            this.Hide();

        }

        private void Usernamelbl_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Usernametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
