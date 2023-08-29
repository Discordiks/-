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
        int ID1 = 0;
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
            chemp.BackColor = Color.LimeGreen;
            sett_chemp.BackColor = Color.DodgerBlue;
            upr_expert.BackColor = Color.DodgerBlue;
            protocols.BackColor = Color.DodgerBlue;

            compet_view.Visible = true;

            expert_view.Visible = false;
            label10.Visible = false;
            ex_fio.Visible = false;
            ex_skill.Visible = false;
            ex_status.Visible = false;
            box_fio.Visible = false;
            box_skill.Visible = false;
            box_status.Visible = false;

            chemp_view.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            name_champ_combo.Visible = false;
            name_champ.Visible = false;
            info.Visible = false;
            city.Visible = false;
            skills.Visible = false;
            gl_expert.Visible = false;
            date_1.Visible = false;
            date_2.Visible = false;
            add.Visible = false;
            change.Visible = false;

            chemp_view.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(chemp_view_RowHeaderMouseClick);
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
                hello.Text = "Добрый день, " + User_info.user_ima;
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(18, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(24, 0, 0))
            {
                hello.Text = "Добрый вечер, " + User_info.user_ima;
            }
            else
            {
                hello.Text = "Доброй ночи, " + User_info.user_ima;
            }
        }
        private void Table_refresh()
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT * FROM competitions", this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            compet_view.DataSource = data_1.Tables[0].DefaultView; //вывод участников в таблицу
            //bindingSource1.DataSource = data_1.Tables[0].DefaultView;

            SQLiteDataAdapter adapter_2 = new SQLiteDataAdapter("SELECT  DISTINCT users.id, users.fio, skills.title AS skills_title, statuses.name AS status_title, users.telephone, users.email FROM users JOIN roles ON roles.id = users.ID_role JOIN statuses ON statuses.id = users.status JOIN skills ON users.skill = skills.id WHERE users.ID_role = 2 OR users.ID_role = 3 OR users.ID_role = 4 OR users.ID_role = 5", this.connection);
            DataSet data_2 = new DataSet();
            adapter_2.Fill(data_2);
            expert_view.DataSource = data_2.Tables[0].DefaultView; //вывод экспертов в таблицу
            bindingSource1.DataSource = data_2.Tables[0].DefaultView;

            SQLiteDataAdapter adapter_6 = new SQLiteDataAdapter("SELECT DISTINCT competitions.id AS `Код`, competitions.title AS `Название чемпионата`, competitions.date_start AS `Начало чемпионата`, competitions.date_end AS `Конец чемпионата`, competitions.description AS `Ифн-я по чемпионату`, competitions.city AS `Место проведения чемпионата`, skills.title AS `Название скилла`, users.fio AS `ФИО гл. эксперта` FROM users JOIN roles ON roles.id = users.ID_role JOIN competitions ON users.championship = competitions.id JOIN skills ON skills.id = users.skill JOIN competitions_skills ON competitions_skills.competition_id = competitions.id WHERE users.ID_role = 3", this.connection);
            DataSet data_6 = new DataSet(); //надо именно экспертов, так что потом следует поставить "где роль = 3"
            adapter_6.Fill(data_6);
            chemp_view.DataSource = data_6.Tables[0].DefaultView; //вывод чемпионатов в таблицу
            this.connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            this.connection.Open();

            SQLiteCommand cmd_0 = new SQLiteCommand("SELECT * FROM competitions", this.connection);
            SQLiteDataReader DR = cmd_0.ExecuteReader();

            while (DR.Read())
            {
                name_champ_combo.Items.Add(DR[1]);
            }

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
        private void add_Click(object sender, EventArgs e) //добавление чемпионата, его скилла и его главного эксперта
        {
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
        private void ClearData1() //метод очищения полей 
        {
            ID1 = 0;
            name_champ.Text = "";
            date_1.Text = "";
            date_2.Text = "";
            info.Text = "";
            city.Text = "";
            skills.Text = "";
            gl_expert.Text = "";
        }
        private void chemp_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //метод заполнения полей значения из таблицы
        {
            ID1 = Convert.ToInt32(chemp_view.Rows[e.RowIndex].Cells[0].Value.ToString());
            name_champ.Text = chemp_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            date_1.Text = chemp_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            date_2.Text = chemp_view.Rows[e.RowIndex].Cells[3].Value.ToString();
            info.Text = chemp_view.Rows[e.RowIndex].Cells[4].Value.ToString();
            city.Text = chemp_view.Rows[e.RowIndex].Cells[5].Value.ToString();
            skills.Text = chemp_view.Rows[e.RowIndex].Cells[6].Value.ToString();
            gl_expert.Text = chemp_view.Rows[e.RowIndex].Cells[7].Value.ToString();
            User_info.championship_delete = gl_expert.Text;
        }
        private void change_Click(object sender, EventArgs e) //изменение чемпионата, его скилла и его главного эксперта
        {
            if (name_champ.Text != "" && date_1.Text != "" && date_2.Text != "" && info.Text != "" && skills.Text != "" && gl_expert.Text != "")
            {
                this.connection.Open();
                SQLiteCommand command_1 = new SQLiteCommand("UPDATE competitions SET title = @l1, date_start = @l2, date_end = @l3, description = @l4, city = @l5 WHERE id=@l0", connection);
                SQLiteCommand command_2 = new SQLiteCommand("UPDATE competitions_skills SET skill_id = @l7 WHERE competition_id=@l0", connection);
                SQLiteCommand command_3 = new SQLiteCommand("UPDATE users SET championship=@l8 WHERE fio = @l9", connection);
                SQLiteCommand command_4 = new SQLiteCommand("UPDATE users SET championship=@l0 WHERE fio = @l10", connection);
                

                command_1.Parameters.AddWithValue("@l0", ID1); //изменение данных чемпионата в таблице competitions
                command_1.Parameters.AddWithValue("@l1", name_champ.Text);
                command_1.Parameters.AddWithValue("@l2", Convert.ToDateTime(date_1.Text));
                command_1.Parameters.AddWithValue("@l3", Convert.ToDateTime(date_2.Text));
                command_1.Parameters.AddWithValue("@l4", info.Text);
                command_1.Parameters.AddWithValue("@l5", city.Text);
                command_1.ExecuteNonQuery();

                SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что изменённого скилла к чемпионату
                DataTable table_ = new DataTable();
                SQLiteCommand command_ = new SQLiteCommand("SELECT * FROM skills WHERE `title` = @l6", this.connection);
                string l6 = skills.Text;
                command_.Parameters.AddWithValue("@l6", l6);
                adapter_.SelectCommand = command_;
                adapter_.Fill(table_);
                SQLiteDataReader reader_ = command_.ExecuteReader();
                while (reader_.Read())
                {
                    User_info.skills_id_update = $"{reader_["id"]}";
                    break;
                }
                reader_.Close();
                string l7 = User_info.skills_id_update;
                command_2.Parameters.AddWithValue("@l0", ID1); //изменение скилла чемпионата в таблице competitions_skills
                command_2.Parameters.AddWithValue("@l7", l7);
                command_2.ExecuteNonQuery();

                string l8 = null;
                string l9 = User_info.championship_delete;
                command_3.Parameters.AddWithValue("@l8", l8); //удаление чемпионата у предыдущего главного эксперта
                command_3.Parameters.AddWithValue("@l9", l9);
                command_3.ExecuteNonQuery();

                string l10= gl_expert.Text;
                command_4.Parameters.AddWithValue("@l0", ID1); //изменение главного эксперта чемпионата в таблице users
                command_4.Parameters.AddWithValue("@l10", l10);
                command_4.ExecuteNonQuery();


                MessageBox.Show("Запись успешно обновлена");
                this.connection.Close();
                Table_refresh();
                ClearData1();

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для обновления");
            }
        }




        private void chemp_Click(object sender, EventArgs e)
        {
            if (chemp.BackColor == Color.LimeGreen)
            {
                MessageBox.Show("Вы уже на этой вкладке");
            }
            else
            {
                chemp.BackColor = Color.LimeGreen;
                sett_chemp.BackColor = Color.DodgerBlue;
                upr_expert.BackColor = Color.DodgerBlue;
                protocols.BackColor = Color.DodgerBlue;

                compet_view.Visible = true;

                expert_view.Visible = false;
                label10.Visible = false;
                ex_fio.Visible = false;
                ex_skill.Visible = false;
                ex_status.Visible = false;
                box_fio.Visible = false;
                box_skill.Visible = false;
                box_status.Visible = false;

                chemp_view.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                name_champ_combo.Visible = false;
                name_champ.Visible = false;
                info.Visible = false;
                city.Visible = false;
                skills.Visible = false;
                gl_expert.Visible = false;
                date_1.Visible = false;
                date_2.Visible = false;
                add.Visible = false;
                change.Visible = false;
            }
        }
        private void sett_chemp_Click(object sender, EventArgs e)
        {
            
            if (sett_chemp.BackColor == Color.LimeGreen)
            {
                MessageBox.Show("Вы уже на этой вкладке");
            }
            else
            {
                chemp.BackColor = Color.DodgerBlue;
                sett_chemp.BackColor = Color.LimeGreen;
                upr_expert.BackColor = Color.DodgerBlue;
                protocols.BackColor = Color.DodgerBlue;

                compet_view.Visible = false;

                expert_view.Visible = false;
                label10.Visible = false;
                ex_fio.Visible = false;
                ex_skill.Visible = false;
                ex_status.Visible = false;
                box_fio.Visible = false;
                box_skill.Visible = false;
                box_status.Visible = false;

                chemp_view.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                name_champ_combo.Visible = true;
                name_champ.Visible = true;
                info.Visible = true;
                city.Visible = true;
                skills.Visible = true;
                gl_expert.Visible = true;
                date_1.Visible = true;
                date_2.Visible = true;
                add.Visible = true;
                change.Visible = true;
            }
        }

        private void upr_expert_Click(object sender, EventArgs e)
        {
            if (upr_expert.BackColor == Color.LimeGreen)
            {
                MessageBox.Show("Вы уже на этой вкладке");
            }
            else
            {
                chemp.BackColor = Color.DodgerBlue;
                sett_chemp.BackColor = Color.DodgerBlue;
                upr_expert.BackColor = Color.LimeGreen;
                protocols.BackColor = Color.DodgerBlue;

                compet_view.Visible = false;

                expert_view.Visible = true;
                label10.Visible = true;
                ex_fio.Visible = true;
                ex_skill.Visible = true;
                ex_status.Visible = true;
                box_fio.Visible = true;
                box_skill.Visible = true;
                box_status.Visible = true;

                chemp_view.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                name_champ_combo.Visible = false;
                name_champ.Visible = false;
                info.Visible = false;
                city.Visible = false;
                skills.Visible = false;
                gl_expert.Visible = false;
                date_1.Visible = false;
                date_2.Visible = false;
                add.Visible = false;
                change.Visible = false;
            }
        }

        private void protocols_Click(object sender, EventArgs e)
        {
            if (protocols.BackColor == Color.LimeGreen)
            {
                MessageBox.Show("Вы уже на этой вкладке");
            }
            else
            {
                chemp.BackColor = Color.DodgerBlue;
                sett_chemp.BackColor = Color.DodgerBlue;
                upr_expert.BackColor = Color.DodgerBlue;
                protocols.BackColor = Color.LimeGreen;

                compet_view.Visible = false;

                expert_view.Visible = false;
                label10.Visible = false;
                ex_fio.Visible = false;
                ex_skill.Visible = false;
                ex_status.Visible = false;
                box_fio.Visible = false;
                box_skill.Visible = false;
                box_status.Visible = false;

                chemp_view.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                name_champ_combo.Visible = false;
                name_champ.Visible = false;
                info.Visible = false;
                city.Visible = false;
                skills.Visible = false;
                gl_expert.Visible = false;
                date_1.Visible = false;
                date_2.Visible = false;
                add.Visible = false;
                change.Visible = false;
            }
        }

        private void box_status_TextChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = "status_title LIKE \'%" + box_status.Text + "%\'";
        }

        private void box_fio_TextChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = "fio LIKE \'%" + box_fio.Text + "%\'";
        }

        private void box_skill_TextChanged(object sender, EventArgs e)
        {
            bindingSource1.Filter = "skills_title LIKE \'%" + box_skill.Text + "%\'";
        }
    }

}
