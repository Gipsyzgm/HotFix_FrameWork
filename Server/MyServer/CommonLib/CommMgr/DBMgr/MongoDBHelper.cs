using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using CommonLib;
using System.Linq.Expressions;
using CommonLib.Configuration;
using System.Threading.Tasks;

namespace CommonLib.Comm.DBMgr
{
    public class MongoDBHelper
    {
        private static readonly MongoDBHelper instance = new MongoDBHelper();
        public static MongoDBHelper Instance => instance;

        /// <summary>
        /// 数据库连接字符串 mongodb://cjh:123456@127.0.0.1:27017/gamedb
        /// </summary>
        private readonly string[] connectionString = ServerSetting.Instance.MongoDBs;// ConfigurationManager.ConnectionStrings["conn_mongodb"].ConnectionString;

        private IMongoDatabase[] _dbs;
        private bool _isConnect = false;
      
        /// <summary>是否连接</summary>
        public bool IsConnect =>_isConnect;
        public MongoDBHelper()
        {
            try
            {
                //已经连上的DB
                Dictionary<string, IMongoDatabase> connectionCache = new Dictionary<string, IMongoDatabase>();
                _dbs = new IMongoDatabase[connectionString.Length];
                Logger.LogError();
                for (int i = 0; i < connectionString.Length; i++)
                {
                    IMongoDatabase _db;
                    if (!connectionCache.TryGetValue(connectionString[i], out _db))
                    {
                        MongoUrl url = new MongoUrl(connectionString[i]);
                        MongoClient clinet = new MongoClient(url);
                        _db = clinet.GetDatabase(url.DatabaseName);
                        var command = new BsonDocument("ping", 1);
                        var result = _db.RunCommand<BsonDocument>(command);
                        connectionCache.Add(connectionString[i], _db);
                    }
                    _dbs[i] = _db;

                }
                _isConnect = true;
                Logger.LogError("数据库已连接!");

            }
            catch(Exception ex)
            {
                Logger.LogError("数据库连接失败，数据库未开启或账号密码错误！！" + ex.ToString()+"\n"+ex.StackTrace);
                _isConnect = false;
            }
        }

        private IMongoCollection<T> GetCollection<T>() where T : ITable
        {
            Type type = typeof(T);            
            return _dbs[TableDBIndex.Instance.GetDB(type)].GetCollection<T>(type.Name);
        }

        //public T RunCommand<T>(ObjectId uid)
        //{
        //    var command = new BsonDocument("eval", $"selectOne({uid})");
        //    return _dbs[0].RunCommand<T>(command);
        //}

        //public BsonDocument SelectAgg<T>(ObjectId uid, BsonDocument[] pipeline) where T : ITable
        //{
        //    IMongoCollection<T> coll = GetCollection<T>();

        //    var match = new BsonDocument("$match", new BsonDocument("_id", uid));
        //    var lookup1 = new BsonDocument("$lookup", new BsonDocument { { "from", "TAccount" }, { "localField", "_id" }, { "foreignField", "_id" }, { "as", "TAccount" } });
        //    var lookup2 = new BsonDocument("$lookup", new BsonDocument { { "from", "THero" }, { "localField", "_id" }, { "foreignField", "pId" }, { "as", "THero" } });
        //    var lookup3 = new BsonDocument("$lookup", new BsonDocument { { "from", "TBuild" }, { "localField", "_id" }, { "foreignField", "pId" }, { "as", "TBuild" } });
        //    var lookup4 = new BsonDocument("$lookup", new BsonDocument { { "from", "TItemProp" }, { "localField", "_id" }, { "foreignField", "pId" }, { "as", "TItemProp" } });
        //    var lookup5 = new BsonDocument("$lookup", new BsonDocument { { "from", "TItemEquip" }, { "localField", "_id" }, { "foreignField", "pId" }, { "as", "TItemEquip" } });

        //    //var pipeline = new[] { match, lookup1, lookup2, lookup3, lookup4, lookup5 };
        //    var result = coll.Aggregate<BsonDocument>(pipeline).First();
        //    return result;
        //}


