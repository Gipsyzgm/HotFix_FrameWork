using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HotFix
{
    //使用必须要Release才能循环使用
    public class GameObjectPool
    {
        /// <summary>
        /// 所有Pool的数据
        /// </summary>
        private readonly Dictionary<string, Stack<GameObject>> pools = new Dictionary<string, Stack<GameObject>>();
        public Transform root;
        //空闲对象最大存在数量
        private int Count = 10;
        public GameObjectPool()
        {
            root = new GameObject("__GameObjectPool__").transform;
            GameObject.DontDestroyOnLoad(root);
            //root.position = Vector3.one * -10000;
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async CTask<GameObject> Get(string name)
        {
            var pool = GetPool(name);
            while (pool.Count > Count)
            {
                GameObject obj = pool.Pop();
                GameObject.Destroy(obj);
            }
            if (pool.Count > 0)
            {
                GameObject obj = pool.Pop();
                if (root!= null)
                {              
                    obj.transform.SetParent(root);
                }               
                obj.SetActive(true);
                obj.transform.localScale = Vector3.one;
                return obj;
            }
            else
            {
                GameObject obj = await Addressables.InstantiateAsync(name).Task;
                if (root != null)
                {
                    obj.transform.SetParent(root);
                }              
                obj.SetActive(true);
                obj.transform.localScale = Vector3.one;
                return obj;
            }
        }
        /// <summary>
        /// 获取对象，并设置父物体，某些预制可能有拖尾，显示不正确，延时一帧显示
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public async CTask<GameObject> Get(string name,Transform parent)
        {
            var pool = GetPool(name);
            while (pool.Count > Count)
            {
                GameObject obj = pool.Pop();
                GameObject.Destroy(obj);
            }
            if (pool.Count > 0)
            {
                GameObject obj = pool.Pop();
                obj.transform.SetParent(parent, false);
                await CTask.WaitForNextFrame();
                obj.SetActive(true);
                obj.transform.localScale = Vector3.one;
                return obj;
            }
            else 
            {
                GameObject obj = await Addressables.InstantiateAsync(name).Task;
                obj.transform.SetParent(parent, false);
                obj.SetActive(true);
                obj.transform.localScale = Vector3.one;
                return obj;
            }
               
        }
        /// <summary>
        /// 释放资源，重新加入缓存等待使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public void Release(string name, GameObject obj)
        {
            if (obj == null) return;
            obj.SetActive(false);
            obj.transform.SetParent(root);           
            var pool = GetPool(name);
            pool.Push(obj);
        }

        public void Clear()
        {
            foreach (var item in pools)
            {
                //清除所有Stack的物体。
                foreach (var gameObject in item.Value)
                {
                    GameObject.Destroy(gameObject);
                }
                item.Value.Clear();
            }
            pools.Clear();
        }
        private Stack<GameObject> GetPool(string name)
        {
            if (!pools.ContainsKey(name))
            {
                var pool = new Stack<GameObject>();
                pools[name] = pool;
                return pool;
            }
            return pools[name];
        }
    }
}
