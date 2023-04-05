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
    public partial class Form1 : Form
    {
        PictureBox[] taskak;
        Label[] osszegek;
        Dictionary<string, int> taskaTartalom;
        int huzasok;
        string sajatTaska;

        MySqlConnection Conn;

        public Form1()
        {
            InitializeComponent();
            Conn = Connect.InitDB();
            taskaTartalom = new Dictionary<string, int>();
            huzasok = 0;
        }
        
        private void GombKattintas(object sender, EventArgs e)
        {
            if (huzasok == 0)
            {
                sajatTaska = ((PictureBox)sender).Name;
                ((PictureBox)sender).Click -= GombKattintas;
                ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
            }
            /*if (sender is PictureBox)
            {
                MessageBox.Show(((PictureBox)sender).Name + Environment.NewLine + taskaTartalom[((PictureBox)sender).Name]);
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void újJátékIndításaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kezdes();
        }

        private void Kezdes()
        {
            int width = panel1.Width;
            int height = panel1.Height;
            int wCount = width / 140;

            taskak = new PictureBox[23];
            osszegek = new Label[23];

            string query = "call pr_TaskaOsszeg()";
            MySqlCommand cmd = new MySqlCommand(query, Conn);
            MySqlDataReader reader = cmd.ExecuteReader();


            for (int i = 0; i < taskak.Length; i++)
            {
                taskak[i] = new PictureBox();
                taskak[i].Size = new Size(130, 110);
                taskak[i].Location = new Point((i % wCount) * 140, (i / wCount) * 150);
                taskak[i].Image = global::AllAzAlku2.Properties.Resources.taska_zarva;
                taskak[i].Click += GombKattintas;
                taskak[i].Name = $"taska{i + 1}";
                taskak[i].SizeMode = PictureBoxSizeMode.StretchImage;
                panel1.Controls.Add(taskak[i]);

                osszegek[i] = new Label();
                osszegek[i].AutoSize = false;
                osszegek[i].Size = new Size(130, 40);
                osszegek[i].Location = new Point((i % wCount) * 140, (i / wCount) * 150 + 110);
                osszegek[i].Text = "";
                osszegek[i].TextAlign = ContentAlignment.MiddleCenter;
                panel1.Controls.Add(osszegek[i]);
            }

            int szamlalo = 1;

            while (reader.Read() && szamlalo <= taskak.Length)
            {
                taskaTartalom.Add(taskak[szamlalo - 1].Name, reader.GetInt32("osszeg"));
                szamlalo++;
            }

            reader.Close();
        }
    }
}
