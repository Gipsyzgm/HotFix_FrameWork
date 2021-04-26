using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix.Module.UI
{
    public class Tips
    {
        private static Queue<string> tipsQueueList = new Queue<string>();
        /// <summary>
        /// 缓存Tips，留着下次使用
        /// </summary>
        public static Queue<TipItem> cacheTipsList = new Queue<TipItem>();

        private static CTaskHandle taskRun;

        private static string currTips;
        /// <summary>
        /// 弹出Tips提示
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="canLastSame">是否允许与上次的Tips相同</param>
        public static void Show(string content, bool canLastSame = false)
        {
            //不充许与队队中最后一个元素的Tips相同
            if (!canLastSame && tipsQueueList.Count > 0 && content == tipsQueueList.Last())
            {
                Debug.Log("元素相同");
                return;
            }            
            tipsQueueList.Enqueue(content);
            if (taskRun.IsDead)
                taskRun = RunShow().Run();        
        }

        private static async CTask RunShow()
        {
            while (tipsQueueList.Count > 0)
            {
                Debug.Log("出列");
                currTips = tipsQueueList.Dequeue();
                TipItem item;
                if (cacheTipsList.Count > 0)
                {
                    item = cacheTipsList.Dequeue();
                    item.TipsAnim(currTips);         
                }
                else
                {
                    item = new TipItem();
                    await item.Instantiate(HotMgr.UI.layer_dict[PanelLayer.UITips]);
                    item.TipsAnim(currTips);
                }
                if (Time.timeScale != 0)
                {
                    await CTask.WaitForSeconds(0.5f);
                }
                else
                {
                    await CTask.WaitForRealSeconds(0.5f);
                }
            }

            currTips = null;
            taskRun.Stop();
        }


        /// <summary>
        /// 跟据多语言Key值弹出Tips提示
        /// </summary>
        /// <param name="key"></param>
        public static void ShowLang(string key)
        {
            //Show(HotMgr.Lang.Get(key));
        }

        public static void ClearTips() 
        {       
            tipsQueueList.Clear();
        }

    }
}
