using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telepathy;
using GameServer.Module;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetMgr netMgr = new NetMgr();
            netMgr.StartServer();
           
        }
    }
}
