using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using static System.Collections.Specialized.BitVector32;

namespace HotelManagementApp
{
    public partial class Log_in : Form
    {
        string encrypted;
        string decryptedConn;

        public Log_in()
        {
            InitializeComponent();
        }

        private void Log_in_Load(object sender, EventArgs e)
        {
            encrypted = File.ReadAllText("conn.txt");
            decryptedConn = AESHelper.Decrypt(encrypted);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tk = textBox1.Text.Trim();
            string mk = textBox2.Text.Trim();
            int userCount = 0;
            Boolean check_acc = false;
            using (SqlConnection connection = new SqlConnection(decryptedConn))
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
                            string sqlGetId = "SELECT Id FROM User_infor WHERE Account = @tk AND Password = @mk";
                            using (SqlCommand cmdId = new SqlCommand(sqlGetId, connection))
                            {
                                cmdId.Parameters.AddWithValue("@tk", tk);
                                cmdId.Parameters.AddWithValue("@mk", mk);

                                int userId = Convert.ToInt32(cmdId.ExecuteScalar());

                                // lưu vào session
                                UserSession.CurrentUserId = userId;
                                UserSession.CurrentUsername = tk;

                                // mở form KhachHangDatPhong
                                KhachHangDatPhong f = new KhachHangDatPhong();
                                f.Show();
                                this.Hide();
                                return;
                            }

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
                                // local admin check (fallback) - also allow admin/admin without DB
                                if ((tk == "admin" && mk == "admin") || (tk == read["TK"].ToString() && mk == read["MK"].ToString()))
                                {
                                    check_acc = true;

                                    // lưu session cho manager
                                    UserSession.CurrentUserId = 0;
                                    UserSession.CurrentUsername = tk;

                                    // open ManagerHome (central manager UI)
                                    var mgrHome = new HotelManagementApp.quanly.ManagerHome();
                                    mgrHome.Show();
                                    this.Hide();

                                    return;
                                }
                            }
                        }
                    }
                    //Staff login
                    string query3 = "SELECT Username, Password, Name FROM Staff WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query3, connection))
                    {
                        command.Parameters.AddWithValue("@Username", tk);
                        command.Parameters.AddWithValue("@Password", mk);

                        using (SqlDataReader read = command.ExecuteReader())
                        {
                            if (read.Read())
                            {
                                check_acc = true;

                                string displayName = read["Name"].ToString();

                                StaffSession.Username = read["Username"].ToString();
                                StaffSession.DisplayName = read["Name"].ToString();
                                //Session.AvatarData = read["avatar"] is DBNull ? null : (byte[])read["avatar"];

                                Staff staffForm = new Staff();
                                staffForm.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
                }
            }
            if (userCount <= 0 && check_acc == false)
            {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}