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
        public virtual string ItemPath {
            get {
                Type type = this.GetType();
                return type.Name;
            }
        }
        /// <summary>
        /// 实例化Item
        /// 这是一个异步实例
        /// </summary>
        public async CTask LoadGameObject(Transform parent = null)
        {
            //加载物体
            GameObject TempObj = await Addressables.LoadAssetAsync<GameObject>(ItemPath).Task;
            CurObj = GameObject.Instantiate(TempObj);
            if (CurObj == null)
                Debug.Log("Item Load Fail,ItemPath= " + ItemPath);
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
                CurObj.transform.localPosition = Vector3.zero;
            }
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

        /// <summary>刷新界面</summary>
        public virtual void Refresh()
        {


        }
        /// <summary>
        /// 重置outlet
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