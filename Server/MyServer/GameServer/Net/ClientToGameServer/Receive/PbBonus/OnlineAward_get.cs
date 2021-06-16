using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取在线奖励
        void OnlineAward_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_onlineAward_get msg = e.Msg as CS_onlineAward_get;

            //发送数据
            //SC_onlineAward_get sendMsg = new SC_onlineAward_get();            
            //e.Send(sendMsg);
        }
    }
}
