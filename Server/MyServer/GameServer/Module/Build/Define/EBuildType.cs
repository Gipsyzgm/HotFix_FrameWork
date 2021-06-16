using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 建筑类型
    /// </summary>
    public enum EBuildType
    {
        /// <summary>主城堡</summary>
        StrongHold = 1,
        /// <summary>农场</summary>
        Fram,
        /// <summary>矿场</summary>
        Mine,
        /// <summary>房屋</summary>
        House,
        /// <summary>粮仓</summary>
        StorageFood,
        /// <summary>角斗场</summary>
        Tower,
        /// <summary>工坊</summary>
        Forge,
        /// <summary>训练场</summary>
        TrainingCamp,
        /// <summary>铁匠铺</summary>
        Smithy,
        /// <summary>矿仓</summary>
        StorageStone,
        /// <summary>高级农场</summary>
        FramAdv,
        /// <summary>高级矿场</summary>
        MineAdv,
        /// <summary>高级房屋</summary>
        HouseAdv,
        /// <summary>高级粮仓</summary>
        StorageFoodAdv,
        /// <summary>高级矿仓</summary>
        StorageStoneAdv,
        /// <summary>猎人小屋</summary>
        HunterHouse,
        /// <summary>炼金实验室</summary>
        AlchemyLab,
        /// <summary>英雄学院</summary>
        HeroSchool,
    }
}
