using MongoDB.Bson;
using System;
/// <summary>
/// 配置文件基类
/// </summary>
namespace CommonLib.Comm.DBMgr
{
    public interface ITable
    {
        /// <summary>唯一ID</summary>
        ObjectId id { get;}

        /// <summary> 简短Id,只用来数据传输,不存库,每次重启服务器值可能不一样 </summary>
        int shortId { get; }

        /// <summary>插入到数据库</summary>
        void Insert();
        /// <summary>从数据库中删除</summary>
        void Delete();
        /// <summary>更新到数据库</summary>
        void Update(bool isImmediately);
    }

    public abstract class BaseTable: ITable
    {
        public virtual ObjectId id { get; protected set; }

        //简短Id
        protected int _shortId;

        /// <summary> 简短Id,只用来数据传输,不存库,每次重启服务器值可能不一样 </summary>
        public virtual int shortId => _shortId;

        /// <summary>
        /// 是否自动创建唯ObjectId 默认不创建
        /// 从数据库中读取时会设置Id值
        /// </summary>
        /// <param name="isCreadId"></param>
        public BaseTable(bool isCreadId = false)
        {
            if(isCreadId)
                id = ObjectId.GenerateNewId();
        }
        public BaseTable(ObjectId oid)
        {
            id = oid;
        }

        /// <summary>
        /// 插入到数据库
        /// </summary>
        public virtual void Insert()
        {
            if (id == ObjectId.Empty)
            {
                CommonLib.Logger.LogError(this.GetType().Name + "没有设置ID,请检查...");
                return;
            }
            DBWrite.Instance.Insert(this);
        }
        /// <summary>
        /// 更新到数据库
        /// </summary>
        public void Update(bool isImmediately = true)
        {
            DBWrite.Instance.Update(this, isImmediately);
        }      
        /// <summary>
        /// 从数据库中删除
        /// </summary>
        public void Delete()
        {
            DBWrite.Instance.Delete(this);
        }
    }
}


