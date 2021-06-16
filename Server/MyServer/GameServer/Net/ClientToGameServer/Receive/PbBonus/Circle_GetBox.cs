using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求开启转盘宝箱
        void Circle_GetBox(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Circle_GetBox msg = e.Msg as CS_Circle_GetBox;

            //发送数据
            //SC_Circle_GetBox sendMsg = new SC_Circle_GetBox();            
            //e.Send(sendMsg);
        }
    }
}
