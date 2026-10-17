using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace btl.Classes
{
	public class DatabaseProcess
	{
		string strConnect = "Data Source=localhost; Database=QuanLyNhapKho; User ID=sa; Password=1234$; Integrated Security=false;";
		SqlConnection sqlConnect = null;
		void OpenConnection()
		{
			sqlConnect = new SqlConnection(strConnect);
			if(sqlConnect.State != ConnectionState.Open)
			{
				sqlConnect.Open();
			}
		}
		void CloseConnection()
		{
			if(sqlConnect.State != ConnectionState.Closed)
			{
				sqlConnect.Close();
				sqlConnect.Dispose();
			}
		}
		public DataTable ReadData(string sql)
		{
			OpenConnection();
			DataTable data = new DataTable();
			SqlDataAdapter sqlData = new SqlDataAdapter(sql, sqlConnect);
			sqlData.Fill(data);
			CloseConnection();
			return data;
		}
		public void ChangeData(string sql)
		{
			OpenConnection();
			SqlCommand sqlComma = new SqlCommand();
			sqlComma.Connection = sqlConnect;
			sqlComma.CommandText = sql;
			sqlComma.ExecuteNonQuery();
			CloseConnection();
		}
	}
}
