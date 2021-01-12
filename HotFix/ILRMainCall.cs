using System;
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



    }
}
