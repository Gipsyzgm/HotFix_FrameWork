using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public enum EGiftType
    {
        /// <summary>
        /// 无条件
        /// </summary>
        None = 0,       
        /// <summary>玩家等级</summary>
        PlayerLv = 1,
        /// <summary>副本战斗</summary>
        FBWar,
        /// <summary>
        /// 获得英雄
        /// </summary>
        AddHero,
        /// <summary>
        /// 英雄属性
        /// </summary>
        HeroType,
        /// <summary>
        /// PVP战斗
        /// </summary>
        PVPWar,
        /// <summary>
        /// 充值钻石
        /// </summary>
        BuyTicket,
        /// <summary>
        /// 消费钻石
        /// </summary>
        UseTicket,
        /// <summary>
        /// 玩家钻石数量
        /// </summary>
        PlayerTicketNum,
        /// <summary>
        /// 玩家建筑时长
        /// </summary>
        BuildTimeNum,
        /// <summary>
        /// 支付间隔时长
        /// </summary>
        PayTime,
        /// <summary>
        /// 离线时长
        /// </summary>
        OffLine,
        /// <summary>
        /// 竞技场积分
        /// </summary>
        AreneScore,
        /// <summary>
        /// 建筑等级
        /// </summary>
        BuildLv,
        /// <summary>
        /// 在线时长
        /// </summary>
        OnLine,
        /// <summary>
        /// 召唤英雄
        /// </summary>        
        BuyHero,
       
        /// <summary>
        /// 召唤装备
        /// </summary>
        BuyEquip,
        /// <summary>
        /// 主城等级
        /// </summary>
        MainBuildLv,
        /// <summary>
        /// VIP是否过期
        /// </summary>
        VipTime,
        /// <summary>
        /// 是否为VIP
        /// </summary>
        IsVip,
        /// <summary>
        /// 是否加入联盟
        /// </summary>
        JoinClub,
        /// <summary>
        /// 战斗暂停天数
        /// </summary>
        WarTime

    }
}
