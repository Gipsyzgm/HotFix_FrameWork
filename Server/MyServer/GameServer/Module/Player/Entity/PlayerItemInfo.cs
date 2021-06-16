using CommonLib;
using MongoDB.Bson;
using System.Collections.Generic;

namespace GameServer.Module
{
    /// <summary>
    /// 玩家物品信息数据
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// 当前交通工具SID
        /// </summary>
        //public int CurrVehicleSID => TItemEquip.ToShortId(Data.vehicleId);

        /// <summary>
        /// 玩家全部装备[sid,ItemEquip]
        /// </summary>
        public DictionarySafe<int, ItemEquip> equipList = new DictionarySafe<int, ItemEquip>();

        /// <summary>
        /// 玩家全部道具[模板Id,道具对象]
        /// </summary>
        public DictionarySafe<int, ItemProp> propList = new DictionarySafe<int, ItemProp>();

        /// <summary>
        /// 玩家全部位置信息[位置id,模板id]
        /// </summary>
        public DictionarySafe<EItemSubTypeEquipIndex, int> placeList = new DictionarySafe<EItemSubTypeEquipIndex, int>();
    }
}
