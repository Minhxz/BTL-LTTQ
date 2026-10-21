using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLyCuaHangSuaXe.DanhMuc.DichVu.Classes
{
    public class DataProcessor
    {
        private string strConnect = @"Data Source=.;Initial Catalog=AutoFixDb;Integrated Security=True";
        private SqlConnection sqlConnect = null;

        private void OpenConnect()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
            {
                sqlConnect.Open();
            }
        }

        private void CloseConnect()
        {
            if (sqlConnect != null && sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

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
