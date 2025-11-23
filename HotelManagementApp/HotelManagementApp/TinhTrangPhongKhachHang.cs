using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class TinhTrangPhongKhachHang : Form
    {
        public TinhTrangPhongKhachHang()
        {
            InitializeComponent();
            this.Load += TinhTrangPhongKhachHang_Load;

          
        }

        private void TinhTrangPhongKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                if (lblUserName != null) lblUserName.Text = "" + UserSession.CurrentUsername;
            }
            catch { }

            // Defensive load: nếu chưa có dữ liệu sample thì load 1 lần
            if (RoomStore.Rooms == null || RoomStore.Rooms.Count == 0)
            {
                try { RoomStore.LoadSampleRooms(); } catch { /* nếu dùng DB thì bỏ qua */ }
            }

            RefreshRoomLists();
        }

        // ===========================================
        //  RENDER DANH SÁCH PHÒNG
        //  (nếu RoomStore rỗng thì cố gắng khôi phục sample)
        // ===========================================
        private void RefreshRoomLists(string filter = "")
        {
            // defensive: nếu control bị null (nhỡ bạn đổi tên) thì thoát sớm
            if (flowAvailable == null || flowReserved == null)
            {
                // không ném lỗi để tránh crash
                return;
            }

            // Nếu RoomStore trống (bị clear ở đâu đó), load sample để tránh UI trắng
            if (RoomStore.Rooms == null || RoomStore.Rooms.Count == 0)
            {
                try
                {
                    RoomStore.LoadSampleRooms();
                }
                catch
                {
                    // nếu không thể load sample (vd: project dùng DB), chỉ clear UI
                    flowAvailable.Controls.Clear();
                    flowReserved.Controls.Clear();
                    return;
                }
            }

            flowAvailable.SuspendLayout();
            flowReserved.SuspendLayout();

            flowAvailable.Controls.Clear();
            flowReserved.Controls.Clear();

            string q = (filter ?? "").Trim().ToLower();

            foreach (var room in RoomStore.Rooms)
            {
                // áp filter tìm kiếm (nếu có)
                if (!string.IsNullOrEmpty(q))
                {
                    bool match =
                        (room.Name?.ToLower().Contains(q) == true) ||
                        ((room.Description ?? "").ToLower().Contains(q));
                    if (!match) continue;
                }

                bool reservedToday = IsRoomOccupiedOnDate(room, DateTime.Today);
                Panel tile = BuildRoomTile(room);

                if (reservedToday) flowReserved.Controls.Add(tile);
                else flowAvailable.Controls.Add(tile);
            }

            flowAvailable.ResumeLayout();
            flowReserved.ResumeLayout();
        }

        private bool IsRoomOccupiedOnDate(Room room, DateTime day)
        {
            if (room == null || room.Reservations == null) return false;
            DateTime d = day.Date;
            return room.Reservations.Any(res => res.StartDate.Date <= d && d < res.EndDate.Date);
        }

        // ===========================================
        //  BUILD ROOM TILE (khi mở chi tiết, đảm bảo xử lý callback an toàn)
        // ===========================================
        private Panel BuildRoomTile(Room room)
        {
            bool reservedToday = IsRoomOccupiedOnDate(room, DateTime.Today);

            Panel panel = new Panel
            {
                Width = 520,
                Height = 120,
                Margin = new Padding(8),
                Padding = new Padding(6),
                BackColor = reservedToday ? Color.LightCoral : Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = room
            };

            // Picture
            PictureBox pb = new PictureBox
            {
                Width = 120,
                Height = 80,
                Dock = DockStyle.Left,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = LoadImageOrPlaceholder(room.ResourceName, 120, 80)
            };

            // Info + controls
            Panel info = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            flow.Controls.Add(new Label
            {
                Text = "Tên phòng: " + room.Name,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true
            });

            flow.Controls.Add(new Label
            {
                Text = "Tình trạng: " + (reservedToday ? "Đã đặt" : "Sẵn sàng"),
                ForeColor = reservedToday ? Color.DarkRed : Color.DarkGreen,
                AutoSize = true
            });

            flow.Controls.Add(new Label
            {
                Text = "Chi phí: " + room.Price.ToString("N0") + " đ/đêm",
                AutoSize = true
            });

            // Nút xem chi tiết
            Button btn = new Button
            {
                Text = "Chi tiết",
                Width = 110,
                Height = 30,
                BackColor = Color.FromArgb(14, 107, 168),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Tag = room
            };
            btn.FlatAppearance.BorderSize = 0;

            btn.Click += (s, e) =>
            {
                var rm = (s as Button).Tag as Room;
                if (rm == null) return;

                // Mở form chi tiết modal (ShowDialog) và đăng ký sự kiện
                using (var detail = new RoomDetailForm(rm))
                {
                    detail.RoomUpdated += (updatedRoom) =>
                    {
                        // Khi room được cập nhật (thêm reservation...), re-render UI toàn bộ
                        // GỌI KHÔNG KÈM FILTER để tránh vô tình ẩn toàn bộ do filter hiện tại
                        RefreshRoomLists();
                    };

                    // Hiện modal — sau khi đóng, UI sẽ được cập nhật qua RoomUpdated
                    detail.ShowDialog(this);
                }
            };

            flow.Controls.Add(btn);
            info.Controls.Add(flow);

            panel.Controls.Add(info);
            panel.Controls.Add(pb);

            return panel;
        }

        // ===========================================
        //  Load image / placeholder (giữ nguyên)
        // ===========================================
        private Image LoadImageOrPlaceholder(string resourceName, int w, int h)
        {
            Image img = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(resourceName))
                {
                    var prop = typeof(Properties.Resources).GetProperty(resourceName,
                        System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    if (prop != null) img = prop.GetValue(null) as Image;
                }
            }
            catch { img = null; }

            if (img == null) return CreatePlaceholderImage(w, h);
            try { return new Bitmap(img, w, h); } catch { return (Image)img.Clone(); }
        }

        private Image CreatePlaceholderImage(int w, int h)
        {
            Bitmap bmp = new Bitmap(Math.Max(1, w), Math.Max(1, h));
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                g.DrawString("No image", new Font("Segoe UI", 9), Brushes.Black, new PointF(5, 5));
            }
            return bmp;
        }

        // ===========================================
        //  EVENTS: tìm kiếm / back
        // ===========================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // dùng giá trị textbox để filter (đã trim bên trong)
            RefreshRoomLists(txtSearch?.Text ?? "");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            RefreshRoomLists(txtSearch?.Text ?? "");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            KhachHangDatPhong khachHangDatPhong = new KhachHangDatPhong();
            khachHangDatPhong.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id = UserSession.CurrentUserId;
                TaiKhoanKhachHang tk = new TaiKhoanKhachHang(id);
                tk.Show();
                this.Hide();
            }
            catch
            {
                TaiKhoanKhachHang tk = new TaiKhoanKhachHang();
                tk.Show();
                this.Hide();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DatPhongKhachHang datPhongKhachHang = new DatPhongKhachHang();
            datPhongKhachHang.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Trang_Chu trang_Chu = new Trang_Chu();
            trang_Chu.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                int id = UserSession.CurrentUserId;
                TaiKhoanKhachHang tk = new TaiKhoanKhachHang(id);
                tk.Show();
                this.Hide();
            }
            catch
            {
                TaiKhoanKhachHang tk = new TaiKhoanKhachHang();
                tk.Show();
                this.Hide();
            }
        }
    }
}
