using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbTask;
using PbBag;
using CommonLib.Comm.DBMgr;

namespace GameServer.Module
{
    public class TaskBountyData : BaseTask<TTaskBounty>
    {
        /*
        public int TaskId => Config.id;

        /// <summary>任务进度</summary>
        public override int Progress => Data == null ? 0 : Data.pro;

        public TaskBountyConfig Config { get; protected set; }
        
        public override int[] Condition => Config.condition;

        /// <summary>
        /// 任务类型
        /// </summary>
        public override ETaskType Type => (ETaskType)Config.type;

        public TaskBountyData(Player _player, TaskBountyConfig config)
        {
            player = _player;
            Config = config;
            if (Data == null)
            {
                Data = new TTaskBounty(true);
                Data.pId = player.ID;
                Data.taskId = TaskId;
                Data.pro = 0;
                Data.Insert();
            }
        }
        public TaskBountyData(Player _player, TTaskBounty data, TaskBountyConfig config)
        {
            player = _player;
            Config = config;
            Data = data;
        }
        
        /// <summary>当前任务状态</summary>
        public override EAwardState State
        {
            get
            {
                if (Data != null)
                {
                    if (Data.isGet)
                        return EAwardState.HaveGet;
                    else if (Progress >= Condition[0])
                        return EAwardState.Done;
                }
                return EAwardState.Undone;
            }
        }

        /// <summary>
        /// 设置任务监听
        /// </summary>
        public void SetListener()
        {
            if (State == EAwardState.Undone)
            {
                player.taskDayListener.AddListener(Type, progressChangeAction);
            }
        }

        /// <summary>
        /// 下次刷新时间
        /// </summary>
        public int NextRefreshTime
        {
            get
            {
                if (State == EAwardState.HaveGet && Data.time != null)
                    return ((DateTime)Data.time).AddHours(Glob.config.settingConfig.TaskBountyRefreshTime).ToTimestamp() + 1;
                return 0;
            }
        }

        /// <summary>
        /// 检查是否到刷新时间
        /// </summary>
        /// <returns></returns>
        public bool CheckRefreshTime()
        {
            if (State == EAwardState.HaveGet && DateTime.Now.ToTimestamp() >= NextRefreshTime)
                return true;

            return false;
        }

        /// <summary>
        /// 成就进度发生改变
        /// </summary>
        /// <param name="value">进度值</param>
        private void progressChangeAction(int value,int arg1,int arg2)
        {
            int newPro = Math.Max(Progress, checkAddProgress(value,arg1,arg2));

            Glob.logMgr.LogTask(player.ID, 3, TaskId, Progress);
            if (newPro != Progress)
            {
                setProgress(newPro);
                //任务条件达成，移除监听
                if (State == EAwardState.Done)
                {
                    player.taskDayListener.RemoveListener(Type);                    
                }

                SC_taskBounty_change msg = new SC_taskBounty_change();
                msg.TaskId = TaskId; 
                msg.Progress = Progress;
                player.Session.Send(msg);
            }
        }

        /// <summary>
        /// 任务消息体
        /// </summary>
        /// <returns></returns>
        public One_taskBounty_item GetTaskMsg()
        {
            One_taskBounty_item one = new One_taskBounty_item();
            one.TaskId = TaskId;
            one.Progress = Progress;
            one.IsGet = Data.isGet;
            one.RefreshTime = NextRefreshTime;
            return one;
        }
      
        /// <summary>
        /// 设置任务进度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isSend"></param>
        private void setProgress(int value)
        {
            if (value == Progress)
                return;

            if (Data == null)
            {
                Data = new TTaskBounty(true);
                Data.pId = player.ID;
                Data.taskId = TaskId;
                Data.pro = value;
                Data.Insert();
            }
            else
            {
                Data.pro = value;
                Data.Update();
            }
        }

        /// <summary>
        /// 获取任务奖励
        /// </summary>
        public List<int[]> getAward()
        {
            //List<int[]> weightItems = new List<int[]>();
            //List<int> weightVals = new List<int>();
            //foreach (int[] iteminfo in Config.items)
            //{
            //    weightItems.Add(new int[] { iteminfo[0], iteminfo[1], iteminfo[2] });
            //    weightVals.Add(iteminfo[3]);
            //}

            //List<int[]> awardList = new List<int[]>();
            //int num = RandomHelper.Random(Config.itemNum);
            //int[] item;
            ////权重取奖励要不重复，取一个删一个
            //for (int k = 0; k < num; k++)
            //{
            //    int index = RandomHelper.WeightRandom(weightVals.ToArray());
            //    item = weightItems[index];
            //    awardList.Add(new int[] { item[0], RandomHelper.Random(item[1], item[2]) });
            //    weightItems.RemoveAt(index);
            //    weightVals.RemoveAt(index);
            //}

            List<int[]> awardList = new List<int[]>();
            int awardId = Config.AwardId[0];
            //if(Type == ETaskType.Arena_KillHero)//任务类型4需要按 竞技段位取奖励id
            //{
            //    if (player.Arena != null)
            //        awardId = Config.AwardId[(int)player.Arena.ArenaLevel];
            //}
            if(!Glob.config.dicItemAwardList.TryGetValue(awardId, out ItemAward itemAward))
            {
                return awardList;
            }
            awardList = itemAward.getAllAward(player);
            Data.isGet = true;
            Data.time = DateTime.Now;
            Data.Update();
            
            return awardList;
        }*/
    }
}
