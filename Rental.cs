using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CarRental
{
    public partial class Rental : Form
    {
        private static readonly HttpClient client = CreateClient();
        private static HttpClient CreateClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            return new HttpClient(handler);
        }

        public Rental() { InitializeComponent(); }

        private async void Rental_Load(object sender, EventArgs e) { await LoadRentals(); }

        private async System.Threading.Tasks.Task LoadRentals()
        {
            try
            {
                var res = await client.GetStringAsync("https://localhost:7215/api/rentals");
                dataGridView1.DataSource = JsonConvert.DeserializeObject<System.Data.DataTable>(res);
            }
            catch { MessageBox.Show("Could not load rentals. Is the API running?"); }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtCarNumber.Text))
            { MessageBox.Show("Fill all fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                var rental = new
                {
                    customerID = int.Parse(txtRentalID.Text),
                    carID = int.Parse(txtCarNumber.Text),
                    startDate = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"),
                    endDate = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd"),
                    total = 0,
                    status = "Active"
                };
                var body = new StringContent(JsonConvert.SerializeObject(rental), Encoding.UTF8, "application/json");
                var res = await client.PostAsync("https://localhost:7215/api/rentals", body);
                var responseText = await res.Content.ReadAsStringAsync();
                MessageBox.Show(res.IsSuccessStatusCode ? "Booking confirmed! " + responseText : "Error: " + responseText);
                await LoadRentals();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRentalID.Text)) { MessageBox.Show("Enter Rental ID."); return; }
            try
            {
                var res = await client.DeleteAsync($"https://localhost:7215/api/rentals/{txtRentalID.Text}");
                MessageBox.Show(res.IsSuccessStatusCode ? "Deleted!" : "Error deleting.");
                await LoadRentals();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRentalID.Text)) { MessageBox.Show("Enter Rental ID."); return; }
            try
            {
                var res = await client.PutAsync(
                    $"https://localhost:7215/api/rentals/{txtRentalID.Text}/cancel",
                    new StringContent("", Encoding.UTF8, "application/json"));
                MessageBox.Show(res.IsSuccessStatusCode ? "Rental cancelled!" : "Error cancelling.");
                await LoadRentals();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnAdd_Click(object sender, EventArgs e) { await LoadRentals(); }

        private void button1_Click(object sender, EventArgs e)
        {
            txtRentalID.Text = ""; txtCustomerName.Text = "";
            txtCarNumber.Text = ""; txtTotal.Text = "";
        }
    }
}