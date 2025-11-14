using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class KhachHangDatPhong : Form
    {
        private FlowLayoutPanel roomsContainer;
        private Panel selectedPanel = null;
        private List<Room> rooms = new List<Room>();

        public KhachHangDatPhong()
        {
            InitializeComponent();
            this.Load += KhachHangDatPhong_Load;
        }

        private void KhachHangDatPhong_Load(object sender, EventArgs e)
        {
           

            // Tìm control tên panelRoomsContainer (nếu bạn đã tạo trong Designer)
            var found = this.Controls.Find("panelRoomsContainer", true).FirstOrDefault();
            if (found is FlowLayoutPanel flp)
            {
                roomsContainer = flp;
            }
            else
            {
                // tạo động FlowLayoutPanel nếu không có trong Designer
                roomsContainer = new FlowLayoutPanel
                {
                    Name = "panelRoomsContainer",
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true,
                    AutoScroll = true,
                    Padding = new Padding(12)
                };
                this.Controls.Add(roomsContainer);
                roomsContainer.BringToFront();
            }

            // Tạo mẫu 10 phòng (gán ResourceName tương ứng tên property trong Properties.Resources)
            rooms.Clear();
            rooms.Add(new Room { Id = 1, Name = "101", Price = 150000, IsOccupied = false, Description = "Phòng 101", ResourceName = "room101", ImageResourceNames = new List<string> { "room101", "room101_2", "room101_3" } });
            rooms.Add(new Room { Id = 2, Name = "102", Price = 160000, IsOccupied = false, Description = "Phòng 102", ResourceName = "room102" });
            rooms.Add(new Room { Id = 3, Name = "103", Price = 170000, IsOccupied = true, Description = "Phòng 103", ResourceName = "room103" });
            rooms.Add(new Room { Id = 4, Name = "104", Price = 180000, IsOccupied = false, Description = "Phòng 104", ResourceName = "room104" });
            rooms.Add(new Room { Id = 5, Name = "105", Price = 190000, IsOccupied = false, Description = "Phòng 105", ResourceName = "room105" });
            rooms.Add(new Room { Id = 6, Name = "106", Price = 200000, IsOccupied = true, Description = "Phòng 106", ResourceName = "room106" });
            rooms.Add(new Room { Id = 7, Name = "107", Price = 210000, IsOccupied = false, Description = "Phòng 107", ResourceName = "room107" });
            rooms.Add(new Room { Id = 8, Name = "108", Price = 220000, IsOccupied = false, Description = "Phòng 108", ResourceName = "room108" });
            rooms.Add(new Room { Id = 9, Name = "109", Price = 230000, IsOccupied = true, Description = "Phòng 109", ResourceName = "room109" });
            rooms.Add(new Room { Id = 10, Name = "110", Price = 240000, IsOccupied = false, Description = "Phòng 110", ResourceName = "room110" });

            // Tạo tile
            roomsContainer.Controls.Clear();
            foreach (var r in rooms)
                roomsContainer.Controls.Add(BuildRoomTile(r));
        }

        // ===== Build tile - load image safely from Properties.Resources (try names, fallback default, placeholder)
        private Panel BuildRoomTile(Room room)
        {
            var p = new Panel
            {
                Width = 340,
                Height = 160,
                Margin = new Padding(8),
                Padding = new Padding(6),
                BackColor = room.IsOccupied ? Color.LightCoral : Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = room
            };

            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

            // PictureBox
            var pb = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(240, 245, 255),
                Margin = new Padding(6)
            };

            // Load image safely (try room.ResourceName, try with _jpg/_png, try default_room resource, else placeholder)
            Image loaded = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(room.ResourceName))
                {
                    var prop = typeof(Properties.Resources).GetProperty(room.ResourceName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    if (prop != null) loaded = prop.GetValue(null, null) as Image;

                    if (loaded == null)
                    {
                        string[] tryNames = { room.ResourceName + "_jpg", room.ResourceName + "_png", room.ResourceName + "_jpeg" };
                        foreach (var tn in tryNames)
                        {
                            var p2 = typeof(Properties.Resources).GetProperty(tn, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                            if (p2 != null) { loaded = p2.GetValue(null, null) as Image; break; }
                        }
                    }
                }
            }
            catch
            {
                loaded = null;
            }

            // try default_room
            if (loaded == null)
            {
                try
                {
                    var dp = typeof(Properties.Resources).GetProperty("default_room", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    if (dp != null) loaded = dp.GetValue(null, null) as Image;
                }
                catch { loaded = null; }
            }

            // placeholder if null
            if (loaded == null)
            {
                int w = 120, h = 80;
                var bmp = new Bitmap(w, h);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.FromArgb(240, 245, 255));
                    using (var f = new Font("Segoe UI", 9))
                    using (var sb = new SolidBrush(Color.Gray))
                    {
                        var txt = "No image";
                        var sz = g.MeasureString(txt, f);
                        g.DrawString(txt, f, sb, (w - sz.Width) / 2f, (h - sz.Height) / 2f);
                    }
                }
                loaded = bmp;
            }

            pb.Image = new Bitmap(loaded);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;

            var imgPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4) };
            imgPanel.Controls.Add(pb);

            // Info panel
            var info = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3, Padding = new Padding(6) };
            info.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            info.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            info.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));

            var lblName = new Label
            {
                Text = "Tên phòng: " + room.Name,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var lblStatus = new Label
            {
                Text = "Tình trạng: " + (room.IsOccupied ? "đã thuê" : "sẵn sàng"),
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                ForeColor = room.IsOccupied ? Color.DarkRed : Color.DarkGreen,
                TextAlign = ContentAlignment.MiddleLeft
            };
            var lblPrice = new Label
            {
                Text = "Chi phí: " + room.Price.ToString("N0") + "đ/đêm",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleLeft
            };

            info.Controls.Add(lblName, 0, 0);
            info.Controls.Add(lblStatus, 0, 1);
            info.Controls.Add(lblPrice, 0, 2);

            // Button panel
            var btnPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(8) };
            var btnPay = new Button
            {
                Text = room.IsOccupied ? "Đã thuê" : "Chi tiết",
                Width = 110,
                Height = 28,
                Enabled = !room.IsOccupied,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(235, 128, 128),
                ForeColor = Color.White
            };
            btnPay.FlatAppearance.BorderSize = 0;
            btnPay.Tag = room;
            btnPay.Click += (s, e) =>
            {
                var frm = new RoomDetailForm(room);
                frm.RoomUpdated += (updatedRoom) =>
                {
                    if (updatedRoom == null) return;
                    room.IsOccupied = updatedRoom.IsOccupied;
                    room.Price = updatedRoom.Price;
                    room.Description = updatedRoom.Description;

                    lblStatus.Text = "Tình trạng: " + (room.IsOccupied ? "đã thuê" : "sẵn sàng");
                    lblStatus.ForeColor = room.IsOccupied ? Color.DarkRed : Color.DarkGreen;
                    btnPay.Enabled = !room.IsOccupied;
                    btnPay.Text = room.IsOccupied ? "Đã thuê" : "Thanh toán";
                    p.BackColor = room.IsOccupied ? Color.LightCoral : Color.White;
                };
                frm.ShowDialog();
            };

            btnPanel.Controls.Add(btnPay);

            // Compose content
            var contentPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            contentPanel.Controls.Add(imgPanel, 0, 0);
            contentPanel.Controls.Add(info, 1, 0);

            tbl.Controls.Add(contentPanel, 0, 0);
            tbl.SetColumnSpan(contentPanel, 2);
            tbl.Controls.Add(btnPanel, 1, 1);

            // select handlers
            p.Click += (s, e) => SelectPanel(p);
            pb.Click += (s, e) => SelectPanel(p);
            lblName.Click += (s, e) => SelectPanel(p);
            lblStatus.Click += (s, e) => SelectPanel(p);
            lblPrice.Click += (s, e) => SelectPanel(p);

            p.Controls.Add(tbl);
            return p;
        }

        // update from detail form (if used elsewhere)
        private void DetailForm_RoomUpdated(Room updatedRoom)
        {
            if (updatedRoom == null) return;
            var r = rooms.Find(x => x.Id == updatedRoom.Id);
            if (r == null) return;

            r.IsOccupied = updatedRoom.IsOccupied;
            r.Price = updatedRoom.Price;
            r.Description = updatedRoom.Description;

            Panel existing = null;
            foreach (Control c in roomsContainer.Controls)
            {
                if (c is Panel p && p.Tag is Room rr && rr.Id == r.Id)
                {
                    existing = p;
                    break;
                }
            }
            if (existing != null)
            {
                int idx = roomsContainer.Controls.GetChildIndex(existing);
                roomsContainer.Controls.Remove(existing);
                var newPanel = BuildRoomTile(r);
                roomsContainer.Controls.Add(newPanel);
                roomsContainer.Controls.SetChildIndex(newPanel, idx);
            }
        }

        private void SelectPanel(Panel p)
        {
            if (selectedPanel != null && !ReferenceEquals(selectedPanel, p))
            {
                if (selectedPanel.Tag is Room prev)
                    selectedPanel.BackColor = prev.IsOccupied ? Color.LightCoral : Color.White;
                selectedPanel.BorderStyle = BorderStyle.FixedSingle;
            }

            selectedPanel = p;
            selectedPanel.BorderStyle = BorderStyle.Fixed3D;
            roomsContainer.ScrollControlIntoView(selectedPanel);
        }

        // placeholder navigation handlers
        private void button2_Click(object sender, EventArgs e) { /* ... */ }
        private void button4_Click(object sender, EventArgs e) { /* ... */ }
        private void button5_Click(object sender, EventArgs e) { /* ... */ }
        private void button6_Click(object sender, EventArgs e) { /* ... */ }
        private void button7_Click(object sender, EventArgs e) { /* ... */ }
        private void button8_Click(object sender, EventArgs e) { /* ... */ }
    }
}
