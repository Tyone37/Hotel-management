using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    public partial class ManagerHome : Form
    {
        public ManagerHome()
        {
            InitializeComponent(); // Designer-managed InitializeComponent in ManagerHome.Designer.cs
            this.Load += ManagerHome_Load;
        }


        private void ManagerHome_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserSession.CurrentUsername))
                    lblWelcome.Text = $"Xin chào, {UserSession.CurrentUsername}";
            }
            catch { }

            // load dashboard by default if UC exists
            TryLoadControl("HotelManagementApp.quanly.UC_Dashboard");

            // set default active tile
            try { SetActiveTile(btnDashboardTile); } catch { }
        }

        // visually mark the active tile and reset others
        private void SetActiveTile(Button active)
        {
            if (active == null) return;
            // tile style: active/back colors (soft pink for active)
            Color activeBack = Color.FromArgb(255, 182, 193); // light pink
            Color activeFore = Color.FromArgb(34, 45, 65);
            Color normalBack = Color.FromArgb(52, 73, 94);
            Color normalFore = Color.White;

            var tiles = new[] { btnDashboardTile, btnRoomsTile, btnStaffTile, btnServicesTile, btnStatsTile };
            foreach (var t in tiles)
            {
                try
                {
                    if (t == active)
                    {
                        t.BackColor = activeBack;
                        t.ForeColor = activeFore;
                    }
                    else
                    {
                        t.BackColor = normalBack;
                        t.ForeColor = normalFore;
                    }
                }
                catch { }
            }
        }

        private void TryLoadControl(string typeFullName)
        {
            try
            {
                // attempt to find type in current assembly
                var asmName = this.GetType().Assembly.FullName;
                var t = Type.GetType(typeFullName + ", " + asmName);
                if (t != null && typeof(UserControl).IsAssignableFrom(t))
                {
                    var uc = (UserControl)Activator.CreateInstance(t);
                    ShowModuleControl(uc);
                }
            }
            catch (Exception ex)
            {
                // show exception so developer can see why control failed to load
                MessageBox.Show("Lỗi khi tải module: " + ex.Message + "\n\n" + ex.ToString(), "Lỗi tải module", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowModuleControl(UserControl uc)
        {
            if (panelMain == null)
            {
                // fallback: try find by name (should be present from Designer)
                var p = this.Controls.Find("panelMain", true).FirstOrDefault() as Panel;
                if (p == null)
                {
                    panelMain = new Panel { Dock = DockStyle.Fill };
                    this.Controls.Add(panelMain);
                    panelMain.BringToFront();
                }
                else panelMain = p;
            }

            panelMain.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc);
            uc.BringToFront();
        }

        // Tile button handlers (wired in Designer)
        private void BtnDashboardTile_Click(object sender, EventArgs e)
        {
            TryLoadControl("HotelManagementApp.quanly.UC_Dashboard");
            SetActiveTile(btnDashboardTile);
        }

        private void BtnRoomsTile_Click(object sender, EventArgs e)
        {
            TryLoadControl("HotelManagementApp.quanly.UC_QuanLyPhong");
            SetActiveTile(btnRoomsTile);
        }

        private void BtnStaffTile_Click(object sender, EventArgs e)
        {
            TryLoadControl("HotelManagementApp.quanly.UC_QuanLyNhanVien");
            SetActiveTile(btnStaffTile);
        }

        private void BtnServicesTile_Click(object sender, EventArgs e)
        {
            TryLoadControl("HotelManagementApp.quanly.UC_QuanLyDichVu");
            SetActiveTile(btnServicesTile);
        }

        private void BtnStatsTile_Click(object sender, EventArgs e)
        {
            TryLoadControl("HotelManagementApp.quanly.UC_ThongKe");
            SetActiveTile(btnStatsTile);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            UserSession.CurrentUserId = 0;
            UserSession.CurrentUsername = null;
            var lg = new HotelManagementApp.Log_in();
            lg.Show();
            this.Close();
        }

        // search button clicked on top bar - simple search by booking id
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            TrySearchBooking(txtSearch?.Text?.Trim());
        }

        // very simple search: if a UC_ThongKe is loaded we ask it to filter by booking id
        private void TrySearchBooking(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Nhập mã đặt phòng để tìm kiếm.", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // if current control in panelMain is UC_ThongKe, call a public method if exists
            try
            {
                if (panelMain.Controls.Count > 0)
                {
                    var ctrl = panelMain.Controls[0];
                    var t = ctrl.GetType();
                    var m = t.GetMethod("FilterByBookingId");
                    if (m != null)
                    {
                        m.Invoke(ctrl, new object[] { query });
                        return;
                    }
                }
            }
            catch { }

            // fallback: try to open Thống kê and filter there
            TryLoadControl("HotelManagementApp.quanly.UC_ThongKe");
            // after load, attempt again
            System.Threading.Tasks.Task.Delay(200).ContinueWith(_ =>
            {
                this.Invoke(new Action(() =>
                {
                    TrySearchBooking(query);
                }));
            });
        }

        private void lblUserSmall_Click(object sender, EventArgs e)
        {

        }
    }
}