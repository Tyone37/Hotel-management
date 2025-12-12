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

namespace HotelManagementApp
{
    public partial class nv_cacphong : Form
    {
        public nv_cacphong()
        {
            InitializeComponent();
        }

        private void nv_cacphong_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StaffSession.Username)) return;

            if (!string.IsNullOrEmpty(StaffSession.DisplayName))
            {
                label2.Text = StaffSession.DisplayName;
            }
            else
            {
                label2.Text = "Tên người dùng";
            }
            ImageHelper.SetAvatarToPictureBox(pictureBox2);

            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            string query = "SELECT Room, Price, Information FROM Hotel_room";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    
                    DataTable dt = new DataTable();

                    
                    adapter.Fill(dt);

                    
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
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
                    catch (Exception)
                    {
                        pbx.Image = null;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StaffSession.Username = null;
            StaffSession.DisplayName = null;

            Log_in loginForm = new Log_in();
            loginForm.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Staff staffForm = new Staff();
            staffForm.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string room = textBox2.Text.ToString();

            string connectionString = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";
            string query = "SELECT Room, Price, Information FROM Hotel_room where Room = @Room";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    adapter.SelectCommand.Parameters.AddWithValue("@Room", room);

                    DataTable dt = new DataTable();


                    adapter.Fill(dt);


                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
