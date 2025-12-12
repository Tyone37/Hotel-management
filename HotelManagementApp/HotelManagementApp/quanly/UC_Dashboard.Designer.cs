using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    partial class UC_Dashboard
    {
        private IContainer components = null;
        private Label lblTitle;
        private Panel pnlTiles;
        private Label lblRoomsCount;
        private Label lblBookingsToday;
        private Label lblRevenueToday;
        private DataGridView dgvRecent;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTiles = new System.Windows.Forms.Panel();
            this.lblRoomsCount = new System.Windows.Forms.Label();
            this.lblBookingsToday = new System.Windows.Forms.Label();
            this.lblRevenueToday = new System.Windows.Forms.Label();
            this.dgvRecent = new System.Windows.Forms.DataGridView();
            this.pnlTiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(222, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bảng điều khiển";
            // 
            // pnlTiles
            // 
            this.pnlTiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTiles.Controls.Add(this.lblRoomsCount);
            this.pnlTiles.Controls.Add(this.lblBookingsToday);
            this.pnlTiles.Controls.Add(this.lblRevenueToday);
            this.pnlTiles.Location = new System.Drawing.Point(16, 56);
            this.pnlTiles.Name = "pnlTiles";
            this.pnlTiles.Size = new System.Drawing.Size(1981, 120);
            this.pnlTiles.TabIndex = 1;
            // 
            // lblRoomsCount
            // 
            this.lblRoomsCount.AutoSize = true;
            this.lblRoomsCount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblRoomsCount.Location = new System.Drawing.Point(8, 12);
            this.lblRoomsCount.Name = "lblRoomsCount";
            this.lblRoomsCount.Size = new System.Drawing.Size(89, 28);
            this.lblRoomsCount.TabIndex = 0;
            this.lblRoomsCount.Text = "Phòng: 0";
            // 
            // lblBookingsToday
            // 
            this.lblBookingsToday.AutoSize = true;
            this.lblBookingsToday.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblBookingsToday.Location = new System.Drawing.Point(360, 12);
            this.lblBookingsToday.Name = "lblBookingsToday";
            this.lblBookingsToday.Size = new System.Drawing.Size(144, 28);
            this.lblBookingsToday.TabIndex = 1;
            this.lblBookingsToday.Text = "Đặt hôm nay: 0";
            // 
            // lblRevenueToday
            // 
            this.lblRevenueToday.AutoSize = true;
            this.lblRevenueToday.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblRevenueToday.Location = new System.Drawing.Point(720, 12);
            this.lblRevenueToday.Name = "lblRevenueToday";
            this.lblRevenueToday.Size = new System.Drawing.Size(205, 28);
            this.lblRevenueToday.TabIndex = 2;
            this.lblRevenueToday.Text = "Doanh thu hôm nay: 0";
            // 
            // dgvRecent
            // 
            this.dgvRecent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecent.ColumnHeadersHeight = 29;
            this.dgvRecent.Location = new System.Drawing.Point(16, 188);
            this.dgvRecent.Name = "dgvRecent";
            this.dgvRecent.ReadOnly = true;
            this.dgvRecent.RowHeadersWidth = 51;
            this.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecent.Size = new System.Drawing.Size(1981, 904);
            this.dgvRecent.TabIndex = 2;
            // 
            // UC_Dashboard
            // 
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlTiles);
            this.Controls.Add(this.dgvRecent);
            this.Name = "UC_Dashboard";
            this.Size = new System.Drawing.Size(1069, 544);
            this.Load += new System.EventHandler(this.UC_Dashboard_Load);
            this.pnlTiles.ResumeLayout(false);
            this.pnlTiles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}