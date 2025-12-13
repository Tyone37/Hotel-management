using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_phong_them : Form
    {
        string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
        string fileAddress = "";

        public trang_phong_them()
        {
            InitializeComponent();
        }

        private void trang_phong_them_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StaffSession.DisplayName))
            {
                label2.Text = StaffSession.DisplayName;
            }
            else
            {
                label2.Text = "Tên người dùng";
            }
            ImageHelper.SetAvatarToPictureBox(pictureBox2);
        }

        public static class ImageHelper
        {
            public static void SetAvatarToPictureBox(PictureBox pbx)
            {
                if (StaffSession.AvatarData != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(StaffSession.AvatarData))
                        {
                            pbx.Image = Image.FromStream(ms);
                            pbx.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch (Exception)
                    {
                        pbx.Image = null;
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            StaffSession.Username = null;
            StaffSession.DisplayName = null;

            Log_in loginForm = new Log_in();
            loginForm.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Staff staffForm = new Staff();
            staffForm.Show();
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

        private void button5_Click(object sender, EventArgs e)
        {
            trang_phong_sua trangPhongSuaForm = new trang_phong_sua();
            trangPhongSuaForm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trang_phong_xoa trangPhongXoaForm = new trang_phong_xoa();
            trangPhongXoaForm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trang_phong_them trangPhongThemForm = new trang_phong_them();
            trangPhongThemForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log_in loginForm = new Log_in();
            loginForm.Show();
            this.Close();
        }

 

        private void label1_Click(object sender, EventArgs e)
        {
            Staff staffForm = new Staff();
            staffForm.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";

            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = System.IO.Path.GetFileName(open.FileName);
                fileAddress = open.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string room = textBox1.Text.Trim();
            string price = textBox2.Text.Trim();
            string information = textBox4.Text.Trim();

            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrEmpty(room) || string.IsNullOrEmpty(price) ||
                string.IsNullOrEmpty(information) || string.IsNullOrEmpty(fileAddress))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin và chọn ảnh!");
                return;
            }

            // Kiểm tra giá phòng có phải là số không
            if (!decimal.TryParse(price, out decimal giaPhong) || giaPhong <= 0)
            {
                MessageBox.Show("Giá phòng phải là số và lớn hơn 0!");
                return;
            }

            // Kiểm tra file ảnh có tồn tại không
            if (!File.Exists(fileAddress))
            {
                MessageBox.Show("File ảnh không tồn tại!");
                return;
            }

            try
            {
                // Đọc file ảnh thành mảng byte
                byte[] imageBytes = File.ReadAllBytes(fileAddress);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra số phòng đã tồn tại chưa
                    string queryCheck = "SELECT COUNT(*) FROM Hotel_room WHERE Room = @room";
                    using (SqlCommand cmd = new SqlCommand(queryCheck, connection))
                    {
                        cmd.Parameters.AddWithValue("@room", room);

                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Số phòng đã tồn tại!");
                            return;
                        }
                    }

                    
string getMaxIdQuery = "SELECT ISNULL(MAX(Id), 0) + 1 FROM Hotel_room";
                    int newId = 1;
                    using (SqlCommand getIdCmd = new SqlCommand(getMaxIdQuery, connection))
                    {
                        newId = (int)getIdCmd.ExecuteScalar();
                    }

                    // Thêm phòng mới, có Id
                    string queryInsert = "INSERT INTO Hotel_room (Id, Room, Price, Information, image) VALUES (@id, @room, @price, @info, @img)";
                    using (SqlCommand cmd = new SqlCommand(queryInsert, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", newId);
                        cmd.Parameters.AddWithValue("@room", room);
                        cmd.Parameters.AddWithValue("@price", giaPhong);
                        cmd.Parameters.AddWithValue("@info", information);
                        cmd.Parameters.AddWithValue("@img", imageBytes);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Thêm phòng thành công!");
                            // Reset form
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            fileAddress = "";
                        }
                        else
                        {
                            MessageBox.Show("Thêm phòng thất bại!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }



    }
}
