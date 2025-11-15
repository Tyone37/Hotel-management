using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class RoomDetailForm : Form
    {
        private Room room;
        public event Action<Room> RoomUpdated;

        // UI controls
        private PictureBox pbMain;
        private FlowLayoutPanel flThumbnails;
        private Label lblName;
        private Label lblPrice;
        private Label lblStatus;
        private TextBox txtDescription;
        private Button btnPayment;
        private Button btnClose;

        // New: date pickers
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label lblFrom;
        private Label lblTo;

        public RoomDetailForm(Room room)
        {
            InitializeComponent(); // giữ nếu Designer có; file là partial
            this.room = room ?? throw new ArgumentNullException(nameof(room));
            BuildUI();
            LoadRoomData();
        }

        private void BuildUI()
        {
            // Form basic
            this.Text = $"Chi tiết - Phòng {room.Name}";
            this.Size = new Size(760, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label
            {
                Text = $"Chi tiết phòng {room.Name}",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 44,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Main layout: left = image, right = info; bottom thumbnails
            var mainTbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new Padding(12)
            };
            mainTbl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320)); // left image area
            mainTbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // right info
            mainTbl.RowStyles.Add(new RowStyle(SizeType.Percent, 70)); // top content
            mainTbl.RowStyles.Add(new RowStyle(SizeType.Percent, 30)); // thumbnails

            // Left: main image
            pbMain = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 250, 255),
                Margin = new Padding(8)
            };
            var leftPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6) };
            leftPanel.Controls.Add(pbMain);

            // Right: info layout
            var infoTbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 7,
                Padding = new Padding(6)
            };
            // rows: 0 name,1 status,2 price,3 dates,4 spacer,5 description,6 buttons
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 32)); // name
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 26)); // status
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 26)); // price
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 34)); // dates panel
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 6));  // spacer
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // description
            infoTbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 56)); // buttons

            lblName = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleLeft };
            lblStatus = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9), TextAlign = ContentAlignment.MiddleLeft };
            lblPrice = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10), TextAlign = ContentAlignment.MiddleLeft };

            txtDescription = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };

            // Date pickers
            lblFrom = new Label { Text = "Ngày đến:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left };
            dtpFrom = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120, MinDate = DateTime.Today, Value = DateTime.Today };

            lblTo = new Label { Text = "Ngày rời:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left };
            dtpTo = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120, MinDate = DateTime.Today.AddDays(1), Value = DateTime.Today.AddDays(1) };

            var datePanel = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, Dock = DockStyle.Fill, Padding = new Padding(2) };
            datePanel.Controls.Add(lblFrom);
            datePanel.Controls.Add(dtpFrom);
            datePanel.Controls.Add(new Label { Width = 12 }); // spacer
            datePanel.Controls.Add(lblTo);
            datePanel.Controls.Add(dtpTo);

            btnPayment = new Button { Text = "Thanh toán / Đặt", Width = 140, Height = 34, Anchor = AnchorStyles.Right };
            btnPayment.Click += BtnPayment_Click;
            btnClose = new Button { Text = "Đóng", Width = 90, Height = 34, Anchor = AnchorStyles.Right };
            btnClose.Click += (s, e) => this.Close();

            var pnlButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(4) };
            pnlButtons.Controls.Add(btnClose);
            pnlButtons.Controls.Add(btnPayment);

            infoTbl.Controls.Add(lblName, 0, 0);
            infoTbl.Controls.Add(lblStatus, 0, 1);
            infoTbl.Controls.Add(lblPrice, 0, 2);
            infoTbl.Controls.Add(datePanel, 0, 3);
            infoTbl.Controls.Add(new Label() { Height = 6 }, 0, 4);
            infoTbl.Controls.Add(txtDescription, 0, 5);
            infoTbl.Controls.Add(pnlButtons, 0, 6);

            // Thumbnails panel (bottom)
            flThumbnails = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoScroll = true,
                WrapContents = false,
                Height = 120,
                Padding = new Padding(6)
            };

            // Compose
            mainTbl.Controls.Add(leftPanel, 0, 0);
            mainTbl.SetRowSpan(leftPanel, 2);
            mainTbl.Controls.Add(infoTbl, 1, 0);
            mainTbl.Controls.Add(flThumbnails, 1, 1);

            this.Controls.Clear();
            this.Controls.Add(mainTbl);
            this.Controls.Add(lblTitle);
        }

        private void LoadRoomData()
        {
            // Info
            lblName.Text = $"Tên phòng: {room.Name}";
            lblStatus.Text = $"Tình trạng: {(room.IsOccupied ? "Đã thuê" : "Sẵn sàng")}";
            lblStatus.ForeColor = room.IsOccupied ? Color.DarkRed : Color.DarkGreen;
            lblPrice.Text = $"Chi phí: {room.Price:N0} đ/đêm";
            txtDescription.Text = string.IsNullOrWhiteSpace(room.Description) ? "(Không có mô tả)" : room.Description;
            btnPayment.Enabled = !room.IsOccupied;

            // Show existing reservations summary in description (append)
            if (room.Reservations != null && room.Reservations.Count > 0)
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine();
                sb.AppendLine("=== Các đặt trước ===");
                foreach (var r in room.Reservations.OrderBy(x => x.StartDate))
                {
                    sb.AppendLine($"{r.StartDate:dd/MM/yyyy} → {r.EndDate:dd/MM/yyyy}  ({r.CustomerName ?? "khách"})");
                }
                txtDescription.Text += Environment.NewLine + sb.ToString();
            }

            // Load thumbnails list (use ImageResourceNames if set, else fallback to ResourceName)
            var list = new List<string>();
            if (room.ImageResourceNames != null && room.ImageResourceNames.Any())
                list.AddRange(room.ImageResourceNames);
            else if (!string.IsNullOrWhiteSpace(room.ResourceName))
                list.Add(room.ResourceName);

            // Clear existing thumbnails
            flThumbnails.Controls.Clear();

            // For each resource name, load image and create small thumbnail button
            foreach (var resName in list)
            {
                var img = LoadImageFromResourcesVariants(resName, 260, 160); // load or fallback
                if (img == null) continue;

                var thumb = new PictureBox
                {
                    Image = new Bitmap(img, new Size(180, 100)),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 180,
                    Height = 100,
                    Margin = new Padding(6),
                    Cursor = Cursors.Hand,
                    Tag = resName
                };
                thumb.Click += (s, e) =>
                {
                    // click => show in main
                    var tn = (s as PictureBox)?.Tag as string;
                    var big = LoadImageFromResourcesVariants(tn, 800, 600);
                    if (big != null)
                    {
                        pbMain.Image?.Dispose();
                        pbMain.Image = new Bitmap(big);
                        pbMain.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                };

                flThumbnails.Controls.Add(thumb);
            }

            // If thumbnails exist, set first as main image
            if (flThumbnails.Controls.Count > 0 && pbMain.Image == null)
            {
                var firstTag = flThumbnails.Controls[0].Tag as string;
                var big = LoadImageFromResourcesVariants(firstTag, 800, 600);
                if (big != null)
                {
                    pbMain.Image = new Bitmap(big);
                    pbMain.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            else if (pbMain.Image == null)
            {
                // fallback placeholder
                pbMain.Image = CreatePlaceholderImage(320, 240);
                pbMain.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }

        // Payment logic - now with availability check and reservation add
        private void BtnPayment_Click(object sender, EventArgs e)
        {
            // lấy ngày từ picker
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;

            if (to <= from)
            {
                MessageBox.Show("Ngày rời phải lớn hơn ngày đến.", "Ngày không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // kiểm tra hợp lệ theo reservation list
            if (!IsRoomAvailable(room, from, to))
            {
                MessageBox.Show($"Phòng {room.Name} đã được đặt trong khoảng đã chọn. Vui lòng chọn ngày khác.", "Đã có đặt trước", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // mở form thanh toán (giữ như trước)
            // nếu ThanhToanKhachHang có constructor nhận ngày, bạn có thể truyền
            var payForm = new ThanhToanKhachHang(room); // nếu muốn: new ThanhToanKhachHang(room, from, to);

            // đăng ký sự kiện RoomPaid (nếu form thanh toán raise event này)
            try
            {
                payForm.RoomPaid += (updatedRoom) =>
                {
                    if (updatedRoom == null) return;

                    // tạo reservation và thêm vào model
                    var newRes = new Reservation
                    {
                        RoomId = room.Id,
                        UserId = UserSession.CurrentUserId,   // >>> thêm dòng này <<<
                        StartDate = from,
                        EndDate = to,
                        CustomerName = UserSession.CurrentUsername ?? "Khách"
                    };
                    if (room.Reservations == null) room.Reservations = new List<Reservation>();
                    room.Reservations.Add(newRes);

                    // cập nhật trạng thái
                    room.IsOccupied = true;
                    lblStatus.Text = "Đã thuê";
                    lblStatus.ForeColor = Color.DarkRed;
                    btnPayment.Enabled = false;

                    // notify parent
                    RoomUpdated?.Invoke(room);
                };
            }
            catch
            {
                // nếu payForm không expose RoomPaid, fallback: we'll check DialogResult
            }

            var dr = payForm.ShowDialog(this);

            // nếu form thanh toán không có RoomPaid event but returns DialogResult.OK on success
            if (dr == DialogResult.OK)
            {
                // add reservation (same as above) only if none added yet for the period
                if (room.Reservations == null) room.Reservations = new List<Reservation>();

                // safety: ensure not duplicate
                if (IsRoomAvailable(room, from, to))
                {
                    var newRes = new Reservation
                    {
                        RoomId = room.Id,
                        StartDate = from,
                        EndDate = to,
                        CustomerName = UserSession.CurrentUsername ?? "Khách"
                    };
                    room.Reservations.Add(newRes);

                    room.IsOccupied = true;
                    lblStatus.Text = "Đã thuê";
                    lblStatus.ForeColor = Color.DarkRed;
                    btnPayment.Enabled = false;

                    RoomUpdated?.Invoke(room);
                }
                // else: maybe already added by RoomPaid event
            }
        }

        private bool IsRoomAvailable(Room r, DateTime requestedFrom, DateTime requestedTo)
        {
            if (r == null) return false;
            if (r.Reservations == null || r.Reservations.Count == 0) return true;

            DateTime b1 = requestedFrom.Date;
            DateTime b2 = requestedTo.Date; // exclusive by convention

            foreach (var res in r.Reservations)
            {
                DateTime a1 = res.StartDate.Date;
                DateTime a2 = res.EndDate.Date;
                // overlap if (a1 < b2) && (b1 < a2)
                if (a1 < b2 && b1 < a2)
                {
                    return false;
                }
            }
            return true;
        }

        private Image LoadImageFromResourcesVariants(string baseName, int desiredWidth = 0, int desiredHeight = 0)
        {
            if (string.IsNullOrWhiteSpace(baseName)) return null;

            Image loaded = null;
            try
            {
                var flags = System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public;
                // try exact
                var p = typeof(Properties.Resources).GetProperty(baseName, flags);
                if (p != null) loaded = p.GetValue(null, null) as Image;

                // try common suffices
                if (loaded == null)
                {
                    string[] tries = { baseName + "_jpg", baseName + "_png", baseName + "_jpeg" };
                    foreach (var tn in tries)
                    {
                        var p2 = typeof(Properties.Resources).GetProperty(tn, flags);
                        if (p2 != null) { loaded = p2.GetValue(null, null) as Image; break; }
                    }
                }

                // try default resource
                if (loaded == null)
                {
                    var dp = typeof(Properties.Resources).GetProperty("default_room", flags);
                    if (dp != null) loaded = dp.GetValue(null, null) as Image;
                }
            }
            catch
            {
                loaded = null;
            }

            if (loaded == null) return null;

            if (desiredWidth > 0 && desiredHeight > 0)
            {
                try
                {
                    return new Bitmap(loaded, desiredWidth, desiredHeight);
                }
                catch
                {
                    return (Image)loaded.Clone();
                }
            }
            return (Image)loaded.Clone();
        }

        private Image CreatePlaceholderImage(int w, int h)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(245, 250, 255));
                using (var f = new Font("Segoe UI", 10))
                using (var sb = new SolidBrush(Color.Gray))
                {
                    var txt = "No image";
                    var sz = g.MeasureString(txt, f);
                    g.DrawString(txt, f, sb, (w - sz.Width) / 2f, (h - sz.Height) / 2f);
                }
            }
            return bmp;
        }

        // keep InitializeComponent if Designer expects it
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RoomDetailForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "RoomDetailForm";
            this.Load += new System.EventHandler(this.RoomDetailForm_Load);
            this.ResumeLayout(false);

        }
        //184, 244, 228
        private void RoomDetailForm_Load(object sender, EventArgs e)
        {
           // this.BackColor = Color.FromArgb(184, 244, 228);
        }
    }
}
