using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CarRental
{
    public partial class Customer : Form
    {
        private static readonly HttpClient client = CreateClient();
        private static HttpClient CreateClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            return new HttpClient(handler);
        }

        public Customer() { InitializeComponent(); }

        private async void Customer_Load(object sender, EventArgs e) { await LoadCustomers(); }

        private async System.Threading.Tasks.Task LoadCustomers()
        {
            try
            {
                var res = await client.GetStringAsync("https://localhost:7215/api/customers");
                dataGridView1.DataSource = JsonConvert.DeserializeObject<System.Data.DataTable>(res);
            }
            catch { MessageBox.Show("Could not load customers. Is the API running?"); }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            { MessageBox.Show("Fill all fields.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                var c = new
                {
                    customerName = txtCustomerName.Text,
                    gender = cmbGender.GetItemText(cmbGender.SelectedItem),
                    email = txtEmail.Text,
                    phone = txtPhone.Text,
                    username = txtCustomerName.Text.ToLower().Replace(" ", ""),
                    password = "pass123"
                };
                var body = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                var res = await client.PostAsync("https://localhost:7215/api/customers/register", body);
                MessageBox.Show(res.IsSuccessStatusCode ? "Customer saved!" : "Error saving.");
                await LoadCustomers();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text)) { MessageBox.Show("Enter Customer ID."); return; }
            try
            {
                var c = new
                {
                    customerID = int.Parse(txtCustomerID.Text),
                    customerName = txtCustomerName.Text,
                    gender = cmbGender.GetItemText(cmbGender.SelectedItem),
                    email = txtEmail.Text,
                    phone = txtPhone.Text,
                    username = txtCustomerName.Text.ToLower().Replace(" ", ""),
                    password = "pass123"
                };
                var body = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                var res = await client.PutAsync($"https://localhost:7215/api/customers/{txtCustomerID.Text}", body);
                MessageBox.Show(res.IsSuccessStatusCode ? "Updated!" : "Error updating.");
                await LoadCustomers();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text)) { MessageBox.Show("Enter Customer ID."); return; }
            try
            {
                var res = await client.DeleteAsync($"https://localhost:7215/api/customers/{txtCustomerID.Text}");
                MessageBox.Show(res.IsSuccessStatusCode ? "Deleted!" : "Error deleting.");
                await LoadCustomers();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private async void btnAdd_Click(object sender, EventArgs e) { await LoadCustomers(); }

        private void label5_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtCustomerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtCustomerName.Focus(); }
        }
    }
}