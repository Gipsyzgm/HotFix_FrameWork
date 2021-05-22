using CenterServer.Net;
using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterServer
{
    public partial class NetMgr
    {

        public LoginToCenterServer loginToCenterServer;
        public GameToCenterServer gameToCenterServer;
   
        public NetMgr()
        {
        }

        public bool Start()
        {
            loginToCenterServer = new LoginToCenterServer();
            bool isLoginToCentert = loginToCenterServer.StartForConfig();
            if (isLoginToCentert) 
            {
                Logger.Sys("LoginToCenterServer 启动成功!");
                loginToCenterServer.StartTick();
            }
              



            gameToCenterServer = new GameToCenterServer();
            bool isGameToCenter = gameToCenterServer.StartForConfig();
            if (isGameToCenter) 
            {
                Logger.Sys("GameToCenterSocket启动成功!");
                gameToCenterServer.StartTick();
            }
              

            return isLoginToCentert && isGameToCenter;
        }

        public void LoginToCenterServerMessage_Received(LoginToCenterServerMessage args)
        {
            LoginToCenterServerAction.Instance.Dispatch(args);
            
        }

        //public void GMToCenterServerMessage_Received(GMToCenterServerMessage args)
        //{
        //    GMToCenterServerAction.Instance.Dispatch(args);
            
        //}

        public void GameToCenterServerMessage_Received(GameToCenterServerMessage args)
        {
            GameToCenterServerAction.Instance.Dispatch(args);
            
        }

    }
}
