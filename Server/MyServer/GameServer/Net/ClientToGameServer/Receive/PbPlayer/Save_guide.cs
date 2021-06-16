using PbPlayer;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //保存指引步骤
        void Save_guide(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_save_guide msg = e.Msg as CS_save_guide;

            //发送数据
            //SC_save_guide sendMsg = new SC_save_guide();            
            //e.Send(sendMsg);
        }
    }
}
