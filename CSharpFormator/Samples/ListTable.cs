/**********************************************************************
* 
***********************************************************************/

using System;
using System.Collections.Generic;

namespace OKZXK.Util.CSharpFormator
{
    public class ListTable
    {
        static void Main(string[] args)
        {
            var vss = DoubleDimensional.Get();

            FormaterSequence formaterSequence = new FormaterSequence();
            DataFormater dataFm = null;
            EnumerableFormater colFm = null;
            EnumerableFormater rowFm = null;

            // List Table
            dataFm = new DataFormater((data) => string.Format("{0,5}", data));
            colFm = new EnumerableFormater((i, data) => dataFm.Format(i + 1));
            formaterSequence.Add(colFm, vss[0]);

            colFm = new EnumerableFormater(dataFm);
            rowFm = new EnumerableFormater(colFm);
            rowFm.separator = "\n";

            formaterSequence.Add(rowFm, vss);
            formaterSequence.Show();

            //Dictionary Table
            Dictionary<string, int[]> dict = new Dictionary<string, int[]>();
            for (int i = 0; i < vss.Length; i++)
            {
                dict.Add(i.ToString(), vss[i]);
            }
            PairFormater<string, int[]> pairFormater = new PairFormater<string, int[]>(colFm);
            DictionaryFormater<string, int[]> dictionaryFormater = new DictionaryFormater<string, int[]>(pairFormater);
            dictionaryFormater.Show(dict);
        }
    }
}
