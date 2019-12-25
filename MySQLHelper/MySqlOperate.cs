/**********************************************************************
* 
***********************************************************************/

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace OKZKX.MySqlHelper
{
    public partial class SqlManager
    {
        private MySqlOperate operate;
        public MySqlOperate Operate => operate = operate ?? new MySqlOperate(Execute);

        public class MySqlOperate
        {
            MySqlExecute Execute;

            public MySqlOperate(MySqlExecute execute)
            {
                Execute = execute;
            }

            public void EachReader(string sql, Action<MySqlDataReader> action, List<MySqlParameter> paras = null)
            {
                using (var reader = Execute.Reader(sql))
                    while (reader.Read())
                        action(reader);
            }

            public List<object[]> ReadTable(string sql)
            {
                List<object[]> table = new List<object[]>();
                EachReader(sql, (reader) =>
                {
                    object[] cols = new object[reader.FieldCount];
                    for (int i = 0; i < cols.Length; i++) cols[i] = reader[i];
                    table.Add(cols);
                });
                return table;
            }
        }
    }
}