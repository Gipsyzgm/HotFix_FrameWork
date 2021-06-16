using CommonLib;
using LoginServer.Http;
using PbGetServer;
using System;
using System.IO;

namespace  LoginServer.Net
{
    public partial class LoginToCenterClientAction
    {
        //收到人数最少的服务器信息
        void Get_GameServer(LoginToCenterClientMessage e)
        {
            //收到的数据
            SC_Get_GameServer msg = e.Msg as SC_Get_GameServer;        
            //收到中心服的信息
            if (Glob.net.dicHttpContext.TryGetValue(msg.ReqUID, out var context))
            {
                if (msg.ServerId == 0)
                {
                    Glob.http.ResponseOutput(context.Response, HttpMsg.ErrorString("No servers are available"));
                }
                else
                {
                    try
                    {                        
                        LoginArgsData data;
                        string loginArgs = "";
                        using (StreamReader reader = new StreamReader(context.Request.InputStream))
                        {
                            loginArgs = reader.ReadToEnd();
                            data = LoginArgsData.Deserialize(loginArgs);
                            reader.Close();
                        }
                        if (Glob.serverStateMgr.servInfo != null)
                        {
                            if (Glob.serverStateMgr.servInfo.State == 1)
                            {
                                Glob.http.ResponseOutput(context.Response, HttpMsg.ErrorString("Server is under maintenance."));
                                return;
                            }
                        }                       
                        bool isVerify = false;
                        //登录验证
                        switch (data.PFType)
                        {
                            case 0://账密登录
                                isVerify = true;
                                break;
                            case 1://sdk

                                break;
                            case 2://unity一键登录玩家账号
                                if (data.PFToken == StringHelper.MD5($"{data.PFId}{Glob.http.gameKey}", true).Substring(0, 16))
                                    isVerify = true;
                                break;
                        }

                        if (!isVerify)
                        {
                            Glob.http.ResponseOutput(context.Response, HttpMsg.ErrorString("Login authentication failed"));
                        }
                        ServUrlMsgData rtnMsg = new ServUrlMsgData();
                        rtnMsg.ServerId = msg.ServerId;
                        rtnMsg.IP = msg.IP;
                        rtnMsg.Port = msg.Port;
                        rtnMsg.Timestamp = DateTime.Now.ToTimestamp();
                        rtnMsg.Token = StringHelper.CreateToken(data.PFId, msg.ServerId, rtnMsg.Timestamp, Glob.http.gameKey);
                        Glob.http.ResponseOutput(context.Response, HttpMsg.Message(rtnMsg.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        Glob.http.ResponseOutput(context.Response, HttpMsg.ErrorString("Failed to obtain server information"));
                    }
                }
            }

        }
    }
}
