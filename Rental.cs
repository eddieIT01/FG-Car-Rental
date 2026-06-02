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
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRentalID.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtCarNumber.Text) || string.IsNullOrWhiteSpace(txtTotal.Text))
            {
                MessageBox.Show("Please fill in all fields before saving.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("insert into rentals Values(@rentalid,@customername, @carnumber, @startdate, @enddate, @total)", con);
            cnn.Parameters.AddWithValue("@RentalID", int.Parse(txtRentalID.Text));
            cnn.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            cnn.Parameters.AddWithValue("@CarNumber", txtCarNumber.Text);
            cnn.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
            cnn.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
            cnn.Parameters.AddWithValue("@Total", int.Parse(txtTotal.Text));
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Saved");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True; TrustServerCertificate=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("Select * from rentals", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRentalID.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtCarNumber.Text) || string.IsNullOrWhiteSpace(txtTotal.Text))
            {
                MessageBox.Show("Please fill in all fields before updating.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("update rentals set [customer name]=@customername, [car number]=@carnumber, [start date]=@startdate, [end date]=@enddate, total=@total where rentalid=@rentalid", con);
            cnn.Parameters.AddWithValue("@RentalID", int.Parse(txtRentalID.Text));
            cnn.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);
            cnn.Parameters.AddWithValue("@CarNumber", txtCarNumber.Text);
            cnn.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
            cnn.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
            cnn.Parameters.AddWithValue("@Total", int.Parse(txtTotal.Text));
            cnn.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Updated");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRentalID.Text))
            {
                MessageBox.Show("Please enter a Rental ID to delete.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True;TrustServerCertificate=True");

            con.Open();
            SqlCommand cnn = new SqlCommand("delete from rentals where rentalid=@rentalid", con);
            cnn.Parameters.AddWithValue("@RentalID", int.Parse(txtRentalID.Text));
            cnn.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Record Deleted");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtRentalID.Text = "";
            txtCustomerName.Text = "";
            txtCarNumber.Text = "";
            txtTotal.Text = "";
        }

        private void Rental_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-H67GPVM\SQLEXPRESS; Initial Catalog=RentalDB; Integrated Security=True; TrustServerCertificate=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("Select * from rentals", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }
    }
}
