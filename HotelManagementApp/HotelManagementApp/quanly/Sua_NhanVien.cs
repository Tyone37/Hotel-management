using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HotelManagementApp
{
    public partial class Sua_NhanVien: Form
    {
        private int _id;
        string encrypted;
        string decryptedConn;
        public Sua_NhanVien(int id,
            string username,
            string email,
            string phone,
            string name,
            string password)
        {
            InitializeComponent();
            encrypted = File.ReadAllText("conn.txt");
            decryptedConn = AESHelper.Decrypt(encrypted);

            _id = id;
            textBox_ID.Text = _id.ToString();
            textBox_Email.Text = email;
            textBox_Name.Text = name;
            textBox_Password.Text = password;
            textBox_Phone.Text = phone;
            textBox_Username.Text = username;

        }

        private bool ValidateInput()
        {
            // Username
            if (string.IsNullOrWhiteSpace(textBox_Username.Text))
            {
                MessageBox.Show("Username không được để trống");
                textBox_Username.Focus();
                return false;
            }

            if (textBox_Username.Text.Length < 3)
            {
                MessageBox.Show("Username phải từ 3 ký tự trở lên");
                textBox_Username.Focus();
                return false;
            }

            // Email
            if (string.IsNullOrWhiteSpace(textBox_Email.Text))
            {
                MessageBox.Show("Email không được để trống");
                textBox_Email.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                textBox_Email.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không đúng định dạng");
                textBox_Email.Focus();
                return false;
            }

            // Phone
            if (string.IsNullOrWhiteSpace(textBox_Phone.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống");
                textBox_Phone.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                textBox_Phone.Text,
                @"^\d{9,11}$"))
            {
                MessageBox.Show("Số điện thoại phải từ 9–11 chữ số");
                textBox_Phone.Focus();
                return false;
            }

            // Name
            if (string.IsNullOrWhiteSpace(textBox_Name.Text))
            {
                MessageBox.Show("Tên không được để trống");
                textBox_Name.Focus();
                return false;
            }

            // Password
            if (!string.IsNullOrWhiteSpace(textBox_Password.Text))
            {
                if (textBox_Password.Text.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên");
                    textBox_Password.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(decryptedConn))
                {
                    conn.Open();
                    string sql = @"UPDATE Staff
                               SET Username=@Username,
                                   Email=@Email,
                                   Phone=@Phone,
                                   Name=@Name,
                                    Password=@Password
                               WHERE Id=@Id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", _id);
                    cmd.Parameters.AddWithValue("@Username", textBox_Username.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
                    cmd.Parameters.AddWithValue("@Phone", textBox_Phone.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox_Name.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox_Password.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Cập nhật thành công");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
