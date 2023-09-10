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
    public partial class Menu_expert : Form
    {
        private SQLiteConnection connection;
        Avtorizacia f1;
        int ID1 = 0;
        public Menu_expert()
        {
            InitializeComponent();
            player_view.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(player_view_RowHeaderMouseClick);
            ex_view.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(ex_view_RowHeaderMouseClick);
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
            list_player.BackColor = Color.LimeGreen;
            list_expert.BackColor = Color.DodgerBlue;
            list_protocol.BackColor = Color.DodgerBlue;
            ex_view.Visible = false;
            ex_add.Visible = false;
            ex_change.Visible = false;
            ex_delete.Visible = false;
            protocol_view.Visible = false;


        }
        private void Table_refresh()
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT users.id, users.fio AS `ФИО`, users.Birthday AS `Дата рождения`, statuses.name AS `Статус подтверждения`  FROM users JOIN statuses ON statuses.id = users.status WHERE users.ID_role =1", this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            player_view.DataSource = data_1.Tables[0].DefaultView; //вывод участников в таблицу
            SQLiteDataAdapter adapter_2 = new SQLiteDataAdapter("SELECT users.id, users.fio AS `ФИО`, users.Birthday AS `Дата рождения`, statuses.name AS `Статус подтверждения`  FROM users JOIN statuses ON statuses.id = users.status WHERE users.ID_role =2 OR users.ID_role =3 OR users.ID_role =4 OR users.ID_role =5", this.connection);
            DataSet data_2 = new DataSet();
            adapter_2.Fill(data_2);
            ex_view.DataSource = data_2.Tables[0].DefaultView; //вывод экспертов в таблицу
            SQLiteDataAdapter adapter_3 = new SQLiteDataAdapter("SELECT DISTINCT users_signs.id, users.fio, protocols.title, signs.sign FROM users JOIN users_signs ON users_signs.user_id = users.id JOIN signs ON signs.id = users_signs.sign_id JOIN protocols ON protocols.id = users_signs.protocol_id WHERE users.id = " + User_info.user_id, this.connection);
            DataSet data_3 = new DataSet();
            adapter_3.Fill(data_3);
            protocol_view.DataSource = data_3.Tables[0].DefaultView; //вывод протоколов в таблицу
            this.connection.Close();
        }
        private void Menu_expert_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            this.connection.Open();
            SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что изменённого скилла к чемпионату
            DataTable table_ = new DataTable();
            SQLiteCommand command_ = new SQLiteCommand("SELECT competitions.title FROM users JOIN competitions ON competitions.id = users.championship WHERE users.id =" + User_info.user_id, this.connection);
            adapter_.SelectCommand = command_;
            adapter_.Fill(table_);
            SQLiteDataReader reader_ = command_.ExecuteReader();
            while (reader_.Read())
            {
                name_competition.Text = reader_["title"] as string;
                break;
            }
            reader_.Close();

            player_g.Items.Add("м"); //добавление пола
            player_g.Items.Add("ж");

            SQLiteCommand cmd_0 = new SQLiteCommand("SELECT name FROM regions", this.connection); //добавление региона
            SQLiteDataReader DR_0 = cmd_0.ExecuteReader();
            while (DR_0.Read())
            {
                player_reg.Items.Add(DR_0[0]);
            }
            DR_0.Close();

            SQLiteCommand cmd_1 = new SQLiteCommand("SELECT title FROM skills", this.connection); //добавление компетенции
            SQLiteDataReader DR_1 = cmd_1.ExecuteReader();
            while (DR_1.Read())
            {
                player_skill.Items.Add(DR_1[0]);
            }
            DR_1.Close();

            SQLiteCommand cmd_3 = new SQLiteCommand("SELECT title FROM competitions", this.connection); //добавление чемпионата
            SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
            while (DR_3.Read())
            {
                player_champ.Items.Add(DR_3[0]);
            }
            DR_3.Close();

            this.connection.Close();
            Table_refresh();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            f1 = new Avtorizacia();
            f1.Show();
            this.Hide();
        }

        private void list_player_Click(object sender, EventArgs e)
        {
            if(list_player.BackColor != Color.LimeGreen)
            {
                list_player.BackColor = Color.LimeGreen;
                list_expert.BackColor = Color.DodgerBlue;
                list_protocol.BackColor = Color.DodgerBlue;

                player_view.Visible = true;
                ex_view.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                player_champ.Visible = true;
                player_data.Visible = true;
                player_fio.Visible = true;
                player_g.Visible = true;
                player_mail.Visible = true;
                player_reg.Visible = true;
                player_skill.Visible = true;
                player_tel.Visible = true;
                player_pin.Visible = true;
                player_pas.Visible = true;
                protocol_view.Visible = false;
                ex_add.Visible = false;
                ex_change.Visible = false;
                ex_delete.Visible = false;
                add_player.Visible = true;
                change_player.Visible = true;
                delete_player.Visible = true;
            }
        }

        private void list_expert_Click(object sender, EventArgs e)
        {
            if (list_expert.BackColor != Color.LimeGreen)
            {
                list_player.BackColor = Color.DodgerBlue;
                list_expert.BackColor = Color.LimeGreen;
                list_protocol.BackColor = Color.DodgerBlue;

                player_view.Visible = false;
                ex_view.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                player_champ.Visible = true;
                player_data.Visible = true;
                player_fio.Visible = true;
                player_g.Visible = true;
                player_mail.Visible = true;
                player_reg.Visible = true;
                player_skill.Visible = true;
                player_tel.Visible = true;
                player_pin.Visible = true;
                player_pas.Visible = true;
                protocol_view.Visible = false;
                ex_add.Visible = true;
                ex_change.Visible = true;
                ex_delete.Visible = true;
                add_player.Visible = false;
                change_player.Visible = false;
                delete_player.Visible = false;

            }
        }

        private void list_protocol_Click(object sender, EventArgs e)
        {
            if (list_protocol.BackColor != Color.LimeGreen)
            {
                list_player.BackColor = Color.DodgerBlue;
                list_expert.BackColor = Color.DodgerBlue;
                list_protocol.BackColor = Color.LimeGreen;

                player_view.Visible = false;
                ex_view.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                player_champ.Visible = false;
                player_data.Visible = false;
                player_fio.Visible = false;
                player_g.Visible = false;
                player_mail.Visible = false;
                player_reg.Visible = false;
                player_skill.Visible = false;
                player_tel.Visible = false;
                player_pin.Visible = false;
                player_pas.Visible = false;
                protocol_view.Visible = true;
                ex_add.Visible = false;
                ex_change.Visible = false;
                ex_delete.Visible = false;
                add_player.Visible = false;
                change_player.Visible = false;
                delete_player.Visible = false;

            }
        }

        private void add_player_Click(object sender, EventArgs e)
        {
            this.connection.Open();
            try
            {
                string sql_1 = "INSERT INTO users(fio, gender, password, pin, Birthday, ID_role, skill, region, championship, status, telephone, email) VALUES (@l1,@l2,@l3,@l4,@l5,@l6,@l7,@l8,@l9,@l10,@l11,@l12)";
                SQLiteCommand command_1 = new SQLiteCommand(sql_1, connection);
                SQLiteParameter param_1 = new SQLiteParameter("@l1", player_fio.Text);
                SQLiteParameter param_2 = new SQLiteParameter("@l2", player_g.Text);
                SQLiteParameter param_3 = new SQLiteParameter("@l3", player_pas.Text);
                SQLiteParameter param_4 = new SQLiteParameter("@l4", Convert.ToInt32(player_pin.Text));
                SQLiteParameter param_5 = new SQLiteParameter("@l5", Convert.ToDateTime(player_data.Text));
                SQLiteParameter param_6 = new SQLiteParameter("@l6", "1");
                SQLiteParameter param_7 = new SQLiteParameter("@l7", player_skill.Text);
                SQLiteParameter param_8 = new SQLiteParameter("@l8", player_reg.Text);
                SQLiteParameter param_9 = new SQLiteParameter("@l9", player_champ.Text);
                SQLiteParameter param_10 = new SQLiteParameter("@l10", "2");
                SQLiteParameter param_11 = new SQLiteParameter("@l11", player_tel.Text);
                SQLiteParameter param_12 = new SQLiteParameter("@l12", player_mail.Text);
                command_1.Parameters.Add(param_1);
                command_1.Parameters.Add(param_2);
                command_1.Parameters.Add(param_3);
                command_1.Parameters.Add(param_4);
                command_1.Parameters.Add(param_5);
                command_1.Parameters.Add(param_6);
                command_1.Parameters.Add(param_7);
                command_1.Parameters.Add(param_8);
                command_1.Parameters.Add(param_9);
                command_1.Parameters.Add(param_10);
                command_1.Parameters.Add(param_11);
                command_1.Parameters.Add(param_12);
                using (command_1) //проверка на добавление записи в бд
                {
                    int count_add_1 = command_1.ExecuteNonQuery();
                    if (count_add_1 <= 0)
                    {
                        MessageBox.Show("Ошибка подключения");
                    }
                    else
                    {
                        MessageBox.Show("Запись успешно добавлена");
                    }
                }
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Введите число!");
            }
            this.connection.Close();
            Table_refresh();
        }
        private void ClearData1() //метод очищения полей 
        {
            ID1 = 0;
            player_fio.Text = "";
            player_data.Text = "";
            player_g.Text = "";
            player_champ.Text = "";
            player_mail.Text = "";
            player_reg.Text = "";
            player_skill.Text = "";
            player_tel.Text = "";
            player_pin.Text = "";
            player_pas.Text = "";
        }
        private void player_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //метод заполнения полей
        {
            ID1 = Convert.ToInt32(player_view.Rows[e.RowIndex].Cells[0].Value.ToString());
            player_fio.Text = player_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            player_data.Text = player_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.connection.Open();
            int r;
            int s;
            int c;
            SQLiteCommand cmd_0 = new SQLiteCommand("SELECT * FROM users WHERE users.id = @l0" , this.connection); 
            SQLiteParameter param_0 = new SQLiteParameter("@l0", ID1);
            cmd_0.Parameters.Add(param_0);
            SQLiteDataReader DR_0 = cmd_0.ExecuteReader();
            while (DR_0.Read())
            {
                player_g.Text = DR_0["gender"] as string;
                player_mail.Text = DR_0["email"] as string;
                player_tel.Text = DR_0["telephone"] as string;
                player_pin.Text = Convert.ToString(DR_0.GetInt32(4));
                player_pas.Text = DR_0["password"] as string;

                c = DR_0.GetInt32(10); //id чемпионата
                SQLiteCommand cmd_1 = new SQLiteCommand("SELECT title FROM competitions WHERE id = @c", this.connection);
                SQLiteParameter param_1 = new SQLiteParameter("@c", c);
                cmd_1.Parameters.Add(param_1);
                SQLiteDataReader DR_1 = cmd_1.ExecuteReader();
                while (DR_1.Read())
                {
                    player_champ.Text = DR_1["title"] as string;
                }
                DR_1.Close();

                r = DR_0.GetInt32(8); //id региона
                SQLiteCommand cmd_2 = new SQLiteCommand("SELECT name FROM regions WHERE id = @r", this.connection);
                SQLiteParameter param_2 = new SQLiteParameter("@r", r);
                cmd_2.Parameters.Add(param_2);
                SQLiteDataReader DR_2 = cmd_2.ExecuteReader();
                while (DR_2.Read())
                {
                    player_reg.Text = DR_2["name"] as string;
                }
                DR_2.Close();

                s = DR_0.GetInt32(7); //id скилла
                SQLiteCommand cmd_3 = new SQLiteCommand("SELECT title FROM skills WHERE id = @s", this.connection);
                SQLiteParameter param_3 = new SQLiteParameter("@s", s);
                cmd_3.Parameters.Add(param_3);
                SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
                while (DR_3.Read())
                {
                    player_skill.Text = DR_3["title"] as string;
                }
                DR_3.Close();

                
            }
            DR_0.Close();
            this.connection.Close();
        }
        private void change_player_Click(object sender, EventArgs e) //изменение участника
        {
            if (player_fio.Text != "" && player_g.Text != "" && player_pas.Text != "" && player_pin.Text != "" && player_skill.Text != "" && player_reg.Text != "" && player_champ.Text != "" && player_tel.Text != "" && player_mail.Text != "")
            {
                this.connection.Open();
                string l1 = player_skill.Text;
                string l2 = player_reg.Text;
                string l3 = player_champ.Text;
                SQLiteCommand cmd_1 = new SQLiteCommand("SELECT id FROM skills WHERE skills.title = @l1", this.connection); //добавление региона
                SQLiteParameter param_1 = new SQLiteParameter("@l1", l1);
                cmd_1.Parameters.Add(param_1);
                SQLiteDataReader DR_1 = cmd_1.ExecuteReader();
                while (DR_1.Read())
                {
                    User_info.s= DR_1.GetInt32(0);
                }
                DR_1.Close();
                SQLiteCommand cmd_2 = new SQLiteCommand("SELECT id FROM regions WHERE regions.name = @l2", this.connection); //добавление региона
                SQLiteParameter param_2 = new SQLiteParameter("@l2", l2);
                cmd_2.Parameters.Add(param_2);
                SQLiteDataReader DR_2 = cmd_2.ExecuteReader();
                while (DR_2.Read())
                {
                    User_info.r = DR_2.GetInt32(0);
                }
                DR_2.Close();
                SQLiteCommand cmd_3 = new SQLiteCommand("SELECT id FROM competitions WHERE competitions.title = @l3", this.connection); //добавление региона
                SQLiteParameter param_3 = new SQLiteParameter("@l3", l3);
                cmd_3.Parameters.Add(param_3);
                SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
                while (DR_3.Read())
                {
                    User_info.c = DR_3.GetInt32(0);
                }
                DR_3.Close();
                SQLiteCommand command_1 = new SQLiteCommand("UPDATE users SET fio=@l1, gender=@l2, password=@l3, pin=@l4, Birthday=@l5, skill=@l6, region=@l7, championship=@l8, status=@l9, telephone=@l10, email=@l11 WHERE id=@l0", connection);
                command_1.Parameters.AddWithValue("@l0", ID1);
                command_1.Parameters.AddWithValue("@l1", player_fio.Text);
                command_1.Parameters.AddWithValue("@l2", player_g.Text);
                command_1.Parameters.AddWithValue("@l3", player_pas.Text);
                command_1.Parameters.AddWithValue("@l4", Convert.ToInt32(player_pin.Text));
                command_1.Parameters.AddWithValue("@l5", Convert.ToDateTime(player_data.Text));
                command_1.Parameters.AddWithValue("@l6", User_info.s); 
                command_1.Parameters.AddWithValue("@l7", User_info.r); 
                command_1.Parameters.AddWithValue("@l8", User_info.c); 
                command_1.Parameters.AddWithValue("@l9", "3");
                command_1.Parameters.AddWithValue("@l10", player_tel.Text);
                command_1.Parameters.AddWithValue("@l11", player_mail.Text);
                command_1.ExecuteNonQuery();
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

        private void delete_player_Click(object sender, EventArgs e) //удаление участника
        {
            this.connection.Open();
            SQLiteCommand cmd_3 = new SQLiteCommand("SELECT status FROM users WHERE id = @l0", this.connection);
            SQLiteParameter param_3 = new SQLiteParameter("@l0", ID1);
            cmd_3.Parameters.Add(param_3);
            SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
            while (DR_3.Read())
            {
                User_info.status = DR_3.GetInt32(0);
                
            }
            DR_3.Close();
            if (User_info.status == 1)
            {
                SQLiteCommand _command = new SQLiteCommand("UPDATE users SET status=@l1 WHERE id = @l0", connection);
                _command.Parameters.AddWithValue("@l0", ID1);
                _command.Parameters.AddWithValue("@l1", User_info.status);
                _command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно удалена");
            }
            else
            {
                MessageBox.Show("Ожидается подтверждение от организатора");
            }
            this.connection.Close();
            Table_refresh();
        }

        private void ex_add_Click(object sender, EventArgs e) //добавление эксперта
        {
            this.connection.Open();
            try
            {
                string sql_1 = "INSERT INTO users(fio, gender, password, pin, Birthday, ID_role, skill, region, championship, status, telephone, email) VALUES (@l1,@l2,@l3,@l4,@l5,@l6,@l7,@l8,@l9,@l10,@l11,@l12)";
                SQLiteCommand command_1 = new SQLiteCommand(sql_1, connection);
                SQLiteParameter param_1 = new SQLiteParameter("@l1", player_fio.Text);
                SQLiteParameter param_2 = new SQLiteParameter("@l2", player_g.Text);
                SQLiteParameter param_3 = new SQLiteParameter("@l3", player_pas.Text);
                SQLiteParameter param_4 = new SQLiteParameter("@l4", Convert.ToInt32(player_pin.Text));
                SQLiteParameter param_5 = new SQLiteParameter("@l5", Convert.ToDateTime(player_data.Text));
                SQLiteParameter param_6 = new SQLiteParameter("@l6", "2");
                SQLiteParameter param_7 = new SQLiteParameter("@l7", player_skill.Text);
                SQLiteParameter param_8 = new SQLiteParameter("@l8", player_reg.Text);
                SQLiteParameter param_9 = new SQLiteParameter("@l9", player_champ.Text);
                SQLiteParameter param_10 = new SQLiteParameter("@l10", "2");
                SQLiteParameter param_11 = new SQLiteParameter("@l11", player_tel.Text);
                SQLiteParameter param_12 = new SQLiteParameter("@l12", player_mail.Text);
                command_1.Parameters.Add(param_1);
                command_1.Parameters.Add(param_2);
                command_1.Parameters.Add(param_3);
                command_1.Parameters.Add(param_4);
                command_1.Parameters.Add(param_5);
                command_1.Parameters.Add(param_6);
                command_1.Parameters.Add(param_7);
                command_1.Parameters.Add(param_8);
                command_1.Parameters.Add(param_9);
                command_1.Parameters.Add(param_10);
                command_1.Parameters.Add(param_11);
                command_1.Parameters.Add(param_12);
                using (command_1) //проверка на добавление записи в бд
                {
                    int count_add_1 = command_1.ExecuteNonQuery();
                    if (count_add_1 <= 0)
                    {
                        MessageBox.Show("Ошибка подключения");
                    }
                    else
                    {
                        MessageBox.Show("Запись успешно добавлена");
                    }
                }
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Введите число!");
            }
            this.connection.Close();
            Table_refresh();
        }

        private void ex_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //метод заполнения полей
        {
            ID1 = Convert.ToInt32(player_view.Rows[e.RowIndex].Cells[0].Value.ToString());
            player_fio.Text = player_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            player_data.Text = player_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.connection.Open();
            int r;
            int s;
            int c;
            SQLiteCommand cmd_0 = new SQLiteCommand("SELECT * FROM users WHERE users.id = @l0", this.connection);
            SQLiteParameter param_0 = new SQLiteParameter("@l0", ID1);
            cmd_0.Parameters.Add(param_0);
            SQLiteDataReader DR_0 = cmd_0.ExecuteReader();
            while (DR_0.Read())
            {
                player_g.Text = DR_0["gender"] as string;
                player_mail.Text = DR_0["email"] as string;
                player_tel.Text = DR_0["telephone"] as string;
                player_pin.Text = Convert.ToString(DR_0.GetInt32(4));
                player_pas.Text = DR_0["password"] as string;

                c = DR_0.GetInt32(10); //id чемпионата
                SQLiteCommand cmd_1 = new SQLiteCommand("SELECT title FROM competitions WHERE id = @c", this.connection);
                SQLiteParameter param_1 = new SQLiteParameter("@c", c);
                cmd_1.Parameters.Add(param_1);
                SQLiteDataReader DR_1 = cmd_1.ExecuteReader();
                while (DR_1.Read())
                {
                    player_champ.Text = DR_1["title"] as string;
                }
                DR_1.Close();

                r = DR_0.GetInt32(8); //id региона
                SQLiteCommand cmd_2 = new SQLiteCommand("SELECT name FROM regions WHERE id = @r", this.connection);
                SQLiteParameter param_2 = new SQLiteParameter("@r", r);
                cmd_2.Parameters.Add(param_2);
                SQLiteDataReader DR_2 = cmd_2.ExecuteReader();
                while (DR_2.Read())
                {
                    player_reg.Text = DR_2["name"] as string;
                }
                DR_2.Close();

                s = DR_0.GetInt32(7); //id скилла
                SQLiteCommand cmd_3 = new SQLiteCommand("SELECT title FROM skills WHERE id = @s", this.connection);
                SQLiteParameter param_3 = new SQLiteParameter("@s", s);
                cmd_3.Parameters.Add(param_3);
                SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
                while (DR_3.Read())
                {
                    player_skill.Text = DR_3["title"] as string;
                }
                DR_3.Close();


            }
            DR_0.Close();
            this.connection.Close();
        }
        private void ex_change_Click(object sender, EventArgs e) //изменение эксперта
        {
            if (player_fio.Text != "" && player_g.Text != "" && player_pas.Text != "" && player_pin.Text != "" && player_skill.Text != "" && player_reg.Text != "" && player_champ.Text != "" && player_tel.Text != "" && player_mail.Text != "")
            {
                this.connection.Open();
                string l1 = player_skill.Text;
                string l2 = player_reg.Text;
                string l3 = player_champ.Text;
                SQLiteCommand cmd_1 = new SQLiteCommand("SELECT id FROM skills WHERE skills.title = @l1", this.connection); //добавление региона
                SQLiteParameter param_1 = new SQLiteParameter("@l1", l1);
                cmd_1.Parameters.Add(param_1);
                SQLiteDataReader DR_1 = cmd_1.ExecuteReader();
                while (DR_1.Read())
                {
                    User_info.s = DR_1.GetInt32(0);
                }
                DR_1.Close();
                SQLiteCommand cmd_2 = new SQLiteCommand("SELECT id FROM regions WHERE regions.name = @l2", this.connection); //добавление региона
                SQLiteParameter param_2 = new SQLiteParameter("@l2", l2);
                cmd_2.Parameters.Add(param_2);
                SQLiteDataReader DR_2 = cmd_2.ExecuteReader();
                while (DR_2.Read())
                {
                    User_info.r = DR_2.GetInt32(0);
                }
                DR_2.Close();
                SQLiteCommand cmd_3 = new SQLiteCommand("SELECT id FROM competitions WHERE competitions.title = @l3", this.connection); //добавление региона
                SQLiteParameter param_3 = new SQLiteParameter("@l3", l3);
                cmd_3.Parameters.Add(param_3);
                SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
                while (DR_3.Read())
                {
                    User_info.c = DR_3.GetInt32(0);
                }
                DR_3.Close();
                SQLiteCommand command_1 = new SQLiteCommand("UPDATE users SET fio=@l1, gender=@l2, password=@l3, pin=@l4, Birthday=@l5, skill=@l6, region=@l7, championship=@l8, status=@l9, telephone=@l10, email=@l11 WHERE id=@l0", connection);
                command_1.Parameters.AddWithValue("@l0", ID1);
                command_1.Parameters.AddWithValue("@l1", player_fio.Text);
                command_1.Parameters.AddWithValue("@l2", player_g.Text);
                command_1.Parameters.AddWithValue("@l3", player_pas.Text);
                command_1.Parameters.AddWithValue("@l4", Convert.ToInt32(player_pin.Text));
                command_1.Parameters.AddWithValue("@l5", Convert.ToDateTime(player_data.Text));
                command_1.Parameters.AddWithValue("@l6", User_info.s);
                command_1.Parameters.AddWithValue("@l7", User_info.r);
                command_1.Parameters.AddWithValue("@l8", User_info.c);
                command_1.Parameters.AddWithValue("@l9", "3");
                command_1.Parameters.AddWithValue("@l10", player_tel.Text);
                command_1.Parameters.AddWithValue("@l11", player_mail.Text);
                command_1.ExecuteNonQuery();
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

        private void ex_delete_Click(object sender, EventArgs e) //удаление эксперта
        {
            this.connection.Open();
            SQLiteCommand cmd_3 = new SQLiteCommand("SELECT status FROM users WHERE id = @l0", this.connection);
            SQLiteParameter param_3 = new SQLiteParameter("@l0", ID1);
            cmd_3.Parameters.Add(param_3);
            SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
            while (DR_3.Read())
            {
                User_info.status = DR_3.GetInt32(0);

            }
            DR_3.Close();
            if (User_info.status == 1)
            {
                SQLiteCommand _command = new SQLiteCommand("UPDATE users SET status=@l1 WHERE id = @l0", connection);
                _command.Parameters.AddWithValue("@l0", ID1);
                _command.Parameters.AddWithValue("@l1", User_info.status);
                _command.ExecuteNonQuery();
                MessageBox.Show("Запись успешно удалена");
            }
            else
            {
                MessageBox.Show("Ожидается подтверждение от организатора");
            }
            this.connection.Close();
            Table_refresh();
        }

        private void protocol_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string f = protocol_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            string pr = protocol_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            string st = protocol_view.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (st == "Подписан")
            {
                MessageBox.Show("Протокол уже подписан");
            }
            else
            {
                DialogResult result = MessageBox.Show(
        "Подписать протокол?",
        "Подпись",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    this.connection.Open();
                    SQLiteCommand cmd_3 = new SQLiteCommand("SELECT id FROM protocols WHERE title = @l0", this.connection);
                    SQLiteParameter param_3 = new SQLiteParameter("@l0", pr);
                    cmd_3.Parameters.Add(param_3);
                    SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
                    while (DR_3.Read())
                    {
                        User_info.status_protocol = DR_3.GetInt32(0);
                    }
                    DR_3.Close();
                    SQLiteCommand cmd_2 = new SQLiteCommand("SELECT id FROM users WHERE fio = @l0", this.connection);
                    SQLiteParameter param_2 = new SQLiteParameter("@l0", f);
                    cmd_2.Parameters.Add(param_2);
                    SQLiteDataReader DR_2 = cmd_2.ExecuteReader();
                    while (DR_2.Read())
                    {
                        User_info.status_fio = DR_2.GetInt32(0);
                    }
                    DR_2.Close();
                    SQLiteCommand _command = new SQLiteCommand("UPDATE users_signs SET sign_id=@l2 WHERE user_id = @l0 AND protocol_id = @l1", connection);
                    _command.Parameters.AddWithValue("@l0", User_info.status_fio);
                    _command.Parameters.AddWithValue("@l1", User_info.status_protocol);
                    _command.Parameters.AddWithValue("@l2", 1);
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Протокол успешно подписан");
                    this.connection.Close();
                }
                Table_refresh();
            }
        }
    }
}
