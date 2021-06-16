using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 建筑-主城
    /// </summary>
    public class BStrongHold : Build
    {
        /// <summary>主城配置</summary>
        public BStrongHoldConfig StrongHoldConfig;

        /// <summary>可建造的建筑位置Ids</summary>
        public List<int> BuildAddrList = new List<int>();

//         public BStrongHold(Player p, TBuild data) : base(p, data)
//         {
//             SetConfig();
//         }

        

        /// <summary>
        /// 根据主城等级设置主城的配置(升级后需调用)
        /// </summary>
        public void SetConfig()
        {
            if (!Glob.config.dicBStrongHold.TryGetValue(Data.level, out StrongHoldConfig))
            {
                Logger.LogError($"{player.ID}的建筑找不到主城等级配置{Data.level}");
                return;
            }
            BuildAddrList.Clear();
            foreach (int areaId in StrongHoldConfig.openArea)
            {
                BuildAreaConfig areaConfig;
                if (Glob.config.dicBuildArea.TryGetValue(areaId, out areaConfig))
                {
                    foreach (int addrId in areaConfig.bulidAdsId)
                    {
                        if (!BuildAddrList.Contains(addrId))
                            BuildAddrList.Add(addrId);
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前建筑对应的等级配置
        /// </summary>
//         protected override void GetLevelConfig()
//         {
//             base.GetLevelConfig();
//             SetConfig();
//         }

        /// <summary>
        /// 主城等级可建造的建筑数量判断
        /// </summary>
        /// <param name="buildId">建筑id</param>
        /// <returns></returns>
        public bool CheckBuildNum(int buildId)
        {
            //int count = player.GetBuildTypeNum(buildId);
            //if (StrongHoldConfig.buildMax[buildId - 2] > count)
            //    return true;
            return false;
        }

    }*/
}
