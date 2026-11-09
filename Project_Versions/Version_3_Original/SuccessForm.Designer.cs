namespace RestaurantBooking
{
    partial class SuccessForm
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
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(122, 32);
            label1.Name = "label1";
            label1.Size = new Size(573, 46);
            label1.TabIndex = 0;
            label1.Text = "Бронирование успешно создано!";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button1.Location = new Point(240, 369);
            button1.Name = "button1";
            button1.Size = new Size(327, 55);
            button1.TabIndex = 1;
            button1.Text = "Новое бронирование";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button2.Location = new Point(240, 459);
            button2.Name = "button2";
            button2.Size = new Size(327, 55);
            button2.TabIndex = 2;
            button2.Text = "Вернуться к авторизации";
            button2.UseVisualStyleBackColor = true;
            // 
            // SuccessForm
            // 
            BackColor = Color.PowderBlue;
            ClientSize = new Size(784, 561);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "SuccessForm";
            Text = "Успех";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Button button1;
        private Button button2;
    }
}