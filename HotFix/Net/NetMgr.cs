using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using Telepathy;
using Google.Protobuf;
using HotFix.Net;
using CSocket;

namespace HotFix
{
    public partial class NetMgr
    {
        Client _socket;
        private bool _isAlertMsg = true;
        public const int MaxMessageSize = 16 * 1024;
        //断线是否走断线重连
        public NetMgr()
        {
            _socket = new Client(MaxMessageSize);
            Telepathy.Log.Info = Debug.Log;
            Telepathy.Log.Warning = Debug.LogWarning;
            Telepathy.Log.Error = Debug.LogError;
            _socket.OnConnected = OnConnected;
            _socket.OnData = OnData;
            _socket.OnDisconnected = OnDisconnected;
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
        //private Queue<IMessage> noSendMessage = new Queue<IMessage>();
        /// <summary>
        /// 发送消息
        /// </summary>        
        public void Send<T>(T data) where T : IMessage
        {
            if (!IsConnect)
            {
                Debug.LogError("服务器未连接");
                //if (! Mgr.UI.IsOpen<Confirm>())
                //    showDisconnectConfirm();
                return;
            }
            ushort protocol = ClientToGameClientProtocol.Instance.GetProtocolByType(data.GetType());
            if (protocol == 0)
            {
                Debug.LogError("ProtocolType 协议号未定义,协议类型:" + data.GetType());
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
            _socket.Send(new ArraySegment<byte>(package));
        }

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        private void LogMsg(bool isSend, ushort protocol, int length, IMessage msg)
        {
            if (protocol < 10) return;
            Debug.Log($"{(isSend ? "发送" : "收到")}[{protocol } L:{length} {msg.GetType().FullName}]:{Dumper.DumpAsString(msg)}");
        }
        public void TestSend(byte[] data) 
        {
            if (!IsConnect)
            {
                Debug.LogError("服务器未连接");              
                return;
            }       
            _socket.Send(new ArraySegment<byte>(data));
        }
        public void Update()
        {
            if (_socket.ReceivePipeCount> 0)
            {
                _socket.Tick(1);              
            }
        }
        public void OnConnected()
        {
            Debug.LogError("已经连接服务器!");
           
        }

        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="buff"></param>
        public void OnData(ArraySegment<byte> buff)
        {
            //收到消息解析一下，获取真实长度的数据
            byte[] bytes = new byte[buff.Count];
            Buffer.BlockCopy(buff.Array, buff.Offset, bytes, 0, buff.Count);

            Debug.LogError("收到回复信息，信息长度："+ bytes.Length);
            Debug.LogError(System.Text.Encoding.Default.GetString(bytes) + ":111");


        }

        /// <summary>
        /// 断线事件
        /// </summary>
        public void OnDisconnected()
        {
            Debug.LogError("断开连接!!!");
            //if (ReLoginMgr.IsReLogin)
            //{
            //    ReLoginMgr.Show(); //走重连
            //}
            //else
            //{
            //    if (!_isAlertMsg) return;
            //    showDisconnectConfirm();
            //}
        }

        ///// <summary>
        ///// 收到消息
        ///// </summary>
        ///// <param name="buff"></param>
        //protected void OnServerDataReceived(byte[] buff)
        //{
        //    ushort protocol = BitConverter.ToUInt16(buff, 0);   //协议号 

        //    byte[] body;
        //    if (ClientToGameClientProtocol.Instance.IsEncryptProtocol(protocol))
        //    {
        //        body = CSocketUtils.MD5Decode(buff, protocol);
        //        if (body == null)
        //        {
        //            Debug.LogError("ProtocolType 解析错误:protocol" + protocol);
        //            MsgWaiting.Close(protocol);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        body = new byte[buff.Length - 2];            //消息内容
        //        Array.Copy(buff, 2, body, 0, body.Length);
        //    }
        //    IMessage proto = ClientToGameClientProtocol.Instance.CreateMsgByProtocol(protocol);
        //    if (proto == null)
        //    {
        //        Debug.LogError("ProtocolType 协议类型未定义:protocol"+protocol);
        //        MsgWaiting.Close(protocol);
        //        return;
        //    }
        //    try
        //    {
        //        proto.MergeFrom(body);
        //        //暂时用Message 后面可以不需要
        //        ClientToGameClientMessage msg = new ClientToGameClientMessage(protocol, proto);
        //        LogMsg(false, protocol, body.Length, proto);
        //        ClientToGameClientAction.Instance.Dispatch(msg);
        //    }
        //    catch
        //    {
        //        Debug.LogError($"消息 {protocol} 解析错误,可能客户端与服务端PB文件不一致");
        //    }
        //    finally
        //    {
        //        MsgWaiting.Close(protocol);
        //    }
        //}



        //public void showDisconnectConfirm()
        //{
        //    Common.Confirm.AlertLangTop(() =>
        //    {
        //        if (AppSetting.PlatformType != EPlatformType.AccountPwd)
        //        {
        //            CYSDK.Instance.logout();
        //            CLog.Log("logout");
        //        }
        //        Mgr.Dispose();
        //        Mgr.UI.Show<LoginUI>();
        //        LoginMgr.I.isConnectIng = false;
        //        ReLoginMgr.Close();

        //    }, "Net.Disconnect", "Net.DisconnectTitle").Run(); //与服务器断开连接
        //}

        /// <summary>
        /// 通讯消息日志
        /// </summary>
        /// <param name="args"></param>
        //private void LogMsg(bool isSend, ushort protocol, int length, IMessage msg)
        //{
        //    if (protocol < 10) return;
        //    Debug.LogError(isSend, $"{(isSend ? "发送" : "收到")}[{protocol } L:{length} {msg.GetType().FullName}]:{Dumper.DumpAsString(msg)}");
        //}

        /// <summary>
        /// 主动断开连接
        /// </summary>
        public void Close(bool isAlertMsg = true)
        {
            Debug.Log("主动断开Socket连接");
            //ReLoginMgr.IsReLogin = false;
            if (IsConnect)
            {
                _isAlertMsg = isAlertMsg;
                _socket.Disconnect();
                
            }
            //noSendMessage.Clear();
        }
        //重连短开测试
        public void ReClose()
        {
            _socket.Disconnect();
        }

        //public void ReLoginSendMessage()
        //{
        //    while (noSendMessage.Count > 0)
        //    {
        //        var data = noSendMessage.Dequeue();
        //        Send(data);
        //    }
        //}

        //bool isReconnectTest = false;
        //public void TestReconnect()
        //{
        //    CLog.Log("断线测试");
        //    isReconnectTest = true;
        //    _socket.Disconnect();
        //}
    }
}