using System.Collections.Generic;
using CommonLib;
using MongoDB.Bson;

namespace CommonLib.Comm.DBMgr
{
    /// <summary>
    ///     读取数据库数据
    /// </summary>
    public class DBReader
    {
        public static DBReader Instance { get; } = new DBReader();


        public bool Initialize()
        {
            return MongoDBHelper.Instance.IsConnect;
        }

        /// <summary>
        ///     读取所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DictionarySafe<ObjectId, T> SelectAll<T>() where T : ITable
        {
            var name = typeof(T).Name;
            var tabName = TableName.Instance.GetName(name);
            if (!string.IsNullOrEmpty(tabName))
                name = name + "[" + tabName + "]";
            Logger.SysStart($"读取{name}...");
            var rtn = new DictionarySafe<ObjectId, T>();
            var list = MongoDBHelper.Instance.Select<T>();
            for (var i = 0; i < list.Count; i++) rtn.Add(list[i].id, list[i]);
            Logger.SysEnd("共" + rtn.Count + "条");
            return rtn;
        }

        /// <summary>
        ///     读取所有数据，返回List列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> SelectAllList<T>() where T : ITable
        {
            var name = typeof(T).Name;
            var tabName = TableName.Instance.GetName(name);
            if (!string.IsNullOrEmpty(tabName))
                name = name + "[" + tabName + "]";
            Logger.SysStart($"读取{name}...");
            var list = MongoDBHelper.Instance.Select<T>();
            Logger.SysEnd("共" + list.Count + "条");
            return list;
        }
    }
}