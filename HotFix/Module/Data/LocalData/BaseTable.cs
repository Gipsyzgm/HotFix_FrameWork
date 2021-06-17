using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
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
