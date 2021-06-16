using MongoDB.Bson;
using PbBonus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public partial class Player
    {
        /*
        /// <summary>
        /// 赏金任务监听
        /// </summary>
        public TaskNewbieListener taskNewbieListener = new TaskNewbieListener();
        /// <summary>
        /// 赏金任务列表
        /// </summary>
        public DictionarySafe<int, TaskNewbieData> taskNewbieList = new DictionarySafe<int, TaskNewbieData>();

        /// <summary>
        /// 设置新手任务列表
        /// </summary>
        /// <param name="dataList"></param>
        public void SetTaskNewbieList(List<TTaskNewbie> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                foreach (TTaskNewbie task in dataList)
                {
                    Glob.config.dicTaskNewbie.TryGetValue(task.taskId, out TaskNewbieConfig config);
                    TaskNewbieData item = new TaskNewbieData(this, task, config);
                    taskNewbieList.Add(item.Config.id, item);
                    item.SetListener();
                }
            }
            CheckTaskNewbie();
        }

        /// <summary>
        /// 根据注册日期检查是否有新的任务开启
        /// </summary>
        public void CheckTaskNewbie(bool isSend=false)
        {
            if (bonusData != null && bonusData.newbieOver)
                return;
            int day = Glob.timerMgr.GetAcrossDay((DateTime)AccountData.regDate) + 1;

            //注册时间20点之前进来的玩家按正常算，20点之后进来的，持续时间加1天
            int sum = Glob.config.bonusSettingsConfig.TaskNewbieExpires;
            if (((DateTime)AccountData.regDate).Hour >= Glob.config.bonusSettingsConfig.TaskNewbieRegHour)
                sum = sum + 1;

            if (day >= sum)//超过注册后第7天，第8天的时候清空所有任务，并发邮件给已完成未领奖的任务奖励
            {
                bonusData.newbieOver = true;
                bonusData.Update();
                OverdueAward();//处理未领奖励
                foreach (var task in taskNewbieList.Values)
                {
                    task.RemoveListener();
                    task.Data.Delete();
                }
                taskNewbieList.Clear();
                return;
            }
            day = Math.Min(7, day);
            List<TaskNewbieConfig> tempList = Glob.config.dicTaskNewbie.Values.Where(t => t.day <= day).ToList();
            bool haveNew = false;
            foreach (var conf in tempList)
            {
                if (!taskNewbieList.ContainsKey(conf.id))
                {
                    TaskNewbieData item = new TaskNewbieData(this, null, conf);
                    taskNewbieList.Add(item.Config.id, item);
                    item.SetListener();
                    haveNew = true;
                }
            }
            if(isSend && haveNew)
            {
                Glob.bonusMgr.SendTashNewbieInfo(this);
            }
        }

        /// <summary>
        /// 发送新手任务信息
        /// </summary>
        public void SendTashNewbieInfo()
        {
            SC_taskNewbie_list msg = new SC_taskNewbie_list();
            msg.IsNewbieComplete = bonusData == null ? false : bonusData.newbieOver;
            
            foreach (var task in taskNewbieList.Values)
            {
                msg.TaskNewbies.Add(task.GetTaskMsg());
            }
            Send(msg);
        }


        ///// <summary>
        ///// 判断指定日期的所有任务是否完成
        ///// </summary>
        ///// <param name="day"></param>
        ///// <returns></returns>
        //public bool TaskNewbieCanGetBoxAward(int day)
        //{
        //    List<TaskNewbieData> list = taskNewbieList.Values.Where(t => t.Config.id == day).ToList();
        //    foreach(var task in list)
        //    {
        //        if (task.State == EAwardState.Undone)
        //            return false;
        //    }
        //    return true;
        //}

        /// <summary>
        /// 判断已完成的任务数量是否达到宝箱要求的数量
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool TaskNewbieCanGetBoxAward(int num)
        {
            int doneNum = taskNewbieList.Values.Where(t => t.State == EAwardState.HaveGet).Count();
            if (doneNum >= num)
                return true;
            return false;
        }

        /// <summary>
        /// 是否所有任务及宝箱都完成
        /// </summary>
        public void CheckAllComplete()
        {
            int taskNum = taskNewbieList.Values.Where(t => t.State == EAwardState.HaveGet).Count();
            List<int> awardIds = new List<int>();
            foreach (int id in Glob.config.dicNewbieAward.Keys)
                awardIds.Add(id);
            if(taskNum == Glob.config.dicTaskNewbie.Count && bonusData.newbieIds.Sum() == awardIds.Sum())
            {
                bonusData.newbieOver = true;
                bonusData.Update();

                foreach(var task in taskNewbieList.Values)
                    task.Data.Delete();
                taskNewbieList.Clear();
            }
        }

        /// <summary>
        /// 新手任务过期处理未领奖的邮件发送
        /// </summary>
        private void OverdueAward()
        {
            List<TaskNewbieData> taskList = taskNewbieList.Values.Where(t => t.State == EAwardState.Done).ToList();
            if (taskList.Count == 0)
                return;
            Dictionary<int, int> dicKv = new Dictionary<int, int>();
            //任务奖励
            foreach(var task in taskList)
            {
                if (task == null)
                    continue;
                foreach(var arr in task.Config.award)
                {
                    if (dicKv.ContainsKey(arr[0]))
                        dicKv[arr[0]] = dicKv[arr[0]] + arr[1];
                    else
                        dicKv.Add(arr[0], arr[1]);
                }
            }
            //宝箱奖励
            int doneNum = taskNewbieList.Values.Where(t => t.State == EAwardState.HaveGet).Count();
            List<int> canIds = new List<int>();//能领的宝箱id
            foreach(int boxId in Glob.config.dicNewbieAward.Keys)
            {
                if (doneNum >= boxId)
                    canIds.Add(boxId);
            }
            List<int> notIds = new List<int>();//未领的宝箱id
            foreach(int getId in canIds)
            {
                if (!bonusData.newbieIds.Contains(getId))
                    notIds.Add(getId);
            }
            foreach(int getId in notIds)
            {
                if (!Glob.config.dicNewbieAward.TryGetValue(getId, out NewbieAwardConfig conf))
                    continue;
                foreach (var arr in conf.items)
                {
                    if (dicKv.ContainsKey(arr[0]))
                        dicKv[arr[0]] = dicKv[arr[0]] + arr[1];
                    else
                        dicKv.Add(arr[0], arr[1]);
                }
            }

            //发奖励邮件
            if (dicKv.Count > 0)
            {
                List<int[]> awards = new List<int[]>();
                foreach (var kv in dicKv)
                    awards.Add(new int[] { kv.Key, kv.Value });

                ELangType langType = (ELangType)this.Data.lang;
                //发送个人邮件
                Glob.mailMgr.SendPersonMail(ID, ObjectId.Empty,
                        Glob.config.bonusSettingsConfig.TaskNewbieExpiresMailTitle.GetValue(langType),
                        Glob.config.bonusSettingsConfig.TaskNewbieExpiresContent.GetValue(langType), awards);
            }
        }*/
    }
}
