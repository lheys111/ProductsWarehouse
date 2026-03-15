using System;
using System.Windows.Forms;

namespace ProductsWarehouse.Forms
{
    public partial class NewShipmentForm : Form
    {
        private int currentUserId;

        public NewShipmentForm(int userId)
        {
            InitializeComponent(); 
            currentUserId = userId;
            this.Text = $"Новая отгрузка - {DateTime.Now:dd.MM.yyyy}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}