/**********************************************************************
* 
***********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace OKZKX.ZJson
{
    public partial class ZJson
    {
        public static ZJson Load(string path)
        {
            return Parse(File.ReadAllText(path));
        }
    }
}
