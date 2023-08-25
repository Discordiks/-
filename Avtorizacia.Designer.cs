
namespace Проект
{
    partial class Avtorizacia
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.enter = new System.Windows.Forms.Button();
            this.login_text = new System.Windows.Forms.Label();
            this.password_text = new System.Windows.Forms.Label();
            this.Label = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // enter
            // 
            this.enter.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.enter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.enter.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.enter.Location = new System.Drawing.Point(309, 321);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(172, 47);
            this.enter.TabIndex = 0;
            this.enter.Text = "Логин";
            this.enter.UseVisualStyleBackColor = false;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // login_text
            // 
            this.login_text.AutoSize = true;
            this.login_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login_text.Location = new System.Drawing.Point(188, 153);
            this.login_text.Name = "login_text";
            this.login_text.Size = new System.Drawing.Size(83, 29);
            this.login_text.TabIndex = 1;
            this.login_text.Text = "Логин";
            // 
            // password_text
            // 
            this.password_text.AutoSize = true;
            this.password_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password_text.Location = new System.Drawing.Point(188, 215);
            this.password_text.Name = "password_text";
            this.password_text.Size = new System.Drawing.Size(99, 29);
            this.password_text.TabIndex = 2;
            this.password_text.Text = "Пароль";
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label.Location = new System.Drawing.Point(304, 57);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(177, 29);
            this.Label.TabIndex = 3;
            this.Label.Text = "Авторизация";
            // 
            // login
            // 
            this.login.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.login.Location = new System.Drawing.Point(309, 137);
            this.login.Multiline = true;
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(261, 45);
            this.login.TabIndex = 4;
            // 
            // password
            // 
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.password.Location = new System.Drawing.Point(309, 201);
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(261, 43);
            this.password.TabIndex = 5;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox.Location = new System.Drawing.Point(286, 270);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(232, 33);
            this.checkBox.TabIndex = 7;
            this.checkBox.Text = "Запомнить меня";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(524, 270);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(254, 150);
            this.dataGridView1.TabIndex = 8;
            // 
            // Avtorizacia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.password);
            this.Controls.Add(this.login);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.password_text);
            this.Controls.Add(this.login_text);
            this.Controls.Add(this.enter);
            this.Name = "Avtorizacia";
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Avtorizacia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Label login_text;
        private System.Windows.Forms.Label password_text;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

