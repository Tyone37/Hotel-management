using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_phong_sua : Form
    {
        private int currentRoomId = -1;
        private string selectedImagePath = "";
        private string connStr = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";

        public trang_phong_sua()
        {
            InitializeComponent();
        }

        private void trang_phong_sua_Load(object sender, EventArgs e)
        {
            // Hiển thị Username
            label2.Text = string.IsNullOrEmpty(StaffSession.Username) ? "Tài khoản" : StaffSession.Username;
            LoadDataGridAgain();
        }

        // ================= LOAD DANH SÁCH PHÒNG =================
        private void LoadDataGridAgain()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Hotel_room", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.Columns.Clear();
                dataGridView2.DataSource = dt;

                // Thêm cột hiển thị ảnh nếu có
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
                    if (row.Cells["image"].Value != DBNull.Value)
                    {
                        if (row.Cells["image"].Value is byte[] imgBytes)
                        {
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                row.Cells["image_preview"].Value = Image.FromStream(ms);
                            }
                        }
                        else if (row.Cells["image"].Value is Image img)
                        {
                            row.Cells["image_preview"].Value = img;
                        }
                    }
                    else
                    {
                        row.Cells["image_preview"].Value = null;
                    }
                }

            }
                dataGridView2.ReadOnly = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
        }

        // ================= CHỌN DÒNG =================
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
            currentRoomId = Convert.ToInt32(row.Cells["id"].Value);
            textBox2.Text = row.Cells["Room"].Value.ToString();
            textBox3.Text = row.Cells["Price"].Value.ToString();
            textBox5.Text = row.Cells["Information"].Value.ToString();
        }

        // ================= CHỌN ẢNH =================
        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = ofd.FileName;
                textBox4.Text = Path.GetFileName(selectedImagePath);
            }
        }

        // ================= SỬA PHÒNG =================
        private void button6_Click(object sender, EventArgs e)
        {
            if (currentRoomId == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng!");
                return;
            }

            if (StaffSession.StaffId <= 0)
            {
                MessageBox.Show("Chưa có thông tin nhân viên đăng nhập!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                byte[] imgBytes = null;
                if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
                    imgBytes = File.ReadAllBytes(selectedImagePath);

                SqlCommand cmd = new SqlCommand(
                    @"UPDATE Hotel_room 
                      SET Room=@room, Price=@price, image=@img, Information=@info 
                      WHERE id=@id", conn);

                cmd.Parameters.AddWithValue("@id", currentRoomId);
                cmd.Parameters.AddWithValue("@room", textBox2.Text);
                cmd.Parameters.Add("@price", SqlDbType.Int).Value = int.Parse(textBox3.Text);
                cmd.Parameters.AddWithValue("@info", textBox5.Text);
                cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = imgBytes != null ? (object)imgBytes : DBNull.Value;

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    GhiLichSu("Sửa", "Sửa phòng số " + textBox2.Text);
                    MessageBox.Show("Cập nhật phòng thành công!");
                    LoadDataGridAgain();
                }
            }
        }

        // ================= TÌM PHÒNG =================
        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Hotel_room WHERE Room = @room", conn);
                cmd.Parameters.AddWithValue("@room", textBox6.Text.Trim());
                conn.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    currentRoomId = Convert.ToInt32(rd["id"]);
                    textBox2.Text = rd["Room"].ToString();
                    textBox3.Text = rd["Price"].ToString();
                    textBox5.Text = rd["Information"].ToString();
                    MessageBox.Show("Đã tìm thấy phòng!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phòng!");
                }
            }
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
