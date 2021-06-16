using MongoDB.Bson;
using MongoDB.Driver;
using PbPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    /*
    public partial class Player
    {
        /// <summary>
        /// 玩家推送礼包所有数据
        /// </summary>
        public DictionarySafe<int, PayPackData> PayPackList = new DictionarySafe<int, PayPackData>();
        /// <summary>
        /// 需要 packMgr 验证的礼包
        /// </summary>
        public List<PayPackConfigDataBase> isAciontPack = new List<PayPackConfigDataBase>();      
        /// <summary>
        /// 发送客户端的礼包
        /// </summary>
        public Dictionary<int, PayPackData> sendPackId = new Dictionary<int, PayPackData>();
        /// <summary>
        /// 玩家礼包数据
        /// </summary>
        public PayPackPlayerData PayPackPlayerData;
        /// <summary>
        /// 最多可以显示4组礼包
        /// </summary>        
        public bool isSend = true;
        public void SetPlayerPayPackData(List<TPayPack> packList)
        {
            PayPackData data = null;
            foreach (TPayPack t in packList)
            {
                data = new PayPackData(this, t);
                if (PayPackPlayerData.Data.isPackMark != Glob.payPackMgr.PackMark)
                {
                    data.Data.isAction = true;
                    data.Data.Update();
                }               
                PayPackList.Add(t.PackId, data);
            }
            if (PayPackPlayerData.Data.isPackMark != Glob.payPackMgr.PackMark)
            {
                PayPackPlayerData.Data.isPackMark = Glob.payPackMgr.PackMark;
                PayPackPlayerData.Data.Update();
            }            
            initActionPack();
        }

       /// <summary>
       /// 根据礼包 礼包组 判断玩家身上礼包 查找礼包ID 送到 礼包管理器检测是否到达开启条件
       /// </summary>
        public void initActionPack()
        {
            isAciontPack.Clear();
            PayPackConfigDataBase groupPack = null;
            bool GROUPSHOW = false;  //每组只要有一个是为显示就不添加新的
            foreach (List<PayPackConfigDataBase> ConfigDataBaseList in Glob.payPackMgr.GroupPack.Values)
            {
                PayPackConfigDataBase DataBaseTime = ConfigDataBaseList.ElementAt(0);
                if (DataBaseTime.DataBase.startTime.ToTimestamp() <= DateTime.Now.ToTimestamp() &&
                 DateTime.Now.ToTimestamp() <= DataBaseTime.DataBase.endTime.ToTimestamp())   //当前礼包组必须在开启时间范围内
                {
                    GROUPSHOW = false;
                    foreach (PayPackConfigDataBase packIsShow in ConfigDataBaseList)
                    {
                        if (PayPackList.TryGetValue(packIsShow.DataBase.packId, out PayPackData showVal))
                        {
                            if (showVal.Data.isShow)
                                GROUPSHOW = true;
                        }
                    }
                    if (!GROUPSHOW) // 当前组内没有一个是为显示的 就送PackMgr 检测
                    {
                        groupPack = null;
                        foreach (PayPackConfigDataBase configDataBase in ConfigDataBaseList)
                        {
                            if (groupPack != null)
                                continue;
                            if (PayPackList.TryGetValue(configDataBase.DataBase.packId, out PayPackData packData))
                            {
                                if (packData.Data.isAction)
                                    groupPack = configDataBase;
                            }
                            else
                                groupPack = configDataBase;
                        }
                    }
                    if (groupPack != null)
                    {
                        isAciontPack.Add(groupPack);
                    }
                }
            }
            
    
        }
        /// <summary>
        /// 设置为已购买
        /// </summary>
        /// <param name="PackId"></param>
        public void UpPlayerPayPack(int PackId) 
        {

            if (PayPackList.TryGetValue(PackId, out PayPackData data))
            {
                SC_player_payOkPack msg = new SC_player_payOkPack();
                if (data.Config.IsGroup == 1 && data.Config.GroupBuy == 1)  // 如果是礼包组 并且礼包组限制购买次数  
                {
                    if (Glob.config.dicPackGroupIds.TryGetValue(data.Config.PackGroup, out List<int> packList))
                    {
                        foreach (int id in packList)
                        {
                            if (PayPackList.TryGetValue(id, out PayPackData groupPack))
                            {
                                groupPack.Data.payNum -= 1;
                                if (groupPack.Data.payNum == 0)
                                    groupPack.Data.isShow = false;
                                groupPack.Data.isPay = true;
                                groupPack.Data.Update();
                                msg.Info.Add(groupPack.GetPackMsg());
                            }
                        }
                    }
                }
                else
                {
                    data.Data.payNum -= 1;
                    if (data.Data.payNum == 0)
                        data.Data.isShow = false;
                    data.Data.isPay = true;
                    data.Data.Update();
                    msg.Info.Add(data.GetPackMsg());
                }
                this.Send(msg);
                initActionPack();
                Glob.payPackMgr.SendCientPayPack(this);
            }
        }

        /// <summary>
        /// 添加玩家身上 更新验证礼包
        /// </summary>
        /// <param name="con"></param>
        public void AddPayPack(List<PayPackConfigDataBase> conList)
        {
            foreach (PayPackConfigDataBase con in conList)
            {
                if (!CheckPackGroup())
                    continue;
                isAciontPack.Remove(con);
                if (con.DataBase.isGroup == 1)
                {
                    if (Glob.payPackMgr.GroupPack.TryGetValue(con.DataBase.packGroup, out List<PayPackConfigDataBase> baseVal))
                        InsertPackConfigIds(baseVal);
                }
                else
                {
                    InsertPackConfig(con);
                }
            }
            initActionPack();
            Glob.payPackMgr.SendCientPayPack(this);
        }


        /// <summary>
        /// 判断玩家身上不可超过4组礼包
        /// </summary>
        /// <returns></returns>
        public bool CheckPackGroup()
        {
            List<int> group = new List<int>();
            foreach (PayPackData data in PayPackList.Values) 
            {
                if (!group.Contains(data.Config.PackGroup) && data.Data.isShow)
                    group.Add(data.Config.PackGroup);
            }
            int num = Glob.payPackMgr.GroupNum;
            if (group.Contains(Glob.payPackMgr.OnePay))
                num += 1;
            return group.Count < num ;
        }


        /// <summary>
        /// 添加推送礼包 在玩家身上就修改  不在就添加 
        /// </summary>
        /// <param name="c"></param>
        private void InsertPackConfig(PayPackConfigDataBase c) 
        {
            TPayPack pay = null;
            if (PayPackList.TryGetValue(c.DataBase.packId, out PayPackData val))
                pay = val.Data;
            if (pay == null)
            {
                pay = new TPayPack(true);
                pay.pid = this.ID;
                pay.PackId = c.DataBase.packId;
                if (c.Config.Time != 0)
                    pay.PackDate = DateTime.Now.AddHours(c.Config.Time);
                pay.openDate = DateTime.Now;
                pay.isPay = false;
                pay.isShow = true;
                pay.isAction = false;
                pay.payNum = c.Config.GetNum;
                pay.Insert();
                Glob.cylogMgr.LogPayPack(this, pay.PackId, pay.PackDate.ToTimestamp());
            }
            else {
                if (c.Config.Time != 0)
                    pay.PackDate = DateTime.Now.AddHours(c.Config.Time);
                pay.openDate = DateTime.Now;
                pay.isPay = false;
                pay.isShow = true;
                pay.payNum = c.Config.GetNum;
                pay.isAction = false;
                pay.Update();
                Glob.cylogMgr.LogPayPack(this, pay.PackId, pay.PackDate.ToTimestamp());
            }
            PayPackList.AddOrUpdate(pay.PackId, new PayPackData(this, pay));

        }


        /// <summary>
        /// 添加组合礼包  在玩家身上就修改  不在就添加 
        /// </summary>
        /// <param name="ids"></param>
        private void InsertPackConfigIds(List<PayPackConfigDataBase> packs)
        {
            TPayPack pay = null;   
            foreach (PayPackConfigDataBase packBase in packs)
            {
                pay = null;
                if (PayPackList.TryGetValue(packBase.DataBase.packId, out PayPackData val))
                    pay = val.Data;
                if (pay == null)
                {
                    pay = new TPayPack(true);
                    pay.pid = this.ID;
                    pay.PackId = packBase.DataBase.packId;
                    if (packBase.Config.Time != 0)
                        pay.PackDate = DateTime.Now.AddHours(packBase.Config.Time);
                    pay.openDate = DateTime.Now;
                    pay.isPay = false;
                    pay.isShow = true;
                    pay.isAction = false;
                    pay.payNum = packBase.Config.GetNum;
                    pay.Insert();
                    Glob.cylogMgr.LogPayPack(this, pay.PackId, pay.PackDate.ToTimestamp());
                }
                else {
                    if (packBase.Config.Time != 0)
                        pay.PackDate = DateTime.Now.AddHours(packBase.Config.Time);
                    pay.openDate = DateTime.Now;
                    pay.isPay = false;
                    pay.isShow = true;
                    pay.isAction = false;
                    pay.payNum = packBase.Config.GetNum;
                    pay.Update();
                    Glob.cylogMgr.LogPayPack(this, pay.PackId, pay.PackDate.ToTimestamp());
                }             
                PayPackList.AddOrUpdate(pay.PackId, new PayPackData(this, pay));
            }
            
        }

    }*/
}
