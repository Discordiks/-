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
    public partial class Menu_player : Form
    {
        private SQLiteConnection connection;
        Avtorizacia f1;
        public Menu_player()
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

            list_player.Visible = true;
            list_protocol.Visible = true;
            player_view.Visible = false;
            prot_view.Visible = false;
        }
        private void Table_refresh()
        {
            this.connection.Open();
            SQLiteDataAdapter adapter_1 = new SQLiteDataAdapter("SELECT users.id, users.fio FROM users WHERE users.championship = " + User_info.user_id_comp, this.connection);
            DataSet data_1 = new DataSet();
            adapter_1.Fill(data_1);
            player_view.DataSource = data_1.Tables[0].DefaultView; //вывод участников в таблицу
            SQLiteDataAdapter adapter_2 = new SQLiteDataAdapter("SELECT DISTINCT users_signs.id, users.fio, protocols.title, signs.sign FROM users JOIN users_signs ON users_signs.user_id = users.id JOIN signs ON signs.id = users_signs.sign_id JOIN protocols ON protocols.id = users_signs.protocol_id WHERE users.id = " + User_info.user_id, this.connection);
            DataSet data_2 = new DataSet();
            adapter_2.Fill(data_2);
            prot_view.DataSource = data_2.Tables[0].DefaultView; //вывод протоколов в таблицу
            this.connection.Close();
        }
        private void Menu_player_Load(object sender, EventArgs e)
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
            this.connection.Close();
            Table_refresh();
        }

        private void list_player_Click(object sender, EventArgs e)
        {
            list_player.Visible = false;
            list_protocol.Visible = false;
            player_view.Visible = true;
            prot_view.Visible = false;
        }

        private void list_protocol_Click(object sender, EventArgs e)
        {
            list_player.Visible = false;
            list_protocol.Visible = false;
            player_view.Visible = false;
            prot_view.Visible = true;
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (list_player.Visible == true && list_protocol.Visible == true)
            {
                f1 = new Avtorizacia();
                f1.Show();
                this.Hide();
            }
            else
            {
                list_player.Visible = true;
                list_protocol.Visible = true;
                player_view.Visible = false;
                prot_view.Visible = false;
            }

        }

        private void prot_view_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(prot_view.Rows[e.RowIndex].Cells[0].Value.ToString());
            string f = prot_view.Rows[e.RowIndex].Cells[1].Value.ToString();
            string pr = prot_view.Rows[e.RowIndex].Cells[2].Value.ToString();
            string st = prot_view.Rows[e.RowIndex].Cells[3].Value.ToString();
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
