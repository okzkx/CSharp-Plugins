/**********************************************************************
* 
***********************************************************************/

using MySql.Data.MySqlClient;
using OKZKX.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKZKX.MySqlHelper
{
    public partial class SqlManager
    {
        private MySqlExecute execute;
        public MySqlExecute Execute => execute = execute ?? new MySqlExecute(conn);

        public class MySqlExecute
        {
            MySqlConnection conn;
            public MySqlExecute(MySqlConnection conn)
            {
                this.conn = conn;
            }
            public MySqlCommand CraeteCmd(string sql, List<MySqlParameter> paras)
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (paras != null)
                    cmd.Parameters.AddRange(paras.ToArray());
                return cmd;
            }
            public MySqlDataReader Reader(string sql, List<MySqlParameter> paras = null)
            {
                return CraeteCmd(sql, paras).ExecuteReader();
            }
            public object Scalar(string sql, List<MySqlParameter> paras = null)
            {
                return CraeteCmd(sql, paras).ExecuteScalar();
            }
            public int NonQuery(string sql, List<MySqlParameter> paras = null)
            {
                return CraeteCmd(sql, paras).ExecuteNonQuery();
            }
        }
    }
}