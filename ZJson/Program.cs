using OKZKX.ZJson;
using System;

namespace ProjectTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "{abc:\"12313\"}";
            ZJson json = ZJson.Parse(str);
        }
    }
}
