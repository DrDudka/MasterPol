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

namespace УП_PR1
{
    public partial class CalculateMaterialForm : Form
    {
        private readonly string connectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";

        public CalculateMaterialForm()
        {
            InitializeComponent();
            LoadProductTypes();
            LoadMaterialTypes();
        }

        private void LoadProductTypes()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT product_type_id, product_type, coeff FROM product_type";

                var dt = new DataTable();
                new NpgsqlDataAdapter(query, conn).Fill(dt);

                comboProductType.DataSource = dt;
                comboProductType.DisplayMember = "product_type";
                comboProductType.ValueMember = "product_type_id";
            }
        }

        private void LoadMaterialTypes()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT material_type_id, material_type, defect_percentage FROM material_type";

                var dt = new DataTable();
                new NpgsqlDataAdapter(query, conn).Fill(dt);

                comboMaterialType.DataSource = dt;
                comboMaterialType.DisplayMember = "material_type";
                comboMaterialType.ValueMember = "material_type_id";
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            int productTypeId = (int)comboProductType.SelectedValue;
            int materialTypeId = (int)comboMaterialType.SelectedValue;
            int quantity = (int)numericQuantity.Value;
            double param1 = double.Parse(txtParam1.Text);
            double param2 = double.Parse(txtParam2.Text);

            int result = ProductionCalculator.CalculateRequiredMaterial(
                productTypeId, materialTypeId, quantity, param1, param2);

            if (result == -1)
            {
                MessageBox.Show("Ошибка в параметрах расчета", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Требуется материала: {result} единиц");
            }
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            errorProvider1.Clear();

            if (comboProductType.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboProductType, "Выберите тип продукции");
                isValid = false;
            }

            if (comboMaterialType.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboMaterialType, "Выберите тип материала");
                isValid = false;
            }

            if (numericQuantity.Value <= 0)
            {
                errorProvider1.SetError(numericQuantity, "Количество должно быть больше 0");
                isValid = false;
            }

            if (!double.TryParse(txtParam1.Text, out double param1) || param1 <= 0)
            {
                errorProvider1.SetError(txtParam1, "Введите положительное число");
                isValid = false;
            }

            if (!double.TryParse(txtParam2.Text, out double param2) || param2 <= 0)
            {
                errorProvider1.SetError(txtParam2, "Введите положительное число");
                isValid = false;
            }

            return isValid;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
