using PbBonus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    /// <summary>
    /// 玩家新手活动任务
    /// </summary>
    public class TaskNewbieData : BaseTask<TTaskNewbie>
    {
        public int TaskId => Config.id;

        /// <summary>任务进度</summary>
        public override int Progress => Data == null ? 0 : Data.pro;

       // public TaskNewbieConfig Config { get; protected set; }

        public override int[] Condition => Config.condition;

        /// <summary>
        /// 任务类型
        /// </summary>
        public override ETaskType Type => (ETaskType)Config.type;

        public TaskNewbieData(Player _player, TTaskNewbie data, TaskNewbieConfig config)
        {
            player = _player;
            Config = config;
            Data = data;
            if (Data == null)
            {
                Data = new TTaskNewbie(true);
                Data.pId = player.ID;
                Data.taskId = TaskId;
                Data.pro = 0;
                Data.Insert();
            }
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
                player.taskNewbieListener.AddListener(Type, TaskId, progressChangeAction);
                //重新计算进度
                int count = recountProgress();
                if (count > 0 && Data != null)
                    setProgress(count);
            }
        }

        public void RemoveListener()
        {
            player.taskNewbieListener.RemoveListener(Type, TaskId);
        }

        /// <summary>
        /// 成就进度发生改变
        /// </summary>
        /// <param name="value">进度值</param>
        private void progressChangeAction(int value, int arg1, int arg2)
        {
            int newPro = Math.Max(Progress, checkAddProgress(value, arg1, arg2));
            
            if (newPro != Progress)
            {
                setProgress(newPro);
                Glob.logMgr.LogTask(player.ID, 1, TaskId, Progress);
                //任务条件达成，移除监听
                if (State == EAwardState.Done)
                {
                    player.taskNewbieListener.RemoveListener(Type, TaskId);                    
                }
                SC_taskNewbie_change msg = new SC_taskNewbie_change();
                msg.TaskId = TaskId;
                msg.Progress = Progress;
                player.Session.Send(msg);
            }
        }

        /// <summary>
        /// 任务消息体
        /// </summary>
        /// <returns></returns>
        public One_TaskNewbie GetTaskMsg()
        {
            One_TaskNewbie one = new One_TaskNewbie();
            one.TaskId = TaskId;
            one.Progress = Progress;
            one.IsGet = Data.isGet;
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

            Data.pro = value;
            Data.Update();
        }

        /// <summary>
        /// 设置任务完成
        /// </summary>
        public void SetComplete()
        {
            Data.isGet = true;
            Data.Update();
        }
    }*/
}
