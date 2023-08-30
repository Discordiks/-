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
    public partial class Setting_champ : Form
    {
        private SQLiteConnection connection;
        public Setting_champ()
        {
            InitializeComponent();
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

            if (DateTime.Now.TimeOfDay >= new TimeSpan(0, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(6, 0, 0))
            {
                hello.Text="Доброй ночи, "+ User_info.user_ima;
            }
            else if (DateTime.Now.TimeOfDay >= new TimeSpan(6, 0, 0) && DateTime.Now.TimeOfDay < new TimeSpan(12, 0, 0))
            {
                hello.Text = "Доброе утро, "+User_info.user_ima;
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

            SQLiteDataAdapter adapter_2 = new SQLiteDataAdapter("SELECT  DISTINCT users.id, users.fio, roles.role, skills.title AS skills_title, users.telephone, users.email FROM users JOIN roles ON roles.id = users.ID_role JOIN statuses ON statuses.id = users.status JOIN skills ON users.skill = skills.id WHERE (users.ID_role = 2 OR users.ID_role = 3 OR users.ID_role = 4 OR users.ID_role = 5) AND championship = " + User_info.id_champ_combo, this.connection);
            DataSet data_2 = new DataSet();
            adapter_2.Fill(data_2);
            expert_view.DataSource = data_2.Tables[0].DefaultView;
            bindingSource2.DataSource = data_2.Tables[0].DefaultView;
            this.connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            this.Table_refresh();
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
    }

}
