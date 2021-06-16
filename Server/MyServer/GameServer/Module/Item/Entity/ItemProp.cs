using CommonLib;
using CommonLib.Comm;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /// <summary>
    /// 道具实体
    /// </summary>
    public class ItemProp : BaseEntity<TItemProp>
    {      
        /// <summary> 道具配置 </summary>
        public ItemConfig Config { get; private set; }

        /// <summary>所属玩家Id</summary>
        public ObjectId PlayerId { get; }

        /// <summary>模板Id</summary>
        public int TemplID => Config.id;

        /// <summary>道具子类</summary>
        public EItemSubTypeProp SubType;

        /// <summary>
        /// 拥有数量
        /// </summary>
        public int Count => Data.count;
        /// <summary>
        /// 是否创建成功
        /// </summary>
        public bool IsSucceed = false;
        public ItemProp(ObjectId playerid, TItemProp data)
        {
            PlayerId = playerid;
            Data = data;
            ItemConfig _config;           
            if (Glob.config.dicItem.TryGetValue(data.templId, out _config))
                Config = _config;
            else
            {
                Logger.LogError($"{playerid}的道具物品创建失败,找不到模板数据{data.templId}");
                return;
            }
            SubType = (EItemSubTypeProp)Config.subType;
            IsSucceed = true;
        }
        public override void Dispose()
        {
            Data = null;
            Config = null;
        }
    }
}
