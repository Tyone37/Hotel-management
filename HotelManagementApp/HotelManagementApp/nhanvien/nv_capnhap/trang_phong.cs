using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_phong : Form
    {
        string connectionString =
    "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";

        public trang_phong()
        {
            InitializeComponent();
            this.Load += trang_phong_Load; // 🔥 ĐẢM BẢO LOAD
        }

        // ================= LOAD FORM =================
        private void trang_phong_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StaffSession.DisplayName))
                label2.Text = StaffSession.DisplayName;
            else
                label2.Text = "Tên người dùng";

            ImageHelper.SetAvatarToPictureBox(pictureBox2);

            void LoadAllKhachHang()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sql = "SELECT * FROM dbo.User_infor";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            // 🔥 LOAD DỮ LIỆU BAN ĐẦU
        }

        // ================= LOAD TOÀN BỘ KHÁCH HÀNG =================
        void LoadAllKhachHang()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM KhachHang";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        // ================= BUTTON TÌM KIẾM =================
        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT * FROM dbo.User_infor
               WHERe Name LIKE @kw
                  OR Email LIKE @kw
                  OR Phone LIKE @kw";
                    ;


                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }


        // ================= TÌM KHI GÕ =================
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        // ================= CÁC BUTTON KHÁC (GIỮ NGUYÊN) =================
        private void button5_Click(object sender, EventArgs e)
        {
            new trang_phong_sua().Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new trang_phong_xoa().Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new trang_phong_them().Show();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new tk_nhanvien().Show();
            this.Close();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new nv_cacphong().Show();
            this.Close();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new nv_khachhang().Show();
            this.Close();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new capNhap().Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffSession.Username = null;
            StaffSession.DisplayName = null;
            new Log_in().Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new Staff().Show();
            this.Close();
        }

        // ================= AVATAR =================
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
                    catch
                    {
                        pbx.Image = null;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
