using PbBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using PbPay;
using MongoDB.Driver;
using PbCom;
using PbCenterPay;

namespace GameServer.Module
{
    public class PayMgr
    {
        public PayMgr()
        {
            
        }

        /// <summary>
        /// 玩家登录，未发货的定单进行发货(向中央服请求)
        /// </summary>
        /// <param name="player"></param>
        public void SendPayGoods(Player player)
        {
            CS_Center_payNoSend centerMsg = new CS_Center_payNoSend();
            centerMsg.PlayerId = player.ID.ToString();
            Glob.net.gameToCenterClient.SendMsg(centerMsg);
        }

        /// <summary>
        /// 充值成功
        /// </summary>
        /// <param name="player"></param>
//         public void PaySucceed(Player player, PayConfig config)
//         {
//             if (player.payData == null)
//             {
//                 player.payData = new TPay(player.ID);
//                 player.payData.firstPay = (int)EAwardState.Done;
//                 player.payData.Insert();
//             }
//             else
//             {
//                 if (player.payData.firstPay == (int)EAwardState.Undone)
//                 {
//                     player.payData.firstPay = (int)EAwardState.Done;
//                     player.payData.Update();
//                 }
//             }
//             //player.PayPackPlayerData.upBuyTicket(config.ticket);
//             //获得钻石
//             player.AddVirtualItemNum(EItemSubTypeVirtual.Ticket, config.ticket, true, PbCom.Enum_bag_itemsType.BiPay);
//             //获得奖励
//             Glob.itemMgr.PlayerAddNewItems(player, config.items, true, PbCom.Enum_bag_itemsType.BiPay);
// 
//         }
              
        /// <summary>
        /// 充值月卡成功
        /// </summary>
        /// <param name="player"></param>
//         public void PayMCSucceed(Player player, MonthCardConfig config)
//         {
//             if (player.payData == null)
//             {
//                 player.payData = new TPay(player.ID);
//                 player.payData.firstPay = (int)EAwardState.Done;
//                 player.payData.MC = config.day;
//                 if (player.payData.MCLv < config.day)
//                     player.payData.MCLv = config.day;
//                 player.payData.Insert();
//             }
//             else
//             {
//                 if(player.payData.firstPay== (int)EAwardState.Undone)
//                     player.payData.firstPay = (int)EAwardState.Done;
//                 player.payData.MC += config.day;
//                 if (player.payData.MCLv < config.day)
//                     player.payData.MCLv = config.day;
//                 player.payData.Update();
//             }
//         }


        /// <summary>
        /// 充值礼包
        /// </summary>
        /// <param name="player"></param>
//         public void PayGiftSucceed(Player player, PayPackConfig config)
//         {
//             if (player.payData == null)
//             {
//                 player.payData = new TPay(player.ID);
//                 player.payData.firstPay = (int)EAwardState.Done;
// 
//                 if (config.GetNum > 0) //有次数限制，记录次数信息
//                 {
//                     player.payData.giftInfos = new Dictionary<int, int[]>();
//                     player.payData.giftInfos.Add(config.id, new int[] { 1, DateTime.Now.ToDateNum() });
//                     player.payData.Insert();
//                 }
//             }
//             else
//             {
//                 if (config.GetNum > 0) //有次数限制，记录次数信息
//                 {                    
//                     if(player.payData.giftInfos==null)
//                         player.payData.giftInfos = new Dictionary<int, int[]>();
//                     int[] infos;
//                     if (!player.payData.giftInfos.TryGetValue(config.id, out infos))
//                     {
//                         infos = new int[] { 1, DateTime.Now.ToDateNum() };
//                         player.payData.giftInfos.Add(config.id, infos);
//                     }
//                     else
//                         infos[0] += 1;
//                     player.payData.Update();
//                 }
//                 if (player.payData.firstPay == (int)EAwardState.Undone)
//                 {
//                     player.payData.firstPay = (int)EAwardState.Done;
//                     player.payData.Update();
//                 }
//             }            
//             //发送奖励道具
//             Glob.itemMgr.PlayerAddNewItems(player, config.Items, true, PbCom.Enum_bag_itemsType.BiPay);
//             player.UpPlayerPayPack(config.id);
//         }

