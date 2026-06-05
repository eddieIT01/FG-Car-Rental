using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CarRental
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }

        private async void btnLogin_Click(object sender, EventArgs e, HttpClient client)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter username and password.", "Missing Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ignore SSL for localhost
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (m, c, ch, e2) => true;
                var httpClient = new HttpClient(handler);

                var body = JsonConvert.SerializeObject(new
                {
                    username = txtUsername.Text,
                    password = txtPassword.Text
                });

                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(
                    "https://localhost:7215/api/admin/login", content);

                if (response.IsSuccessStatusCode)
                {
                    Main mn = new Main();
                    mn.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to API. Make sure the API project is running.\n" + ex.Message,
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtPassword.Focus(); }
        }
    }
}