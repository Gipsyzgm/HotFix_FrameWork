using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求单抽英雄
        void Box_GetHero(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Box_GetHero msg = e.Msg as CS_Box_GetHero;

            //发送数据
            //SC_Box_GetHero sendMsg = new SC_Box_GetHero();            
            //e.Send(sendMsg);
        }
    }
}