        /// <summary>
        /// 充值基金成功
        /// </summary>
        /// <param name="player"></param>
//         public void PayFundSucceed(Player player, FundConfig config)
//         {
//             if (player.payData == null)
//             {
//                 player.payData = new TPay(player.ID);
//                 player.payData.firstPay = (int)EAwardState.Done;
//                 player.payData.openFundBuy = true;
//                 player.payData.Insert();
//             }
//             else
//             {
//                 if (player.payData.firstPay == (int)EAwardState.Undone)
//                     player.payData.firstPay = (int)EAwardState.Done;
//                 player.payData.openFundBuy = true;
//                 player.payData.Update();
//             }
//             if (config.items.Count > 0)
//             {
//                 //获得奖励
//                 Glob.itemMgr.PlayerAddNewItems(player, config.items, true, PbCom.Enum_bag_itemsType.BiPay);
//             }
//         }

        ///// <summary>
        ///// 充值英勇卡成功
        ///// </summary>
        ///// <param name="player"></param>
        //public void PayHeroicSucceed(Player player, HeroicCardConfig config)
        //{
        //    if (player.payData == null)
        //    {
        //        player.payData = new TPay(player.ID);
        //        player.payData.firstPay = (int)EAwardState.Done;
        //        player.payData.Insert();
        //    }
        //    else
        //    {
        //        if (player.payData.firstPay == (int)EAwardState.Undone)
        //            player.payData.firstPay = (int)EAwardState.Done;
        //        player.payData.Update();

        //    }
        //    HeroicRoad heroicRoad = player.Heroic;
        //    if (heroicRoad != null)//激活英勇之路的英勇卡状态
        //    {
        //        heroicRoad.Data.isBuy = true;
        //        heroicRoad.Data.Update();
        //    }
        //    if (config.items.Count > 0)
        //    {
        //        //获得奖励
        //        Glob.itemMgr.PlayerAddNewItems(player, config.items, true, Enum_bag_itemsType.BiPay);
        //    }
        //    Glob.cylogMgr.LogHeroicRoad(player, 1);
        //}

        /// <summary>
        /// 充值发货
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <param name="config">商品配置</param>
//         public bool PaySendGoods(Player player, PayGoodsConfig config)
//         {
//             bool isSucce = false;
//             switch ((PayType)config.type)
//             {
//                 case PayType.Normal:
//                     isSucce = PayNormal(player, config.configId);
//                     break;
//                 case PayType.MonthCard:
//                     isSucce = PayMonthCard(player, config.configId);
//                     break;
//                 case PayType.Gift:
//                     isSucce = PayGift(player, config.configId);
//                     break;
//                 case PayType.Fund:
//                     isSucce = PayFund(player, config.configId);
//                     break;
//                 //case PayType.HeroicCard:
//                 //    isSucce = PayHeroicCard(player, config.configId);
//                 //    break;
//             }
//             if (isSucce)
//             {
//                 player.AccountData.payMoney += config.price;
//                 player.Data.payMoney += config.price;
//                 player.SaveData();
//                 player.PayPackPlayerData.upPayTime();
//             }
//             return isSucce;
//         }


        //请求充值
//         bool PayNormal(Player player, int id)
//         {
//             bool isSucce = true;
//             //发送数据
//             SC_pay_normal sendData = new SC_pay_normal();
//             PayConfig config;
//             if (Glob.config.dicPay.TryGetValue(id, out config))
//             {
//                 sendData.Result = Enum_pay_result.PrSucceed;
//                 //充值成功
//                 PaySucceed(player, config);
//                 sendData.ID = id;
//                 sendData.FirstPayState = player.payData.firstPay;
// 
//                 player.TriggerTask(ETaskType.Pay_Total, config.ticket);                          
//                 player.TriggerTask(ETaskType.Connect_Pay, config.ticket);
//             }
//             else
//             {
//                 sendData.Result = Enum_pay_result.PrUnknown;
//                 isSucce = false;
//             }
//             player.Session.Send(sendData);
//             return isSucce;
//         }

