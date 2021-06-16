using PbStore;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求召唤
        void Summon_buy(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_summon_buy msg = e.Msg as CS_summon_buy;

            //发送数据
            //SC_summon_buy sendMsg = new SC_summon_buy();            
            //e.Send(sendMsg);
        }
    }
}
