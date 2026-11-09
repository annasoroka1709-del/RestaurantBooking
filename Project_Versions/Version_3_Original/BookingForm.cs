using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantBooking
{
    public partial class BookingForm : Form
    {
        private TextBox? txtName, txtEmail, txtPhone;
        private ComboBox? cmbGuests, cmbTable, cmbTime;
        private DateTimePicker? dtpDate;
        private Button? btnBook;

        public BookingForm()
        {
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Бронирование столика";
            this.ClientSize = new Size(900, 700);
            this.BackColor = Color.FromArgb(212, 230, 255);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            Label lblTitle = new Label { Text = "Бронирование столика", Font = new Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(250, 20), AutoSize = true };
            Label lblClient = new Label { Text = "Данные клиента", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(80, 100), AutoSize = true };
            Label lblBooking = new Label { Text = "Данные бронирования", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(500, 100), AutoSize = true };

            // --- КЛИЕНТ ---
            Label lblName = new Label { Text = "Имя клиента", Font = new Font("Segoe UI", 12), Location = new Point(80, 160), AutoSize = true };
            txtName = new TextBox { Font = new Font("Segoe UI", 12), Location = new Point(80, 190), Size = new Size(300, 30) };
            // Валидация: только буквы и пробелы
            txtName.KeyPress += (s, e) => { if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ') e.Handled = true; };

            Label lblEmail = new Label { Text = "Электронная почта", Font = new Font("Segoe UI", 12), Location = new Point(80, 240), AutoSize = true };
            txtEmail = new TextBox { Font = new Font("Segoe UI", 12), Location = new Point(80, 270), Size = new Size(300, 30) };

            Label lblPhone = new Label { Text = "Номер телефона", Font = new Font("Segoe UI", 12), Location = new Point(80, 320), AutoSize = true };
            txtPhone = new TextBox { Font = new Font("Segoe UI", 12), Location = new Point(80, 350), Size = new Size(300, 30) };
            // Валидация: только цифры
            txtPhone.KeyPress += (s, e) => { if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true; };

            // --- БРОНИРОВАНИЕ ---
            Label lblGuests = new Label { Text = "Количество гостей", Font = new Font("Segoe UI", 12), Location = new Point(500, 160), AutoSize = true };
            cmbGuests = new ComboBox { Font = new Font("Segoe UI", 12), Location = new Point(500, 190), Size = new Size(300, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            for (int i = 1; i <= 10; i++) cmbGuests.Items.Add($"{i} человек");

            Label lblTable = new Label { Text = "Номер столика", Font = new Font("Segoe UI", 12), Location = new Point(500, 240), AutoSize = true };
            cmbTable = new ComboBox { Font = new Font("Segoe UI", 12), Location = new Point(500, 270), Size = new Size(300, 30), DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblTime = new Label { Text = "Время бронирования", Font = new Font("Segoe UI", 12), Location = new Point(500, 320), AutoSize = true };
            cmbTime = new ComboBox { Font = new Font("Segoe UI", 12), Location = new Point(500, 350), Size = new Size(300, 30), DropDownStyle = ComboBoxStyle.DropDownList };

            // Время с 12:00 до 23:00
            cmbTime.Items.AddRange(new string[] { "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00" });

            Label lblDate = new Label { Text = "Дата бронирования", Font = new Font("Segoe UI", 12), Location = new Point(500, 400), AutoSize = true };
            // Запрет на прошлые даты
            dtpDate = new DateTimePicker { Font = new Font("Segoe UI", 12), Location = new Point(500, 430), Size = new Size(300, 30), Format = DateTimePickerFormat.Short, MinDate = DateTime.Today };

            btnBook = new Button { Text = "✔ Забронировать", Font = new Font("Segoe UI", 16, FontStyle.Bold), Size = new Size(300, 60), Location = new Point(300, 550), BackColor = Color.SeaGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnBook.Click += BtnBook_Click;

            // Обновляем занятость столов при смене времени или даты
            cmbTime.SelectedIndexChanged += (s, e) => UpdateTableComboBox();
            dtpDate.ValueChanged += (s, e) => UpdateTableComboBox();

            this.Controls.AddRange(new Control[] { lblTitle, lblClient, lblBooking, lblName, txtName, lblEmail, txtEmail, lblPhone, txtPhone, lblGuests, cmbGuests, lblTable, cmbTable, lblTime, cmbTime, lblDate, dtpDate, btnBook });

            cmbTime.SelectedIndex = 0;
            UpdateTableComboBox();
        }

        private void UpdateTableComboBox()
        {
            if (cmbTable == null || cmbTime == null || dtpDate == null) return;

            int selectedIndex = cmbTable.SelectedIndex;
            cmbTable.Items.Clear();
            string dateStr = dtpDate.Value.ToString("yyyy-MM-dd");
            string timeStr = cmbTime.Text;

            for (int i = 1; i <= 10; i++)
            {
                string tableName = $"Столик №{i}";
                string display = tableName;

                if (!string.IsNullOrEmpty(timeStr))
                {
                    bool isBusy = Database.IsTableBusy(tableName, dateStr, timeStr);
                    display += isBusy ? " (Занят)" : " (Свободен)";
                }
                cmbTable.Items.Add(display);
            }

            if (selectedIndex >= 0 && selectedIndex < cmbTable.Items.Count)
                cmbTable.SelectedIndex = selectedIndex;
            else if (cmbTable.Items.Count > 0)
                cmbTable.SelectedIndex = 0;
        }

        private void BtnBook_Click(object? sender, EventArgs e)
        {
            if (txtName == null || txtEmail == null || txtPhone == null || cmbGuests == null || cmbTable == null || cmbTime == null) return;

            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) || cmbGuests.SelectedIndex == -1 || cmbTable.SelectedIndex == -1 || cmbTime.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка почты на @
            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Электронная почта должна содержать символ @", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDate != null && dtpDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Нельзя забронировать столик на прошедшую дату!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string dateStr = dtpDate != null ? dtpDate.Value.ToString("yyyy-MM-dd") : string.Empty;
            string tableStr = cmbTable.Text.Split('(')[0].Trim();
            string timeStr = cmbTime.Text;

            // Финальная проверка занятости перед сохранением
            if (Database.IsTableBusy(tableStr, dateStr, timeStr))
            {
                MessageBox.Show("Этот столик уже занят на выбранное время и дату! Выберите другой.", "Столик занят", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTableComboBox();
                return;
            }

            Database.AddBooking(txtName.Text, txtEmail.Text, txtPhone.Text, cmbGuests.Text, tableStr, timeStr, dateStr);

            new SuccessForm(txtName.Text, cmbGuests.Text, tableStr, dateStr, timeStr).Show();
            this.Hide();
        }
    }
}