        //请求充值
//         bool PayMonthCard(Player player, int id)
//         {
//             bool isSucce = true;
//             //发送数据
//             SC_pay_monthCard sendData = new SC_pay_monthCard();
//             MonthCardConfig config;
//             if (Glob.config.dicMonthCard.TryGetValue(id, out config))
//             {
//                 sendData.Result = Enum_pay_result.PrSucceed;
//                 //充值成功
//                 PayMCSucceed(player, config);
//                 sendData.ID = id;
//                 sendData.MCDay = player.payData.MC;
//                 sendData.FirstPayState = player.payData.firstPay;
//                 sendData.IsGetMC = player.payData.isGetMC;
//             }
//             else
//             {
//                 sendData.Result = Enum_pay_result.PrUnknown;
//                 isSucce = false;
//             }
// 
//             player.Session.Send(sendData);
//             //Glob.logMgr.LogFun(player.ID, id == 1 ? FunctionUseType.BuyMemberM : FunctionUseType.BuyMemberY);
// 
//             return isSucce;
//         }

        //请求充值每日礼包
//         bool PayGift(Player player, int id)
//         {
//             bool isSucce = true;
//             //发送数据
//             SC_pay_gift sendData = new SC_pay_gift();
//             PayPackConfig config;
//             if (Glob.config.dicPayPack.TryGetValue(id, out config))
//             {
//                 sendData.Result = Enum_pay_result.PrSucceed;
//                 //充值成功发货
//                 PayGiftSucceed(player, config);
//                 sendData.ID = id;
//                 sendData.FirstPayState = player.payData.firstPay;
//             }
//             else
//             {
//                 sendData.Result = Enum_pay_result.PrUnknown;
//                 isSucce = false;
//             }
//             player.Session.Send(sendData);
//             return isSucce;
//         }

        //请求基金
//         bool PayFund(Player player, int id)
//         {
//             bool isSucce = true;
//             //发送数据
//             SC_pay_fund sendData = new SC_pay_fund();
//             FundConfig config;
//             if (Glob.config.dicFund.TryGetValue(id, out config))
//             {
//                 sendData.Result = Enum_pay_result.PrSucceed;
//                 //充值成功
//                 PayFundSucceed(player, config);
//                 sendData.FirstPayState = player.payData.firstPay;
//             }
//             else
//             {
//                 sendData.Result = Enum_pay_result.PrUnknown;
//                 isSucce = false;
//             }
// 
//             player.Session.Send(sendData);
// 
//             return isSucce;
//         }

        /// <summary>
        /// 畅游sdk支付下单
        /// </summary>
        /// <param name="player"></param>
        /// <param name="config">商品</param>
        /// <param name="msgData"></param>
//         public void PayCYOrder(Player player, PayGoodsConfig config, CS_pay_order msgData = null)
//         {
//             CS_Center_payOrder centerMsg = new CS_Center_payOrder();
//             centerMsg.PlayerId = player.ID.ToString();
//             centerMsg.GoodsID = config.id;
//             centerMsg.PlatformType = msgData.PlatformType;
//             centerMsg.PlatParams = msgData.PlatParams;
//             Glob.net.gameToCenterClient.Send(centerMsg);

            //SC_pay_order sendData = new SC_pay_order();
            ////生成定单数据，收到充值回调再保存数据
            //TPayOrder data = new TPayOrder(true);
            //data.pid = player.ID;
            //data.platform = player.AccountData.pfCh;
            //data.gId = config.id;
            //data.isPay = false;
            //data.orderData = DateTime.Now;

            //data.Insert();

            //string ext = player.AccountData.serverId + "_" + config.id + "_" + msgData.PlatformType;
            //sendData.GoodsID = config.id;
            //sendData.PlatformType = msgData.PlatformType;
            //sendData.Ext = ext;
            //sendData.OrderId = data.id.ToString();
            //sendData.Result = Enum_payOrder_result.OrSucceed;
            //player.Session.Send(sendData);
       // }

        //请求英勇卡
        //bool PayHeroicCard(Player player, int id)
        //{
        //    bool isSucce = true;
        //    //发送数据
        //    SC_pay_heroicCard sendData = new SC_pay_heroicCard();
        //    HeroicCardConfig config;
        //    if (Glob.config.dicHeroicCard.TryGetValue(id, out config))
        //    {
        //        sendData.Result = Enum_pay_result.PrSucceed;
        //        //充值成功
        //        Glob.payMgr.PayHeroicSucceed(player, config);
        //        sendData.FirstPayState = player.payData.firstPay;
        //    }
        //    else
        //    {
        //        sendData.Result = Enum_pay_result.PrUnknown;
        //        isSucce = false;
        //    }
        //    player.Session.Send(sendData);

        //    return isSucce;
        //}
    }
}
