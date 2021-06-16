using PbLogin;
using UnityEngine;

namespace  HotFix.Net
{
    public partial class ClientToGameClientAction
    {
        //收到服务器通知客户端进入游戏,进入游戏必要数据发送完成
        void Login_enter(ClientToGameClientMessage e)
        {
            //收到的数据
            SC_login_enter msg = e.Msg as SC_login_enter;
            Main.IsDataEnd = true;
            Debug.Log("设置完登录数据");
        }
    }
}
