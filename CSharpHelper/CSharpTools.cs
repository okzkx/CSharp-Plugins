/**********************************************************************
* 我的 CSharp 工具类
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace OKZKX.Util
{
    public class CSharpTools
    {
        public const string DesktopUrl = @"C:\Users\Administrator\Desktop\";
        public string FullTimeNow => DateTime.Now.ToString("yyyymmddhhmmss");
            //bitmap.Save(url);

        public static string ToString<T>(T t)
        {
            if (t == null) return "NULL";

            StringBuilder stringBuilder = new StringBuilder();

            if (t is Array array)
            {
                var enrt = array.GetEnumerator();
                while (enrt.MoveNext())
                {
                    stringBuilder.Append(ToString(enrt.Current));
                    stringBuilder.Append(enrt.Current is Array ? "\n" : ",");
                }
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
            }
            else
            {
                stringBuilder.Append(t.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}
