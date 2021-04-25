using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>系统设置</summary>
    public class SettingConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => NameMaxLen;
        /// <summary>
        /// 名字最大长度,一个汉字占二个长度 (玩家名，马名，公会名公用)
        /// </summary>
        public int NameMaxLen { get; set; }
        /// <summary>
        /// 清除1分钟CD花费点券数 没特殊指定统一使用这个
        /// </summary>
        public int ClearCDTicket { get; set; }
        /// <summary>
        /// 【充值】首充奖励
        /// </summary>
        public List<int[]> PayFirst { get; set; }
        /// <summary>
        /// 【体力】默认体力上限值（未满开始恢复，超过不计算恢复，购买可超出）
        /// </summary>
        public int MaxPower { get; set; }
        /// <summary>
        /// 【体力】单次观看广告恢复体力点数
        /// </summary>
        public int ADRePower { get; set; }
        /// <summary>
        /// 【体力】赛季令牌提高体力上限值
        /// </summary>
        public int MaxAddPower { get; set; }
        /// <summary>
        /// 【体力】恢复一点体力所需时间（秒）
        /// </summary>
        public int RePowerTime { get; set; }
        /// <summary>
        /// 【体力】单次购买恢复体力点数
        /// </summary>
        public int BuyRePower { get; set; }
        /// <summary>
        /// 【体力】单次购买体力所需钻石数量（起始数量，购买次数越多递增）
        /// </summary>
        public int BuyPowerCost { get; set; }
        /// <summary>
        /// 【广告】每日获取体力最大可观看广告次数
        /// </summary>
        public int MaxADTimes { get; set; }
        /// <summary>
        /// 【赛季】赛季持续时间（天）
        /// </summary>
        public int SeasonDays { get; set; }
        /// <summary>
        /// 【赛季令牌】击杀一个普通怪物增加令牌经验值
        /// </summary>
        public int SeasonExpOne { get; set; }
        /// <summary>
        /// 【赛季令牌】击杀一个Boss增加令牌经验值
        /// </summary>
        public int SeasonExpOneBoss { get; set; }
        /// <summary>
        /// 【赛季令牌】开启赛季令牌累计需看广告次数
        /// </summary>
        public int SeasonOpenADNum { get; set; }
        /// <summary>
        /// 【邮件】邮件过期天数(不删除数据)
        /// </summary>
        public int MailOverdueDay { get; set; }
        /// <summary>
        /// 【邮件】邮件过期删除天数(删除数据)
        /// </summary>
        public int MailOverdueDeleteDay { get; set; }
        /// <summary>
        /// 【背包】背包最大容量
        /// </summary>
        public int BagMax { get; set; }
        /// <summary>
        /// 【英雄】英雄进化最高层
        /// </summary>
        public int HeroMaxBreak { get; set; }
        /// <summary>
        /// 【英雄】英雄升级经验消耗基础值（当前等级 * 基础值）
        /// </summary>
        public int HeroAddLevel { get; set; }
        /// <summary>
        /// 【英雄】英雄升阶需要道具（实际数量）
        /// </summary>
        public int[] HeroBreakFactor { get; set; }
        /// <summary>
        /// 【装备】装备最高等级
        /// </summary>
        public int EquipMaxLevel { get; set; }
        /// <summary>
        /// 【装备】装备升阶消耗金币_数量
        /// </summary>
        public int[] EquipBreakCost { get; set; }
        /// <summary>
        /// 【装备】装备升级所需虚拟物品 id_数量(Cint(数量 * 当前等级 * 系数))
        /// </summary>
        public int[] EquipAddLevelCost { get; set; }
        /// <summary>
        /// 【装备】装备外刀升级所需道具 id_数量(数量 * 当前等级 * 系数)
        /// </summary>
        public int[] EquipAddLevelCostOut { get; set; }
        /// <summary>
        /// 【装备】装备内刀升级所需道具 id_数量(数量 * 当前等级 * 系数)
        /// </summary>
        public int[] EquipAddLevelCostIn { get; set; }
        /// <summary>
        /// 【装备】装备戒指升级所需道具 id_数量(数量 * 当前等级 * 系数)
        /// </summary>
        public int[] EquipAddLevelCostRi { get; set; }
        /// <summary>
        /// 【装备】装备防具升级所需道具 id_数量(Cint(数量 * 当前等级 * 系数))
        /// </summary>
        public int[] EquipAddLevelCostDe { get; set; }
        /// <summary>
        /// 【装备】装备宠物升级所需道具 id_数量(数量 * 当前等级 * 系数)
        /// </summary>
        public int[] EquipAddLevelCostPe { get; set; }
        /// <summary>
        /// 【装备】英雄升级所需道具 id_数量(数量 * 当前等级 * 系数)
        /// </summary>
        public int[] EquipAddLevelCostHe { get; set; }
        /// <summary>
        /// 【装备】英雄（装备）升级计算道具数量系数(实际消耗数量 = 配表数量 * 当前等级 * 系数)
        /// </summary>
        public double EquipAddLevelFactor { get; set; }
        /// <summary>
        /// 【装备】装备升阶最高等级
        /// </summary>
        public int EquipBreakMax { get; set; }
        /// <summary>
        /// 【装备】装备升阶需要道具系数（实际数量 = 系数 * 当前等级）
        /// </summary>
        public int EquipBreakFactor { get; set; }
        /// <summary>
        /// 【英雄】已有英雄转化成碎片数量
        /// </summary>
        public int HeroTransformNum { get; set; }
        /// <summary>
        /// 【装备】已有装备转化成碎片数量
        /// </summary>
        public int EquipTransformNum { get; set; }
        /// <summary>
        /// 【副本】每日副本可免费重生次数
        /// </summary>
        public int FbRebirthMax { get; set; }
        /// <summary>
        /// 【副本】副本扣除体力值（简单；困难）
        /// </summary>
        public int[] FbDePower { get; set; }
        /// <summary>
        /// 【副本】副本击杀1个小怪累计荣耀点数
        /// </summary>
        public int FbKillPoint { get; set; }
        /// <summary>
        /// 【副本】副本击杀1个boss累计荣耀点数
        /// </summary>
        public int FbKillBossPoint { get; set; }
        /// <summary>
        /// 【挂机系统】挂机系统开启章节（通关开启)
        /// </summary>
        public int HangOpenChapter { get; set; }
        /// <summary>
        /// 【商城】商城广告赠送道具（不包含礼包）系数（实际数量 = 系数 * 最大章节id * 配表数）
        /// </summary>
        public double ShopFactorByChapter { get; set; }
        /// <summary>
        /// 【每日任务】每日任务固定条数
        /// </summary>
        public int TaskDailyNum { get; set; }
        /// <summary>
        /// 【新手关卡】新手关卡完成奖励
        /// </summary>
        public List<int[]> GuideAward { get; set; }
    }
}
