using System;
using System.Collections.Generic;
using CSocket;
using CommonLib;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace LoginServer.Net
{
    public partial class LoginToCenterClientAction
    {
        private Dictionary<ushort, Action<LoginToCenterClientMessage>> _actionList = new Dictionary<ushort, Action<LoginToCenterClientMessage>>();
                
         private static readonly LoginToCenterClientAction instance = new LoginToCenterClientAction();
        public static LoginToCenterClientAction Instance => instance;
        private LoginToCenterClientAction()
        {
            _actionList.Add(102, Get_GameServer);      //收到人数最少的服务器信息
            _actionList.Add(202, Pay_Succeed);      //收到中央服充值付款成功处理返回


        }
        protected void onDispatchMainThread(LoginToCenterClientMessage e)
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
        public void Dispatch(LoginToCenterClientMessage e)
        {
            MainThreadContext.Instance.Post(o => {{ onDispatchMainThread(e); }});
        }
    }
}
