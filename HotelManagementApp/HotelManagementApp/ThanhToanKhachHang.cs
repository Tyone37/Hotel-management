using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class ThanhToanKhachHang : Form
    {
        private Room currentRoom;
        // phát khi phòng đã được thanh toán; truyền về Room đã được cập nhật
        public event Action<Room> RoomPaid;


        public ThanhToanKhachHang()
        {
            InitializeComponent();
        }

        // Thêm constructor nhận Room
        public ThanhToanKhachHang(Room room) : this()
        {
            Populate(room);
        }

        // Nếu bạn muốn khởi tạo rồi set sau, dùng method này
        public void Populate(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            currentRoom = room;

            // --- GÁN GIÁ TRỊ VÀO CONTROL (Đổi tên control nếu project bạn dùng tên khác) ---
            // Ví dụ: trong Designer bạn có PictureBox tên pictureBoxRoom,
            // Label tên lblRoomName, lblStatus, lblPrice, TextBox txtDescription
            //
            // Nếu tên control khác, đổi tương ứng.

            // Tên phòng
            if (this.Controls.Find("lblRoomName", true).FirstOrDefault() is Label lblRoomName)
                lblRoomName.Text = $"Tên phòng: {room.Name}";

            // Trạng thái
            if (this.Controls.Find("lblRoomStatus", true).FirstOrDefault() is Label lblRoomStatus)
            {
                lblRoomStatus.Text = $"Tình trạng: {(room.IsOccupied ? "Đã thuê" : "Sẵn sàng")}";
                lblRoomStatus.ForeColor = room.IsOccupied ? Color.DarkRed : Color.DarkGreen;
            }

            // Giá
            if (this.Controls.Find("lblRoomPrice", true).FirstOrDefault() is Label lblRoomPrice)
                lblRoomPrice.Text = $"Chi phí: {room.Price:N0}đ/đêm";

            // Mô tả
            if (this.Controls.Find("txtRoomDescription", true).FirstOrDefault() is TextBox txtRoomDescription)
                txtRoomDescription.Text = string.IsNullOrWhiteSpace(room.Description) ? "(Không có mô tả)" : room.Description;

            // Ảnh chính - PictureBox: pictureBoxRoom (hoặc đổi tên)
            if (this.Controls.Find("pictureBoxRoom", true).FirstOrDefault() is PictureBox pb)
            {
                // load ảnh từ Resources theo room.ResourceName (hàm helper bên dưới)
                var img = LoadImageFromResourcesVariants(room.ResourceName, 600, 400);
                if (img != null)
                {
                    pb.Image?.Dispose();
                    pb.Image = img;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    // placeholder nếu không có ảnh
                    pb.Image?.Dispose();
                    pb.Image = CreatePlaceholderImage(pb.Width, pb.Height);
                    pb.SizeMode = PictureBoxSizeMode.CenterImage;
                }
            }

            // Nếu bạn muốn hiển thị danh sách ảnh chi tiết ở form thanh toán,
            // bạn có thể có một FlowLayoutPanel tên flowThumbnails trong Designer.
            if (this.Controls.Find("flowThumbnails", true).FirstOrDefault() is FlowLayoutPanel fl)
            {
                fl.Controls.Clear();
                // ưu tiên ImageResourceNames nếu có; nếu không dùng ResourceName
                var list = room.ImageResourceNames != null && room.ImageResourceNames.Any()
      ? room.ImageResourceNames
      : new List<string> { room.ResourceName };

                foreach (var res in list)
                {
                    var small = LoadImageFromResourcesVariants(res, 180, 120) ?? CreatePlaceholderImage(180, 120);
                    var thumb = new PictureBox
                    {
                        Image = new Bitmap(small),
                        Width = 120,
                        Height = 80,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Margin = new Padding(6),
                        Cursor = Cursors.Hand,
                        Tag = res
                    };
                    thumb.Click += (s, e) =>
                    {
                        // khi click thumbnail đổi ảnh lớn
                        if (this.Controls.Find("pictureBoxRoom", true).FirstOrDefault() is PictureBox pbMain)
                        {
                            var tn = (s as PictureBox)?.Tag as string;
                            var big = LoadImageFromResourcesVariants(tn, 600, 400) ?? CreatePlaceholderImage(pbMain.Width, pbMain.Height);
                            pbMain.Image?.Dispose();
                            pbMain.Image = new Bitmap(big);
                            pbMain.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    };
                    fl.Controls.Add(thumb);
                }
            }
        }

        // Helper giống các code trước: thử lấy resource theo tên, tên + _jpg/_png, fallback default
        private Image LoadImageFromResourcesVariants(string baseName, int desiredWidth = 0, int desiredHeight = 0)
        {
            if (string.IsNullOrWhiteSpace(baseName)) return null;
            Image loaded = null;
            try
            {
                var flags = System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public;
                var p = typeof(Properties.Resources).GetProperty(baseName, flags);
                if (p != null) loaded = p.GetValue(null, null) as Image;

                if (loaded == null)
                {
                    string[] tries = { baseName + "_jpg", baseName + "_png", baseName + "_jpeg" };
                    foreach (var tn in tries)
                    {
                        var p2 = typeof(Properties.Resources).GetProperty(tn, flags);
                        if (p2 != null) { loaded = p2.GetValue(null, null) as Image; break; }
                    }
                }

                if (loaded == null)
                {
                    var dp = typeof(Properties.Resources).GetProperty("default_room", flags);
                    if (dp != null) loaded = dp.GetValue(null, null) as Image;
                }
            }
            catch { loaded = null; }

            if (loaded == null) return null;
            if (desiredWidth > 0 && desiredHeight > 0)
            {
                try { return new Bitmap(loaded, desiredWidth, desiredHeight); }
                catch { return (Image)loaded.Clone(); }
            }
            return (Image)loaded.Clone();
        }

        private Image CreatePlaceholderImage(int w, int h)
        {
            var bmp = new Bitmap(Math.Max(1, w), Math.Max(1, h));
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(245, 250, 255));
                using (var f = new Font("Segoe UI", 9))
                using (var sb = new SolidBrush(Color.Gray))
                {
                    var txt = "No image";
                    var sz = g.MeasureString(txt, f);
                    g.DrawString(txt, f, sb, (bmp.Width - sz.Width) / 2f, (bmp.Height - sz.Height) / 2f);
                }
            }
            return bmp;
        }

        // Event mẫu: khi người bấm nút xác nhận thanh toán trên form này, bạn có thể
        // raise event, hoặc trực tiếp cập nhật currentRoom.IsOccupied = true và Close.
        

        // keep existing auto-generated event handlers
        private void label5_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void ThanhToanKhachHang_Load(object sender, EventArgs e) {
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
        }

      

        private void flowThumbnails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click_2_Click(object sender, EventArgs e)
        {
            if (currentRoom == null) return;

            // TODO: thực hiện logic thanh toán (API / save DB) ở đây

            // đánh dấu đã thuê
            currentRoom.IsOccupied = true;

            // phát event cho caller (RoomDetailForm hoặc KhachHangDatPhong)
            RoomPaid?.Invoke(currentRoom);

            // ✨ Hiện thông báo
            MessageBox.Show(
                "Thanh toán thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // trả DialogResult = OK để caller biết đã thanh toán
            this.DialogResult = DialogResult.OK;

            // đóng form
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
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
