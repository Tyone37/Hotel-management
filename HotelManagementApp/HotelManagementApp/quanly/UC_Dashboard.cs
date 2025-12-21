using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    public partial class UC_Dashboard : UserControl
    {
        private KetNoi kn = new KetNoi();

        public UC_Dashboard()
        {
            InitializeComponent();
            LoadOverview();
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            // Designer yêu cầu handler này — gọi lại LoadOverview để đảm bảo UI cập nhật khi control load từ Designer/runtime
            try
            {
                LoadOverview();
            }
            catch
            {
                // ignore errors at design-time
            }
        }

        private void LoadOverview()
        {
            try
            {
                // Rooms count
                try
                {
                    var dtRooms = kn.LayDuLieu("SELECT COUNT(1) AS Total FROM Phong");
                    if (dtRooms.Rows.Count > 0) lblRoomsCount.Text = "Phòng: " + dtRooms.Rows[0]["Total"].ToString();
                }
                catch { lblRoomsCount.Text = "Phòng: [N/A]"; }

                // Bookings today + revenue today (adjust table/columns if different)
                string today = DateTime.Today.ToString("yyyy-MM-dd");
                try
                {
                    var dt = kn.LayDuLieu($"SELECT COUNT(1) AS Cnt, ISNULL(SUM(Total),0) AS Revenue FROM Bookings WHERE CAST(PaymentDate AS DATE) = '{today}'");
                    if (dt.Rows.Count > 0)
                    {
                        lblBookingsToday.Text = "Đặt hôm nay: " + dt.Rows[0]["Cnt"].ToString();
                        lblRevenueToday.Text = "Doanh thu hôm nay: " + dt.Rows[0]["Revenue"].ToString();
                    }
                }
                catch { lblBookingsToday.Text = "Đặt hôm nay: [N/A]"; lblRevenueToday.Text = "Doanh thu hôm nay: [N/A]"; }

                // Recent bookings (top 20)
                try
                {
                    var dtRecent = kn.LayDuLieu("SELECT TOP 20 Id, RoomNumber, PaymentDate, Total FROM Bookings ORDER BY PaymentDate DESC");
                    dgvRecent.DataSource = dtRecent;
                }
                catch
                {
                    dgvRecent.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tổng quan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}