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
    public partial class Menu_organ : Form
    {
        private SQLiteConnection connection;
        MO_settings_champ f2;
        public Menu_organ()
        {
            InitializeComponent();

            
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
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM competitions", this.connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0].DefaultView;
            //bindingSource1.DataSource = data.Tables[0].DefaultView;
            this.connection.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            dataGridView1.Visible = false;
            this.Table_refresh();
        }

        private void sett_chemp_Click(object sender, EventArgs e)
        {
            f2 = new MO_settings_champ();
            f2.Show();
        }

        
    }

}
