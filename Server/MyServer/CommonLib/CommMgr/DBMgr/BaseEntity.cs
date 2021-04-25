using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Comm.DBMgr
{
    public class BaseEntity<T>: IDisposable where T: ITable
    {
        /// <summary>实体数据</summary>
        public T Data { get; protected set; }
        /// <summary>实体ObjectID</summary>
        public ObjectId ID => Data.id;
        /// <summary>实简短ID</summary>
        public int SID => Data.shortId;
        
        public bool disposedValue = false;

        public virtual void Dispose()
        {
            Data = default(T);
            disposedValue = true;
        }
    }
}
