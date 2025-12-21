using System;
using System.Windows.Forms;

namespace HotelManagementApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load sample rooms once at startup (shared source)
            try
            {
                RoomStore.LoadSampleRooms();
            }
            catch
            {
                // nếu RoomStore không tồn tại hoặc lỗi, bỏ qua để tránh crash
            }

            // Start app - dùng form đăng nhập của bạn (Log_in)
            Application.Run(new Trang_Chu());
        }
    }
}
