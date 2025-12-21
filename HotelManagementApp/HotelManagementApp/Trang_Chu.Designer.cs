using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementApp
{
    partial class Trang_Chu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trang_Chu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel_Reload = new System.Windows.Forms.LinkLabel();
            this.link_DangKy = new System.Windows.Forms.LinkLabel();
            this.link_DangNhap = new System.Windows.Forms.LinkLabel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.linkLabel_Reload);
            this.panel1.Controls.Add(this.link_DangKy);
            this.panel1.Controls.Add(this.link_DangNhap);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1170, 72);
            this.panel1.TabIndex = 0;
            // 
            // linkLabel_Reload
            // 
            this.linkLabel_Reload.AutoSize = true;
            this.linkLabel_Reload.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.linkLabel_Reload.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel_Reload.LinkColor = System.Drawing.Color.Black;
            this.linkLabel_Reload.Location = new System.Drawing.Point(46, 22);
            this.linkLabel_Reload.Name = "linkLabel_Reload";
            this.linkLabel_Reload.Size = new System.Drawing.Size(154, 34);
            this.linkLabel_Reload.TabIndex = 1;
            this.linkLabel_Reload.TabStop = true;
            this.linkLabel_Reload.Text = "Hotel Lý Anh";
            this.linkLabel_Reload.UseCompatibleTextRendering = true;
            this.linkLabel_Reload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Reload_LinkClicked);
            // 
            // link_DangKy
            // 
            this.link_DangKy.AutoSize = true;
            this.link_DangKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_DangKy.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.link_DangKy.LinkColor = System.Drawing.Color.Black;
            this.link_DangKy.Location = new System.Drawing.Point(1050, 22);
            this.link_DangKy.Name = "link_DangKy";
            this.link_DangKy.Size = new System.Drawing.Size(98, 25);
            this.link_DangKy.TabIndex = 2;
            this.link_DangKy.TabStop = true;
            this.link_DangKy.Text = "Đăng ký";
            this.link_DangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_DangKy_LinkClicked);
            // 
            // link_DangNhap
            // 
            this.link_DangNhap.AutoSize = true;
            this.link_DangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_DangNhap.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.link_DangNhap.LinkColor = System.Drawing.Color.Black;
            this.link_DangNhap.Location = new System.Drawing.Point(918, 22);
            this.link_DangNhap.Name = "link_DangNhap";
            this.link_DangNhap.Size = new System.Drawing.Size(126, 25);
            this.link_DangNhap.TabIndex = 1;
            this.link_DangNhap.TabStop = true;
            this.link_DangNhap.Text = "Đăng nhập";
            this.link_DangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_DangNhap_LinkClicked);
            // 
            // txtSearch
            // 
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(441, 28);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(307, 20);
            this.txtSearch.TabIndex = 99;
            this.txtSearch.Text = "Tìm kiếm phòng...";
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(868, 92);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(305, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(686, 51);
            this.label2.TabIndex = 3;
            this.label2.Text = "Khách sạn Lý Anh – Nơi Nghỉ Dưỡng Đẳng Cấp 5 Sao ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(293, 534);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(680, 51);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dịch vụ && Tiện ích nổi bật";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(152, 611);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 47);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ẩm thực đẳng cấp";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(134, 658);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(239, 92);
            this.label5.TabIndex = 8;
            this.label5.Text = "- Hải sản Địa Trung Hải tinh tế\r\n- Ẩm thực Châu Á đương đại\r\n- Lounge cocktail tầ" +
    "ng thượng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(493, 658);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(239, 92);
            this.label6.TabIndex = 10;
            this.label6.Text = "- Spa.\r\n- Hồ bơi vô cực\r\n- Phòng gym";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(492, 611);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(234, 47);
            this.label7.TabIndex = 9;
            this.label7.Text = "Chăm sóc && thư giãn";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(859, 658);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(239, 92);
            this.label8.TabIndex = 12;
            this.label8.Text = "- Dịch vụ quản gia\r\n- Đưa đón sân bay\r\n- Hỗ trợ 24/7";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(858, 611);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(203, 47);
            this.label9.TabIndex = 11;
            this.label9.Text = "Dịch vụ cao cấp";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(293, 782);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(680, 51);
            this.label10.TabIndex = 13;
            this.label10.Text = "Các giải thưởng và công nhận";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 934);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(267, 92);
            this.label11.TabIndex = 14;
            this.label11.Text = "Được vinh danh là Khách sạn Sang trọng Tốt nhất Châu Á bởi tạp chí Global Travel." +
    "";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(56, 859);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(192, 75);
            this.label12.TabIndex = 15;
            this.label12.Text = "Traveler\'s Choice Award (2023)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(314, 859);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(257, 75);
            this.label13.TabIndex = 17;
            this.label13.Text = "Green Key Certification (2022-2024)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(304, 934);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(267, 92);
            this.label14.TabIndex = 16;
            this.label14.Text = "Chứng nhận Khách sạn Xanh, công nhận cam kết của chúng tôi đối với sự phát triển " +
    "bền vững và bảo vệ môi trường.";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(591, 859);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(218, 75);
            this.label15.TabIndex = 19;
            this.label15.Text = "World Luxury Hotel Awards (2024)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(577, 934);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(260, 68);
            this.label16.TabIndex = 18;
            this.label16.Text = "Giải thưởng cho Hồ bơi Vô cực Đẹp nhất";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(906, 849);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(192, 75);
            this.label17.TabIndex = 21;
            this.label17.Text = "TripAdvisor Excellence Badge ";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(869, 934);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(267, 92);
            this.label18.TabIndex = 20;
            this.label18.Text = "Nhận điểm đánh giá xuất sắc dựa trên phản hồi của khách hàng toàn cầu.";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Location = new System.Drawing.Point(2, 1054);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1170, 185);
            this.panel2.TabIndex = 100;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(762, 97);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(357, 57);
            this.label22.TabIndex = 3;
            this.label22.Text = "Mạng xã hội: \r\nFacebook: https://www.facebook.com/khachsanlyanh\r\nWebsite: https:/" +
    "/www.khachsanlyanh.com";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(374, 88);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(207, 57);
            this.label21.TabIndex = 2;
            this.label21.Text = "Email: \r\ncontact@khachsanlyanh.com\r\nbooking@khachsanlyanh.com";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 88);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 57);
            this.label20.TabIndex = 1;
            this.label20.Text = "Hotline(24/7): \r\n(+84) 1900 8999\r\n(+84) 97 123 4567";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(3, 12);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(596, 57);
            this.label19.TabIndex = 0;
            this.label19.Text = "Địa chỉ: \r\nKhách sạn Lý Anh\r\nSố 128 Đường Trần Phú, Phường Lộc Thọ, Thành phố Nha" +
    " Trang, Khánh Hòa, Việt Nam";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::HotelManagementApp.Properties.Resources.room_service_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.pictureBox4.Location = new System.Drawing.Point(822, 620);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(39, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 24;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::HotelManagementApp.Properties.Resources.spa_24dp_000000_FILL0_wght400_GRAD0_opsz24;
            this.pictureBox3.Location = new System.Drawing.Point(447, 620);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(39, 35);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HotelManagementApp.Properties.Resources.local_dining_24dp_1F1F1F_FILL0_wght400_GRAD0_opsz24;
            this.pictureBox2.Location = new System.Drawing.Point(107, 620);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HotelManagementApp.Properties.Resources.be_boi_vo_cuc_31;
            this.pictureBox1.Location = new System.Drawing.Point(-2, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1172, 229);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(754, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 19);
            this.button1.TabIndex = 100;
            this.button1.Text = "Tìm kiếm";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Trang_Chu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(240)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(1173, 748);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Trang_Chu";
            this.Text = "Trang_Chu";
            this.Load += new System.EventHandler(this.Trang_Chu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearch;
        private LinkLabel link_DangKy;
        private LinkLabel link_DangNhap;
        private LinkLabel linkLabel_Reload;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Panel panel2;
        private Label label19;
        private Label label22;
        private Label label21;
        private Label label20;
        private System.Windows.Forms.Button button1;
    }
}