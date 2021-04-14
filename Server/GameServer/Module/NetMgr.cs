using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telepathy;

namespace GameServer.Module
{
    class NetMgr
    {
        public const int MaxMessageSize = 16 * 1024;
        static long messagesReceived = 0;
        static long dataReceived = 0;
        Server server;
        public NetMgr()
        {
            server = new Server(MaxMessageSize);
            server.OnConnected = OnConnected;
            server.OnData = OnData;
            server.OnDisconnected = OnDisconnected;          
        }
        public void StartServer() 
        {
            server.Start(1337);
            int serverFrequency = 60;
            Log.Info("started server");
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                // tick and process as many as we can. will auto reply.
                // (100k limit to avoid deadlocks)
                server.Tick(100000);
                // sleep
                Thread.Sleep(1000 / serverFrequency);
                // report every 10 seconds
                if (stopwatch.ElapsedMilliseconds > 1000 * 2)
                {
                    Log.Info(string.Format("Thread[" + Thread.CurrentThread.ManagedThreadId + "]: Server in={0} ({1} KB/s)  out={0} ({1} KB/s) ReceiveQueue={2}", messagesReceived, (dataReceived * 1000 / (stopwatch.ElapsedMilliseconds * 1024)), server.ReceivePipeTotalCount.ToString()));
                    stopwatch.Stop();
                    stopwatch = Stopwatch.StartNew();
                    messagesReceived = 0;
                    dataReceived = 0;
                }
            }
        }


        public void Send(int connectionId,byte[] package)
        {
            server.Send(connectionId,new ArraySegment<byte>(package));
        }

        public void OnConnected(int connectionId)
        {
            Console.WriteLine(connectionId + " Connected");
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnData(int connectionId, ArraySegment<byte> buff)
        {
          
            Console.WriteLine(connectionId + " Data: " + buff.Array.Length+ ":"+ Encoding.UTF8.GetString(buff.Array,buff.Offset, buff.Count));
            Console.WriteLine(connectionId + " Data: " + BitConverter.ToString(buff.Array, buff.Offset, buff.Count));
            byte[] bt = buff.Array;
            messagesReceived++;
            dataReceived += buff.Count;
            for (int i = 0; i < 10; i++)
            {
                Send(connectionId, bt);

            }

        }

        /// <summary>
        /// 断线事件
        /// </summary>
        public void OnDisconnected(int connectionId)
        {
            Console.WriteLine(connectionId + " Disconnected");
        }
    }
}
