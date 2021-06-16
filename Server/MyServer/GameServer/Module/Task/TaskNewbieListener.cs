using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 新手任务监听器
    /// </summary>
    public class TaskNewbieListener
    {
        DictionarySafe<ETaskType, DictionarySafe<int, Action<int, int, int>>> listenerList = new DictionarySafe<ETaskType, DictionarySafe<int, Action<int, int, int>>>();

        /// <summary>
        /// 增加监听
        /// </summary>
        /// <param name="taskType">任务类型</param>
        /// <param name="taskId">任务编号</param>
        /// <param name="action">触发行为</param>
        public void AddListener(ETaskType taskType, int taskId, Action<int, int, int> action)
        {
            DictionarySafe<int, Action<int, int, int>> typeList;
            if (!listenerList.TryGetValue(taskType, out typeList))
            {
                typeList = new DictionarySafe<int, Action<int, int, int>>();
                listenerList.Add(taskType, typeList);
            }
            typeList.AddOrUpdate(taskId, action);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="taskType"></param>
        public void RemoveListener(ETaskType taskType, int taskId)
        {
            DictionarySafe<int, Action<int, int, int>> typeList;
            if (listenerList.TryGetValue(taskType, out typeList))
            {
                typeList.Remove(taskId);
            }
        }

        /// <summary>
        /// 触发任务类型
        /// </summary>
        /// <param name="taskType">任务活动类型</param>
        /// <param name="addValue">增加值(条件参数0)</param>
        /// <param name="arg2">条件参数1</param>
        /// <param name="arg3">条件参数2</param>
        public void Dispatch(ETaskType taskType, int addPro = 1, int arg1 = -1, int arg2 = -1)
        {
            DictionarySafe<int, Action<int, int, int>> typeList;
            if (listenerList.TryGetValue(taskType, out typeList))
            {
                foreach (Action<int, int, int> act in typeList.Values)
                    act(addPro, arg1, arg2);
            }
        }

        public void Dispose()
        {
            listenerList.Clear();
            listenerList = null;
        }
    }
}
