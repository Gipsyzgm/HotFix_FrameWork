using CommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonLib.Comm
{
    /// <summary>需要自定义解析方法写在这里面</summary>
    public partial class ConfigMgr
    {
        
        protected virtual void customRead()
        {

        }

        public void Initialize()
        {
            
        }

        /// <summary>
        /// 集合重新赋值
        /// Key相同重新赋值，不存在的新加进去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="retDic">原集合对象</param>
        /// <param name="newDic">新集合对象</param>
        protected void copyDictionary<T>(Dictionary<object, T> retDic, Dictionary<object, T> newDic) where T : BaseConfig
        {
            lock (retDic)
            {
                T cnf;
                foreach (var kv in newDic)
                {
                    //存在替换值
                    if (retDic.TryGetValue(kv.Key, out cnf))
                        copyClassValue<T>(cnf, kv.Value);
                    else //不存在直接加进去
                        retDic.Add(kv.Value.UniqueID, kv.Value);
                }
                List<object> delList = new List<object>();
                //新表里不存在的把老的删除掉
                foreach (var k in retDic.Keys)
                {
                    if (!newDic.ContainsKey(k))
                        delList.Add(k);
                }
                for (int i = 0; i < delList.Count; i++)
                    retDic.Remove(delList[i]);
            }
        }

        protected void copyClassValue<T>(T retObj, T newObj)
        {
            //字段
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];
                field.SetValue(retObj, field.GetValue(newObj));
            }
            //属性
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                if (property.CanWrite)
                    property.SetValue(retObj, property.GetValue(newObj));
            }
        }

        /// <summary>
        /// 重新读取配置表
        /// </summary>
        public void ReloadConfig()
        {
            reloadAll();
            customRead();
        }

    }
}
