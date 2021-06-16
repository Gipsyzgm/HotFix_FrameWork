using PbEquipUp;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求装备升阶
        void Equip_break(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_equip_break msg = e.Msg as CS_equip_break;

            //发送数据
            //SC_equip_break sendMsg = new SC_equip_break();            
            //e.Send(sendMsg);
        }
    }
}
