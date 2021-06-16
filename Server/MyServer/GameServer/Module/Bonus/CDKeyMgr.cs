using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using MongoDB.Bson;
using System.Collections;
using CommonLib;
using CommonLib.Comm.DBMgr;
using CommonLib.Comm;

namespace GameServer.Module
{
    public class CDKeyMgr
    {
        private byte[] byte128;
        private long[] pow128;

        string[] key = new string[16] {
            "1Koz3=-#", "4832zO=-", "041a93-#", "5874aa43" ,
            "3R74619a", "E0*a882d", "!@d#f%3r", "&83f+-1f",
            "0/818a83", "1_&df*5%","^&*29jk3","2fe/2er3",
            "+/.,r123","*73n,(12","aR:d{1f2","31(2L:d1" };
        List<byte[]> byteKey = new List<byte[]>();

        /// <summary>已使用过的CDKey [cdkeyId,cdkey序号] </summary>
        public DictionarySafe<int, Hashtable> cdkeyUsedList = new DictionarySafe<int, Hashtable>();
        public CDKeyMgr()
        {
            byte128 = new byte[128];
            for (byte i = 0; i < 128; i++)
                byte128[i] = i;

            pow128 = new long[8];
            for (int i = 0; i < 8; i++)
                pow128[i] = (long)Math.Pow(128, i);

            for (int i = 0; i < 16; i++)
                byteKey.Add(Encoding.UTF8.GetBytes(key[i]));

            //所有兑换过的CDKey信息
            DictionarySafe<ObjectId, TCDKey> cdkAll = DBReader.Instance.SelectAll<TCDKey>();
            foreach (TCDKey cdkey in cdkAll.Values)
            {
                Hashtable usedList;
                if (!cdkeyUsedList.TryGetValue(cdkey.cdkId, out usedList))
                {
                    usedList = new Hashtable();
                    cdkeyUsedList.Add(cdkey.cdkId, usedList);
                }
                usedList.Add(cdkey.num, null);
            }
        }
        

        /// <summary>
        /// 解析CDKey
        /// </summary>
        /// <param name="cdkey">cdkey</param>
        /// <returns>返回0为无效CDKey</returns>
        public long DecodeCDKey(string cdkey)
        {
            if (cdkey.Length != 17)
                return 0;
            long num = 0;
            try
            {
                int r = Convert.ToInt32(cdkey[14].ToString(), 16);
                int x = Convert.ToInt32(cdkey[12].ToString(), 16);
                int y = Convert.ToInt32(cdkey[3].ToString(), 16);
                int ki = (32 + r - x - y) % 16;
                cdkey = cdkey.Remove(14, 1);
                string deStr = DecryptDES(cdkey, byteKey[ki]);
                num = Convert128To10(deStr);
            }
            catch { }
            return num;
        }

        /// <summary>
        /// CDKey是否已使用
        /// </summary>
        public bool IsUsed(int id,int num)
        {
            Hashtable usedList;
            if (cdkeyUsedList.TryGetValue(id, out usedList))
            {
                if (usedList.ContainsKey(num))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获取玩家使用此类cdkey的数量
        /// </summary>
        /// <param name="player"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUserCount(Player player, int id)
        {
            if (player.cdkeyData != null)
            {
                int index = Array.IndexOf(player.cdkeyData.cdkIds, id);
                if (index != -1)
                    return player.cdkeyData.cdkCounts[index];
            }
            return 0;
        }
        /// <summary>
        /// 使用CDKey
        /// </summary>
        /// <param name="player"></param>
        /// <param name="id"></param>
        /// <param name="num"></param>
        public void UseCDKey(Player player, int id, int num)
        {
           
            CDKeyConfig config;
            if (!Glob.config.dicCDKey.TryGetValue(id, out config))
                return;
            List<int[]> itemsInfo = config.items;
            if (player.cdkeyData != null)
            {
                int index = Array.IndexOf(player.cdkeyData.cdkIds, id);
                if (index == -1)
                {
                    List<int> ids = player.cdkeyData.cdkIds.ToList();
                    ids.Add(id);
                    player.cdkeyData.cdkIds = ids.ToArray();

                    List<int> counts = player.cdkeyData.cdkCounts.ToList();
                    counts.Add(1);
                    player.cdkeyData.cdkCounts = counts.ToArray();
                }
                else
                {
                    player.cdkeyData.cdkCounts[index] = player.cdkeyData.cdkCounts[index] + 1;
                }
                player.cdkeyData.Update();
            }
            else
            {
                player.cdkeyData = new TCDKeyPlayer(player.ID);
                player.cdkeyData.cdkIds = new int[] { id };
                player.cdkeyData.cdkCounts = new int[] { 1 };
                player.cdkeyData.Insert();
            }
            //CDKey记录到已使用表中
            Hashtable usedList;
            if (!cdkeyUsedList.TryGetValue(id, out usedList))
            {
                usedList = new Hashtable();
                cdkeyUsedList.Add(id, usedList);
            }
            usedList.Add(num,null);

            TCDKey cdkey = new TCDKey(true);
            cdkey.cdkId = id;
            cdkey.num = num;
            cdkey.Insert();
           Glob.itemMgr.PlayerAddNewItems(player, itemsInfo, true);
        
        }

        //// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        private string DecryptDES(string decryptString, byte[] key)
        {
            int len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            for (int i = 0; i < len; i++)
            {
                int pos = i * 2;
                inputByteArray[i] = byte.Parse(decryptString.Substring(pos, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(key, key), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        /// <summary>
        /// 128进制转10进制
        /// </summary>
        /// <param name="str128"></param>
        /// <returns></returns>
        private long Convert128To10(string str128)
        {
            long result = 0;
            int len = str128.Length;
            for (int i = 0; i < len; i++)
                result += Array.IndexOf(byte128, (byte)(str128[i])) * pow128[i];
            return result;
        }

    }

}
