using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_kh_sua : Form
    {
        public trang_kh_sua()
        {
            InitializeComponent();
        }

        private void trang_kh_sua_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session.DisplayName))
            {
                label2.Text = Session.DisplayName;
            }
            else
            {
                label2.Text = "Tên người dùng";
            }
            ImageHelper.SetAvatarToPictureBox(pictureBox2);

            if (!string.IsNullOrEmpty(Session.DisplayName))
            {
                label2.Text = Session.DisplayName;
            }
            else
            {
                label2.Text = "Tên người dùng";
            }
            ImageHelper.SetAvatarToPictureBox(pictureBox2);

            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            string query = "SELECT Name, Phone, Email, Account, Password FROM User_infor";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


                    DataTable dt = new DataTable();


                    adapter.Fill(dt);


                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        public static class ImageHelper
        {
            public static void SetAvatarToPictureBox(PictureBox pbx)
            {
                if (Session.AvatarData != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(Session.AvatarData))
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

        private void button2_Click(object sender, EventArgs e)
        {
            Session.Username = null;
            Session.DisplayName = null;

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
        private void pictureBox3_Click(object sender, EventArgs e)
        {

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

        private void button7_Click(object sender, EventArgs e)
        {
            string infor_searching = textBox6.Text.ToString();

            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            string query = "SELECT Name, Phone, Email, Account, Password FROM User_infor where Name = @infor_searching OR Phone = @infor_searching OR Email = @infor_searching OR Account = @infor_searching";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    adapter.SelectCommand.Parameters.AddWithValue("@infor_searching", infor_searching);

                    DataTable dt = new DataTable();


                    adapter.Fill(dt);


                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
