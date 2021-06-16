using PbRank;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求排行榜数据
        void Rank_List(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_Rank_List msg = e.Msg as CS_Rank_List;

            //发送数据
            //SC_Rank_List sendMsg = new SC_Rank_List();            
            //e.Send(sendMsg);
        }
    }
}
