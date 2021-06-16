using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using MongoDB.Driver;
using PbBag;
using PbMail;
using System;
using System.Collections.Generic;
using System.Linq;
namespace GameServer.Module
{
    public class MailMgr
    {
        ///// <summary>
        ///// 群发邮件列表
        ///// </summary>
        //public DictionarySafe<ObjectId, Mail> MassMailList = new DictionarySafe<ObjectId, Mail>();
        ///// <summary>
        ///// 玩家群发邮件子项列表[邮件ID,[玩家ID,邮件子项数据]]
        ///// </summary>
        //public DictionarySafe<ObjectId, DictionarySafe<ObjectId, TMailSub>> MassMailSubList = new DictionarySafe<ObjectId, DictionarySafe<ObjectId, TMailSub>>();
        ///// <summary>
        ///// 玩家邮件列表[玩家ID,[邮件ID,邮件]]
        ///// </summary>
        //public DictionarySafe<ObjectId, DictionarySafe<ObjectId, Mail>> PlayerMailList = new DictionarySafe<ObjectId, DictionarySafe<ObjectId, Mail>>();

        public MailMgr()
        {
            ////删除过期邮件
            //deleteOverdueMail();
            ////读取邮件信息
            //DictionarySafe<ObjectId, TMail> maillist = DBReader.Instance.SelectAll<TMail>();            
            //foreach (TMail tData in maillist.Values)
            //{
            //    createMail(tData);
            //}

            //// 读取邮件子信息
            //DictionarySafe<ObjectId, TMailSub> mailSubList = DBReader.Instance.SelectAll<TMailSub>();

            //List<ObjectId> delMailSubIds = new List<ObjectId>();
            //DictionarySafe<ObjectId, TMailSub> mailSubPlayer;
            //foreach (TMailSub tData in mailSubList.Values)
            //{
            //    if (maillist.ContainsKey(tData.mId))
            //    {
            //        if (MassMailSubList.TryGetValue(tData.mId, out mailSubPlayer))
            //            mailSubPlayer.AddOrUpdate(tData.pId, tData);
            //    }
            //    else
            //    {
            //        delMailSubIds.Add(tData.mId);
            //    }      
            //}
            //delMailSubIds = delMailSubIds.Distinct().ToList();
            ////删除邮件子项
            //deleteMailSub(delMailSubIds);
        }
        /// <summary>
        ///  清理过期邮件 (超过30天的直接删除)
        /// </summary>
        private void deleteOverdueMail()
        {
            //eq相等   ne、neq不相等，   gt大于， lt小于 gte、ge大于等于   lte、le 小于等于   not非
            //删除过期邮件
            FilterDefinition<TMail> filter = Builders<TMail>.Filter.Lt(t => t.sTime, DateTime.Now.AddDays(-Glob.config.settingConfig.MailOverdueDeleteDay));
            long delCount = MongoDBHelper.Instance.DeleteMany<TMail>(filter);
            Logger.Sys($"删除{delCount}条过期邮件");
        }
        ///// <summary>
        ///// 删除不存在的群发邮件子项
        ///// </summary>
        ///// <param name="mids"></param>
        //private void deleteMailSub(List<ObjectId> mids)
        //{
        //    long delCount = 0;
        //    for (int i = 0; i < mids.Count;i++)
        //    {
        //        FilterDefinition<TMailSub> filter = Builders<TMailSub>.Filter.Eq(t => t.mId,mids[i]);
        //        delCount += MongoDBHelper.Instance.DeleteMany<TMailSub>(filter);
        //    }
        //    Logger.Sys($"删除{delCount}条邮件子项");
        //}
        
