/**********************************************************************
* 
***********************************************************************/

using System;
using System.Collections.Generic;

namespace OKZXK.Util.CSharpFormator
{
    public class DoubleDimensional
    {
        public static int[][] Get()
        {
            int[][] vss = new int[5][];
            for (int i = 0; i < 5; i++)
            {
                vss[i] = new int[5];
                for (int j = 0; j < 5; j++)
                {
                    vss[i][j] = i * j;
                }
            }
            return vss;
        }

      
    }
}
