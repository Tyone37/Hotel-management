using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementApp
{
    public partial class lichsuhoatdong : Form
    {
        string connStr = "Data Source=26.250.133.82,5000;Initial Catalog=QLKS;User ID=admin;Password=12345678";

        public lichsuhoatdong()
        {
            InitializeComponent();
            LoadLichSu();
        }

        void LoadLichSu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT 
            tk.username  AS [Tài khoản],
            ls.ThoiGian   AS [Thời gian],
          
            ls.HanhDong  AS [Hành động],
            ls.NoiDung   AS [Nội dung]
          FROM LichSuHoatDong ls
          INNER JOIN staff tk ON ls.MaNV = tk.ID
          ORDER BY ls.ThoiGian DESC", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
        }



        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadLichSu();
        }
    }
}
