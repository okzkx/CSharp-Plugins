/**********************************************************************
* 
***********************************************************************/

using OKZKX.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKZKX.MySqlHelper
{
    public partial class SqlManager
    {
        private MySqlModel model;
        public MySqlModel Model => model = model ?? new MySqlModel(Execute, Operate);

        public class MySqlModel
        {
            MySqlExecute Execute;
            MySqlOperate Operate;

            public MySqlModel(MySqlExecute execute, MySqlOperate operate)
            {
                Execute = execute;
                Operate = operate;
            }

            public List<T> Read<T>()
            {
                Type type = typeof(T);
                List<T> table = new List<T>();
                // select * from user
                string sql = $"select * from {type.Name};";

                Operate.EachReader(sql, (reader) =>
                {
                    T t = (T)Activator.CreateInstance(type);
                    RefrectionTool.EachField(t, (fi) =>
                    {
                        fi.SetValue(t, reader[fi.Name]);
                    });
                    table.Add(t);
                });
                return table;
            }

            public void Insert<T>(T t)
            {
                //insert user values('okzkx','123');
                StringBuilder paras = new StringBuilder();
                RefrectionTool.EachField(t, (fi) =>
                {
                    paras.Append($"'{fi.GetValue(t)}',");
                });

                if (paras.Length > 0)
                {
                    paras.Remove(paras.Length - 1, 1);
                }

                string sql = $"insert {t.GetType().Name} values({paras});";
                Execute.NonQuery(sql);
            }
        }
    }
}