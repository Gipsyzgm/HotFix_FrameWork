using PbHero;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求更换出战英雄
        void Hero_Change(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_hero_Change msg = e.Msg as CS_hero_Change;

            //发送数据
            //SC_hero_Change sendMsg = new SC_hero_Change();            
            //e.Send(sendMsg);
        }
    }
}
