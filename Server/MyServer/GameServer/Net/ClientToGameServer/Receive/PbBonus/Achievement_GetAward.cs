using PbBonus;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求领取成就奖励
        void Achievement_GetAward(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Achievement_GetAward msg = e.Msg as CS_Achievement_GetAward; 

            //发送数据
            //SC_Achievement_GetAward sendMsg = new SC_Achievement_GetAward();            
            //e.Send(sendMsg);
        }
    }
}
