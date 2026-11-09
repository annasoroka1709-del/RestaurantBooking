using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantBooking
{
    public partial class LoginForm : Form
    {
        private Label? lblTitle, lblWelcome, lblLogin, lblPassword;
        private Button? btnGuest, btnAdmin, btnLogin;
        private TextBox? txtLogin, txtPassword;
        private Panel? panelLogin;

        public LoginForm()
        {
            SetupUI();
            Database.InitializeDatabase();
            Database.SimulateRandomBookings();
        }

        private void SetupUI()
        {
            this.Text = "Ресторан - Добро пожаловать";
            this.ClientSize = new Size(800, 600);
            this.BackColor = Color.FromArgb(212, 230, 255);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int centerX = this.ClientSize.Width / 2;

            // Заголовки теперь без фона, с автоматическим размером
            lblTitle = new Label
            {
                Text = "RESTAURANT",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = Color.DodgerBlue,
                BackColor = Color.Transparent,
                AutoSize = true
            };

            lblWelcome = new Label
            {
                Text = "Добро пожаловать!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = true
            };

            btnGuest = new Button
            {
                Text = "Войти как гость",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(centerX - 150, 220),
                BackColor = Color.SeaGreen,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnGuest.Click += (s, e) => { new BookingForm().Show(); this.Hide(); };

            btnAdmin = new Button
            {
                Text = "Вход для администратора",
                Font = new Font("Segoe UI", 12),
                Size = new Size(300, 40),
                Location = new Point(centerX - 150, 290),
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnAdmin.Click += (s, e) => { if (panelLogin != null) panelLogin.Visible = true; };

            panelLogin = new Panel
            {
                Size = new Size(300, 120),
                Location = new Point(centerX - 150, 350),
                Visible = false
            };

            // Исправлено: текстбоксы сдвинуты вправо, чтобы надписи не наезжали
            lblLogin = new Label { Text = "Логин:", Font = new Font("Segoe UI", 12), Location = new Point(0, 10), AutoSize = true };
            txtLogin = new TextBox { Font = new Font("Segoe UI", 12), Location = new Point(80, 5), Size = new Size(200, 30) };

            lblPassword = new Label { Text = "Пароль:", Font = new Font("Segoe UI", 12), Location = new Point(0, 50), AutoSize = true };
            txtPassword = new TextBox { Font = new Font("Segoe UI", 12), Location = new Point(80, 45), Size = new Size(200, 30), PasswordChar = '*' };

            btnLogin = new Button
            {
                Text = "Войти в систему",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(300, 50),
                Location = new Point(centerX - 150, 480),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.Click += BtnLogin_Click;

            panelLogin.Controls.Add(lblLogin); panelLogin.Controls.Add(txtLogin);
            panelLogin.Controls.Add(lblPassword); panelLogin.Controls.Add(txtPassword);

            this.Controls.Add(lblTitle); this.Controls.Add(lblWelcome);
            this.Controls.Add(btnGuest); this.Controls.Add(btnAdmin);
            this.Controls.Add(panelLogin); this.Controls.Add(btnLogin);

            // Центрирование заголовков после их создания (так как AutoSize = true)
            lblTitle.Location = new Point(centerX - (lblTitle.Width / 2), 50);
            lblWelcome.Location = new Point(centerX - (lblWelcome.Width / 2), 130);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin?.Text) || string.IsNullOrWhiteSpace(txtPassword?.Text))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Database.ValidateUser(txtLogin.Text, txtPassword.Text))
            {
                new AdminForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}