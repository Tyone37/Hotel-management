using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    public partial class UC_QuanLyNhanVien : UserControl
    {
        private KetNoi kn = new KetNoi();

        public UC_QuanLyNhanVien()
        {
            InitializeComponent();
            LoadStaff();
            // Khi người dùng double-click một hàng sẽ hiển thị dịch vụ và khách hàng liên quan
            this.dgvStaff.CellDoubleClick += DgvStaff_CellDoubleClick;
        }

        private void LoadStaff()
        {
            try
            {
                var dt = kn.LayDuLieu("SELECT Id, Username, Name, Email, Phone FROM Staff ORDER BY Username");
                dgvStaff.DataSource = dt;
                if (dgvStaff.Columns.Contains("Id")) dgvStaff.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string plain)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plain));
                var sb = new StringBuilder();
                foreach (var b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string pwd = txtPassword.Text;
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("Email và mật khẩu là bắt buộc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = kn.LayKetNoi())
                {
                    conn.Open();
                    using (var chk = new SqlCommand("SELECT COUNT(1) FROM Staff WHERE Email=@em OR Username=@em", conn))
                    {
                        chk.Parameters.AddWithValue("@em", email);
                        if ((int)chk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Email/Username đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string hashed = HashPassword(pwd);
                    using (var cmd = new SqlCommand("INSERT INTO Staff (Username, Password, Email, Phone) VALUES (@u,@p,@e,@ph)", conn))
                    {
                        cmd.Parameters.AddWithValue("@u", email);
                        cmd.Parameters.AddWithValue("@p", hashed);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@ph", phone);
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0) { MessageBox.Show("Tạo nhân viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadStaff(); }
                        else MessageBox.Show("Tạo thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                // try get from selected row
                if (dgvStaff.CurrentRow != null) email = (dgvStaff.CurrentRow.Cells["Email"].Value ?? "").ToString();
            }
            if (string.IsNullOrEmpty(email)) { MessageBox.Show("Nhập Email hoặc chọn nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (MessageBox.Show($"Bạn có chắc xóa tài khoản: {email} ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                using (var conn = kn.LayKetNoi())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM Staff WHERE Email=@e OR Username=@e OR Phone=@p", conn))
                    {
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@p", email);
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0) { MessageBox.Show("Xóa thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadStaff(); }
                        else MessageBox.Show("Không tìm thấy tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void UC_QuanLyNhanVien_Load(object sender, EventArgs e)
        {

        }

        // --- Các hàm mới: lấy dữ liệu Dịch vụ và Khách hàng theo nhân viên ---
        private void DgvStaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvStaff.Rows[e.RowIndex];
            string idVal = (row.Cells["Id"].Value ?? "").ToString();
            string email = (row.Cells["Email"].Value ?? "").ToString();
            string username = (row.Cells["Username"].Value ?? "").ToString();
            string name = (row.Cells["Name"].Value ?? "").ToString();

            try
            {
                DataTable dtServices = null;
                DataTable dtCustomers = null;

                if (!string.IsNullOrEmpty(idVal) && int.TryParse(idVal, out int staffId))
                {
                    // Nếu trong DB có trường liên kết bằng StaffId dùng id trực tiếp
                    dtServices = kn.LayDuLieu(
                        "SELECT s.Id, s.Name AS ServiceName, s.Price, su.DateUsed " +
                        "FROM Services s JOIN ServiceUsage su ON s.Id = su.ServiceId " +
                        "WHERE su.StaffId = " + staffId + " ORDER BY su.DateUsed DESC");

                    dtCustomers = kn.LayDuLieu(
                        "SELECT c.Id, c.Name AS CustomerName, c.Email, c.Phone, r.CheckInDate, r.CheckOutDate " +
                        "FROM Customers c JOIN Reservations r ON c.Id = r.CustomerId " +
                        "WHERE r.StaffId = " + staffId + " ORDER BY r.CheckInDate DESC");
                }
                else if (!string.IsNullOrEmpty(email))
                {
                    // Nếu không có Id, dùng Email/Username; escape ' để tránh lỗi SQL
                    string eEsc = email.Replace("'", "''");
                    dtServices = kn.LayDuLieu(
                        "SELECT s.Id, s.Name AS ServiceName, s.Price, su.DateUsed " +
                        "FROM Services s JOIN ServiceUsage su ON s.Id = su.ServiceId " +
                        "JOIN Staff st ON su.StaffId = st.Id " +
                        "WHERE st.Email = '" + eEsc + "' OR st.Username = '" + eEsc + "' " +
                        "ORDER BY su.DateUsed DESC");

                    dtCustomers = kn.LayDuLieu(
                        "SELECT c.Id, c.Name AS CustomerName, c.Email, c.Phone, r.CheckInDate, r.CheckOutDate " +
                        "FROM Customers c JOIN Reservations r ON c.Id = r.CustomerId " +
                        "JOIN Staff st ON r.StaffId = st.Id " +
                        "WHERE st.Email = '" + eEsc + "' OR st.Username = '" + eEsc + "' " +
                        "ORDER BY r.CheckInDate DESC");
                }
                else
                {
                    MessageBox.Show("Không xác định được nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ShowDetailsDialog(dtServices, dtCustomers, name, username, email);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu liên quan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tạo form tạm thời hiển thị 2 DataGridView: Dịch vụ và Khách hàng
        private void ShowDetailsDialog(DataTable services, DataTable customers, string staffName, string username, string email)
        {
            var f = new Form();
            f.Text = string.Format("Chi tiết nhân viên: {0} ({1})", string.IsNullOrEmpty(staffName) ? username : staffName, username);
            f.Width = 1000;
            f.Height = 700;
            f.StartPosition = FormStartPosition.CenterParent;

            var split = new SplitContainer();
            split.Dock = DockStyle.Fill;
            split.Orientation = Orientation.Horizontal;
            split.SplitterDistance = f.Height / 2;

            // Panel trên: Dịch vụ
            var panelTop = new Panel { Dock = DockStyle.Fill };
            var lblS = new Label { Text = "Dịch vụ (Service)", Dock = DockStyle.Top, Height = 22 };
            var dgvS = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = services
            };
            panelTop.Controls.Add(dgvS);
            panelTop.Controls.Add(lblS);

            // Panel dưới: Khách hàng
            var panelBottom = new Panel { Dock = DockStyle.Fill };
            var lblC = new Label { Text = "Khách hàng (Customer)", Dock = DockStyle.Top, Height = 22 };
            var dgvC = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = customers
            };
            panelBottom.Controls.Add(dgvC);
            panelBottom.Controls.Add(lblC);

            split.Panel1.Controls.Add(panelTop);
            split.Panel2.Controls.Add(panelBottom);

            f.Controls.Add(split);

            // Nếu muốn: thêm nút đóng
            var btnClose = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 30 };
            btnClose.Click += (s, e) => { f.Close(); };
            f.Controls.Add(btnClose);

            f.ShowDialog();
        }
    }
}