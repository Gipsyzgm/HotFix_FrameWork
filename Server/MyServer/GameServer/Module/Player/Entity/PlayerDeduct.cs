using MongoDB.Bson;
using System.Collections.Generic;
using PbPlayer;
using PbBag;
using System;
using CommonLib;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家数据扣除
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// 扣除道具
        /// </summary>
        /// <param name="p_templId">模板Id</param>
        /// <param name="p_count">数量</param>
        /// <param name="type">消耗类型</param>
        public bool DeductItemPropNum(int p_templId, int p_count, ItemCostType type = ItemCostType.Normal)
        {
            if (p_count == 0)
                return true;

            int havecount = 0;
            ItemProp prop;
            if (propList.TryGetValue(p_templId, out prop))
                havecount = prop.Data.count;

            if (p_count < 0 || p_count > havecount)
            {
                Logger.LogWarning("扣除道具不足!");
                return false;
            }
            prop.Data.count -= p_count;

            if (prop.Data.count <= 0)  //用完了移除掉
            {
                prop.Data.Delete();
                propList.Remove(p_templId);
            }
            else//更新数量
                prop.Data.Update();

            //Glob.cylogMgr.LogItem(this, prop.Config, (int)type, p_count, prop.Data.count, -1);
            //TriggerTask(ETaskType.Item_Collect, 1);

            SC_bag_updateItemCount item = new SC_bag_updateItemCount();
            item.TemplID = prop.TemplID;
            item.Num = prop.Data.count;
            Send(item);
            return true;
        }
        /// <summary>
        /// 扣除金币
        /// </summary>
        public bool DeductGoldNum(int p_count)
        {
            if (p_count == 0)
                return true;

            if (p_count < 0 || p_count > Data.gold)
            {
                Logger.LogWarning("扣除金币不足!");
                return false;
            }
            Data.gold -= p_count;
            SaveData();
            SC_player_updateVirtual vir = new SC_player_updateVirtual();
            vir.VirtualType = (int)EItemSubTypeVirtual.Gold;
            vir.Value = Data.gold;
            Send(vir);
            return true;
        }

        /// <summary>
        /// 扣除金券
        /// </summary>
        /// <param name="p_count">金券数量</param>
        /// <param name="funType">功能类型</param>
        /// <returns></returns>
        public bool DeductTicketNum(int p_count, TicketCostType type)
        {
            if (p_count == 0)
                return true; 

            if (p_count < 0 || p_count > Data.ticket)
            {
                Logger.LogWarning("扣除钻石不足!");
                return false;
            }
            Data.ticket -= p_count;
            SaveData();
            //加入消费统计
            

            SC_player_updateVirtual vir = new SC_player_updateVirtual();
            vir.VirtualType = (int)EItemSubTypeVirtual.Ticket;
            vir.Value = Data.ticket;
            Send(vir);


            return true;
        }

        /// <summary>
        /// 扣除虚拟道具
        /// </summary>
        public bool DeductVirtualItemNum(EItemSubTypeVirtual virType,int p_count, bool isSend = true)
        {
            if (p_count == 0)
                return true;

            long value = 0;
            //判断虚拟物品是否足够
            switch (virType)
            {
                case EItemSubTypeVirtual.Gold://金币
                    if (p_count < 0 || p_count > Data.gold)
                    {
                        Logger.LogWarning("扣除金币不足!");
                        return false;
                    }
                    Data.gold -= p_count;
                    value = Data.gold;
                    break;
                case EItemSubTypeVirtual.Ticket://钻石)
                    if (p_count < 0 || p_count > Data.ticket)
                    {
                        Logger.LogWarning("扣除钻石不足!");
                        return false;
                    }
                    Data.ticket -= p_count;
                    value = Data.ticket;
                    //触发消费钻石任务
                    //TriggerTask(TaskTypeId.CostTicket, p_count);
                    //触发活动消费钻石
                    //TriggerActivityEvent(ActivityType.Consume, p_count);
                    break;
                case EItemSubTypeVirtual.Exp://玩家经验
                    if (p_count < 0 || p_count > Data.exp)
                    {
                        Logger.LogWarning("扣除经验不足!");
                        return false;
                    }
                    Data.exp -= p_count;
                    value = Data.exp;
                    break;
                case EItemSubTypeVirtual.Power://玩家体力
               
                    sendPointChange();
                    return true;
                //case EItemSubTypeVirtual.Food://食物
                //    if (p_count < 0 || p_count > Data.food)
                //    {
                //        Logger.LogWarning("扣除食物不足!");
                //        return false;
                //    }
                //    Data.food -= p_count;
                //    value = Data.food;
                //    break;
                //case EItemSubTypeVirtual.Stone://石头
                //    if (p_count < 0 || p_count > Data.stone)
                //    {
                //        Logger.LogWarning("扣除石头不足!");
                //        return false;
                //    }
                //    Data.stone -= p_count;
                //    value = Data.stone;
                //    break;
                //case EItemSubTypeVirtual.People://人口
                //    if (p_count < 0 || p_count > Data.people)
                //    {
                //        Logger.LogWarning("扣除人口不足!");
                //        return false;
                //    }
                //    Data.people -= p_count;
                //    value = Data.people;
                //    break;
                //case EItemSubTypeVirtual.ActionPoint://副本行动点数
                //    if (p_count < 0 || p_count > Data.actionPoint)
                //    {
                //        Logger.LogWarning("扣除行动点数不足!");
                //        return false;
                //    }
                //    if (Data.actionPoint >= ActionPointMax)
                //        Data.lastAddApTime = DateTime.Now;
                //    //else
                //    //    Data.lastAddApTime = ((DateTime)Data.lastAddApTime).AddSeconds(Glob.config.settingConfig.ActionPointRecoverTime);
                //    Data.actionPoint -= p_count;
                //    value = Data.actionPoint;
                //    sendPointChange();
                //    break;
                //case EItemSubTypeVirtual.ArenaPoint://竞技行动点数
                //    if (p_count < 0 || p_count > Data.arenaPoint)
                //    {
                //        Logger.LogWarning("扣除竞技行动点数不足!");
                //        return false;
                //    }
                //    if (Data.arenaPoint >= Glob.config.settingConfig.ArenaPointMax)
                //        Data.lastAddArenaPTime = DateTime.Now;
                //    //else
                //    //    Data.lastAddArenaPTime = ((DateTime)Data.lastAddArenaPTime).AddSeconds(Glob.config.settingConfig.ArenaPointRecoverTime);
                //    Data.arenaPoint -= p_count;
                //    value = Data.arenaPoint;
                //    sendPointChange();
                //    break;
                //case EItemSubTypeVirtual.BossPoint://公会Boss行动点
                //    if (p_count < 0 || p_count > Data.bossPoint)
                //    {
                //        Logger.LogWarning("扣除Boss行动点不足!");
                //        return false;
                //    }
                //    if (Data.bossPoint >= Glob.config.settingConfig.BossPointMax)
                //        Data.lastAddBpTime = DateTime.Now;
                //    //else
                //    //    Data.lastAddBpTime = ((DateTime)Data.lastAddArenaPTime).AddSeconds(Glob.config.settingConfig.BossPointRecoverTime);
                //    Data.bossPoint -= p_count;
                //    value = Data.bossPoint;
                //    sendPointChange();
                //    break;
                //case EItemSubTypeVirtual.ClubContri://俱乐部贡献
                //    if (p_count < 0 || p_count > Data.contri)
                //    {
                //        Logger.LogWarning("扣除俱乐部贡献不足!");
                //        return false;
                //    }
                //    Data.contri -= p_count;
                //    value = Data.contri;
                //    break;
                default:
                    return false;
            }
            SaveData();
            if (isSend)
            {
                SC_player_updateVirtual vir = new SC_player_updateVirtual();
                vir.VirtualType = (int)virType;
                vir.Value = value;
                Send(vir);
            }
            return true;
        }
    }
}
