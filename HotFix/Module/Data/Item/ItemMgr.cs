using PbBag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    /// <summary>
    /// 物品管理器
    /// </summary>
    public class ItemMgr : BaseDataMgr<ItemMgr>, IDisposable
    {
        /// <summary>玩家已获得的道具列表[模板Id,道具]</summary>
        public Dictionary<int, ItemProp> dicItemProp = new Dictionary<int, ItemProp>();
        /// <summary>玩家已获得的全部装备[模板Id（联网时对应的是Sid）,装备]</summary>
        public Dictionary<int, ItemEquip> dicItemEquip = new Dictionary<int, ItemEquip>();
        /// <summary>穿戴的装备[位置,对应的装备]</summary>
        public Dictionary<EItemEquipPlace, ItemEquip> dicUseEquip = new Dictionary<EItemEquipPlace, ItemEquip>();
        /// <summary>
        /// 单机模式的全部物品数据
        /// </summary>
        protected TItems Data = new TItems();
        /// <summary>
        /// 单机模式的存储道具数据
        /// </summary>
        public void SaveData()
        {
            Data.Save();
        }
        /// <summary>
        /// 获取某位置正在装备中的道具
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public ItemEquip GetUseEquip(EItemEquipPlace place)
        {
            if (dicUseEquip.TryGetValue(place, out var equip))
                return equip;
            return null;
        }
        /// <summary>
        /// 获得物品
        /// </summary>
        /// <param name="itemId">物品Id</param>
        /// <param name="num">数量</param>
        /// <param name="Sid">联网的话需要的唯一ID</param>
        /// <param name="place">装备位置</param>
        /// <param name="isTips">是否显示Tips</param>
        public void AddNewItem(int itemId, int num, int Sid = 0, EItemEquipPlace place = EItemEquipPlace.None, bool isTips = true)
        {
            ItemConfig config = ItemUtils.GetItemConfig(itemId);
            if (config == null) return;
            switch ((EItemType)config.type)
            {
                case EItemType.Virtual:
                    AddVirtualItems((EItemSubTypeVirtual)config.subType, num, isTips);
                    return;
                case EItemType.Prop:
                    if (dicItemProp.TryGetValue(itemId, out var prop))
                        prop.Count += num;
                    else
                    {
                        TItemProp propData = new TItemProp();
                        propData.TemplId = itemId;
                        propData.Count = num;
                        var propN = new ItemProp(propData);
                        dicItemProp.Add(itemId, propN);
                        Data.PropList.Add(propData);
                    }
                    break;
                case EItemType.Hero:
                case EItemType.Pet:
                case EItemType.OutWeapon:
                case EItemType.InnerWeapon:
                case EItemType.Ring:
                    if (Sid!=0&&dicItemEquip.TryGetValue(Sid, out var sequip))
                    {
                        Debug.Log("装备重复Sid:" + Sid);
                        return;
                    }
                    if (dicItemEquip.TryGetValue(itemId, out var equip)) 
                    {
                        equip.Count += num;
                    }                      
                    else
                    {
                        TItemEquip equipData = new TItemEquip();                    
                        equipData.TemplId = itemId;
                        equipData.SId = Sid;
                        equipData.Index = (int)place;
                        CreateEquip(equipData);
                        Data.EquipList.Add(equipData);
                    }
                    break;
            }
            SaveData();
            if (isTips)
                ItemUtils.ShowItem(itemId, num);
        }

        /// <summary>
        /// 使用装备
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="place"></param>
        public void UseEquip(ItemEquip equip, EItemEquipPlace place = EItemEquipPlace.None)
        {
            EItemEquipPlace targetPlace = EItemEquipPlace.None;
            switch (equip.Type)
            {
                case EItemType.Hero:
                    targetPlace = EItemEquipPlace.Hero;
                    break;
                case EItemType.Pet:
                    targetPlace = EItemEquipPlace.Pet;
                    break;
                case EItemType.OutWeapon:
                    targetPlace = EItemEquipPlace.OutWeapon;
                    break;
                case EItemType.InnerWeapon:
                    targetPlace = EItemEquipPlace.InnerWeapon;
                    break;
                case EItemType.Ring:
                    if (place == EItemEquipPlace.None)
                        targetPlace = EItemEquipPlace.Ring1;
                    else
                        targetPlace = place;
                    break;
                default:
                    break;
            }
            if (targetPlace == EItemEquipPlace.None) return;
            equip.Place = targetPlace;
            if (dicUseEquip.TryGetValue(targetPlace, out var useEquip))
            {
                useEquip.Place = EItemEquipPlace.None;
                dicUseEquip.Remove(equip.Place);
            }
            dicUseEquip.Add(equip.Place, equip);
            PlayerMgr.PlayerWarData.Recount();
            SaveData();
        }
        /// <summary>
        /// 卸下装备
        /// </summary>
        /// <param name="place"></param>
        public void DisEquip(EItemEquipPlace place)
        {
            if (dicUseEquip.TryGetValue(place, out var useEquip))
            {
                useEquip.Place = EItemEquipPlace.None;
                dicUseEquip.Remove(place);
                PlayerMgr.PlayerWarData.Recount();
                SaveData();
            }
        }
        /// <summary>
        /// 增加虚拟物品
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="count">数量</param>
        /// <param name="isTips">是否显示Tips</param>
        public void AddVirtualItems(EItemSubTypeVirtual type, int count, bool isTips = true)
        {
            if (count == 0) return;
            switch (type)
            {
                case EItemSubTypeVirtual.Gold://金币
                    PlayerMgr.MainPlayer.Gold += count;
                    break;
                case EItemSubTypeVirtual.Ticket://点券
                    PlayerMgr.MainPlayer.Ticket += count;
                    break;
                case EItemSubTypeVirtual.Power://体力
                    PlayerMgr.MainPlayer.Ticket += count;
                    break;
                case EItemSubTypeVirtual.Exp://玩家经验
                    PlayerMgr.MainPlayer.AddExp(count);
                    break;
            }
            PlayerMgr.I.SavePlayerData();
            if (isTips)
                ItemUtils.ShowVirtualItem(type, count);

        }
        /// <summary>
        /// 创建装备实体并加入数据
        /// </summary>
        /// <param name="equipData"></param>
        protected void CreateEquip(TItemEquip equipData)
        {
            ItemConfig config = ItemUtils.GetItemConfig(equipData.TemplId);
            if (config != null)
            {
                var equip = new ItemEquip(equipData);
                //如果需要分类型显示，可以可以做分类操作
                dicItemEquip.Add(equip.TempId, equip);
                if (equipData.SId != 0)
                {
                    //如果装备的数据是服务器给的，都拿Sid来处理。
                    if (!dicItemEquip.ContainsKey(equipData.SId))
                    {                    
                        dicItemEquip.Add(equipData.SId, equip);
                    }
                }
                else
                {                    
                    if (!dicItemEquip.ContainsKey(equip.TempId))
                    {                     
                        dicItemEquip.Add(equip.TempId, equip);
                    }
                }
                if (equip.Place == EItemEquipPlace.None) return;
                //如果该位置有装备的话，先清数据
                if (dicUseEquip.TryGetValue(equip.Place, out var useEquip))
                {
                    useEquip.Place = EItemEquipPlace.None;
                    dicUseEquip.Remove(equip.Place);
                }
                dicUseEquip.Add(equip.Place, equip);
            }
        }


        /// <summary>
        /// 移除物体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public void RemoveItem(int TempID)
        {

           
            
        }


        /// <summary>
        /// 装备排序
        /// </summary>
        /// <param name="ItemList"></param>
        public void SortBag(ref List<TItemEquip> ItemEquipList)
        {
            ItemEquipList = ItemEquipList.OrderByDescending(c => c.Star).ThenByDescending(c => c.TemplId).ThenBy(c => c.Level).ToList();
        }

        /// <summary>
        /// 单机模式设置物品数据
        /// </summary>
        /// <param name="items"></param>
        public void SetData(TItems items)
        {
            Data = items;
            TItemProp prop;
            for (int i = 0; i < items.PropList.Count; i++)
            {
                prop = items.PropList[i];
                dicItemProp.Add(prop.TemplId, new ItemProp(prop));
            }
            TItemEquip equip;
            for (int i = 0; i < items.EquipList.Count; i++)
            {
                equip = items.EquipList[i];
                CreateEquip(equip);
            }
        }
        /// <summary>
        /// 联网模式设置物品数据
        /// </summary>
        
        public void SetData(SC_bag_list msg)
        {
            for (int i = 0; i < msg.EquipList.Count; i++)
            {
                TItemEquip equip = new TItemEquip();
                equip.SId = msg.EquipList[i].SID;
                equip.TemplId = msg.EquipList[i].TemplID;
                equip.Star = msg.EquipList[i].Breaklv;
                equip.Level = msg.EquipList[i].Level;
                equip.Index = msg.EquipList[i].Index;
                equip.Count = msg.EquipList[i].Num;
                Data.EquipList.Add(equip);
                CreateEquip(equip);
            }
            for (int i = 0; i < msg.PropList.Count; i++)
            {
                TItemProp prop = new TItemProp();
                prop.TemplId = msg.PropList[i].TemplID;
                prop.Count = msg.PropList[i].Num;
                Data.PropList.Add(prop);
            }
        }

        public override void Dispose()
        {
            dicItemProp.Clear();
            dicItemEquip.Clear();
            dicUseEquip.Clear();
            Data = null;
        }
    }
}
