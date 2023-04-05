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
    public partial class AjanlatDialog : Form
    {
        public AjanlatDialog()
        {
            InitializeComponent();
        }

        private void AjanlatDialog_Load(object sender, EventArgs e)
        {
            label1.Text = Form1.offer;
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            Form1.JatekVegeAjanlatElfogad();
            this.Close();
        }
    }
}
