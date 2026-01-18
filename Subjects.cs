using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsManagementSystem
{
    public partial class Subjects : Form
    {

        SqlConnection con = new SqlConnection(
       @"Data Source=ASKA\SQLEXPRESS;
               Initial Catalog=Student_Details;
               Integrated Security=True;
               TrustServerCertificate=True");

        public Subjects()
        {
            InitializeComponent();
            LoadSubjects();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbox_sub_id.Text) || string.IsNullOrEmpty(combobox_sub_name.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            try
            {
                string query = "INSERT INTO subject (subjectID, subject_name) VALUES (@id, @name)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", txtbox_sub_id.Text);
                    cmd.Parameters.AddWithValue("@name", combobox_sub_name.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Subject Added Successfully");
                    LoadSubjects();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

             // 1. Validation: Ensure ID is provided to know which record to update
             if (string.IsNullOrEmpty(txtbox_sub_id.Text))
             {
                 MessageBox.Show("Please enter or select a Subject ID to update.");
                 return;
             }

             // 2. SQL Query
             string query = "UPDATE subject SET subject_name = @name WHERE subjectID = @id";

             try
             {
                 using (SqlCommand cmd = new SqlCommand(query, con))
                 {
                     // 3. Add parameters safely
                     cmd.Parameters.AddWithValue("@id", txtbox_sub_id.Text);
                     cmd.Parameters.AddWithValue("@name", combobox_sub_name.Text);

                     con.Open();
                     int rowsAffected = cmd.ExecuteNonQuery();
                     con.Close();

                     // 4. Check if the update actually happened
                     if (rowsAffected > 0)
                     {
                         MessageBox.Show("Subject Updated Successfully");
                         LoadSubjects(); // Refresh the grid
                         ClearFields();  // Clear the inputs
                     }
                     else
                     {
                         MessageBox.Show("No subject found with that ID.");
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error: " + ex.Message);
                 // Ensure connection is closed if an error occurs while it was open
                 if (con.State == ConnectionState.Open) con.Close();
             }
         
    
         


        }
        private void clear()
        {
            txtbox_sub_id.Clear();
            
        
        
        
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                clear();

               
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                txtbox_sub_id.Text = row.Cells["subjectID"].Value.ToString();
                combobox_sub_name.Text = row.Cells["subject_name"].Value.ToString();
            }
        }
        private void LoadSubjects()
        {
            try
            {
                string query = "SELECT subjectID, subject_name FROM subject";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt; // Using dataGridView2 as per your screenshot
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subjects: " + ex.Message);
            }
        }
        private void ClearFields()
        {
            txtbox_sub_id.Clear();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Validation: Ensure ID is provided to know which record to delete
            if (string.IsNullOrEmpty(txtbox_sub_id.Text))
            {
                MessageBox.Show("Please enter or select a Subject ID to delete.");
                return;
            }

            // 2. Confirmation: It is best practice to ask before deleting data
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this subject?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string query = "DELETE FROM subject WHERE subjectID = @id";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // 3. Add parameter safely
                        cmd.Parameters.AddWithValue("@id", txtbox_sub_id.Text);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        // 4. Check if the deletion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Subject Deleted Successfully");
                            LoadSubjects(); // Refresh the grid to show the record is gone
                            ClearFields();  // Clear the inputs
                        }
                        else
                        {
                            MessageBox.Show("No subject found with that ID.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    // Ensure connection is closed if an error occurs while it was open
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
        }
    }
    }

