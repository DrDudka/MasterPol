using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace УП_PR1
{
    public partial class HistoryForm : Form
    {
        private readonly string connectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";
        private readonly int userId;

        public HistoryForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void HistroryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

                        string query = "SELECT pp.products_partners_id, pa.name AS \"Наименование партнера\", pr.name AS \"Наименование продукции\", pp.count AS \"Количество\", pp.date AS \"Дата\"\r\nFROM partners as pa\r\nJOIN partners_products as pp ON pa.partner_id = pp.partner_id\r\nJOIN products as pr ON pr.product_id = pp.product_id\r\nORDER BY pa.name";

                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            DataTable dt = new DataTable();
                            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                            adapter.Fill(dt);

                            dataGridViewHistory.DataSource = dt;

                            dataGridViewHistory.Columns["products_partners_id"].Visible = false;
                            dataGridViewHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridViewHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            dataGridViewHistory.MultiSelect = false;
                            dataGridViewHistory.ReadOnly = true;
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

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            MenuForm menuForm = new MenuForm(userId);
            menuForm.Show();
            this.Hide();
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            var calcForm = new CalculateMaterialForm();
            calcForm.ShowDialog();
        }
    }
}
