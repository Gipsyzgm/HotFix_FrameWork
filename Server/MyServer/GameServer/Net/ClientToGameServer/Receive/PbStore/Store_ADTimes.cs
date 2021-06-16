using PbStore;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取一次商城广告奖励
        void Store_ADTimes(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_store_ADTimes msg = e.Msg as CS_store_ADTimes;

            //发送数据
            //SC_store_ADTimes sendMsg = new SC_store_ADTimes();            
            //e.Send(sendMsg);
        }
    }
}
