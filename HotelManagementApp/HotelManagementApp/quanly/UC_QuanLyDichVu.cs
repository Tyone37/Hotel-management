using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    public partial class UC_QuanLyDichVu : UserControl
    {
        private KetNoi kn = new KetNoi();

        public UC_QuanLyDichVu()
        {
            InitializeComponent();
            LoadServices();
        }

        private void LoadServices(string filter = null)
        {
            try
            {
                var dt = kn.LayDuLieu("SELECT Id, Name, Price, Status FROM DichVu ORDER BY Name");
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    var rows = dt.Select($"Name LIKE '%{filter.Replace("'", "''")}%'");
                    dt = rows.Length > 0 ? rows.CopyToDataTable() : dt.Clone();
                }
                dgvServices.DataSource = dt;
                if (dgvServices.Columns["Id"] != null) dgvServices.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách dịch vụ: " + ex.Message);
            }
        }

        private void dgvServices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServices.CurrentRow == null) return;
            var r = dgvServices.CurrentRow;
            txtServiceName.Text = r.Cells["Name"].Value?.ToString() ?? "";
            decimal price = 0;
            decimal.TryParse(r.Cells["Price"].Value?.ToString() ?? "0", out price);
            numServicePrice.Value = price;
            cmbServiceStatus.Text = r.Cells["Status"].Value?.ToString() ?? "";
        }

        private void btnServiceAdd_Click(object sender, EventArgs e)
        {
            string name = txtServiceName.Text.Trim();
            decimal price = numServicePrice.Value;
            string status = cmbServiceStatus.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Nhập tên dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = kn.LayKetNoi())
                {
                    conn.Open();
                    using (SqlCommand chk = new SqlCommand("SELECT COUNT(1) FROM DichVu WHERE Name=@name", conn))
                    {
                        chk.Parameters.AddWithValue("@name", name);
                        if ((int)chk.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Dịch vụ đã tồn tại.", "Thông báo");
                            return;
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO DichVu (Name, Price, Status) VALUES (@name,@price,@status)", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@status", status);
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0) { MessageBox.Show("Đã thêm dịch vụ."); LoadServices(); }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm dịch vụ: " + ex.Message); }
        }

        private void btnServiceEdit_Click(object sender, EventArgs e)
        {
            if (dgvServices.CurrentRow == null) { MessageBox.Show("Chọn dịch vụ để sửa."); return; }
            int id = Convert.ToInt32(dgvServices.CurrentRow.Cells["Id"].Value);
            string name = txtServiceName.Text.Trim();
            decimal price = numServicePrice.Value;
            string status = cmbServiceStatus.Text.Trim();

            try
            {
                using (SqlConnection conn = kn.LayKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE DichVu SET Name=@name, Price=@price, Status=@status WHERE Id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@id", id);
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0) { MessageBox.Show("Cập nhật thành công."); LoadServices(); }
                        else MessageBox.Show("Cập nhật thất bại.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi cập nhật: " + ex.Message); }
        }

        private void btnServiceDelete_Click(object sender, EventArgs e)
        {
            if (dgvServices.CurrentRow == null) { MessageBox.Show("Chọn dịch vụ để xóa."); return; }
            int id = Convert.ToInt32(dgvServices.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("Xóa dịch vụ đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = kn.LayKetNoi())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM DichVu WHERE Id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int r = cmd.ExecuteNonQuery();
                        if (r > 0) { MessageBox.Show("Xóa thành công."); LoadServices(); }
                        else MessageBox.Show("Xóa thất bại.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
        }

        private void btnServiceRefresh_Click(object sender, EventArgs e) => LoadServices();
        private void btnServiceSearch_Click(object sender, EventArgs e) => LoadServices(txtServiceSearch.Text.Trim());
    }
}