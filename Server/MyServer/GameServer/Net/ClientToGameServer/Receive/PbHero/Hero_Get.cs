using PbHero;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求英雄获取
        void Hero_Get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_hero_Get msg = e.Msg as CS_hero_Get;

            //发送数据
            //SC_hero_Get sendMsg = new SC_hero_Get();            
            //e.Send(sendMsg);
        }
    }
}
