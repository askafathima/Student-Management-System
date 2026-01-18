using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentsManagementSystem
{
    public partial class Course : Form
    {
        SqlConnection con = new SqlConnection(
       @"Data Source=ASKA\SQLEXPRESS;
               Initial Catalog=Student_Details;
               Integrated Security=True;
               TrustServerCertificate=True");

        public Course()
        {
            InitializeComponent();
            LoadCourses();
            // this.Load += new System.EventHandler(this.Course_Load);

        }

        private void Course_Load(object sender, EventArgs e)
        {

        }

        private void LoadCourses()
        {


            try
            {
                con.Open();

                string query = "SELECT CourseId, CourseName, Duration FROM Courses";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                 con.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            // 1. Validate Input (ensure textboxes aren't empty)
            if (string.IsNullOrEmpty(txtbox_cur_id.Text) || string.IsNullOrEmpty(txtbox_cur_name.Text))
            {
                MessageBox.Show("Please fill in the Course ID and Name.");
                return;
            }

            // 2. Define the SQL Query using Parameters (to prevent SQL Injection)
            string query = "INSERT INTO Courses (CourseId, CourseName, Duration) VALUES (@id, @name, @duration)";

            try
            {


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // 3. Add parameters safely
                    cmd.Parameters.AddWithValue("@id", txtbox_cur_id.Text);
                    cmd.Parameters.AddWithValue("@name", txtbox_cur_name.Text);
                    cmd.Parameters.AddWithValue("@duration", txtbox_du.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Course added successfully!");

                        // 4. Refresh the Grid and Clear Fields
                        LoadCourses();
                        ClearFields();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Helper method to clear textboxes after adding
        private void ClearFields()
        {
            txtbox_du.Clear();
            txtbox_cur_name.Clear();
            txtbox_cur_id.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)


        {
            if (string.IsNullOrEmpty(txtbox_cur_id.Text))
            {
                MessageBox.Show("Please enter the Course ID you wish to update.");
                return;
            }

            // SQL Query to update details based on the ID
            string query = "UPDATE Courses SET CourseName = @name, Duration = @duration WHERE CourseId = @id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Adding parameters safely
                    cmd.Parameters.AddWithValue("@id", txtbox_cur_id.Text);
                    cmd.Parameters.AddWithValue("@name", txtbox_cur_name.Text);
                    cmd.Parameters.AddWithValue("@duration", txtbox_du.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Course updated successfully!");

                        // Refresh the Grid and Clear Fields
                        LoadCourses();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("No record found with that ID.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                
            }
        }

       

        private void btnDelete_Click(object sender, EventArgs e)
        {
            

            if (string.IsNullOrEmpty(txtbox_cur_id.Text))
            {
                MessageBox.Show("Please enter the Course ID to delete.");
                return;
            }

            // Optional: Ask for confirmation before deleting
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.No) return;

            string query = "DELETE FROM Courses WHERE CourseId = @id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", txtbox_cur_id.Text);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Course deleted successfully!");

                        // Refresh the Grid and Clear Fields
                        LoadCourses();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Course ID not found.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            } 
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                // 1. Get the specific row that was clicked
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // 2. Fill the textboxes using the column names from your database
                // We use .ToString() to convert the database value into text for the textbox
                txtbox_cur_id.Text = row.Cells["CourseId"].Value.ToString();
                txtbox_cur_name.Text = row.Cells["CourseName"].Value.ToString();
                txtbox_du.Text = row.Cells["Duration"].Value.ToString();
            }
        }
    }
}
    
    

