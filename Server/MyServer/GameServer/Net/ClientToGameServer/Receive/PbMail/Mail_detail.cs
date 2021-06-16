using PbMail;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求邮件详细信息
        void Mail_detail(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_mail_detail msg = e.Msg as CS_mail_detail;

            //发送数据
            //SC_mail_detail sendMsg = new SC_mail_detail();            
            //e.Send(sendMsg);
        }
    }
}
