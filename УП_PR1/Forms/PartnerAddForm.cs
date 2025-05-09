using Npgsql;
using System;
using System.Windows.Forms;

namespace УП_PR1.Forms
{
    public partial class PartnerAddForm : Form
    {
        private readonly string connectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";
        private readonly int userId;

        public PartnerAddForm()
        {
            InitializeComponent();
        }

        private void PartnerAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ButtonAddPartner_Click(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Partners 
                        (partner_type, name, director, email, phone, legal_address, inn, rating) 
                        VALUES 
                        (@PartnerType, @Name, @Director, @Email, @Phone, @LegalAddress, @INN, @Rating)";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartnerType", comboBoxPartnerType.Text);
                        command.Parameters.AddWithValue("@PartnerType", txtDirector.Text);
                        command.Parameters.AddWithValue("@Name", txtName.Text);
                        command.Parameters.AddWithValue("@Director", txtDirector.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@LegalAddress", txtLegalAddress.Text);
                        command.Parameters.AddWithValue("@INN", txtINN.Text);
                        command.Parameters.AddWithValue("@Rating", Convert.ToDecimal(txtRating.Text));

                        command.ExecuteNonQuery(); 
                    }
                }

                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception)
            {
                MessageBox.Show($"Ошибка: Заполните данные!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            comboBoxPartnerType.Text = "";
            txtDirector.Text = "";
            txtName.Text = "";
            txtDirector.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtLegalAddress.Text = "";
            txtINN.Text = "";
            txtRating.Text = "";
        }

        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm(userId);
            menuForm.Show();
            this.Hide();
        }

        private void buttonEditPartner_Click(object sender, EventArgs e)
        {
            PartnerEditForm partnerEditForm = new PartnerEditForm();
            partnerEditForm.Show();
            this.Hide();
        }
    }
}
