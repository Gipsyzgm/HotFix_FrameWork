using GameServer.XGame.Module;
using PbPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
   /*
    public class PayPackMgr
    {

        /// <summary>
        /// GM设置数据表推送礼包
        /// </summary>
        public Dictionary<int, PayPackConfigDataBase> GMSetPack = new Dictionary<int, PayPackConfigDataBase>();

        /// <summary>
        /// GM设置数据表推送礼包组
        /// </summary>
        public Dictionary<int, List<PayPackConfigDataBase>> GroupPack = new Dictionary<int, List<PayPackConfigDataBase>>();

        /// <summary>
        /// 礼包批次ID
        /// </summary>
        public int PackMark = 0;

        /// <summary>
        /// 最多可添加多少礼包
        /// </summary>
        public int GroupNum = 4;

        /// <summary>    
        /// 存的在礼包类型 0英雄召唤 2装备召唤 2离线时长
        /// </summary>
        public bool[] isPackTriType = new bool[3];
        /// <summary>
        /// 首冲礼包组ID 不在礼包数量限制内
        /// </summary>
        public int OnePay = 1;
        public PayPackMgr()
        {

            InitLoadData();
        }
        
        /// <summary>
        /// 初始化加载推送礼包数据表
        /// </summary>
        public void InitLoadData()
        {
            List<TGMSetPayPack> packList = MongoDBHelper.Instance.Select<TGMSetPayPack>();
            GroupPack.Clear();
            GMSetPack.Clear();
            packList.Sort((a, b) =>a.packId.CompareTo(b.packId));
            isPackTriType[0] = false;
            isPackTriType[1] = false;
            isPackTriType[2] = false;
            foreach (TGMSetPayPack pack in packList)
            {
                if (pack.packMark != null)
                    PackMark = pack.packMark.ToTimestamp();
                else
                    PackMark = 0;
                PayPackConfigDataBase dataBase = new PayPackConfigDataBase(pack);
                GMSetPack.Add(pack.packId, dataBase);
                if (GroupPack.TryGetValue(pack.packGroup, out List<PayPackConfigDataBase> packVal))
                {
                    GroupPack[pack.packGroup].Add(dataBase);
                }
                else {
                    List<PayPackConfigDataBase> group = new List<PayPackConfigDataBase>();
                    group.Add(dataBase);
                    GroupPack.Add(pack.packGroup, group);
                }

                if (pack.triggerType == (int)EGiftType.BuyHero)
                    isPackTriType[0] = true;
                if (pack.triggerType == (int)EGiftType.BuyEquip)
                    isPackTriType[1] = true;
                if(pack.triggerType == (int)EGiftType.OffLine)
                    isPackTriType[2] = true;

            }                
        }




        /// <summary>
        /// 验证玩家 需要验证的礼包
        /// </summary>
        /// <param name="player"></param>
        public void ActionPlayerPack(Player player)
        {
            player.initActionPack();        //每天分钟检测
            CheckPackTime(player);          //判断玩家身上礼包是否过期
            if (!player.CheckPackGroup())
                return;
            List<PayPackConfigDataBase> addPack = new List<PayPackConfigDataBase>();
            List<PayPackConfigDataBase> checkCon = player.isAciontPack.OrderBy(t => t.Config.SortNum).ToList();
            foreach (PayPackConfigDataBase payPackCon in checkCon)
            {            
                if (isAcionPack(payPackCon, player))
                {
                    addPack.Add(payPackCon);
                }
            }
            if(addPack.Count>0)
                player.AddPayPack(addPack);
        }

        /// <summary>
        /// 通知客户端，礼包更新
        /// </summary>      
        public void SendCientPayPack(Player player)
        {
            player.sendPackId.Clear();
            int GNum = Glob.payPackMgr.GroupNum;
            Dictionary<int, PayPackData> tup = CheckPackTime(player);   // 发送之前判断礼包是否过期

            List<PayPackData> packList = tup.Values.OrderBy(t => t.Config.SortNum).ToList();    //排序

            

            if (packList.Where(t => t.Config.PackGroup == Glob.payPackMgr.OnePay).ToList().Count > 0)  //首充礼包不在限制内
                GNum += 1;
            int num = packList.Count > GNum ? GNum : packList.Count;  // 限制发送玩家身上只有4个 
            for (int i = 0; i < num; i++)
            {
                PayPackData eType3 = packList.ElementAt(i);
                if (!player.sendPackId.ContainsKey(eType3.Config.id))
                    player.sendPackId.Add(eType3.Config.id, eType3);
            }
           
            SC_player_payPackList msg = new SC_player_payPackList();
            foreach (PayPackData send in player.sendPackId.Values)
            {
                if (send.Config.IsGroup == 1)
                {
                    if (Glob.payPackMgr.GroupPack.TryGetValue(send.Config.PackGroup, out List<PayPackConfigDataBase> groupVal))  //如果玩家身上的被删除了 就读配置表的
                    {
                        foreach (PayPackConfigDataBase val in groupVal)
                        {
                            if (player.PayPackList.TryGetValue(val.DataBase.packId, out PayPackData pack))
                                if (!player.sendPackId.ContainsKey(val.Config.id))
                                    msg.Info.Add(pack.GetPackMsg());
                        }
                    }
                    else {
                        if (Glob.config.dicPayPackGroup.TryGetValue(send.Config.PackGroup, out List<PayPackConfig> groupConVal))
                        {
                            foreach (PayPackConfig val in groupConVal)
                            {
                                if (player.PayPackList.TryGetValue(val.id, out PayPackData packConfig))
                                {
                                    if (!player.sendPackId.ContainsKey(val.id))
                                        msg.Info.Add(packConfig.GetPackMsg());
                                }
                            }
                        }
                    }
                }               
                msg.Info.Add(send.GetPackMsg());
            }

            List<One_pack_info> infoOrder = msg.Info.OrderBy(t => t.PackId).ToList();
            msg.Info.Clear();
            msg.Info.Add(infoOrder);
            player.Send(msg);
        }

        /// <summary>
        /// 判断礼包时间是否到期
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private Dictionary<int, PayPackData> CheckPackTime(Player player)
        {
            Dictionary<int, PayPackData> order = new Dictionary<int, PayPackData>();
            foreach (PayPackData tPack in player.PayPackList.Values)
            {
                if (!tPack.Data.isShow || tPack.Data.payNum == 0)
                    continue;
                if (tPack.Config.GiftType != 4)
                {
                    if (DateTime.Now.ToTimestamp() >= tPack.Data.PackDate.ToTimestamp())
                    {
                        tPack.Data.isShow = false;
                        tPack.Data.Update();
                        continue;
                    }
                }
                if (!order.ContainsKey(tPack.Config.PackGroup))    //一组礼包只显示一个
                    order.Add(tPack.Config.PackGroup, tPack);
            }           
            return order;            
        }
        

        /// <summary>
        /// 根据类型 判断是否开启
        /// </summary>
        /// <param name="packCon"></param>
        /// <returns></returns>
        public bool isAcionPack(PayPackConfigDataBase packCon, Player e)
        {         
            if (packCon.Config.GiftType == 1)
            {
                if (Utils.CheckIsDate(packCon.DataBase.startTime.ToTimestamp()) && isTriGger(packCon.DataBase, e))
                    return true;
            }
            else if (packCon.Config.GiftType == 2)
            {
                if (Utils.CheckIsDate(packCon.DataBase.startTime.ToTimestamp()))
                    return true;
            }
            else if (packCon.Config.GiftType == 3 || packCon.Config.GiftType == 4)
            {
                if (isTriGger(packCon.DataBase, e))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 条件开启判断
        /// </summary>
        /// <param name="packCon"></param>
        /// <returns></returns>
        private bool isTriGger(TGMSetPayPack gmPack, Player e)
        {
            switch ((EGiftType)gmPack.triggerType)
            {
                case EGiftType.None:
                    return true;
                case EGiftType.PlayerLv:
                    if (e.Level >= gmPack.trigger[0])
                        return true;
                    return false;               
                case EGiftType.FBWar:

                    if (e.PayPackPlayerData.Data.pveNum.TryGetValue(gmPack.trigger[2], out int[] fbNum))
                    {
                        if (gmPack.trigger[1] != -1)
                        {
                            if (gmPack.trigger[1] < Extension.GetAcrossDay(e.AccountData.regDate, DateTime.Now))
                                return false;
                        }
                        if (gmPack.trigger[3] == 0)
                        {
                            if (gmPack.trigger[0] <= fbNum[1])
                                return true;
                        }
                        else if (gmPack.trigger[3] == 1)
                        {
                            if (gmPack.trigger[0] <= fbNum[0])
                                return true;
                        }
                        else
                        {
                            if (gmPack.trigger[0] <= (fbNum.Sum()))
                                return true;
                        }                            
                    }
                    return false;
                case EGiftType.AddHero:
                    if (gmPack.trigger[1] != -1)
                    {
                        if (e.PayPackPlayerData.heroElemStarNum[gmPack.trigger[2], gmPack.trigger[1]]>= gmPack.trigger[0])
                            return true;
                    }
                    else
                    {
                        return false;
                    }
                    if (gmPack.trigger[3] != -1)
                        if (e.PayPackPlayerData.heroIdNum.TryGetValue(gmPack.trigger[3], out int num))
                            if (num >= gmPack.trigger[0])
                                return true;
                            else
                                return false;
                    return false;
                case EGiftType.HeroType:
                    if (gmPack.trigger[1] != -1)
                    {
                        if (e.PayPackPlayerData.heroIdLv.TryGetValue(gmPack.trigger[1], out int[] hero))
                        {
                            if (gmPack.trigger[2] <= hero[0] && gmPack.trigger[3] <= hero[1])
                                return true;
                        }
                        else
                            return false;
                    }
                    else if (gmPack.trigger[0] != -1)
                    {
                        if (e.PayPackPlayerData.heroStarLv.TryGetValue(gmPack.trigger[0], out int[] hero))
                        {
                            if (gmPack.trigger[2] <= hero[0] && gmPack.trigger[3] <= hero[1])
                                return true;
                        }
                        else
                            return false;
                    }
                    return false;
                case EGiftType.PVPWar:
                    if (e.PayPackPlayerData.Data.pvpNum.TryGetValue(gmPack.trigger[0], out int[] pvpNum))
                    {
                        if (gmPack.trigger[2] == -1)
                        {
                            if (pvpNum.Sum() >= gmPack.trigger[1])
                                return true;
                        }
                        else
                        {
                            if (pvpNum[gmPack.trigger[2]] >= gmPack.trigger[1])
                                return true;
                        }
                    }
                    return false;                  
                case EGiftType.BuyTicket:
                    if (e.PayPackPlayerData.Data.buyTicket >= gmPack.trigger[0])
                        return true;
                    return false;
                case EGiftType.UseTicket:
                    if (e.PayPackPlayerData.Data.useTicket >= gmPack.trigger[0])
                        return true;
                    return false;
                case EGiftType.PlayerTicketNum:                    
                    if (e.Data.ticket <= gmPack.trigger[0])
                        return true;
                    return false;
                case EGiftType.PayTime:
                    if (e.PayPackPlayerData.Data.payTime == null)
                        return false;
                    if (gmPack.trigger[0] <= Extension.GetAcrossDay(e.PayPackPlayerData.Data.payTime, DateTime.Now))
                        return true;
                    return false;
                case EGiftType.OffLine:
                    if(e.PayPackPlayerData.Data.offlineTime!=null)
                        if (gmPack.trigger[0] <= Extension.GetAcrossDay(e.PayPackPlayerData.Data.offlineTime, DateTime.Now))
                            return true;
                    return false;
                //case EGiftType.AreneScore:
                //    if (gmPack.trigger[0] >= e.Arena.Score)
                //        return true;
                //    return false;
                case EGiftType.BuildLv:
                    //foreach (Build build in e.buildList.Values)
                    //    if (build.TemplId == gmPack.trigger[1] && build.Level >= gmPack.trigger[0])
                    //        return true;
                    return false;
                case EGiftType.OnLine:
                    if (gmPack.trigger[0] <= e.PayPackPlayerData.Data.onlineTime)
                        return true;
                    return false;
                case EGiftType.BuyHero:
                    if (gmPack.trigger[0] <= e.PayPackPlayerData.Data.buyHeroOrEquip[0])
                        return true;
                    return false;
                case EGiftType.BuyEquip:
                    if (gmPack.trigger[0] <= e.PayPackPlayerData.Data.buyHeroOrEquip[1])
                        return true;
                    return false;
                case EGiftType.MainBuildLv:
                    //if (e.StrongHold.Level >= gmPack.trigger[0])
                    //    return true;
                    //else
                        return false;
                case EGiftType.VipTime:
                    if (e.payData == null)
                        return false;
                    return gmPack.trigger[0] == 0 ? e.payData.MC <= 0 : e.payData.MC > 0;
                case EGiftType.IsVip:
                    return gmPack.trigger[0] == 0 ? e.payData != null : e.payData == null;
                //case EGiftType.JoinClub:
                //    return gmPack.trigger[0] == 0 ? e.Club != null : e.Club == null;
                case EGiftType.WarTime:
                    int[] fbId = e.PayPackPlayerData.Data.warTime;
                    if (Glob.config.dicFBLevelStage.TryGetValue(fbId[0], out FBLevelStageConfig con))
                        if (gmPack.trigger[0] <= con.levelId && con.levelId >= gmPack.trigger[1])
                            if (Extension.GetAcrossDay(fbId[1].ToDateTime(), DateTime.Now) > gmPack.trigger[2])
                                return true;
                    return false;
            }
            return false;
        }
            

        /// <summary>
        /// 创建玩家礼包验证数据
        /// </summary>
        /// <param name="p"></param>
        public void PlayerCreateInitPayPackData(Player p)
        {
            TPayPackData t = new TPayPackData(p.ID);
            t.pveNum = new Dictionary<int, int[]>();
            t.pvpNum = new Dictionary<int, int[]>();  
            t.warTime = new int[2];
            t.buyTicket = 0;
            t.useTicket = 0;
            t.isPackMark = 0;
            t.payTime = null;
            t.onlineTime = 0;
            t.offlineTime = null;
            t.buyHeroOrEquip = new int[2];
            t.Insert();
            p.PayPackPlayerData = new PayPackPlayerData(p, t);             
        }


       
    }*/
}
