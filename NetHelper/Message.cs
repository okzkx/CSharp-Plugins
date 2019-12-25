
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetHelper
{
    public class Message
    {
        public const int LENGTH = 1024;
        public byte[] buffer = new byte[LENGTH];

        internal string ToString(int count)
        {
            return Encoding.UTF8.GetString(buffer, 0, count);
        }

        internal static byte[] StrToBytes(string msg)
        {
            return Encoding.UTF8.GetBytes(msg);
        }

        ///// <summary>
        ///// 包装前4个字节为二进制数组长度
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static byte[] GetBytes(string data)
        //{
        //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //    byte[] lengthBytes = BitConverter.GetBytes(dataBytes.Length);
        //    return lengthBytes.Concat(dataBytes).ToArray();
        //}
    }
}
