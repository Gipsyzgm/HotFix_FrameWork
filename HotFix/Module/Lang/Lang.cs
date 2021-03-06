﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HotFix { 

    public class Lang
    {        
        public string key { get; set; }
        public Lang(string _key)
        {
            key = _key;
        }
        public Lang()
        {

        }
        /// <summary>
        /// 系统黓认语言
        /// </summary>
        public string Value
        {
            get
            {
                return HotMgr.Lang.Get(key);
            }
        }

        /// <summary>
        /// 跟据语言类型获取值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetValue(ELangType type)
        {
            return HotMgr.Lang.Get(key, type);
        }

    }
}
