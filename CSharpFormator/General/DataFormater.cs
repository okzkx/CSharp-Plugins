using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OKZXK.Util.CSharpFormator
{
    public class DataFormater : IDataFormatable
    {
        public Func<object, string> FormatData;
        public DataFormater(Func<object, string> FormatData = null)
        {
            this.FormatData = FormatData ?? ((data) => data.ToString());
        }

        public string Format(object obj) => FormatData.Invoke(obj);

        public void Show(object obj)
        {
            Console.WriteLine(Format(obj));
        }
    }
}
