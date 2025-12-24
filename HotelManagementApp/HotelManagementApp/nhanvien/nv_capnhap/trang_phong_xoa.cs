using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_phong_xoa : Form
    {
        string connStr = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";

        public trang_phong_xoa()
        {
            InitializeComponent();
        }

        private void trang_phong_xoa_Load(object sender, EventArgs e)
        {
            label2.Text = string.IsNullOrEmpty(StaffSession.Username) ? "Manager" : StaffSession.Username;
            LoadPhong();
        }

        // ================= LOAD PHÒNG =================
        void LoadPhong(string filter = "")
        {
            dataGridView2.DataError += (s, e) =>
            {
                e.ThrowException = false;
            };

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sql = string.IsNullOrEmpty(filter)
                    ? "SELECT * FROM Hotel_room"
                    : "SELECT * FROM Hotel_room WHERE Room LIKE '%' + @room + '%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                if (!string.IsNullOrEmpty(filter))
                    da.SelectCommand.Parameters.AddWithValue("@room", filter);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.Columns.Clear();
                dataGridView2.DataSource = dt;

                // Thêm cột ảnh nếu có
                if (!dataGridView2.Columns.Contains("image_preview"))
                {
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    imgCol.Name = "image_preview";
                    imgCol.HeaderText = "Ảnh phòng";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dataGridView2.Columns.Add(imgCol);
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    try
                    {
                        if (row.Cells["image"].Value != DBNull.Value)
                        {
                            byte[] imgBytes = row.Cells["image"].Value as byte[];

                            if (imgBytes != null && imgBytes.Length > 0)
                            {
                                try
                                {
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        row.Cells["image_preview"].Value = Image.FromStream(ms);
                                    }
                                }
                                catch
                                {
                                    // Nếu dữ liệu không phải là ảnh hợp lệ
                                    row.Cells["image_preview"].Value = null;
                                }
                            }
                            else
                            {
                                row.Cells["image_preview"].Value = null;
                            }
                        }
                        else
                        {
                            row.Cells["image_preview"].Value = null;
                        }
                    }
                    catch
                    {
                        row.Cells["image_preview"].Value = null;
                    
                }

            }



            dataGridView2.ReadOnly = true;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.AllowUserToAddRows = false;
            }
        }

        // ================= XÓA PHÒNG =================
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!");
                return;
            }

            if (StaffSession.StaffId <= 0)
            {
                MessageBox.Show("Chưa có thông tin nhân viên đăng nhập!");
                return;
            }

            int roomId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);
            string roomName = dataGridView2.SelectedRows[0].Cells["Room"].Value.ToString();

            DialogResult rs = MessageBox.Show(
                $"Xóa phòng {roomName} ?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (rs != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Hotel_room WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", roomId);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    GhiLichSu("Xóa", $"Xóa phòng {roomName}");
                    MessageBox.Show("Xóa phòng thành công!");
                    LoadPhong();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        // ================= TÌM KIẾM =================
        private void button8_Click(object sender, EventArgs e)
        {
            string filter = textBox6.Text.Trim();
            LoadPhong(filter);
        }

        // ================= GHI LỊCH SỬ =================
        void GhiLichSu(string hanhDong, string noiDung)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO LichSuHoatDong (MaNV, HanhDong, NoiDung, ThoiGian)
                      VALUES (@MaNV, @HD, @ND, @TG)", conn);

                cmd.Parameters.AddWithValue("@MaNV", StaffSession.StaffId);
                cmd.Parameters.AddWithValue("@HD", hanhDong);
                cmd.Parameters.AddWithValue("@ND", noiDung);
                cmd.Parameters.AddWithValue("@TG", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
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
            trang_phong_them trang_phong_them = new trang_phong_them();
            trang_phong_them.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            trang_phong_xoa trang_phong_xoa = new trang_phong_xoa();
            trang_phong_xoa.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            trang_phong_sua trang_phong_sua = new trang_phong_sua();
            trang_phong_sua.Show();
            this.Close();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lichsuhoatdong lichsuhoatdong = new lichsuhoatdong();
            lichsuhoatdong.Show();
            this.Close(); // nếu muốn đóng form hiện tại
        }

    }
}
