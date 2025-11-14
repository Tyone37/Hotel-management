using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HotelManagementApp
{
    public partial class Log_in : Form
    {
        string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
        
        public Log_in()
        {
            InitializeComponent();
        }

        private void Log_in_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tk = textBox1.Text.ToString();
            string mk = textBox2.Text.ToString();
            int userCount = 0;
            Boolean check_acc = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query1 = "SELECT TK, MK FROM Manager_Hotel WHERE TK = @taiKhoan AND MK = @matKhau";
                    string query2 = "SELECT COUNT(1) FROM User_infor WHERE Account = @taiKhoan AND Password = @matKhau";
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@taiKhoan", tk);
                        command.Parameters.AddWithValue("@matKhau", mk);

                        userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            // ✅ Khi người dùng đăng nhập đúng, chuyển sang form KhachHangDatPhong
                            KhachHangDatPhong khachHangDatPhong = new KhachHangDatPhong();
                            khachHangDatPhong.Show();
                            this.Hide(); 
                        }
                    }
                    using (SqlCommand command = new SqlCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@taiKhoan", tk);
                        command.Parameters.AddWithValue("@matKhau", mk);

                        using (SqlDataReader read = command.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                if (tk == "admin" && mk == "admin")
                                {
                                    check_acc = true;
                                    Manager manager = new Manager();
                                    manager.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    check_acc = true;
                                    Staff staff = new Staff();
                                    staff.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
                }
            }
            if (userCount <= 0 && check_acc == false) {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            check_acc = false;
            userCount = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sign_in sign_In = new Sign_in();
            sign_In.Show();
            this.Hide();
        }
    }
}
