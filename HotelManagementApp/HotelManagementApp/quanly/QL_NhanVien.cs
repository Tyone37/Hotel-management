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

namespace HotelManagementApp
{
    public partial class QL_NhanVien: Form
    {
        string encrypted;
        string decryptedConn;

        public QL_NhanVien()
        {
            InitializeComponent();
        }

        private void QL_NhanVien_Load(object sender, EventArgs e)
        {
            encrypted = File.ReadAllText("conn.txt");
            decryptedConn = AESHelper.Decrypt(encrypted);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(decryptedConn))
                {
                    string sql = "SELECT Id, Username, Email, Phone, Name, Password FROM Staff";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                    AddActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ!");
            }
        }

        private void AddActionButtons()
        {
            // Nút Sửa
            if (dataGridView1.Columns.Contains("btnEdit"))
                dataGridView1.Columns.Remove("btnEdit");

            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.Name = "btnEdit";
            btnEdit.HeaderText = "Sửa";
            btnEdit.Text = "✏️";
            btnEdit.UseColumnTextForButtonValue = true;
            btnEdit.Width = 60;

            //Nút Xóa
            if (dataGridView1.Columns.Contains("btnDelete"))
                dataGridView1.Columns.Remove("btnDelete");

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "btnDelete";
            btnDelete.HeaderText = "Xóa";
            btnDelete.Text = "🗑";
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Width = 60;

            dataGridView1.Columns.Add(btnEdit);
            dataGridView1.Columns.Add(btnDelete);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            int id = Convert.ToInt32(
                dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

            // ===== SỬA =====
            if (columnName == "btnEdit")
            {
                string username = dataGridView1.Rows[e.RowIndex]
                                    .Cells["Username"].Value.ToString();
                string email = dataGridView1.Rows[e.RowIndex]
                                    .Cells["Email"].Value.ToString();
                string phone = dataGridView1.Rows[e.RowIndex]
                                    .Cells["Phone"].Value.ToString();
                string name = dataGridView1.Rows[e.RowIndex]
                                    .Cells["Name"].Value.ToString();
                string password = dataGridView1.Rows[e.RowIndex]
                                    .Cells["Password"].Value.ToString();

                Sua_NhanVien frm = new Sua_NhanVien(
                    id,
                    username,
                    email,
                    phone,
                    name,
                    password
                );

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // load lại danh sách sau khi sửa
                }

                return;
            }

            // ===== XÓA =====
            if (columnName == "btnDelete")
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhân viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    DeleteStaff(id);
                    LoadData();
                }
            }
        }

        private void DeleteStaff(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(decryptedConn))
                {
                    conn.Open();
                    string sql = "DELETE FROM Staff WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Xóa thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword = textBox_Search.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData(); // load lại toàn bộ nếu rỗng
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(decryptedConn))
                {
                    conn.Open();

                    string sql;

                    // Nếu nhập số → tìm theo Id
                    if (int.TryParse(keyword, out int id))
                    {
                        sql = "SELECT Id, Username, Email, Phone, Name FROM Staff WHERE Id = @Id";
                    }
                    else // nhập chữ → tìm theo Name
                    {
                        sql = "SELECT Id, Username, Email, Phone, Name FROM Staff WHERE Name LIKE @Name";
                    }

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    if (int.TryParse(keyword, out id))
                        cmd.Parameters.AddWithValue("@Id", id);
                    else
                        cmd.Parameters.AddWithValue("@Name", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    AddActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private bool ValidateAdd()
        {
            if (string.IsNullOrWhiteSpace(textBox_Username.Text))
            {
                MessageBox.Show("Username không được để trống");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox_Email.Text))
            {
                MessageBox.Show("Email không được để trống");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                textBox_Email.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không đúng định dạng");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox_Phone.Text))
            {
                MessageBox.Show("Số điện thoại không được để trống");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                textBox_Phone.Text,
                @"^\d{9,11}$"))
            {
                MessageBox.Show("SĐT phải từ 9–11 số");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox_Name.Text))
            {
                MessageBox.Show("Tên không được để trống");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox_Password.Text))
            {
                MessageBox.Show("Mật khẩu không được để trống");
                return false;
            }

            if (textBox_Password.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải ≥ 6 ký tự");
                return false;
            }

            return true;
        }

        private bool IsDuplicate(string username, string email)
        {
            using (SqlConnection conn = new SqlConnection(decryptedConn))
            {
                conn.Open();
                string sql = @"SELECT COUNT(*) FROM Staff
                       WHERE Username=@Username OR Email=@Email";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (!ValidateAdd())
                return;

            if (IsDuplicate(textBox_Username.Text.Trim(), textBox_Email.Text.Trim()))
            {
                MessageBox.Show("Username hoặc Email đã tồn tại");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(decryptedConn))
                {
                    conn.Open();

                    string sql = @"INSERT INTO Staff
                           (Username, Email, Phone, Name, Password)
                           VALUES
                           (@Username, @Email, @Phone, @Name, @Password)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Username", textBox_Username.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", textBox_Email.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", textBox_Phone.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", textBox_Name.Text.Trim());

                    cmd.Parameters.AddWithValue("@Password", textBox_Password.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thêm nhân viên thành công");
                ClearInput();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void ClearInput()
        {
            textBox_Username.Clear();
            textBox_Email.Clear();
            textBox_Phone.Clear();
            textBox_Name.Clear();
            textBox_Password.Clear();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
    }
}
