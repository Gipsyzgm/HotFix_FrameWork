using PbMail;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求打开邮件
        void Mail_open(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_mail_open msg = e.Msg as CS_mail_open;

            //发送数据
            //SC_mail_open sendMsg = new SC_mail_open();            
            //e.Send(sendMsg);
        }
    }
}
