using CommonLib;
using MongoDB.Bson;
using System.Collections.Generic;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家物品信息数据
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// 判断道具数量是否足够
        /// </summary>
        /// <param name="p_templId">模板Id</param>
        /// <param name="p_count">数量</param>
        public bool CheckItemPropNum(int p_templId, int p_count, bool isLog=true)
        {
            int havecount = 0;
            ItemProp prop;
            if (propList.TryGetValue(p_templId, out prop))
                havecount =  prop.Data.count;

            if (p_count > havecount)
            {
                if(isLog)
                    Logger.LogWarning("所需道具不足!");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断金币是否足够
        /// </summary>
        public bool CheckGoldNum(int p_count)
        {
            if (p_count<0||p_count > Data.gold)
            {
                Logger.LogWarning("所需金币不足!");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断钻石(点券)是否足够
        /// </summary>
        public bool CheckTicketNum(int p_count)
        {
            if (p_count < 0||p_count > Data.ticket)
            {
                Logger.LogWarning("所需钻石不足!");
                return false;
            }
            return true;
        }     

        /// <summary>
        /// 判断最高等级限制 (人物等级不够)
        /// 注:升到下级判断要使用下级等级
        /// </summary>
        /// <param name="p_level">等级</param>
        public bool CheckMaxLevel(int p_level)
        {
            if (p_level > Level)
            {
                Logger.LogWarning("玩家等级不够!");
                return false;
            }
            return true;
        }     

        /// <summary>
        /// 判断最VIP 等级限制
        /// </summary>
        /// <param name="p_level">等级</param>
        public bool CheckVipLevel(int p_level)
        {
            if (p_level > VipLevel)
            {
                Logger.LogWarning("VIP等级不够!");
                return false;
            }
            return true;
        }
               

        /// <summary>
        /// 判断虚拟物品数量是否足够
        /// </summary>
        /// <param name="virType">虚拟物品类型</param>
        /// <param name="count">所需数量</param>
        /// <param name="hintTxt">提示文字 null不提示,""默认提示,"自定义提示" </param>
        /// <returns></returns>
        public bool CheckVirtualItemNum(EItemSubTypeVirtual virType, int count)
        {
            if (count == 0)
                return true;
            long haveCount = 0;
            string defTxt = string.Empty;
            //判断虚拟物品是否足够
            switch (virType)
            {
                case EItemSubTypeVirtual.Gold://金币
                    haveCount = Data.gold;
                    defTxt = "所需金币不足!";
                    break;
                case EItemSubTypeVirtual.Ticket://钻石(点券)
                    haveCount = Data.ticket;
                    defTxt = "所需钻石不足!";
                    break;
                case EItemSubTypeVirtual.Exp://玩家经验
                    haveCount = Data.exp;
                    defTxt = "所需经验不足!";
                    break;
                case EItemSubTypeVirtual.Power://玩家体力
                    defTxt = "所需体力不足!";
                    break;
                    //case EItemSubTypeVirtual.Food://食物
                    //    haveCount = Data.food;
                    //    defTxt = "所需食物不足!";
                    //    break;
                    //case EItemSubTypeVirtual.Stone://石头
                    //    haveCount = Data.stone;
                    //    defTxt = "所需石头不足!";
                    //    break;
                    //case EItemSubTypeVirtual.People://人口       
                    //    haveCount = Data.people;
                    //    defTxt = "所需人口不足!";
                    //    break;
                    //case EItemSubTypeVirtual.ActionPoint://副本行动点数
                    //    haveCount = Data.actionPoint;
                    //    defTxt = "所需副本行动点数不足!";
                    //    break;
                    //case EItemSubTypeVirtual.ArenaPoint://竞技行动点数
                    //    haveCount = Data.arenaPoint;
                    //    defTxt = "所需竞技行动点数不足!";
                    //    break;
                    //case EItemSubTypeVirtual.BossPoint://公会Boss行动点
                    //    haveCount = Data.bossPoint;
                    //    defTxt = "所需Boss行动点不足!";
                    //    break;
                    //case EItemSubTypeVirtual.ClubContri://俱乐部贡献
                    //    haveCount = Data.contri;
                    //    defTxt = "所俱乐部贡献不足!";
                    //    break;
            }
            if (count<0||count > haveCount)
            {
                //Logger.LogWarning(defTxt);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取VIP权限次数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
//         public int VipRight(VipRightType type)
//         {
//             VipConfig config;
//             if (Glob.config.dicVip.TryGetValue(VipLevel, out config))
//                 return config.right[(int)type];
//             return 0;
//         }

        /// <summary>
        /// 判断玩家新增的资源是否超过上限
        /// </summary>
        /// <param name="resType">资源类型</param>
        /// <param name="getNum">新增的数量</param>
        /// <param name="canGetNum">可增加的数量()</param>
        /// <returns></returns>
        public bool CheckResLimit(EItemSubTypeVirtual resType, int getNum, out int canGetNum)
        {
            canGetNum = 0;
            bool b = false;
            //if (resType == EItemSubTypeVirtual.Food && (Data.food + getNum >= FoodMax))
            //{
            //    canGetNum = FoodMax - Data.food;
            //    b = true;
            //}
            //if(resType == EItemSubTypeVirtual.Stone && (Data.stone + getNum >= StoneMax))
            //{
            //    canGetNum = StoneMax - Data.stone;
            //    b = true;
            //}
            //if(resType == EItemSubTypeVirtual.People && (Data.people + getNum >= PeopleMax))
            //{
            //    canGetNum = PeopleMax - Data.people;
            //    b = true;
            //}
            return b;
        }
        
    }
}
