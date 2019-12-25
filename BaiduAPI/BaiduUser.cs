namespace BaiduAPI
{
    public class BaiduUser
    {
        #region AI

        const string API_KEY = "tyVwblLfiaSy25XHngAc2nxQ";
        const string SECRET_KEY = "1O8tLb2QDFgEl9I3KIuECxoeiYsR5Z9S";

        #endregion

        #region Translate

        const string appId = "20191119000358381";
        const string secretKey = "JJu2RV5SCosUx3CJW_nB";

        #endregion


        private static BaiduUser instance;
        public static BaiduUser Instance => instance = instance ?? new BaiduUser();

        BaiduAIClient BaiduAIClient;
        BaiduTranslateClient BaiduTranslateClient;

        public BaiduUser()
        {
            BaiduAIClient = new BaiduAIClient(API_KEY, SECRET_KEY);
            BaiduTranslateClient = new BaiduTranslateClient(appId, secretKey);
        }

        public string OCRImage(byte[] image) => BaiduAIClient.GeneralBasic(image);

        public string Translate(string strToTrans, string from = "en", string to = "zh")
        {
            if (strToTrans == null) return null;
            return BaiduTranslateClient.Translate(strToTrans, from, to);
        }
    }
}
