using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using System.Data.SQLite;

namespace Проект
{
    public partial class Avtorizacia : Form
    {
        private SQLiteConnection connection;
        Menu_organ f2;
        Menu_player f3;
        Menu_expert f4;
        public Avtorizacia()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyValue == (char)Keys.Enter) enter_Click(enter, null); }; //работа клавиши "Enter"
        }

        private void enter_Click(object sender, EventArgs e)
        {
            string loginU = login.Text;
            string passwordU = password.Text;
            this.connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            DataTable table = new DataTable();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM users WHERE `pin` = @ul AND `password`=@up", this.connection);
            
            command.Parameters.AddWithValue("@ul", loginU);
            command.Parameters.AddWithValue("@up", passwordU);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            SQLiteDataReader reader = command.ExecuteReader(); 
            while (reader.Read()) //сохранение данных для будущих действий
            {
                User_info.user_id = $"{reader.GetInt32(0)}";
                User_info.user_ima = $"{reader["fio"]}"; //сохранение ФИО для будущих действий
                User_info.user_role= $"{reader["ID_role"]}";
                User_info.user_id_comp = $"{reader["championship"]}";
            }
            if (table.Rows.Count > 0)
            {
                if (User_info.user_role == "6")
                {
                    MessageBox.Show("Вы успешно зашли в свой аккаунт!");
                    f2 = new Menu_organ();
                    f2.Show();
                    this.Hide();
                }
                if (User_info.user_id_comp != null && User_info.user_id_comp != "")
                {
                    
                    if (User_info.user_role == "1")
                    {
                        MessageBox.Show("Вы успешно зашли в свой аккаунт!");
                        f3 = new Menu_player();
                        f3.Show();
                        this.Hide();
                    }
                    else 
                    {
                        MessageBox.Show("Вы успешно зашли в свой аккаунт!");
                        f4 = new Menu_expert();
                        f4.Show();
                        this.Hide();
                    }
                }
                else
                {
                    if(User_info.user_role != "6")
                    {
                        MessageBox.Show("Вас ещё не зарегистрировали ни на один чемпионат :(");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Логин или пароль неверный!");
            }
            this.connection.Close();
        }
        private void Table_refresh()
        {
            this.connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT password, pin FROM users", this.connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0].DefaultView;
            this.connection.Close();
        }
        private void Avtorizacia_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            this.Table_refresh();
        }
    }
}
