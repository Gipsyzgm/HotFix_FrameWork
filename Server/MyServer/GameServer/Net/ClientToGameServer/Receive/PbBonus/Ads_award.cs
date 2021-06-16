using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取广告奖励
        void Ads_award(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_ads_award msg = e.Msg as CS_ads_award;

            //发送数据
            //SC_ads_award sendMsg = new SC_ads_award();            
            //e.Send(sendMsg);
        }
    }
}
