using System;
using System.Collections.Generic;
using CSocket;
using CommonLib;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.Net
{
    public partial class GameToCenterClientAction
    {
        private Dictionary<ushort, Action<GameToCenterClientMessage>> _actionList = new Dictionary<ushort, Action<GameToCenterClientMessage>>();
                
         private static readonly GameToCenterClientAction instance = new GameToCenterClientAction();
        public static GameToCenterClientAction Instance => instance;
        private GameToCenterClientAction()
        {
            _actionList.Add(102, Register_GameServer);      //收到注册GameServer
            _actionList.Add(202, Center_PlayerLogin);      //返回中服务登录注册成功返回(成功T下线后才返回)
            _actionList.Add(203, Center_PlayerLogout);      //中央服务器通知GameServer玩家下线
            _actionList.Add(205, Center_PlayerBan);      //中央服务器通知GameServer玩家是否登录
            _actionList.Add(206, Center_PlayerTalk);      //中央服务器通知游戏服务器玩家是否禁言
            _actionList.Add(207, Center_GMMail);      //中央服务器通知游戏服务器玩家发送邮件
            _actionList.Add(250, Center_AIHelpPush);      //通知游戏服玩家有客服推送消息
            _actionList.Add(252, Center_Activity);      //收到中央服更新活动
            _actionList.Add(253, Center_SeasonUpdate);      //通知游戏服赛季更新消息
            _actionList.Add(301, Command);      //向游戏服发送命令行
            _actionList.Add(502, Center_payOrder);      //收到中央服充值下单信息
            _actionList.Add(503, Center_paySucceed);      //收到中央服支付成功通知GS发货
            _actionList.Add(506, Center_PayPack);      //收到中央服更新礼包


        }
        protected void onDispatchMainThread(GameToCenterClientMessage e)
        {
            try
            {
                 ushort protocol = e.Protocol;
                _actionList[protocol].Invoke(e); 
            }
            catch (Exception ex)
            {   
                Logger.LogError(ex.Message+"\n"+ex.StackTrace);
            }
        }
        public void Dispatch(GameToCenterClientMessage e)
        {
            MainThreadContext.Instance.Post(o => {{ onDispatchMainThread(e); }});
        }
    }
}
