using HotFix.Module.UI;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace HotFix.Module.Login
{   
    /// <summary>
    /// 服务器列表数据
    /// </summary>
    public class ServerListMgr : BaseDataMgr<ServerListMgr>, IDisposable
    {
        private string Prefs_ServerURL = "Prefs_ServerURL";

        /// <summary>服务器列表</summary>
        public Dictionary<string, ServerItemData> dicServerList = new Dictionary<string, ServerItemData>();

        /// <summary>
        /// 选中的服务器地址
        /// </summary>
        public string ServerURL = string.Empty;

        /// <summary>
        /// 是否获取到服务器信息
        /// </summary>
        public bool IsGetServerData = false;

        public ServerListMgr()
        {
            ServerURL = PlayerPrefs.GetString(Prefs_ServerURL, string.Empty);
        }

        /// <summary>
        /// 获取当前选中的服务器数据
        /// </summary>
        public ServerItemData GetSelectServer()
        {
            dicServerList.TryGetValue(ServerURL, out var data);
            if (data == null && dicServerList.Count > 0)
                return dicServerList.Values.ElementAt(0);
            return data;
        }

        public int GetSelectServerId()
        {
            ServerItemData data;
            if (dicServerList.TryGetValue(ServerURL, out data))
                return data.ServerId;
            return 1;
        }

        public void SetServerId(string url)
        {
            if (ServerURL != url)
            {
                ServerURL = url;
                if (HotMgr.Net.IsConnect == true)
                    HotMgr.Net.Close(false);
                HotMgr.UI.GetPanel<LoginUI>()?.ResetConnectInt();
            }
            PlayerPrefs.SetString(Prefs_ServerURL, ServerURL);
            PlayerPrefs.Save();
            HotMgr.UI.GetPanel<LoginUI>()?.SetServerInfo();
        }
        /// <summary>
        /// 请求服务器列表
        /// </summary>
        public async CTask ReqServerList()
        {                
            string serverListFilesURL = AppSetting.HTTPServerURL + "ServerList";
            Debug.Log(serverListFilesURL);
            //请求服务器列表看看需不需要带什么参数，按需添加
            JsonData fields = new JsonData();
            fields["PF"] = Utility.GetPlatformName();
            byte[] byteArray = Encoding.UTF8.GetBytes(fields.ToJson());          
            UnityWebRequest request = new UnityWebRequest(serverListFilesURL, UnityWebRequest.kHttpVerbPOST);
            request.uploadHandler = new UploadHandlerRaw(byteArray);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            await request.SendWebRequest();
            if (request.error != null || request.downloadHandler.text == "error")
            {
                Debug.Log($"URL Error[{serverListFilesURL}]:{request.error} ");
                //请求资源信息错误
                Confirm.Show(() => { ReqServerList().Run();}, () => { Application.Quit(); }, HotMgr.Lang.Get("ServerList.Failed"));
                return;
            }
            else 
            {            
                JsonData jsonData = JsonMapper.ToObject(request.downloadHandler.text);
                if ((int)jsonData["code"] != 0)
                {
                    Confirm.Show(() => { ReqServerList().Run(); }, () => { Application.Quit(); }, (string)jsonData["msg"]); ;
                    return;
                }
                else
                {
                    dicServerList = new Dictionary<string, ServerItemData>();
                    List<ServerItemData> serverList = LitJson.JsonMapper.ToObject<List<ServerItemData>>((string)jsonData["data"]);
                    foreach (ServerItemData data in serverList)
                        dicServerList.Add(data.URL, data);
                }

            }         
            //编辑器模式才添加几个开发服务器
            if (Application.isEditor)
            {
                ServerItemData item = new ServerItemData();
                item.ServerId = 999;
                item.ServerName = "Jsf-测试服";
                item.URL = "http://127.0.0.1:7000";
                if (!dicServerList.ContainsKey(item.URL))
                    dicServerList.Add(item.URL, item);                     
            }

            if (AppSetting.PlatformType == EPlatformType.AccountPwd)
            {
                HotMgr.UI.GetPanel<LoginUI>()?.SetServerInfo();
            }
            IsGetServerData = true;
        }

        public override void Dispose()
        {

        }
    }
}
