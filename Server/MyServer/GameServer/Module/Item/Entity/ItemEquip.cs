using CommonLib;
using CommonLib.Comm;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using PbCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 装备实体
    /// </summary>
    public partial class ItemEquip : BaseEntity<TItemEquip>
    {
        /// <summary> 道具配置 </summary>
        public ItemConfig Config;

        /// <summary>所属玩家Id</summary>
        public ObjectId PlayerId { get; }

        /// <summary>模板Id</summary>
        public int TemplId => Config.id;

        /// <summary>装备等级</summary>
        public int Level => Data.level;




        /// <summary>装备类型</summary>
        public EItemType Type;

      

        /// <summary>
        /// 是否创建成功
        /// </summary>
        public bool IsSucceed = false;
        public ItemEquip(ObjectId playerid, TItemEquip data)
        {
            PlayerId = playerid;
            Data = data;
            //Equipped = ObjectId.Empty == data.owner ? false : true;
            if (!Glob.config.dicItem.TryGetValue(data.templId, out Config))
            {
                Logger.LogError($"{playerid}的装备物品创建失败,找不到模板数据{data.templId}");
                return;
            }
            Type = (EItemType)Config.type;
//            int levelId = data.templId * 100 + data.level;
//             if (!Glob.config.dicItemEquip.TryGetValue(levelId, out EquipConfig))
//             {
//                 Logger.LogError($"{playerid}的装备物品创建失败,找不到装备属性数据{levelId}");
//                 return;
//             }
//             if(!Glob.config.dicItemEquipExp.TryGetValue(data.level, out EquipExpConfig))
//             {
//                 Logger.LogError($"{playerid}的装备物品创建失败,找不到装备等级经验数据{data.templId}");
//                 return;
//             }
            IsSucceed = true;
        }

        /// <summary>
        /// 当前装备升级增加经验
        /// </summary>
        /// <param name="val">经验值</param>
        //public void AddExp(int val)
        //{
        //    if (val <= 0)
        //        return;
        //    int leftExp = val;
        //    //ItemEquipExpConfig config = EquipExpConfig;
        //    int lv = Data.level;
            //int exp = Data.exp;
//             while (leftExp > 0)
//             {
//                 if (Glob.config.dicItemEquipExp.TryGetValue(lv, out config))
//                 {
//                     exp = exp + leftExp;
//                     if (exp >= config.exp[Quality - 1]) //升级了
//                     {
//                         leftExp = exp - config.exp[Quality - 1];
//                         exp = 0;
//                         lv += 1;
//                     }
//                     else
//                     {
//                         leftExp = 0;
//                     }
//                     if (lv == MaxLevel)//满级了 经验给0
//                         exp = 0;
//                 }
//                 else
//                 {
//                     lv = lv - 1;
//                     exp = Glob.config.dicItemEquipExp[lv].exp[Quality - 1];
//                     leftExp = 0;
//                 }
//             }
//             if (lv >= MaxLevel)
//             {
//                 lv = MaxLevel;
//                 exp = 0;
//             }
//             if (Data.level != lv || Data.exp != exp)  //等级发生变化
//             {
//                 if (Data.level != lv)
//                 {
//                     Data.level = lv;
//                     if (Glob.config.dicItemEquipExp.TryGetValue(lv, out ItemEquipExpConfig conf))
//                         EquipExpConfig = conf;
//                     int levelId = Data.templId * 100 + Data.level;
//                     if (Glob.config.dicItemEquip.TryGetValue(levelId, out ItemEquipConfig confE))
//                         EquipConfig = confE;
//                 }
//                 Data.exp = exp;
//                 Data.Update();
// 
//                 //SC_player_exp msg = new SC_player_exp();
//                 //msg.Level = Data.level;
//                 //msg.Exp = Data.exp;
//                 //msg.AddExp = val;
//                 //Send(msg);
//             }
 //       }

        /// <summary>装备总属性</summary>
        //public int[] GetTotalAttr()
        //{
        //    int[] totalAttr = new int[4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        //totalAttr[i] += EquipConfig.baseAttr[i];
        //        //totalAttr[i] += EquipConfig.addAttr[i] * Level;
        //        //if (Data.insetLv != null &&　Data.insetLv[i] > 0)
        //          //  totalAttr[i] += Glob.config.dicGemAttr[Data.insetLv[i]].attr;
        //    }
        //    return totalAttr;
        //}

        public One_bag_equip GetEquipInfo()
        {
            One_bag_equip oneEquip = new One_bag_equip()
            {
                SID = Data.shortId,
                TemplID = Data.templId,
                Level = Data.level,

            };
            return oneEquip;
        }

//         public Cross_bag_equip GetEquipMsg()
//         {
//             Cross_bag_equip oneEquip = new Cross_bag_equip()
//             {
//                 ItemID = ID.ToString(),
//                 TemplID = TemplId,
//                 Level = Level,
//                 Exp = Data.exp,
//             };
//             return oneEquip;
//         }

        public override void Dispose()
        {
            Data = null;
            Config = null;
//             EquipConfig = null;
//             EquipExpConfig = null;
        }
    }
}
