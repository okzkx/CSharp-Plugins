/**********************************************************************
* 
***********************************************************************/

using System;
using System.IO;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using Newtonsoft.Json.Linq;

namespace BaiduAPI
{
    internal class BaiduTranslateClient
    {
        // 改成您的APP ID
        string appId;
        // 改成您的密钥
        string secretKey;

        public BaiduTranslateClient(string appId, string secretKey)
        {
            this.appId = appId;
            this.secretKey = secretKey;
        }

        public string Translate(string strToTrans, string from, string to)
        {
            Random rd = new Random();
            string salt = rd.Next(100000).ToString();
            string sign = EncryptString(appId + strToTrans + salt + secretKey);
            string url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
            url += "q=" + HttpUtility.UrlEncode(strToTrans);
            url += "&from=" + from;
            url += "&to=" + to;
            url += "&appid=" + appId;
            url += "&salt=" + salt;
            url += "&sign=" + sign;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = 6000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            JObject jObject = JObject.Parse(retString);
            JArray jArray = jObject["trans_result"] as JArray;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in jArray)
            {
                stringBuilder.Append(item["dst"].ToString());
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        // 计算MD5值
        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
    }
}