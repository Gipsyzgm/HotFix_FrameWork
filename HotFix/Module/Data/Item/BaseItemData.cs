using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    /// <summary>
    /// Item数据基类
    /// </summary>
    public class BaseItemData
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TempId => Config.id;

        /// <summary>
        /// 物品配置
        /// </summary>
        public ItemConfig Config;
               
        /// <summary>
        /// 物品大类
        /// </summary>
        public EItemType Type => (EItemType)Config.type;

        /// <summary>
        /// 物品品质
        /// </summary>
       // public EQuality Quality => (EQuality)Config.quality;

       
        /// <summary></summary>
        /// <param name="tempId">物品模板ID</param>
        public BaseItemData(int tempId)
        {
            Config = ItemUtils.GetItemConfig(tempId);
        }
    }


}
