using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    public class LocalDataMgr : BaseDataMgr<LocalDataMgr>, IDisposable
    {

        //如果是单机的话需要从这里获取玩家的数据
        public void LoadAll()
        {
            TPlayer player = Load<TPlayer>();
            bool isNewPlayer = false;
            if (player == null)
            {
                player = new TPlayer()
                {
                    Power = 20,
                    Level = 1,
                };
                isNewPlayer = true;
                player.Save();
            }                
            if (isNewPlayer) //加一个新装备
            {
                ItemMgr.I.AddNewItem(4001, 10, EItemEquipPlace.OutWeapon, false); //加一个外刀
                //ItemMgr.I.AddNewItem(4002, 1, EItemEquipPlace.OutWeapon, false); //加一个外刀
                //ItemMgr.I.AddNewItem(4003, 1, EItemEquipPlace.OutWeapon, false); //加一个外刀
                //ItemMgr.I.AddNewItem(4004, 1, EItemEquipPlace.OutWeapon, false); //加一个外刀
                //ItemMgr.I.AddNewItem(4005, 1, EItemEquipPlace.OutWeapon, false); //加一个外刀

                ItemMgr.I.AddNewItem(2001, 1, EItemEquipPlace.Hero, false);  //增加一个英雄面具
                //ItemMgr.I.AddNewItem(2002, 1, EItemEquipPlace.Hero, false);  //增加一个英雄面具

                ItemMgr.I.AddNewItem(5004, 1, EItemEquipPlace.InnerWeapon, false); //加一个内刀
                //ItemMgr.I.AddNewItem(5002, 1, EItemEquipPlace.None, false); //加一个内刀
                //ItemMgr.I.AddNewItem(5003, 1, EItemEquipPlace.None, false); //加一个内刀
                //ItemMgr.I.AddNewItem(5004, 1, EItemEquipPlace.None, false); //加一个内刀

                //ItemMgr.I.AddNewItem(6001, 1, EItemEquipPlace.None, false); //增加测试戒指A
                //ItemMgr.I.AddNewItem(6002, 1, EItemEquipPlace.None, false); //增加测试戒指B
                //ItemMgr.I.AddNewItem(6003, 1, EItemEquipPlace.None, false); //增加测试戒指C

                ItemMgr.I.AddNewItem(3001, 1, EItemEquipPlace.None, false); //增加宠物A
                //ItemMgr.I.AddNewItem(3002, 1, EItemEquipPlace.None, false); //增加宠物B
                if (Application.isEditor)
                {
                    ItemMgr.I.AddNewItem(1, 1000000, EItemEquipPlace.None, false); //增加金币
                }
                else
                {
                    ItemMgr.I.AddNewItem(1, 1000, EItemEquipPlace.None, false); //增加金币
                }


            }

            PlayerMgr.PlayerWarData.Recount();

            //CLog.Error(JsonMapper.ToJson(PlayerMgr.PlayerWarData));
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>() where T : BaseTable
        {
            string json = PlayerPrefs.GetString("Table_" + typeof(T).Name, string.Empty);
            T t = null;
            if (json != string.Empty)
                t = JsonMapper.ToObject<T>(json);
            return t;
        }

        /// <summary>
        /// 加载数据，不存在创建新的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadCreate<T>() where T : BaseTable, new()
        {
            string json = PlayerPrefs.GetString("Table_" + typeof(T).Name, string.Empty);
            T t = null;
            if (json != string.Empty)
                t = JsonMapper.ToObject<T>(json);
            if (t == null)
                t = new T();
            return t;
        }

    }
}
