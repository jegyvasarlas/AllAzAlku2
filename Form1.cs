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
using System.Diagnostics;
using System.IO;

namespace AllAzAlku2
{
    public partial class Form1 : Form
    {
        static PictureBox[] taskak;
        static Label[] osszegek;
        static Label[] hatter;
        static Dictionary<string, int> taskaTartalom;
        public static int huzasok;
        static string sajatTaska;
        int kinyitottTaskakbanLevoOsszesPenz;
        public static string offer;
        public static int offerPenz;
        public static int sajatTaskaOsszege;

        MySqlConnection Conn;

        public Form1()
        {
            InitializeComponent();
            Conn = Connect.InitDB();
            taskaTartalom = new Dictionary<string, int>();
            huzasok = kinyitottTaskakbanLevoOsszesPenz = 0;
      ;
        }
        
        private void GombKattintas(object sender, EventArgs e)
        {
            if (huzasok == 0)
            {
                sajatTaska = ((PictureBox)sender).Name;
                ((PictureBox)sender).Click -= GombKattintas;
                int taskaSzama = Convert.ToInt32(((PictureBox)sender).Name.Substring(5));
                hatter[taskaSzama - 1].BackColor = Color.Red;
                osszegek[taskaSzama - 1].BackColor = Color.Red;
                huzasok++;
                jatekinfo.Text = "Valassz 5 db taskat!";

            }
            else if (huzasok > 0 && huzasok<=6)
            {
                ((PictureBox)sender).Image = global::AllAzAlku2.Properties.Resources.taska_nyitva;
                ((PictureBox)sender).Click -= GombKattintas;
                osszegek[Convert.ToInt32(((PictureBox)sender).Name.Substring(5)) - 1].Text = taskaTartalom[((PictureBox)sender).Name].ToString();
                huzasok++;
                string kivalasztottTaska = ((PictureBox)sender).Name;
                int kivalasztottTaskabanLevoPenz = taskaTartalom[kivalasztottTaska];
                kinyitottTaskakbanLevoOsszesPenz += kivalasztottTaskabanLevoPenz;
            }
            if (huzasok == 6)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 3 db taskat!";
            }
            else if (huzasok >= 7)
            {
                ((PictureBox)sender).Image = global::AllAzAlku2.Properties.Resources.taska_nyitva;
                ((PictureBox)sender).Click -= GombKattintas;
                osszegek[Convert.ToInt32(((PictureBox)sender).Name.Substring(5)) - 1].Text = taskaTartalom[((PictureBox)sender).Name].ToString();
                huzasok++;
                string kivalasztottTaska = ((PictureBox)sender).Name;
                int kivalasztottTaskabanLevoPenz = taskaTartalom[kivalasztottTaska];
                kinyitottTaskakbanLevoOsszesPenz += kivalasztottTaskabanLevoPenz;
            }
            if (huzasok == 10)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 3 db taskat!";
            }
            if (huzasok == 13)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 3 db taskat!";
            }
            if (huzasok == 16)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 2 db taskat!";
            }
            if (huzasok == 18)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 2 db taskat!";
            }
            if (huzasok == 20)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 2 db taskat!";
            }
            if (huzasok == 22)
            {
                jatekinfo.Text = "Nezd meg az ajanlatot!";
                Ajanlo();
                jatekinfo.Text = "Valassz 2 db taskat!";
            }
            if (huzasok == 24)
            {
                JatekVegeAjanlatElfogad();
            }
        }


        private void Ajanlo()
        {
            int osszeg = taskaTartalom.Select(x => x.Value).Sum();
            double ajanlat = Math.Round(((osszeg - kinyitottTaskakbanLevoOsszesPenz) / Math.Pow(huzasok - 1, 2)), 0);
            offer = $"Az ajanlat: {ajanlat} Ft";
            offerPenz = Convert.ToInt32(ajanlat);
            AjanlatDialog ajanlatDialog = new AjanlatDialog();
            ajanlatDialog.ShowDialog();
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
            hatter = new Label[23];

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
                osszegek[i].Text = $"{i+1}";
                osszegek[i].TextAlign = ContentAlignment.MiddleCenter;
                panel1.Controls.Add(osszegek[i]);

                hatter[i] = new Label();
                hatter[i].Size = new Size(136, 150);
                hatter[i].Location = new Point((i % wCount) * 140 - 3, (i / wCount) * 150 - 3);
                hatter[i].BackColor = Color.Transparent;
                hatter[i].Name = $"hatter{i + 1}";
                panel1.Controls.Add(hatter[i]);

                taskak[i].BringToFront();
                osszegek[i].BringToFront();

            }

            int szamlalo = 1;

            while (reader.Read() && szamlalo <= taskak.Length)
            {
                taskaTartalom.Add(taskak[szamlalo - 1].Name, reader.GetInt32("osszeg"));
                szamlalo++;
            }

            reader.Close();

            jatekinfo.Text = "Valassz egy taskat...";
            újJátékIndításaToolStripMenuItem.Enabled = false;
        }

        public static void JatekVegeAjanlatElfogad()
        {
            MessageBox.Show($"Nyertel {offerPenz} Ft-ot!");
            Process.Start(@"C:\Users\admin\Desktop\AllAzAlku2\bin\Debug\AllAzAlku2.exe");
            Application.Exit();
        }

        public static void JatekVegeNyitas()
        {
            MessageBox.Show($"Nyertel Ft-ot!");
            Process.Start(@"C:\Users\admin\Desktop\AllAzAlku2\bin\Debug\AllAzAlku2.exe");
            Application.Exit();
        }
    }
}
