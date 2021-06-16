using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求兑换CDKey
        void Cdkey_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_cdkey_get msg = e.Msg as CS_cdkey_get;

            //发送数据
            //SC_cdkey_get sendMsg = new SC_cdkey_get();            
            //e.Send(sendMsg);
        }
    }
}
