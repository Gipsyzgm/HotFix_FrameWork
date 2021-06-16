using PbBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 工坊
    /// </summary>
    public class BForge : Build
    {
        public BForge(Player p, TBuild data) : base(p, data)
        {

        }

        /// <summary>
        /// 生产项研究开始
        /// </summary>
        public override void Research(int ItemId)
        {
            //Glob.config.dicBForgeOutput.TryGetValue(ItemId, out BForgeOutputConfig outputConfig);
            //Data.outputId = ItemId;
            //Data.outputNum = outputConfig.num;
            //Data.getTime = null;
            //Data.outTime = DateTime.Now.AddSeconds(outputConfig.openTime);
            //Data.state = (int)Enum_build_state.BsResearch;
            //Data.Update();
            
        }

        /// <summary>
        /// 工坊开始制作
        /// </summary>
        /// <param name="ItemId"></param>
        public override void Work(int ItemId)
        {
            //Glob.config.dicBForgeOutput.TryGetValue(ItemId, out BForgeOutputConfig outputConfig);
            //Data.outputId = ItemId;
            //Data.outputNum = 1;
            //Data.getTime = DateTime.Now;
            //Data.outTime = DateTime.Now.AddSeconds(outputConfig.time);
            //Data.state = (int)Enum_build_state.BsOutput;
            //Data.Update();
        }

        /// <summary>
        /// 收取资源
        /// </summary>
        public override void GetRes()
        {
            //if (!Glob.config.dicBForgeOutput.TryGetValue(Data.outputId, out BForgeOutputConfig OutputConfig))
            //{
            //    Logger.LogError($"找不到工坊产出配置{Data.outputId}");
            //    player.SendError(typeof(SC_build_getRes), $"找不到工坊产出配置{Data.outputId}");
            //    return;
            //}
            //if (Data.state == (int)Enum_build_state.BsResearch)
            //{
            //    ResearchEnd(OutputConfig);
            //    return;
            //}
            //if (Data.state == (int)Enum_build_state.BsOutput)
            //{
            //    WorkEnd(OutputConfig);
            //}
        }

        /// <summary>
        /// 研究完成
        /// </summary>
        /// <param name="OutputConfig"></param>
        private void ResearchEnd(BForgeOutputConfig OutputConfig)
        {
            //if (DateTime.Now.ToTimestamp() - Data.outTime.ToTimestamp() < 0)
            //{
            //    Logger.LogError($"BForge研究时间未到不能收取");
            //    player.SendError(typeof(SC_build_getRes), $"BForge研究时间未到不能收取");
            //    return;
            //}
            ////指引特殊判断  首次研究完成时 收取指定道具
            //if (player.Guide.IsOpen(Glob.config.guideSettingConfig.firstMakeItemGuideId[0]))
            //{
            //    Glob.itemMgr.PlayerAddNewItem(player, Glob.config.guideSettingConfig.firstMakeItemGuideId[1], 1, true, PbCom.Enum_bag_itemsType.BiForgeOutput);
            //}
            //else
            //{
            //    Glob.itemMgr.PlayerAddNewItem(player, OutputConfig.itemId, OutputConfig.num, true, PbCom.Enum_bag_itemsType.BiForgeOutput);
            //}
            //Glob.config.dicItem.TryGetValue(OutputConfig.itemId, out ItemConfig itemConf);
            //if(itemConf != null)
            //    player.TriggerTask(ETaskType.Item_Make, OutputConfig.num, itemConf.quality, itemConf.id);
            //Glob.logMgr.LogBuildWork(player.ID, (int)BuildType, OutputConfig.id, OutputConfig.num);
            //Glob.logMgr.LogFun(player.ID, FunctionUseType.ForgeWork, OutputConfig.num);
            //Data.outputId = 0;
            //Data.outputNum = 0;
            //Data.getTime = null;
            //Data.outTime = null;
            //Data.state = (int)Enum_build_state.BsNormal;
            //Data.Update();
            //player.AddBuildOutputId(OutputConfig.id, BuildType);
            //SC_build_getRes msg = new SC_build_getRes();
            //msg.SID = SID;
            //msg.GetTime = GetLastResTime();
            //msg.OutTime = GetOutputTime();
            //msg.OpenId = OutputConfig.id;
            //msg.State = State;
            //player.Send(msg);
        }

        /// <summary>
        /// 收取已制作完成的道具
        /// </summary>
        /// <param name="OutputConfig"></param>
        private void WorkEnd(BForgeOutputConfig OutputConfig)
        {
            //if (DateTime.Now.ToTimestamp() - Data.getTime.ToTimestamp() < OutputConfig.time)
            //{
            //    Logger.LogError($"BForge生产时间未到不能收取");
            //    player.SendError(typeof(SC_build_getRes), $"BForge生产时间未到不能收取");
            //    return;
            //}
            //int time = DateTime.Now.ToTimestamp() - Data.getTime.ToTimestamp();
            //int num = time / OutputConfig.time;
            //if (num > Data.outputNum)
            //    num = Data.outputNum;
            //Glob.itemMgr.PlayerAddNewItem(player, OutputConfig.itemId, num, true, PbCom.Enum_bag_itemsType.BiForgeOutput);
            //Glob.logMgr.LogBuildWork(player.ID, (int)BuildType, OutputConfig.id, num);
            //Glob.logMgr.LogFun(player.ID, FunctionUseType.ForgeWork, num);
            //if (num == Data.outputNum)
            //{
            //    Data.outputId = 0;
            //    Data.outputNum = 0;
            //    Data.getTime = null;
            //    Data.outTime = null;
            //    Data.state = (int)Enum_build_state.BsNormal;
            //}
            //else
            //{
            //    Data.outputNum -= num;
            //    Data.getTime = ((DateTime)Data.getTime).AddSeconds(num * OutputConfig.time);
            //}
            //Glob.config.dicItem.TryGetValue(OutputConfig.itemId, out ItemConfig itemConf);
            //if (itemConf != null)
            //    player.TriggerTask(ETaskType.Item_Make, num, itemConf.quality, itemConf.id);
            //Data.Update();
            
            //SC_build_getRes msg = new SC_build_getRes();
            //msg.SID = SID;
            //msg.GetTime = GetLastResTime();
            //msg.OutTime = GetOutputTime();
            //msg.State = State;
            //msg.OpenId = Data.outputId;
            //msg.OutNum = Data.outputNum;
            //player.Send(msg);
        }

        /// <summary>
        /// 生产研究加速
        /// </summary>
        public override void WorkQuickly()
        {
            if (!Glob.config.dicBForgeOutput.TryGetValue(Data.outputId, out BForgeOutputConfig OutputConfig))
            {
                Logger.LogError($"找不到工坊产出配置{Data.outputId}");
                return;
            }
            Data.getTime = DateTime.Now.AddSeconds(-Data.outputNum * OutputConfig.time -3);
            Data.outTime = DateTime.Now.AddSeconds(-3);
        }

        /// <summary>
        /// 增减生产数量
        /// </summary>
        /// <param name="isAdd"></param>
        public override void WorkNum(bool isAdd)
        {
            //Glob.config.dicBForgeOutput.TryGetValue(Data.outputId, out BForgeOutputConfig outputConfig);
            //if (isAdd)
            //{
            //    //扣除资源
            //    foreach (int[] nums in outputConfig.cost)
            //    {
            //        player.DeductVirtualItemNum((EItemSubTypeVirtual)nums[0], nums[1]);
            //    }
            //    foreach (int[] items in outputConfig.costItems)
            //    {
            //        player.DeductItemPropNum(items[0], items[1], ItemCostType.ForgeMake);
            //    }
            //    Data.outputNum += 1;
            //    Data.outTime = ((DateTime)Data.outTime).AddSeconds(outputConfig.time);
            //}
            //else
            //{
            //    if(Data.outputNum == 1)
            //    {
            //        Data.outputId = 0;
            //        Data.outputNum = 0;
            //        Data.getTime = null;
            //        Data.outTime = null;
            //        Data.state = (int)Enum_build_state.BsNormal;
            //    }
            //    else
            //    {
            //        int lastTime = Data.outTime.ToTimestamp() - DateTime.Now.ToTimestamp();
            //        //有完成的未领取，但还有最后一个在制作，不返还资源
            //        if (lastTime > 0 && lastTime < outputConfig.time)
            //        {
            //            Data.outputNum -= 1;
            //            Data.outTime = ((DateTime)Data.outTime).AddSeconds(-outputConfig.time);
            //        }
            //        else
            //        {
            //            //返还资源
            //            foreach (int[] nums in outputConfig.cost)
            //            {
            //                player.AddVirtualItemNum((EItemSubTypeVirtual)nums[0], nums[1]);
            //            }
            //            Glob.itemMgr.PlayerAddNewItems(player, outputConfig.costItems);
            //            Data.outputNum -= 1;
            //            Data.outTime = ((DateTime)Data.outTime).AddSeconds(-outputConfig.time);
            //        }
            //    }
            //}
            //Data.Update();
        }
    }*/
}
