using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 副本项
    /// </summary>
    public class FBStageItem
    {
        /// <summary>所需要章节配置</summary>
        //public FBChapterConfig ChapterConfig;

        /// <summary>所属关卡配置</summary>
        public FBLevelConfig LevelConfig;
        
        /// <summary>所需章节配置</summary>
       // public FBLevelStageConfig StageConfig;

        /// <summary>关卡阶段ID </summary>
        public int StageId => StageConfig.id;

        /// <summary>副本ID </summary>
        public int LevelId => LevelConfig.id;

        /// <summary>章节ID </summary>
        public int ChapterId => ChapterConfig.id;

        /// <summary>
        /// 是否为当天章节的第一关第一阶段
        /// </summary>
        public bool IsFirstLevelStage = false;
        public FBStageItem(FBLevelStageConfig config)
        {
            StageConfig = config;
            if (!Glob.config.dicFBLevel.TryGetValue(config.levelId, out LevelConfig))
            {
               Logger.LogError($"副本关卡配置[FBLevelConfig]未找到{config.levelId}");
                return;
            }
            if (!Glob.config.dicFBChapter.TryGetValue(LevelConfig.chId, out ChapterConfig))
            {
                Logger.LogError($"副本配置[FBChapterConfig]未找到{LevelConfig.chId}");
                return;
            }

            if (StageId == (ChapterId * 100 + 1) * 100 + 1)
                IsFirstLevelStage = true;
        }

        /// <summary>
        /// 判断关卡是否可挑战
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool CheckChallenge(Player player)
        {
            if (player.Level < ChapterConfig.needLv)
                return false;

            if(player.Data.fbInfo==null)
                player.Data.fbInfo = new List<int>();
            List<int> fbInfo = player.Data.fbInfo;
            if (fbInfo.Count < ChapterId)
            {
                for (int i= fbInfo.Count; i < ChapterId;i++)
                    fbInfo.Add(0);
            }
            int maxId = fbInfo[ChapterId - 1];
            //可以挑战
            if (maxId >= StageId-1 || IsFirstLevelStage|| LastStagePass(maxId))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断关卡是否可扫荡
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool CheckQuickWar(Player player)
        {
            List<int> fbInfo = player.Data.fbInfo;
            if (fbInfo == null)
                return false;
            int maxId = fbInfo[ChapterId - 1];
            //可以挑战
            if (maxId >= StageId)
            {
                return true;
            }
            return false;
        }

        private bool LastStagePass(int maxId)
        {            
            if (StageId == LevelId * 100 + 1) //关卡的第一关
            {
                if (maxId / 100 == LevelId - 1)
                {
                    if (!Glob.config.dicFBLevelStage.ContainsKey(maxId + 1))
                        return true;
                }
            }
            return false;
        }

    }*/
}
