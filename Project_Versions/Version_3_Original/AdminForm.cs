using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantBooking
{
    public partial class AdminForm : Form
    {
        private DataGridView? dgvBookings;
        private TextBox? txtSearch;
        private Button? btnRefresh;
        private Button? btnDelete;
        private Button? btnToBooking;

        public AdminForm()
        {
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.Text = "Мои бронирования";
            this.ClientSize = new Size(1000, 600);
            this.BackColor = Color.FromArgb(212, 230, 255);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Список всех бронирований",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            Label lblSearch = new Label
            {
                Text = "Поиск:",
                Font = new Font("Segoe UI", 12),
                Location = new Point(20, 75),
                AutoSize = true
            };

            // Текстовое поле поиска
            txtSearch = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(90, 70),
                Size = new Size(180, 30)
            };
            txtSearch.TextChanged += (s, e) => FilterData(txtSearch.Text);

            // ШИРОКИЕ КНОПКИ С ВЫРАВНИВАНИЕМ ТЕКСТА ПО ЦЕНТРУ (ширина 220px)
            btnRefresh = new Button
            {
                Text = "Обновить",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(220, 35),
                Location = new Point(290, 68),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnRefresh.Click += (s, e) => LoadData();

            btnDelete = new Button
            {
                Text = "Удалить",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(220, 35),
                Location = new Point(520, 68),
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnDelete.Click += BtnDelete_Click;

            btnToBooking = new Button
            {
                Text = "К бронированию",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(220, 35),
                Location = new Point(750, 68),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnToBooking.Click += (s, e) => { new BookingForm().Show(); this.Hide(); };

            // Таблица опущена вниз
            dgvBookings = new DataGridView
            {
                Location = new Point(20, 150),
                Size = new Size(940, 400),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnToBooking);
            this.Controls.Add(dgvBookings);

            // Выводим на передний план
            lblTitle.BringToFront();
            lblSearch.BringToFront();
            txtSearch.BringToFront();
            btnRefresh.BringToFront();
            btnDelete.BringToFront();
            btnToBooking.BringToFront();
        }

        private void LoadData()
        {
            if (dgvBookings == null) return;

            dgvBookings.DataSource = Database.GetBookings();
            if (dgvBookings.Columns.Contains("Id")) dgvBookings.Columns["Id"].Visible = false;

            if (dgvBookings.Columns.Contains("ClientName")) dgvBookings.Columns["ClientName"].HeaderText = "Клиент";
            if (dgvBookings.Columns.Contains("Email")) dgvBookings.Columns["Email"].HeaderText = "Почта";
            if (dgvBookings.Columns.Contains("Phone")) dgvBookings.Columns["Phone"].HeaderText = "Телефон";
            if (dgvBookings.Columns.Contains("Guests")) dgvBookings.Columns["Guests"].HeaderText = "Гостей";
            if (dgvBookings.Columns.Contains("TableNumber")) dgvBookings.Columns["TableNumber"].HeaderText = "Столик";
            if (dgvBookings.Columns.Contains("Time")) dgvBookings.Columns["Time"].HeaderText = "Время";
            if (dgvBookings.Columns.Contains("Date")) dgvBookings.Columns["Date"].HeaderText = "Дата";
        }

        private void FilterData(string searchText)
        {
            if (dgvBookings == null) return;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadData();
                return;
            }

            DataTable dt = Database.GetBookings();
            DataView dv = dt.DefaultView;
            string safeSearch = searchText.Replace("'", "''");
            dv.RowFilter = $"ClientName LIKE '%{safeSearch}%' OR Email LIKE '%{safeSearch}%' OR Phone LIKE '%{safeSearch}%'";
            dgvBookings.DataSource = dv;
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvBookings == null || dgvBookings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Вы уверены, что хотите удалить это бронирование?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvBookings.SelectedRows[0].Cells["Id"].Value);
                Database.DeleteBooking(id);
                LoadData();
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}