        //private Mail createMail(TMail tData)
        //{
        //    Mail mail = new Mail(tData);
        //    //已过期的邮件不处理
        //    if (mail.IsOverdue)
        //        return null;
        //    DictionarySafe<ObjectId, Mail> playerMail;
        //    if (mail.Type == MailType.Mass)
        //    {
        //        MassMailList.Add(mail.ID, mail);
        //        MassMailSubList.Add(mail.ID, new DictionarySafe<ObjectId, TMailSub>());
        //    }
        //    else
        //    {
        //        if (!PlayerMailList.TryGetValue(mail.PlayerId, out playerMail))
        //        {
        //            playerMail = new DictionarySafe<ObjectId, Mail>();
        //            PlayerMailList.Add(mail.PlayerId, playerMail);
        //        }
        //        playerMail.Add(mail.ID, mail);
        //    }
        //    return mail;
        //}

        /// <summary>
        /// 发送玩家邮件列表信息
        /// </summary>
        /// <param name="plyaer"></param>
        public void SendMailList(Player player)
        {
            SC_mail_list msg = new SC_mail_list();
            One_mail_info one;
            //群发邮件
            foreach (Mail mail in player.MassMailList.Values)
            {
                if (!mail.IsOverdue || !mail.IsDel())
                {
                    one = new One_mail_info();
                    one.SID = mail.SID;
                    one.Title = mail.Data.title;
                    one.IsOpen = mail.IsOpen();
                    one.SendTime = ((DateTime)mail.Data.sTime).ToTimestamp();
                    one.IsItems = !(mail.Data.items == null || mail.Data.items.Length == 0);
                    one.IsGet = mail.IsGet(player.ID);
                    msg.MailList.Add(one);
                }
            }
            //人个邮件
            foreach (Mail mail in player.PlayerMailList.Values)
            {
                if (!mail.IsOverdue)
                {
                    one = new One_mail_info();
                    one.SID = mail.SID;
                    one.Title = mail.Data.title;
                    one.IsOpen = mail.IsOpen();
                    one.SendTime = ((DateTime)mail.Data.sTime).ToTimestamp();
                    one.IsItems = !(mail.Data.items == null || mail.Data.items.Length == 0);
                    one.IsGet = mail.IsGet(player.ID);
                    msg.MailList.Add(one);
                }
            }
            player.Send(msg);
        }

        ///// <summary>
        ///// 获取邮件
        ///// </summary>
        ///// <param name="playerId">玩家ID</param>
        ///// <param name="SID">邮件SID</param>
        ///// <returns></returns>
        //public Mail GetMail(ObjectId playerId, int SID)
        //{
        //    ObjectId id = TMail.ToObjectId(SID);
        //    Mail mail;
        //    if (MassMailList.TryGetValue(id, out mail))
        //        return mail;

        //    DictionarySafe<ObjectId, Mail> playerMail;
        //    if (PlayerMailList.TryGetValue(playerId, out playerMail))
        //    {
        //        if (playerMail.TryGetValue(id, out mail))
        //            return mail;
        //    }
        //    return null;
        //}

        ///// <summary>
        ///// 打开邮件
        ///// </summary>
        ///// <param name="player"></param>
        ///// <param name="SID"></param>
        ///// <returns></returns>
        //public bool OpenMail(Player player, int[] sidList)
        //{
        //    Mail mail;
        //    int SID = 0;
        //    bool isOpenSucc = false;
        //    //Dictionary<int, int> items = new Dictionary<int, int>();
        //    for (int im = 0; im < sidList.Length; im++)
        //    {
        //        SID = sidList[im];
        //        mail = GetMail(player.ID, SID);
        //        if (mail != null)
        //        {
        //            if (mail.IsOpen(player.ID))
        //                continue;

        //            if (mail.Type == MailType.Person)
        //            {
        //                mail.Data.isOpen = true;
        //                mail.Data.Update();
        //            }
        //            else
        //            {
        //                TMailSub mailSub;
        //                if (!Glob.mailMgr.MassMailSubList[mail.ID].TryGetValue(player.ID, out mailSub))
        //                {
        //                    mailSub = new TMailSub(true);
        //                    mailSub.mId = mail.ID;
        //                    mailSub.pId = player.ID;
        //                    mailSub.Insert();
        //                    Glob.mailMgr.MassMailSubList[mail.ID].AddOrUpdate(player.ID, mailSub);
        //                }
        //            }
        //            isOpenSucc = true; 
        //            ////邮件附件 打开自动领取
        //            //if (mail.Data.items != null)
        //            //{
        //            //    int itemId = 0;
        //            //    int num = 0;
        //            //    for (int i = 0; i < mail.Data.items.Length; i++)
        //            //    {
        //            //        itemId = mail.Data.items[i];
        //            //        num = mail.Data.nums[i];
        //            //        if (!items.ContainsKey(itemId))
        //            //            items.Add(itemId, num);
        //            //        else
        //            //            items[itemId] += num;
        //            //    }
        //            //}
                    
