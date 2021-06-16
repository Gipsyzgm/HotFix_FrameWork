using PbStore;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求购买商城物品
        void Store_buyItem(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_store_buyItem msg = e.Msg as CS_store_buyItem;

            //发送数据
            //SC_store_buyItem sendMsg = new SC_store_buyItem();            
            //e.Send(sendMsg);
        }
    }
}
