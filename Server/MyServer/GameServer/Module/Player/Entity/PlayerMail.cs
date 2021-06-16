using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public partial class Player
    {
        /// <summary>
        /// 玩家个人邮件列表[邮件ID,邮件]
        /// </summary>
        public DictionarySafe<ObjectId, Mail> PlayerMailList = new DictionarySafe<ObjectId, Mail>();

        /// <summary>
        /// 群发邮件列表[邮件ID,邮件]
        /// </summary>
        public DictionarySafe<ObjectId, Mail> MassMailList = new DictionarySafe<ObjectId, Mail>();

        /// <summary>
        /// 玩家群发邮件子项列表[邮件ID,邮件子项数据]
        /// </summary>
        public DictionarySafe<ObjectId, TMailSub> MassMailSubList = new DictionarySafe<ObjectId, TMailSub>();

        /// <summary>
        /// 设置玩家邮件列表
        /// </summary>
        /// <param name="mailList">数据库邮件列表</param>
        /// <param name="mailSubs">数据库群邮件子项列表</param>
        public void SetPlayerMail(List<TMail> mailList, List<TMailSub> mailSubs)
        {
            foreach (TMail tData in mailList)
            {
                createMail(tData);
            }
            List<ObjectId> delMailSubIds = new List<ObjectId>();
            foreach (TMailSub tData in mailSubs)
            {
                if (PlayerMailList.ContainsKey(tData.mId) || MassMailList.ContainsKey(tData.mId))
                {
                    MassMailSubList.AddOrUpdate(tData.mId, tData);
                } else
                {
                    delMailSubIds.Add(tData.mId);
                }
            }
            delMailSubIds = delMailSubIds.Distinct().ToList();
            //删除邮件子项
            deleteMailSub(delMailSubIds);
        }

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="tData"></param>
        /// <returns></returns>
        public Mail createMail(TMail tData)
        {
            Mail mail = new Mail(this, tData);
            //已过期的邮件不处理
            if (mail.IsOverdue)
                return null;
            if (mail.Type == MailType.Mass)
            {
                MassMailList.Add(mail.ID, mail);
            }
            else
            {
                PlayerMailList.Add(mail.ID, mail);
            }
            return mail;
        }

        /// <summary>
        /// 删除不存在的群发邮件子项
        /// </summary>
        /// <param name="mids"></param>
        public void deleteMailSub(List<ObjectId> mids)
        {
            FilterDefinition<TMailSub> filter = Builders<TMailSub>.Filter.In(t => t.mId, mids);
            long delCount = MongoDBHelper.Instance.DeleteMany<TMailSub>(filter);
            //Logger.Sys($"删除{delCount}条邮件子项");
        }

        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="SID">邮件SID</param>
        /// <returns></returns>
        public Mail GetMail(int SID)
        {
            ObjectId id = TMail.ToObjectId(SID);
            Mail mail;
            if (MassMailList.TryGetValue(id, out mail))
                return mail;

            if (PlayerMailList.TryGetValue(id, out mail))
                return mail;
            return null;
        }

        /// <summary>
        /// 打开邮件
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public bool OpenMail(int[] sidList)
        {
            Mail mail;
            int SID = 0;
            bool isOpenSucc = false;
            //Dictionary<int, int> items = new Dictionary<int, int>();
            for (int im = 0; im < sidList.Length; im++)
            {
                SID = sidList[im];
                mail = GetMail(SID);
                if (mail != null)
                {
                    if (mail.IsOpen())
                        continue;

                    if (mail.Type == MailType.Person)
                    {
                        mail.Data.isOpen = true;
                        mail.Data.Update();
                    }
                    else
                    {
                        TMailSub mailSub;
                        if (!MassMailSubList.TryGetValue(mail.ID, out mailSub))
                        {
                            mailSub = new TMailSub(true);
                            mailSub.mId = mail.ID;
                            mailSub.pId = ID;
                            mailSub.Insert();
                            MassMailSubList.AddOrUpdate(mail.ID, mailSub);
                        }
                    }
                    isOpenSucc = true;
                    ////邮件附件 打开自动领取
                    //if (mail.Data.items != null)
                    //{
                    //    int itemId = 0;
                    //    int num = 0;
                    //    for (int i = 0; i < mail.Data.items.Length; i++)
                    //    {
                    //        itemId = mail.Data.items[i];
                    //        num = mail.Data.nums[i];
                    //        if (!items.ContainsKey(itemId))
                    //            items.Add(itemId, num);
                    //        else
                    //            items[itemId] += num;
                    //    }
                    //}

                }
            }
            //List<int[]> itemInfos = new List<int[]>();
            //foreach (KeyValuePair<int, int> keyVal in items)
            //{
            //    itemInfos.Add(new int[] { keyVal.Key, keyVal.Value });
            //}            
            //Glob.itemMgr.PlayerAddNewItems(player, itemInfos, true);
            return isOpenSucc;
        }

        /// <summary>
        /// 领取一个邮件附件
        /// </summary>
        /// <param name="player"></param>
        /// <param name="SID"></param>
        /// <returns></returns>
        public bool GetMailAward(int SID)
        {
            Mail mail = GetMail(SID);
            bool isOpenSucc = false;
            Dictionary<int, int> items = new Dictionary<int, int>();
            if (mail != null)
            {
                if (mail.IsGet(ID))
                    return false;
                //邮件附件  超过限制不可领取
                if (mail.Data.items != null)
                {
                    int itemId = 0;
                    int num = 0;
                    for (int i = 0; i < mail.Data.items.Length; i++)
                    {
                        itemId = mail.Data.items[i];
                        num = mail.Data.nums[i];
                        if (!items.ContainsKey(itemId))
                            items.Add(itemId, num);
                        else
                            items[itemId] += num;                                               
                    }
                }               
                List<int[]> itemInfos = new List<int[]>();
                foreach (KeyValuePair<int, int> keyVal in items)
                {                    
//                     if (Glob.config.dicHero.TryGetValue(keyVal.Key, out HeroConfig _config))
//                     {
//                         if (keyVal.Value > this.HeroLimit)
//                             return false;
//                     }
                    itemInfos.Add(new int[] { keyVal.Key, keyVal.Value });
                }

                if (mail.Type == MailType.Person)
                {
                    mail.Data.isGet = true;
                    mail.Data.Update();
                    isOpenSucc = true;
                }
                else
                {
                    if (MassMailSubList.TryGetValue(mail.ID, out TMailSub mailSub))
                    {
                        mailSub.isGet = true;
                        mailSub.Update();
                        MassMailSubList.AddOrUpdate(mail.ID, mailSub);
                        isOpenSucc = true;
                    }
                }

                Glob.itemMgr.PlayerAddNewItems(this, itemInfos, true, PbCom.Enum_bag_itemsType.BiMail);
            }
            return isOpenSucc;
        }

        /// <summary>
        /// 删除一个邮件
        /// </summary>
        /// <param name="player"></param>
        /// <param name="SID"></param>
        /// <returns></returns>
        public bool DelMail(int SID)
        {
            Mail mail = GetMail(SID);
            bool isOpenSucc = false;

            if (mail != null)
            {
                if (mail.Type == MailType.Person)
                {
                    if (PlayerMailList.ContainsKey(mail.ID))
                    {
                        PlayerMailList.Remove(mail.ID);
                        mail.Data.Delete();
                        isOpenSucc = true;
                    }
                }
                else
                {
                    if (MassMailSubList.TryGetValue(mail.ID, out TMailSub mailSub))
                    {
                        mailSub.isDel = true;
                        mailSub.Update();
                        MassMailSubList.AddOrUpdate(mail.ID, mailSub);
                        isOpenSucc = true;
                    }
                }

            }
            return isOpenSucc;
        }
    }
}
