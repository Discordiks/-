using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Проект
{
    public partial class MO_settings_champ : Form
    {
        private SQLiteConnection connection;
        [Serializable]
        class Skill
        {
            public int id { get; set; }
            public string skill_name { get; set; }
        }
        [Serializable]
        class User
        {
            public int id { get; set; }
            public string user_name { get; set; }
            public int skill_id { get; set; }
        }
        List<User> users_ = new List<User>();
        List<Skill> skills_ = new List<Skill>();
        public MO_settings_champ()
        {
            InitializeComponent();
            if (DateTime.Now.TimeOfDay >= new TimeSpan(0, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(6, 0, 0))
            {
                hello.Text = "Доброй ночи, " + User_info.user_ima;
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(6, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(12, 0, 0))
            {
                hello.Text = "Доброе утро, " + User_info.user_ima;
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
            SQLiteDataAdapter adapter_6 = new SQLiteDataAdapter("SELECT DISTINCT competitions.id AS `Код`, competitions.title AS `Название чемпионата`, competitions.date_start AS `Начало чемпионата`, competitions.date_end AS `Конец чемпионата`, competitions.description AS `Ифн-я по чемпионату`, competitions.city AS `Место проведения чемпионата`, skills.title AS `Название скилла`, users.fio AS `ФИО гл. эксперта` FROM users JOIN roles ON roles.id = users.ID_role JOIN competitions ON users.championship = competitions.id JOIN skills ON skills.id = users.skill JOIN competitions_skills ON competitions_skills.competition_id = competitions.id WHERE users.ID_role = 3", this.connection);
            DataSet data_6 = new DataSet(); //надо именно экспертов, так что потом следует поставить "где роль = 3"
            adapter_6.Fill(data_6);
            chemp_view.DataSource = data_6.Tables[0].DefaultView; //вывод чемпионатов в таблицу
            //bindingSource1.DataSource = data.Tables[0].DefaultView;
            this.connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            this.connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM skills", this.connection);
            SQLiteDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                //string title = $"{rd["title"]}";
                //int a = rd.GetInt32(0);
                skills.Items.Add(rd["title"]);

                skills_.Add(new Skill()
                {
                    id = rd.GetInt32(0),
                    skill_name = rd["title"] as string
                });

            }
            this.connection.Close();
            this.connection.Open();
            SQLiteCommand cmdd = new SQLiteCommand("SELECT * FROM users WHERE users.ID_role = 3", this.connection);
            SQLiteDataReader rdd = cmdd.ExecuteReader();
            while (rdd.Read())
            {
                users_.Add(new User()
                {
                    id = rdd.GetInt32(0),
                    user_name = rdd["fio"] as string,
                    skill_id = rdd.GetInt32(7)
                });

            }
            this.connection.Close();
            Table_refresh();
        }
        private string[] GetUsersByID(int id)
        {
            return users_.Where(line => line.skill_id == id).Select(l => l.user_name).ToArray();
        }
        private void skills_SelectedIndexChanged(object sender, EventArgs e)
        {
            gl_expert.Items.Clear();
            int id = skills_[skills.SelectedIndex].id;
            foreach (string name in GetUsersByID(id))
            {
                this.gl_expert.Items.Add(name);
            }
        }
        private void add_Click(object sender, EventArgs e)
        {
            //добавление чемпионата, его скилла и его главного эксперта
            this.connection.Open(); //добавление чемпионата
            string label_1 = name_champ.Text;
            DateTime label_2 = Convert.ToDateTime(date_1.Text);
            DateTime label_3 = Convert.ToDateTime(date_2.Text);
            string label_4 = skills.Text;
            string label_5 = info.Text;
            string label_6 = city.Text;
            if (label_2 <= label_3)
            {
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
                    User_info.title_champ = $"{reader["id"]}";
                    break;
                }
                reader.Close();
                SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что добавленного скилла к чемпионату
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
                reader_.Close();
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
                }
                string sql_3 = "UPDATE users SET championship = @label_7 WHERE fio = @label_9"; //присвоение чемпионата главному эксперту в таблицу users
                SQLiteCommand command_3 = new SQLiteCommand(sql_3, connection);
                string label_9 = gl_expert.Text;
                SQLiteParameter param_9 = new SQLiteParameter("@label_9", label_9);
                command_3.Parameters.Add(param_7);
                command_3.Parameters.Add(param_9);
                using (command_3) //проверка на присвоение записи чемпионата (championship) в бд
                {
                    int count_add_3 = command_3.ExecuteNonQuery();
                    if (count_add_3 <= 0)
                    {
                        MessageBox.Show("Ошибка подключения");
                    }
                    else
                    {
                        MessageBox.Show("Добавлен");
                    }
                }
                this.connection.Close();
                //Table_refresh();
            }
            else
            {
                MessageBox.Show("Дата конца чемпионата не может быть раньше даты начала чемпионата");
            }
            Table_refresh();
        }

        private void chemp_Click(object sender, EventArgs e)
        {

        }

        private void change_Click(object sender, EventArgs e)
        {

        }
    }

}
