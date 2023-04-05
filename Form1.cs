using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllAzAlku2
{
    public partial class Form1 : Form
    {
        PictureBox[] taskak;
        public Form1()
        {
            InitializeComponent();
            
        }
        
        private void GombKattintas(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int width = panel1.Width;
            int height = panel1.Height;
            int wCount = width / 140;

            taskak = new PictureBox[23];
            for (int i = 0; i < taskak.Length; i++)
            {
                taskak[i] = new PictureBox();
                taskak[i].Size = new Size(130, 110);
                taskak[i].Location = new Point((i % wCount) * 140, (i / wCount) * 150);
                taskak[i].Image = global::AllAzAlku2.Properties.Resources.taska_zarva;
                taskak[i].Click += GombKattintas;
                taskak[i].SizeMode = PictureBoxSizeMode.StretchImage;
                panel1.Controls.Add(taskak[i]);
            }
        }
    }
}
