using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    //所有需要存在本地的数据的基类，可根据自己的需求扩展类型。
    //理论上单机数据有玩家和道具数据可以了。
    public class BaseTable
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save()
        {
            string json = JsonMapper.ToJson(this);
            PlayerPrefs.SetString("Table_" + this.GetType().Name, json);
        }
    }
}
