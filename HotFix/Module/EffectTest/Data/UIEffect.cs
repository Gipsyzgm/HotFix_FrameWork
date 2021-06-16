using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Archer.Effect
{
    public class UIEffect: BaseEffect
    {        
        private int m_order = -1;
        private bool isNeedOrder = false;
        public UIEffect(EffectConfig config, Vector3 pos, Transform parent) : base(config, pos, parent)
        {

        }
        protected override void SetComponent()
        {
            if (gameObject != null)
            {
                if (m_order == -1)
                {
                    Canvas canvas = gameObject.GetComponentInParent<Canvas>();
                    if (canvas != null)
                        m_order = canvas.sortingOrder + 1;
                    SetOrder(m_order);
                }
                else if (isNeedOrder)
                    SetOrder(m_order);
            }
            base.SetComponent();
        }

        /// <summary>
        /// 设置UI特效排序
        /// </summary>
        /// <param name="order"></param>
        public void SetOrder(int order)
        {
            m_order = order;
            if (gameObject != null)
            {
                Renderer[] renders = gameObject.GetComponentsInChildren<Renderer>();
                for (int i = 0; i < renders.Length; i++)
                    renders[i].sortingOrder = m_order;

                Canvas[] canvas = gameObject.GetComponentsInChildren<Canvas>();
                for (int i = 0; i < canvas.Length; i++)
                    canvas[i].sortingOrder = m_order+ canvas[i].sortingOrder;

                isNeedOrder = false;
            }
            else
                isNeedOrder = true;
        }


        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
