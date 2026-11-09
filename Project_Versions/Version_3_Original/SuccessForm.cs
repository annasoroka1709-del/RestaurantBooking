using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantBooking
{
    public partial class SuccessForm : Form
    {
        public SuccessForm(string name, string guests, string table, string date, string time)
        {
            SetupUI(name, guests, table, date, time);
        }

        private void SetupUI(string name, string guests, string table, string date, string time)
        {
            this.Text = "Бронирование";
            this.ClientSize = new Size(800, 600);
            this.BackColor = Color.FromArgb(212, 230, 255);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Заголовок: делаем его на всю ширину окна и выравниваем текст ровно по центру
            Label lblSuccess = new Label
            {
                Text = "Бронирование успешно создано!",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(this.ClientSize.Width, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 80)
            };

            // Детали брони
            Label lblDetails = new Label
            {
                Text = $"Клиент: {name}\nКоличество гостей: {guests}\nСтолик: {table}\nДата: {date}\nВремя: {time}",
                Font = new Font("Segoe UI", 16),
                Location = new Point(250, 200),
                AutoSize = true
            };

            // Кнопки (ширина 350, строго по центру)
            Button btnNew = new Button
            {
                Text = "Новое бронирование",
                Font = new Font("Segoe UI", 14),
                Size = new Size(350, 50),
                Location = new Point((this.ClientSize.Width - 350) / 2, 400),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNew.Click += (s, e) => { new BookingForm().Show(); this.Close(); };

            Button btnBack = new Button
            {
                Text = "Вернуться к авторизации",
                Font = new Font("Segoe UI", 14),
                Size = new Size(350, 50),
                Location = new Point((this.ClientSize.Width - 350) / 2, 470),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBack.Click += (s, e) => { new LoginForm().Show(); this.Close(); };

            this.Controls.Add(lblSuccess);
            this.Controls.Add(lblDetails);
            this.Controls.Add(btnNew);
            this.Controls.Add(btnBack);
        }
    }
}