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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.flowTiles = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDashboardTile = new System.Windows.Forms.Button();
            this.btnRoomsTile = new System.Windows.Forms.Button();
            this.btnStaffTile = new System.Windows.Forms.Button();
            this.btnServicesTile = new System.Windows.Forms.Button();
            this.btnStatsTile = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLeft.SuspendLayout();
            this.flowTiles.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.panelLeft.Controls.Add(this.flowTiles);
            this.panelLeft.Controls.Add(this.lblTitle);
            this.panelLeft.Controls.Add(this.btnLogout);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(12);
            this.panelLeft.Size = new System.Drawing.Size(300, 640);
            this.panelLeft.TabIndex = 2;
            // 
            // flowTiles
            // 
            this.flowTiles.AutoScroll = true;
            this.flowTiles.BackColor = System.Drawing.Color.Transparent;
            this.flowTiles.Controls.Add(this.btnDashboardTile);
            this.flowTiles.Controls.Add(this.btnRoomsTile);
            this.flowTiles.Controls.Add(this.btnStaffTile);
            this.flowTiles.Controls.Add(this.btnServicesTile);
            this.flowTiles.Controls.Add(this.btnStatsTile);
            this.flowTiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTiles.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowTiles.Location = new System.Drawing.Point(12, 76);
            this.flowTiles.Name = "flowTiles";
            this.flowTiles.Padding = new System.Windows.Forms.Padding(8);
            this.flowTiles.Size = new System.Drawing.Size(276, 512);
            this.flowTiles.TabIndex = 0;
            this.flowTiles.WrapContents = false;
            // 
            // btnDashboardTile
            // 
            this.btnDashboardTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnDashboardTile.FlatAppearance.BorderSize = 0;
            this.btnDashboardTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboardTile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDashboardTile.ForeColor = System.Drawing.Color.White;
            this.btnDashboardTile.Location = new System.Drawing.Point(16, 16);
            this.btnDashboardTile.Margin = new System.Windows.Forms.Padding(8);
            this.btnDashboardTile.Name = "btnDashboardTile";
            this.btnDashboardTile.Size = new System.Drawing.Size(260, 90);
            this.btnDashboardTile.TabIndex = 0;
            this.btnDashboardTile.Text = "Bảng điều khiển\r\n\r\nXem tổng quan hoạt động";
            this.btnDashboardTile.UseVisualStyleBackColor = false;
            this.btnDashboardTile.Click += new System.EventHandler(this.BtnDashboardTile_Click);
            // 
            // btnRoomsTile
            // 
            this.btnRoomsTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnRoomsTile.FlatAppearance.BorderSize = 0;
            this.btnRoomsTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomsTile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRoomsTile.ForeColor = System.Drawing.Color.White;
            this.btnRoomsTile.Location = new System.Drawing.Point(16, 122);
            this.btnRoomsTile.Margin = new System.Windows.Forms.Padding(8);
            this.btnRoomsTile.Name = "btnRoomsTile";
            this.btnRoomsTile.Size = new System.Drawing.Size(260, 90);
            this.btnRoomsTile.TabIndex = 1;
            this.btnRoomsTile.Text = "Quản lý phòng\r\n\r\nThêm / Sửa / Xóa";
            this.btnRoomsTile.UseVisualStyleBackColor = false;
            this.btnRoomsTile.Click += new System.EventHandler(this.BtnRoomsTile_Click);
            // 
            // btnStaffTile
            // 
            this.btnStaffTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnStaffTile.FlatAppearance.BorderSize = 0;
            this.btnStaffTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaffTile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStaffTile.ForeColor = System.Drawing.Color.White;
            this.btnStaffTile.Location = new System.Drawing.Point(16, 228);
            this.btnStaffTile.Margin = new System.Windows.Forms.Padding(8);
            this.btnStaffTile.Name = "btnStaffTile";
            this.btnStaffTile.Size = new System.Drawing.Size(260, 90);
            this.btnStaffTile.TabIndex = 2;
            this.btnStaffTile.Text = "Quản lý nhân viên\r\n\r\nTạo / Xóa tài khoản";
            this.btnStaffTile.UseVisualStyleBackColor = false;
            this.btnStaffTile.Click += new System.EventHandler(this.btnStaffTile_Click);
            // 
            // btnServicesTile
            // 
            this.btnServicesTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnServicesTile.FlatAppearance.BorderSize = 0;
            this.btnServicesTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServicesTile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnServicesTile.ForeColor = System.Drawing.Color.White;
            this.btnServicesTile.Location = new System.Drawing.Point(16, 334);
            this.btnServicesTile.Margin = new System.Windows.Forms.Padding(8);
            this.btnServicesTile.Name = "btnServicesTile";
            this.btnServicesTile.Size = new System.Drawing.Size(260, 90);
            this.btnServicesTile.TabIndex = 3;
            this.btnServicesTile.Text = "Quản lý dịch vụ\r\n\r\nThêm / Sửa / Xóa dịch vụ";
            this.btnServicesTile.UseVisualStyleBackColor = false;
            this.btnServicesTile.Click += new System.EventHandler(this.BtnServicesTile_Click);
            // 
            // btnStatsTile
            // 
            this.btnStatsTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.btnStatsTile.FlatAppearance.BorderSize = 0;
            this.btnStatsTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatsTile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStatsTile.ForeColor = System.Drawing.Color.White;
            this.btnStatsTile.Location = new System.Drawing.Point(16, 440);
            this.btnStatsTile.Margin = new System.Windows.Forms.Padding(8);
            this.btnStatsTile.Name = "btnStatsTile";
            this.btnStatsTile.Size = new System.Drawing.Size(260, 90);
            this.btnStatsTile.TabIndex = 4;
            this.btnStatsTile.Text = "Thống kê\r\n\r\nDoanh thu & Tần suất phòng";
            this.btnStatsTile.UseVisualStyleBackColor = false;
            this.btnStatsTile.Click += new System.EventHandler(this.BtnStatsTile_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(276, 64);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "QUẢN LÝ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(12, 588);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(276, 40);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.lblWelcome);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(300, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(12);
            this.panelTop.Size = new System.Drawing.Size(800, 60);
            this.panelTop.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblWelcome.Location = new System.Drawing.Point(12, 12);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(776, 36);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Xin chào, Quản lý";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(300, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(16);
            this.panelMain.Size = new System.Drawing.Size(800, 580);
            this.panelMain.TabIndex = 0;
            // 
            // ManagerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 640);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.Name = "ManagerHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang chủ Quản lý - Khách sạn Lý Anh";
            this.panelLeft.ResumeLayout(false);
            this.flowTiles.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}