using System;
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
    public partial class LichSuGiaoDichKhachHang: Form
    {
        public LichSuGiaoDichKhachHang()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            TaiKhoanKhachHang taiKhoanKhachHang = new TaiKhoanKhachHang();
            taiKhoanKhachHang.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TinhTrangPhongKhachHang tinhTrangPhongKhachHang = new TinhTrangPhongKhachHang();
            tinhTrangPhongKhachHang.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DatPhongKhachHang datPhongKhachHang = new DatPhongKhachHang();
            datPhongKhachHang.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LichSuGiaoDichKhachHang lichSuGiaoDichKhachHang = new LichSuGiaoDichKhachHang();
            lichSuGiaoDichKhachHang.Show();
            this.Hide();
        }
    }
}
