using PbWar;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求随机商人购买
        void War_TraderBuy(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_war_TraderBuy msg = e.Msg as CS_war_TraderBuy;

            //发送数据
            //SC_war_TraderBuy sendMsg = new SC_war_TraderBuy();            
            //e.Send(sendMsg);
        }
    }
}
