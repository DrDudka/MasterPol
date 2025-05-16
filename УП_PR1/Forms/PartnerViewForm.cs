using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using УП_PR1.Forms;

namespace УП_PR1
{
    public partial class PartnerViewForm : Form
    {
        private readonly string connectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";
        private readonly int userId;

        public PartnerViewForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT pa.name, pa.partner_type, pa.director, pa.phone, pa.rating,
                               SUM(pp.count) AS count
                        FROM partners as pa
                        JOIN partners_products as pp ON pa.partner_id = pp.partner_id
                        JOIN products as pr ON pr.product_id = pp.product_id
                        GROUP BY pa.partner_id
                        ORDER BY pa.name";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Panel panel = CreatePartnerPanel(reader);
                        flowLayoutPanel1.Controls.Add(panel);
                    }
                }
            }
        }

        private static decimal CalculateDiscount(decimal totalSales)
        {
            if (totalSales < 10000m) return 0m;
            if (totalSales < 50000m) return 5m;
            if (totalSales < 300000m) return 10m;
            return 15m;
        }

        private Panel CreatePartnerPanel(NpgsqlDataReader reader)
        {
            Panel panel = new Panel()
            {
                Size = new Size(950, 150),
                BorderStyle = BorderStyle.Fixed3D,
                Margin = new Padding(10),
                BackColor = Color.FromArgb(0x67, 0xBA, 0x80),
                Cursor = Cursors.Hand
            };

            panel.Click += (s, e) => OpenPartnerEditForm();

            Label TypeAndPartner = new Label()
            {
                Location = new Point(10, 10),
                Text = reader["partner_type"] + " | " + reader["name"],
                AutoSize = true,
                Font = new Font("Segoe UI", 16),
            };

            decimal totalSales = reader["count"] != DBNull.Value ? Convert.ToDecimal(reader["count"]) : 0;
            decimal discount = CalculateDiscount(totalSales);

            Label Discount = new Label()
            {
                Location = new Point(850, 10),
                Text = $"{discount}%",
                AutoSize = true,
                Font = new Font("Segoe UI", 16)
            };

            Label Director = new Label()
            {
                Location = new Point(10, 40),
                Text = reader["director"].ToString(),
                AutoSize = true,
                Font = new Font("Segoe UI", 14)
            };

            Label Phone = new Label()
            {
                Location = new Point(10, 60),
                Text = reader["phone"].ToString(),
                AutoSize = true,
                Font = new Font("Segoe UI", 14)
            };

            Label Rating = new Label()
            {
                Location = new Point(10, 80),
                Text = "Рейтинг: " + reader["rating"].ToString(),
                AutoSize = true,
                Font = new Font("Segoe UI", 14)
            };

            panel.Controls.Add(TypeAndPartner);
            panel.Controls.Add(Director);
            panel.Controls.Add(Phone);
            panel.Controls.Add(Rating);
            panel.Controls.Add(Discount);

            return panel;
        }

        private void OpenPartnerEditForm()
        {
            PartnerEditForm editForm = new PartnerEditForm();
            editForm.FormClosed += (s, args) => LoadData();
            editForm.Show();
            this.Hide();
        }

        private void PartnerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonMenu_Click(object sender, System.EventArgs e)
        {
            MenuForm menuForm = new MenuForm(userId);
            menuForm.Show();
            this.Hide();
        }

        private void buttonAddPartner_Click(object sender, System.EventArgs e)
        {
            PartnerAddForm partnerAddForm = new PartnerAddForm();
            partnerAddForm.Show();
            this.Hide();
        }

        private void buttonEditPartner_Click(object sender, System.EventArgs e)
        {
            PartnerEditForm partnerEditForm = new PartnerEditForm();
            partnerEditForm.Show();
            this.Hide();
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            var calcForm = new HistoryForm();
            calcForm.Show();
            this.Hide();
        }
    }
}

