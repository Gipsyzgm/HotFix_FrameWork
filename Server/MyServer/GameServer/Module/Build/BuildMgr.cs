

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 建筑管理
    /// </summary>
    public class BuildMgr
    {
        ///// <summary>全部玩家建筑数据</summary>
        //public DictionarySafe<ObjectId, DictionarySafe<ObjectId, TBuild>> playerBuildList = new DictionarySafe<ObjectId, DictionarySafe<ObjectId, TBuild>>();

        public BuildMgr()
        {
            //List<TBuild> buildList = DBReader.Instance.SelectAllList<TBuild>();
            //DictionarySafe<ObjectId, TBuild> playBuildList;
            //foreach (TBuild build in buildList)
            //{
            //    if (!playerBuildList.TryGetValue(build.pId, out playBuildList))
            //    {
            //        playBuildList = new DictionarySafe<ObjectId, TBuild>();
            //        playerBuildList.Add(build.pId, playBuildList);
            //    }
            //    playBuildList.AddOrUpdate(build.id, build);
            //}
        }

        /// <summary>
        /// 创建玩家默认建筑
        /// </summary>
        /// <param name="player"></param>
        public void PlayerCreateInitBuild(Player player)
        {
            PlayerInitConfig config = Glob.config.playerInitConfig;
            
            for (int i = 0; i < config.initBuilds.Length; i++)
            {
                TBuild build = new TBuild(true);
                build.pId = player.ID;
                build.templId = config.initBuilds[i];
                build.level = 1;
                if ((EBuildType)build.templId == EBuildType.StrongHold)
                {
                    build.location = 0;
                    player.Data.cityLv = 1;
                }
                else
                    build.location = i + 1;
                build.state = 0;
                build.Insert();
                AddTBuildData(player, build);
            }
        }


        /// <summary>
        /// 增加建筑数据到集合，并创建建筑实体
        /// </summary>
        /// <param name="player"></param>
        /// <param name="build"></param>
        public void AddTBuildData(Player player, TBuild build)
        {
            //DictionarySafe<ObjectId, TBuild> pBuildList;
            //if (!playerBuildList.TryGetValue(player.ID, out pBuildList))
            //{
            //    pBuildList = new DictionarySafe<ObjectId, TBuild>();
            //    playerBuildList.Add(player.ID, pBuildList);
            //}
            //pBuildList.AddOrUpdate(build.id, build);
            PlayerAddBuild(player, build);
        }

        /// <summary>
        /// 读取玩家建筑数据
        /// </summary>
        /// <param name="player"></param>
        public void ReadPlayerBuild(Player player)
        {
            //DictionarySafe<ObjectId, TBuild> list;
            //if (playerBuildList.TryGetValue(player.ID, out list))
            //{
            //    foreach (TBuild build in list.Values)
            //    {
            //        PlayerAddBuild(player, build);
            //    }
            //    //初始计算资源上限
            //    player.GetResMax();
            //}
        }


        /// <summary>
        /// 玩家新建筑实体
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="tbuild">建筑数据</param>
        public void PlayerAddBuild(Player player, TBuild tbuild)
        {
            Build build = null;
            switch ((EBuildType)tbuild.templId)
            {
                case EBuildType.StrongHold:
                    build = new BStrongHold(player, tbuild);
                    break;
                case EBuildType.Fram:
                    build = new BFram(player, tbuild);
                    break;
                case EBuildType.Mine:
                    build = new BMine(player, tbuild);
                    break;
                case EBuildType.House:
                    build = new BHouse(player, tbuild);
                    break;
                case EBuildType.StorageFood:
                    build = new BStorageFood(player, tbuild);
                    break;
                case EBuildType.Tower:
                    build = new BTower(player, tbuild);
                    break;
                case EBuildType.Forge:
                    build = new BForge(player, tbuild);
                    break;
                case EBuildType.TrainingCamp:
                    build = new BTrainingCamp(player, tbuild);
                    break;
                case EBuildType.Smithy:
                    build = new BSmithy(player, tbuild);
                    break;
                case EBuildType.StorageStone:
                    build = new BStorageStone(player, tbuild);
                    break;
                case EBuildType.FramAdv:
                    build = new BFramAdv(player, tbuild);
                    break;
                case EBuildType.MineAdv:
                    build = new BMineAdv(player, tbuild);
                    break;
                case EBuildType.HouseAdv:
                    build = new BHouseAdv(player, tbuild);
                    break;
                case EBuildType.StorageFoodAdv:
                    build = new BStorageFoodAdv(player, tbuild);
                    break;
                case EBuildType.StorageStoneAdv:
                    build = new BStorageStoneAdv(player, tbuild);
                    break;
                case EBuildType.HunterHouse:
                    build = new BHunterHouse(player, tbuild);
                    break;
                case EBuildType.AlchemyLab:
                    build = new BAlchemyLab(player, tbuild);
                    break;
                case EBuildType.HeroSchool:
                    build = new BHeroSchool(player, tbuild);
                    break;
            }
            if (build == null || !build.IsSucceed)
                return;
            //player.buildList.Add(build.SID, build);
            //if (build.BuildType == EBuildType.StrongHold)
            //    player.StrongHold = (BStrongHold)build;
        }


        /// <summary>
        /// 发送玩家建筑数据
        /// </summary>
        /// <param name="player"></param>
        public void SendPlayerBuild(Player player)
        {
            //SC_build_list msg = new SC_build_list();
            //One_buildInfo one;
            //foreach (Build item in player.buildList.Values)
            //{
            //    one = new One_buildInfo()
            //    {
            //        SID = item.SID,
            //        TemplID = item.Data.templId,
            //        Level = item.Data.level,
            //        Location = item.Data.location,
            //        BuildTime = item.GetBuildTime(),
            //        OutTime = item.GetOutputTime(),
            //        GetTime = item.GetLastResTime(),
            //        State = item.State,
            //        OutputId = item.Data.outputId,
            //        OutputNum = item.Data.outputNum,
            //        FromId = TBuild.ToShortId(item.Data.fromId)
            //    };
            //    msg.List.Add(one);
            //}
            //if(player.Data.forgeIds != null)
            //    msg.ForgeIds.Add(player.Data.forgeIds);
            //if(player.Data.trainingIds != null)
            //    msg.TrainingIds.Add(player.Data.trainingIds);
            //if (player.Data.smithyIds != null)
            //    msg.SmithyIds.Add(player.Data.smithyIds);
            //player.Send(msg);
        }

    }*/
}
