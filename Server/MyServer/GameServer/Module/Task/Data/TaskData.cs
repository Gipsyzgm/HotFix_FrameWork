using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbTask;

namespace GameServer.Module
{
    /*
    public class TaskData: BaseTask<TTask>
    {

        /// <summary>任务进度</summary>
        public override int Progress =>  Data.pro;

       // public TaskConfig Config { get; set; }

        /// <summary>任务所在线</summary>
        public int Line { get; private set; }

        /// <summary>任务所在线索引</summary>
        public int Index=> Data.index;
        /// <summary>
        /// 当前任务线是否已完成
        /// </summary>
        public override bool IsComplete => Data.taskId == 0;

        public int TaskId => Data.taskId;

        //public override int[] Condition => Config.condition;

        /// <summary>
        /// 任务类型
        /// </summary>
        //public override ETaskType Type => Config == null ? ETaskType.None : (ETaskType)Config.type;

        //public TaskData(Player _player, TTask data)
        //{
        //    player = _player;
        //    Data = data;
        //    Line = data.line;
        //    if (TaskId != 0)
        //    {
        //        TaskConfig config;
        //        if (Glob.config.dicTask.TryGetValue(TaskId, out config))
        //            Config = config;
        //    }
        //}
        
        /// <summary>
        /// 设置任务监听
        /// </summary>
        //public void SetListener()
        //{
        //    if (!IsComplete)
        //    {
        //        player.taskListener.AddListener(Type, Line,progressChangeAction);
        //        //重新计算进度
        //        int count = recountProgress();
        //        if (count > 0 && Data != null)
        //            setProgress(count);
        //    }
        //}

        /// <summary>
        /// 成就进度发生改变
        /// </summary>
        /// <param name="value">进度值</param>
        //private void progressChangeAction(int value,int arg1,int arg2)
        //{
        //    if (Config == null || Config.level > player.Level) return;

        //    int newPro = Math.Max(Progress, checkAddProgress(value,arg1,arg2));
        //    if (newPro != Progress)
        //    {
        //        setProgress(newPro);
        //        Glob.logMgr.LogTask(player.ID, 2, TaskId, Progress);
        //        //任务条件达成，移除监听
        //        if (State == EAwardState.Done)
        //        {
        //            player.taskListener.RemoveListener(Type, Line);                    
        //        }
                
        //        SC_taskLine_change msg = new SC_taskLine_change();
        //        msg.Task = GetTaskMsg();
        //        player.Session.Send(msg);



        //        //向客户端发送进度
        //        //Logger.Log($"进度发生变化 任务Id:{Config.id} 当前进度({Progress}/{Config.condition[0]})");    
        //    }
        //}

        public One_taskLine_item GetTaskMsg()
        {
            One_taskLine_item one = new One_taskLine_item();
            one.TaskId = TaskId;
            one.Line = Line;
            one.Progress = Progress;
            one.Index = Index;
            return one;
        }

        /// <summary>
        /// 设置任务完成
        /// </summary>
        //public void SetComplete()
        //{
        //    List<TaskConfig> lineTask;
        //    Data.index += 1;
        //    Data.taskId = 0;
        //    Data.pro = 0;
        //    if (Glob.config.dicTaskLine.TryGetValue(Line, out lineTask))  //整体任务线上的任务都被删除了!!!!!!
        //    {
        //        if (Data.index < lineTask.Count)
        //        {
        //            Config = lineTask[Data.index];
        //            Data.taskId = Config.id;
        //            Data.pro = 0; //重置进度
        //            SetListener();
        //        }
        //    }
        //    Data.Update();
        //}

        /// <summary>
        /// 设置成就进度
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
    }*/
}
