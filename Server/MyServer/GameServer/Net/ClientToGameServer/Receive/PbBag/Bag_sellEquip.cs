using PbBag;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求出售装备
        void Bag_sellEquip(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_bag_sellEquip msg = e.Msg as CS_bag_sellEquip;

            //发送数据
            //SC_bag_sellEquip sendMsg = new SC_bag_sellEquip();            
            //e.Send(sendMsg);
        }
    }
}
