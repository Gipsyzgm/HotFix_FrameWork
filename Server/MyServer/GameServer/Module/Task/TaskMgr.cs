using PbBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PbTask;

namespace GameServer.Module
{
    
    public class TaskMgr
    {
        /// <summary>
        /// 赏金任务类型集合
        /// </summary>
        //public DictionarySafe<int, List<TaskBountyConfig>> TaskBountyTypeList = new DictionarySafe<int, List<TaskBountyConfig>>();

        public TaskMgr()
        {
            //foreach (var kv in Glob.config.dicTaskBounty)
            //{
            //    if (TaskBountyTypeList.ContainsKey(kv.Value.kind))
            //        TaskBountyTypeList[kv.Value.kind].Add(kv.Value);
            //    else
            //    {
            //        List<TaskBountyConfig> list = new List<TaskBountyConfig>();
            //        list.Add(kv.Value);
            //        TaskBountyTypeList.Add(kv.Value.kind, list);
            //    }
            //}
        }

        /// <summary>
        /// 发送任务信息
        /// </summary>
        /// <param name="player"></param>
        public void SendTaskInfo(Player player)
        {
            SC_taskLine_list msg = new SC_taskLine_list();
            //任务列表信息
         

            player.Send(msg);
        }

        /// <summary>
        /// 增加任务进度
        /// </summary>
        /// <param name="player"></param>
        public void AddTaskPro(Player player,int id, int num, bool isSend = true, ETaskDay eTaskDay = ETaskDay.None)
        {
          
            if (isSend)
                SendTaskInfo(player);

        }
   
        /// <summary>
        /// 根据任务类型获取任务id
        /// </summary>
        public List<int> GetTaskIdByType(ETaskDay eTaskDay)
        {
            List<int> ids = new List<int>();
            foreach(var p in Glob.config.dicTaskDay)
            {
                if (p.Value.type == (int)eTaskDay)
                    ids.Add(p.Value.id);
            }
            return ids;
        }
        /// <summary>
        /// 重置每日任务
        /// </summary>
        public void ResetTaskDay(Player player,bool IsSend = true)
        {
           
            if(IsSend)
                SendTaskInfo(player);

        }

    }
}
