using MongoDB.Bson;
using PbError;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 玩家建筑基类
    /// </summary>
    public class Build : BaseEntity<TBuild>
    {
        /// <summary>所属玩家</summary>
        public Player player { get; private set; }

        /// <summary>建筑等级</summary>
        public int Level => Data.level;

        /// <summary>建筑模型Id</summary>
        public int TemplId => Data.templId;

        /// <summary>建筑位置</summary>
        public int Location => Data.location;

        /// <summary>是否创建成功</summary>
        public bool IsSucceed = false;

        /// <summary>建筑配置</summary>
        public BuildConfig BuildConfig;

        /// <summary>建筑类型</summary>
        public EBuildType BuildType => (EBuildType)Data.templId;

        ///// <summary>建筑状态</summary>
        //public Enum_build_state State => (Enum_build_state)Data.state;

        /// <summary>建筑等级配置</summary>
        public BuildLvConfig LevelConfig;

        /// <summary>是否显示在地图上(除主城外位置为0的为不显示)</summary>
        public bool IsDisplay => Data.templId == 1 ? true : (Data.location != 0);

        /// <summary>是否为高级建筑</summary>
        public bool IsAdvanced => Data.templId > 10 ? true : false;

        /// <summary>改造前原建筑SID</summary>
        public int FromId => TBuild.ToShortId(Data.fromId);

        //public Build(Player p, TBuild data)
        //{
        //    player = p;
        //    Data = data;

        //    if (!Glob.config.dicBuild.TryGetValue(data.templId, out BuildConfig))
        //    {
        //        Logger.LogError($"{player.ID}的建筑创建失败,找不到建筑配置{data.templId}");
        //        return;
        //    }
        //    GetLevelConfig();
        //    IsSucceed = true;
        //}

        /// <summary>
        /// 获取当前建筑对应的等级配置
        /// </summary>
        protected virtual void GetLevelConfig()
        {
            int confId = TemplId * 1000 + Level;
            if (!Glob.config.dicBuildLv.TryGetValue(confId, out LevelConfig))
            {
                Logger.LogError($"{player.ID}的建筑找不到等级配置{confId}");
                return;
            }
        }

        /// <summary>
        /// 获取建造完成时间
        /// </summary>
        /// <returns></returns>
        public virtual int GetBuildTime()
        {
            if (Data.buildTime != null)
                return Data.buildTime.ToTimestamp() - 2;
            return 0;
        }

        /// <summary>
        /// 获取生产完成时间
        /// </summary>
        /// <returns></returns>
        public virtual int GetOutputTime()
        {
            if (Data.outTime != null)
                return Data.outTime.ToTimestamp();
            return 0;
        }

        /// <summary>
        /// 获取上次收取资源时间
        /// </summary>
        /// <returns></returns>
        public virtual int GetLastResTime()
        {
            if (Data.getTime != null)
                return Data.getTime.ToTimestamp();
            return 0;
        }

        /// <summary>
        /// 建筑收取资源
        /// </summary>
        public virtual void GetRes()
        {
            if ((EBuildType)TemplId == EBuildType.Mine || (EBuildType)TemplId == EBuildType.Fram)
            {
                foreach (var guideId in Glob.config.guideSettingConfig.firstBuildGetRes)
                {
                    if(player.Guide.IsOpen(guideId))
                        player.Guide.FinishGuide(guideId);
                }
            }
        }

        /// <summary>
        /// 建筑生产项研究
        /// </summary>
        public virtual void Research(int ItemId)
        {

        }

        /// <summary>
        /// 建筑开始生产
        /// </summary>
        /// <param name="ItemId"></param>
        public virtual void Work(int ItemId)
        {

        }

        /// <summary>
        /// 建筑生产加速
        /// </summary>
        public virtual void WorkQuickly()
        {

        }

        /// <summary>
        /// 建筑增减生产数量
        /// </summary>
        /// <param name="isAdd"></param>
        public virtual void WorkNum(bool isAdd)
        {

        }
        
        /// <summary>
        /// 建筑开始升级
        /// </summary>
        public virtual void BuildLevelUp(BuildLvConfig lvConfig)
        {
            //Data.buildTime = DateTime.Now.AddSeconds(lvConfig.times);
            //Data.state = (int)Enum_build_state.BsUpgrade;
            //Data.Update();
        }

        /// <summary>
        /// 建造、升级完成
        /// </summary>
        public virtual void BuildEnd()
        {
            //Data.buildTime = null;
            //if(State == Enum_build_state.BsUpgrade || State == Enum_build_state.BsBeingBuilt)
            //    Data.level += 1;
            //Data.state = (int)Enum_build_state.BsNormal;
            //if (BuildType == EBuildType.Fram || BuildType == EBuildType.Mine || BuildType == EBuildType.Tower ||
            //    BuildType == EBuildType.FramAdv || BuildType == EBuildType.MineAdv)
            //{
            //    Data.getTime = DateTime.Now;
            //    Data.state = (int)Enum_build_state.BsOutput;
            //    if (BuildType == EBuildType.Tower)
            //    {
            //        Data.outputId = DateTime.Now.ToTimestamp();
            //        Data.outputNum = DateTime.Now.ToTimestamp();
            //    }
            //}
            //if (BuildType == EBuildType.StrongHold)
            //{
            //    player.Data.cityLv = Level;
            //    player.SaveData();
            //}
            ////重新获取等级配置
            //GetLevelConfig();
            //Data.Update();
            ////重新计算资源上限
            //if (BuildType == EBuildType.StorageFood || BuildType == EBuildType.StorageFoodAdv ||
            //    BuildType == EBuildType.StorageStone || BuildType == EBuildType.StorageStoneAdv || BuildType == EBuildType.House)
            //    player.GetResMax();
            //player.TriggerTask(ETaskType.Build_LevelUp, 1, (int)BuildType, Level);

            ////高级建筑改造完成时,处理原建筑解锁
            //if(FromId != 0)
            //{
            //    if (player.buildList.TryGetValue(FromId, out Build beforeBuild))
            //        beforeBuild.TransUnLock();
            //}
        }

        /// <summary>
        /// 转换完成
        /// </summary>
        public virtual void TransEnd()
        {
            //Data.buildTime = null;
            //Data.state = (int)Enum_build_state.BsNormal;
            //if (BuildType == EBuildType.Fram || BuildType == EBuildType.Mine || 
            //    BuildType == EBuildType.FramAdv || BuildType == EBuildType.MineAdv)
            //{
            //    Data.getTime = DateTime.Now;
            //    if (Data.surplus > 0)//有剩余资源的,重新显示时加上
            //    {
            //        Data.getTime = DateTime.Now.AddSeconds(-Data.surplus-10);
            //        Data.surplus = 0;
            //    }
            //    Data.state = (int)Enum_build_state.BsOutput;
            //}
            //if (!IsAdvanced)//低级建筑fromid赋空
            //    Data.fromId = ObjectId.Empty;
            //else
            //{
            //    if (Data.location == 0)//高级建筑为空闲状态时fromid赋空
            //        Data.fromId = ObjectId.Empty;
            //}
            //Data.Update();
            ////重新计算资源上限
            //if (BuildType == EBuildType.StorageFood || BuildType == EBuildType.StorageFoodAdv ||
            //    BuildType == EBuildType.StorageStone || BuildType == EBuildType.StorageStoneAdv)
            //    player.GetResMax();
        }

        /// <summary>
        /// 改造、转换目标完成时,解除原建筑锁定
        /// </summary>
        public virtual void TransUnLock()
        {
            //Data.buildTime = null;
            //Data.state = (int)Enum_build_state.BsNormal;
            //if (!IsAdvanced)//低级建筑fromid赋空
            //    Data.fromId = ObjectId.Empty;
            //else
            //{
            //    if(Data.location == 0)//高级建筑为空闲状态时fromid赋空
            //        Data.fromId = ObjectId.Empty;
            //}
            //Data.Update();
        }

        /// <summary>
        /// 检测建筑时间（登陆时调用）
        /// </summary>
        public virtual void CheckBuildTime()
        {
            //if (Data.buildTime != null)
            //{
            //    if(DateTime.Now.ToTimestamp() >= Data.buildTime.ToTimestamp())
            //    {
            //        if (Data.state == (int)Enum_build_state.BsTransform)
            //        {
            //            if (Data.location != 0)
            //                TransEnd();
            //            else
            //                TransUnLock();
            //        }
            //        else
            //        {
            //            BuildEnd();
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 设置建筑位置
        /// </summary>
        /// <param name="loc"></param>
        public void SetLocation(int loc)
        {
            if (Data.location != loc)
            {
                Data.location = loc;
                Data.Update();
            }
        }

        /// <summary>
        /// 建筑随机权重产出生产物
        /// </summary>
        /// <param name="rItems">生产物权重配置</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public List<int> RandomOutput(List<int[]> rItems, int num)
        {
            List<int> ids = new List<int>();
            int newId = 0;
            List<int> wightVals = new List<int>();
            List<int> weightItems = new List<int>();
            foreach (int[] iteminfo in rItems)
            {
                weightItems.Add(iteminfo[0]);
                wightVals.Add(iteminfo[1]);
            }
            int[] weightItemVal = wightVals.ToArray();
            for (int i = 0; i < num; i++)
            {
                newId = weightItems[RandomHelper.WeightRandom(weightItemVal)];
                ids.Add(newId);
            }
            return ids;
        }

        public override void Dispose()
        {
            player = null;
            Data = null;
            BuildConfig = null;
            LevelConfig = null;
        }
    }*/
}
