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
    public partial class Setting_champ : Form
    {
        private SQLiteConnection connection;
        int ID1 = 0;
        int ID2 = 0;
        public Setting_champ()
        {
            InitializeComponent();
            prot_view.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(prot_view_RowHeaderMouseClick);
            name_competition.Text = User_info.name_champ;
            player.BackColor = Color.LimeGreen;
            sett_champ.BackColor = Color.DodgerBlue;
            list_expert.BackColor = Color.DodgerBlue;
            protocols.BackColor = Color.DodgerBlue;

            users_view.Visible = true;
            user_status.Visible = true;
            user_fio.Visible = true;
            user_skill.Visible = true;
            box_fio.Visible = true;
            box_skill.Visible = true;
            box_status.Visible = true;
            label10.Visible = true;

            expert_view.Visible = false;
            ex_fio.Visible = false;
            ex_skill.Visible = false;
            box_ex_fio.Visible = false;
            box_ex_skill.Visible = false;

            tabControl1.Visible = false;

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
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT  DISTINCT users.id, users.fio, skills.title AS skills_title, statuses.name AS status_title FROM users JOIN roles ON roles.id = users.ID_role JOIN statuses ON statuses.id = users.status JOIN skills ON users.skill = skills.id WHERE users.ID_role = 1 AND championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            users_view.DataSource = data_1.Tables[0].DefaultView;
            bindingSource1.DataSource = data_1.Tables[0].DefaultView;

            SQLiteDataAdapter adapter_2 = new SQLiteDataAdapter("SELECT  DISTINCT users.id, users.fio, roles.role, skills.title AS skills_title, users.telephone, users.email FROM users JOIN roles ON roles.id = users.ID_role JOIN statuses ON statuses.id = users.status JOIN skills ON users.skill = skills.id WHERE championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_2 = new DataSet();
            adapter_2.Fill(data_2);
            expert_view.DataSource = data_2.Tables[0].DefaultView;
            bindingSource2.DataSource = data_2.Tables[0].DefaultView;

            SQLiteDataAdapter adapter_3 = new SQLiteDataAdapter("SELECT DISTINCT protocols.id, protocols.title, protocols.content FROM protocols JOIN roles_protocols ON roles_protocols.protocol_id = protocols.id JOIN users ON roles_protocols.role_id = users.ID_role WHERE championship =" + User_info.id_champ_combo, this.connection);
            DataSet data_3 = new DataSet();
            adapter_3.Fill(data_3);
            prot_view.DataSource = data_3.Tables[0].DefaultView;

            SQLiteDataAdapter adapter_4 = new SQLiteDataAdapter("SELECT DISTINCT credentials.id, credentials.name,credentials.info, credentials.min_count_ex, credentials.max_count_users FROM credentials JOIN roles_credentials ON roles_credentials.credential_id = credentials.id JOIN users ON roles_credentials.role_id = users.ID_role WHERE championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_4 = new DataSet();
            adapter_4.Fill(data_4);
            cred_view.DataSource = data_4.Tables[0].DefaultView;
            this.connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            Table_refresh();
            this.connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного чемпионата
            DataTable table = new DataTable();
            SQLiteCommand command = new SQLiteCommand("SELECT expert_1, expert_2 FROM competitions WHERE `id` = " + User_info.id_champ_combo, this.connection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetInt32(0) == 1)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (reader.GetInt32(1) == 1)
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;
                }
            }
            reader.Close();
            SQLiteCommand cmd_0 = new SQLiteCommand("SELECT * FROM roles", this.connection);
            SQLiteDataReader DR = cmd_0.ExecuteReader();
            while (DR.Read())
            {
                prot_role.Items.Add(DR[1]);
                poln_role.Items.Add(DR[1]);
            }
            DR.Close();
            this.connection.Close();
            radio_all.Checked = true;

        }

        private void player_Click(object sender, EventArgs e)
        {
            if (player.BackColor != Color.LimeGreen)
            {
                player.BackColor = Color.LimeGreen;
                sett_champ.BackColor = Color.DodgerBlue;
                list_expert.BackColor = Color.DodgerBlue;
                protocols.BackColor = Color.DodgerBlue;

                users_view.Visible = true;
                user_status.Visible = true;
                user_fio.Visible = true;
                user_skill.Visible = true;
                box_fio.Visible = true;
                box_skill.Visible = true;
                box_status.Visible = true;
                label10.Visible = true;

                expert_view.Visible = false;
                ex_fio.Visible = false;
                ex_skill.Visible = false;
                box_ex_fio.Visible = false;
                box_ex_skill.Visible = false;

                tabControl1.Visible = false;
            }
        }
        private void sett_champ_Click(object sender, EventArgs e)
        {
            if (sett_champ.BackColor != Color.LimeGreen)
            {
                player.BackColor = Color.DodgerBlue;
                sett_champ.BackColor = Color.LimeGreen;
                list_expert.BackColor = Color.DodgerBlue;
                protocols.BackColor = Color.DodgerBlue;

                users_view.Visible = false;
                user_status.Visible = false;
                user_fio.Visible = false;
                user_skill.Visible = false;
                box_fio.Visible = false;
                box_skill.Visible = false;
                box_status.Visible = false;
                label10.Visible = false;

                expert_view.Visible = false;
                ex_fio.Visible = false;
                ex_skill.Visible = false;
                box_ex_fio.Visible = false;
                box_ex_skill.Visible = false;

                tabControl1.Visible = true;
            }
        }
        private void list_expert_Click(object sender, EventArgs e)
        {
            if (list_expert.BackColor != Color.LimeGreen)
            {
                player.BackColor = Color.DodgerBlue;
                sett_champ.BackColor = Color.DodgerBlue;
                list_expert.BackColor = Color.LimeGreen;
                protocols.BackColor = Color.DodgerBlue;

                users_view.Visible = false;
                user_status.Visible = false;
                user_fio.Visible = false;
                user_skill.Visible = false;
                box_fio.Visible = false;
                box_skill.Visible = false;
                box_status.Visible = false;
                label10.Visible = true;

                expert_view.Visible = true;
                ex_fio.Visible = true;
                ex_skill.Visible = true;
                box_ex_fio.Visible = true;
                box_ex_skill.Visible = true;

                tabControl1.Visible = false;
            }
        }

        private void protocols_Click(object sender, EventArgs e)
        {
            if (protocols.BackColor != Color.LimeGreen)
            {
                player.BackColor = Color.DodgerBlue;
                sett_champ.BackColor = Color.DodgerBlue;
                list_expert.BackColor = Color.DodgerBlue;
                protocols.BackColor = Color.LimeGreen;

                users_view.Visible = false;
                user_status.Visible = false;
                user_fio.Visible = false;
                user_skill.Visible = false;
                box_fio.Visible = false;
                box_skill.Visible = false;
                box_status.Visible = false;
                label10.Visible = false;

                expert_view.Visible = false;
                ex_fio.Visible = false;
                ex_skill.Visible = false;
                box_ex_fio.Visible = false;
                box_ex_skill.Visible = false;

                tabControl1.Visible = false;
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

        private void box_ex_fio_TextChanged(object sender, EventArgs e)
        {
            bindingSource2.Filter = "fio LIKE \'%" + box_ex_fio.Text + "%\'";
        }

        private void box_ex_skill_TextChanged(object sender, EventArgs e)
        {
            bindingSource2.Filter = "skills_title LIKE \'%" + box_ex_skill.Text + "%\'";
        }



        private void save_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного чемпионата
                DataTable table = new DataTable();
                SQLiteCommand command = new SQLiteCommand("UPDATE competitions SET expert_1 = @chek_1 WHERE `id` = " + User_info.id_champ_combo, this.connection);
                SQLiteParameter param_1 = new SQLiteParameter("@chek_1", "1");
                command.Parameters.Add(param_1);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                this.connection.Close();
            }
            else
            {
                this.connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного чемпионата
                DataTable table = new DataTable();
                SQLiteCommand command = new SQLiteCommand("UPDATE competitions SET expert_1 = @chek_1 WHERE `id` = " + User_info.id_champ_combo, this.connection);
                SQLiteParameter param_1 = new SQLiteParameter("@chek_1", "0");
                command.Parameters.Add(param_1);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                this.connection.Close();
            }
            if (checkBox2.Checked == true)
            {
                this.connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного чемпионата
                DataTable table = new DataTable();
                SQLiteCommand command = new SQLiteCommand("UPDATE competitions SET expert_2 = @chek_2 WHERE `id` = " + User_info.id_champ_combo, this.connection);
                SQLiteParameter param_2 = new SQLiteParameter("@chek_2", "1");
                command.Parameters.Add(param_2);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                this.connection.Close();
            }
            else
            {
                this.connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного чемпионата
                DataTable table = new DataTable();
                SQLiteCommand command = new SQLiteCommand("UPDATE competitions SET expert_2 = @chek_2 WHERE `id` = " + User_info.id_champ_combo, this.connection);
                SQLiteParameter param_2 = new SQLiteParameter("@chek_2", "0");
                command.Parameters.Add(param_2);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                this.connection.Close();
            }
            MessageBox.Show("Изменения успешно сохранены");
            Table_refresh();
        }

        private void radio_uch_CheckedChanged(object sender, EventArgs e)
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT DISTINCT protocols.id, protocols.title, protocols.content FROM protocols JOIN roles_protocols ON roles_protocols.protocol_id = protocols.id JOIN users ON roles_protocols.role_id = users.ID_role WHERE users.ID_role = 1 AND championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            prot_view.DataSource = data_1.Tables[0].DefaultView;
            this.connection.Close();
        }

        private void radio_ex_CheckedChanged(object sender, EventArgs e)
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT DISTINCT protocols.id, protocols.title, protocols.content FROM protocols JOIN roles_protocols ON roles_protocols.protocol_id = protocols.id JOIN users ON roles_protocols.role_id = users.ID_role WHERE (users.ID_role = 2 OR users.ID_role = 3 OR users.ID_role = 4 OR users.ID_role = 5) AND championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            prot_view.DataSource = data_1.Tables[0].DefaultView;
            this.connection.Close();

        }

        private void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT DISTINCT protocols.id, protocols.title, protocols.content FROM protocols JOIN roles_protocols ON roles_protocols.protocol_id = protocols.id JOIN users ON roles_protocols.role_id = users.ID_role WHERE championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            prot_view.DataSource = data_1.Tables[0].DefaultView;
            this.connection.Close();
        }

        private void add_prot_Click(object sender, EventArgs e) //добавление протокола
        {
            this.connection.Open(); 
            string label_1 = prot_name.Text;
            string label_2 = prot_info.Text;
            string label_3 = prot_role.Text;
            string sql_1 = "INSERT INTO protocols(title, content) VALUES (@label_1,@label_2)";
            SQLiteCommand command_1 = new SQLiteCommand(sql_1, connection);
            SQLiteParameter param_1 = new SQLiteParameter("@label_1", label_1);
            SQLiteParameter param_2 = new SQLiteParameter("@label_2", label_2);
            command_1.Parameters.Add(param_1);
            command_1.Parameters.Add(param_2);
            using (command_1) //проверка на добавление записи протокола (protocols) в бд
            {
                int count_add_1 = command_1.ExecuteNonQuery();
                if (count_add_1 <= 0)
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного протокола
            DataTable table = new DataTable();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM protocols WHERE `title` = @label_1 and `content` = @label_2", this.connection);
            command.Parameters.AddWithValue("@label_1", label_1);
            command.Parameters.AddWithValue("@label_2", label_2);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User_info.id_protocol = $"{reader["id"]}";
                break;
            }
            reader.Close();
            SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что добавленнй роли к протоколу
            DataTable table_ = new DataTable();
            SQLiteCommand command_ = new SQLiteCommand("SELECT * FROM roles WHERE `role` = @label_3", this.connection);
            command_.Parameters.AddWithValue("@label_3", label_3);
            adapter_.SelectCommand = command_;
            adapter_.Fill(table_);
            SQLiteDataReader reader_ = command_.ExecuteReader();
            while (reader_.Read())
            {
                User_info.id_role_protocol = $"{reader_["id"]}";
                break;
            }
            reader_.Close();
            string sql_2 = "INSERT INTO roles_protocols(role_id, protocol_id) VALUES (@label_4,@label_5)";
            SQLiteCommand command_2 = new SQLiteCommand(sql_2, connection);
            SQLiteParameter param_4 = new SQLiteParameter("@label_4", User_info.id_role_protocol);
            SQLiteParameter param_5 = new SQLiteParameter("@label_5", User_info.id_protocol);
            command_2.Parameters.Add(param_4);
            command_2.Parameters.Add(param_5);
            using (command_2) //проверка на добавление записи чемпионата (competitions) в бд
            {
                int count_add = command_2.ExecuteNonQuery();
                if (count_add <= 0)
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
        private void ClearData1() //метод очищения полей 
        {
            ID1 = 0;
            prot_name.Text = "";
            prot_info.Text = "";
            prot_role.Text = "";
        }
        private void prot_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //метод заполнения полей значения из таблицы
        {
            ID1 = Convert.ToInt32(prot_view.Rows[e.RowIndex].Cells[0].Value.ToString());
            prot_name.Text = prot_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            prot_info.Text = prot_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            //prot_role.Text = prot_view.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void change_prot_Click(object sender, EventArgs e) //изменение протокола
        {
            if (prot_name.Text != "" && prot_info.Text != "" && prot_role.Text != "")
            {
                this.connection.Open();
                SQLiteCommand command_1 = new SQLiteCommand("UPDATE protocols SET title = @l1, content = @l2 WHERE id=@l0", connection);
                SQLiteCommand command_2 = new SQLiteCommand("UPDATE roles_protocols SET role_id = @l4 WHERE protocol_id=@l0", connection);


                command_1.Parameters.AddWithValue("@l0", ID1); //изменение данных протокола в таблице protocols
                command_1.Parameters.AddWithValue("@l1", prot_name.Text);
                command_1.Parameters.AddWithValue("@l2", prot_info.Text);
                command_1.ExecuteNonQuery();

                SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что изменённого скилла к чемпионату
                DataTable table_ = new DataTable();
                SQLiteCommand command_ = new SQLiteCommand("SELECT * FROM roles WHERE `role` = @l3", this.connection);
                command_.Parameters.AddWithValue("@l3", prot_role.Text);
                adapter_.SelectCommand = command_;
                adapter_.Fill(table_);
                SQLiteDataReader reader_ = command_.ExecuteReader();
                while (reader_.Read())
                {
                    User_info.role_id_update = $"{reader_["id"]}";
                    break;
                }
                reader_.Close();

                command_2.Parameters.AddWithValue("@l0", ID1); //изменение роли чемпионата в таблице roles_protocols
                command_2.Parameters.AddWithValue("@l4", User_info.role_id_update);
                command_2.ExecuteNonQuery();

                MessageBox.Show("Запись успешно обновлена");
                this.connection.Close();
                Table_refresh();
                ClearData1();

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для обновления");
            }
            radio_all.Checked = true;
        } 

        private void delete_prot_Click(object sender, EventArgs e) //удаление протокола
        {
            this.connection.Open();
            SQLiteCommand _command = new SQLiteCommand("DELETE FROM protocols WHERE id = @l0", connection);
            _command.Parameters.AddWithValue("@l0", ID1);
            _command.ExecuteNonQuery();
            SQLiteCommand _command_ = new SQLiteCommand("DELETE FROM roles_protocols WHERE protocol_id = @l0", connection);
            _command_.Parameters.AddWithValue("@l0", ID1);
            _command_.ExecuteNonQuery();
            this.connection.Close();
            MessageBox.Show("Запись успешно удалена");
            Table_refresh();
        }

        private void add_poln_Click(object sender, EventArgs e) //добавление полномочия
        {
            this.connection.Open(); 
            try
            {
                string label_1 = poln_name.Text;
                string label_2 = poln_info.Text;
                int label_3 = Convert.ToInt32(min.Text);
                int label_4 = Convert.ToInt32(max.Text);
                string label_5 = poln_role.Text;
                string sql_1 = "INSERT INTO credentials(name, info, min_count_ex, max_count_users) VALUES (@label_1,@label_2,@label_3,@label_4)";
                SQLiteCommand command_1 = new SQLiteCommand(sql_1, connection);
                SQLiteParameter param_1 = new SQLiteParameter("@label_1", label_1);
                SQLiteParameter param_2 = new SQLiteParameter("@label_2", label_2);
                SQLiteParameter param_3 = new SQLiteParameter("@label_3", label_3);
                SQLiteParameter param_4 = new SQLiteParameter("@label_4", label_4);
                command_1.Parameters.Add(param_1);
                command_1.Parameters.Add(param_2);
                command_1.Parameters.Add(param_3);
                command_1.Parameters.Add(param_4);
                using (command_1) //проверка на добавление записи полномочия (credentials) в бд
                {
                    int count_add_1 = command_1.ExecuteNonQuery();
                    if (count_add_1 <= 0)
                    {
                        MessageBox.Show("Ошибка подключения");
                    }
                }

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(); //поиск и запись id только что добавленного полномочия
                DataTable table = new DataTable();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM credentials WHERE `name` = @label_1 AND `info` = @label_2 AND `min_count_ex` = @label_3 AND `max_count_users` = @label_4", this.connection);
                command.Parameters.AddWithValue("@label_1", label_1);
                command.Parameters.AddWithValue("@label_2", label_2);
                command.Parameters.AddWithValue("@label_3", label_3);
                command.Parameters.AddWithValue("@label_4", label_4);
                adapter.SelectCommand = command;
                adapter.Fill(table);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User_info.id_credential = $"{reader["id"]}";
                    break;
                }
                reader.Close();
                SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что добавленной роли к протоколу
                DataTable table_ = new DataTable();
                SQLiteCommand command_ = new SQLiteCommand("SELECT * FROM roles WHERE `role` = @label_5", this.connection);
                command_.Parameters.AddWithValue("@label_5", label_5);
                adapter_.SelectCommand = command_;
                adapter_.Fill(table_);
                SQLiteDataReader reader_ = command_.ExecuteReader();
                while (reader_.Read())
                {
                    User_info.id_role_credential = $"{reader_["id"]}";
                    break;
                }
                reader_.Close();
                string sql_2 = "INSERT INTO roles_credentials(role_id, credential_id) VALUES (@label_6,@label_7)";
                SQLiteCommand command_2 = new SQLiteCommand(sql_2, connection);
                SQLiteParameter param_6 = new SQLiteParameter("@label_6", User_info.id_role_credential);
                SQLiteParameter param_7 = new SQLiteParameter("@label_7", User_info.id_credential);
                command_2.Parameters.Add(param_6);
                command_2.Parameters.Add(param_7);
                using (command_2) //проверка на добавление записи полномочия (credentials) в бд
                {
                    int count_add = command_2.ExecuteNonQuery();
                    if (count_add <= 0)
                    {
                        MessageBox.Show("Ошибка подключения");
                    }
                    else
                    {
                        MessageBox.Show("Добавлен");
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
        private void ClearData2() //метод очищения полей 
        {
            ID2 = 0;
            poln_name.Text = "";
            poln_info.Text = "";
            min.Text = "";
            max.Text = "";
            poln_role.Text = "";

        }
        private void cred_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) //метод заполнения полей значения из таблицы
        {
            ID2 = Convert.ToInt32(cred_view.Rows[e.RowIndex].Cells[0].Value.ToString());
            poln_name.Text = cred_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            poln_info.Text = cred_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            min.Text = cred_view.Rows[e.RowIndex].Cells[3].Value.ToString();
            max.Text = cred_view.Rows[e.RowIndex].Cells[4].Value.ToString();
            //prot_role.Text = prot_view.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void change_poln_Click(object sender, EventArgs e) //изменение полномочия
        {
            try
            { 
                if (poln_name.Text != "" && poln_info.Text != "")
                {
                    this.connection.Open();
                    SQLiteCommand command_1 = new SQLiteCommand("UPDATE credentials SET name = @l1, info = @l2, min_count_ex = @l3,max_count_users = @l4 WHERE id=@l0", connection);
                    SQLiteCommand command_2 = new SQLiteCommand("UPDATE roles_credentials SET role_id = @l4 WHERE credential_id=@l0", connection);


                    command_1.Parameters.AddWithValue("@l0", ID2); //изменение данных полномочия в таблице protocols
                    command_1.Parameters.AddWithValue("@l1", poln_name.Text);
                    command_1.Parameters.AddWithValue("@l2", poln_info.Text);
                    command_1.Parameters.AddWithValue("@l3", Convert.ToInt32(min.Text));
                    command_1.Parameters.AddWithValue("@l4", Convert.ToInt32(max.Text));
                    command_1.ExecuteNonQuery();

                    SQLiteDataAdapter adapter_ = new SQLiteDataAdapter(); //поиск и запись id только что изменёной роли к протоклу
                    DataTable table_ = new DataTable();
                    SQLiteCommand command_ = new SQLiteCommand("SELECT * FROM roles WHERE `role` = @l5", this.connection);
                    command_.Parameters.AddWithValue("@l5", poln_role.Text);
                    adapter_.SelectCommand = command_;
                    adapter_.Fill(table_);
                    SQLiteDataReader reader_ = command_.ExecuteReader();
                    while (reader_.Read())
                    {
                        User_info.role_id_update = $"{reader_["id"]}";
                        break;
                    }
                    reader_.Close();

                    command_2.Parameters.AddWithValue("@l0", ID2); //изменение роли полномочия в таблице roles_credentials
                    command_2.Parameters.AddWithValue("@l4", User_info.role_id_update);
                    command_2.ExecuteNonQuery();

                    MessageBox.Show("Запись успешно обновлена");
                    this.connection.Close();
                    Table_refresh();
                    ClearData2();

                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите запись для обновления");
                }
        }
            catch //(Exception ex)
            {
                MessageBox.Show("Введите число!");
            }


}

        private void delete_poln_Click(object sender, EventArgs e) //удаление полномочия
        {
            this.connection.Open();
            SQLiteCommand _command = new SQLiteCommand("DELETE FROM credentials WHERE id = @l0", connection);
            _command.Parameters.AddWithValue("@l0", ID2);
            _command.ExecuteNonQuery();
            SQLiteCommand _command_ = new SQLiteCommand("DELETE FROM roles_credentials WHERE credential_id = @l0", connection);
            _command_.Parameters.AddWithValue("@l0", ID2);
            _command_.ExecuteNonQuery();
            this.connection.Close();
            MessageBox.Show("Запись успешно удалена");
            Table_refresh();
        }


        private void users_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            DialogResult result = MessageBox.Show(
        "Согласовать запись?",
        "Согласование",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                this.connection.Open();
                int id = Convert.ToInt32(users_view.Rows[e.RowIndex].Cells[0].Value.ToString());
                SQLiteCommand cmd_3 = new SQLiteCommand("SELECT status FROM users WHERE id = @l0", this.connection);
                SQLiteParameter param_3 = new SQLiteParameter("@l0", id);
                cmd_3.Parameters.Add(param_3);
                SQLiteDataReader DR_3 = cmd_3.ExecuteReader();
                while (DR_3.Read())
                {
                    User_info.status_delete = DR_3.GetInt32(0);
                }
                DR_3.Close();
                if (User_info.status_delete == 4)
                {
                    SQLiteCommand _command = new SQLiteCommand("DELETE FROM users WHERE id = @l0", connection);
                    _command.Parameters.AddWithValue("@l0", id);
                    _command.ExecuteNonQuery();
                }
                else
                {
                    SQLiteCommand _command = new SQLiteCommand("UPDATE users SET status=@l1 WHERE id = @l0", connection);
                    _command.Parameters.AddWithValue("@l0", id);
                    _command.Parameters.AddWithValue("@l1", 1);
                    _command.ExecuteNonQuery();
                }
                
                this.connection.Close();
                MessageBox.Show("Запись успешно согласована");
                Table_refresh();
            }
        }
    }

}
