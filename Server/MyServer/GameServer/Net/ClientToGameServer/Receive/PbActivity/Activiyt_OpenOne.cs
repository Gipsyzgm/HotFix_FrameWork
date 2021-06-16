using PbActivity;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求单个领取节日奖励
        void Activiyt_OpenOne(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Activiyt_OpenOne msg = e.Msg as CS_Activiyt_OpenOne; 

            //发送数据
            //SC_Activiyt_OpenOne sendMsg = new SC_Activiyt_OpenOne();            
            //e.Send(sendMsg);
        }
    }
}
