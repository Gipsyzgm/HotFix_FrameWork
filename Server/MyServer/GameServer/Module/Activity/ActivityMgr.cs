using CommonLib;
using CommonLib.Comm.DBMgr;
using MongoDB.Driver;
using PbActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 活动管理器
    /// </summary>
    public class ActivityMgr
    {
        /// <summary>当前正在开放的活动  活动ID  活动对象  每天检测 </summary>
        public DictionarySafe<int, ActivityOpen> ActivityOpenList = new DictionarySafe<int, ActivityOpen>();
     
        /// <summary>活动任务数据库数据  任务ID 任务</summary>
        public DictionarySafe<int, TActivityTask> ActivityTaskList = new DictionarySafe<int,TActivityTask>();

        /// <summary> 活动任务数据库数据 活动ID 任务list </summary>
        DictionarySafe<int, DictionarySafe<int, TActivityTask>> ActivityTasksList = new DictionarySafe<int, DictionarySafe<int, TActivityTask>>();
      
        /// <summary>当前开放的活动任务  任务ID 任务</summary>
        public DictionarySafe<int, TActivityTask> ActivityOpenTaskList = new DictionarySafe<int, TActivityTask>();

        /// <summary>可开放的活动ID, 批次ID</summary>
        public DictionarySafe<int, DateTime?> OpenActivity = new DictionarySafe<int, DateTime?>();


        
        public ActivityMgr()
        {
            UpdateActivity(true);
           
        }

        /// <summary>
        /// 检测活动结束和开放  每天检测
        /// </summary>       
        public void CheckOpen(bool init=false)
        {
            bool isUpdate = false;
            List<int> removeIds = new List<int>();
            //删除已过期的活动
            foreach (ActivityOpen act in ActivityOpenList.Values)
            {
                if (!act.IsOpen)  // 删除过期
                {
                    removeIds.Add(act.ActId);
                    continue;
                }
                
                if (OpenActivity.ContainsKey(act.ActId))        //判断开启的活动ID 
                {
                    if (OpenActivity[act.ActId] != act.Mark)    // 删除不是同一批
                    {
                        removeIds.Add(act.ActId);
                        continue;
                    }
                }
                else {
                    removeIds.Add(act.ActId);
                    continue;
                }                               
            }
            foreach(int actId in removeIds)
            {
                ActivityOpenList.Remove(actId);
            }
            if (removeIds.Count > 0)
                isUpdate = true;

            foreach(DictionarySafe<int, TActivityTask> tDic in ActivityTasksList.Values) {
                ActivityOpen open = new ActivityOpen(tDic);
                if (ActivityOpenList.ContainsKey(open.ActId))   //已存在不添加
                    continue;
                if (!open.IsOpen)                               //时间未到不开启
                    continue;
                ActivityOpenList.Add(open.ActId, open);
                isUpdate = true;
            }

            if (isUpdate)  //有更新通知客户端
            {
                ActivityOpenTaskList.Clear();   //更新开放活动任务 
                OpenActivity.Clear();           //更新开放活动ID 
                foreach (ActivityOpen open in ActivityOpenList.Values)
                {
                    OpenActivity.Add(open.ActId, open.Mark);
                    foreach (TActivityTask t in open.TaskList.Values)
                        ActivityOpenTaskList.Add(t.taskId, t);
                }
                if (!init)
                {
                    foreach (Player player in Glob.playerMgr.onlinePlayerList.Values)
                    {
                        player.SendActivityUpdateMsg();
                        //GetMsg(player);
                    }
                }
                
            }            
        }

        /// <summary>
        /// 更新活动 重新读取数据
        /// </summary>
        public void UpdateActivity(bool init = false)
        {
            ActivityTaskList.Clear();
            ActivityTasksList.Clear();
            OpenActivity.Clear();
            ActivityOpenTaskList.Clear();

            List<TActivityTask> list2 = MongoDBHelper.Instance.Select<TActivityTask>(null);
            list2.Sort((a, b) => a.taskId.CompareTo(b.taskId));
            foreach (TActivityTask task in list2)
            {
                if (!ActivityTaskList.ContainsKey(task.taskId))  //所有任务
                    ActivityTaskList.Add(task.taskId, task);
                if (DateTime.Now < task.startTime || DateTime.Now > task.endTime)
                    continue;
                if (!ActivityOpenTaskList.ContainsKey(task.taskId)) //开放任务
                    ActivityOpenTaskList.Add(task.taskId, task);
                
                if (!OpenActivity.ContainsKey(task.actId)) // 开放的活动 批次
                    OpenActivity.Add(task.actId,task.Mark);               
            }

            foreach (TActivityTask t in ActivityTaskList.Values) //数据库 活动 任务list
            {
                if (ActivityTasksList.ContainsKey(t.actId))
                {
                    ActivityTasksList[t.actId].Add(t.taskId, t);
                }
                else
                {
                    DictionarySafe<int, TActivityTask> taskList = new DictionarySafe<int, TActivityTask>();
                    taskList.Add(t.taskId, t);
                    ActivityTasksList.Add(t.actId, taskList);
                }
            }


            //清掉在线玩家身上  已经不存在的活动任务数据    
            foreach (Player player in Glob.playerMgr.onlinePlayerList.Values)
            {
                player.delNoOpenActivity();
            }
            CheckOpen(init);

        }

        /// <summary>
        /// 获取当前活动
        /// </summary>
        /// <returns></returns>
        public void GetMsg(Player player)
        {
            player.CheckActivity();
            SC_Activity_Info sendMsg = new SC_Activity_Info();          
            foreach (ActivityOpen activity in ActivityOpenList.Values)
            {
                
                if (!activity.IsOpen)
                    continue;
                One_Activity actOne = new One_Activity();
                actOne.MID = activity.MID;
                actOne.AID = activity.ActId;
                actOne.Start = activity.defActivity.startTime.ToTimestamp();
                actOne.End = activity.defActivity.endTime.ToTimestamp();
                foreach (int taskId in activity.TaskList.Keys)
                {
                    Logger.Log("SEND" + taskId);
                    ActivityData task = player.activityList[taskId];
                    One_Activity_Task taskOne = task.GetTaskMsg();
                    actOne.TaskList.Add(taskOne);
                }
                sendMsg.ActivityList.Add(actOne);
            }
            player.Send(sendMsg);
        }




        /// <summary>
        /// 重置每日活动
        /// </summary>
        //public void ResetDailyActiivty()
        //{
        //    //重置在线玩家的每日活动
        //    foreach (Player player in Glob.playerMgr.onlinePlayerList.Values)
        //    {
        //        foreach (ActivityData act in player.activityList.Values)
        //        {
        //            if (act.TaskObj.daily == 1)
        //            {
        //                act.Data.pro = 0;
        //                act.Data.isGet = false;
        //            }
        //        }
        //        player.SendActivityUpdateMsg();
        //    }
        //    List<int> dailyTaskIds = new List<int>();
        //    TActivityTask task;
        //    //foreach(int taskId in TaskOpenList.Keys)
        //    //{
        //    //if (!ActivityTaskList.TryGetValue(taskId, out task))
        //    //    continue;
        //    //if (task.daily == 1)
        //    //    dailyTaskIds.Add(taskId);
        //    //}
        //    //更新重置所有玩家的每日活动
        //    if (dailyTaskIds.Count > 0)
        //    {
        //        FilterDefinition<TActivity> filter = Builders<TActivity>.Filter.In(t => t.taskId, dailyTaskIds);
        //        UpdateDefinition<TActivity> update = Builders<TActivity>.Update.Set(t => t.pro, 0).Set(t => t.isGet, false);
        //        MongoDBHelper.Instance.UpdateMany<TActivity>(filter, update);
        //    }
        //}

    }
}
