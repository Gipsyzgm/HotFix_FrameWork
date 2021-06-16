using PbTask;
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
        /// 任务线列表
        /// </summary>
        //public DictionarySafe<int, TaskData> taskList = new DictionarySafe<int, TaskData>();

        ///// <summary>
        ///// 任务线数据
        ///// </summary>
        //public DictionarySafe<int, TTask> taskLineDataList = new DictionarySafe<int, TTask>();

        ///// <summary>
        ///// 任务监听
        ///// </summary>
        //public TaskListener taskListener = new TaskListener();

        /// <summary>
        /// 触发任务类型
        /// </summary>
        /// <param name="taskType">任务活动类型</param>
        /// <param name="addValue">增加值(条件参数0)</param>
        /// <param name="arg2">条件参数1</param>
        /// <param name="arg3">条件参数2</param>
        //public void TriggerTask(ETaskType taskType, int addPro = 1, int arg1 = -1, int arg2 = -1)
        //{

        //    taskListener.Dispatch(taskType, addPro, arg1, arg2);
        //    activityListener.Dispatch(taskType, addPro, arg1, arg2);
        //    //taskDayListener.Dispatch(taskType, addPro, arg1, arg2);
        //    //taskHeroicListener.Dispatch(taskType, addPro, arg1, arg2);
        //    //taskNewbieListener.Dispatch(taskType, addPro, arg1, arg2);
        //}

        //public TaskData TaskOpenNewLine(int line)
        //{
        //    List<TaskConfig> list;
        //    if (!Glob.config.dicTaskLine.TryGetValue(line, out list) || list.Count == 0)
        //        return null;

        //    TTask data = new TTask(true);
        //    data.pId = ID;
        //    data.line = line;
        //    data.index = 0;
        //    data.pro = 0;
        //    data.taskId = list[0].id;
        //    data.Insert();
        //    taskLineDataList.Add(line, data);
        //    TaskData task = new TaskData(this, data);
        //    taskList.Add(line, task);
        //    task.SetListener();
        //    return task;
        //}

        /// <summary>
        /// 设置任务列表
        /// </summary>
        /// <param name="dataList"></param>
        //public void SetTaskList(List<TTask> dataList)
        //{
        //    //任务线数据
        //    if (dataList != null)
        //    {
        //        foreach (TTask data in dataList)
        //            taskLineDataList.AddOrUpdate(data.line, data);
        //    }
        //    if (dataList == null)
        //    {
        //        TaskOpenNewLine(0);
        //        return;
        //    }

        //    foreach (KeyValuePair<int, List<TaskConfig>> keyVal in Glob.config.dicTaskLine)
        //    {
        //        TaskData task = null;
        //        TTask data;
        //        if (taskLineDataList.TryGetValue(keyVal.Key, out data))
        //        {
        //            task = new TaskData(this, data);
        //            taskList.Add(keyVal.Key, task);
        //        }
        //        else
        //        {
        //            //判断线是否开启
        //            if (Glob.taskMgr.CheckLineIsOpen(this, keyVal.Key))
        //                task = TaskOpenNewLine(keyVal.Key);
        //        }
        //        if (task == null)
        //            continue;

        //        int index;
        //        if (!Glob.config.dicTaskLineIndex.TryGetValue(task.Data.taskId, out index))
        //        {
        //            //任务被策划大大删除了！！！！,跟据数据的index重新指定一个任务Id                
        //            if (task.Data.index < keyVal.Value.Count)
        //            {
        //                task.Data.taskId = keyVal.Value[task.Data.index].id;
        //                task.Data.pro = 0; //重置进度
        //                task.Config = keyVal.Value[task.Data.index];
        //            }
        //            else
        //                task.Data.taskId = 0;
        //        }
        //        else
        //            task.Data.index = index;  //重新设置索引编号
        //        task.SetListener();
        //    }
        //}


        /////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 赏金任务监听
        /// </summary>
        // public TaskBountyListener taskDayListener = new TaskBountyListener();
        /// <summary>
        /// 赏金任务列表
        /// </summary>
        // public DictionarySafe<int, TaskBountyData> taskBountyList = new DictionarySafe<int, TaskBountyData>();

        /// <summary>
        /// 设置赏金任务列表
        /// </summary>
        /// <param name="dataList"></param>
        //public void SetTaskDayList(List<TTaskBounty> dataList)
        //{
        //    if (dataList != null && dataList.Count > 0)
        //    {
        //        List<int> expireList = new List<int>();
        //        List<int> kindList = new List<int>() { 1, 2, 3 };//还没有出现的类型集合
        //        foreach (TTaskBounty task in dataList)
        //        {
        //            Glob.config.dicTaskBounty.TryGetValue(task.taskId, out TaskBountyConfig config);
        //            TaskBountyData item = new TaskBountyData(this, task, config);
        //            taskBountyList.Add(item.Config.id, item);
        //            item.SetListener();
        //            if (item.CheckRefreshTime() && item.State == EAwardState.HaveGet)
        //                expireList.Add(task.taskId);
        //        }
        //        //刷新到期且已领取的任务
        //        foreach (int taskId in expireList)
        //        {
        //            int kind = taskId / 100;
        //            kindList.Remove(kind);
        //            RefreshOneTaskBounty(kind);
        //        }
        //    }
        //    else
        //    {
        //        RefreshOneTaskBounty(1);
        //        RefreshOneTaskBounty(2);
        //        RefreshOneTaskBounty(3);
        //    }
        //}

        /// <summary>
        /// 检查是否需要开启新种类的赏金任务
        /// </summary>
        //public void CheckOpenBountyKind()
        //{
        //    List<int> kindList = new List<int>() { 1, 2, 3 };//还没有出现的类型集合
        //    foreach(TaskBountyData item in taskBountyList.Values)
        //    {
        //        if (kindList.Contains(item.Config.kind))
        //            kindList.Remove(item.Config.kind);
        //    }
        //    if (kindList.Count > 0)
        //    {
        //        foreach (int kind in kindList)
        //            RefreshOneTaskBounty(kind);
        //    }
        //    kindList.Clear();
        //    kindList = null;

        //    if(taskBountyList.Count > 0)//推送最新的赏金任务列表
        //    {
        //        SC_taskLine_list msg = new SC_taskLine_list();
        //        foreach (TaskBountyData item in taskBountyList.Values)
        //        {
        //            if (item.Data != null)
        //                msg.TaskBountyList.Add(item.GetTaskMsg());
        //        }
        //        Send(msg);
        //    }
        //}

        /// <summary>
        /// 刷新一个类型的赏金任务
        /// </summary>
        /// <param name="type"></param>
        //public TaskBountyData RefreshOneTaskBounty(int type)
        //{
        //    if(Glob.taskMgr.TaskBountyTypeList.TryGetValue(type, out List<TaskBountyConfig> taskList))
        //    {
        //        List<int> ids = new List<int>();
        //        List<int> weights = new List<int>();
        //        foreach (TaskBountyConfig task in taskList)
        //        {
        //            if (task.openLevel <= Level)
        //            {
        //                ids.Add(task.id);
        //                weights.Add(task.weight);
        //            }
        //        }
        //        if (ids.Count == 0)
        //            return null;

        //        int index = RandomHelper.WeightRandom(weights, weights.Count);
        //        TaskBountyConfig config = taskList[index];

        //        int delId = 0;
        //        foreach(var kv in taskBountyList)
        //        {
        //            if (kv.Key / 100 == type)
        //                delId = kv.Key;
        //        }
        //        if (delId != 0)
        //        {
        //            TaskBountyData task = taskBountyList[delId];
        //            taskDayListener.RemoveListener(task.Type);
        //            task.Data.Delete();
        //            taskBountyList.Remove(delId);
        //        }
        //        TaskBountyData item = new TaskBountyData(this, config);
        //        taskBountyList.Add(item.Config.id, item);
        //        item.SetListener();
        //        return item;
        //    }
        //    return null;
        //}
    }
}
