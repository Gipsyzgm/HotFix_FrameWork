using HotFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSocket
{
    public class CSocketUtils
    {       
        /// <summary>
        /// 消息加密
        /// </summary>
        public static byte[] MD5Encrypt(byte[] data, int protocol)
        {
            int time = (HotMgr.TimeMgr.ServerTimestamp + 300) / 600;
            byte[] random = BitConverter.GetBytes(time ^ protocol ^ (data.Length));
            byte[] newData = new byte[data.Length + random.Length];
            Array.Copy(random, 0, newData, 0, random.Length);  //协议号
            Array.Copy(data, 0, newData, random.Length, data.Length);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] outputL = md5.ComputeHash(newData);
            byte[] outputS = new byte[8];
            Array.Copy(outputL, 6, outputS, 0, 8);
            //跟据md5打乱数据
            for (int i = 0; i < data.Length; i++)
                data[i] += (byte)(outputS[i % outputS.Length]);
            return outputS;
        }
        /// <summary>
        /// 消息解密
        /// </summary>
        public static byte[] MD5Decode(byte[] encryBuff, ushort protocol)
        {
            try
            {
                byte[] body = new byte[encryBuff.Length - 2 - 8];            //消息内容
                byte[] md5 = new byte[8];
                Array.Copy(encryBuff, 2, body, 0, body.Length);
                Array.Copy(encryBuff, 2 + body.Length, md5, 0, md5.Length);
                for (int i = 0; i < body.Length; i++)
                    body[i] -= (byte)(md5[i % md5.Length]);

                int time = (HotMgr.TimeMgr.ServerTimestamp + 300) / 600;
                byte[] random = BitConverter.GetBytes(time ^ protocol ^ body.Length);
                byte[] newData = new byte[body.Length + random.Length];
                Array.Copy(random, 0, newData, 0, random.Length);  //协议号
                Array.Copy(body, 0, newData, random.Length, body.Length);
                MD5 md5Msg = new MD5CryptoServiceProvider();
                byte[] outputL = md5Msg.ComputeHash(newData);
                byte[] outputS = new byte[8];
                Array.Copy(outputL, 6, outputS, 0, 8);
                if (!CompareArray(outputS, md5))
                    return null;
                return body;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 数组比较是否相等
        /// </summary>
        /// <param name="bt1">数组1</param>
        /// <param name="bt2">数组2</param>
        /// <returns>true:相等，false:不相等</returns>
        public static bool CompareArray(byte[] bt1, byte[] bt2)
        {
            var len1 = bt1.Length;
            var len2 = bt2.Length;
            if (len1 != len2)
            {
                return false;
            }
            for (var i = 0; i < len1; i++)
            {
                if (bt1[i] != bt2[i])
                    return false;
            }
            return true;
        }
    }
}