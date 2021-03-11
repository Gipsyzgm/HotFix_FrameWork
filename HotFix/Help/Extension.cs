using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    public static class Extension
    {
        /// <summary>
        /// 获取字符串全角长度，一个汉字算二个
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetFullLength(this string str)
        {
            if (str.Length == 0) return 0;
            ASCIIEncoding ascii   = new ASCIIEncoding();
            int           tempLen = 0;
            byte[]        s       = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int) s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }

        /// <summary>
        /// 判断是否有非法字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckIllegalChar(this string str)
        {
            Regex reg = new Regex("[?!@#$%\\^&*()]+");
            Match m   = reg.Match(str);
            return m.Success;
        }

        /// <summary>
        /// 保证返回数据为范围内的数值.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">待测值.</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>T.</returns>
        public static T Limit<T>(this T val, T min, T max) where T : IComparable
        {
            var maxVal = (val.CompareTo(min)    > 0) ? val : min;
            var minVal = (maxVal.CompareTo(max) > 0) ? max : maxVal;
            return minVal;
        }


        public static Vector2 ToVector2(this int[] val)
        {
            return new Vector2(val[0], val[1]);
        }

        public static Vector3 ToVector3(this int[] val)
        {
            return new Vector3(val[0], val[1], val[3]);
        }


        /// <summary>
        /// 万分比值 转成  百分比字符串  1=0.01%
        /// </summary>
        /// <param name="myriadVal"></param>
        /// <returns></returns>
        public static string ToPctString(this int val)
        {
            return val / 100f + "%";
        }

        /// <summary>
        /// 货币显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToMoney(this int val)
        {
            return ToMoney((long) val);
        }

        /// <summary>
        /// 货币显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToMoney(this long val)
        {
            //K M G B T
            string sv = string.Empty;
            if (val < 1000)
                sv = val.ToString();
            else if (val < 100000)
                sv = (val / 1000f).ToString("##.#") + "K";
            else if (val < 1000000) //<1000K
                sv = (val / 1000) + "K";
            else if (val < 100000000)
                sv = (val / 1000000f).ToString("##.#") + "M";
            else
                sv = (val / 1000000) + "M";
            return sv;
        }

        /// <summary>
        /// 血量显示转换
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToHP(this int val)
        {
            return ToMoney((long) val);
        }


        public static string GetUTF8String(this byte[] buffer)
        {
            if (buffer == null)
                return null;
            if (buffer.Length <= 3)
                return Encoding.UTF8.GetString(buffer);
            byte[] bomBuffer = new byte[] {0xef, 0xbb, 0xbf};
            if (buffer[0] == bomBuffer[0] && buffer[1] == bomBuffer[1] && buffer[2] == bomBuffer[2])
                return new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// 颜色码转换成Color
        /// </summary>
        /// <param name="colorStr"></param>
        /// <returns></returns>
        public static Color ToColor(this string colorStr)
        {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString(colorStr, out color);
            return color;
        }

        public static T GetAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T compent = gameObject.GetComponent<T>();
            if (compent == null)
                compent = gameObject.AddComponent<T>();
            return compent;
        }

        public static RectTransform RectTransform(this GameObject gameobject)
        {
            return gameobject.GetComponent<RectTransform>();
        }

        public static RectTransform RectTransform(this Transform transform)
        {
            return transform.GetComponent<RectTransform>();
        }


        /// <summary>
        /// 剩余时间显示(带时间符号)
        /// 12m 22s
        /// </summary>
        /// <returns></returns>
        public static string IntToTimeStr(this int val)
        {
            string time = string.Empty;
            if (val < 60)
                time = val + "s";
            else if (val < 3600)
                time = (val / 60) + "m " + val % 60 + "s";
            else if (val < 86400)
                time = (val / 3600) + "h " + ((val % 3600) / 60) + "m";
            else if (val < 2592000)
                time = (val / 86400) + "d " + (val % 86400 / 3600) + "h";
            else
                time = (val / 2592000) + "M " + (val % 2592000 / 86400) + "d";
            return time;
        }

        /// <summary>
        /// 剩余时间显示(不带时间符号)
        /// 2:12:22
        /// </summary>
        /// <returns></returns>
        public static string IntToTimeStr_noSymbol(this int val)
        {
            string time = string.Empty;
            if (val < 60)
                time = $"{val}s";
            else if (val < 3600)
                time = $"{val / 60}:{val % 60:D}";
            else
                time = $"{val / 3600}:{val % 3600 / 60:D}:{val % 3600 % 60:D}";
            return time;
        }
    }
}