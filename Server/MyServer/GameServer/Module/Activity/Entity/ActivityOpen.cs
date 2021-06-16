using CommonLib;
using CommonLib.Comm.DBMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 活动开放的内容对象
    /// </summary>
    public class ActivityOpen
    {
        /// <summary>活动id</summary>
        public int ActId { get; set; }        
        /// <summary>
        /// 开启时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int MID { get; set; }
        /// <summary>
        /// 批次标记
        /// </summary>
        public DateTime? Mark { get; set; }
        public TActivityTask defActivity { get; set; }

        /// <summary>活动任务列表</summary>
        public DictionarySafe<int, TActivityTask> TaskList = new DictionarySafe<int, TActivityTask>();
        public ActivityOpen(DictionarySafe<int, TActivityTask> list)
        {           
            foreach (TActivityTask task in list.Values)
                TaskList.Add(task.taskId, task);

            if (list.Values.Count > 0)
            {
                defActivity = list.Values.ElementAt(0);
                ActId = defActivity.actId;
                MID = defActivity.mid;
                StartTime = defActivity.startTime;
                EndTime = defActivity.endTime;
                Mark = defActivity.Mark;
            }
        }
        /// <summary>是否开放</summary>
        public bool IsOpen
        {
            get
            {
                if (StartTime == null || EndTime == null)
                    return false;
                if (DateTime.Now > StartTime && DateTime.Now < EndTime)
                {
                    return true;
                }
                return false;
            }
        }
        
    }
}
