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

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_kh_them : Form
    {
        string encrypted;
        string decryptedConn;

        public trang_kh_them()
        {
            InitializeComponent();
        }

        private void trang_kh_them_Load(object sender, EventArgs e)
        {
            encrypted = File.ReadAllText("conn.txt");
            decryptedConn = AESHelper.Decrypt(encrypted);

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

        private void label1_Click_1(object sender, EventArgs e)
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            trang_kh_them trang_Kh_Them = new trang_kh_them();
            trang_Kh_Them.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trang_kh_xoa trang_Kh_Xoa = new trang_kh_xoa();
            trang_Kh_Xoa.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trang_kh_sua trang_Kh_Sua = new trang_kh_sua();
            trang_Kh_Sua.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text.ToString();
            string sdt = textBox3.Text.ToString();
            string email = textBox2.Text.ToString();
            string acc = textBox1.Text.ToString();
            string pass = textBox5.Text.ToString();
            string confirm_pass = textBox6.Text.ToString();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirm_pass))
            {
                MessageBox.Show("Không được để trống thông tin");
                return;
            }
            else if (pass != confirm_pass)
            {
                MessageBox.Show("Mật khẩu xác nhận hoặc mật khẩu sai");
                return;
            }

            if (!Regex.IsMatch(name, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                MessageBox.Show("Họ tên không hợp lệ. Không được chứa số hoặc ký tự đặc biệt.");
                return;
            }

            if (!((sdt.Length) == 10))
            {
                MessageBox.Show("Số điện thoại phải đủ 10 số");
                return;
            }

            if (sdt.All(char.IsDigit) == false)
            {
                MessageBox.Show("Số điện thoại phải là số");
                return;
            }

            if (!email.Contains("@gmail.com"))
            {
                MessageBox.Show("Email phải theo định dạng @gmail.com");
                return;
            }

            if ((pass.Length) < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(decryptedConn))
                {
                    connection.Open();

                    string query = "INSERT INTO User_infor (Name, Phone, Email, Account, Password) VALUES (@name, @sdt, @email, @acc, @pass)";

                    string query1 = "SELECT COUNT(1) FROM User_infor WHERE Account = @acc";

                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@acc", acc);
                        int user_count = (int)command.ExecuteScalar();

                        if (user_count > 0)
                        {
                            DialogResult check_chuyen_trang = MessageBox.Show(
                                "Tài khoản này đã có người đăng ký, bạn có muốn chuyển đến trang đăng nhập?",
                                "Xác Nhận",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question
                            );
                            if (check_chuyen_trang == DialogResult.OK)
                            {
                                Log_in log_in = new Log_in();
                                log_in.Show();
                                this.Hide();
                                return;
                            }
                            else if (check_chuyen_trang == DialogResult.Cancel)
                            {
                                return;
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@sdt", sdt);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@acc", acc);
                        command.Parameters.AddWithValue("@pass", pass);
                        int rowsAdd = command.ExecuteNonQuery();

                        if (rowsAdd > 0)
                        {
                            MessageBox.Show("Đã đăng ký thành công");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            textBox6.Text = "";
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
