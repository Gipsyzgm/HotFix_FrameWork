using PbBonus;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求五连抽宝石
        void Gem_GetFive(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Gem_GetFive msg = e.Msg as CS_Gem_GetFive; 

            //发送数据
            //SC_Gem_GetFive sendMsg = new SC_Gem_GetFive();            
            //e.Send(sendMsg);
        }
    }
}
