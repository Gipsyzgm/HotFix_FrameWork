using PbMail;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求一键领取所有未读邮件
        void Mail_openAll(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_mail_openAll msg = e.Msg as CS_mail_openAll;

            //发送数据
            //SC_mail_openAll sendMsg = new SC_mail_openAll();            
            //e.Send(sendMsg);
        }
    }
}
