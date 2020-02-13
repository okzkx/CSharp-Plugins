using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OKZXK.Util.CSharpFormator
{
    public class EnumerableFormater : IDataFormatable
    {
        public Func<int, object, string> FormatData;


        public string separator = "";
        public string end = "";

        #region Constructor

        public EnumerableFormater(IDataFormatable fmt)
        {
            FormatData = (i, obj) => fmt.Format(obj);
        }

        public EnumerableFormater(Func<int, object, string> FormatData = null)
        {
            this.FormatData = FormatData ?? ((id, data) => data.ToString());
        }

        #endregion

        #region Formater

        public string Format(IEnumerable enumerable)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerator enumerator = enumerable.GetEnumerator();
            int index = 0;
            while (enumerator.MoveNext())
            {
                sb.Append(FormatData(index++, enumerator.Current));
                sb.Append(separator);
            }
            sb.Remove(sb.Length-separator.Length,separator.Length);
            sb.Append(end);
            return sb.ToString();
        }

        public string Format(object obj)
        {
            IEnumerable enumerable = obj as IEnumerable;
            if (obj == null)
            {
                Console.WriteLine($"尝试将{obj} 转为 IEnumerable 出错");
                return obj.ToString();
            }
            return Format(enumerable);
        }

        #endregion

        public void Show(IEnumerable enumerable)
        {
            Console.WriteLine(Format(enumerable));
        }
    }
}
