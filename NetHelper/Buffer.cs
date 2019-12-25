/**********************************************************************
* 缓存封装
* 
* 可以解决分包和粘包问题, 暂时不考虑
* 
* 从缓存中根据前4个字节生成的长度来读取信息，
* 读取成功则继续读取
* 读取失败则返回空字符串
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetHelper
{
    class Buffer {
        const int bufferSize = 1024;

        int index=0;//第一个还未存储数据的标志位，以及已经存的字节数量
        byte[] content = new byte[bufferSize];

        public int offset { get => index; set => index = value; }
        public byte[] Content { get => content; set => content = value; }
        public int size { get { return bufferSize - index; } }

        public void AddCount(int count) {
            index += count;
        }
        /// <summary>
        /// 从缓存中根据前4个字节生成的长度来读取信息，
        /// 读取成功则继续读取
        /// 读取失败则返回空字符串
        /// 解决分包和粘包问题
        /// </summary>
        /// <returns></returns>
        public string ReadMessage() {
            string message = "";
            while (true) {
                if (index >= 4) {
                    int count = BitConverter.ToInt32(content, 0);
                    if (index - 4 >= count) {
                        message += Encoding.UTF8.GetString(content, 4, count) + " ";
                        Array.Copy(content, 4 + count, content, 0, index - 4 - count);
                        index -= (4 + count);
                    } else {
                        return message;
                    }
                } else {
                    return message;
                }
            }
        }
    }
}
