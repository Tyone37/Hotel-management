using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    partial class UC_QuanLyPhong
    {
        private IContainer components = null;
        private DataGridView dgvRooms;
        private GroupBox grpEditor;
        private Label lblSoPhong;
        private TextBox txtSoPhong;
        private Label lblLoai;
        private ComboBox cmbLoai;
        private Label lblGia;
        private NumericUpDown numGia;
        private Label lblTrangThai;
        private ComboBox cmbTrangThai;
        private Label lblMoTa;
        private TextBox txtMoTa;
        private PictureBox picRoom;
        private Button btnUpload;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClear;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.dgvRooms = new DataGridView();
            this.grpEditor = new GroupBox();
            this.lblSoPhong = new Label();
            this.txtSoPhong = new TextBox();
            this.lblLoai = new Label();
            this.cmbLoai = new ComboBox();
            this.lblGia = new Label();
            this.numGia = new NumericUpDown();
            this.lblTrangThai = new Label();
            this.cmbTrangThai = new ComboBox();
            this.lblMoTa = new Label();
            this.txtMoTa = new TextBox();
            this.picRoom = new PictureBox();
            this.btnUpload = new Button();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.btnRefresh = new Button();

            ((ISupportInitialize)(this.dgvRooms)).BeginInit();
            ((ISupportInitialize)(this.numGia)).BeginInit();
            this.grpEditor.SuspendLayout();
            ((ISupportInitialize)(this.picRoom)).BeginInit();
            this.SuspendLayout();

            // dgvRooms
            this.dgvRooms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvRooms.Location = new Point(12, 48);
            this.dgvRooms.Size = new Size(620, 480);
            this.dgvRooms.ReadOnly = true;
            this.dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // grpEditor
            this.grpEditor.Text = "Thông tin phòng";
            this.grpEditor.Location = new Point(640, 48);
            this.grpEditor.Size = new Size(420, 340);

            // lblSoPhong / txtSoPhong
            this.lblSoPhong.Text = "Số phòng";
            this.lblSoPhong.Location = new Point(12, 28);
            this.lblSoPhong.AutoSize = true;
            this.txtSoPhong.Location = new Point(120, 24);
            this.txtSoPhong.Width = 200;

            // lblLoai / cmbLoai
            this.lblLoai.Text = "Loại";
            this.lblLoai.Location = new Point(12, 64);
            this.lblLoai.AutoSize = true;
            this.cmbLoai.Location = new Point(120, 60);
            this.cmbLoai.Width = 200;
            this.cmbLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbLoai.Items.AddRange(new object[] { "Đơn", "Đôi", "Suite", "Deluxe" });

            // lblGia / numGia
            this.lblGia.Text = "Giá";
            this.lblGia.Location = new Point(12, 100);
            this.lblGia.AutoSize = true;
            this.numGia.Location = new Point(120, 96);
            this.numGia.Maximum = 100000000;
            this.numGia.DecimalPlaces = 0;
            this.numGia.Width = 120;

            // lblTrangThai / cmbTrangThai
            this.lblTrangThai.Text = "Trạng thái";
            this.lblTrangThai.Location = new Point(12, 136);
            this.lblTrangThai.AutoSize = true;
            this.cmbTrangThai.Location = new Point(120, 132);
            this.cmbTrangThai.Width = 200;
            this.cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrangThai.Items.AddRange(new object[] { "Trống", "Đã đặt", "Đang thuê", "Bảo trì" });

            // lblMoTa / txtMoTa
            this.lblMoTa.Text = "Mô tả";
            this.lblMoTa.Location = new Point(12, 172);
            this.lblMoTa.AutoSize = true;
            this.txtMoTa.Location = new Point(120, 168);
            this.txtMoTa.Size = new Size(280, 60);
            this.txtMoTa.Multiline = true;

            // picRoom / btnUpload
            this.picRoom.Location = new Point(120, 240);
            this.picRoom.Size = new Size(120, 90);
            this.picRoom.BorderStyle = BorderStyle.FixedSingle;
            this.picRoom.SizeMode = PictureBoxSizeMode.Zoom;
            this.btnUpload.Text = "Chọn ảnh";
            this.btnUpload.Location = new Point(250, 280);
            this.btnUpload.Size = new Size(80, 28);

            // add editor controls into group
            this.grpEditor.Controls.Add(this.lblSoPhong);
            this.grpEditor.Controls.Add(this.txtSoPhong);
            this.grpEditor.Controls.Add(this.lblLoai);
            this.grpEditor.Controls.Add(this.cmbLoai);
            this.grpEditor.Controls.Add(this.lblGia);
            this.grpEditor.Controls.Add(this.numGia);
            this.grpEditor.Controls.Add(this.lblTrangThai);
            this.grpEditor.Controls.Add(this.cmbTrangThai);
            this.grpEditor.Controls.Add(this.lblMoTa);
            this.grpEditor.Controls.Add(this.txtMoTa);
            this.grpEditor.Controls.Add(this.picRoom);
            this.grpEditor.Controls.Add(this.btnUpload);

            // action buttons
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Location = new Point(640, 404);
            this.btnAdd.Size = new Size(90, 32);

            this.btnEdit.Text = "Sửa";
            this.btnEdit.Location = new Point(740, 404);
            this.btnEdit.Size = new Size(90, 32);

            this.btnDelete.Text = "Xóa";
            this.btnDelete.Location = new Point(840, 404);
            this.btnDelete.Size = new Size(90, 32);

            this.btnClear.Text = "Làm mới";
            this.btnClear.Location = new Point(940, 404);
            this.btnClear.Size = new Size(90, 32);
            // search / refresh
            this.txtSearch.Location = new Point(12, 12);
            this.txtSearch.Width = 220;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.Location = new Point(240, 8);
            this.btnSearch.Size = new Size(60, 28);
            this.btnRefresh.Text = "Tải lại";
            this.btnRefresh.Location = new Point(308, 8);
            this.btnRefresh.Size = new Size(80, 28);
            // add controls
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRefresh);
            this.Size = new Size(1080, 540);
            ((ISupportInitialize)(this.dgvRooms)).EndInit();
            ((ISupportInitialize)(this.numGia)).EndInit();
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            ((ISupportInitialize)(this.picRoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
            