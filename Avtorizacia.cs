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
        Form1 f2;
        public Avtorizacia()
        {
            InitializeComponent();
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
            //bindingSource1.DataSource = data.Tables[0].DefaultView;
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Tes");
                f2 = new Form1();
                f2.Show(); 
            }
            else
            {
                MessageBox.Show("No");
            }
                
            this.connection.Close();

        }
        private void Table_refresh()
        {
            this.connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM users", this.connection);
            DataSet data = new DataSet();
            adapter.Fill(data);
            dataGridView1.DataSource = data.Tables[0].DefaultView;
            //bindingSource1.DataSource = data.Tables[0].DefaultView;
            this.connection.Close();
        }

        private void Avtorizacia_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection("Data Source=b.db");
            this.Table_refresh();
        }
    }
}
