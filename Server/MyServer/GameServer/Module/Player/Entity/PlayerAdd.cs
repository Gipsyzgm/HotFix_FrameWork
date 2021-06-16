using PbCom;
using PbPay;
using PbPlayer;
using System;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家增加物品，货币
    /// </summary>
    public partial class Player
    {

        /// <summary>
        /// 加虚拟物品
        /// </summary>
        /// <param name="virType">物品类型</param>
        /// <param name="p_count">物品数量</param>
        /// <param name="isSend">是否发送到客户端(默认发送)</param>
        /// <param name="noTips">是否不弹 获得道具Tips  默认false(要弹)</param>
        /// <param name="type">获得来源类型</param>
        public void AddVirtualItemNum(EItemSubTypeVirtual virType, int p_count, bool isSend = true, 
            Enum_bag_itemsType type = Enum_bag_itemsType.BiNone)
        {
            if (p_count <= 0)
                return;

            long value = 0;
            //判断虚拟物品是否足够
            switch (virType)
            {
                case EItemSubTypeVirtual.Gold://金币                    
                    Data.gold += p_count;
                    value = Data.gold;
                    break;
                case EItemSubTypeVirtual.Ticket://钻石            
                    Data.ticket += p_count;
                    value = Data.ticket;

                    break;
                case EItemSubTypeVirtual.Exp://玩家经验                 
                    AddPlayerExp(p_count);
                    return;
                case EItemSubTypeVirtual.VipExp://VIP经验             
                    AddVipExp(p_count);
                    return;
            
                case EItemSubTypeVirtual.Power://体力
                                  
                    sendPointChange();
                    return;
                case EItemSubTypeVirtual.Hornor://副本荣誉点
                 
                    break;
             

                default:
                    return;
            }
            SaveData();
            if (isSend)
            {
                SC_player_updateVirtual vir = new SC_player_updateVirtual();
                vir.VirtualType = (int)virType;
                vir.Value = value;
                Send(vir);
            }
       
        }

        /// <summary>
        /// 增加VIP经验
        /// </summary>
        /// <param name="val"></param>
        public void AddVipExp(int val)
        {
            if (val <= 0)
                return;
            Data.vipExp += val;
            int vipLv = Data.vipLv;

            SaveData();

            SC_vip_exp msg = new SC_vip_exp();
            msg.VipLevel = Data.vipLv;
            msg.VipExp = Data.vipExp;
            Session.Send(msg);

        }
        /// <summary>
        /// 增加队伍经验
        /// </summary>
        /// <param name="val"></param>
        public void AddPlayerExp(int val)
        {
            if (val <= 0)
                return;
            int leftExp = val;
        
            int beforeLv = Data.level;
            int lv = Data.level;
            int exp = Data.exp;

        
            bool isChange = false;
            int slv = 0;
            if (Data.level != lv || Data.exp != exp)  //等级发生变化
            {
                if (Data.level != lv)
                {
                    isChange = true;
                    slv = Data.level;
                    Data.level = lv;
              
                
                    //等级奖励
                    Glob.bonusMgr.GetLevelAward(this);

                }
                Data.exp = exp;
                SaveData();
           

                SC_player_exp msg = new SC_player_exp();
                msg.Level = Data.level;
                msg.Exp = Data.exp;
                msg.AddExp = val;
                Send(msg);

                if (isChange)
                {
                    //升级体力补满
                    //ComplementActionPoint();
                    //增加天赋点数
                    AddDowerPoint(lv,slv);
                }
            }

        }


    }
}
