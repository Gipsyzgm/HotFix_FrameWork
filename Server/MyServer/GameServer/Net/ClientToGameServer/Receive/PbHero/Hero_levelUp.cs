using PbHero;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求英雄升级
        void Hero_levelUp(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_hero_levelUp msg = e.Msg as CS_hero_levelUp;

            //发送数据
            //SC_hero_levelUp sendMsg = new SC_hero_levelUp();            
            //e.Send(sendMsg);
        }
    }
}
