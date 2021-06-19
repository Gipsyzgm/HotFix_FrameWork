using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix
{
    public class Utils
    {      
        public static Vector3 Vector3Null = new Vector3(-1000, -1000, -1000);
        private static Color[] QualityColor = new Color[]
        {
            new Color(0.5f, 0.5f, 0.5f, 1),       //c3d5ec
            new Color(0.764f, 0.835f, 0.925f, 1), //c3d5ec
            new Color(0.160f, 0.960f, 0.329f, 1), //29f554
            new Color(0.254f, 0.658f, 0.972f, 1), //41a8f8
            new Color(1f, 0.258f, 0.858f, 1),     //ff42db
            new Color(1f, 0.490f, 0, 1),          //ff7d00
        };

        private static string[] qualityColorStr = new string[]
        {
            "808080","c3d5ec", "29f554", "41a8f8", "ff42db", "ff7d00"
        };

        /// <summary>
        /// 获取品质颜色
        /// </summary>
        public static Color GetQualityColor(EQuality quality)
        {
            return GetQualityColor((int)quality);
        }

        public static Color GetQualityColor(int quality)
        {
            return QualityColor[quality];
        }

        public static string GetQualityColorStr(EQuality quality, string str)
        {
            return GetQualityColorStr((int)quality, str);
        }

        public static string GetQualityColorStr(int quality, string str)
        {
            return $"<color=#{qualityColorStr[quality]}>{str}</color>";
        }

        /// <summary>获取自定义颜色</summary>
        public static string GetColorStr(string color, string str)
        {
            return $"<color=#{color}>{str}</color>";
        }

        /// <summary>获取属性颜色（0-1）</summary>
        public static Color GetAttrColor(float index)
        {
            if (index < 0 || index > 1)
            {
                Debug.LogError("属性颜色输入参数错误，应为0-1");
                return Color.white;
            }
            return index <= 0.5 ? new Color(index * 2, 1, 0) : new Color(1, 1 - (index - 0.5f) * 2, 0);
        }

        /// <summary>
        /// 获取枚举值名称
        /// 如:GetEnumName(EQuality.Good)  返回优秀
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="addColon">是否后面加冒号，默认不加</param>
        /// <param name="meaning">多重意思编号,默认没有</param>
        /// <returns></returns>
        public static string GetEnumName<T>(T enu, bool addColon = false, int meaning = 0) where T : struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
                return string.Empty;
            string key = type.Name + "." + enu.ToString();
            if (meaning > 0) //此枚举有多重意思
                key += meaning;
            return HotMgr.Lang.Get(key) + (addColon ? ":" : string.Empty);
        }

       
    }

    /// <summary>
    /// 品质类型
    /// </summary>
    public enum EQuality
    {
        /// <summary>0废品(灰)</summary>
        Waste = 0,
        /// <summary>1普通(白)</summary>
        Normal = 1,
        /// <summary>0优秀(绿)</summary>
        Good = 2,
        /// <summary>3珍稀(蓝)</summary>
        Rare = 3,
        /// <summary>4绝世(紫)</summary>
        Peerless = 4,
        /// <summary>5传奇(橙)</summary>
        Tale = 5,
    }
}
