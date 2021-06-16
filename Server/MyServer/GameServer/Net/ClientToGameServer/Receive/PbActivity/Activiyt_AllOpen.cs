using PbActivity;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求一键领取节日奖励
        void Activiyt_AllOpen(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Activiyt_AllOpen msg = e.Msg as CS_Activiyt_AllOpen;

            //发送数据
            //SC_Activiyt_AllOpen sendMsg = new SC_Activiyt_AllOpen();            
            //e.Send(sendMsg);
        }
    }
}
