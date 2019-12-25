/**********************************************************************
* 服务器程序
* 
* 异步服务端Socket程序，
* 等待客户端连接或消息时不影响主线程运行
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace NetHelper
{
    public class TCPServer
    {
        Socket socket;
        public List<Client> clients = new List<Client>();

        public ServerData ServerData = new ServerData();
        public Action OnBeginAccept;
        public Action OnAsseptClient;
        public Action<string> OnMessageReceive;
        public TCPServer()
        {
            //建立和配置服务端Socket:类型（电脑上的网络接口），指明ipv4，数据流，Tcp
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //为套接字赋值，同时向操作系统申请
            socket.Bind(ServerData.IPEndPoint);
            //设置最长等待队列为无限
            socket.Listen(0);
        }

        public void Start()
        {
            OnBeginAccept?.Invoke();

            //异步线程得到第一个客户端连接,开始对其监听
            socket.BeginAccept(OnAccept, socket);
        }

        public void Broadcast(string msg)
        {
            foreach (var c in clients)
            {
                c.Send(msg);
            }
        }

        /// <summary>
        /// 异步监听到有客户端连接时的处理和再次监听
        /// </summary>
        /// <param name="ar"></param>
        void OnAccept(IAsyncResult ar)
        {
            //一个客户端连接上
            OnAsseptClient?.Invoke();
            Client client = new Client(ar);
            client.OnRecieveMessage = (msg) => OnMessageReceive?.Invoke(msg);
            client.OnClose = () => clients.Remove(client);
            clients.Add(client);

            //此线程结束,再次开启线程等待下一个客户端连接
            socket.BeginAccept(OnAccept, socket);
        }

        public static void Sample()
        {
            TCPServer server = new TCPServer();
            //server.ServerData = new ServerData();
            server.OnBeginAccept = () => Console.WriteLine("开始等待客户端连接...");
            server.OnAsseptClient = () => Console.WriteLine("一个客户端连接上...");
            server.OnMessageReceive = (msg) => Console.WriteLine($"接收到客户端消息:{msg}...");
            server.Start();

            //使主线程不关闭
            Console.WriteLine("输入q退出Server,输入其他进行广播...");

            for (string msg = Console.ReadLine(); msg != "q"; msg = Console.ReadLine())
            {
                server.Broadcast(msg);
            }
        }

        public class Client
        {
            Socket serverSocket;
            Socket clientSocket;
            Message message = new Message();

            public Action<string> OnRecieveMessage;
            public Action OnClose;

            public Client(IAsyncResult ar)
            {
                //获取双方 Socket
                serverSocket = ar.AsyncState as Socket;
                clientSocket = serverSocket.EndAccept(ar);

                CreateReceiveTask();
            }

            public void Send(string msg)
            {
                clientSocket.Send(Message.StrToBytes(msg));
            }

            /// <summary>
            /// 异步监听到有客户端消息时的处理和再次监听
            /// </summary>
            /// <param name="ar"></param>
            void OnReceive(IAsyncResult ar)
            {

                //该次监听所获得的数据量
                int dataCount = 0;

                try
                {
                    dataCount = clientSocket.EndReceive(ar);
                }
                catch (Exception e)
                {
                    //客户端强制关闭连接
                    Close(e.ToString());
                    return;
                }

                //处理客户端正常关闭连接
                if (dataCount == 0)
                {
                    Close($"{clientSocket.ToString()}客户端正常关闭");
                    return;
                }

                //新开线程异步监听 
                CreateReceiveTask();

                //字节消息处理
                OnRecieveMessage?.Invoke(message.ToString(dataCount));
            }

            public void CreateReceiveTask()
            {
                clientSocket.BeginReceive(message.buffer, 0, Message.LENGTH, SocketFlags.None, OnReceive, clientSocket);
            }

            public void Close(string msg = null)
            {
                if (msg != null)
                {
                    Console.WriteLine(msg);
                }
                clientSocket?.Close();
                OnClose?.Invoke();
            }
        }
    }
}
