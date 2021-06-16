using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取等级奖励
        void LevelAward_get(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_levelAward_get msg = e.Msg as CS_levelAward_get;

            //发送数据
            //SC_levelAward_get sendMsg = new SC_levelAward_get();            
            //e.Send(sendMsg);
        }
    }
}
