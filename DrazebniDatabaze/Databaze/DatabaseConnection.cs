using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Drazebni_databaze
{
    public class DatabaseConnection
    {
        private static SqlConnection conn = null;
        private DatabaseConnection()
        {

        }
		public static SqlConnection GetInstance()
		{
			if (conn == null)
			{
				SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
				consStringBuilder.UserID = ReadSetting("Name");
				consStringBuilder.Password = ReadSetting("Password");
				consStringBuilder.InitialCatalog = ReadSetting("Database");
				consStringBuilder.DataSource = ReadSetting("DataSource");
				consStringBuilder.ConnectTimeout = 30;
				conn = new SqlConnection(consStringBuilder.ConnectionString);
				conn.Open();
			}
			return conn;
		}
		public static void CloseConnection()
		{
			if (conn != null)
			{
				conn.Close();
				conn.Dispose();
			}
		}
		private static string ReadSetting(string key)
		{
			var appSettings = ConfigurationManager.AppSettings;
			string result = appSettings[key] ?? "Not Found";
			return result;
		}

	}
}
