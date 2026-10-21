using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QLyCuaHangSuaXe.Classes
{
    internal class DataProcessor
    {
        string strConnect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=;Integrated Security=True";
        SqlConnection sqlConnect = null;

        private void KetNoiCSDL()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
                sqlConnect.Open();
        }

        private void DongKetNoiCSDL()
        {
            if (sqlConnect.State != ConnectionState.Closed)
                sqlConnect.Close();
        }

        public DataTable DocBang(string sql)
        {
            DataTable dtBang = new DataTable();
            KetNoiCSDL();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnect);
            sqlDataAdapter.Fill(dtBang);
            DongKetNoiCSDL();
            return dtBang;
        }

        public void CapNhatDuLieu(string sql)
        {
            KetNoiCSDL();
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnect);
            sqlCommand.ExecuteNonQuery();
            DongKetNoiCSDL();
            sqlCommand.Dispose();
        }

        public int CapNhatDuLieu(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(strConnect))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
