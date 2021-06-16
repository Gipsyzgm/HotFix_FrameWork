using PbHero;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求英雄突破
        void Hero_break(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_hero_break msg = e.Msg as CS_hero_break;

            //发送数据
            //SC_hero_break sendMsg = new SC_hero_break();            
            //e.Send(sendMsg);
        }
    }
}
