using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class Trang_Chu : Form
    {
        public Trang_Chu()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log_in log_in = new Log_in();
            log_in.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sign_in sign_In = new Sign_in();
            sign_In.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log_in log_in = new Log_in();
            log_in.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log_in log_in = new Log_in();
            log_in.Show();
            this.Hide();
        }

        private void Trang_Chu_Load(object sender, EventArgs e)
        {

        }
    }
}
