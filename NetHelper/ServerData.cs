/**********************************************************************
* 服务器端信息配置
* 
* 1.地址和接口信息
* 2.数据库信息
***********************************************************************/
using System.Net;

namespace NetHelper
{
    public class ServerData
    {

        #region TCP服务器配置

        ////ip 地址通过 ipconfig 查询,可以写局域网或者因特网地址
        public string ipStr = "127.0.0.1";
        ////开放的接口自己决定
        public int tcpPort = 88;

        IPEndPoint iPEndPoint;
        public IPEndPoint IPEndPoint {
            get {
                if (iPEndPoint == null)
                {
                    //IP 地址封装
                    IPAddress iPAddress = IPAddress.Parse(ipStr);
                    //二元组 IP:端口号 
                    iPEndPoint = new IPEndPoint(iPAddress, (int)tcpPort);
                }
                return iPEndPoint;
            }
        }

        #endregion
    }
}
