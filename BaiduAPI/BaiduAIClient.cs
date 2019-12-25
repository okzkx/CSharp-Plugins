/**********************************************************************
* 
***********************************************************************/


using Baidu.Aip.Ocr;
using Newtonsoft.Json.Linq;
using System.Text;

namespace BaiduAPI
{
    public class BaiduAIClient
    {

        Ocr client;

        public BaiduAIClient(string API_KEY, string SECRET_KEY)
        {
            client = new Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间
        }

        public string GeneralBasic(byte[] imageBytes)
        {
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            JObject result = client.GeneralBasic(imageBytes);

            StringBuilder stringBuilder = new StringBuilder();
            if (result != null)
            {
                JArray jArray = result["words_result"] as JArray;
                foreach (var item in jArray)
                {
                    stringBuilder.Append(item["words"].ToString());
                    stringBuilder.Append("\n");
                }
            }
            return stringBuilder.ToString();
        }
    }
}
