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
    public partial class Player
    {
        /// <summary>
        /// 角色当前活动任务列表
        /// </summary>
        public DictionarySafe<int, ActivityData> activityList = new DictionarySafe<int, ActivityData>();
        
        /// <summary>
        /// 活动检测监听
        /// </summary>
        public ActivityListener activityListener = new ActivityListener();

        /// <summary>
        /// 给角色创建新活动任务
        /// </summary>
        /// <param name="taskId">活动任务id</param>
        /// <returns></returns>
        public ActivityData ActivityNew(int taskId, TActivity data = null)
        {
            if (data == null)
            {
                data = new TActivity(true);
                data.pid = ID;
                data.taskId = taskId;
                data.pro = 0;
                data.isGet = false;
                if (Glob.activityMgr.ActivityOpenTaskList.TryGetValue(taskId, out TActivityTask t))
                    data.Mark = t.Mark;
                data.Insert();
            }
            ActivityData activity = new ActivityData(this, data);
            activityList.Add(taskId, activity);
            Logger.Log("LOAD:" + taskId);
            activity.SetListener();
            return activity;
        }

        /// <summary>
        /// 填充活动数据  
        /// </summary>
        /// <param name="tList"></param>
        public void SetActivityList(List<TActivity> tList)
        {
            if (tList == null)
                return;
            foreach (TActivity act in tList)
            {
                if (Glob.activityMgr.ActivityOpenTaskList.ContainsKey(act.taskId))
                    ActivityNew(act.taskId, act);
                else
                    act.Delete();
              
            }
            CheckActivity();
        }

        /// <summary>
        /// 检测活动是否到期    
        /// </summary>
        public void CheckActivity()
        {           
            //删除过期的活动
            List<int> removeIds = new List<int>();

            //foreach (TActivityTask t in Glob.activityMgr.ActivityOpenTaskList.Values)
            //{
            //    if (activityList.TryGetValue(t.taskId, out ActivityData val))
            //    {
            //        if (val.Data.Mark != t.Mark)
            //            removeIds.Add(val.TaskId);
            //    }
            //    else {
            //        removeIds.Add(t.taskId);
            //    }
            //}
            foreach(ActivityData activity in activityList.Values)
            {
                if (Glob.activityMgr.ActivityOpenTaskList.TryGetValue(activity.TaskId, out TActivityTask val))  //删除玩家身上，未开启的活动
                {
                    if (activity.Data.Mark != val.Mark)
                        removeIds.Add(activity.TaskId);
                }
                else
                    removeIds.Add(activity.TaskId);
            }


            foreach(int taskId in removeIds)
            {
                ActivityData act;
                if (!activityList.TryGetValue(taskId, out act))
                    continue;
                activityListener.RemoveListener(act.Type, taskId);
                act.Data.Delete();
                activityList.Remove(taskId);
            }

            //增加新开放的活动
            foreach(int taskId in Glob.activityMgr.ActivityOpenTaskList.Keys)
            {
                if(!activityList.ContainsKey(taskId))
                {
                    ActivityData activity = ActivityNew(taskId);
                    activityList.Add(taskId, activity);
                }
            }
            foreach (ActivityData data in activityList.Values)
                Logger.Log("player:" + data.TaskId);
        }

        /// <summary>
        /// 删除玩家身上不存在开放任务中的任务
        /// </summary>
        /// <param name="openList">开放的活动任务id</param>
        public void delNoOpenActivity()
        {
            //删除不存在的活动
            List<int> removeIds = new List<int>();
            foreach (ActivityData activity in activityList.Values)
            {
                if (Glob.activityMgr.ActivityOpenTaskList.TryGetValue(activity.TaskId,out TActivityTask task))
                {
                    if (activity.Data.Mark != task.Mark)
                        removeIds.Add(activity.TaskId);
                }
                else
                {
                    removeIds.Add(activity.TaskId);
                }                         
            }
                
            foreach (int taskId in removeIds)
            {
                ActivityData act;
                if (!activityList.TryGetValue(taskId, out act))
                    continue;
                activityListener.RemoveListener(act.Type, taskId);
                act.Data.Delete();
                activityList.Remove(taskId);
            }
        }

        /// <summary>
        /// 消除所有活动任务的监听
        /// </summary>
        public void ClearAllListener()
        {
            foreach(ActivityData act in activityList.Values)
            {
                activityListener.RemoveListener(act.Type, act.TaskId);
            }
        }

        /// <summary>
        /// 向玩家推送活动列表需要更新的消息
        /// </summary>
        public void SendActivityUpdateMsg()
        {
            SC_Activiyt_Updata msg = new SC_Activiyt_Updata();
            Send(msg);
            Logger.Log("SC_Activiyt_Updata");
        }

        
    }
}
