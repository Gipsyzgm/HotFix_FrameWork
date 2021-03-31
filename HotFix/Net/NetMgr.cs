using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CSF;
using CSF.Tasks;
using Google.Protobuf;
using HotFix_MK.Common;
using HotFix_MK.CSocket;
using HotFix_MK.Login;
using HotFix_MK.Module.Login.DataMgr;
using UnityEngine;
using Telepathy;
using PbWar;

namespace HotFix_MK.Net
{
    public partial class NetMgr
    {
        Client _socket;
        private bool _isAlertMsg = true;

        //断线是否走断线重连
        public NetMgr()
        {
            _socket = new Client();
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        public async CTask Connect(string ip, int port)
        {
            _socket.Connect(ip, port);
            await CTask.WaitUntil(() => { return !_socket.Connecting; });
        }

        public bool IsConnect => _socket.Connected;
        public bool mIsConnect = false;
        private Queue<IMessage> noSendMessage = new Queue<IMessage>();
        /// <summary>
        /// 发送消息
        /// </summary>        
        public void Send<T>(T data) where T : IMessage
        {
            if (!IsConnect)
            {
                CLog.Error("服务器未连接");
                //if (!Mgr.UI.IsOpen<Confirm>())
                //    showDisconnectConfirm();
                if (data.GetType() == typeof(CS_war_result))
                {
                    noSendMessage.Enqueue(data);
                }
                return;
            }
            ushort protocol = ClientToGameClientProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {
                CLog.Error("ProtocolType 协议号未定义,协议类型:", data.GetType());
                return;
            }
            if (!MsgWaiting.Show(protocol)) //显示并判断可发送
                return;
            byte[] body = data.ToByteArray();
            LogMsg(true, protocol, body.Length, data);

            byte[] md5 = null;
            if (ClientToGameClientProtocol.Instance.IsEncryptProtocol(protocol))
                md5 = CSocketUtils.MD5Encrypt(body, protocol);

            byte[] package;
            if (md5 != null)
                package = new byte[body.Length + 2 + md5.Length];
            else
                package = new byte[body.Length + 2];
            Array.Copy(BitConverter.GetBytes(protocol), 0, package, 0, 2);  //协议号
            Array.Copy(body, 0, package, 2, body.Length);
            if (md5 != null)
                Array.Copy(md5, 0, package, 2 + body.Length, md5.Length);
            _socket.Send(package);
        }
        public void Update()
        {
            if (_socket.ReceiveQueueCount>0)
            {                
                while (_socket.GetNextMessage(out var msg))
                {
                    switch (msg.eventType)
                    {
                        case EventType.Connected:
                            //OnConnected(SocketError.Success);
                            break;
                        case EventType.Data:
                            OnServerDataReceived(msg.data);
                            break;
                        case EventType.Disconnected:
                            DisconnectEvent();
                            break;
                    }
                }
            }
        }

        protected void OnServerDataReceived(byte[] buff)
        {
            ushort protocol = BitConverter.ToUInt16(buff, 0);   //协议号 

            byte[] body;
            if (ClientToGameClientProtocol.Instance.IsEncryptProtocol(protocol))
            {
                body = CSocketUtils.MD5Decode(buff, protocol);
                if (body == null)
                {
                    CLog.Error("ProtocolType 解析错误:protocol" + protocol);
                    MsgWaiting.Close(protocol);
                    return;
                }
            }
            else
            {
                body = new byte[buff.Length - 2];            //消息内容
                Array.Copy(buff, 2, body, 0, body.Length);
            }
            IMessage proto = ClientToGameClientProtocol.Instance.CreateMsgByProtocol(protocol);
            if (proto == null)
            {
                CLog.Error("ProtocolType 协议类型未定义:protocol", protocol);
                MsgWaiting.Close(protocol);
                return;
            }
            try
            {
                proto.MergeFrom(body);
                //暂时用Message 后面可以不需要
                ClientToGameClientMessage msg = new ClientToGameClientMessage(protocol, proto);
                LogMsg(false, protocol, body.Length, proto);
                ClientToGameClientAction.Instance.Dispatch(msg);
            }
            catch
            {
                CLog.Error($"消息 {protocol} 解析错误,可能客户端与服务端PB文件不一致");
            }
            finally
            {
                MsgWaiting.Close(protocol);
            }
        }

        /// <summary>
        /// 断线事件
        /// </summary>
        public void DisconnectEvent()
        {
            CLog.Error("断开连接!!!");
            if (ReLoginMgr.IsReLogin)
            {
                ReLoginMgr.Show(); //走重连
            }
            else
            {
                if (!_isAlertMsg) return;
                showDisconnectConfirm();
            }
        }

        public void showDisconnectConfirm()
        {
            Common.Confirm.AlertLangTop(() =>
            {
                if (AppSetting.PlatformType != EPlatformType.AccountPwd)
                {
                    CYSDK.Instance.logout();
                    CLog.Log("logout");
                }
                Mgr.Dispose();
                Mgr.UI.Show<LoginUI>();
                LoginMgr.I.isConnectIng = false;
                ReLoginMgr.Close();
               
            }, "Net.Disconnect", "Net.DisconnectTitle").Run(); //与服务器断开连接
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        private void LogMsg(bool isSend, ushort protocol, int length, IMessage msg)
        {
            if (protocol < 10) return;
            CLog.LogMsg(isSend, $"{(isSend ? "发送" : "收到")}[{protocol } L:{length} {msg.GetType().FullName}]:{Dumper.DumpAsString(msg)}");
        }

        /// <summary>
        /// 主动断开连接
        /// </summary>
        public void Close(bool isAlertMsg = true)
        {
            CLog.Log("主动断开Socket连接");
            ReLoginMgr.IsReLogin = false;
            if (IsConnect)
            {
                _isAlertMsg = isAlertMsg;
                _socket.Disconnect();
                //Dispose();
                //_socket.SendString("");
            }
            noSendMessage.Clear();
        }
        //重连短开测试
        public void ReClose()
        {
            _socket.Disconnect();
        }

        public void ReLoginSendMessage()
        {
            while (noSendMessage.Count > 0)
            {
                var data = noSendMessage.Dequeue();
                Send(data);
            }
        }

        //bool isReconnectTest = false;
        //public void TestReconnect()
        //{
        //    CLog.Log("断线测试");
        //    isReconnectTest = true;
        //    _socket.Disconnect();
        //}
    }
}