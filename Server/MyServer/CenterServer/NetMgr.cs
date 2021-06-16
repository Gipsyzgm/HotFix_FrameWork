using CenterServer.Net;
using CommonLib;
using CommonLib.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenterServer
{
    public partial class NetMgr
    {

        public LoginToCenterServer loginToCenterServer;
        public GameToCenterServer gameToCenterServer;
        public bool isLoginToCentert = false;
        public bool isGameToCenter = false;
        public NetMgr()
        {

        }

        public bool Start()
        {
            ServerElement config = ServerSet.Instance.GetConfig("LoginToCenterServer");      
            loginToCenterServer = new LoginToCenterServer(config.receiveBufferSize);
            isLoginToCentert = loginToCenterServer.StartForConfig(config.port);
            if (isLoginToCentert) 
            {
                Logger.Sys("LoginToCenterServer 启动成功!");
                
            }
            ServerElement config1 = ServerSet.Instance.GetConfig("GameToCenterServer");
            gameToCenterServer = new GameToCenterServer(config1.receiveBufferSize);
            isGameToCenter = gameToCenterServer.StartForConfig(config1.port);
            if (isGameToCenter) 
            {
                Logger.Sys("GameToCenterSocket启动成功!");
                
            }       
            return isLoginToCentert && isGameToCenter;
        }
        public void StartTick()
        {
          
            Thread.Sleep(1000/50);
            if (isLoginToCentert)
            {
                loginToCenterServer.Tick(100000);
            }
            if (isGameToCenter)
            {  
                gameToCenterServer.Tick(100000);
            }

        }

        //public void LoginToCenterServerMessage_Received(LoginToCenterServerMessage args)
        //{
        //    LoginToCenterServerAction.Instance.Dispatch(args);
        //    
        //}

        //public void GMToCenterServerMessage_Received(GMToCenterServerMessage args)
        //{
        //    GMToCenterServerAction.Instance.Dispatch(args);

        //}

        //public void GameToCenterServerMessage_Received(GameToCenterServerMessage args)
        //{
        //    GameToCenterServerAction.Instance.Dispatch(args);
        //    
        //}

    }
}
