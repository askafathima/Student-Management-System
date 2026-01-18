using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentsManagementSystem
{
    public partial class Student : Form
    {
        SqlConnection con = new SqlConnection(
              @"Data Source=ASKA\SQLEXPRESS;
               Initial Catalog=Student_Details;
               Integrated Security=True;
               TrustServerCertificate=True");



        public Student()
        {
            InitializeComponent();
            LoadStudentData();
           
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadStudentData()
        {
            try
            {
                /* con.Open();
                 SqlCommand cdm = new SqlCommand("SELECT * FROM [Student]", con);
                 cdm.ExecuteNonQuery();
                 SqlDataAdapter studentadapter = new SqlDataAdapter(cdm);
                 DataTable studenttable = new DataTable();
                 studentadapter.Fill(studenttable);
                 dataGridView1.DataSource = studenttable;
                 con.Close();*/
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Student", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();

                SqlCommand qr = new SqlCommand(
                "INSERT INTO Student (Student_Name, Gender, PhoneNo, DOB, Father_name) " +
                "VALUES (@name, @gender, @phone, @dob, @father)", con);

                qr.Parameters.AddWithValue("@name", txtbox_studentName.Text);
                qr.Parameters.AddWithValue("@gender", combox_gender.Text);
                qr.Parameters.AddWithValue("@phone", Convert.ToInt32(txtbox_phone.Text));
                qr.Parameters.AddWithValue("@dob", dpt.Value);
                qr.Parameters.AddWithValue("@father", txtbox_fathername.Text);


                qr.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Added Successfully");
                LoadStudentData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }


        }

        // Separate function to fetch data
        private void GetStudentList()
        {
            /*
              SqlCommand cmd = new SqlCommand("Select * from StudentTable", con);
              DataTable dt = new DataTable();

              con.Open();
              SqlDataReader sdr = cmd.ExecuteReader();
              dt.Load(sdr);
              con.Close(); */
        }
        private void button3_Click(object sender, EventArgs e)
        {


            try
            {
                con.Open();

                SqlCommand qr = new SqlCommand(
                "UPDATE Student SET " +
                "Student_Name = @name, " +
                "Gender = @gender, " +
                "PhoneNo = @phone, " +
                "DOB = @dob, " +
                "Father_name = @father " +
                "WHERE Student_id = @id", con);

                   
                qr.Parameters.AddWithValue("@name", txtbox_studentName.Text);
                qr.Parameters.AddWithValue("@gender", combox_gender.Text);
                qr.Parameters.AddWithValue("@phone", Convert.ToInt32(txtbox_phone.Text));
                qr.Parameters.AddWithValue("@dob", dpt.Value);
                qr.Parameters.AddWithValue("@father", txtbox_fathername.Text);

                int rows = qr.ExecuteNonQuery();
                con.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Student Updated Successfully");
                    GetStudentList();
                    LoadStudentData(); // refresh grid
                }
                else
                {
                    MessageBox.Show("Update Failed! Student ID not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }























        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Student_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet3.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter2.Fill(this.dataSet3.Students);
            // TODO: This line of code loads data into the 'dataSet2.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter1.Fill(this.dataSet2.Students);
            // TODO: This line of code loads data into the 'dataSet1.Students' table. You can move, or remove it, as needed.
            this.studentsTableAdapter.Fill(this.dataSet1.Students);

        }

       
        private void clear()
        {
            txtbox_fathername.Text = "";
            txtbox_phone.Clear();
            txtbox_phone.Clear();
            
            txtbox_studentName.Clear();
           



}

        private void button4_Click(object sender, EventArgs e)
        {
            // Check if the ID field is empty before trying to delete
            

            // Ask the user for confirmation
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    con.Open();

                    // SQL query to delete based on Student_id
                    SqlCommand qr = new SqlCommand("DELETE FROM Student WHERE Student_id = @id", con);

                    // Adding the parameter to prevent SQL injection
                    

                    int rows = qr.ExecuteNonQuery();
                    con.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Student Deleted Successfully");

                        // Clear the textboxes after deletion
                        clear();

                        // Refresh the DataGridView to show updated data
                        LoadStudentData();
                    }
                    else
                    {
                        MessageBox.Show("Delete Failed! Student ID not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        private void combox_gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtbox_studentName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
                clear();


            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                combox_gender.Text = dataGridView1.SelectedRows[0].Cells["Gender"].Value.ToString();
                txtbox_fathername.Text = dataGridView1.SelectedRows[0].Cells["Father_name"].Value.ToString();
                txtbox_phone.Text = dataGridView1.SelectedRows[0].Cells["PhoneNo"].Value.ToString();

                txtbox_studentName.Text = dataGridView1.SelectedRows[0].Cells["Student_Name"].Value.ToString();
                dpt.Text = dataGridView1.SelectedRows[0].Cells["DOB"].Value.ToString();






            }

        }
    }
    
}



