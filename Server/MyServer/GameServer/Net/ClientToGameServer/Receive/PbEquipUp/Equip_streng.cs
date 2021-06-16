using PbEquipUp;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求装备升级
        void Equip_streng(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_equip_streng msg = e.Msg as CS_equip_streng;

            //发送数据
            //SC_equip_streng sendMsg = new SC_equip_streng();            
            //e.Send(sendMsg);
        }
    }
}
