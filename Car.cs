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

namespace CarRental
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("update cars set carname=@carname, carnumber=@carnumber, carmodel=@carmodel, rentprice=@rentprice, carstatus=@carstatus where carid=@carid)", con);
            cnn.Parameters.AddWithValue("@CarID", int.Parse(txtCarID.Text));
            cnn.Parameters.AddWithValue("@CarName", txtCarName.Text);
            cnn.Parameters.AddWithValue("@CarNumber", txtCarNb.Text);
            cnn.Parameters.AddWithValue("@CarModel", txtCarModel.Text);
            cnn.Parameters.AddWithValue("@RentPrice", int.Parse(txtRentPrice.Text));
            cnn.Parameters.AddWithValue("@CarStatus", txtCarStatus.Text);
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("delete from cars where carid=@carid", con);
            cnn.Parameters.AddWithValue("@CarID", int.Parse(txtCarID.Text));
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Deleted");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("insert into cars Values(@carid,@carname, @carnumber, @carmodel, @rentprice, @carstatus)", con);
            cnn.Parameters.AddWithValue("@CarID", int.Parse(txtCarID.Text));
            cnn.Parameters.AddWithValue("@CarName", txtCarName.Text);
            cnn.Parameters.AddWithValue("@CarNumber", txtCarNb.Text);
            cnn.Parameters.AddWithValue("@CarModel", txtCarModel.Text);
            cnn.Parameters.AddWithValue("@RentPrice", int.Parse(txtRentPrice.Text));
            cnn.Parameters.AddWithValue("@CarStatus", txtCarStatus.Text);
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Saved");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True; TrustServerCertificate=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("Select * from cars", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void Car_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True; TrustServerCertificate=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("Select * from cars", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCarID.Text = "";
            txtCarName.Text = "";
            txtCarNb.Text = "";
            txtCarModel.Text = "";
            txtRentPrice.Text = "";
            txtCarStatus.Text = "";

        }
    }
}
