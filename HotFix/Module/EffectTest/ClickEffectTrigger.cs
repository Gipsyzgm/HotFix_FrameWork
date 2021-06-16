//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace HotFix_Archer.Effect
//{
//    /// <summary>
//    /// 屏幕点击效果
//    /// </summary>
//    public class ClickEffectTrigger
//    {
//        /// <summary>
//        /// 缓存Tips，留着下次使用
//        /// </summary>
//        private static Queue<UIEffect> cacheList = new Queue<UIEffect>();

//        public ClickEffectTrigger()
//        {
//            Update();
//        }
//        async void Update()
//        {
//            WaitForUpdate waitUpdate = new WaitForUpdate();
//            while (true)
//            {
//                await waitUpdate;
//                if (Input.GetMouseButtonUp(0))
//                {
//                    showClick();
//                }
//            }
//        }
//        private WaitForSeconds wait = new WaitForSeconds(1.2f);
//        private async void showClick()
//        {
//            UIEffect effect;
//            if (cacheList.Count < 1)
//            {
//                effect = Mgr.Effect.CreateEffect<UIEffect>(1);
//                //effect.SetOrder(1000);
//            }
//            else
//            {
//                effect = cacheList.Dequeue();
//                effect.SetActive(true);
//            }
//            effect.SetPosition(getMousePosition());
//            //自行做销毁处理
//            await wait;
//            cacheList.Enqueue(effect);
//            effect.SetActive(false);

//        }

//        private Vector2 getMousePosition()
//        {
//            Vector2 pos = Vector2.zero;
//            RectTransformUtility.ScreenPointToLocalPointInRectangle(Mgr.UI.Canvas.transform as RectTransform, Input.mousePosition, Mgr.UI.Canvas.worldCamera, out pos);
//            return pos;
//        }

//    }

   
//}
