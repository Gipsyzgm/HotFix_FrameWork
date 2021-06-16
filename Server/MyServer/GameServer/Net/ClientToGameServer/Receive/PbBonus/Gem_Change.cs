using PbBonus;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求精粹兑换
        void Gem_Change(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Gem_Change msg = e.Msg as CS_Gem_Change; 

            //发送数据
            //SC_Gem_Change sendMsg = new SC_Gem_Change();            
            //e.Send(sendMsg);
        }
    }
}
