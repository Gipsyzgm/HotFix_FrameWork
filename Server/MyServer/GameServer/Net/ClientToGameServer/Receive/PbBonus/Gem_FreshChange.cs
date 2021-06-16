using PbBonus;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求刷新精粹兑换
        void Gem_FreshChange(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Gem_FreshChange msg = e.Msg as CS_Gem_FreshChange; 

            //发送数据
            //SC_Gem_FreshChange sendMsg = new SC_Gem_FreshChange();            
            //e.Send(sendMsg);
        }
    }
}
