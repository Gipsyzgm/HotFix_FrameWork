using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求五连抽装备
        void Box_GetEquipfive(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Box_GetEquipfive msg = e.Msg as CS_Box_GetEquipfive;

            //发送数据
            //SC_Box_GetEquipfive sendMsg = new SC_Box_GetEquipfive();            
            //e.Send(sendMsg);
        }
    }
}
