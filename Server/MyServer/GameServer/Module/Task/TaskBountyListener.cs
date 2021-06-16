using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 赏金任务监听器
    /// </summary>
    public class TaskBountyListener
    {
        DictionarySafe<ETaskType,  Action<int, int, int>> listenerList = new DictionarySafe<ETaskType,  Action<int, int, int>>();
                
        /// <summary>
        /// 增加监听
        /// </summary>
        /// <param name="taskType">任务类型</param>
        /// <param name="action">触发行为</param>
        public void AddListener(ETaskType taskType, Action<int, int, int> action)
        {
            listenerList.AddOrUpdate(taskType, action);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="taskType"></param>
        public void RemoveListener(ETaskType taskType)
        {
            listenerList.Remove(taskType);
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
            Action<int, int, int> act;
            if (listenerList.TryGetValue(taskType, out act))
                act(addPro, arg1, arg2);
        }

        public void Dispose()
        {
            listenerList.Clear();
            listenerList = null;
        }
    }
}
