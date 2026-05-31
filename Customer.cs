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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CarRental
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("insert into customers Values(@customerid,@customername, @gender, @email, @phone)", con);
            cnn.Parameters.AddWithValue("@CustomerID", int.Parse(txtCustomerID.Text));
            cnn.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            cnn.Parameters.AddWithValue("@Gender", cmbGender.GetItemText(cmbGender.SelectedItem));
            cnn.Parameters.AddWithValue("@Email", txtEmail.Text);
            cnn.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Saved");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True; TrustServerCertificate=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("Select * from customers", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("update customers set [ customername]=@customername, gender=@gender, email=@email, phone=@phone where customerid=@customerid", con);
            cnn.Parameters.AddWithValue("@CustomerID", int.Parse(txtCustomerID.Text));
            cnn.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            cnn.Parameters.AddWithValue("@Gender", cmbGender.GetItemText(cmbGender.SelectedItem));
            cnn.Parameters.AddWithValue("@Email", txtEmail.Text);
            cnn.Parameters.AddWithValue("@Phone", txtPhone.Text);
            cnn.ExecuteNonQuery(); 
            con.Close();
            MessageBox.Show("Record Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("delete from customers where customerid=@customerid", con);
            cnn.Parameters.AddWithValue("@CustomerID", int.Parse(txtCustomerID.Text));
           
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Deleted");
        
            }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Customer_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True; TrustServerCertificate=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("Select * from customers", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void txtCustomerID_KeyDown(object sender, KeyEventArgs e)
        {
            // also checks if enter was pressed
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtCustomerName.Focus();
            }
        }
    }
    }
 