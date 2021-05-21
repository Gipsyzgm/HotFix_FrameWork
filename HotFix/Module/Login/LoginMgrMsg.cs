using HotFix.Module.UI;
using HotFix.Net;
using PbLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace HotFix.Module.Login
{
    public partial class LoginMgr : BaseDataMgr<LoginMgr>, IDisposable
    {
        public async CTask<bool> Login(string userName, string pwd)
        {
            string url = $"{AppSetting.HTTPServerURL}ServUrl";
            Debug.LogError("url："+url);
            LoginArgsData data = new LoginArgsData();
            data.PFType = 0;
            data.PFToken = pwd;
            string chidTemple = string.Empty;      
            data.PFId = userName;
            byte[] byteArray = Encoding.UTF8.GetBytes(data.ToString());
            UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(byteArray);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            await request.SendWebRequest();
            if (request.error != null) 
            {
                Tips.ShowLang("Net.ConnectFailed"); //连接服务器失败
                return false;
            }
            else
            {
                HttpMsg httpmsg = HttpMsg.Deserialize(request.downloadHandler.text);
                Debug.Log("request.downloadHandler.text" + request.downloadHandler.text);
                if (httpmsg.code != 0)
                {
                    Tips.Show(httpmsg.msg);
                    return false;
                }
                else
                {
                    ServUrlMsgData serMsg = httpmsg.DeserializeData<ServUrlMsgData>();
                    reLoginServerInfo = serMsg;
                    reLoginPfId = userName;
                    Debug.Log($"连接ip:{serMsg.IP} 端口:{serMsg.Port} ");
                    await HotMgr.Net.Connect(serMsg.IP, serMsg.Port);
                    //连接成功
                    if (HotMgr.Net.IsConnect)
                    {
                        Debug.Log("连接成功发送登陆信息");
                        CS_login_verify msg = new CS_login_verify();
                        msg.LoginType = Enum_login_type.LtAccountPwd;
                        msg.Platform = Enum_login_platform.LpAccountPwd;                    
                        msg.PlatformId = userName;
                        msg.Token = serMsg.Token;
                        msg.ServerId = serMsg.ServerId;
                        msg.Timestamp = serMsg.Timestamp;
                        msg.Lang = (int)HotMgr.Lang.LangType;
                        HotMgr.Net.Send(msg);
                        return true;
                    }
                    else
                    {
                        Tips.ShowLang("Net.ConnectFailed"); //连接服务器失败
                        return true;
                    }
                }
            }
        }

        public async CTask<bool> ReLogin()
        {
            try
            {
                await HotMgr.Net.Connect(reLoginServerInfo.IP, reLoginServerInfo.Port);
                //连接成功
                if (HotMgr.Net.IsConnect)
                {
                    //CS_login_verify msg = new CS_login_verify();
                    //if (AppSetting.PlatformType == EPlatformType.AccountPwd)
                    //{
                    //    msg.Platform = Enum_login_platform.LpAccountPwd;
                    //}
                    //else
                    //{
                    //    msg.Platform = Enum_login_platform.LpCy;
                    //    switch (AppSetting.PlatformType)
                    //    {
                    //        case EPlatformType.AccountPwd:
                    //            msg.ChId = "1";
                    //            break;
                    //        case EPlatformType.OWN_GP:
                    //            msg.ChId = "101";
                    //            break;
                    //        case EPlatformType.OWN_IOS:
                    //            msg.ChId = "102";
                    //            break;
                    //    }
                    //    msg.DeviceId = SDKManager.I.DeviceId;
                    //    msg.Channel = SDKManager.I.MediaChannelId;
                    //    msg.SdkPayCh = SDKManager.I.ChannelId;
                    //}

                    //msg.PlatformId = reLoginPfId;
                    //msg.Token = reLoginServerInfo.Token;
                    //msg.ServerId = reLoginServerInfo.ServerId;
                    //msg.Timestamp = reLoginServerInfo.Timestamp;
                    ////msg.ServerStartTime = HotMgr.Time.ServerStartTime;
                    //msg.Version = Application.version;
                    //msg.Lang = (int)HotMgr.Lang.LangType;
                    //msg.IsReLogin = true;
                    //HotMgr.Net.Send(msg);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //请求服务器
        public async CTask<string> SDKLogin(string userid)
        {
            string url = $"{AppSetting.HTTPServerURL}ServUrl";
            Debug.Log("url     ----" + url + " userid " + userid);
            LoginArgsData data = new LoginArgsData();
            data.PFType = 1;
            data.PFId = userid;
            //data.PFToken = SDKManager.I.Sid;
            //data.ChannelId = SDKManager.I.ChannelId;
            //data.MediaChannelId = SDKManager.I.MediaChannelId;
            //data.Tag = RandomHelper.Random(1, 9999999).ToString();
            //data.DeviceId = SDKManager.I.DeviceId;
            //data.SdkVer = SDKManager.I.SDKVersion;
            data.ClientVer = Application.version;


            Debug.Log($"输出sdk登陆信息:PFId == {data.PFId}," +
                $"PFToken == {data.PFToken}," +
                $"ClientVer == {data.ClientVer}," +
                $"ChannelId == {data.ChannelId}," +
                $"MediaChannelId == {data.MediaChannelId}," +
                $"Tag == {data.Tag}," +
                $"DeviceId == {data.DeviceId}," +
                $"SdkVer == {data.SdkVer},");

            byte[] byteArray = Encoding.UTF8.GetBytes(data.ToString());
            UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(byteArray);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            await request.SendWebRequest();
            if (request.error != null)
            {
                Tips.ShowLang("Net.ConnectFailed"); //连接服务器失败
                return HotMgr.Lang.Get("Net.ConnectFailed");
            }
            else
            {
                HttpMsg httpmsg = HttpMsg.Deserialize(request.downloadHandler.text);
                if (httpmsg.code != 0)
                {
                    Tips.Show(httpmsg.msg);
                    return httpmsg.msg;
                }
                else
                {
                    ServUrlMsgData serMsg = httpmsg.DeserializeData<ServUrlMsgData>();
                    reLoginServerInfo = serMsg;
                    reLoginPfId = data.PFId;
                    await HotMgr.Net.Connect(serMsg.IP, serMsg.Port);
                    //连接成功
                    if (HotMgr.Net.IsConnect)
                    {
                        nowServerId = serMsg.ServerId.ToString();
                        //AIHelpSDK.Instance.SetServerId(nowServerId);

                        //CS_login_verify msg = new CS_login_verify();
                        ////msg.LoginType = Enum_login_type.;
                        //switch (AppSetting.PlatformType)
                        //{
                        //    case EPlatformType.AccountPwd:
                        //        msg.ChId = "1";
                        //        break;
                        //    case EPlatformType.OWN_GP:
                        //        msg.ChId = "101";
                        //        break;
                        //    case EPlatformType.OWN_IOS:
                        //        msg.ChId = "102";
                        //        break;
                        //}

                        //msg.Platform = Enum_login_platform.LpCy;
                        //msg.PlatformId = SDKManager.I.PFId;
                        //msg.Token = serMsg.Token;
                        //msg.ServerId = serMsg.ServerId;
                        //msg.Timestamp = serMsg.Timestamp;
                        //msg.DeviceId = SDKManager.I.DeviceId;
                        //msg.Channel = SDKManager.I.MediaChannelId;
                        //msg.SdkPayCh = SDKManager.I.ChannelId;
                        //msg.Version = Application.version;
                        //msg.Lang = (int)HotMgr.Lang.LangType;
                        //HotMgr.Net.Send(msg);
                        return null;
                    }
                    else
                    {
                        Tips.ShowLang("Net.ConnectFailed"); //连接服务器失败
                        return null;
                    }
                }
            }
        }

    }
}
