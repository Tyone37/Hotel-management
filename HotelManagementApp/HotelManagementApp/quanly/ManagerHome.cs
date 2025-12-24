using System;
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
            catch
            {
                // ignore - control may not exist yet
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
        private void BtnDashboardTile_Click(object sender, EventArgs e) => TryLoadControl("HotelManagementApp.quanly.UC_Dashboard");
        private void BtnRoomsTile_Click(object sender, EventArgs e) => TryLoadControl("HotelManagementApp.quanly.UC_QuanLyPhong");
        private void BtnStaffTile_Click(object sender, EventArgs e)
        {
            QL_NhanVien qL_NhanVien = new QL_NhanVien();
            qL_NhanVien.Show();
        }
        private void BtnServicesTile_Click(object sender, EventArgs e) => TryLoadControl("HotelManagementApp.quanly.UC_QuanLyDichVu");
        private void BtnStatsTile_Click(object sender, EventArgs e) => TryLoadControl("HotelManagementApp.quanly.UC_ThongKe");

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            UserSession.CurrentUserId = 0;
            UserSession.CurrentUsername = null;
            var lg = new HotelManagementApp.Log_in();
            lg.Show();
            this.Close();
        }
    }
}