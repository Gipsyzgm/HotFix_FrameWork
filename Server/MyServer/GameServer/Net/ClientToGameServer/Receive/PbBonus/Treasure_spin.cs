using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求宝藏抽奖
        void Treasure_spin(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_treasure_spin msg = e.Msg as CS_treasure_spin;

            //发送数据
            //SC_treasure_spin sendMsg = new SC_treasure_spin();            
            //e.Send(sendMsg);
        }
    }
}
