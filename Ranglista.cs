using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AllAzAlku2
{
    public partial class Ranglista : Form
    {
        MySqlConnection Conn;
        public Ranglista()
        {
            InitializeComponent();
            Conn = Connect.InitDB();
        }

        private void Ranglista_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM `ranglista` ORDER BY `ranglista`.`penz` DESC limit 10";
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("nev", "Nev");
            dataGridView1.Columns.Add("penz", "Penz");
            while (rdr.Read())
            {
                dataGridView1.Rows.Add(rdr[0], rdr[1]);
            }
            rdr.Close();
        }
    }
}
