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
        /// <summary>
        /// 需要显示的Tips
        /// </summary>
        private static Queue<string> TipsQueueList = new Queue<string>();
        /// <summary>
        /// 缓存Tips实例，留着下次使用
        /// </summary>
        public static Queue<TipItem> CacheTipsList = new Queue<TipItem>();

        private static CTaskHandle TaskRun;
        //当前显示的Tips
        private static string CurrTips;
        /// <summary>
        /// 弹出Tips提示
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="canLastSame">是否允许与上次的Tips相同</param>
        public static void Show(string content, bool canLastSame = false)
        {
            //不充许与队队中最后一个元素的Tips相同
            if (!canLastSame && TipsQueueList.Count > 0 && content == TipsQueueList.Last())
            {
                return;
            }            
            TipsQueueList.Enqueue(content);
            if (TaskRun.IsDead)
                TaskRun = RunShow().Run();        
        }

        private static async CTask RunShow()
        {
            while (TipsQueueList.Count > 0)
            {
                CurrTips = TipsQueueList.Dequeue();
                TipItem item;
                if (CacheTipsList.Count > 0)
                {
                    item = CacheTipsList.Dequeue();
                    item.TipsAnim(CurrTips);         
                }
                else
                {
                    item = new TipItem();
                    await item.Instantiate(HotMgr.UI.layer_dict[PanelLayer.UITips]);
                    item.TipsAnim(CurrTips);
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

            CurrTips = null;
            TaskRun.Stop();
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
            TipsQueueList.Clear();
        }

    }
}
