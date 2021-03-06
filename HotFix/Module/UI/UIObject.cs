using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HotFix
{
    public class UIObject
    {
        /// <summary>
        /// 在场景中的物体
        /// </summary>
        public GameObject CurObj;
        /// <summary>
        /// 面板下包含的已标记的子物体
        /// </summary>
        protected Dictionary<string, GameObject> ObjectList = new Dictionary<string, GameObject>();
        /// <summary>
        /// UI物体的位置
        /// </summary>
        public Transform transform;
        /// <summary>
        ///  UI物体的位置
        /// </summary>
        public RectTransform rectTransform;
        /// <summary>
        /// UI所属节点类型
        /// </summary>
        public PanelLayer layer = PanelLayer.UIMain;
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        public bool IsInstance = false;
        /// <summary>
        /// 是否已经销毁掉了
        /// </summary>
        protected bool IsDispose = false;
        protected void InitGameObject(GameObject obj)
        {
            CurObj = obj;
            transform = obj.transform;
            rectTransform = obj.GetComponent<RectTransform>();
            UIOutlet uiInfo = obj.GetComponent<UIOutlet>();
            for (int i = 0; i < uiInfo.OutletInfos.Count; i++)
            {
                ObjectList.Add(uiInfo.OutletInfos[i].Name, uiInfo.OutletInfos[i].Object as GameObject);
            }
            layer = (PanelLayer)uiInfo.Layer; 
            InitComponent();         
            Init();
            IsInstance = true;
        }
        /// <summary>初始化组件(代码生成器生成)</summary>
        public virtual void InitComponent()
        {

        }
        /// <summary>
        /// 初始化按钮事件
        /// </summary>
        public virtual void Init() 
        {


        }

        public virtual void SetActive(bool value)
        {
            if (CurObj != null)
                CurObj.SetActive(value);
        }


        /// <summary>
        /// 获取控件引用对象(在UIOutlet中定义的)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">控件名</param>
        /// <returns></returns>
        public T Get<T>(string name) where T : Component
        {
            GameObject obj = Get(name);
            if (obj == null)
                return null;
            return obj.GetComponent<T>();
        }
        /// <summary>
        /// 获取引用对象(在UIOutlet中定义的)
        /// </summary>
        public GameObject Get(string name)
        {
            GameObject obj;
            if (!ObjectList.TryGetValue(name, out obj))
            {
                Debug.Log($"未找到GameObject对象,请在UIOutlet中设置:{name}");
            }
            return obj;
        }
        /// <summary>
        /// 获取控件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">路径</param>
        /// <param name="parent">父对象</param>
        /// <returns></returns>
        public T GetComponen<T>(string path, Transform parent = null) where T : Component
        {
            if (parent == null)
                parent = transform;
            Transform tran = parent.Find(path);
            if (tran == null)
            {
                string str = $"父对象[{parent.name}],子路径[{path}]下未找到[{typeof(T).Name}]类型的对象";
                Debug.Log(str);
                return null;
            }
            return parent.Find(path).GetComponent<T>();
        }
        /// <summary>
        /// 渐现菜单,使用关闭动画会在动画结束自动设置隐藏
        /// 使用Fade模式的话，不要把Ui对象的a值设为0
        /// </summary>
        /// <param name="target">菜单游戏对象</param>
        /// <param name="anim">动画类型</param>
        /// <param name="CloseCallBack">关闭动画后需要进行的操作/避免在播动画时物体消毁了</param>
        public void ShowUIAnim(GameObject target, UIAnim anim ,Action CloseCallBack = null)
        {
            float time = 0.5f;
            switch (anim)
            {
                case UIAnim.FadeOut:
                    time = 0.35f;
                    break;
                case UIAnim.ScaleOut:
                    time = 0.2f;
                    break;
            }
            if (anim == UIAnim.None || target == null)
            {
                return;
            }
            //UI淡入淡出效果
            if (anim == UIAnim.FadeIn || anim == UIAnim.FadeOut)
            {
                Graphic[] comps = target.GetComponentsInChildren<Graphic>();
                for (int i = comps.Length; --i >= 0;)
                {
                    if (anim == UIAnim.FadeIn)
                    {
                        if (comps[i].color.a == 0)
                        {
                            comps[i].DOFade(1, time).SetUpdate(true);
                        }
                        else
                        {
                            comps[i].DOFade(0, time).SetUpdate(true).From();
                        }
                    }
                    else 
                    {
                        if (i == 0)
                        {
                            comps[i].DOFade(0, time).SetUpdate(true).OnComplete(() =>
                            {                            
                                target.SetActive(false);                                  
                                CloseCallBack?.Invoke();
                            });
                        }
                        else 
                        {
                            comps[i].DOFade(0, time).SetUpdate(true);
                        }                       
                    }
                        
                }            
            }
            else if (anim == UIAnim.ScaleIn || anim == UIAnim.ScaleOut)
            {
                if (anim == UIAnim.ScaleIn)
                {
                    target.transform.DOScale(0, time).SetUpdate(true).SetEase(Ease.OutBack).From();
                }
                else
                {
                    target.transform.DOScale(0, time).SetUpdate(true).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        if (target.activeSelf)
                        {
                            target.SetActive(false);
                            target.transform.localScale = Vector3.one;
                            CloseCallBack?.Invoke();
                        }
                    }); 
                }
            }
        }      

        public virtual void Dispose()
        {
            IsDispose = true;
            ObjectList = null;
            CurObj = null;
            transform = null;
        }
    }
}
