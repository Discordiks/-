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
    public partial class MO_settings_champ : Form
    {
        private SQLiteConnection connection;
        public MO_settings_champ()
        {
            InitializeComponent();

            
            if (DateTime.Now.TimeOfDay >= new TimeSpan(0, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(6, 0, 0))
            {
                hello.Text="Доброй ночи, "+User_info.user_ima;
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(6, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(12, 0, 0))
            {
                hello.Text = "Доброе утро, "+User_info.user_ima;
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(12, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(18, 0, 0))
            {
                hello.Text = "Добрый день, ";
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(18, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(24, 0, 0))
            {
                hello.Text = "Добрый вечер, ";
            }
            else
            {
                hello.Text = "Доброй ночи, ";
            }
        }

        private void Table_refresh()
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT * FROM users", this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            SQLiteDataAdapter adapter_2 = new SQLiteDataAdapter("SELECT * FROM competitions", this.connection);
            DataSet data_2 = new DataSet();
            adapter_2.Fill(data_2);
            SQLiteDataAdapter adapter_3 = new SQLiteDataAdapter("SELECT * FROM skills_blocks", this.connection);
            DataSet data_3 = new DataSet();
            adapter_3.Fill(data_3);
            SQLiteDataAdapter adapter_4 = new SQLiteDataAdapter("SELECT * FROM skills", this.connection);
            DataSet data_4 = new DataSet();
            adapter_4.Fill(data_4);
            SQLiteDataAdapter adapter_5 = new SQLiteDataAdapter("SELECT * FROM competitions_skills", this.connection);
            DataSet data_5 = new DataSet();
            adapter_5.Fill(data_5);
            //dataGridView1.DataSource = data.Tables[0].DefaultView;
            //bindingSource1.DataSource = data.Tables[0].DefaultView;
            this.connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            DataTable table = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM skills", this.connection);
            adapter.Fill(table);
            skills.DataSource = table;
            //skills.DisplayMember = "sd";// столбец для отображения
            skills.ValueMember = "title";

            DataTable table_1 = new DataTable();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT  users.fio, competitions.title, skills_blocks.title, skills.title FROM users JOIN competitions ON users.championship = competitions.id JOIN skills_blocks ON skills_blocks.id = skills.skill_block_id JOIN skills ON skills.id = users.skill WHERE users.ID_role = 3", this.connection);
            adapter_1.Fill(table_1);
            gl_expert.DataSource = table_1;
            //skills.DisplayMember = "sd";// столбец для отображения
            gl_expert.ValueMember = "fio";

            Table_refresh();
        }


        private void add_Click(object sender, EventArgs e)
        {
            //добавление чемпионата и его скилла
            this.connection.Open(); //добавление чемпионата
            string label_1 = name_champ.Text;
            DateTime label_2 = Convert.ToDateTime(date_1.Text);
            DateTime label_3 = Convert.ToDateTime(date_2.Text);
            string label_4 = skills.Text;
            string label_5 = info.Text;
            string label_6 = city.Text;
            string sql_1 = "INSERT INTO competitions(title, date_start, date_end, description, city) VALUES (@label_1,@label_2, @label_3, @label_5, @label_6)";
            SQLiteCommand command_1 = new SQLiteCommand(sql_1, connection);
            SQLiteParameter param_1 = new SQLiteParameter("@label_1", label_1);
            SQLiteParameter param_2 = new SQLiteParameter("@label_2", label_2);
            SQLiteParameter param_3 = new SQLiteParameter("@label_3", label_3);
            SQLiteParameter param_5 = new SQLiteParameter("@label_5", label_5);
            SQLiteParameter param_6 = new SQLiteParameter("@label_6", label_6);
            command_1.Parameters.Add(param_1);
            command_1.Parameters.Add(param_2);
            command_1.Parameters.Add(param_3);
            command_1.Parameters.Add(param_5);
            command_1.Parameters.Add(param_6);
            using (command_1) //проверка на добавление записи чемпионата (competitions) в бд
            {
                int count_add_1 = command_1.ExecuteNonQuery();
                if (count_add_1 <= 0) 
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного чемпионата
            DataTable table = new DataTable();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM competitions WHERE `title` = @label_1 and `description` = @label_5 and `city` = @label_6", this.connection);
            command.Parameters.AddWithValue("@label_1", label_1);
            command.Parameters.AddWithValue("@label_5", label_5);
            command.Parameters.AddWithValue("@label_6", label_6);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User_info.title_champ = $"{reader["id"]}" ;
                break;
            }

            SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и записаь id только что добавленного скилла к чемпионату
            DataTable table_ = new DataTable();
            SQLiteCommand command_ = new SQLiteCommand("SELECT * FROM skills WHERE `title` = @label_4", this.connection);
            command_.Parameters.AddWithValue("@label_4", label_4);
            adapter_.SelectCommand = command_;
            adapter_.Fill(table_);
            SQLiteDataReader reader_ = command_.ExecuteReader();
            while (reader_.Read())
            {
                User_info.title_id = $"{reader_["id"]}";
                break;
            }
            string sql_2 = "INSERT INTO competitions_skills(competition_id, skill_id) VALUES (@label_7,@label_8)"; //добавление чемпионата и скилла в таблицу competitions_skills
            SQLiteCommand command_2 = new SQLiteCommand(sql_2, connection);
            string label_7 = User_info.title_champ;
            string label_8 = User_info.title_id;
            SQLiteParameter param_7 = new SQLiteParameter("@label_7", Convert.ToInt32(label_7)); 
            SQLiteParameter param_8 = new SQLiteParameter("@label_8", Convert.ToInt32(label_8));
            command_2.Parameters.Add(param_7); 
            command_2.Parameters.Add(param_8);
            using (command_2) //проверка на добавление записи скилла (skills) в бд
            {
                int count_add_2 = command_2.ExecuteNonQuery();
                if (count_add_2 <= 0)
                {
                    MessageBox.Show("Ошибка подключения");
                }
                else
                {
                    MessageBox.Show("Добавлен");
                }
            }
            this.connection.Close();
            Table_refresh();
        }

        
    }

}
