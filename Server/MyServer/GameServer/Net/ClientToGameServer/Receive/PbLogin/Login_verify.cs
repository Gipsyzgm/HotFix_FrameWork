using CommonLib;
using CommonLib.Comm;
using CommonLib.Comm.DBMgr;
using PbCenterPlayer;
using PbLogin;
using System;
using System.Threading.Tasks;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求登录验证
        void Login_verify(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_login_verify msg = e.Msg as CS_login_verify;
            Logger.LogError("收到请求登录验证");
            //发送数据
            var client = e.ClientId;
            //收到
            CS_login_verify data = e.Msg as CS_login_verify;
            Task.Factory.StartNew(() => {
                try
                {                    
                    Login_verify_Task(client, data, e);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }
            });

        }
        void Login_verify_Task(int client, CS_login_verify data, ClientToGameServerMessage e)
        {
            //发送数据
            SC_login_verify sendData = new SC_login_verify();
            sendData.ServerTime = DateTime.Now.ToTimestamp();
            sendData.IsReLogin = data.IsReLogin;
            if (Glob.net.ServerId != data.ServerId)
            {
                sendData.Result = Enum_verify_result.VrServerIdError;
                e.Send(sendData);
                return;
            }
            sendData.Platform = data.Platform;
            int loginType = 0;
            //获取账号信息
            TAccount acc = Glob.tAccountMgr.GetCreateAccount(/*config.id*/0, /*config.pf*/0, data.PlatformId, string.Empty,
                (int)data.LoginType, data.ServerId, data.DeviceId, data.Channel, data.Version, data.SdkPayCh, out loginType);

            acc.sdkCh = data.Channel;
            acc.sdkPayCh = data.SdkPayCh;
            acc.version = data.Version;


            if (acc == null)
            {
                sendData.Result = Enum_verify_result.VrFailure;
                e.Send(sendData);
                return;
            }
            //账号被禁止登陆
            if (acc.isBan)
            {
                sendData.Result = Enum_verify_result.VrBanned;
                e.Send(sendData);
                return;
            }
            sendData.PlatformId = data.PlatformId;

            //向中央服务器发送登录状态请求
            CS_Center_PlayerLogin cmsg = new CS_Center_PlayerLogin();
            cmsg.SessionId = client;
            cmsg.PlayerId = acc.id.ToString();
            cmsg.LoginType = loginType;
            cmsg.IsReLogin = data.IsReLogin;
            Glob.net.gameToCenterClient.SendMsg(cmsg);

            //玩家信息存在,进入游戏
            if (Glob.tPlayerMgr.playerDataList.ContainsKey(acc.id))
            {
                //发送登录成功
                sendData.Result = Enum_verify_result.VrSucceed;
                e.Send(sendData);
                //老玩家登录流程
                Glob.playerMgr.PlayerLogin(acc, e);
            }
            else //玩家信息不存在，通知客户端创建角色
            {
                //发送登录成功
                sendData.Result = Enum_verify_result.VrSucceed;
                e.Send(sendData);
                Glob.playerMgr.NewPlayerLogin(acc, sendData.PlatformId, e);
            }
        }
    }
}
