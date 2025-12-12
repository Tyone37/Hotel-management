using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace HotelManagementApp
{
    internal class KetNoi
    {
        private string strKetNoi = "Data Source=26.250.133.82,5000;Initial Catalog = QLKS; Persist Security Info=True;User ID = admin; Password=***********;Encrypt=True";

        public SqlConnection LayKetNoi()
        {
            return new SqlConnection(strKetNoi);
        }

        // Hàm lấy dữ liệu đổ vào bảng (SELECT)
        public DataTable LayDuLieu(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = LayKetNoi())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // Hàm thực hiện lệnh Thêm/Sửa/Xóa (INSERT, UPDATE, DELETE)
        public bool ThucThi(string sql)
        {
            try
            {
                using (SqlConnection conn = LayKetNoi())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery(); // Thực thi lệnh
                    return true; // Thành công
                }
            }
            catch (Exception)
            {
                return false; // Thất bại
            }
        }
    }
}
