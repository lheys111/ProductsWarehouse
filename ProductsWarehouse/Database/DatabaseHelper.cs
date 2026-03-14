using System;
using System.Data.SQLite;
using System.IO;
using ProductsWarehouse.Helpers;

namespace ProductsWarehouse.Database
{
    public class DatabaseHelper
    {
        private readonly string connectionString;
        private readonly string dbPath;

        public DatabaseHelper()
        {
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ProductsWarehouse");

            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            dbPath = Path.Combine(appDataPath, "warehouse.db");
            connectionString = $"Data Source={dbPath};Version=3;";
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public string GetDatabasePath()
        {
            return dbPath;
        }

        public void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using (var connection = GetConnection())
            {
                connection.Open();

                // Таблица пользователей
                string createUsers = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Email TEXT UNIQUE NOT NULL,
                        PasswordHash TEXT NOT NULL,
                        FullName TEXT NOT NULL,
                        Role TEXT NOT NULL,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                    )";

                // Таблица товаров
                string createProducts = @"
                    CREATE TABLE IF NOT EXISTS Products (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Article TEXT UNIQUE NOT NULL,
                        Name TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Unit TEXT NOT NULL,
                        PurchasePrice DECIMAL(10,2) NOT NULL,
                        CurrentStock INTEGER NOT NULL DEFAULT 0,
                        IsActive BOOLEAN DEFAULT 1
                    )";

                // Таблица отгрузок
                string createShipments = @"
                    CREATE TABLE IF NOT EXISTS Shipments (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DocumentNumber TEXT UNIQUE NOT NULL,
                        DocumentDate DATETIME NOT NULL,
                        Status TEXT NOT NULL,
                        CreatedByUserId INTEGER NOT NULL,
                        CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                        FOREIGN KEY (CreatedByUserId) REFERENCES Users(Id)
                    )";

                // Таблица позиций отгрузки
                string createShipmentItems = @"
                    CREATE TABLE IF NOT EXISTS ShipmentItems (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ShipmentId INTEGER NOT NULL,
                        ProductId INTEGER NOT NULL,
                        ProductName TEXT NOT NULL,
                        ProductArticle TEXT NOT NULL,
                        Quantity INTEGER NOT NULL,
                        Price DECIMAL(10,2) NOT NULL,
                        FOREIGN KEY (ShipmentId) REFERENCES Shipments(Id),
                        FOREIGN KEY (ProductId) REFERENCES Products(Id)
                    )";

                ExecuteNonQuery(connection, createUsers);
                ExecuteNonQuery(connection, createProducts);
                ExecuteNonQuery(connection, createShipments);
                ExecuteNonQuery(connection, createShipmentItems);

                CreateDefaultAdmin(connection);
            }
        }

        private void ExecuteNonQuery(SQLiteConnection connection, string query)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void CreateDefaultAdmin(SQLiteConnection connection)
        {
            string check = "SELECT COUNT(*) FROM Users WHERE Role = 'Admin'";
            using (var command = new SQLiteCommand(check, connection))
            {
                long count = (long)command.ExecuteScalar();
                if (count == 0)
                {
                    string passwordHash = PasswordHasher.HashPassword("admin123");
                    string insert = @"
                        INSERT INTO Users (Email, PasswordHash, FullName, Role, CreatedAt) 
                        VALUES (@email, @passwordHash, @fullName, @role, datetime('now'))";

                    using (var cmd = new SQLiteCommand(insert, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", "admin@ptichki.ru");
                        cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@fullName", "Главный администратор");
                        cmd.Parameters.AddWithValue("@role", "Admin");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void SeedTestData()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    string checkStorekeepers = "SELECT COUNT(*) FROM Users WHERE Role = 'Storekeeper'";
                    using (var cmd = new SQLiteCommand(checkStorekeepers, connection))
                    {
                        long count = (long)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            AddTestStorekeepers(connection);
                        }
                    }

                    string checkProducts = "SELECT COUNT(*) FROM Products";
                    using (var cmd = new SQLiteCommand(checkProducts, connection))
                    {
                        long count = (long)cmd.ExecuteScalar();
                        if (count == 0)
                        {
                            AddTestProducts(connection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка при создании тестовых данных: {ex.Message}");
            }
        }

        private void AddTestStorekeepers(SQLiteConnection connection)
        {
            string[] storekeepers = new[]
            {
                "ivanov@mail.ru|Иванов Иван Иванович|pass123",
                "petrov@mail.ru|Петров Петр Петрович|pass123",
                "sidorov@mail.ru|Сидоров Сидор Сидорович|pass123"
            };

            string insertQuery = @"
                INSERT INTO Users (Email, PasswordHash, FullName, Role, CreatedAt)
                VALUES (@email, @passwordHash, @fullName, 'Storekeeper', datetime('now'))";

            foreach (var sk in storekeepers)
            {
                var parts = sk.Split('|');
                string email = parts[0];
                string fullName = parts[1];
                string password = parts[2];
                string passwordHash = PasswordHasher.HashPassword(password);

                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void AddTestProducts(SQLiteConnection connection)
        {
            string[] products = new[]
            {
                "PR-001|Гречка|Крупы|кг|45.50",
                "PR-002|Рис|Крупы|кг|65.00",
                "PR-003|Пшено|Крупы|кг|35.20",
                "PR-004|Макароны|Макаронные изделия|кг|50.00",
                "PR-005|Спагетти|Макаронные изделия|кг|70.00"
            };

            Random rand = new Random();
            string insertQuery = @"
                INSERT INTO Products (Article, Name, Category, Unit, PurchasePrice, CurrentStock, IsActive)
                VALUES (@article, @name, @category, @unit, @price, @stock, 1)";

            foreach (var prod in products)
            {
                var parts = prod.Split('|');
                string article = parts[0];
                string name = parts[1];
                string category = parts[2];
                string unit = parts[3];
                decimal price = decimal.Parse(parts[4]);
                int stock = rand.Next(20, 100);

                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@article", article);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@unit", unit);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}