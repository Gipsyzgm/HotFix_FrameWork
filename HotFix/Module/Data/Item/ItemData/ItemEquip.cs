using PbBag;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    /// <summary>
    /// 装备
    /// </summary>
    public class ItemEquip : BaseItemData
    {
        public TItemEquip Data;

        private int mStar = 1;
        /// <summary>星级</summary>
        public int Star
        {
            get => Data == null ? mStar : Data.Star;
            set
            {
                if (Data == null)
                    mStar = value;
                else
                    Data.Star = value;
            }
        }


        private int mLevel = 1;
        /// <summary>强化等级</summary>
        public int Level
        {
            get => Data == null ? mLevel : Data.Level;
            set
            {
                if (Data == null)
                    mLevel = value;
                else
                    Data.Level = value;
            }
        }

    
        private EItemEquipPlace mPlace = EItemEquipPlace.None;
        /// <summary>穿戴位置 0没穿戴</summary>
        public EItemEquipPlace Place
        {
            get => Data == null ? mPlace : (EItemEquipPlace)Data.Index;
            set
            {
                if (Data == null)
                    mPlace = value;
                else
                    Data.Index = (int)value;
            }
        }

        private int mCount = 0;
        /// <summary>数量</summary>
        public int Count
        {
            get => Data == null ? mCount : Data.Count;
            set
            {
                if (Data == null)
                    mCount = value;
                else
                    Data.Count = value;
            }
        }

        //装备属性值
        public float[] AttribsValue = new float[ItemSetting.AttribTypeCount];

        //装备攻击属性作用类型
        public EItemAttackType AttackType;

        public ItemEquip(int tempId) : base(tempId)
        {
        }

        public ItemEquip(TItemEquip data) : base(data.TemplId)
        {
            Data = data;
            setStarLevelConfig();

            setAttribs();
        }
        protected void setStarLevelConfig()
        {
            int starLevel = TempId * 10 + Star;
            //if (!Mgr.Config.dicItemStarLevel.TryGetValue(starLevel, out StarLevelConfig))
            //{
            //    CLog.Error("StarLevelConfig Not Find:" + starLevel);
            //    return;
            //}
        }
        protected void setAttribs()
        {
            //for (int i = 0; i < AttribsValue.Length; i++)
            //    AttribsValue[i] = StarLevelConfig.baseAttr[i] + StarLevelConfig.lvAddAttr[i] * (Level - 1);            
        }
             
        //是否达到最高等级最高星级
        //public bool IsMaxStarLevel=> StarLevelConfig.costItem == 0;

        //是否达到当前星级最高等级
        //public bool IsMaxLevel=> Level >= StarLevelConfig.lvMax;
        //升级花费
        //public int LevelUpCost => StarLevelConfig.costMoney + StarLevelConfig.costMoneyAdd * (Level - 1);

        PlayerWarData warData = PlayerMgr.PlayerWarData;

        //执行升级
        public void LevelUp()
        {
            //if (IsMaxLevel) return;
            //PlayerMgr.MainPlayer.Gold -= LevelUpCost;
            Level += 1;
            setAttribs();
            ItemMgr.I.SaveData();
            warData.Recount();

        }

        //执行升星
        public void StartUp()
        {
            //if (IsMaxStarLevel) return;
            //Count -= StarLevelConfig.costItem;
            Star += 1;
            Level = 1; //升完星后变成1级
            setStarLevelConfig();
            setAttribs();
            ItemMgr.I.SaveData();
            warData.Recount();

        }

    }
}