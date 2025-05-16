using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace УП_PR1.Forms
{
    public partial class PartnerAddForm : Form
    {
        private readonly string connectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";
        private readonly int userId;
        private readonly List<TextBox> requiredTextBoxes = new List<TextBox>();

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
                        if (!decimal.TryParse(txtRating.Text, out decimal rating) || rating < 0 || rating > 10)
                        {
                            MessageBox.Show("Рейтинг должен быть в диапазоне от 0 до 10");
                            txtRating.SelectAll();
                            return;
                        }

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

                        foreach (var textBox in requiredTextBoxes)
                        {
                            textBox.Focus();
                            this.ValidateChildren();
                        }

                        bool hasErrors = requiredTextBoxes.Any(t => !string.IsNullOrEmpty(errorProvider1.GetError(t)));

                        if (hasErrors)
                        {
                            MessageBox.Show("Заполните все обязательные поля", "Ошибка",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
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

        private void PartnerAddForm_Load(object sender, EventArgs e)
        {
            requiredTextBoxes.Add(txtName);
            requiredTextBoxes.Add(txtDirector);
            requiredTextBoxes.Add(txtLegalAddress);
            requiredTextBoxes.Add(txtINN);

            foreach (var textBox in requiredTextBoxes)
            {
                textBox.Validating += TextBox_Validating;
            }
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                errorProvider1.SetError(textBox, "Поле обязательно для заполнения");
            }
            else
            {
                errorProvider1.SetError(textBox, "");
            }
        }

        private void txtINN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }

            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 10 && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
                return;
            }
        }
    }
}