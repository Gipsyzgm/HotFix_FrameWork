using HotFix.Module.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    public class ItemUtils
    {
        /// <summary>
        /// 获取物品配置
        /// </summary>
        public static ItemConfig GetItemConfig(int tempId)
        {
            if (tempId == 0) return null;
            ItemConfig config;
            if (!HotMgr.Config.dicItem.TryGetValue(tempId, out config))
                Debug.LogError($"物品[{tempId}]未找到模板配置!!");
            return config;
        }

        /// <summary>
        /// 获取物品名称，带品质颜色
        /// </summary>
        /// <param name="templId"></param>
        /// <param name="addStr">额外增加字符串和品质颜色保持一至</param>
        /// <returns></returns>
        public static string GetItemColorName(int templId, string addStr = "")
        {
            ItemConfig config = GetItemConfig(templId);
            string itemName = string.Empty;
            if (config != null)
                itemName = Utils.GetQualityColorStr(1, config.name.Value + addStr);
            return itemName;
        }

        /// <summary>
        /// 获取虚拟物品名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetVirtualItemName(EItemSubTypeVirtual type)
        {
            ItemConfig config = GetItemConfig((int)type);
            return config.name.Value;
        }

        ///// <summary>
        ///// 获取玩家道具物品数量
        ///// </summary>
        ///// <returns></returns>+
        //public static int GetItemPropNum(int templId)
        //{
        //    ItemProp item;
        //    if (ItemMgr.I.dicItemProp.TryGetValue(templId, out item))
        //        return item.Num;
        //    return 0;
        //}

        /// <summary>
        /// 跟据物品品质获取物品边框
        /// </summary>
        /// <param name="quality"></param>
        public static string GetItemBK(int quality)
        {
            return "BK_Item" + quality;
        }

        /// <summary>
        /// 显示获得虚拟物品提示
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        public static void ShowVirtualItem(EItemSubTypeVirtual type, int count)
        {
            ShowItem((int)type, count);
        }
        /// <summary>
        /// 显示获得物品提示
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        public static void ShowItem(int templId, int count)
        {
            if (count > 0)
            {
                ItemConfig config = GetItemConfig(templId);
                if (config != null)
                {
                    Tips.Show(Utils.GetQualityColorStr(1, config.name.Value) + "+" + count, true);
                }
            }
        }


        /// <summary>
        /// 判断道具是否为某一子类型
        /// </summary>
        /// <returns></returns>
        public static bool CheckPropType(ItemConfig config, EItemSubTypeProp subType)
        {
            return (EItemType)config.type == EItemType.Prop && (EItemSubTypeProp)config.subType == subType;
        }
    }
}