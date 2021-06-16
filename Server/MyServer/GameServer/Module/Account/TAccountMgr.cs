using MongoDB.Bson;
using System;
using PbLogin;
using System.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CommonLib.Comm.DBMgr;
using CommonLib;

namespace GameServer.Module
{
    /// <summary>
    /// 账号数据管理器
    /// </summary>
    public class TAccountMgr
    {
        /// <summary>账号列表</summary>
        public DictionarySafe<ObjectId, TAccount> accountList;

        /// <summary>账号唯一ID对应的账号信息</summary>
        public DictionarySafe<AccountUniqueID, TAccount> accUIDList = new DictionarySafe<AccountUniqueID, TAccount>();
        
        public TAccountMgr()
        {
            accountList = DBReader.Instance.SelectAll<TAccount>();
            AccountUniqueID accUID;
            foreach (TAccount acc in accountList.Values)
            {
                accUID = new AccountUniqueID(acc.pfType, acc.pfId);
                if (accUIDList.ContainsKey(accUID))
                    Logger.LogError("AccountMgr.accUIDList 有相同Id:" + accUID.ToString());
                else
                    accUIDList.Add(accUID, acc);
            }
        }

        /// <summary>
        /// 获取账号信息
        /// 如果账号不存存，创建一个新的账号，存存则更新登录时间
        /// </summary>
        public TAccount GetCreateAccount(int pfCh,int pf,string platformid,string pfUserMail, 
            int loginType, int serverid, string deviceId, string sdkChannel, string version, string sdkPayChannel, out int type)
        {
            FilterDefinition<TAccount> filter = Builders<TAccount>.Filter.Eq(t =>t.pfType, pf) & Builders<TAccount>.Filter.Eq(t=>t.pfId,platformid);
            TAccount acc = MongoDBHelper.Instance.SelectFilter(filter).FirstOrDefault();
            type = 2;
            if (acc == null)
            {
                type = 1;
                acc = new TAccount(true);
                acc.pfType = pf;
                acc.pfCh = pfCh;
                acc.pfId = platformid;
                acc.loginType = loginType;
                acc.serverId = serverid;
                acc.pfEmail = pfUserMail;
                acc.regDate = DateTime.Now;
                acc.deviceId = deviceId;
                acc.sdkCh = sdkChannel;
                acc.version = version;
                acc.sdkPayCh = sdkPayChannel;
                acc.Insert();

            }
            

            AccountUniqueID accUID = new AccountUniqueID(pf, platformid);            
            return acc;


        }
    }
}
