using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class KhachHangDatPhong : Form
    {
        private FlowLayoutPanel roomsContainer;
        private Panel selectedPanel = null;

        public KhachHangDatPhong()
        {
            InitializeComponent();
            this.Load += KhachHangDatPhong_Load;

            // đảm bảo textBox1 tồn tại trước khi gán event
            try
            {
                if (this.Controls.Find("textBox1", true).FirstOrDefault() is TextBox tb)
                    tb.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
            }
            catch { }
        }

        private void KhachHangDatPhong_Load(object sender, EventArgs e)
        {
            
            

            // Hiển thị username nếu có UserSession
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

            // tìm hoặc tạo FlowLayoutPanel panelRoomsContainer
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
                    Padding = new Padding(12),
                    Location = new Point(10, 50),
                    Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 60),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                };
                this.Controls.Add(roomsContainer);
                roomsContainer.BringToFront();
            }

            // If RoomStore empty, try to load (defensive)
            try
            {
                if (RoomStore.Rooms == null || RoomStore.Rooms.Count == 0)
                    RoomStore.LoadSampleRooms();
            }
            catch { }

            // Render initial rooms
            RenderRooms();

            // ensure lblNoResult exists
            var existing = this.Controls.Find("lblNoResult", true).FirstOrDefault();
            if (existing == null)
            {
                var lbl = new Label
                {
                    Name = "lblNoResult",
                    Text = "Không tìm thấy phòng phù hợp.",
                    AutoSize = true,
                    ForeColor = Color.DarkRed,
                    Visible = false,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Location = new Point(roomsContainer.Left + 10, roomsContainer.Top + 10)
                };
                this.Controls.Add(lbl);
                lbl.BringToFront();
            }
        }

        /// <summary>
        /// Render lại tất cả tile phòng từ RoomStore (an toàn khi RoomStore rỗng)
        /// </summary>
        private void RenderRooms(string filter = "")
        {
            if (roomsContainer == null) return;

            // Defensive: nếu RoomStore rỗng thì show thông báo
            if (RoomStore.Rooms == null || RoomStore.Rooms.Count == 0)
            {
                roomsContainer.Controls.Clear();
                var lbl = this.Controls.Find("lblNoResult", true).FirstOrDefault() as Label;
                if (lbl != null)
                {
                    lbl.Text = "Không có phòng để hiển thị.";
                    lbl.Visible = true;
                }
                return;
            }

            roomsContainer.SuspendLayout();
            roomsContainer.Controls.Clear();

            string q = (filter ?? "").Trim().ToLowerInvariant();

            foreach (var r in RoomStore.Rooms)
            {
                if (!string.IsNullOrEmpty(q))
                {
                    if (!(r.Name?.ToLower().Contains(q) == true || (r.Description ?? "").ToLower().Contains(q)))
                        continue;
                }
                roomsContainer.Controls.Add(BuildRoomTile(r));
            }

            // hide/show no result label
            var noLbl = this.Controls.Find("lblNoResult", true).FirstOrDefault() as Label;
            if (noLbl != null) noLbl.Visible = (roomsContainer.Controls.Count == 0);

            roomsContainer.ResumeLayout();
        }

        private bool IsRoomOccupiedOnDate(Room r, DateTime day)
        {
            if (r?.Reservations == null) return false;
            DateTime d = day.Date;
            return r.Reservations.Any(res => res.StartDate.Date <= d && d < res.EndDate.Date);
        }

        // Build tile with safe image loading and correct reservedToday logic
        private Panel BuildRoomTile(Room room)
        {
            bool reservedToday = IsRoomOccupiedOnDate(room, DateTime.Today);

            var p = new Panel
            {
                Width = 340,
                Height = 160,
                Margin = new Padding(8),
                Padding = new Padding(6),
                BackColor = reservedToday ? Color.LightCoral : Color.White,
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

            // Try load image from resources, fallback placeholder
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
            catch { loaded = null; }

            if (loaded == null)
            {
                try
                {
                    var dp = typeof(Properties.Resources).GetProperty("default_room", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    if (dp != null) loaded = dp.GetValue(null, null) as Image;
                }
                catch { loaded = null; }
            }

            if (loaded == null)
            {
                var bmp = new Bitmap(120, 80);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.FromArgb(240, 245, 255));
                    using (var f = new Font("Segoe UI", 9))
                    using (var sb = new SolidBrush(Color.Gray))
                    {
                        var txt = "No image";
                        var sz = g.MeasureString(txt, f);
                        g.DrawString(txt, f, sb, (bmp.Width - sz.Width) / 2f, (bmp.Height - sz.Height) / 2f);
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
                Text = "Tình trạng: " + (reservedToday ? "Đã thuê" : "Sẵn sàng"),
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9),
                ForeColor = reservedToday ? Color.DarkRed : Color.DarkGreen,
                TextAlign = ContentAlignment.MiddleLeft
            };

            var lblPrice = new Label
            {
                Text = "Chi phí: " + room.Price.ToString("N0") + " đ/đêm",
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
                Text = reservedToday ? "Đã thuê" : "Chi tiết",
                Width = 110,
                Height = 28,
                Enabled = !reservedToday,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(235, 128, 128),
                ForeColor = Color.White,
                Tag = room
            };
            btnPay.FlatAppearance.BorderSize = 0;
            btnPay.Click += (s, e) =>
            {
                var rm = (s as Button).Tag as Room;
                if (rm == null) return;

                var frm = new RoomDetailForm(rm);
                frm.RoomUpdated += (updatedRoom) =>
                {
                    // Re-render full list (no filter) to avoid accidental hide-all
                    RenderRooms();
                };
                frm.ShowDialog(this);
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

        private void DetailForm_RoomUpdated(Room updatedRoom)
        {
            // when other callers use this callback shape, simply re-render to reflect changes
            RenderRooms();
        }

        private void SelectPanel(Panel p)
        {
            if (selectedPanel != null && !ReferenceEquals(selectedPanel, p))
            {
                if (selectedPanel.Tag is Room prev)
                    selectedPanel.BackColor = IsRoomOccupiedOnDate(prev, DateTime.Today) ? Color.LightCoral : Color.White;
                selectedPanel.BorderStyle = BorderStyle.FixedSingle;
            }

            selectedPanel = p;
            selectedPanel.BorderStyle = BorderStyle.Fixed3D;
            roomsContainer.ScrollControlIntoView(selectedPanel);
        }

        // navigation placeholders (Designer may wire these up)
        private void button2_Click(object sender, EventArgs e) {
            Trang_Chu trang_Chu = new Trang_Chu();
            trang_Chu.Show();
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
        private void button5_Click(object sender, EventArgs e) {
        TinhTrangPhongKhachHang tinhTrangPhongKhachHang = new TinhTrangPhongKhachHang();
            tinhTrangPhongKhachHang.Show();
            this.Hide();
        }
        private void button6_Click(object sender, EventArgs e) {
            DatPhongKhachHang datPhongKhachHang = new DatPhongKhachHang();
            datPhongKhachHang.Show();
            this.Hide();
        }
        private void button7_Click(object sender, EventArgs e) { /* ... */ }
        private void button8_Click(object sender, EventArgs e) {
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

        // Search/filter handlers (Designer may contain textBox1)
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (roomsContainer == null) return;

            string q = this.Controls.Find("textBox1", true).FirstOrDefault() is TextBox tb ? tb.Text?.Trim() ?? "" : "";
            if (string.IsNullOrEmpty(q))
            {
                foreach (Control c in roomsContainer.Controls) c.Visible = true;
                var noLbl = this.Controls.Find("lblNoResult", true).FirstOrDefault() as Label;
                if (noLbl != null) noLbl.Visible = false;
                return;
            }

            q = q.ToLowerInvariant();
            Panel firstMatch = null;
            int visibleCount = 0;

            foreach (Control c in roomsContainer.Controls)
            {
                bool match = false;
                if (c.Tag is Room r)
                {
                    if (!string.IsNullOrEmpty(r.Name) && r.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                        match = true;
                    else if (!string.IsNullOrEmpty(r.Description) && r.Description.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                        match = true;
                }

                c.Visible = match;
                if (match)
                {
                    visibleCount++;
                    if (firstMatch == null && c is Panel p) firstMatch = p;
                }
            }

            var lblNo = this.Controls.Find("lblNoResult", true).FirstOrDefault() as Label;
            if (lblNo != null) lblNo.Visible = (visibleCount == 0);

            if (firstMatch != null)
            {
                SelectPanel(firstMatch);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1_TextChanged(sender, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void panelRoomsContainer_Paint(object sender, PaintEventArgs e) { }

       

       

       
    }
}
