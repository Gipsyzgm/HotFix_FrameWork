using PbBag;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求出售道具
        void Bag_sellProp(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_bag_sellProp msg = e.Msg as CS_bag_sellProp;

            //发送数据
            //SC_bag_sellProp sendMsg = new SC_bag_sellProp();            
            //e.Send(sendMsg);
        }
    }
}
