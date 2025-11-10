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
    public partial class Sign_in : Form
    {
        string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
        public Sign_in()
        {
            InitializeComponent();
        }

        private void Sign_in_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log_in log_in = new Log_in();
            log_in.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.ToString();
            string sdt = textBox2.Text.ToString();
            string email = textBox3.Text.ToString();
            string acc = textBox4.Text.ToString();
            string pass = textBox5.Text.ToString();
            string confirm_pass = textBox6.Text.ToString();

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(acc) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirm_pass))
            {
                MessageBox.Show("Không được để trống thông tin");
                return;
            }
            else if(pass != confirm_pass)
            {
                MessageBox.Show("Mật khẩu xác nhận hoặc mật khẩu sai");
                return;
            }

            if(sdt.All(char.IsDigit) == false)
            {
                MessageBox.Show("Số điện thoại phải là số");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                            else if(check_chuyen_trang == DialogResult.Cancel)
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
