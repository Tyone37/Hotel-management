using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            // export currently selected tab's grid to CSV
            try
            {
                DataTable dt = null;
                if (tabStats != null && tabStats.SelectedTab != null)
                {
                    var t = tabStats.SelectedTab.Text;
                    if (t.Contains("Doanh thu") && dgvRevenueByDay != null) dt = dgvRevenueByDay.DataSource as DataTable;
                    else if (t.Contains("Chi tiết") && dgvDetail != null) dt = dgvDetail.DataSource as DataTable;
                    else if (t.Contains("Tần suất") && dgvRoomUsage != null) dt = dgvRoomUsage.DataSource as DataTable;
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.", "Xuất báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv";
                    sfd.FileName = "report.csv";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportDataTableToCsv(dt, sfd.FileName);
                        MessageBox.Show("Xuất báo cáo thành công.", "Xuất báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportDataTableToCsv(DataTable dt, string path)
        {
            var sb = new System.Text.StringBuilder();
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(f => "\"" + (f?.ToString().Replace("\"", "\"\"") ?? "") + "\"");
                sb.AppendLine(string.Join(",", fields));
            }
            System.IO.File.WriteAllText(path, sb.ToString(), System.Text.Encoding.UTF8);
        }

        // Public method called by ManagerHome search: filter by booking id (simple contains match)
        public void FilterByBookingId(string bookingId)
        {
            if (string.IsNullOrWhiteSpace(bookingId)) return;
            try
            {
                // try to filter detail grid if available
                if (dgvDetail != null && dgvDetail.DataSource is DataTable dt)
                {
                    // BookingID is integer in DB - try parse
                    if (int.TryParse(bookingId, out int id))
                    {
                        var rows = dt.Select($"BookingID = {id}");
                        dgvDetail.DataSource = rows.Length > 0 ? rows.CopyToDataTable() : dt.Clone();
                    }
                    else
                    {
                        // fallback: no numeric id provided, show empty
                        dgvDetail.DataSource = dt.Clone();
                    }
                }
            }
            catch { }
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

            // Adjusted to match existing schema: BookingID, UserID, RoomID, CheckInDate, CheckOutDate, TotalAmount, Status, CreatedAt
            string sqlDetail = "SELECT BookingID, UserID, RoomID, CheckInDate, CheckOutDate, TotalAmount, Status, CreatedAt FROM Bookings WHERE CAST(CheckOutDate AS DATE) BETWEEN @from AND @to ORDER BY CheckOutDate";
            string sqlRevenueByDay = "SELECT CAST(CheckOutDate AS DATE) AS [Date], ISNULL(SUM(TotalAmount),0) AS Revenue FROM Bookings WHERE CAST(CheckOutDate AS DATE) BETWEEN @from AND @to GROUP BY CAST(CheckOutDate AS DATE) ORDER BY [Date]";
            string sqlRoomUsage = "SELECT RoomID, COUNT(1) AS Uses FROM Bookings WHERE CAST(CheckOutDate AS DATE) BETWEEN @from AND @to GROUP BY RoomID ORDER BY Uses DESC";

            try
            {
                DataTable dtDetail = GetDataTable(sqlDetail, from, to);
                DataTable dtRevenue = GetDataTable(sqlRevenueByDay, from, to);
                DataTable dtUsage = GetDataTable(sqlRoomUsage, from, to);

                // bind to grids (if created)
                if (dgvDetail != null) dgvDetail.DataSource = dtDetail ?? new DataTable();
                if (dgvRevenueByDay != null) dgvRevenueByDay.DataSource = dtRevenue ?? new DataTable();
                if (dgvRoomUsage != null) dgvRoomUsage.DataSource = dtUsage ?? new DataTable();

                // bind chart: simple column chart of revenue by date
                try
                {
                    if (this.chartRevenue != null && dtRevenue != null)
                    {
                        this.chartRevenue.Series.Clear();
                        var s = this.chartRevenue.Series.Add("Doanh thu");
                        s.ChartType = SeriesChartType.Column;
                        s.XValueMember = "Date";
                        s.YValueMembers = "Revenue";
                        this.chartRevenue.DataSource = dtRevenue;
                        this.chartRevenue.DataBind();
                    }
                }
                catch { }

                // compute total revenue (use TotalAmount column)
                decimal total = 0;
                if (dtDetail != null && dtDetail.Columns.Contains("TotalAmount"))
                {
                    foreach (DataRow r in dtDetail.Rows)
                    {
                        if (r["TotalAmount"] == DBNull.Value) continue;
                        decimal v;
                        if (decimal.TryParse(r["TotalAmount"].ToString(), out v)) total += v;
                    }
                }
                else if (dtRevenue != null && dtRevenue.Columns.Contains("Revenue"))
                {
                    foreach (DataRow r in dtRevenue.Rows)
                    {
                        decimal v;
                        if (decimal.TryParse(r["Revenue"]?.ToString(), out v)) total += v;
                    }
                }

                lblTotalRevenue.Text = string.Format("Tổng doanh thu ({0:d} - {1:d}): {2:N0}", from, to, total);
                if (tabStats != null && tabStats.TabPages.Count > 0) tabStats.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper: execute parameterized query and return DataTable
        private DataTable GetDataTable(string sql, DateTime from, DateTime to)
        {
            var dt = new DataTable();
            using (var conn = kn.LayKetNoi())
            using (var cmd = new SqlCommand(sql, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@from", from.Date);
                cmd.Parameters.AddWithValue("@to", to.Date);
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }

        private void UC_ThongKe_Load(object sender, EventArgs e)
        {

        }
    }
}