        //        }
        //    }
        //    //List<int[]> itemInfos = new List<int[]>();
        //    //foreach (KeyValuePair<int, int> keyVal in items)
        //    //{
        //    //    itemInfos.Add(new int[] { keyVal.Key, keyVal.Value });
        //    //}            
        //    //Glob.itemMgr.PlayerAddNewItems(player, itemInfos, true);
        //    return isOpenSucc;
        //}

        ///// <summary>
        ///// 领取一个邮件附件
        ///// </summary>
        ///// <param name="player"></param>
        ///// <param name="SID"></param>
        ///// <returns></returns>
        //public bool GetMailAward(Player player, int SID)
        //{
        //    Mail mail = GetMail(player.ID, SID); 
        //    bool isOpenSucc = false;
        //    Dictionary<int, int> items = new Dictionary<int, int>();
            
        //    if (mail != null)
        //    {
        //        if (mail.Type == MailType.Person)
        //        {
        //            mail.Data.isGet = true;
        //            mail.Data.Update();
        //            isOpenSucc = true;
        //        }
        //        else
        //        {
        //            TMailSub mailSub;
        //            if (Glob.mailMgr.MassMailSubList[mail.ID].TryGetValue(player.ID, out mailSub))
        //            {
        //                mailSub.isGet = true;
        //                mailSub.Update();
        //                Glob.mailMgr.MassMailSubList[mail.ID].AddOrUpdate(player.ID, mailSub);
        //                isOpenSucc = true;
        //            }
        //        }
        //        //邮件附件
        //        if (mail.Data.items != null)
        //        {
        //            int itemId = 0;
        //            int num = 0;
        //            for (int i = 0; i < mail.Data.items.Length; i++)
        //            {
        //                itemId = mail.Data.items[i];
        //                num = mail.Data.nums[i];
        //                if (!items.ContainsKey(itemId))
        //                    items.Add(itemId, num);
        //                else
        //                    items[itemId] += num;
        //            }
        //        }
        //        List<int[]> itemInfos = new List<int[]>();
        //        foreach (KeyValuePair<int, int> keyVal in items)
        //        {
        //            itemInfos.Add(new int[] { keyVal.Key, keyVal.Value });
        //        }
        //        Glob.itemMgr.PlayerAddNewItems(player, itemInfos, true, PbCom.Enum_bag_itemsType.BiMail);
        //    }
        //    return isOpenSucc;
        //}

        ///// <summary>
        ///// 删除一个邮件
        ///// </summary>
        ///// <param name="player"></param>
        ///// <param name="SID"></param>
        ///// <returns></returns>
        //public bool DelMail(Player player, int SID)
        //{
        //    Mail mail = GetMail(player.ID, SID);
        //    bool isOpenSucc = false;

        //    if (mail != null)
        //    {
        //        if (mail.Type == MailType.Person)
        //        {
        //            DictionarySafe<ObjectId, Mail> playerMail;
        //            if (PlayerMailList.TryGetValue(player.ID, out playerMail))
        //            {
        //                if (playerMail.ContainsKey(mail.ID))
        //                {
        //                    PlayerMailList[player.ID].Remove(mail.ID);
        //                    mail.Data.Delete();
        //                    isOpenSucc = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            TMailSub mailSub;
        //            if (Glob.mailMgr.MassMailSubList[mail.ID].TryGetValue(player.ID, out mailSub))
        //            {
        //                mailSub.isDel = true;
        //                mailSub.Update();
        //                Glob.mailMgr.MassMailSubList[mail.ID].AddOrUpdate(player.ID, mailSub);
        //                isOpenSucc = true;
        //            }
        //        }

