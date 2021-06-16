using PbEquipUp;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求装备融合
        void Equip_merge(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_equip_merge msg = e.Msg as CS_equip_merge;

            //发送数据
            //SC_equip_merge sendMsg = new SC_equip_merge();            
            //e.Send(sendMsg);
        }
    }
}
