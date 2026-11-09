namespace RestaurantBooking
{
    partial class AdminForm
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
            dataGridView1 = new DataGridView();
            label2 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.Location = new Point(25, 9);
            label1.Name = "label1";
            label1.Size = new Size(421, 41);
            label1.TabIndex = 0;
            label1.Text = "Список всех бронирований";
            label1.Click += label1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Info;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(25, 127);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(932, 400);
            dataGridView1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 72);
            label2.Name = "label2";
            label2.Size = new Size(55, 20);
            label2.TabIndex = 2;
            label2.Text = "Поиск:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(86, 65);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(160, 27);
            textBox1.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = Color.DodgerBlue;
            button1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button1.ForeColor = SystemColors.Control;
            button1.Location = new Point(275, 63);
            button1.Name = "button1";
            button1.Size = new Size(161, 40);
            button1.TabIndex = 4;
            button1.Text = "Обновить";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(460, 65);
            button2.Name = "button2";
            button2.Size = new Size(161, 40);
            button2.TabIndex = 5;
            button2.Text = "Удалить";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.LightSlateGray;
            button3.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button3.ForeColor = SystemColors.Control;
            button3.Location = new Point(642, 65);
            button3.Name = "button3";
            button3.Size = new Size(161, 40);
            button3.TabIndex = 6;
            button3.Text = "К бронированию";
            button3.UseVisualStyleBackColor = false;
            // 
            // AdminForm
            // 
            BackColor = Color.PowderBlue;
            ClientSize = new Size(984, 561);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "AdminForm";
            Text = "Панель администратора";
            Load += AdminForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private DataGridView dataGridView1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}