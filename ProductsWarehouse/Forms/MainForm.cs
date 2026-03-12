/*using System;
using System.Windows.Forms;

namespace ProductsWarehouse.Forms
{
    public partial class MainForm : Form
    {
        private int currentUserId;
        private string currentUserFullName;
        private string currentUserRole;

        public MainForm(int userId, string fullName, string role)
        {
            InitializeComponent();
            currentUserId = userId;
            currentUserFullName = fullName;
            currentUserRole = role;

            this.Text = $"Складской учет - {fullName} ({GetRoleName(role)})";
            UpdateStatusBar();
            SetupMenuByRole();
        }

        private string GetRoleName(string role)
        {
            return role == "Admin" ? "Администратор" : "Кладовщик";
        }

        private void UpdateStatusBar()
        {
            lblUserInfo.Text = $"Пользователь: {currentUserFullName} ({GetRoleName(currentUserRole)})";
            lblDate.Text = $"Дата: {DateTime.Now:dd.MM.yyyy}";
            lblTime.Text = $"Время: {DateTime.Now:HH:mm:ss}";

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, e) =>
            {
                lblTime.Text = $"Время: {DateTime.Now:HH:mm:ss}";
            };
            timer.Start();
        }

        private void SetupMenuByRole()
        {
            if (currentUserRole == "Admin")
            {
                // Меню для администратора
                var справочники = new ToolStripMenuItem("Справочники");
                справочники.DropDownItems.Add("Товары", null, OpenProductsForm);
                справочники.DropDownItems.Add(new ToolStripSeparator());
                справочники.DropDownItems.Add("Остатки (просмотр)", null, OpenStockForm);

                menuStrip.Items.Add(справочники);
            }
            else
            {
                // Меню для кладовщика
                var склад = new ToolStripMenuItem("Склад");
                склад.DropDownItems.Add("Остатки", null, OpenStockForm);

                var документы = new ToolStripMenuItem("Документы");
                документы.DropDownItems.Add("Отгрузки", null, OpenShipmentsForm);
                документы.DropDownItems.Add(new ToolStripSeparator());
                документы.DropDownItems.Add("Новая отгрузка", null, OpenNewShipmentForm);

                menuStrip.Items.Add(склад);
                menuStrip.Items.Add(документы);
            }

            // Справка для всех
            var справка = new ToolStripMenuItem("Справка");
            справка.DropDownItems.Add("О программе", null, ShowAboutInfo);
            menuStrip.Items.Add(справка);
        }

        private void OpenProductsForm(object sender, EventArgs e)
        {
            // Будет создано позже
            MessageBox.Show("Форма товаров будет доступна в следующей версии",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenStockForm(object sender, EventArgs e)
        {
            MessageBox.Show("Форма остатков будет доступна в следующей версии",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenShipmentsForm(object sender, EventArgs e)
        {
            MessageBox.Show("Журнал отгрузок будет доступен в следующей версии",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenNewShipmentForm(object sender, EventArgs e)
        {
            MessageBox.Show("Создание отгрузки будет доступно в следующей версии",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAboutInfo(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Птички-тупички: Складской учет\n\n" +
                "Версия: 1.0\n" +
                $"Пользователь: {currentUserFullName}\n" +
                $"Роль: {GetRoleName(currentUserRole)}\n\n" +
                "© 2026 Все права защищены",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(
                    "Вы действительно хотите выйти?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}*/
using System;
using System.Windows.Forms;

namespace ProductsWarehouse.Forms
{
    public partial class MainForm : Form
    {
        private int currentUserId;
        private string currentUserFullName;
        private string currentUserRole;

        public MainForm(int userId, string fullName, string role)
        {
            InitializeComponent();
            currentUserId = userId;
            currentUserFullName = fullName;
            currentUserRole = role;

            this.Text = $"Складской учет - {fullName} ({GetRoleName(role)})";
            UpdateStatusBar();
            SetupMenuByRole();
        }

        private string GetRoleName(string role)
        {
            if (role == "Admin")
                return "Администратор";
            else
                return "Кладовщик";
        }

        private void UpdateStatusBar()
        {
            lblUserInfo.Text = $"Пользователь: {currentUserFullName} ({GetRoleName(currentUserRole)})";
            lblDate.Text = $"Дата: {DateTime.Now:dd.MM.yyyy}";
            lblTime.Text = $"Время: {DateTime.Now:HH:mm:ss}";

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = $"Время: {DateTime.Now:HH:mm:ss}";
        }

        private void SetupMenuByRole()
        {
            if (currentUserRole == "Admin")
            {
                ToolStripMenuItem справочники = new ToolStripMenuItem("Справочники");
                справочники.DropDownItems.Add("Товары", null, OpenProductsForm);
                справочники.DropDownItems.Add(new ToolStripSeparator());
                справочники.DropDownItems.Add("Остатки (просмотр)", null, OpenStockForm);
                menuStrip.Items.Add(справочники);
            }
            else
            {
                ToolStripMenuItem склад = new ToolStripMenuItem("Склад");
                склад.DropDownItems.Add("Остатки", null, OpenStockForm);

                ToolStripMenuItem документы = new ToolStripMenuItem("Документы");
                документы.DropDownItems.Add("Отгрузки", null, OpenShipmentsForm);
                документы.DropDownItems.Add(new ToolStripSeparator());
                документы.DropDownItems.Add("Новая отгрузка", null, OpenNewShipmentForm);

                menuStrip.Items.Add(склад);
                menuStrip.Items.Add(документы);
            }

            ToolStripMenuItem справка = new ToolStripMenuItem("Справка");
            справка.DropDownItems.Add("О программе", null, ShowAboutInfo);
            menuStrip.Items.Add(справка);
        }

        private void OpenProductsForm(object sender, EventArgs e)
        {
            MessageBox.Show("Форма товаров будет доступна в следующей версии", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenStockForm(object sender, EventArgs e)
        {
            MessageBox.Show("Форма остатков будет доступна в следующей версии", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenShipmentsForm(object sender, EventArgs e)
        {
            MessageBox.Show("Журнал отгрузок будет доступен в следующей версии", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenNewShipmentForm(object sender, EventArgs e)
        {
            MessageBox.Show("Создание отгрузки будет доступно в следующей версии", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAboutInfo(object sender, EventArgs e)
        {
            string info = "Птички-тупички: Складской учет\n\n";
            info += "Версия: 1.0\n";
            info += $"Пользователь: {currentUserFullName}\n";
            info += $"Роль: {GetRoleName(currentUserRole)}\n\n";
            info += "© 2026 Все права защищены";

            MessageBox.Show(info, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}