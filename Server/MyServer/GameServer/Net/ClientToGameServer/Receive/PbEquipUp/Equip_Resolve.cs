using PbEquipUp;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求分解装备
        void Equip_Resolve(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_equip_Resolve msg = e.Msg as CS_equip_Resolve;

            //发送数据
            //SC_equip_Resolve sendMsg = new SC_equip_Resolve();            
            //e.Send(sendMsg);
        }
    }
}
