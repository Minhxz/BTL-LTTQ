using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DanhMuc.Classes
{
    public class DataProcessor
    {
        // Chuỗi kết nối tới CSDL SQL Server
        private string strConnect = @"Data Source=.;Initial Catalog=QuanLyGara;Integrated Security=True";
        private SqlConnection sqlConnect = null;

        // Phương thức mở kết nối
        private void OpenConnect()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
            {
                sqlConnect.Open();
            }
        }

        // Phương thức đóng kết nối
        private void CloseConnect()
        {
            if (sqlConnect != null && sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

        // Phương thức đọc dữ liệu (SELECT)
        public DataTable ReadData(string sqlSelect)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnect();
                SqlDataAdapter da = new SqlDataAdapter(sqlSelect, sqlConnect);
                da.Fill(dt);
                CloseConnect();
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // Phương thức cập nhật dữ liệu (INSERT, UPDATE, DELETE)
        public void ChangeData(string sql)
        {
            try
            {
                OpenConnect();
                SqlCommand cmd = new SqlCommand(sql, sqlConnect);
                cmd.ExecuteNonQuery();
                CloseConnect();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hỗ trợ thêm phương thức kiểm tra giá trị đơn (ví dụ kiểm tra trùng mã)
        public object GetScalar(string sql)
        {
            object result = null;
            try
            {
                OpenConnect();
                SqlCommand cmd = new SqlCommand(sql, sqlConnect);
                result = cmd.ExecuteScalar();
                CloseConnect();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}
