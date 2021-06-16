using CommonLib;
using CommonLib.Comm;
using CommonLib.Comm.DBMgr;
using MongoDB.Bson;
using PbBag;
using PbCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class ItemMgr
    {
        ///// <summary>
        ///// 全部玩家装备集合
        ///// </summary>
        DictionarySafe<ObjectId, DictionarySafe<int, TItemEquip>> playerEquipList = new DictionarySafe<ObjectId, DictionarySafe<int, TItemEquip>>();

        public ItemMgr()
        {
            List<TItemEquip> equipList = DBReader.Instance.SelectAllList<TItemEquip>();

            DictionarySafe<int, TItemEquip> playEquipList;
            foreach (TItemEquip equip in equipList)
            {
                if (!playerEquipList.TryGetValue(equip.pId, out playEquipList))
                {
                    playEquipList = new DictionarySafe<int, TItemEquip>();
                    playerEquipList.Add(equip.pId, playEquipList);
                }
                playEquipList.AddOrUpdate(equip.shortId, equip);
            }
        }

        /// <summary>
        /// 读取玩家装备数据
        /// </summary>
        /// <param name="player"></param>
        public void ReadPlayerEquip(Player player)
        {
            DictionarySafe<int, TItemEquip> list;
            if (playerEquipList.TryGetValue(player.ID, out list))
            {
                foreach (TItemEquip tEquip in list.Values)
                {
                    PlayerAddEquip(player, tEquip);
                }
            }
        }

        /// <summary>
        /// 玩家增加一个新的物品
        /// 同时增加多个使用 PlayerAddNewItems
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="templId">物品模板Id</param>
        /// <param name="count">数量</param>
        /// <param name="isSend">是否通知客户端，默认不通知</param>
        /// <returns></returns>
        public void PlayerAddNewItem(Player player, int templId, int count = 1,bool isSend = true, Enum_bag_itemsType type = Enum_bag_itemsType.BiNone)
        {
            if (count <= 0)
                return;
            PlayerAddNewItems(player, new List<int[]>() { new int[] { templId, count } }, isSend, type);
        }

        /// <summary>
        /// 验证玩家虚拟资源是否超最大数   返回只能领取取大资源
        /// </summary>
        /// <param name="player">玩家ID</param>
        /// <param name="itemsInfo">应该添加的资源</param>
        /// <param name="heroMax">英雄是否可超过 默认不可超</param>
//        public List<int[]> isPlayerMaxItems(Player player, List<int[]> itemsInfo,bool heroMax=false)
//        {

//            ItemConfig _config;
//            List<int[]> items = new List<int[]>();
//            foreach (int[] item in itemsInfo)
//            {
//                if (item[0] < 0)
//                {
////                     if (!player.CheckHeroNum() || heroMax)
////                         items.Add(item);
//                }
//                else {                    
//                    if (Glob.config.dicItem.TryGetValue(item[0], out _config))
//                    {
//                        if ((EItemType)_config.type == EItemType.Virtual)
//                        {
//                            EItemSubTypeVirtual itemType = (EItemSubTypeVirtual)_config.subType;
//                            //switch (itemType)
//                            //{
//                            //    case EItemSubTypeVirtual.Food://食物
//                            //        if (player.Data.food + item[1] > player.FoodMax)
//                            //        {
//                            //            item[1] = player.FoodMax < player.Data.food ? 0 : player.FoodMax - player.Data.food;
//                            //        }
//                            //        break;
//                            //    case EItemSubTypeVirtual.Stone://石头
//                            //        if (player.Data.stone + item[1] > player.StoneMax)
//                            //        {
//                            //            item[1] = player.StoneMax < player.Data.stone ? 0 : player.StoneMax - player.Data.stone;
//                            //        }
//                            //        break;
//                            //    case EItemSubTypeVirtual.People://人口
//                            //        if (player.Data.people + item[1] > player.PeopleMax)
//                            //        {
//                            //            item[1] = player.PeopleMax < player.Data.people ? 0 : player.PeopleMax - player.Data.people;
//                            //        }
//                            //        break;
//                            //}
//                        }
//                        items.Add(item);
//                    }
//                }                         
//            }
//            return items;
//        }

        /// <summary>
        /// 一次获得多个物品
        /// </summary>
        /// <param name="player">玩家对象</param>
        /// <param name="itemsInfo">List[模板Id,数量]</param>
        /// <param name="isSend">是否通知客户端，默认通知</param>
        public void PlayerAddNewItems(Player player, List<int[]> itemsInfo, bool isSend = true, Enum_bag_itemsType type = Enum_bag_itemsType.BiNone)
        {
            if (itemsInfo == null || itemsInfo.Count <= 0)
                return;
            ItemConfig _config;
            int templId = 0;
            int count = 0;

            SC_bag_newItems newitems = new SC_bag_newItems();
            One_bag_item item;
            for (int i = 0; i < itemsInfo.Count; i++)
            {
                templId = itemsInfo[i][0];
                count = itemsInfo[i][1];
                if (count <= 0)
                    continue;

                if (!Glob.config.dicItem.TryGetValue(templId, out _config))
                {
                    Logger.LogError($"{player.ID}的物品创建失败,找不到模板数据{templId}");
                    continue;
                }

                EItemType itemType = (EItemType) _config.type;
                switch (itemType)
                {
                    case EItemType.Virtual: //虚拟物品，增加数对应数据对角色身上
                        player.AddVirtualItemNum((EItemSubTypeVirtual) _config.subType, count, false, type);
                        if (_config.subType == (int)EItemSubTypeVirtual.Exp)
                            continue;
                        item = new One_bag_item()
                        {
                            TemplID = templId,
                            Num = count
                        };
                        newitems.Items.Add(item);
                        break;
                    case EItemType.Hero:
                        /*int inum = 0;*/
                        for (int c = 0; c < count; c++)
                        {
                            if(Glob.hreoMgr.PlayerAddNewHero(player, templId, type) == 0)
                            {
                                item = new One_bag_item()
                                {
                                    TemplID = templId,
                                    Level = 1,
                                    BreakLv = 1,
                                    IsGetHero = true,
                                };
                                newitems.Items.Add(item);
                                isSend = true;
                            }
                            else
                            {
                                isSend = false;
                            }
                        }
                        break;
                    case EItemType.Pet: //英雄装备
                    case EItemType.OutCut:
                    case EItemType.InCut:
                    case EItemType.Ring:
                    case EItemType.Defence:
                        for (int c = 0; c < count; c++)
                        {
                            int blv = templId % 10;
                            TItemEquip equip = new TItemEquip(true);
                            equip.templId = templId;
                            equip.pId = player.ID;
                            equip.level = 1;
                         
                            equip.Insert();
                            PlayerAddEquip(player, equip);
                            item = new One_bag_item()
                            {
                                SID = equip.shortId,
                                TemplID = templId,
                                Level = 1,
                                BreakLv = blv
                            };
                            newitems.Items.Add(item);
                        }

                        //for (int j = 0; j < count; j++)
                        //{

                        //    PlayerAddEquip(player, equip);
                        //    item = new One_bag_item()
                        //    {   
                        //        TemplID = templId,
                        //        Num = 0,
                        //        Level = equip.level,
                        //        //Exp = equip.exp
                        //    };
                        //    newitems.Items.Add(item);
                        //}
                        //int sum = player.equipList.Values.Count(t => t.Data.templId == templId);
                        //Glob.cylogMgr.LogItem(player, _config, (int)type, count, sum, 1);

                        break;
                    case EItemType.Prop: //道具
                    //case EItemType.Materials: //材料
                    //case EItemType.PlayerIcon://玩家头像
                        ItemProp prop;
                        if (!player.propList.TryGetValue(templId, out prop))
                        {
                            TItemProp tprop = new TItemProp(true);
                            tprop.pId = player.ID;
                            tprop.templId = templId;
                            tprop.count = count;
                            tprop.Insert();
                            PlayerAddProp(player, tprop);
                        }
                        else
                        {
                            prop.Data.count += count;
                            prop.Data.Update();
                        }

                        item = new One_bag_item()
                        {
                            TemplID = templId,
                            Num = count
                        };
                        newitems.Items.Add(item);
                        ////触发道具兑换的任务检测
                        //player.TriggerTask(ETaskType.Item_Collect, 1);                            
                        int num = player.propList.Values.Count(t => t.Data.templId == templId);
                      
                      
                        break;
                }
                
            }
            //通知客户端获得装备或道具
            if (isSend)
            {
                newitems.ItemsType = type;
                player.Send(newitems);
            }
        }

        /// <summary>
        /// 创建玩家初始装备
        /// </summary>
        /// <param name="player"></param>
        public void PlayerCreateInitEpuip(Player player)
        {
            //PlayerInitConfig config = Glob.config.playerInitConfig;
            //for (int i = 0; i < config.initEpuips.Length; i++)
            //{
            //    TItemEquip equip = new TItemEquip(true);
            //    equip.templId = config.initEpuips[i];
            //    equip.pId = player.ID;
            //    equip.level = 1;
            //    if(!Glob.config.dicItem.TryGetValue(config.initEpuips[i],out var itemConfig))
            //    {
            //        Logger.LogError($"物品表未找到初始装备ID:{config.initEpuips[i]}");
            //    }
            //    EItemSubTypeEquipIndex index = EItemSubTypeEquipIndex.None;
            //    switch(itemConfig.type)
            //    {
            //        case 3:
            //            index = EItemSubTypeEquipIndex.Pet;
            //            break;
            //        case 4:
            //            index = EItemSubTypeEquipIndex.OutCut;
            //            break;
            //        case 5:
            //            index = EItemSubTypeEquipIndex.InCut;
            //            break;
            //        case 6:
            //            index = EItemSubTypeEquipIndex.Ring1;
            //            break;
            //        case 7:
            //            index = EItemSubTypeEquipIndex.Armor;
            //            break;
            //    }
            //    equip.Insert();
            //    PlayerAddEquip(player, equip);
            //}
        }
        /// <summary>
        /// 创建英雄默认内刀
        /// </summary>
        /// <param name="player"></param>
        /// <param name="hero"></param>
        public void PlayerCreateInitInCut(Player player,THero hero,bool IsInit = false)
        {
            if(!Glob.config.dicItem.TryGetValue(hero.templId,out var itemConfig))
            {
                Logger.LogError(hero.templId + "物品表没有找到id,请检查...");
                return;
            }
            int cutid = itemConfig.DefHeroInside;
            int index = 0;
            if(IsInit)
                index = (int)EItemSubTypeEquipIndex.InCut;
            if (!Glob.config.dicItem.TryGetValue(cutid, out var config))
            {
                Logger.LogError(cutid + "物品表没有找到id,请检查...");
                return;
            }

            TItemEquip equip = new TItemEquip(true);
            equip.templId = cutid;
            equip.pId = player.ID;
            equip.level = 1;       
            equip.Insert();
            PlayerAddEquip(player, equip);

            if (!IsInit)
            {
                if (!player.equipList.TryGetValue(equip.shortId, out var item))
                {
                    Logger.LogError("找不到初始内刀数据");
                    return;
                }
                SC_bag_newItems msg = new SC_bag_newItems();
                One_bag_item one = new One_bag_item();
                one.SID = item.SID;
                one.TemplID = cutid;
                one.Level = item.Level;
                msg.ItemsType = Enum_bag_itemsType.BiSummon;
                msg.Items.Add(one);
                player.Send(msg);
            }


        }
        /// <summary>
        /// 增加装备,装备属性在传进来之前改变
        /// 装备自动插入数据库
        /// </summary>
        /// <param name="player"></param>
        /// <param name="titem"></param>
        public void PlayerCreateEquip(Player player, TItemEquip equip, Enum_bag_itemsType type = Enum_bag_itemsType.BiNone)
        {
            equip.pId = player.ID;

            //equip.exp = 0;
            equip.Insert();
            PlayerAddEquip(player, equip);
            One_bag_item item = new One_bag_item()
            {
                SID = equip.shortId,
                TemplID = equip.templId,            
                Level = equip.level,

            };
            SC_bag_newItems newitems = new SC_bag_newItems();
            newitems.Items.Add(item);
            newitems.ItemsType = type;
            player.Send(newitems);
        }


        /// <summary>
        /// 增加装备
        /// </summary>
        /// <param name="player"></param>
        /// <param name="titem"></param>
        public void PlayerAddEquip(Player player, TItemEquip titem)
        {
            ItemEquip item = new ItemEquip(player.ID, titem);
            if (!item.IsSucceed)
                return;
            player.equipList.AddOrUpdate(titem.shortId, item);

            DictionarySafe<int, TItemEquip> playEquipList;
            if (!playerEquipList.TryGetValue(titem.pId, out playEquipList))
            {
                playEquipList = new DictionarySafe<int, TItemEquip>();
                playerEquipList.Add(titem.pId, playEquipList);
            }
            playEquipList.AddOrUpdate(titem.shortId, titem);          
        }
        
        //增加道具
        public void PlayerAddProp(Player player, TItemProp titem)
        {
            ItemProp item = new ItemProp(player.ID, titem);
            if (!item.IsSucceed)
                return;
            player.propList.Add(titem.templId, item);
        }

        /// <summary>
        /// 删除装备
        /// </summary>
        /// <param name="player"></param>
        /// <param name="equip"></param>
        public void RemoveEquip(Player player, ItemEquip equip)
        {
            playerEquipList[player.ID].Remove(equip.SID);
            player.equipList.Remove(equip.SID);
            equip.Data.Delete();

            //int num = player.equipList.Values.Count(t => t.Data.templId == equip.Config.id);
            //Glob.cylogMgr.LogItem(player, equip.Config, (int)type, 1, num, -1);
        }

        
        /// <summary>
        /// 发送玩家背包数据 装备，道具
        /// </summary>
        /// <param name="player"></param>
        public void SendPlayerBag(Player player)
        {
            //装备信息
            SC_bag_list bagList = new SC_bag_list();
            One_bag_equip equip;
            foreach (ItemEquip item in player.equipList.Values)
            {
                equip = item.GetEquipInfo();
                bagList.EquipList.Add(equip);
            }

            foreach (Hero item in player.heroList.Values)
            {
                equip = item.GetHeroInfo();
                bagList.EquipList.Add(equip);
            }
            foreach(var one in Glob.config.dicItem.Values)
            {
                if (one.type != (int)EItemType.Hero || player.heroList.ContainsKey(one.id) || one.id % 10 != 1)
                    continue;
                equip = new One_bag_equip();
                equip.TemplID = one.id;
                equip.Level = 1;
                equip.Breaklv = 1;
                bagList.EquipList.Add(equip);
            }
            //道具信息
            One_bag_prop prop;
            foreach (ItemProp item in player.propList.Values)
            {
                prop = new One_bag_prop()
                {
                    TemplID = item.TemplID,
                    Num = item.Data.count
                };
                bagList.PropList.Add(prop);
            }
            
            player.Send(bagList);
        }

    }
}
