using System;
using System.Collections.Generic;
using Google.Protobuf;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameServer.Net
{
     public class GameToCenterClientProtocol
     {
        private Dictionary<Type, ushort> typeToProtocolDic = new Dictionary<Type, ushort>();

        private static readonly GameToCenterClientProtocol instance = new GameToCenterClientProtocol();
        public static GameToCenterClientProtocol Instance => instance;
        private HashSet<int> _encryptList = new HashSet<int>();

        private GameToCenterClientProtocol()
        {
            typeToProtocolDic.Add(typeof(PbRegister.CS_Register_GameServer), 101); //请求注册GameServer
            typeToProtocolDic.Add(typeof(PbCenterPlayer.CS_Center_PlayerLogin), 201); //请求中央服务器玩家登录注册
            typeToProtocolDic.Add(typeof(PbCenterPlayer.CS_Center_PlayerLogout), 204); //GameServer通知中央务器通知GameServer玩家下线
            typeToProtocolDic.Add(typeof(PbCenterPlayer.CS_Center_AIHelpPush), 251); //向中央服请求玩家未发送的推送消息（登录时）
            typeToProtocolDic.Add(typeof(PbGameSerCommand.CS_Command), 302); //游戏服返回命令行信息
            typeToProtocolDic.Add(typeof(PbCenterPay.CS_Center_payOrder), 501); //向中央服请求充值下定单
            typeToProtocolDic.Add(typeof(PbCenterPay.CS_Center_payNoSend), 504); //向中央服请求未发货订单（登录时）

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
                case 102:msg = new PbRegister.SC_Register_GameServer(); break;//收到注册GameServer
                case 202:msg = new PbCenterPlayer.SC_Center_PlayerLogin(); break;//返回中服务登录注册成功返回(成功T下线后才返回)
                case 203:msg = new PbCenterPlayer.SC_Center_PlayerLogout(); break;//中央服务器通知GameServer玩家下线
                case 205:msg = new PbCenterPlayer.SC_Center_PlayerBan(); break;//中央服务器通知GameServer玩家是否登录
                case 206:msg = new PbCenterPlayer.SC_Center_PlayerTalk(); break;//中央服务器通知游戏服务器玩家是否禁言
                case 207:msg = new PbCenterPlayer.SC_Center_GMMail(); break;//中央服务器通知游戏服务器玩家发送邮件
                case 250:msg = new PbCenterPlayer.SC_Center_AIHelpPush(); break;//通知游戏服玩家有客服推送消息
                case 252:msg = new PbCenterPlayer.SC_Center_Activity(); break;//收到中央服更新活动
                case 253:msg = new PbCenterPlayer.SC_Center_SeasonUpdate(); break;//通知游戏服赛季更新消息
                case 301:msg = new PbGameSerCommand.SC_Command(); break;//向游戏服发送命令行
                case 502:msg = new PbCenterPay.SC_Center_payOrder(); break;//收到中央服充值下单信息
                case 503:msg = new PbCenterPay.SC_Center_paySucceed(); break;//收到中央服支付成功通知GS发货
                case 506:msg = new PbCenterPay.SC_Center_PayPack(); break;//收到中央服更新礼包
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
