using PbBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbPay;

namespace GameServer.Module
{
    public class VipMgr
    {
        /// <summary>
        /// 在线玩家每日重置月卡数据
        /// 不在线的上线时判断
        /// </summary>
        /// <param name="player"></param>
        public void ResetEverdayData(Player player)
        {
            if (player.payData != null)
            {
                if (player.payData.MC > 0)
                    player.payData.MC -= 1;              
                player.payData.isGetMC = false;
                if (player.payData.MC < 1)
                    player.payData.MCLv = 0;
                player.payData.Update();
            }          
        }
        /// <summary>
        /// 登录游戏时减去月卡天数
        /// </summary>
        /// <param name="player"></param>
        /// <param name="acrossDay">跨越天数</param>
        public void LoginSetPlayerMC(Player player, int acrossDay)
        {
            if (player.payData != null)
            {
                player.payData.MC = Math.Max(0, player.payData.MC - acrossDay);
                player.payData.isGetMC = false;
                if (player.payData.MC < 1)
                    player.payData.MCLv = 0;
                player.payData.Update();
            }
        }

        /// <summary>
        /// 发送VIP,月卡，充值信息
        /// </summary>
        /// <param name="player"></param>
        public void SendVipInfo(Player player)
        {
            SC_vip_info msg = new SC_vip_info();
            //msg.VipLevel = player.Data.vipLv;
            //msg.VipExp = player.Data.vipExp;
            if (player.payData != null)
            {
                //if (player.payData.vipGifts != null)
                //    msg.VipGifts.Add(player.payData.vipGifts);
                ////开服基金
                //msg.OpenFundBuy = player.payData.openFundBuy;
                //if (player.payData.openFundIds != null)
                //    msg.OpenFundIds.Add(player.payData.openFundIds);
                //月卡
                msg.MCLv = player.payData.MCLv;
                if (player.payData.MC > 0 && player.payData.MCLv == 0)//月卡已购买的玩家 新增的字段MCLv为0时处理
                {
                    msg.MCLv = 7;
                    if (player.payData.MC > 7)
                        msg.MCLv = 30;
                }
                msg.MCDay = player.payData.MC;
                msg.IsGetMC = player.payData.isGetMC;
                //首充奖励
                msg.FirstPayState = player.payData.firstPay;
                //每日充值礼包
                if (player.payData.giftInfos != null)
                {
                    int giftCount = player.payData.giftInfos.Count;
                    if (giftCount > 0)
                    {
                        //One_gift_info one;
                        //PayGiftConfig config;
                        int nowDay = DateTime.Now.Day;
                        foreach (int id in player.payData.giftInfos.Keys.ToArray())
                        {
//                             if (!Glob.config.dicPayGift.TryGetValue(id, out config)) //配置表中已不存在
//                                 player.payData.giftInfos.Remove(id);
//                             else
//                             {
//                                 int[] infos = player.payData.giftInfos[id];
//                                 if (!Utils.CheckInDate(infos[1], config.starTime, config.endTime) ||  //已经过期了
//                                     config.restType == 1 && infos[1] % 100 != nowDay)   //每日重置
//                                     player.payData.giftInfos.Remove(id);
//                                 else
//                                 {
//                                     one = new One_gift_info();
//                                     one.Id = id;
//                                     one.Num = infos[0];
//                                     msg.GiftList.Add(one);
//                                 }
//                             }
                        }
                        //删除了一些过期的，重新保存一下
                        if (giftCount != player.payData.giftInfos.Count)
                            player.payData.Update();
                    }
                }                    
            }
            //msg.VipTryLevel = player.Data.vipTryLv;
            //if (player.Data.vipTryTime != null)
            //    msg.VipTryEndTime = ((DateTime)player.Data.vipTryTime).ToTimestamp();
            player.Session.Send(msg);
        }


        /// <summary>
        /// 购买VIP礼包
        /// </summary>
        /// <param name="player"></param>
        /// <param name="config"></param>
        /// <returns>是否领取成功</returns>
//         public bool BuyVipGif(Player player, VipGiftConfig config)
//         {
//             //return false;
//             if (player.payData == null)
//             {
//                 player.payData = new TPay(player.ID);
//                 player.payData.vipGifts = new int[] { config.level };
//                 player.payData.Insert();
//                 Glob.itemMgr.PlayerAddNewItems(player, config.items, true);
//                 //player.DeductTicketNum(config.ticket);
//                 return true;
//             }
//             else
//             {
//                 if (player.payData.vipGifts == null)
//                 {
//                     player.payData.vipGifts = new int[] { config.level };
//                 }
//                 else if (player.payData.vipGifts.Contains(config.level))
//                 {
//                     return false;
//                 }
//                 else
//                 {
//                     List<int> ids = player.payData.vipGifts.ToList();
//                     ids.Add(config.level);
//                     player.payData.vipGifts = ids.ToArray();
//                 }
//                 player.payData.Update();
//                 Glob.itemMgr.PlayerAddNewItems(player, config.items, true);
//                 //player.DeductTicketNum(config.ticket);
//                 return true;
//             }
//         }

        /// <summary>
        /// 获取VIP权限
        /// </summary>
        /// <param name="player"></param>
        /// <param name="type"></param>
        /// <returns></returns>
//         public int GetVipRight(Player player, VipRightType type)
//         {
//             VipConfig config;
//             if (Glob.config.dicVip.TryGetValue(player.VipLevel, out config))
//                 return config.right[(int)type];
//             return 0;
//         }    

    }    
}
