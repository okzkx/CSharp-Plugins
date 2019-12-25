/**********************************************************************
* 服务器程序
* 
* 异步服务端Socket程序，
* 等待客户端连接或消息时不影响主线程运行
***********************************************************************/

using System;
using System.Net.Sockets;
using System.Text;

namespace NetHelper
{
    public class TCPClient
    {
        public ServerData ServerData = new ServerData();
        //实例化客户端Socket
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Message message = new Message();

        public Action OnConnect;
        public Action<string> OnReceiveMessage;

        public void Connect()
        {
            //同步连接服务端
            socket.Connect(ServerData.IPEndPoint);
            OnConnect?.Invoke();

            //开启子线程等待消息接收
            CreateWaitingReceiveTask();
        }

        public void Send(string msg)
        {
            socket.Send(Encoding.UTF8.GetBytes(msg));
        }

        public void Close()
        {
            socket.Close();
        }

        private void OnReceive(IAsyncResult asyncResult)
        {
            //此线程即将结束,再开一个线程等待远端消息
            CreateWaitingReceiveTask();
            Socket serverSocket = asyncResult.AsyncState as Socket;
            int dataCount = serverSocket.EndReceive(asyncResult);
            OnReceiveMessage?.Invoke(message.ToString(dataCount));
        }

        private void CreateWaitingReceiveTask()
        {
            socket.BeginReceive(message.buffer, 0, Message.LENGTH, SocketFlags.None, OnReceive, socket);
        }

        public static void Sample()
        {
            TCPClient client = new TCPClient();
            client.OnConnect += () => Console.WriteLine("成功连接上服务端...");
            client.OnReceiveMessage += (msg) => Console.WriteLine($"接收到服务端消息: {msg}...");
            client.Connect();

            //连接上后的操作测试
            while (true)
            {
                string str_send = Console.ReadLine();
                if (str_send != "q")
                {
                    //消息发送测试
                    client.Send(str_send);
                }
                else
                {
                    client.Close();
                    return;
                }
            }
        }
    }
}