        /// <summary> 跟据唯一Id查找记录 </summary>
        public T Select<T>(ObjectId uid) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(t => t.id, uid);
            return coll.Find<T>(filter).FirstOrDefault<T>();
        }

        /// <summary>跟据查询条件查询,查一条</summary>
        public T SelectOne<T>(FilterDefinition<T> filter) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            return coll.Find<T>(filter).FirstOrDefault<T>();

        }
        /// <summary>跟据查询条件查询,如果没条件查询全部</summary>
        public List<T> Select<T>(Expression<Func<T, bool>> filter=null) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            if (filter == null)
                return coll.Find<T>(Builders<T>.Filter.Empty).ToList<T>();
            return coll.Find<T>(filter).ToList<T>();
        }
        /// <summary>跟据查询条件查询,如果没条件查询全部</summary>
        public List<T> SelectFilter<T>(FilterDefinition<T> filter = null) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            if (filter == null)
                filter = Builders<T>.Filter.Empty;
            return coll.Find<T>(filter).ToList<T>();
        }

        /// <summary>
        /// 用于排名查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sort">排序规则</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="limit">反回数量默认100</param>
        /// <returns></returns>
        public List<T> RankSelect<T>(SortDefinition<T> sort, FilterDefinition<T> filter = null, int limit = 100) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            if (filter == null)
                filter = Builders<T>.Filter.Empty;
            return coll.Find<T>(filter).Sort(sort).Limit(limit).ToList<T>();
        }

        /// <summary> 插入一条记录 </summary>
        public void Insert<T>(T data) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            coll.InsertOne(data);
        }
        /// <summary>
        /// 插入一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void InsertList<T>(List<T> data) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>(); 
            coll.InsertMany(data);
        }


        /// <summary> 更新一条记录 </summary>
        public void Update<T>(T data) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            coll.ReplaceOne(t => t.id == data.id, data);
        }
        /// <summary>
        /// 按条件更新多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        public long UpdateMany<T>(FilterDefinition<T> filter,UpdateDefinition<T> update) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            if (filter == null)
                filter = Builders<T>.Filter.Empty;
            UpdateResult result =  coll.UpdateMany(filter, update);
            return result.ModifiedCount;
        }

        /// <summary>
        /// 按条件更新多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        public long UpdateManyList<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            if (filter == null)
                filter = Builders<T>.Filter.Empty;
            UpdateResult result = coll.UpdateMany(filter, update);
            
            return result.ModifiedCount;
        }



        /// <summary> 删除一条记录 </summary>
        public void Delete<T>(T data) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            coll.DeleteOne(t => t.id == data.id);
        }

        /// <summary>
        /// 按条件删除多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        public long DeleteMany<T>(FilterDefinition<T> filter) where T : ITable
        {
            if (filter == null)
                filter = Builders<T>.Filter.Empty;
            IMongoCollection<T> coll = GetCollection<T>();
            DeleteResult result = coll.DeleteMany(filter);
            return result.DeletedCount;
        }

        /// <summary>
        /// 清空表
        /// </summary>
        public void DeleteAll<T>() where T : ITable
        {
            Type type = typeof(T);
            _dbs[TableDBIndex.Instance.GetDB(type)].DropCollection(type.Name);
        }


        #region 异步方法
        /// <summary> 跟据唯一Id查找记录 </summary>
        public async Task<T> SelectAsync<T>(ObjectId uid) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(t => t.id, uid);
            return await coll.FindAsync<T>(filter).Result.FirstAsync<T>();
        }


        public T SelectAsync2<T>(ObjectId uid) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(t => t.id, uid);
            return coll.Find<T>(filter).ToListAsync<T>().Result.First<T>();
        }


        /// <summary>跟据查询条件查询,如果没条件查询全部</summary>
        public async Task<List<T>> SelectAsync<T>(Expression<Func<T, bool>> filter = null) where T : ITable
        {
            IMongoCollection<T> coll = GetCollection<T>();
            if (filter == null)
                return await coll.FindAsync<T>(Builders<T>.Filter.Empty).Result.ToListAsync<T>();
            return await coll.FindAsync<T>(filter).Result.ToListAsync<T>();
        }
        #endregion

    }

}
