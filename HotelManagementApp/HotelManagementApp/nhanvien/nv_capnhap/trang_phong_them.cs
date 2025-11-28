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
            string room = textBox1.Text.ToString();
            string price = textBox2.Text.ToString();
            string information = textBox4.Text.ToString();

            if (string.IsNullOrEmpty(room) || string.IsNullOrEmpty(price) || string.IsNullOrEmpty(fileAddress) || string.IsNullOrEmpty(information))
            {
                MessageBox.Show("Không được để trống thông tin");
                return;
            }

            try
            {
                byte[] imageBytes = null;
                using (FileStream fs = new FileStream(fileAddress, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        imageBytes = br.ReadBytes((int)fs.Length);
                    }
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Hotel_room (Room, Price, Information, image) VALUES (@room, @price, @information, @image)";

                    string query1 = "SELECT * FROM Hotel_room WHERE Room = @room";

                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@room", room);
                        object check = command.ExecuteScalar();

                        if (check != null)
                        {
                            DialogResult warning = MessageBox.Show(
                                "Số phòng đã tồn tại, bạn có muốn xem lại danh sách phòng Không?",
                                "Xác Nhận",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question
                            );
                            if (warning == DialogResult.OK)
                            {
                                nv_cacphong nvCacPhongForm = new nv_cacphong();
                                nvCacPhongForm.Show();
                                this.Hide();
                                return;
                            }
                            else
                                return;
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@room", room);
                        command.Parameters.AddWithValue("@price", price);
                        command.Parameters.AddWithValue("@image", imageBytes);
                        command.Parameters.AddWithValue("@information", information);
                        int rowsAdd = command.ExecuteNonQuery();

                        if (rowsAdd > 0)
                        {
                            MessageBox.Show("Đã đăng ký thành công");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            fileAddress = "";
                        }
                        else
                        {
                            MessageBox.Show("Đăng ký thất bại");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu");
                return;
            }
        }


    }
}
