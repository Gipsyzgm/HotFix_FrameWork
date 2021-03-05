using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HotFix
{
    public class BaseItem: UIObject 
    {
        /// <summary>
        /// UI路径名,自动获取,跟据UI脚本名(如果不符合自己重写此方法)
        /// 对应Addressables的简单命名
        /// 获取结果为:BaseItem
        /// </summary>
        public virtual string ItemPath {
            get {
                Type type = this.GetType();
                return type.Name;
            }
        }

        /// <summary>
        /// 在已有的GameObject上添加脚本
        /// 注:GameObject上要有UIOutlet脚本
        /// </summary>
        public void Instantiate(GameObject go)
        {
            InitGameObject(go);
        }

        /// <summary>
        /// 实例化Item
        /// </summary>
        /// <param name="prefab">预制体,会重新实例一个</param>
        /// <param name="parent">父对象</param>
        /// <param name="parent">是否实例一个新的预制体 </param>
        public void Instantiate(GameObject prefab, Transform parent)
        {
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            go.transform.SetParent(parent, false);
            InitGameObject(go);
        }

        /// <summary>
        /// 实例化Item
        /// 这是一个异步实例
        /// </summary>
        public async CTask Instantiate(Transform parent = null)
        {
            //加载物体
            GameObject TempObj = await Addressables.LoadAssetAsync<GameObject>(ItemPath).Task;
            if (TempObj==null)
            {
                Debug.Log("Item Load Fail,ItemPath= " + ItemPath);
                return;
            }
            CurObj = GameObject.Instantiate(TempObj);
            //初始化该物体
            InitGameObject(CurObj);
            if (IsDispose)
            {
                GameObject.DestroyImmediate(CurObj);
                return;
            }
            if (parent != null)
            {
                CurObj.transform.SetParent(parent, false);
                //是否初始化位置物体
                //CurObj.transform.localPosition = Vector3.zero;
            }
        }
   

        /// <summary>刷新界面</summary>
        public virtual void Refresh()
        {


        }
        /// <summary>
        /// 重置outlet，可以和Instantiate(GameObject go)配合使用
        /// </summary>
        public void ResetInit()
        {
            ObjectList.Clear();
            IsInstance = false;
        }

        public override void Dispose()
        {
            GameObject.Destroy(CurObj);
            base.Dispose();
        }
    }
}