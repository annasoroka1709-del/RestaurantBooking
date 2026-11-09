using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantBooking
{
    public class Booking
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Guests { get; set; } = "";
        public string Table { get; set; } = "";
        public string Time { get; set; } = "";
        public string Date { get; set; } = "";
    }

    public static class BookingStore
    {
        public static List<Booking> Items { get; } = new();
        public static bool IsBusy(string table, string date, string time) =>
            Items.Any(x => x.Table == table && x.Date == date && x.Time == time);
    }

    internal static class AppStyle
    {
        public static readonly bool Styled = false;
        public static readonly Color Background = Styled ? Color.FromArgb(239, 244, 250) : SystemColors.Control;
        public static readonly Color Primary = Styled ? Color.FromArgb(40, 91, 145) : SystemColors.Control;
        public static readonly Color Accent = Styled ? Color.FromArgb(46, 125, 95) : SystemColors.Control;
        public static readonly Color Text = Styled ? Color.FromArgb(35, 48, 62) : SystemColors.ControlText;

        public static void Prepare(Form form, string title, Size size)
        {
            form.Text = title;
            form.ClientSize = size;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.BackColor = Background;
            form.Font = Styled ? new Font("Segoe UI", 10) : SystemFonts.DefaultFont;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        public static Label Label(string text, int x, int y, int width = 250, int height = 28, bool heading = false)
        {
            return new Label {
                Text = text, Location = new Point(x,y), Size = new Size(width,height),
                Font = Styled && heading ? new Font("Segoe UI", 18, FontStyle.Bold) :
                       Styled ? new Font("Segoe UI", 10) : SystemFonts.DefaultFont,
                ForeColor = Text, TextAlign = heading ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleLeft
            };
        }

        public static Button Button(string text, int x, int y, int width = 220, int height = 38, bool primary = false)
        {
            var b = new Button { Text=text, Location=new Point(x,y), Size=new Size(width,height) };
            if (Styled) {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.BackColor = primary ? Accent : Primary;
                b.ForeColor = Color.White;
                b.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }
            return b;
        }

        public static void AddField(Form form, string caption, Control control, int x, int y, int width=300)
        {
            form.Controls.Add(Label(caption, x, y, width, 24));
            control.Location = new Point(x, y+25);
            control.Size = new Size(width, 30);
            if (Styled) control.Font = new Font("Segoe UI", 10);
            form.Controls.Add(control);
        }
    }

    public class LoginForm : Form
    {
        private TextBox login = new();
        private TextBox password = new();
        private Panel adminPanel = new();

        public LoginForm()
        {
            AppStyle.Prepare(this, "Система бронирования ресторана", new Size(560, 440));
            Controls.Add(AppStyle.Label("Бронирование столиков", 90, 45, 400, 45, true));
            Controls.Add(AppStyle.Label("Выберите способ входа", 155, 105, 260, 28));
            var guest = AppStyle.Button("Войти как гость", 150, 150, 260, 44, true);
            guest.Click += (_,_) => { new BookingForm().Show(); Hide(); };
            Controls.Add(guest);
            var admin = AppStyle.Button("Вход администратора", 150, 210, 260, 40);
            admin.Click += (_,_) => adminPanel.Visible = true;
            Controls.Add(admin);

            adminPanel.Location = new Point(125,265);
            adminPanel.Size = new Size(310,130);
            adminPanel.Visible = false;
            var l1 = AppStyle.Label("Логин", 0, 0, 80, 25);
            login.Location = new Point(85,0); login.Size = new Size(210,25);
            var l2 = AppStyle.Label("Пароль", 0, 35, 80, 25);
            password.Location = new Point(85,35); password.Size = new Size(210,25);
            password.PasswordChar = '*';
            var enter = AppStyle.Button("Войти", 85, 75, 210, 35, true);
            enter.Click += (_,_) => {
                if (login.Text == "admin" && password.Text == "1234") {
                    new AdminForm().Show(); Hide();
                } else MessageBox.Show("Неверный логин или пароль. Для демонстрации: admin / 1234");
            };
            adminPanel.Controls.AddRange(new Control[]{l1,login,l2,password,enter});
            Controls.Add(adminPanel);
        }
    }

    public class BookingForm : Form
    {
        private TextBox name = new(), email = new(), phone = new();
        private ComboBox guests = new(), table = new(), time = new();
        private DateTimePicker date = new();

        public BookingForm()
        {
            AppStyle.Prepare(this, "Бронирование столика", new Size(850, 620));
            Controls.Add(AppStyle.Label("Бронирование столика", 35, 20, 700, 42, true));
            Controls.Add(AppStyle.Label("Данные клиента", 35, 85, 330, 30, true));
            Controls.Add(AppStyle.Label("Данные бронирования", 450, 85, 350, 30, true));

            AppStyle.AddField(this, "Имя клиента", name, 35, 135);
            AppStyle.AddField(this, "Электронная почта", email, 35, 215);
            AppStyle.AddField(this, "Номер телефона", phone, 35, 295);

            guests.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i=1;i<=10;i++) guests.Items.Add($"{i} человек");
            guests.SelectedIndex = 0;
            AppStyle.AddField(this, "Количество гостей", guests, 450, 135);

            table.DropDownStyle = ComboBoxStyle.DropDownList;
            AppStyle.AddField(this, "Номер столика", table, 450, 215);
            time.DropDownStyle = ComboBoxStyle.DropDownList;
            time.Items.AddRange(new object[]{"12:00","13:00","14:00","15:00","16:00","17:00","18:00","19:00","20:00","21:00","22:00","23:00"});
            time.SelectedIndex = 0;
            AppStyle.AddField(this, "Время бронирования", time, 450, 295);
            date.MinDate = DateTime.Today;
            AppStyle.AddField(this, "Дата бронирования", date, 450, 375);
            time.SelectedIndexChanged += (_,_) => UpdateTables();
            date.ValueChanged += (_,_) => UpdateTables();
            var save = AppStyle.Button("Забронировать", 285, 495, 280, 48, true);
            save.Click += SaveBooking;
            Controls.Add(save);
            UpdateTables();
        }

        private void UpdateTables()
        {
            string selected = table.SelectedItem?.ToString() ?? "";
            table.Items.Clear();
            string d = date.Value.ToString("yyyy-MM-dd");
            for (int i=1;i<=10;i++) {
                string t = $"Столик №{i}";
                table.Items.Add(t + (BookingStore.IsBusy(t,d,time.Text) ? " (Занят)" : " (Свободен)"));
            }
            int index = -1;
            for (int i=0;i<table.Items.Count;i++)
                if (table.Items[i]!.ToString() == selected) index=i;
            table.SelectedIndex = index >= 0 ? index : 0;
        }

        private void SaveBooking(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(phone.Text) || guests.SelectedIndex < 0 ||
                table.SelectedIndex < 0 || time.SelectedIndex < 0) {
                MessageBox.Show("Пожалуйста, заполните все поля."); return;
            }
            if (!email.Text.Contains("@")) { MessageBox.Show("Проверьте адрес электронной почты."); return; }
            string tableName = table.Text.Split('(')[0].Trim();
            string dateText = date.Value.ToString("yyyy-MM-dd");
            if (BookingStore.IsBusy(tableName,dateText,time.Text)) {
                MessageBox.Show("Этот столик уже занят. Выберите другой."); UpdateTables(); return;
            }
            var item = new Booking { Name=name.Text, Email=email.Text, Phone=phone.Text,
                Guests=guests.Text, Table=tableName, Time=time.Text, Date=dateText };
            BookingStore.Items.Add(item);
            new SuccessForm(item).Show();
            Hide();
        }
    }

    public class SuccessForm : Form
    {
        public SuccessForm(Booking item)
        {
            AppStyle.Prepare(this, "Бронирование создано", new Size(600, 440));
            Controls.Add(AppStyle.Label("Бронирование успешно создано!", 45, 45, 510, 45, true));
            var details = AppStyle.Label(
                $"Клиент: {item.Name}\nКоличество гостей: {item.Guests}\nСтолик: {item.Table}\nДата: {item.Date}\nВремя: {item.Time}",
                90, 125, 430, 150);
            details.Font = new Font("Segoe UI", AppStyle.Styled ? 12 : 10);
            Controls.Add(details);
            var again = AppStyle.Button("Новое бронирование", 145, 300, 300, 42, true);
            again.Click += (_,_) => { new BookingForm().Show(); Close(); };
            Controls.Add(again);
            var home = AppStyle.Button("На главную", 145, 350, 300, 36);
            home.Click += (_,_) => { new LoginForm().Show(); Close(); };
            Controls.Add(home);
        }
    }

    public class AdminForm : Form
    {
        private DataGridView grid = new();
        private TextBox search = new();

        public AdminForm()
        {
            AppStyle.Prepare(this, "Панель администратора", new Size(1000, 600));
            Controls.Add(AppStyle.Label("Список бронирований", 20, 20, 500, 40, true));
            search.Location = new Point(20,80); search.Size = new Size(250,30);
            search.PlaceholderText = "Поиск по клиенту или почте";
            search.TextChanged += (_,_) => LoadRows();
            Controls.Add(search);
            var refresh = AppStyle.Button("Обновить", 290, 77, 150, 36);
            refresh.Click += (_,_) => LoadRows();
            Controls.Add(refresh);
            var delete = AppStyle.Button("Удалить выбранное", 455, 77, 190, 36);
            delete.Click += (_,_) => DeleteSelected();
            Controls.Add(delete);
            var booking = AppStyle.Button("К бронированию", 660, 77, 180, 36, true);
            booking.Click += (_,_) => { new BookingForm().Show(); Hide(); };
            Controls.Add(booking);
            grid.Location = new Point(20,135); grid.Size = new Size(950,420);
            grid.ReadOnly = true; grid.AllowUserToAddRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (AppStyle.Styled) { grid.BackgroundColor=Color.White; grid.BorderStyle=BorderStyle.None; }
            Controls.Add(grid);
            LoadRows();
        }

        private void LoadRows()
        {
            var query = BookingStore.Items.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(search.Text))
                query = query.Where(x => x.Name.Contains(search.Text,StringComparison.OrdinalIgnoreCase) ||
                                         x.Email.Contains(search.Text,StringComparison.OrdinalIgnoreCase));
            grid.DataSource = query.Select(x => new {
                Клиент=x.Name, Почта=x.Email, Телефон=x.Phone, Гостей=x.Guests,
                Столик=x.Table, Дата=x.Date, Время=x.Time
            }).ToList();
        }

        private void DeleteSelected()
        {
            if (grid.CurrentRow == null) { MessageBox.Show("Выберите бронирование."); return; }
            string client = Convert.ToString(grid.CurrentRow.Cells["Клиент"].Value) ?? "";
            var item = BookingStore.Items.FirstOrDefault(x => x.Name == client);
            if (item != null) BookingStore.Items.Remove(item);
            LoadRows();
        }
    }
}
