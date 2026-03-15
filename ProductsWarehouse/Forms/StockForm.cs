using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using ProductsWarehouse.Database;
using ProductsWarehouse.Models;

namespace ProductsWarehouse.Forms
{
    public partial class StockForm : Form
    {
        private DatabaseHelper dbHelper;
        private List<Product> products;

        public StockForm()
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
                                    CurrentStock = reader.GetInt32(6)
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
            dgvStock.DataSource = null;
            dgvStock.DataSource = products;

            dgvStock.Columns["Id"].HeaderText = "ID";
            dgvStock.Columns["Article"].HeaderText = "Артикул";
            dgvStock.Columns["Name"].HeaderText = "Наименование";
            dgvStock.Columns["Category"].HeaderText = "Категория";
            dgvStock.Columns["Unit"].HeaderText = "Ед. изм.";
            dgvStock.Columns["PurchasePrice"].HeaderText = "Цена";
            dgvStock.Columns["CurrentStock"].HeaderText = "Остаток";
            dgvStock.Columns["IsActive"].Visible = false;

            dgvStock.Columns["PurchasePrice"].DefaultCellStyle.Format = "C2";

            // Подсветка остатков
            dgvStock.CellFormatting += (s, e) =>
            {
                if (dgvStock.Columns[e.ColumnIndex].Name == "CurrentStock" && e.Value != null)
                {
                    int stock = (int)e.Value;
                    if (stock == 0)
                        e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                    else if (stock < 10)
                        e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
                }
            };

            dgvStock.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text.Trim());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadProducts();
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
