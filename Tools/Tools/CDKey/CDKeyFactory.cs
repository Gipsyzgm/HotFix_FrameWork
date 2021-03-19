using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public class CDKeyFactory
    {
        private byte[] byte128;
        private long[] pow128;
        
        string[] key = new string[16] {
            "1Koz3=-#", "4832zO=-", "041a93-#", "5874aa43" ,
            "3R74619a", "E0*a882d", "!@d#f%3r", "&83f+-1f",
            "0/818a83", "1_&df*5%","^&*29jk3","2fe/2er3",
            "+/.,r123","*73n,(12","aR:d{1f2","31(2L:d1" };
        List<byte[]> byteKey = new List<byte[]>();
        public CDKeyFactory()
        {
            byte128 = new byte[128];
            for (byte i = 0; i < 128; i++)
                byte128[i] = i;

            pow128 = new long[8];
            for (int i = 0; i < 8; i++)
                pow128[i] = (long)Math.Pow(128, i);

            for (int i = 0; i < 16; i++)
                byteKey.Add(Encoding.UTF8.GetBytes(key[i]));
        }



        /// <summary>
        /// 生成CDKey
        /// </summary>
        /// <param name="id">CDKeyID</param>
        /// <param name="num">序号</param>
        public string CreateCDKey(int id, int num)
        {
            long str = id * 1000000 + num;
            string str128 = Convert10To128(str);
            int ki = id % 16;
            string encStr = EncryptDES(str128, byteKey[ki]);
            int x = Convert.ToInt32(encStr[12].ToString(), 16);
            int y = Convert.ToInt32(encStr[3].ToString(), 16);
            ki = (ki + x + y) % 16;
            return encStr.Insert(14, ki.ToString("x")).ToUpper();
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




        //10进制转换成36进制
        private string Convert10To128(long val)
        {
            StringBuilder result = new StringBuilder();
            while (val >= 128)
            {
                result.Append((char)(byte128[(int)(val % 128)]));
                val /= 128;
            }
            if (val >= 0)
                result.Append((char)(byte128[(int)(val % 128)]));
            return result.ToString();
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

        //// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        private string EncryptDES(string encryptString, byte[] key)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(key, key), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return BitConverter.ToString(mStream.ToArray()).ToLower().Replace("-", "");
            //return Convert.ToBase64String(mStream.ToArray());
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

    }
}
