using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace HotelManagementApp
{
    public partial class tk_nhanvien : Form
    {
        public tk_nhanvien()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //taskbar link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tk_nhanvien tkNhanVienForm = new tk_nhanvien();
            tkNhanVienForm.Show();
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

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

        private void label1_Click(object sender, EventArgs e)
        {

            Staff staffForm = new Staff();
            staffForm.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void LoadStaffInfo()
        {
            if (string.IsNullOrEmpty(StaffSession.Username)) return;

            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT Username, Email, Phone, Name, Attendance, salary, avatar FROM Staff WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", StaffSession.Username);

                        using (SqlDataReader read = command.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                label7.Text = read["Name"].ToString();

                                label10.Text = read["Phone"].ToString();

                                label11.Text = read["Email"].ToString();

                                label15.Text = read["Username"].ToString();

                                object attendanceValue = read["Attendance"];
                                if (attendanceValue == DBNull.Value)
                                {
                                    label13.Text = "XX/24";
                                }
                                else
                                {
                                    label13.Text = $"{attendanceValue.ToString()}/24";
                                }

                                object salaryValue = read["salary"];
                                if (salaryValue == DBNull.Value)
                                {
                                    label14.Text = "N/A";
                                }
                                else
                                {
                                    // Định dạng X.XXX.XXX VND
                                    long salary = Convert.ToInt64(salaryValue);

                                    CultureInfo viCulture = new CultureInfo("vi-VN");
                                    label14.Text = salary.ToString("N0", viCulture) + " VND";
                                }

                                object avatarValue = read["avatar"];

                                if (avatarValue != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])avatarValue;

                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        pictureBox4.Image = Image.FromStream(ms);
                                    }
                                }
                                else
                                {
                                    ImageHelper.SetAvatarToPictureBox(pictureBox4);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tk_nhanvien_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StaffSession.DisplayName))
            {
                label2.Text = StaffSession.DisplayName;
            }
            else
            {
                label2.Text = "Tên người dùng";
            }
            ImageHelper.SetAvatarToPictureBox(pictureBox4);

            LoadStaffInfo();
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Mở cửa sổ Explorer để chọn ảnh
            OpenFileDialog openFd = new OpenFileDialog();
            openFd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            openFd.FilterIndex = 1;
            openFd.RestoreDirectory = true;
            openFd.Title = "Chọn ảnh đại diện";

            if (openFd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imagePath = openFd.FileName;
                    Image selectedImage = Image.FromFile(imagePath);

                    pictureBox4.Image = selectedImage;
                    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Lưu ảnh vào cơ sở dữ liệu
                    SaveImageToDatabase(imagePath);

                    MessageBox.Show("Ảnh đại diện đã được cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi chọn hoặc lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveImageToDatabase(string imagePath)
        {
            byte[] imageData;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                imageData = new byte[fs.Length];
                fs.Read(imageData, 0, Convert.ToInt32(fs.Length));
            }

            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            string username = StaffSession.Username;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Staff SET avatar = @avatar WHERE Username = @username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@avatar", SqlDbType.Image).Value = imageData;
                    command.Parameters.AddWithValue("@username", username);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }



        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            StaffSession.Username = null;
            StaffSession.DisplayName = null;

            Log_in loginForm = new Log_in();
            loginForm.Show();
            this.Close();
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                pictureBox4.Image = Image.FromFile(filePath);

                SaveImageToDatabase(filePath);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Staff staffForm = new Staff();
            staffForm.Show();
            this.Close();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
 
    }
}
