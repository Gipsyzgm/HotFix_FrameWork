using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer
{
    /// <summary>
    /// 服务器信息管理
    /// </summary>
    public class ServerStateMgr
    {
        /// <summary>服务器标识</summary>
        public static string serverTag = ConfigurationManager.AppSettings["ServerTag"];
        /// <summary>当前服务器信息</summary>
        public TServerInfo servInfo;
        public ServerStateMgr()
        {
            GetServInfo();
        }
        /// <summary>
        /// 查询数据库获取当前信息服务器信息
        /// </summary>
        public void GetServInfo()
        {    
            List<TServerInfo> servList = MongoDBHelper.Instance.Select<TServerInfo>();
            if (servList != null && servList.Count > 0)
                servInfo = servList[0];
        }

        /// <summary>
        /// 简单的校验key
        /// </summary>
        /// <param name="time"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckKey(string time, string key)
        {
            if (key == StringHelper.MD5($"{time}&{Glob.http.gameKey}"))
                return true;
            return false;
        }
    }
}
