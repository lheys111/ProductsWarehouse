using System;
using System.Data.SQLite;
using System.Windows.Forms;
using ProductsWarehouse.Database;
using ProductsWarehouse.Models;

namespace ProductsWarehouse.Forms
{
    public partial class ProductForm : Form
    {
        private DatabaseHelper dbHelper;
        private Product currentProduct;
        private bool isEditMode;

        public ProductForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            isEditMode = false;
            this.Text = "Добавление товара";
        }

        public ProductForm(Product product)
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            currentProduct = product;
            isEditMode = true;
            this.Text = "Редактирование товара";
            LoadProductData();
        }

        private void LoadProductData()
        {
            txtArticle.Text = currentProduct.Article;
            txtName.Text = currentProduct.Name;
            txtCategory.Text = currentProduct.Category;
            txtUnit.Text = currentProduct.Unit;
            txtPrice.Text = currentProduct.PurchasePrice.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                string.IsNullOrWhiteSpace(txtUnit.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Некорректная цена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = dbHelper.GetConnection())
                {
                    connection.Open();

                    if (isEditMode)
                    {
                        string query = @"UPDATE Products 
                                        SET Article = @article, Name = @name, Category = @category, 
                                            Unit = @unit, PurchasePrice = @price
                                        WHERE Id = @id";

                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", currentProduct.Id);
                            command.Parameters.AddWithValue("@article", txtArticle.Text.Trim());
                            command.Parameters.AddWithValue("@name", txtName.Text.Trim());
                            command.Parameters.AddWithValue("@category", txtCategory.Text.Trim());
                            command.Parameters.AddWithValue("@unit", txtUnit.Text.Trim());
                            command.Parameters.AddWithValue("@price", price);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string checkQuery = "SELECT COUNT(*) FROM Products WHERE Article = @article";
                        using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@article", txtArticle.Text.Trim());
                            long count = (long)checkCommand.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Артикул уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        string query = @"INSERT INTO Products (Article, Name, Category, Unit, PurchasePrice, CurrentStock, IsActive)
                                        VALUES (@article, @name, @category, @unit, @price, 0, 1)";

                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@article", txtArticle.Text.Trim());
                            command.Parameters.AddWithValue("@name", txtName.Text.Trim());
                            command.Parameters.AddWithValue("@category", txtCategory.Text.Trim());
                            command.Parameters.AddWithValue("@unit", txtUnit.Text.Trim());
                            command.Parameters.AddWithValue("@price", price);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Товар сохранен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }
    }
}
