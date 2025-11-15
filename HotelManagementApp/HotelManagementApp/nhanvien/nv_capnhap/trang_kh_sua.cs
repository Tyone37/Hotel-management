using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementApp.nv_capnhap
{
    public partial class trang_kh_sua : Form
    {
        public trang_kh_sua()
        {
            InitializeComponent();
        }

        private void trang_kh_sua_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session.DisplayName))
            {
                label2.Text = Session.DisplayName;
            }
            else
            {
                label2.Text = "Tên người dùng";
            }
            ImageHelper.SetAvatarToPictureBox(pictureBox2);
        }

        public static class ImageHelper
        {
            public static void SetAvatarToPictureBox(PictureBox pbx)
            {
                if (Session.AvatarData != null)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(Session.AvatarData))
                        {
                            pbx.Image = Image.FromStream(ms);
                            pbx.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                    catch (Exception)
                    {
                        pbx.Image = null;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Log_in loginForm = new Log_in();
            loginForm.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Staff staffForm = new Staff();
            staffForm.Show();
            this.Close();
        }
    }
}
