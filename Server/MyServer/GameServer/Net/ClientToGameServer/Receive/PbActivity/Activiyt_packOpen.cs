using PbActivity;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求开启活动礼包
        void Activiyt_packOpen(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Activiyt_packOpen msg = e.Msg as CS_Activiyt_packOpen;

            //发送数据
            //SC_Activiyt_packOpen sendMsg = new SC_Activiyt_packOpen();            
            //e.Send(sendMsg);
        }
    }
}
