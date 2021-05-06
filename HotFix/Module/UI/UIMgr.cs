/*
 *  项目名字：MyFrameWork
 *  创建时间：2019.12.28
 *  描述信息：UI页面控制。
 *  注意事项：
 *  1：引入PanelLayer概念。处于下层的Layer的页面优先显示，优先级大于Open级别。
 *  例：在Tips层的UI页面打开Panel层的UI页面。在Tips层的UI处于显示状态的话一定遮挡Panel层的UI。
 *  如果不需要Layer控制，全部放在Start层也没问题。
 */
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace HotFix
{
    public class UIMgr
    {
        public Transform _canvas = MainMgr.UI.canvas.transform;
        private Dictionary<string, BaseUI> Paneldict = new Dictionary<string, BaseUI>();
        public Dictionary<PanelLayer, Transform> layer_dict;
        /// <summary>
        /// 用于记录当前页面显示顺序并实现关闭当前页面功能。
        /// </summary>
        private List<string> ExictPanel = new List<string>();

        public GameObject Loading;
        public Image LoadingMask { get; private set; }

        public UIMgr()
        {
            InitLayer();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitLayer()
        {
            Debug.Log("初始化layer");        
            if (_canvas == null)
            {
                Debug.Log("UIMgr.InitLayerFail,Canvas is null");
            }              
            Transform UIRoot = _canvas.Find("UIRoot");
            if (UIRoot==null)
                Debug.Log("UICanvas下未找到UIRoot");
            Loading = UIRoot.Find("UILoading").gameObject;
            LoadingMask = Loading.transform.Find("Mask").GetComponent<Image>();
            layer_dict = new Dictionary<PanelLayer, Transform>();
           
            //存layer的位置
            foreach (PanelLayer pl in Enum.GetValues(typeof(PanelLayer)))
            {
                string name = pl.ToString();
                Transform transform = UIRoot.Find(name);
                if (transform == null)
                    Debug.Log("UIRoot未找到节点："+name);
                layer_dict.Add(pl, transform);
            }
        }
        /// <summary>
        /// 首次打开必须根据类型打开页面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="skinPath"></param>
        /// <param name="_args"></param>
        public async CTask Show<T>(UIAnim uIAnim = UIAnim.None,UILoading uILoading =UILoading.None,params object[] _args) where T : BaseUI
        {
            string name = typeof(T).ToString();
            Type type = Type.GetType(name);
            if (Paneldict.ContainsKey(name))
            {
                //设置页面在最后面（摄像机最先渲染位置）
                GetPanel(name).CurObj.transform.SetAsLastSibling();
                //如果物体是处于显示状态，直接显示出来。
                if (GetPanel(name).CurObj.gameObject.activeInHierarchy)
                {
                    AddToListLast(name);
                    return;
                }
                //如果物体是处于隐藏状态，刷新页面。
                GetPanel(name).args = _args;
                AddToList(name);
                GetPanel(name).OnShow(uIAnim);
                GetPanel(name).Refresh();
                return;
            }
            //创建实例
            BaseUI UIPanel = Activator.CreateInstance(type) as BaseUI;
            UIPanel.IsLoading = uILoading;
            //加载物体
            await UIPanel.LoadGameObject();
            UIPanel.CurObj.transform.SetAsLastSibling();
            //赋值
            UIPanel.Init(_args);
            Paneldict.Add(name, UIPanel);
            //设置父物体层级
            Transform Parent;
            if (layer_dict.TryGetValue(UIPanel.layer, out Parent))
            {
                UIPanel.CurObj.transform.SetParent(Parent, false);
            }
            else 
            {
                Debug.Log("没有找到对应Ui层级:"+ UIPanel.layer);
            }          
            AddToList(name);          
            UIPanel.OnShow(uIAnim);
            UIPanel.Refresh();
        }

        /// <summary>
        /// 依次关闭最上层的panel
        /// </summary>
        public void CloseCurrentPanel()
        {
            if (ExictPanel == null)
            {
                Debug.Log("所有页面关闭或ExictPanelList异常");
                return;
            }
            if (ExictPanel.Count == 1)
            {
                Debug.Log("除主页面和隐藏页面外已经全部关闭");
                return;
            }
            string name = ExictPanel[ExictPanel.Count - 1];
            ClosePanel(name);
        }
        /// <summary>
        /// 关闭页面
        /// </summary>
        /// <param name="name"></param>
        public void ClosePanel(string name,UIAnim uIAnim = UIAnim.None)
        {
            BaseUI panel;
            Paneldict.TryGetValue(name, out panel);
            if (panel == null)
                return;
            Paneldict.Remove(name);
            RomoveToList(name);
            panel.OnClose(uIAnim);
        }
        /// <summary>
        /// 隐藏页面
        /// </summary>
        /// <param name="panelName"></param>
        public void HidePanel(string panelName,UIAnim uIAnim = UIAnim.None)
        {           
            BaseUI panel = GetPanel(panelName);
            if (panel != null)
            {
                if (!panel.CurObj.activeInHierarchy)
                    return;
                RomoveToList(panelName.ToString());
                panel.OnHide(uIAnim);
            }
        }
        /// <summary>
        /// 通过名字获得panel
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private BaseUI GetPanel(string name)
        {
            BaseUI panel;
            if (Paneldict.TryGetValue(name, out panel))
            {
                return panel;
            }
            return null;
        }
        public T GetPanel<T>() where T : BaseUI
        {
            string name = typeof(T).Name;
            return GetPanel(name) as T;
        }

        public void ClosePanel<T>(UIAnim uIAnim = UIAnim.None) where T : BaseUI
        {   
            //关闭之前可以播关闭动画
            string name = typeof(T).Name;
            ClosePanel(name, uIAnim);
        }

        /// <summary>
        /// 每次未关闭页面置顶的时候放到最后面
        /// </summary>
        /// <param name="item"></param>
        private void AddToListLast(string item)
        {
            ExictPanel.Remove(item);
            ExictPanel.Add(item);
        }
        /// <summary>
        /// 每次打开页面或者隐藏页面显示时Add一次
        /// </summary>
        /// <param name="item"></param>
        private void AddToList(string item)
        {
            ExictPanel.Add(item);
        }
        /// <summary>
        /// 每次隐藏或者关闭Romove掉
        /// </summary>
        /// <param name="item"></param>
        private void RomoveToList(string item)
        {
            ExictPanel.Remove(item);
        }
       

    }

}
