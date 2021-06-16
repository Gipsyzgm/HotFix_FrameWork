using PbPay;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取VIP礼包
        void Vip_buyGift(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_vip_buyGift msg = e.Msg as CS_vip_buyGift;

            //发送数据
            //SC_vip_buyGift sendMsg = new SC_vip_buyGift();            
            //e.Send(sendMsg);
        }
    }
}