        //    }
        //    return isOpenSucc;
        //}

        /// <summary>
        /// 发送群体邮件
        /// </summary>
        /// <param name="title">邮件标题</param>
        /// <param name="cont">邮件内容</param>
        /// <param name="itemInfos">附件物品信息</param>
        /// <param name="itemInfos">是否为gm邮件</param>
        public void SendMassMail(string title,ObjectId mid, string cont, List<int[]> itemInfos=null, bool isGM=false)
        {
            TMail tMail=null;
            if (isGM)
                tMail = new TMail(mid);
            else
                tMail = new TMail(true);
            tMail.title = title;
            tMail.cont = cont;
            tMail.isGM = isGM;

            if (itemInfos != null)
            {
                int[] items = new int[itemInfos.Count];
                int[] nums = new int[itemInfos.Count];
                for (int i = 0; i < itemInfos.Count; i++)
                {
                    items[i] = itemInfos[i][0];
                    nums[i] = itemInfos[i][1];
                }
                tMail.items = items;
                tMail.nums = nums;
            }

            tMail.sTime = DateTime.Now;
            tMail.type = (int)MailType.Mass;
            //tMail.Insert();

            //通知在线玩家有新的邮件
            SC_mail_one msg = new SC_mail_one();
            msg.OneMail = new One_mail_info();
            msg.OneMail.SID = tMail.shortId;
            msg.OneMail.Title = title;
            msg.OneMail.IsOpen = false;
            msg.OneMail.SendTime = ((DateTime)tMail.sTime).ToTimestamp();
            msg.OneMail.IsItems = !(tMail.items == null || tMail.items.Length == 0);

            foreach (Player player in Glob.playerMgr.onlinePlayerList.Values)
            {
                player.createMail(tMail);
                player.Send(msg);
            }

        }

        /// <summary>
        /// 发送个人邮件
        /// </summary>
        /// <param name="playerId">接收人</param>
        /// <param name="title">邮件标题</param>
        /// <param name="cont">邮件文字内容</param>
        /// <param name="itemInfos">附件物品</param>
        /// <param name="isGM">是否为gm邮件</param>
        public void SendPersonMail(ObjectId playerId, ObjectId mid, string title, string cont, List<int[]> itemInfos=null, bool isGM = false)
        {
            TMail tMail = null;
            if (isGM)
                tMail = new TMail(mid);
            else
                tMail = new TMail(true);
            tMail.title = title;
            tMail.pId = playerId;
            tMail.cont = cont;
            tMail.isGM = isGM;
            if (itemInfos != null)
            {
                int[] items = new int[itemInfos.Count];
                int[] nums = new int[itemInfos.Count];
                for (int i = 0; i < itemInfos.Count; i++)
                {
                    items[i] = itemInfos[i][0];
                    nums[i] = itemInfos[i][1];
                }
                tMail.items = items;
                tMail.nums = nums;
            }

            tMail.sTime = DateTime.Now;
            tMail.isOpen = false;
            tMail.type = (int)MailType.Person;
            if(!isGM)
                tMail.Insert();
            SendPlayerMail(tMail);
        }


        /// <summary>
        /// 发送到客户端
        /// </summary>
        /// <param name="tMail"></param>
        private void SendPlayerMail(TMail tMail)
        {
            if (Glob.playerMgr.onlinePlayerList.TryGetValue(tMail.pId, out Player player))
            {
                player.createMail(tMail);
                //通知在线玩家有新的邮件
                SC_mail_one msg = new SC_mail_one();
                msg.OneMail = new One_mail_info();
                msg.OneMail.SID = tMail.shortId;
                msg.OneMail.Title = tMail.title;
                msg.OneMail.IsOpen = false;
                msg.OneMail.SendTime = ((DateTime)tMail.sTime).ToTimestamp();
                msg.OneMail.IsItems = !(tMail.items == null || tMail.items.Length == 0);
                player.Send(msg);
            }
        }
    }
}
