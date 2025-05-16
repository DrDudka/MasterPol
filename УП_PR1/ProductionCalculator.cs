using Npgsql;
using System;
using System.Windows.Forms;

namespace УП_PR1
{
    public static class ProductionCalculator
    {
        private static readonly string ConnectionString = "Host=localhost;Database=master_pol;Username=postgres;Password=123;Port=5433";

        public static int CalculateRequiredMaterial(
            int productTypeId,
            int materialTypeId,
            int productQuantity,
            double parameter1,
            double parameter2)
        {
            try
            {
                

                // Получаем коэффициенты из БД
                (double productCoefficient, double defectPercentage) = GetCoefficientsFromDatabase(productTypeId, materialTypeId);

                if (productQuantity <= 0 || parameter1 <= 0 || parameter2 <= 0)
                {
                    MessageBox.Show("Ошибка: один из параметров меньше или равен нулю");
                    return -1;
                }

                if (productCoefficient < 0 || defectPercentage < 0)
                {
                    MessageBox.Show("Ошибка: не найдены коэффициенты в БД");
                    return -1;
                }

                if (productCoefficient < 0 || defectPercentage < 0)
                    return -1;

                // Расчет материала на одну единицу продукции
                double materialPerUnit = parameter1 * parameter2 * productCoefficient;

                // Расчет общего количества материала с учетом брака
                double totalMaterial = productQuantity * materialPerUnit;
                double totalWithDefect = totalMaterial * (1 + defectPercentage / 100);

                // Округляем в большую сторону
                return (int)Math.Ceiling(totalWithDefect);
            }
            catch
            {
                return -1;
            }
        }

        private static (double productCoefficient, double defectPercentage) GetCoefficientsFromDatabase(
            int productTypeId, int materialTypeId)
        {
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    pt.coeff,
                    mt.defect_percentage
                FROM product_type pt
                JOIN material_type mt ON pt.material_type_id = mt.material_type_id
                WHERE pt.product_type_id = @productTypeId 
                AND pt.material_type_id = @materialTypeId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@productTypeId", productTypeId);
                        cmd.Parameters.AddWithValue("@materialTypeId", materialTypeId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return (
                                    Convert.ToDouble(reader["coeff"]),
                                    Convert.ToDouble(reader["defect_percentage"])
                                );
                            }
                            else
                            {
                                MessageBox.Show("Не найдены записи в БД для указанных ID");
                                return (-1, -1);
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show($"Ошибка БД: {ex.Message}");
                    return (-1, -1);
                }
            }
        }
    }
}
