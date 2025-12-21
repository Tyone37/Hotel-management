using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    partial class ManagerHome
    {
        private IContainer components = null;
        private Panel panelLeft;
        private Panel panelTop;
        private FlowLayoutPanel flowTiles;
        private Button btnDashboardTile;
        private Button btnRoomsTile;
        private Button btnStaffTile;
        private Button btnServicesTile;
        private Button btnStatsTile;
        private Button btnLogout;
        private Label lblTitle;
        private Label lblWelcome;
        private Panel panelMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.panelLeft = new Panel();
            this.flowTiles = new FlowLayoutPanel();
            this.lblTitle = new Label();
            this.btnDashboardTile = new Button();
            this.btnRoomsTile = new Button();
            this.btnStaffTile = new Button();
            this.btnServicesTile = new Button();
            this.btnStatsTile = new Button();
            this.btnLogout = new Button();
            this.panelTop = new Panel();
            this.lblWelcome = new Label();
            this.panelMain = new Panel();

            this.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelTop.SuspendLayout();

            // panelLeft
            this.panelLeft.BackColor = Color.FromArgb(34, 45, 65);
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 300;
            this.panelLeft.Padding = new Padding(12);

            // lblTitle
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.Text = "QUẢN LÝ";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.AutoSize = false;
            this.lblTitle.Height = 64;
            this.lblTitle.Dock = DockStyle.Top;

            // flowTiles
            this.flowTiles.Dock = DockStyle.Fill;
            this.flowTiles.FlowDirection = FlowDirection.TopDown;
            this.flowTiles.WrapContents = false;
            this.flowTiles.AutoScroll = true;
            this.flowTiles.Padding = new Padding(8);
            this.flowTiles.BackColor = Color.Transparent;

            // tile style constants
            Size tileSize = new Size(260, 90);
            Color tileBack = Color.FromArgb(52, 73, 94);
            Font tileFont = new Font("Segoe UI", 10F, FontStyle.Bold);

            // btnDashboardTile
            this.btnDashboardTile.Size = tileSize;
            this.btnDashboardTile.Text = "Bảng điều khiển" + Environment.NewLine + Environment.NewLine + "Xem tổng quan hoạt động";
            this.btnDashboardTile.BackColor = tileBack;
            this.btnDashboardTile.ForeColor = Color.White;
            this.btnDashboardTile.FlatStyle = FlatStyle.Flat;
            this.btnDashboardTile.FlatAppearance.BorderSize = 0;
            this.btnDashboardTile.Font = tileFont;
            this.btnDashboardTile.Margin = new Padding(8);
            this.btnDashboardTile.Click += new EventHandler(this.BtnDashboardTile_Click);

            // btnRoomsTile
            this.btnRoomsTile.Size = tileSize;
            this.btnRoomsTile.Text = "Quản lý phòng" + Environment.NewLine + Environment.NewLine + "Thêm / Sửa / Xóa";
            this.btnRoomsTile.BackColor = tileBack;
            this.btnRoomsTile.ForeColor = Color.White;
            this.btnRoomsTile.FlatStyle = FlatStyle.Flat;
            this.btnRoomsTile.FlatAppearance.BorderSize = 0;
            this.btnRoomsTile.Font = tileFont;
            this.btnRoomsTile.Margin = new Padding(8);
            this.btnRoomsTile.Click += new EventHandler(this.BtnRoomsTile_Click);

            // btnStaffTile
            this.btnStaffTile.Size = tileSize;
            this.btnStaffTile.Text = "Quản lý nhân viên" + Environment.NewLine + Environment.NewLine + "Tạo / Xóa tài khoản";
            this.btnStaffTile.BackColor = tileBack;
            this.btnStaffTile.ForeColor = Color.White;
            this.btnStaffTile.FlatStyle = FlatStyle.Flat;
            this.btnStaffTile.FlatAppearance.BorderSize = 0;
            this.btnStaffTile.Font = tileFont;
            this.btnStaffTile.Margin = new Padding(8);
            this.btnStaffTile.Click += new EventHandler(this.BtnStaffTile_Click);

            // btnServicesTile
            this.btnServicesTile.Size = tileSize;
            this.btnServicesTile.Text = "Quản lý dịch vụ" + Environment.NewLine + Environment.NewLine + "Thêm / Sửa / Xóa dịch vụ";
            this.btnServicesTile.BackColor = tileBack;
            this.btnServicesTile.ForeColor = Color.White;
            this.btnServicesTile.FlatStyle = FlatStyle.Flat;
            this.btnServicesTile.FlatAppearance.BorderSize = 0;
            this.btnServicesTile.Font = tileFont;
            this.btnServicesTile.Margin = new Padding(8);
            this.btnServicesTile.Click += new EventHandler(this.BtnServicesTile_Click);

            // btnStatsTile
            this.btnStatsTile.Size = tileSize;
            this.btnStatsTile.Text = "Thống kê" + Environment.NewLine + Environment.NewLine + "Doanh thu & Tần suất phòng";
            this.btnStatsTile.BackColor = tileBack;
            this.btnStatsTile.ForeColor = Color.White;
            this.btnStatsTile.FlatStyle = FlatStyle.Flat;
            this.btnStatsTile.FlatAppearance.BorderSize = 0;
            this.btnStatsTile.Font = tileFont;
            this.btnStatsTile.Margin = new Padding(8);
            this.btnStatsTile.Click += new EventHandler(this.BtnStatsTile_Click);

            // btnLogout
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Height = 40;
            this.btnLogout.Dock = DockStyle.Bottom;
            this.btnLogout.BackColor = Color.FromArgb(200, 50, 60);
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.Click += new EventHandler(this.BtnLogout_Click);

            // add controls to panelLeft in order: title, flow, logout
            this.panelLeft.Controls.Add(this.flowTiles);
            this.panelLeft.Controls.Add(this.lblTitle);
            this.panelLeft.Controls.Add(this.btnLogout);

            // add tiles into flowTiles
            this.flowTiles.Controls.Add(this.btnDashboardTile);
            this.flowTiles.Controls.Add(this.btnRoomsTile);
            this.flowTiles.Controls.Add(this.btnStaffTile);
            this.flowTiles.Controls.Add(this.btnServicesTile);
            this.flowTiles.Controls.Add(this.btnStatsTile);

            // panelTop
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.BackColor = Color.WhiteSmoke;
            this.panelTop.Padding = new Padding(12);

            // lblWelcome
            this.lblWelcome.Dock = DockStyle.Fill;
            this.lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
            this.lblWelcome.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.lblWelcome.Text = "Xin chào, Quản lý";
            this.lblWelcome.AutoSize = false;
            this.panelTop.Controls.Add(this.lblWelcome);

            // panelMain
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = Color.White;
            this.panelMain.Padding = new Padding(16);

            // ManagerHome form
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 640);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Trang chủ Quản lý - Khách sạn Lý Anh";
            this.Name = "ManagerHome";

            this.panelTop.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}