using PbEquipUp;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求分解宝石
        void Gems_resolve(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_gems_resolve msg = e.Msg as CS_gems_resolve; 

            //发送数据
            //SC_gems_resolve sendMsg = new SC_gems_resolve();            
            //e.Send(sendMsg);
        }
    }
}
