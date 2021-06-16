using PbStore;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求商城兑换金币
        void Store_exchange(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_store_exchange msg = e.Msg as CS_store_exchange;

            //发送数据
            //SC_store_exchange sendMsg = new SC_store_exchange();            
            //e.Send(sendMsg);
        }
    }
}
