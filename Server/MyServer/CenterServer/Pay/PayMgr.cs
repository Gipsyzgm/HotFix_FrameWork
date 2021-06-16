using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using CenterServer.Player;
using PbCenterPay;
using CommonLib;
using CommonLib.Comm.DBMgr;

namespace CenterServer.Pay
{
    public class PayMgr
    {

        //所有定单列表
        public DictionarySafe<ObjectId, TPayOrder> PayOrderList = new DictionarySafe<ObjectId, TPayOrder>();


        //已支付，但未发货的定单信息[key,玩家信息]
        public DictionarySafe<ObjectId, DictionarySafe<ObjectId, TPayOrder>> noSendOrderList = new DictionarySafe<ObjectId, DictionarySafe<ObjectId, TPayOrder>>();


        public PayMgr()
        {
            //删除过期未支付定单
            deleteOverdueOrder();
            DictionarySafe<ObjectId, TPayOrder> list = DBReader.Instance.SelectAll<TPayOrder>();
            foreach (TPayOrder tData in list.Values)
            {
                PayOrderList.Add(tData.id, tData);          
                if (!tData.isSend && tData.isPay)
                    AddNoSendOrder(tData);
            }
        }

        /// <summary>
        /// 增加到未发货定单列表
        /// </summary>
        /// <param name="order"></param>
        public void AddNoSendOrder(TPayOrder order)
        {
            DictionarySafe<ObjectId, TPayOrder> list;
            if (!noSendOrderList.TryGetValue(order.pid, out list))
            {
                list = new DictionarySafe<ObjectId, TPayOrder>();
                noSendOrderList.Add(order.pid, list);
            }
            list.Add(order.id, order);
        }
        /// <summary>
        /// 玩家登录，未发货的定单进行发货
        /// </summary>
        /// <param name="player"></param>
//         public void SendPayGoods(PlayerData player)
//         {
//             DictionarySafe<ObjectId, TPayOrder> list;
//             List<ObjectId> sucId = new List<ObjectId>();
//             if (noSendOrderList.TryGetValue(player.Id, out list))
//             {
//                 PayGoodsConfig config;
//                 foreach (TPayOrder order in list.Values)
//                 {
//                     if (Glob.config.dicPayGoods.TryGetValue(order.gId, out config))
//                     {
//                         if (PaySendGoods(player, order, config))
//                         {
//                             order.isSend = true;
//                             order.Update();
//                             sucId.Add(order.id);
//                         }
//                     }
//                 }
//             }
//             if (sucId.Count > 0)
//             {
//                 foreach (ObjectId id in sucId)
//                     list.Remove(id);
//                 Logger.LogWarning($"玩家[{player.Id}] 未发货定单已经补发,共{sucId.Count }条");
//             }
//         }

        /// <summary>
        ///  清理过期定单 (超过30天的直接删除)
        /// </summary>
        private void deleteOverdueOrder()
        {
            //eq相等   ne、neq不相等，   gt大于， lt小于 gte、ge大于等于   lte、le 小于等于   not非
            //删除过期定单
            FilterDefinition<TPayOrder> filter = Builders<TPayOrder>.Filter.Eq(t => t.isPay, false);
            filter = filter & Builders<TPayOrder>.Filter.Lt(t => t.orderData, DateTime.Now.AddDays(-30));
            long delCount = MongoDBHelper.Instance.DeleteMany<TPayOrder>(filter);
            Logger.Sys($"删除{delCount}条过期未支付定单");
        }

        /// <summary>
        /// 通知GS充值发货
        /// </summary>
        /// <param name="playerId">玩家Id</param>
        /// <param name="config">商品配置</param>
//         public bool PaySendGoods(PlayerData player, TPayOrder payOrder, PayGoodsConfig config)
//         {
//             SC_Center_paySucceed sendMsg = new SC_Center_paySucceed();
//             sendMsg.PlayerId = player.Id.ToString();
//             sendMsg.OrderId = payOrder.id.ToString();
//             sendMsg.GoodsId = config.id;
//             sendMsg.PFOrderId = payOrder.orderNo;
//             player.Send(sendMsg);
//             return true;
//         }

        /// <summary>
        /// 玩家下单
        /// </summary>
        /// <param name="player"></param>
        /// <param name="config">商品</param>
        /// <param name="platform">平台类型</param>
//         public void PayOrder(PlayerData player, PayGoodsConfig config, int platform)
//         {
//             //生成定单数据，收到充值回调再保存数据
//             TPayOrder data = new TPayOrder(true);
//             data.pid = player.Id;
//             data.platform = platform;
//             data.gId = config.id;
//             data.isPay = false;
//             data.orderData = DateTime.Now;
//             PayOrderList.AddOrUpdate(data.id, data);
//             data.Insert();
// 
//             //发送数据
//             SC_Center_payOrder sendMsg = new SC_Center_payOrder();
//             sendMsg.PlayerId = player.Id.ToString();
//             sendMsg.Result = PbCom.Enum_payOrder_result.OrSucceed;
//             sendMsg.GoodsID = config.id;
//             sendMsg.PlatformType = platform;
//             sendMsg.OrderId = data.id.ToString();
//             player.Send(sendMsg);
//         }
    }
}
