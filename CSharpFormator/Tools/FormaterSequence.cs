/**********************************************************************
* 
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace OKZXK.Util.CSharpFormator
{
    public class FormaterSequence
    {
        public List<StringSpawner> stringSpawners = new List<StringSpawner>();
        public string separator = "\n";

        public string SpawnString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in stringSpawners)
            {
                stringBuilder.Append(item.SpawnString());
                stringBuilder.Append(separator);
            }
            stringBuilder.Remove(stringBuilder.Length-separator.Length,separator.Length);
            return stringBuilder.ToString();
        }

        public void Add(IDataFormatable sf, object data)
        {
            stringSpawners.Add(new StringSpawner(sf, data));
        }

        public void Show()
        {
            Console.WriteLine(SpawnString());
        }
    }

    public class StringSpawner
    {
       IDataFormatable stringFormatable;
       object data;

        public StringSpawner(IDataFormatable stringFormatable, object data)
        {
            this.stringFormatable = stringFormatable;
            this.data = data;
        }

        public string SpawnString()
        {
            return stringFormatable.Format(data);
        }
    }
}
