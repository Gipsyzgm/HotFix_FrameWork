using PbEquipUp;
namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求镶嵌(卸下)宝石
        void Gems_Inlay(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_gems_Inlay msg = e.Msg as CS_gems_Inlay; 

            //发送数据
            //SC_gems_Inlay sendMsg = new SC_gems_Inlay();            
            //e.Send(sendMsg);
        }
    }
}
