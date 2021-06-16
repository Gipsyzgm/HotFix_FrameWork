using PbBonus;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求每日签到领奖
        void SignIn_award(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_signIn_award msg = e.Msg as CS_signIn_award;

            //发送数据
            //SC_signIn_award sendMsg = new SC_signIn_award();            
            //e.Send(sendMsg);
        }
    }
}
