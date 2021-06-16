using PbMail;

namespace GameServer.Net
{
    public partial class ClientToGameServerAction
    {
        //请求删除一个邮件
        void Mail_delete(ClientToGameServerMessage e)
        {
            //收到的数据
            CS_mail_delete msg = e.Msg as CS_mail_delete;

            //发送数据
            //SC_mail_delete sendMsg = new SC_mail_delete();            
            //e.Send(sendMsg);
        }
    }
}
