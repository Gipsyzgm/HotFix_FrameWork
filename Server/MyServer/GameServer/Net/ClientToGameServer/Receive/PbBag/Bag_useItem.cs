using PbBag;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求使用物品
        void Bag_useItem(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_bag_useItem msg = e.Msg as CS_bag_useItem;

            //发送数据
            //SC_bag_useItem sendMsg = new SC_bag_useItem();            
            //e.Send(sendMsg);
        }
    }
}
