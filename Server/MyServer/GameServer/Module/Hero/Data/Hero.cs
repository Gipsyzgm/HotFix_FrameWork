using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using PbCom;
using PbHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家英雄对象
    /// </summary>
    public partial class Hero : BaseEntity<THero>
    {
        /// <summary>所属玩家</summary>
        public Player player { get; private set; }
      
        /// <summary>等级</summary>
        public int Level => Data.level;

//         /// <summary>英雄配置</summary>
//         public HeroConfig Config;
// 
//         /// <summary>英雄等级经验配置</summary>
//         public HeroExpConfig ExpConfig;
// 
//         /// <summary>英雄星级配置</summary>
//         public HeroStarConfig StarConfig;
// 
//         /// <summary>英雄突破配置</summary>
//         public HeroBreakConfig BreakConfig;

        /// <summary>当前可升到的最大等级</summary>
       // public int MaxLevel => StarConfig.heroLvMax[Data.breakLv - 1];

        /// <summary>
        /// 属性天赋所带属性值
        /// </summary>
        //public Dictionary<EDowerType, int> DowerAttributes = new Dictionary<EDowerType, int>();


        //获取天赋类型总值
//         public int GetDowerAttribValue(EDowerType type)
//         {
//             if (DowerAttributes.TryGetValue(type, out var value))
//                 return value;
//             return 0;
//         }

        /// <summary>
        /// 防御
        /// </summary>
//         public int Defend
//         {
//             get
//             {
                //最终值=基础值+每级加成+每阶加成+天赋加成
                //                 int defend =  Config.attrs[1] + (int)(SumLevel * (Config.attrsAdd[1] / 100.0)) + 
                //                     (int)((Data.breakLv - 1) * (Config.attrsAdv[1] / 100.0));
                //return defend + GetDowerAttribValue(EDowerType.Defense) + 
                //    (int)(GetDowerAttribValue(EDowerType.DefensePct) / 10000f * defend);
//                 return 0;
//             }
//         }

        /// <summary>
        /// 攻击
        /// </summary>
//         public int Attack
//         {
//             get
//             {
                //最终值=基础值+每级加成+每阶加成+天赋加成
                //int attack =  Config.attrs[0] + (int)(SumLevel * (Config.attrsAdd[0] / 100.0)) + 
                //    (int)((Data.breakLv - 1) * (Config.attrsAdv[0] / 100.0));
                //return attack + GetDowerAttribValue(EDowerType.Attack) + 
                //    (int)(GetDowerAttribValue(EDowerType.AttackPct) / 10000f * attack);
//                 return 0;
//             }
//         }

        /// <summary>
        /// 生命
        /// </summary>
//         public int HP
//         {
//             get
//             {
//                 //最终值=基础值+每级加成+每阶加成+天赋加成
                //int hp = Config.attrs[2] + (int)(SumLevel * (Config.attrsAdd[2] / 100.0)) + 
                //    (int)((Data.breakLv - 1) * (Config.attrsAdv[2] / 100.0));
                //return hp + GetDowerAttribValue(EDowerType.HP) + (int)(GetDowerAttribValue(EDowerType.HPPct) / 10000f * hp);
//                 return 0;
//             }
//         }

        /// <summary>
        /// 英雄战力
        /// </summary>
//         public int Power
//         {
//             get
//             {
                //战斗力=（攻击力*3+防御力*2+生命）/6+技能等级*10
                //int val = (Attack * 3 + Defend * 2 + HP) / 6 + (Data.skillLvArr[0] * 10);

                //新的  用这个
                //战斗力=攻击力/3+防御力/3+生命/8+技能等级*5+星级战力
                //星级战力: 1星 = 0,2星 = 8,3星 = 26,4星 = 46,5星 = 86
                //                 int val = Attack / 3 + Defend / 3 + HP / 8 + Data.skillLvArr[0] * 5 +
                //                     Glob.config.heroSettingConfig.HeroStarPower[Config.star - 1];
                //                 return val;
//                 return 0;
//             }
//         }

        /// <summary>是否创建成功</summary>
        public bool IsSucceed = false;
        public Hero(Player p, THero data)
        {
            player = p;
            Data = data;
//             if (!Glob.config.dicHero.TryGetValue(data.templId, out Config))
//             {
//                 Logger.LogError($"{player.ID}的英雄创建失败,找不到模板配置{data.templId}");
//                 return;
//             }
//             //升级id = 星级 * 10000 + 阶数 * 1000 + 等级
//             int levelId = Config.star * 10000 + Data.breakLv * 1000 + Level;
//             if (!Glob.config.dicHeroExp.TryGetValue(levelId, out ExpConfig))
//             {
//                 Logger.LogError($"{player.ID}的英雄创建失败,找不到等级经验配置{data.level}");
//                 return;
//             }
//             if(!Glob.config.dicHeroStar.TryGetValue(Config.star, out StarConfig))
//             {
//                 Logger.LogError($"{player.ID}的英雄创建失败,找不到星级配置{Config.star}");
//                 return;
//             }
//             int breakId = (Config.elemType + 1) * 1000 + MaxLevel;
//             if (!Glob.config.dicHeroBreak.TryGetValue(breakId, out BreakConfig))
//             {
//                 if(MaxLevel != StarConfig.heroLvMax.Max())
//                     Logger.LogError($"{player.ID}英雄升阶,找不到升阶配置 {breakId}");
//             }
            //if (Data.dowers != null)
            //{
            //    for (int i = 0; i < Data.dowers.Count; i++)
            //        DowerLevelUp(Data.dowers[i]);
            //}
            IsSucceed = true;
        }

        /// <summary>总等级</summary>
        //public int SumLevel
        //{
        //    get
        //    {
//                //总等级 = （当前等级 + 所有已升阶数的最大等级之和 - 1*阶数）
//                int sum = 0;
////                 for (int i = 1; i < Data.breakLv; i++)
////                 {
////                     sum += StarConfig.heroLvMax[i - 1];
////                 }
//                return Level + sum - Data.breakLv * 1;
//            }
//        }

        /// <summary>
        /// 英雄被吞噬时经验总值
        /// <param name="opHero">当前升级的英雄</param>
        /// </summary>
//        public int SumExp(Hero opHero)
//        {
//            //英雄提供的经验值 = 英雄基础经验 * ( 1 + 总等级 * 5%) 
//            //总等级 = （当前等级 + 所有已升阶数的最大等级之和 - 1*阶数）
//            int baseExp = GetBaseExp(opHero);
//            int sumLv = SumLevel;
//            int sumExp = 0;
////             //训练英雄取表固定值
////             if (Config.trainer == 1)
////                 sumExp = baseExp;
////             else
////             {
////                 //非训练英雄按公式计算
////                 sumExp = (int)Math.Floor(baseExp * (1 + sumLv * 0.05));
////             }
//            return sumExp;
//        }

        /// <summary>
        /// 获取升级被吞噬时 用于计算总经验的基础经验
        /// </summary>
        /// <param name="opHero">当前升级的英雄</param>
        /// <returns></returns>
//        private int GetBaseExp(Hero opHero)
//        {
//            int baseExp = 0;
////             if(Config.trainer == 1)
////                 baseExp = Config.elemType == opHero.Config.elemType ? StarConfig.baseExp[3] : StarConfig.baseExp[2];
////             else
////                 baseExp = Config.elemType == opHero.Config.elemType ? StarConfig.baseExp[1] : StarConfig.baseExp[0];
//            return baseExp;
//        }

        /// <summary>
        /// 当前英雄是否满阶
        /// </summary>
        /// <returns></returns>
        public bool IsMax
        {
            get
            {
                if (Data.breakLv >= Glob.config.settingConfig.HeroMaxBreak)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// 获取升级被吞噬时 技能升级成功率
        /// </summary>
        /// <param name="opHero">当前升级的英雄</param>
        /// <returns></returns>
        //public int GetSkillRate(Hero opHero)
        //{
            //int type = 0;
            //             if (Config.elemType == opHero.Config.elemType)
            //                 type = Config.id == opHero.Config.id ? 2 : 1;
            // 
            //             return opHero.IsMax ? StarConfig.skillMaxRate[type] : StarConfig.skillRate[type];
        //    return 0;
        //}

        /// <summary>
        /// 当前英雄增加经验(英雄升级)
        /// </summary>
        /// <param name="val"></param>
        public void AddExp(int val)
        {
            if (val <= 0)
                return;
            int leftExp = val;
            //HeroExpConfig config = ExpConfig;
            int lv = Data.level == 0 ? 1 : Data.level;
            int exp = Data.exp;
            
            int levelexp = 0;
            while (leftExp > 0)
            {
                levelexp = Glob.config.settingConfig.HeroAddLevel * lv;

                exp = exp + leftExp;
                if (exp >= levelexp) //升级了
                {
                    leftExp = exp - levelexp;
                    exp = 0;
                    lv += 1;
                }
                else
                {
                    leftExp = 0;
                }
            }

            if (Data.level != lv || Data.exp != exp)  //等级发生变化
            {
                if (Data.level != lv)
                {
                    Data.level = lv;
                    //通知英雄升级
                }
                Data.exp = exp;
                Data.Update();//放到外面保存
            }
        }

        ///// <summary>
        ///// 英雄升星
        ///// </summary>
        ///// <param name="level">保留最大等级</param>
        ///// <param name="exp">保留经验</param>
        //public void StarLevelUp(int level, int exp)
        //{
        //    Glob.config.dicHero.TryGetValue(Config.nextLv, out HeroConfig newConfig);
        //    if (!Glob.config.dicHeroStar.TryGetValue(newConfig.star, out HeroStarConfig starConf))
        //    {
        //        Logger.LogError($"{player.ID}英雄升星,找不到星级配置{newConfig.star}");
        //        return;
        //    }
        //    Config = newConfig;
        //    StarConfig = starConf;
        //    Data.templId = Config.id;
        //    Data.level = level;
        //    Data.exp = exp;
        //    Data.Update();
        //}

        /// <summary>
        /// 英雄升阶
        /// </summary>
//         public void BreakThrough()
//         {
//             int breakId = (Config.elemType + 1) * 1000 + Data.level;
//             if (!Glob.config.dicHeroBreak.TryGetValue(breakId, out HeroBreakConfig conf))
//             {
//                 Logger.LogError($"{player.ID}英雄升阶,找不到升阶配置 {breakId}");
//                 return;
//             }
//             BreakConfig = conf;
//             Data.breakLv += 1;
//             Data.level = 1;
//             Data.exp = 0;
//             if (Data.skillLvArr[0] < Glob.config.heroSettingConfig.HeroSkillMaxLv[Config.trainer])
//                 Data.skillLvArr[0] += 1;
//             Data.Update();
//        }

        /// <summary>
        /// 英雄消息数据
        /// </summary>
        /// <returns></returns>
        public One_bag_equip GetHeroInfo()
        {
            One_bag_equip one = new One_bag_equip()
            {
                TemplID = Data.templId,
                Level = Data.level,           
            };
            return one;
        }

        /// <summary>
        /// 英雄消息数据
        /// </summary>
        /// <returns></returns>
        //public Cross_heroInfo GetHeroMsg()
        //{
        //    Cross_heroInfo one = new Cross_heroInfo()
        //    {
        //        ID = ID.ToString(),
        //        TemplID = Data.templId,
        //        Level = Data.level,
        //        Exp = Data.exp,
        //        BreakLv = Data.breakLv,
        //        IsLock = Data.isLock,
        //    };
        //    one.SkillsLv.Add(Data.skillLvArr);
        //    if (Data.dowers != null)
        //        one.Dowers.Add(Data.dowers);

        //    return one;
        //}

        //天赋升级
        //public void DowerLevelUp(int dowerNo)
        //{
//             if (Config.dowerTreeId == 0) return;
//             if (Glob.config.dicDowerTree.TryGetValue(Config.dowerTreeId * 1000 + dowerNo, out var treeConfig))
//             {
//                 if (Glob.config.dicDower.TryGetValue(treeConfig.dowerId, out var config))//
//                 {
//                     if (config.type < 20)  //20以下的都为加属性的天赋
//                     {
//                         EDowerType type = (EDowerType)config.type;
//                         if (DowerAttributes.ContainsKey(type))
//                             DowerAttributes[type] += config.value;
//                         else
//                             DowerAttributes[type] = config.value;
//                     }                  
//                 }
//             }
        //}

        public override void Dispose()
        {
            player = null;
            Data = null;
        }
    }
}
