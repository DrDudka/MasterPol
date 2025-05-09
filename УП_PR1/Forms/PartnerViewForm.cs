using Npgsql;
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

                string query = @"SELECT * FROM partners";
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

        private Panel CreatePartnerPanel(NpgsqlDataReader reader)
        {
            Panel panel = new Panel()
            {
                Size = new Size(950, 150),
                BorderStyle = BorderStyle.Fixed3D,
                Margin = new Padding(10),
                BackColor = Color.FromArgb(0x67, 0xBA, 0x80)
            };

            Label TypeAndPartner = new Label()
            {
                Location = new Point(10, 10),
                Text = reader["partner_type"] + " | " + reader["name"],
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

            return panel;
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
    }
}

