using PbEquipUp;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求更换装备
        void Equip_Change(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_equip_Change msg = e.Msg as CS_equip_Change;

            //发送数据
            //SC_equip_Change sendMsg = new SC_equip_Change();            
            //e.Send(sendMsg);
        }
    }
}
