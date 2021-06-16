using System;
using System.Collections.Generic;
using Google.Protobuf;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace LoginServer.Net
{
     public class LoginToCenterClientProtocol
     {
        private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

        private static readonly LoginToCenterClientProtocol instance = new LoginToCenterClientProtocol();
        public static LoginToCenterClientProtocol Instance => instance;
        private HashSet<int> _encryptList = new HashSet<int>();

        private LoginToCenterClientProtocol()
        {
            typeToProtocolDic.Add(typeof(PbGetServer.CS_Get_GameServer), 101); //请求获取人数最少的服务器信息
            typeToProtocolDic.Add(typeof(PbPay.CS_Pay_Succeed), 201); //通知中央服充值付款成功
            typeToProtocolDic.Add(typeof(PbPlayer.CS_AIHelp_Push), 301); //通知中央服AIHelp有推送消息

        }

        /// <summary>
        /// 跟据发送消息类型获取消息协议号
        /// </summary>
        public ushort GetProtocolByType(Type type)
        {
            ushort protocol = 0;
            typeToProtocolDic.TryGetValue(type, out protocol);
            return protocol;
        }
        /// <summary>
        /// 跟据收到的请求协议号创建消息数据结构类
        /// </summary>
        public IMessage CreateMsgByProtocol(ushort protocl)
        {
            IMessage msg = null;
            switch (protocl)
            {
                case 102:msg = new PbGetServer.SC_Get_GameServer(); break;//收到人数最少的服务器信息
                case 202:msg = new PbPay.SC_Pay_Succeed(); break;//收到中央服充值付款成功处理返回
            }
            return msg;
        }
        /// <summary>是否加密协议</summary>
        public bool IsEncryptProtocol(ushort protocl)
        {
            return _encryptList.Contains(protocl);
        }
    }
}
