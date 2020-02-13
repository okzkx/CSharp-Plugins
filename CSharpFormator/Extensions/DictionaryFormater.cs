using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OKZXK.Util.CSharpFormator
{
    public class DictionaryFormater<TKey, TValue> : IDataFormatable
    {
        public EnumerableFormater enumerableFormater = new EnumerableFormater();

        public string separator { set { enumerableFormater.separator = value; } }
        public string end { set { enumerableFormater.end = value; } }

        public DictionaryFormater(PairFormater<TKey, TValue> pairFormater)
        {
            enumerableFormater = new EnumerableFormater(pairFormater);
            separator = "\n";
        }

        public string Format(object obj) => enumerableFormater.Format(obj);

        public void Show(IEnumerable enumerable) => Console.WriteLine(Format(enumerable));
    }
}
