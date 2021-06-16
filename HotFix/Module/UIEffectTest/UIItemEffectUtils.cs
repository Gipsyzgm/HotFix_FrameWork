using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace HotFix_Archer.UIEffect
{
    public static class UIItemEffectUtils
    {
        /// <summary>
        /// 为界面按钮绑定效果
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Btns"></param>
        public static void Bind(this UIItemEffectEnum type,params Button[] Btns)
        {
            for (int i = 0; i < Btns.Length; i++)
            {
                Button item = Btns[i];
                EventListener.Get(item.gameObject).onDown = (data) => { Mgr.UIItemEffect.TriggerEffect(type, item); };
                EventListener.Get(item.gameObject).onUp = (data) => { Mgr.UIItemEffect.ResetEffect(item); };
            }
        }
    }
}
