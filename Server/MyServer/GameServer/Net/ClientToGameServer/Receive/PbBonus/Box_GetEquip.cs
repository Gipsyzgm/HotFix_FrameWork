using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求单抽装备
        void Box_GetEquip(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Box_GetEquip msg = e.Msg as CS_Box_GetEquip;

            //发送数据
            //SC_Box_GetEquip sendMsg = new SC_Box_GetEquip();            
            //e.Send(sendMsg);
        }
    }
}
