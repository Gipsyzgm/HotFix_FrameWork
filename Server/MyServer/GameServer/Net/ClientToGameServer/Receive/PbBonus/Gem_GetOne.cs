using PbBonus;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求单抽宝石
        void Gem_GetOne(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Gem_GetOne msg = e.Msg as CS_Gem_GetOne; 

            //发送数据
            //SC_Gem_GetOne sendMsg = new SC_Gem_GetOne();            
            //e.Send(sendMsg);
        }
    }
}
