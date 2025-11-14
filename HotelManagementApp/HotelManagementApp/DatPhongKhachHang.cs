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
    public partial class DatPhongKhachHang: Form
    {
        public DatPhongKhachHang()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            KhachHangDatPhong khachHangDatPhong = new KhachHangDatPhong();
            khachHangDatPhong.Show();
            this.Hide();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThanhToanKhachHang thanhToanKhachHang = new ThanhToanKhachHang();
            thanhToanKhachHang.Show();
            this.Hide();
        }
    }
}
