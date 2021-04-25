using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>福利设置</summary>
    public class BonusSettingsConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => HangBaseGold;
        /// <summary>
        /// 【挂机奖励】挂机基础每分钟金币数（实际获得需累加天赋等级获得）
        /// </summary>
        public int HangBaseGold { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机最大获取金币数
        /// </summary>
        public int HangMaxGold { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机奖励所有物品id
        /// </summary>
        public int[] HangItems { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机奖励获取物品数量_权重
        /// </summary>
        public List<int[]> HangNumWeight { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机奖励最大累计时间（小时），超时不累计奖励
        /// </summary>
        public int HangMaxHours { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机基础每分钟钻石数
        /// </summary>
        public double HangBaseTicket { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机最大获取钻石数
        /// </summary>
        public int HangMaxTicket { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机奖励获取物品每分钟累计数量
        /// </summary>
        public double HangNumAdd { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机奖励间隔（分钟）时间累加
        /// </summary>
        public int HangAddTime { get; set; }
        /// <summary>
        /// 【挂机奖励】挂机奖励领取间隔时间（分钟）
        /// </summary>
        public int HangGetTime { get; set; }
        /// <summary>
        /// 【商城】免费抽取英雄，装备间隔时间（分钟）
        /// </summary>
        public int FreeShopGet { get; set; }
        /// <summary>
        /// 【商城】抽取一次英雄所需钻石数
        /// </summary>
        public int OneGetHeroCost { get; set; }
        /// <summary>
        /// 【商城】抽取一次高级装备所需钻石数
        /// </summary>
        public int OneGetEquipCost { get; set; }
        /// <summary>
        /// 【商城】五连抽取英雄所需钻石数
        /// </summary>
        public int FiveGetHeroCost { get; set; }
        /// <summary>
        /// 【商城】五连抽取高级装备所需钻石数
        /// </summary>
        public int FiveGetEquipCost { get; set; }
        /// <summary>
        /// 【商城】五连抽取英雄权重（只出一个，单抽五次也是这个权重取值）
        /// </summary>
        public List<int[]> GetHeroWeight { get; set; }
        /// <summary>
        /// 【商城】五连抽取装备权重（只出一个，单抽五次也是这个权重取值）
        /// </summary>
        public List<int[]> GetEquipWeight { get; set; }
        /// <summary>
        /// 【商城】抽取一次英雄获得概率（万分比）
        /// </summary>
        public int OneGetHeroPer { get; set; }
        /// <summary>
        /// 【商城】抽取一次高级装备获得概率（万分比）
        /// </summary>
        public int OneGetEquipPer { get; set; }
        /// <summary>
        /// 【商城】抽取一次英雄连续失败累计必送次数
        /// </summary>
        public int HeroSendTimes { get; set; }
        /// <summary>
        /// 【商城】抽取一次高级装备连续失败累计必送次数
        /// </summary>
        public int EquipSendTimes { get; set; }
        /// <summary>
        /// 【商城】抽取一次英雄失败补偿物品（id_数量_权值）
        /// </summary>
        public List<int[]> HeroFailSend { get; set; }
        /// <summary>
        /// 【商城】抽取一次装备失败补偿物品（id_数量_权值）
        /// </summary>
        public List<int[]> EquipFailSend { get; set; }
        /// <summary>
        /// 【三日奖励】激活三日奖励所需观看广告数
        /// </summary>
        public int ThreeAwardAdNum { get; set; }
        /// <summary>
        /// 【转盘】转盘累计宝箱重置时间（小时）
        /// </summary>
        public int CircleResetTime { get; set; }
        /// <summary>
        /// 【转盘】转盘免广告转动重置时间（小时）
        /// </summary>
        public int CircleFreeResetTime { get; set; }
    }
}
