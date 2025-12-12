using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    public partial class UC_ThongKe : UserControl
    {
        private KetNoi kn = new KetNoi();

        // dynamic controls (we create a TabControl with three grids)
        private TabControl tabStats;
        private DataGridView dgvRevenueByDay;
        private DataGridView dgvDetail;
        private DataGridView dgvRoomUsage;

        public UC_ThongKe()
        {
            InitializeComponent();

            // wire existing button
            btnLoad.Click += BtnLoad_Click;

            // Replace single designer grid by a TabControl with 3 tabs
            InitializeTabGrids();
        }

        private void InitializeTabGrids()
        {
            try
            {
                // remove designer grid if present (we'll use dynamic grids)
                if (this.Controls.Contains(dgvStats))
                {
                    this.Controls.Remove(dgvStats);
                }

                // create tab control positioned where dgvStats was in Designer
                tabStats = new TabControl
                {
                    Location = new System.Drawing.Point(16, 100),
                    Size = new System.Drawing.Size(1060, 360),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                };

                // revenue by day grid
                dgvRevenueByDay = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                // detailed records grid
                dgvDetail = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                // room usage grid
                dgvRoomUsage = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };

                // create pages
                var pageRevenue = new TabPage("Doanh thu theo ngày") { Padding = new Padding(6) };
                var pageDetail = new TabPage("Chi tiết giao dịch") { Padding = new Padding(6) };
                var pageUsage = new TabPage("Tần suất sử dụng phòng") { Padding = new Padding(6) };

                pageRevenue.Controls.Add(dgvRevenueByDay);
                pageDetail.Controls.Add(dgvDetail);
                pageUsage.Controls.Add(dgvRoomUsage);

                tabStats.TabPages.Add(pageRevenue);
                tabStats.TabPages.Add(pageDetail);
                tabStats.TabPages.Add(pageUsage);

                this.Controls.Add(tabStats);
                tabStats.BringToFront();
            }
            catch
            {
                // non-fatal: keep designer grid if dynamic creation fails
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;
            if (to < from)
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NOTE: Adjust table/column names below to match your DB schema.
            // Assumed table: Bookings (Id, RoomNumber, PaymentDate, Total)
            string whereDate = $"CAST(PaymentDate AS DATE) BETWEEN '{from:yyyy-MM-dd}' AND '{to:yyyy-MM-dd}'";

            string sqlDetail = $"SELECT Id, RoomNumber, PaymentDate, Total FROM Bookings WHERE {whereDate} ORDER BY PaymentDate";
            string sqlRevenueByDay = $"SELECT CAST(PaymentDate AS DATE) AS [Date], ISNULL(SUM(Total),0) AS Revenue FROM Bookings WHERE {whereDate} GROUP BY CAST(PaymentDate AS DATE) ORDER BY [Date]";
            string sqlRoomUsage = $"SELECT RoomNumber, COUNT(1) AS Uses FROM Bookings WHERE {whereDate} GROUP BY RoomNumber ORDER BY Uses DESC";

            try
            {
                // load detail
                DataTable dtDetail = null;
                try { dtDetail = kn.LayDuLieu(sqlDetail); }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải chi tiết giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // load revenue by day
                DataTable dtRevenue = null;
                try { dtRevenue = kn.LayDuLieu(sqlRevenueByDay); }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải doanh thu theo ngày: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // load room usage
                DataTable dtUsage = null;
                try { dtUsage = kn.LayDuLieu(sqlRoomUsage); }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải tần suất phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // bind to grids (if created)
                if (dgvDetail != null && dtDetail != null) dgvDetail.DataSource = dtDetail;
                if (dgvRevenueByDay != null && dtRevenue != null) dgvRevenueByDay.DataSource = dtRevenue;
                if (dgvRoomUsage != null && dtUsage != null) dgvRoomUsage.DataSource = dtUsage;

                // compute total revenue (from detail or revenue table)
                decimal total = 0;
                if (dtDetail != null && dtDetail.Columns.Cast<DataColumn>().Any(c => string.Equals(c.ColumnName, "Total", StringComparison.OrdinalIgnoreCase)))
                {
                    foreach (DataRow r in dtDetail.Rows)
                    {
                        if (decimal.TryParse(r["Total"]?.ToString(), out decimal v)) total += v;
                    }
                }
                else if (dtRevenue != null && dtRevenue.Columns.Cast<DataColumn>().Any(c => string.Equals(c.ColumnName, "Revenue", StringComparison.OrdinalIgnoreCase)))
                {
                    foreach (DataRow r in dtRevenue.Rows)
                    {
                        if (decimal.TryParse(r["Revenue"]?.ToString(), out decimal v)) total += v;
                    }
                }

                lblTotalRevenue.Text = $"Tổng doanh thu ({from:d} - {to:d}): {total:N0}";
                // switch to revenue tab by default
                if (tabStats != null && tabStats.TabPages.Count > 0) tabStats.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}