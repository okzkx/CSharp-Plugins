using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OKZXK.Util.CSharpFormator
{
    public class PairFormater<TKey, TValue> : IDataFormatable
    {
        public Func<object, string> FormatKey;
        public Func<object, string> FormatValue;
        public PairFormater(IDataFormatable valueFormater = null)
        {
            this.FormatKey = ((data) => data.ToString());
            this.FormatValue = ((data) => data.ToString());
            if (valueFormater != null)
            {
                FormatValue = valueFormater.Format;
            }
        }

        public string Format(object obj)
        {
            if (AsPair(obj, out object key, out object value))
            {
                return $"{FormatKey.Invoke(key)}: {FormatValue.Invoke(value)}";
            }
            return string.Empty;

        }

        public bool AsPair(object obj, out object key, out object value)
        {
            try
            {
                var pair = (KeyValuePair<TKey, TValue>)obj;
                key = pair.Key;
                value = pair.Value;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            key = null;
            value = null;
            return false;
        }

        public void Show(object obj)
        {
            Console.WriteLine(Format(obj));
        }
    }
}
