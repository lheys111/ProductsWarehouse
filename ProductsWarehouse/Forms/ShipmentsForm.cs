using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using ProductsWarehouse.Database;

namespace ProductsWarehouse.Forms
{
    public partial class ShipmentsForm : Form
    {
        private DatabaseHelper dbHelper;

        public ShipmentsForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
            LoadShipments();
        }

        private void LoadShipments()
        {
            try
            {
                var shipments = new List<ShipmentDisplay>();

                using (var connection = dbHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        SELECT s.Id, s.DocumentNumber, s.DocumentDate, s.Status, 
                               u.FullName as UserName,
                               (SELECT COUNT(*) FROM ShipmentItems WHERE ShipmentId = s.Id) as ItemsCount
                        FROM Shipments s
                        JOIN Users u ON s.CreatedByUserId = u.Id
                        ORDER BY s.DocumentDate DESC";

                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shipments.Add(new ShipmentDisplay
                            {
                                Id = reader.GetInt32(0),
                                DocumentNumber = reader.GetString(1),
                                DocumentDate = DateTime.Parse(reader.GetString(2)),
                                Status = reader.GetString(3),
                                UserName = reader.GetString(4),
                                ItemsCount = reader.GetInt32(5)
                            });
                        }
                    }
                }

                dgvShipments.DataSource = null;
                dgvShipments.DataSource = shipments;

                dgvShipments.Columns["Id"].Visible = false;
                dgvShipments.Columns["DocumentNumber"].HeaderText = "Номер документа";
                dgvShipments.Columns["DocumentDate"].HeaderText = "Дата";
                dgvShipments.Columns["Status"].HeaderText = "Статус";
                dgvShipments.Columns["UserName"].HeaderText = "Кто создал";
                dgvShipments.Columns["ItemsCount"].HeaderText = "Кол-во позиций";

                dgvShipments.Columns["DocumentDate"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

                dgvShipments.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadShipments();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvShipments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите отгрузку", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selected = (ShipmentDisplay)dgvShipments.SelectedRows[0].DataBoundItem;
            MessageBox.Show($"Просмотр отгрузки {selected.DocumentNumber} будет доступен в следующей версии",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Создание новой отгрузки будет доступно в следующей версии",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class ShipmentDisplay
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public int ItemsCount { get; set; }
    }
}