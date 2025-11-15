using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class DatPhongKhachHang : Form
    {
        public DatPhongKhachHang()
        {
            InitializeComponent();
            this.Load += DatPhongKhachHang_Load;
           
        }

        private void DatPhongKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                var lblUser = this.Controls.Find("label2", true).FirstOrDefault() as Label;
                if (lblUser == null)
                {
                    // tạo label tạm nếu Designer không có
                    lblUser = new Label { Name = "label2", AutoSize = true, Location = new Point(12, 12) };
                    this.Controls.Add(lblUser);
                    lblUser.BringToFront();
                }
                lblUser.Text = UserSession.CurrentUsername;
            }
            catch { }
            LoadUserReservations();
        }

        private void LoadUserReservations()
        {
            flowBooked.Controls.Clear();

            int userId = UserSession.CurrentUserId;

            // Lấy tất cả phòng mà người dùng này đã đặt
            var booked = RoomStore.Rooms
                .Where(r => r.Reservations != null &&
                            r.Reservations.Any(x => x.UserId == userId))
                .ToList();

            if (booked.Count == 0)
            {
                flowBooked.Controls.Add(new Label
                {
                    Text = "Bạn chưa đặt phòng nào.",
                    AutoSize = true,
                    ForeColor = Color.DarkRed,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Margin = new Padding(10)
                });
                return;
            }

            // Tạo tile cho mỗi phòng
            foreach (var room in booked)
            {
                var userRes = room.Reservations
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.StartDate)
                    .ToList();

                foreach (var res in userRes)
                {
                    flowBooked.Controls.Add(BuildBookedTile(room, res));
                }
            }
        }

        private Panel BuildBookedTile(Room room, Reservation res)
        {
            Panel p = new Panel
            {
                Width = 520,
                Height = 130,
                Margin = new Padding(10),
                Padding = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            PictureBox pb = new PictureBox
            {
                Width = 120,
                Height = 90,
                Dock = DockStyle.Left,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = LoadImageOrPlaceholder(room.ResourceName, 120, 90)
            };

            Panel info = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            FlowLayoutPanel fl = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            fl.Controls.Add(new Label
            {
                Text = "Phòng: " + room.Name,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true
            });

            fl.Controls.Add(new Label
            {
                Text = $"Ngày đặt: {res.StartDate:dd/MM/yyyy}",
                AutoSize = true
            });

            fl.Controls.Add(new Label
            {
                Text = $"Ngày rời đi: {res.EndDate:dd/MM/yyyy}",
                AutoSize = true
            });

            fl.Controls.Add(new Label
            {
                Text = "Giá: " + room.Price.ToString("N0") + " đ/đêm",
                AutoSize = true
            });

            info.Controls.Add(fl);

            p.Controls.Add(info);
            p.Controls.Add(pb);

            return p;
        }

        private Image LoadImageOrPlaceholder(string name, int w, int h)
        {
            Image img = null;
            try
            {
                var prop = typeof(Properties.Resources).GetProperty(name);
                if (prop != null) img = prop.GetValue(null) as Image;
            }
            catch { img = null; }

            if (img != null)
                return new Bitmap(img, w, h);

            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                g.DrawString("No Image", new Font("Segoe UI", 9),
                    Brushes.Black, new PointF(10, 10));
            }
            return bmp;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var frm = Application.OpenForms.OfType<KhachHangDatPhong>().FirstOrDefault();
            if (frm != null)
            {
                frm.Show();
                frm.BringToFront();
            }
            else
            {
                new KhachHangDatPhong().Show();
            }

            this.Close();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            KhachHangDatPhong khdp = new KhachHangDatPhong();
            khdp.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Trang_Chu trang_Chu = new Trang_Chu();
            trang_Chu.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TaiKhoanKhachHang taiKhoanKhachHang = new TaiKhoanKhachHang();
            taiKhoanKhachHang.Show();
            this.Hide();
        }
    }
}
