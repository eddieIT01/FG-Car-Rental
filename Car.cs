using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarRental
{
    public partial class Car : Form
    {
        private static readonly HttpClient client = CreateClient();

        private static HttpClient CreateClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            return new HttpClient(handler);
        }

        public Car() { InitializeComponent(); }

        private async void Car_Load(object sender, EventArgs e)
        {
            await LoadCars();
        }

        private async System.Threading.Tasks.Task LoadCars()
        {
            try
            {
                var res = await client.GetStringAsync("https://localhost:7215/api/cars");
                var cars = JsonConvert.DeserializeObject<List<dynamic>>(res);
                dataGridView1.DataSource = JsonConvert.DeserializeObject<System.Data.DataTable>(res);
            }
            catch { MessageBox.Show("Could not load cars. Is the API running?"); }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarName.Text) || string.IsNullOrWhiteSpace(txtCarNb.Text))
            {
                MessageBox.Show("Please fill all fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var car = new
                {
                    carName = txtCarName.Text,
                    carNumber = txtCarNb.Text,
                    carModel = txtCarModel.Text,
                    rentPrice = int.Parse(txtRentPrice.Text),
                    carStatus = txtCarStatus.Text,
                    carClass = (string)null,
                    color = (string)null
                };
                var body = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
                var res = await client.PostAsync("https://localhost:7215/api/cars", body);
                MessageBox.Show(res.IsSuccessStatusCode ? "Car saved!" : "Error saving car.");
                await LoadCars();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarID.Text)) { MessageBox.Show("Enter Car ID."); return; }
            try
            {
                var car = new
                {
                    carID = int.Parse(txtCarID.Text),
                    carName = txtCarName.Text,
                    carNumber = txtCarNb.Text,
                    carModel = txtCarModel.Text,
                    rentPrice = int.Parse(txtRentPrice.Text),
                    carStatus = txtCarStatus.Text,
                    carClass = (string)null,
                    color = (string)null
                };
                var body = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
                var res = await client.PutAsync($"https://localhost:7215/api/cars/{txtCarID.Text}", body);
                MessageBox.Show(res.IsSuccessStatusCode ? "Car updated!" : "Error updating.");
                await LoadCars();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarID.Text)) { MessageBox.Show("Enter Car ID."); return; }
            try
            {
                var res = await client.DeleteAsync($"https://localhost:7215/api/cars/{txtCarID.Text}");
                MessageBox.Show(res.IsSuccessStatusCode ? "Car deleted!" : "Error deleting.");
                await LoadCars();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            await LoadCars();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtCarID.Text = ""; txtCarName.Text = ""; txtCarNb.Text = "";
            txtCarModel.Text = ""; txtRentPrice.Text = ""; txtCarStatus.Text = "";
        }
    }
}