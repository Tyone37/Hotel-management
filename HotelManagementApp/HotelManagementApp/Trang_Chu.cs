using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementApp
{
    public partial class Trang_Chu : Form
    {
        public Trang_Chu()
        {
            InitializeComponent();
            this.Shown += Trang_Chu_Shown;
        }
        private void Trang_Chu_Load(object sender, EventArgs e)
        {
            SetPlaceholder();
            this.ActiveControl = null;
            txtSearch.SelectionStart = 0;
            txtSearch.SelectionLength = 0;
        }
        private void Trang_Chu_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null; 
        }
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.ForeColor == Color.Gray)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                SetPlaceholder();
            }
        }
        private void SetPlaceholder()
        {
            txtSearch.Text = "Tìm kiếm phòng...";
            txtSearch.ForeColor = Color.Gray;
        }

        private void linkLabel_Reload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReloadForm();
        }

        private void ReloadForm()
        {
            this.Controls.Clear();
            InitializeComponent();

            // Gọi lại load nếu cần
            Trang_Chu_Load(null, null);
        }

        private void link_DangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Log_in login = new Log_in();
            login.Show();
            this.Hide();
        }

        private void link_DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Sign_in signin = new Sign_in();
            signin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu ô tìm kiếm rỗng hoặc đang ở trạng thái placeholder
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || txtSearch.ForeColor == Color.Gray)
            {
                MessageBox.Show("Vui lòng nhập nội dung cần tìm kiếm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông báo yêu cầu đăng nhập
            DialogResult result = MessageBox.Show(
                "Bạn cần đăng nhập để sử dụng chức năng tìm kiếm.\nBạn có muốn chuyển đến trang đăng nhập không?",
                "Yêu cầu đăng nhập",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Chuyển đến Form đăng nhập
                Log_in loginForm = new Log_in();
                loginForm.Show();
                this.Hide();
            }
            else
            {
                // Chọn No → không chuyển trang
                MessageBox.Show("Bạn đã chọn hủy. Vui lòng đăng nhập nếu muốn tiếp tục tìm kiếm.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
