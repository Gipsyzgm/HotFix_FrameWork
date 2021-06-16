using CommonLib;
using CommonLib.Comm.DBMgr;
using PbActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 一个活动任务数据
    /// </summary>
    public class ActivityData : BaseTask<TActivity>
    {
        /// <summary>任务进度</summary>
        public override int Progress => Data.pro;

        /// <summary>任务模型数据</summary>
        public TActivityTask TaskObj { get; protected set; }

        /// <summary>当前任务是否已完成</summary>
        public override bool IsComplete => Data.taskId == 0;

        /// <summary>活动任务Id</summary>
        public int TaskId => Data.taskId;

        /// <summary>任务完成条件数据</summary>
        public override int[] Condition => TaskObj.condition;

        /// <summary>是否已领取</summary>
        public bool IsGet => Data.isGet;

        /// <summary>
        /// 任务类型
        /// </summary>
        public override ETaskType Type => TaskObj == null ? ETaskType.None : (ETaskType)TaskObj.type;

        public ActivityData(Player _player, TActivity data)
        {
            player = _player;
            Data = data;
            if (Glob.activityMgr.ActivityTaskList.ContainsKey(TaskId))
                TaskObj = Glob.activityMgr.ActivityTaskList[TaskId];
        }

        /// <summary>
        /// 设置任务监听
        /// </summary>
        public void SetListener()
        {
            if (!IsComplete)
            {
                if(Type == ETaskType.Connect_Pay)
                    player.activityListener.AddListener(Type, TaskId, ConnectPayAction);
                else
                    player.activityListener.AddListener(Type, TaskId, progressChangeAction);
                //重新计算进度
                int count = recountProgress();
                if (count > 0 && Data != null)
                    setProgress(count);
            }
        }


        private void ConnectPayAction(int val, int value, int arg2)
        {
            DictionarySafe<int, Action<int, int, int>> action = null;

            if (player.activityListener.listenerList.TryGetValue(ETaskType.Connect_Pay,out action))
            {
                Dictionary<int, Action<int, int, int>> vals= action.OrderBy(t => t.Key).ToDictionary(t => t.Key, u => u.Value);      
                //List<int> vals = action.OrderBy(t => t.Key).ToList();
                int num = val;
                foreach (int aid in vals.Keys)
                {
                    if (num > 0)
                    {
                        if (player.activityList.TryGetValue(aid, out ActivityData data))
                        {
                            if (data.Data.pro + num >= data.Condition[0])
                            {
                                num = (data.Data.pro + num) - data.Condition[0];
                                data.setProgress(data.Condition[0]);                              
                            }
                            else {
                              
                                data.setProgress(data.Data.pro + num);
                                num = 0;
                            }
                            if (data.State == EAwardState.Done && data.TaskObj.times != 1)
                                player.activityListener.RemoveListener(data.Type, data.TaskId);
                            SC_Activity_Change msg = new SC_Activity_Change();
                            msg.TaskId = data.TaskId;
                            msg.Progress = data.Progress;
                            msg.Mid = data.TaskObj.mid;
                            player.Send(msg);
                            Logger.Log(msg.TaskId, msg.Progress, data.State);
                        }
                    }                    
                }                
            }
        }

        /// <summary>
        /// 活动进度发生改变
        /// </summary>
        /// <param name="value">进度值</param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private void progressChangeAction(int value, int arg1, int arg2)
        {
            int newPro = 0;
            int pro = checkAddProgress(value, arg1, arg2);
            newPro = Math.Max(Progress, pro);
            if (newPro != Progress)
            {
                setProgress(newPro);
                //任务条件达成，移除监听
                if (State == EAwardState.Done  && TaskObj.times != 1)
                    player.activityListener.RemoveListener(Type, TaskId);

                SC_Activity_Change msg = new SC_Activity_Change();
                msg.TaskId = TaskId;
                msg.Progress = Progress;
                msg.Mid = TaskObj.mid;
                player.Session.Send(msg);
                Logger.Log(msg.TaskId, msg.Progress, State);
                //向客户端发送进度
                //Logger.Log($"进度发生变化 活动任务Id:{TaskId} 当前进度({Progress}/{TaskObj.condition[0]})");
            }
        }


        /// <summary>
        /// 设置活动进度
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
        /// 当前任务消息
        /// </summary>
        /// <returns></returns>
        public One_Activity_Task GetTaskMsg()
        {
            One_Activity_Task one = new One_Activity_Task();
            one.MID = TaskObj.mid;      
            one.Condition.Add(TaskObj.condition);
            one.TackId = TaskObj.taskId;
            one.Progress = Progress;
            one.OrderId = TaskObj.orderid;
            one.Type = TaskObj.type;
            one.IsGet = Data.isGet;
            foreach (int[] list in TaskObj.award)
            {
                One_Activity_Award award = new One_Activity_Award();
                award.ItemId = list[0];
                award.Count = list[1];
                one.Award.Add(award);
            }
            
            return one;
        }

        /// <summary>
        /// 领取活动奖励
        /// </summary>
        public void GetAward()
        {
            if (State != EAwardState.Done)
            {
                Logger.LogWarning($"当前活动不可领奖");
                return;
            }
            if(IsGet && TaskObj.times != 1)
            {
                Logger.LogWarning($"当前活动不可重复领奖");
                return;
            }

            ////兑换活动，需要扣除道具
            //if((ETaskType)TaskObj.type == ETaskType.Item_Collect)
            //{
            //    if (Condition[1] != 0)
            //        player.DeductItemPropNum(Condition[1], Condition[0]);
            //    if(Condition[3] != 0)
            //        player.DeductItemPropNum(Condition[3], Condition[2]);
            //    Data.pro = 0;
            //}
            //else
            //{
                Data.isGet = true;
            //}

            Glob.itemMgr.PlayerAddNewItems(player, TaskObj.award, true);

            Data.Update();

            SC_Activity_Get sendMsg = new SC_Activity_Get();
            sendMsg.TaskId = TaskId;
            sendMsg.Progress = Progress;
            sendMsg.Mid = TaskObj.mid;
            sendMsg.IsGet = IsGet;
            player.Send(sendMsg);

    
        }
        
    }
}
