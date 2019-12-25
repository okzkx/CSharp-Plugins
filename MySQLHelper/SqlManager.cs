/**********************************************************************
* 
***********************************************************************/

using MySql.Data.MySqlClient;
using System;

namespace OKZKX.MySqlHelper
{
    public partial class SqlManager
    {
        public MySqlConfig MySqlData = new MySqlConfig();
        public MySqlConnection conn;
        public bool Connect()
        {
            string dataStr =
                        $"database={MySqlData.database};" +
                        $"data source={MySqlData.dataSource};" +
                        $"port={MySqlData.mysqlPort};" +
                        $"user id={MySqlData.mysqlUserId};" +
                        $"password={MySqlData.password};";
            try
            {
                conn = new MySqlConnection(dataStr);
                conn.Open();
            }
            catch (Exception)
            {
                conn = null;
                throw;
            }

            return true;
        }
        public void Close()
        {
            conn?.Close();
            conn = null;
        }
    }
}
