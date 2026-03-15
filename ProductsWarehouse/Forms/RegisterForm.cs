using System;
using System.Data.SQLite;
using System.Windows.Forms;
using ProductsWarehouse.Database;
using ProductsWarehouse.Helpers;

namespace ProductsWarehouse.Forms
{
    public partial class RegisterForm : Form
    {
        private DatabaseHelper dbHelper;

        public RegisterForm()
        {
            InitializeComponent();
            dbHelper = new DatabaseHelper();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // проверки
            if (fullName == "" || email == "" || password == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Пароль должен быть не меньше 6 символов");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Неправильный email");
                return;
            }

            try
            {
                dbHelper.GetConnection().Open();
                var conn = dbHelper.GetConnection();

                // проверка есть ли уже такой email
                string check = "SELECT COUNT(*) FROM Users WHERE Email = '" + email + "'";
                SQLiteCommand cmd1 = new SQLiteCommand(check, conn);
                int count = Convert.ToInt32(cmd1.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Такой email уже есть");
                    return;
                }

                // регистрация
                string hash = PasswordHasher.HashPassword(password);
                string insert = "INSERT INTO Users (Email, PasswordHash, FullName, Role, CreatedAt) VALUES ('" + email + "', '" + hash + "', '" + fullName + "', 'Storekeeper', datetime('now'))";
                SQLiteCommand cmd2 = new SQLiteCommand(insert, conn);
                cmd2.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Регистрация успешна!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при регистрации");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}