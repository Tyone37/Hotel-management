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

namespace HotelManagementApp
{
    public partial class capNhap : Form
    {
        public capNhap()
        {
            InitializeComponent();
        }

        private void capNhap_Load(object sender, EventArgs e)
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
    }
}
