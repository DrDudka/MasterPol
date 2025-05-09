using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace УП_PR1.Forms
{
    public partial class PartnerEditForm : Form
    {
        private readonly string connectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";
        private readonly int userId;

        public PartnerEditForm()
        {
            InitializeComponent();
            LoadData();
            ConfigureDataGridView();
        }

        private void LoadData()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT * FROM Partners";

                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            DataTable dt = new DataTable();
                            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                            adapter.Fill(dt);

                            dataGridViewPartners.DataSource = dt;

                            dataGridViewPartners.Columns["partner_id"].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridViewPartners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPartners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPartners.MultiSelect = false;
            dataGridViewPartners.ReadOnly = true;

            dataGridViewPartners.Columns["partner_type"].HeaderText = "Тип партнера";
            dataGridViewPartners.Columns["name"].HeaderText = "Наименование партнера";
            dataGridViewPartners.Columns["director"].HeaderText = "Директор";
            dataGridViewPartners.Columns["email"].HeaderText = "Электронная почта";
            dataGridViewPartners.Columns["phone"].HeaderText = "Номер телефона";
            dataGridViewPartners.Columns["legal_address"].HeaderText = "Юридический адрес";
            dataGridViewPartners.Columns["inn"].HeaderText = "ИНН";
            dataGridViewPartners.Columns["rating"].HeaderText = "Рейтинг";
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm(userId);
            menuForm.Show();
            this.Hide();
        }

        private void PartnerEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridViewPartners_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPartners.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewPartners.SelectedRows[0];

                comboBoxPartnerType.Text = row.Cells["partner_type"].Value.ToString();
                txtName.Text = row.Cells["name"].Value.ToString();
                txtDirector.Text = row.Cells["director"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtLegalAddress.Text = row.Cells["legal_address"].Value.ToString();
                txtINN.Text = row.Cells["inn"].Value.ToString();
                txtRating.Text = row.Cells["rating"].Value.ToString();
            }
        }

        private void buttonEditPartner_Click(object sender, EventArgs e)
        {
            if (dataGridViewPartners.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите партнёра для редактирования!");
                return;
            }

            int partnerId = Convert.ToInt32(dataGridViewPartners.SelectedRows[0].Cells["partner_id"].Value);

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Partners 
                        SET 
                            partner_type = @PartnerType,
                            name = @Name,
                            director = @Director,
                            email = @Email,
                            phone = @Phone,
                            legal_address = @LegalAddress,
                            inn = @INN,
                            rating = @Rating
                          WHERE partner_id = @PartnerId";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PartnerId", partnerId);
                        command.Parameters.AddWithValue("@PartnerType", comboBoxPartnerType.Text);
                        command.Parameters.AddWithValue("@Name", txtName.Text);
                        command.Parameters.AddWithValue("@Director", txtDirector.Text);
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        command.Parameters.AddWithValue("@LegalAddress", txtLegalAddress.Text);
                        command.Parameters.AddWithValue("@INN", txtINN.Text);
                        command.Parameters.AddWithValue("@Rating", Convert.ToDecimal(txtRating.Text));

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Данные обновлены!", "Успех");
                            LoadData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void buttonAddPartner_Click(object sender, EventArgs e)
        {
            PartnerAddForm partnerAddForm = new PartnerAddForm();
            partnerAddForm.Show();
            this.Hide();
        }
    }
}
