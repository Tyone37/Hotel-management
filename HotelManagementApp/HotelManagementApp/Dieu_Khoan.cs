using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class Dieu_Khoan : Form
    {
        public Dieu_Khoan()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dieu_Khoan_Load(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "Dieu_Khoan.txt");

            if (File.Exists(filePath))
            {
                richTextBox1.Text = File.ReadAllText(filePath);
            }
            else
            {
                richTextBox1.Text = "Không tìm thấy nội dung điều khoản.";
            }
        }
    }
}
