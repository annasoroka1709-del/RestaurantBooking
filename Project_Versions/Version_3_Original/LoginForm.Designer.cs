namespace RestaurantBooking
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.PowderBlue;
            label1.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.DodgerBlue;
            label1.Location = new Point(168, 39);
            label1.Name = "label1";
            label1.Size = new Size(418, 81);
            label1.TabIndex = 0;
            label1.Text = "RESTAURANT";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label2.Location = new Point(168, 120);
            label2.Name = "label2";
            label2.Size = new Size(411, 54);
            label2.TabIndex = 1;
            label2.Text = "Добро пожаловать!";
            // 
            // button1
            // 
            button1.BackColor = Color.Khaki;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.Location = new Point(235, 296);
            button1.Name = "button1";
            button1.Size = new Size(300, 49);
            button1.TabIndex = 2;
            button1.Text = "Вход для администратора";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.ForestGreen;
            button2.Cursor = Cursors.IBeam;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.Location = new Point(235, 241);
            button2.Name = "button2";
            button2.Size = new Size(300, 49);
            button2.TabIndex = 3;
            button2.Text = "Войти как гость";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.DodgerBlue;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button3.ForeColor = SystemColors.Control;
            button3.Location = new Point(235, 471);
            button3.Name = "button3";
            button3.Size = new Size(300, 49);
            button3.TabIndex = 4;
            button3.Text = "Войти в систему";
            button3.UseVisualStyleBackColor = false;
            // 
            // LoginForm
            // 
            BackColor = Color.PowderBlue;
            ClientSize = new Size(784, 561);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LoginForm";
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}