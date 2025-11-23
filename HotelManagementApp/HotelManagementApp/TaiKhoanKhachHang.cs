using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class TaiKhoanKhachHang: Form
    {
        private int _userId;
        public TaiKhoanKhachHang()
        {
            InitializeComponent();
            
        }
        // Constructor mới nhận userId
        public TaiKhoanKhachHang(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }
        private void TaiKhoanKhachHang_Load(object sender, EventArgs e)
        {
            if (_userId > 0)
            {
                LoadUserInfo(_userId);
            }
        }

       private void LoadUserInfo(int id)
        {
            string sql = "SELECT Name, Phone, Email, Account FROM User_infor WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection("Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678"))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        txtName.Text = rd["Name"].ToString();
                        txtPhone.Text = rd["Phone"].ToString();
                        txtEmail.Text = rd["Email"].ToString();

                        // label để hiển thị không đổi
                        label6.Text = "Tên tài khoản: " + rd["Account"].ToString();
                        label2.Text = rd["Account"].ToString();
                    }
                }
            }
        }

        private void UpdateUserInfo()
        {
            string sql = "UPDATE User_infor SET Name = @Name, Phone = @Phone, Email = @Email WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection("Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678"))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Id", _userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // load lại dữ liệu mới
            LoadUserInfo(_userId);
        }


        private void panelRoomsContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = UserSession.CurrentUserId;
                TaiKhoanKhachHang tk = new TaiKhoanKhachHang(id);
                tk.Show();
                this.Hide();
            }
            catch
            {
                TaiKhoanKhachHang tk = new TaiKhoanKhachHang();
                tk.Show();
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button10_Click(object sender, EventArgs e)
        {
            UpdateUserInfo();


        }

        private void button11_Click(object sender, EventArgs e)
        {
            KhachHangDatPhong khachHangDatPhong = new KhachHangDatPhong();
            khachHangDatPhong.Show();
            this.Hide();
        }
    }
}
