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

namespace Gym_Management_System
{
    public partial class FormAddExecise : Form
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        dbConnect dbcon = new dbConnect();
        SqlDataReader reader;
        public FormAddExecise()
        {
            InitializeComponent();
            con = new SqlConnection(dbcon.connection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            checkForm();
            if (checkForm())
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to save this exercise?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("INSERT INTO exercises (name) VALUES (@name)", con);
                        cmd.Parameters.AddWithValue("@name", txtExName.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Exercise has been successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Boolean checkForm()
        {
            if (string.IsNullOrWhiteSpace(txtExID.Text))
            {
                MessageBox.Show("Please enter the exercise ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtExName.Text))
            {
                MessageBox.Show("Please enter the exercise Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            checkForm();
            if (checkForm())
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to update this exercise?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("UPDATE exercises SET name = @name WHERE id = @id", con);
                        cmd.Parameters.AddWithValue("@name", txtExName.Text);
                        cmd.Parameters.AddWithValue("@id", txtExID.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Exercise has been successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExID.Text))
            {
                txtExName.Clear(); 
                return;
            }

            if (int.TryParse(txtExID.Text, out int exerciseId))
            {
                try
                {
                    string q = "SELECT name FROM exercises WHERE id = @ExerciseID";

                    using (SqlCommand cmd = new SqlCommand(q, con))
                    {
                        cmd.Parameters.AddWithValue("@ExerciseID", exerciseId);

                        if (con.State == ConnectionState.Closed) con.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            
                            txtExName.Text = result.ToString();
                        }
                        else
                        {
           
                            txtExName.Text = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching exercise: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            else
            {
                
                txtExID.Text = "Invalid ID format";
            }
        }
    }
}
