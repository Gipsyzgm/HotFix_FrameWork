using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{

    public static class StringHelper
    {
        /// <summary>
        /// 字符串分离到数组
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="str">源字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static T[] SplitToArr<T>(string str, char separator = ',')
        {
            if (str == string.Empty || str == null)
                return new T[] { };
            string[] objarr = str.Split(separator);
            T[] tArr = new T[objarr.Length];
            for (int i = 0; i < objarr.Length; i++)
                tArr[i] = (T)Convert.ChangeType(objarr[i], typeof(T));
            return tArr;
        }

        /// <summary>
        /// 物品数组分离到数组中
        /// </summary>
        /// <param name="itemsStr">[itemid1_num1,itemid2_num2]</param>
        /// <returns>[[itemid1,num1],[itemid2,num2]]</returns>
        public static List<int[]> SplitToItems(string[] itemsStr)
        {
            List<int[]> items = new List<int[]>();
            foreach (string itstr in itemsStr)
            {
                int[] iteminfo = SplitToArr<int>(itstr, '_');
                items.Add(new int[] { iteminfo[0], iteminfo[1] });
            }
            return items;
        }

        /**
     * 字符串长度限制 ,一个中文字符算二个长度
     * @param str 原始字符串
     * @param maxLen 最大长度
     * @return 返回限制长度后的字符串
     */
        public static string StringLenLimit(String str, int maxLen)
        {
            if (str.Length < maxLen / 2)
                return str;
            String reStr = string.Empty;
            int length = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((int)str[i] > 255)
                    length += 2;
                else
                    length += 1;
                if (length <= maxLen) reStr += str[i];
            }
            return reStr;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str, bool isLower = true)
        {
            byte[] result = Encoding.UTF8.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string rtn = BitConverter.ToString(output).Replace("-", "");
            if (isLower)
                return rtn.ToLower();
            return rtn;
        }

        /// <summary>
        /// HMACSHA1加密 返回Base64
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HMACSHA1(string str, string key)
        {
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(key));
            byte[] byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(str));
            return System.Convert.ToBase64String(byteText);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="value">The value to Url encode</param>
        /// <returns>Returns a Url encoded string</returns>
        public static string UrlEncode(string value)
        {
            StringBuilder result = new StringBuilder();
            string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Base64字符串解码
        /// </summary>
        /// <param name="base64Str">base64源串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string Base64Decode(string base64Str)
        {
            byte[] outputb = Convert.FromBase64String(base64Str);
            base64Str = Encoding.UTF8.GetString(outputb);
            return base64Str;
        }

        /// <summary>
        /// 去年json字符串里带的转义符
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static string FixJson(this string strJson)
        {
            strJson = strJson.TrimStart('\"');
            strJson = strJson.TrimEnd('\"');
            strJson = strJson.Replace("\\", "");
            return strJson;
        }


        public static IPEndPoint ToIPEndPoint(string ip, int port)
        {
            if (ip == "Any")
                return new IPEndPoint(IPAddress.Any, port);
            else
                return new IPEndPoint(IPAddress.Parse(ip), port);
        }

        /// <summary>
        /// 生成客户端登录口令[Login生成 GS验证]
        /// </summary>
        /// <param name="pfId">账号id</param>
        /// <param name="serverId">服务器id</param>
        /// <param name="time">时间戳</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string CreateToken(string pfId, int serverId, int time, string key)
        {
            return StringHelper.MD5(string.Format("{0}{1}{2}{3}", pfId, serverId, time, key));
        }
        //获取本地IP
        public static string GetLocalIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    return AddressIP;
                }
            }
            return AddressIP;
        }



    }
}