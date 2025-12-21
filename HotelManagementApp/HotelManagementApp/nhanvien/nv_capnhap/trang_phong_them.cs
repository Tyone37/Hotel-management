using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_phong_them : Form
    {
        // 🔹 Chuỗi kết nối
        string connStr = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
        string fileAddress = "";

        public trang_phong_them()
        {
            InitializeComponent();
        }

        private void trang_phong_them_Load(object sender, EventArgs e)
        {
            // Hiển thị username
            label2.Text = string.IsNullOrEmpty(StaffSession.Username) ? "Manager" : StaffSession.Username;
        }

        // ===================== ĐĂNG XUẤT =====================
        private void button2_Click(object sender, EventArgs e)
        {
            StaffSession.StaffId = 0;
            StaffSession.Username = null;

            Log_in f = new Log_in();
            f.Show();
            this.Close();
        }

        // ===================== CHỌN ẢNH =====================
        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = Path.GetFileName(open.FileName);
                fileAddress = open.FileName;

                // Hiển thị ảnh trong PictureBox
               // if (pictureBox1 != null)
                   // pictureBox1.Image = Image.FromFile(fileAddress);
            }
        }

        // ===================== THÊM PHÒNG =====================
        private void button6_Click(object sender, EventArgs e)
        {
            string room = textBox1.Text.Trim();
            string priceText = textBox2.Text.Trim();
            string info = textBox4.Text.Trim();

            if (string.IsNullOrEmpty(room) || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(info))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (!decimal.TryParse(priceText, out decimal giaPhong) || giaPhong <= 0)
            {
                MessageBox.Show("Giá phòng không hợp lệ!");
                return;
            }

            if (!File.Exists(fileAddress))
            {
                MessageBox.Show("Vui lòng chọn ảnh phòng hợp lệ!");
                return;
            }
          

            if (StaffSession.StaffId <= 0)
            {
                MessageBox.Show("Chưa có thông tin nhân viên đăng nhập!");
                return;
            }

            try
            {
                byte[] imgBytes = File.ReadAllBytes(fileAddress);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 🔹 Kiểm tra trùng phòng
                    SqlCommand check = new SqlCommand(
                        "SELECT COUNT(*) FROM Hotel_room WHERE Room=@room", conn);
                    check.Parameters.AddWithValue("@room", room);

                    if ((int)check.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Số phòng đã tồn tại!");
                        return;
                    }

                    // 🔹 Thêm phòng (Id để DB tự tăng nếu có IDENTITY)
                    // Giả sử bạn muốn tự tạo Id
                    SqlCommand getMaxId = new SqlCommand("SELECT ISNULL(MAX(Id),0)+1 FROM Hotel_room", conn);
                    int newId = (int)getMaxId.ExecuteScalar();

                    SqlCommand cmd = new SqlCommand(
                        @"INSERT INTO Hotel_room(Id, Room, Price, Information, image)
      VALUES (@id, @room, @price, @info, @img)", conn);

                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.Parameters.AddWithValue("@room", room);
                    cmd.Parameters.AddWithValue("@price", giaPhong);
                    cmd.Parameters.AddWithValue("@info", info);
                    cmd.Parameters.AddWithValue("@img", imgBytes);


                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        // 🔹 Ghi lịch sử
                        GhiLichSu("Thêm", $"Thêm phòng {room} - Giá: {giaPhong}");

                        MessageBox.Show("Thêm phòng thành công!");
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Thêm phòng thất bại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ===================== RESET FORM =====================
        void ResetForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            fileAddress = "";

            if (pictureBox1 != null)
                pictureBox1.Image = null;
        }

        // ===================== GHI LỊCH SỬ =====================
        void GhiLichSu(string hanhDong, string noiDung)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO LichSuHoatDong (MaNV, HanhDong, NoiDung, ThoiGian)
                      VALUES (@MaNV, @HD, @ND, @TG)", conn);

                cmd.Parameters.AddWithValue("@MaNV", StaffSession.StaffId);
                cmd.Parameters.AddWithValue("@HD", hanhDong);
                cmd.Parameters.AddWithValue("@ND", noiDung);
                cmd.Parameters.AddWithValue("@TG", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            trang_phong_them trang_phong_them = new trang_phong_them();
            trang_phong_them.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trang_phong_xoa trang_phong_xoa = new trang_phong_xoa();
            trang_phong_xoa.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trang_phong_sua trang_phong_sua = new trang_phong_sua();
            trang_phong_sua.Show();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tk_nhanvien tkNhanVienForm = new tk_nhanvien();
            tkNhanVienForm.Show();
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nv_cacphong nvCacPhongForm = new nv_cacphong();
            nvCacPhongForm.Show();
            this.Close();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            nv_khachhang nvKhachHangForm = new nv_khachhang();
            nvKhachHangForm.Show();
            this.Close();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            capNhap CapNhapForm = new capNhap();
            CapNhapForm.Show();
            this.Close();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lichsuhoatdong lichsuhoatdong = new lichsuhoatdong();
            lichsuhoatdong.Show();
            this.Close(); // nếu muốn đóng form hiện tại
        }
    }
}
