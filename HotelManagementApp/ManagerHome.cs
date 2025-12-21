csharp HotelManagementApp\quanly\ManagerHome.Designer.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    partial class ManagerHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Controls
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.FlowLayoutPanel flowTiles;
        private System.Windows.Forms.Button btnDashboardTile;
        private System.Windows.Forms.Button btnRoomsTile;
        private System.Windows.Forms.Button btnStaffTile;
        private System.Windows.Forms.Button btnServicesTile;
        private System.Windows.Forms.Button btnStatsTile;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panelMain; // <--- Add this so Designer knows the field
        #endregion

        // ... Dispose ...

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Suspend layout for stable designer rendering
            this.SuspendLayout();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.flowTiles = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDashboardTile = new System.Windows.Forms.Button();
            this.btnRoomsTile = new System.Windows.Forms.Button();
            this.btnStaffTile = new System.Windows.Forms.Button();
            this.btnServicesTile = new System.Windows.Forms.Button();
            this.btnStatsTile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();

            this.panelTop = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();

            // panelMain is now declared above
            this.panelMain = new System.Windows.Forms.Panel();

            // (existing initialization code follows...)
            // panelLeft settings...
            // flowTiles settings...
            // add controls etc.

            // add final ResumeLayout calls
            this.panelTop.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ...
    }
}