using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementApp.quanly
{
    partial class UC_QuanLyDichVu
    {
        private IContainer components = null;
        private DataGridView dgvServices;
        private GroupBox grpEditor;
        private TextBox txtServiceName;
        private NumericUpDown numServicePrice;
        private ComboBox cmbServiceStatus;
        private Button btnServiceAdd;
        private Button btnServiceEdit;
        private Button btnServiceDelete;
        private Button btnServiceRefresh;
        private TextBox txtServiceSearch;
        private Button btnServiceSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.grpEditor = new System.Windows.Forms.GroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numServicePrice = new System.Windows.Forms.NumericUpDown();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbServiceStatus = new System.Windows.Forms.ComboBox();
            this.btnServiceAdd = new System.Windows.Forms.Button();
            this.btnServiceEdit = new System.Windows.Forms.Button();
            this.btnServiceDelete = new System.Windows.Forms.Button();
            this.btnServiceRefresh = new System.Windows.Forms.Button();
            this.txtServiceSearch = new System.Windows.Forms.TextBox();
            this.btnServiceSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.grpEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numServicePrice)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvServices
            // 
            this.dgvServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServices.ColumnHeadersHeight = 29;
            this.dgvServices.Location = new System.Drawing.Point(16, 56);
            this.dgvServices.Name = "dgvServices";
            this.dgvServices.ReadOnly = true;
            this.dgvServices.RowHeadersWidth = 51;
            this.dgvServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServices.Size = new System.Drawing.Size(1439, 583);
            this.dgvServices.TabIndex = 0;
            this.dgvServices.SelectionChanged += new System.EventHandler(this.dgvServices_SelectionChanged);
            // 
            // grpEditor
            // 
            this.grpEditor.Controls.Add(this.lblName);
            this.grpEditor.Controls.Add(this.txtServiceName);
            this.grpEditor.Controls.Add(this.lblPrice);
            this.grpEditor.Controls.Add(this.numServicePrice);
            this.grpEditor.Controls.Add(this.lblStatus);
            this.grpEditor.Controls.Add(this.cmbServiceStatus);
            this.grpEditor.Location = new System.Drawing.Point(728, 56);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(340, 260);
            this.grpEditor.TabIndex = 1;
            this.grpEditor.TabStop = false;
            this.grpEditor.Text = "Thông tin dịch vụ";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(100, 23);
            this.lblName.TabIndex = 0;
            // 
            // txtServiceName
            // 
            this.txtServiceName.Location = new System.Drawing.Point(110, 24);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(200, 22);
            this.txtServiceName.TabIndex = 1;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(0, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(100, 23);
            this.lblPrice.TabIndex = 2;
            // 
            // numServicePrice
            // 
            this.numServicePrice.Location = new System.Drawing.Point(110, 60);
            this.numServicePrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numServicePrice.Name = "numServicePrice";
            this.numServicePrice.Size = new System.Drawing.Size(120, 22);
            this.numServicePrice.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 23);
            this.lblStatus.TabIndex = 4;
            // 
            // cmbServiceStatus
            // 
            this.cmbServiceStatus.Items.AddRange(new object[] {
            "Kích hoạt",
            "Vô hiệu"});
            this.cmbServiceStatus.Location = new System.Drawing.Point(110, 96);
            this.cmbServiceStatus.Name = "cmbServiceStatus";
            this.cmbServiceStatus.Size = new System.Drawing.Size(140, 24);
            this.cmbServiceStatus.TabIndex = 5;
            // 
            // btnServiceAdd
            // 
            this.btnServiceAdd.Location = new System.Drawing.Point(728, 330);
            this.btnServiceAdd.Name = "btnServiceAdd";
            this.btnServiceAdd.Size = new System.Drawing.Size(100, 34);
            this.btnServiceAdd.TabIndex = 2;
            this.btnServiceAdd.Text = "Thêm";
            this.btnServiceAdd.Click += new System.EventHandler(this.btnServiceAdd_Click);
            // 
            // btnServiceEdit
            // 
            this.btnServiceEdit.Location = new System.Drawing.Point(836, 330);
            this.btnServiceEdit.Name = "btnServiceEdit";
            this.btnServiceEdit.Size = new System.Drawing.Size(100, 34);
            this.btnServiceEdit.TabIndex = 3;
            this.btnServiceEdit.Text = "Sửa";
            this.btnServiceEdit.Click += new System.EventHandler(this.btnServiceEdit_Click);
            // 
            // btnServiceDelete
            // 
            this.btnServiceDelete.Location = new System.Drawing.Point(944, 330);
            this.btnServiceDelete.Name = "btnServiceDelete";
            this.btnServiceDelete.Size = new System.Drawing.Size(100, 34);
            this.btnServiceDelete.TabIndex = 4;
            this.btnServiceDelete.Text = "Xóa";
            this.btnServiceDelete.Click += new System.EventHandler(this.btnServiceDelete_Click);
            // 
            // btnServiceRefresh
            // 
            this.btnServiceRefresh.Location = new System.Drawing.Point(1052, 330);
            this.btnServiceRefresh.Name = "btnServiceRefresh";
            this.btnServiceRefresh.Size = new System.Drawing.Size(80, 34);
            this.btnServiceRefresh.TabIndex = 5;
            this.btnServiceRefresh.Text = "Tải lại";
            this.btnServiceRefresh.Click += new System.EventHandler(this.btnServiceRefresh_Click);
            // 
            // txtServiceSearch
            // 
            this.txtServiceSearch.Location = new System.Drawing.Point(16, 16);
            this.txtServiceSearch.Name = "txtServiceSearch";
            this.txtServiceSearch.Size = new System.Drawing.Size(220, 22);
            this.txtServiceSearch.TabIndex = 6;
            // 
            // btnServiceSearch
            // 
            this.btnServiceSearch.Location = new System.Drawing.Point(244, 12);
            this.btnServiceSearch.Name = "btnServiceSearch";
            this.btnServiceSearch.Size = new System.Drawing.Size(60, 28);
            this.btnServiceSearch.TabIndex = 7;
            this.btnServiceSearch.Text = "Tìm";
            this.btnServiceSearch.Click += new System.EventHandler(this.btnServiceSearch_Click);
            // 
            // UC_QuanLyDichVu
            // 
            this.Controls.Add(this.dgvServices);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.btnServiceAdd);
            this.Controls.Add(this.btnServiceEdit);
            this.Controls.Add(this.btnServiceDelete);
            this.Controls.Add(this.btnServiceRefresh);
            this.Controls.Add(this.txtServiceSearch);
            this.Controls.Add(this.btnServiceSearch);
            this.Name = "UC_QuanLyDichVu";
            this.Size = new System.Drawing.Size(1839, 663);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numServicePrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lblName;
        private Label lblPrice;
        private Label lblStatus;
    }
}