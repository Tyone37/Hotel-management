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

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_phong_sua : Form
    {
        // ✔ THÊM BIẾN NÀY – KHÔNG CÓ SẼ BÁO LỖI
        private int currentRoomId = -1;

        public trang_phong_sua()
        {
            InitializeComponent();
        }

        private void trang_phong_sua_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(StaffSession.DisplayName))
                label2.Text = StaffSession.DisplayName;
            else
                label2.Text = "Tên người dùng";

            ImageHelper.SetAvatarToPictureBox(pictureBox2);

            // ✔ Gọi hàm load dữ liệu (tách riêng để tái sử dụng)
            LoadDataGridAgain();
        }

        // ✔ THÊM HÀM NÀY – ĐỂ UPDATE XONG LOAD LẠI
        private void LoadDataGridAgain()
        {
            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            string query = "SELECT * FROM Hotel_room";

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

        // ✔ THÊM SỰ KIỆN NÀY – LẤY ID DÒNG ĐƯỢC CHỌN
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                currentRoomId = Convert.ToInt32(row.Cells["id"].Value);

                textBox2.Text = row.Cells["Room"].Value?.ToString();
                textBox3.Text = row.Cells["Price"].Value?.ToString();
                textBox4.Text = row.Cells["image"].Value?.ToString();
                textBox5.Text = row.Cells["Information"].Value?.ToString();

            }
            catch { }
        }

        public static class ImageHelper
        {
            public static void SetAvatarToPictureBox(PictureBox pbx)
            {
                if (StaffSession.AvatarData != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(StaffSession.AvatarData))
                        {
                            pbx.Image = Image.FromStream(ms);
                            pbx.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch
                    {
                        pbx.Image = null;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
    string.IsNullOrWhiteSpace(textBox3.Text) ||
    string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Nếu chưa chọn phòng, thử tìm id theo số phòng nhập
            if (currentRoomId == -1)
            {
                string connString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string sqlFind = "SELECT id FROM Hotel_room WHERE Room = @room";
                    SqlCommand cmdFind = new SqlCommand(sqlFind, conn);
                    cmdFind.Parameters.AddWithValue("@room", textBox2.Text);

                    conn.Open();
                    object result = cmdFind.ExecuteScalar();
                    if (result == null)
                    {
                        MessageBox.Show("Không tìm thấy phòng có số này!");
                        return;
                    }
                    currentRoomId = Convert.ToInt32(result);
                }
            }

            // Tiếp tục sửa như cũ
            string connString2 = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            using (SqlConnection conn2 = new SqlConnection(connString2))
            {
                string sql = @"UPDATE Hotel_room 
              SET Room=@room, Price=@price, image=@pic, Information=@detail 
              WHERE id=@id";

                SqlCommand cmd = new SqlCommand(sql, conn2);
                cmd.Parameters.AddWithValue("@id", currentRoomId);
                cmd.Parameters.AddWithValue("@room", textBox2.Text);
                cmd.Parameters.AddWithValue("@price", textBox3.Text);
                byte[] imgBytes = null;

                // nếu người dùng có chọn ảnh mới
                if (!string.IsNullOrEmpty(selectedImagePath) && File.Exists(selectedImagePath))
                {
                    imgBytes = File.ReadAllBytes(selectedImagePath);
                }

                SqlParameter pImg = new SqlParameter("@pic", SqlDbType.VarBinary);
                pImg.Value = (object)imgBytes ?? DBNull.Value;
                cmd.Parameters.Add(pImg);


                cmd.Parameters.AddWithValue("@detail", textBox5.Text);
                try
                {
                    conn2.Open();
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cập nhật: " + rows + " dòng.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật phòng: " + ex.Message);
                }

                LoadDataGridAgain();
            }
        }

        private string selectedImagePath = "";

        private void button8_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image files|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = ofd.FileName;
                    textBox4.Text = Path.GetFileName(ofd.FileName); // chỉ hiện tên
                    pictureBox3.Image = null; // không hiển thị ảnh
                }
            }
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Hotel_room WHERE Room = @room";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@room", SqlDbType.Int).Value = int.Parse(textBox6.Text.Trim());


                try
                {
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        // Lấy ID cho việc cập nhật
                        currentRoomId = Convert.ToInt32(rd["id"]);

                        // Gán dữ liệu vào TextBox
                        textBox3.Text = rd["Price"].ToString();
                        textBox5.Text = rd["Information"].ToString();

                        // Nếu có ảnh thì hiển thị lên pictureBox3
                        if (rd["image"] != DBNull.Value)
                        {
                            byte[] imgBytes = (byte[])rd["image"];
                            using (MemoryStream ms = new MemoryStream(imgBytes))
                            {
                                pictureBox3.Image = Image.FromStream(ms);
                                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                        else
                        {
                            pictureBox3.Image = null;
                        }

                        MessageBox.Show("Đã tìm thấy phòng!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng!");
                    }

                    rd.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
                }
            }
        }


    }
}
