﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    /// <summary>
    /// 提供主工程调用的接口
    /// </summary>
    public class ILRMainCall
    {

        public static void Test()
        {
           Debug.Log("Test1");
             
        }

        public static string Test1()
        {
            Debug.Log("Test1返回值");
            return "返回值";

        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLang(string key, int type = -1)
        {
            if (type == -1)
                return HotMgr.Lang.Get(key);
            return HotMgr.Lang.Get(key, (ELangType)type);
        }

        /// <summary>
        /// 广告的回调
        /// </summary>
        public static void OnRewarded(string ID)
        {        
            int id = int.Parse(ID);
            switch (id)
            {
                case 1:                  
                    break;             
                default:
                    break;
            }
        }
    }
}
