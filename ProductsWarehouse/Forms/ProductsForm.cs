using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using ProductsWarehouse.Database;
using ProductsWarehouse.Models;

namespace ProductsWarehouse.Forms
{
    public partial class ProductsForm : Form
    {
        private DatabaseHelper dbHelper;
        private List<Product> products;
        private Product selectedProduct;

        public ProductsForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            products = new List<Product>();
            LoadProducts();
        }

        private void LoadProducts(string searchText = "")
        {
            try
            {
                products.Clear();
                using (var connection = dbHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Products WHERE IsActive = 1";

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        query += " AND (Name LIKE @search OR Article LIKE @search OR Category LIKE @search)";
                    }

                    query += " ORDER BY Name";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            command.Parameters.AddWithValue("@search", $"%{searchText}%");
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                products.Add(new Product
                                {
                                    Id = reader.GetInt32(0),
                                    Article = reader.GetString(1),
                                    Name = reader.GetString(2),
                                    Category = reader.GetString(3),
                                    Unit = reader.GetString(4),
                                    PurchasePrice = reader.GetDecimal(5),
                                    CurrentStock = reader.GetInt32(6),
                                    IsActive = reader.GetBoolean(7)
                                });
                            }
                        }
                    }
                }

                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataGridView()
        {
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = products;

            dgvProducts.Columns["Id"].HeaderText = "ID";
            dgvProducts.Columns["Article"].HeaderText = "Артикул";
            dgvProducts.Columns["Name"].HeaderText = "Название";
            dgvProducts.Columns["Category"].HeaderText = "Категория";
            dgvProducts.Columns["Unit"].HeaderText = "Ед. изм.";
            dgvProducts.Columns["PurchasePrice"].HeaderText = "Цена";
            dgvProducts.Columns["CurrentStock"].HeaderText = "Остаток";
            dgvProducts.Columns["IsActive"].Visible = false;

            dgvProducts.Columns["PurchasePrice"].DefaultCellStyle.Format = "C2";
            dgvProducts.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            if (productForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;
                ProductForm productForm = new ProductForm(selectedProduct);
                if (productForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                selectedProduct = (Product)dgvProducts.SelectedRows[0].DataBoundItem;

                DialogResult result = MessageBox.Show($"Удалить товар '{selectedProduct.Name}'?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeleteProduct(selectedProduct.Id);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteProduct(int productId)
        {
            try
            {
                using (var connection = dbHelper.GetConnection())
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE Id = @id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", productId);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Товар удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadProducts(txtSearch.Text.Trim());
                e.Handled = true;
            }
        }

       
    }
}
