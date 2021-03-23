using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HotFix
{
    public partial class ConfigMgr
    {
        private int loadCount = 0; //加载资源数
        private int loadedCount = 0; //已经加载资源数

        private char splitFieldChar = '∴';
      
        /// <summary>
        /// 读取配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private async CTask readConfig<T>(Dictionary<object, T> source, bool isEncrypt = false) where T : BaseConfig, new()
        {
            loadCount += 1;
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await Addressables.LoadAssetAsync<UnityEngine.Object>(fileName).Task;
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                if (isEncrypt)
                    strconfig = DecryptDES(strconfig);
                if (IsCSV)
                {
                    using (StringReader sr = new StringReader(strconfig))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            T config = ConfigUtils.ToObject<T>(line.Split(splitFieldChar));
                            if (source.ContainsKey(config.UniqueID))
                                Debug.LogError($"表[{fileName}]中有相同键({config.UniqueID})");
                            else
                                source.Add(config.UniqueID, config);
                        }
                    }
                }
                else
                {
                    List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (source.ContainsKey(list[i].UniqueID))
                            Debug.LogError($"表[{fileName}]中有相同键({list[i].UniqueID})");
                        else
                            source.Add(list[i].UniqueID, list[i]);
                    }
                }
            }
            else
            {
                Debug.LogError($"配置文件不存在{fileName}");
            }
            loadedCount += 1;
        }


        private byte[] Keys = { 0x45, 0xDC, 0x37, 0xFB, 0xBC, 0xAB, 0xCD, 0xEF };
        private byte[] encryKeys = { 0xAB, 0x33, 0x37, 0x5C, 0xBB, 0x75, 0xC3, 0xAB };


        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string DecryptDES(string decryptString)
        {
            try
            {
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(encryKeys, Keys), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }


        /// <summary>
        /// 重新加载配置表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public async CTask ReloadConfig<T>(Dictionary<object, T> source) where T : BaseConfig, new()
        {
            string fileName = typeof(T).Name;
            source.Clear();
            UnityEngine.Object configObj = await Addressables.LoadAssetAsync<UnityEngine.Object>( fileName).Task;
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                if (IsCSV)
                {
                    using (StringReader sr = new StringReader(strconfig))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            T config = ConfigUtils.ToObject<T>(line.Split(splitFieldChar));
                            if (source.ContainsKey(config.UniqueID))
                                Debug.LogError($"表[{fileName}]中有相同键({config.UniqueID})");
                            else
                                source.Add(config.UniqueID, config);
                        }
                    }
                }
                else
                {
                    List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (source.ContainsKey(list[i].UniqueID))
                            Debug.LogError($"表[{fileName}]中有相同键({list[i].UniqueID})");
                        else
                            source.Add(list[i].UniqueID, list[i]);
                    }
                }
            }
            else
            {
                Debug.LogError($"配置文件不存在{fileName}");
            }
        }



        /// <summary>
        /// 读取竖表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        private async CTask<T> readConfigV<T>(bool isEncrypt = false) where T : BaseConfig, new()
        {
            string fileName = typeof(T).Name;
            UnityEngine.Object configObj = await Addressables.LoadAssetAsync<UnityEngine.Object>(fileName).Task;
            if (configObj != null)
            {
                string strconfig = configObj.ToString();
                if (isEncrypt)
                    strconfig = DecryptDES(strconfig);
                if (IsCSV)
                {
                    using (StringReader sr = new StringReader(strconfig))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            T config = ConfigUtils.ToObject<T>(line.Split(splitFieldChar));
                            return config;
                        }
                    }
                }
                else
                {
                    List<T> list = JsonMapper.ToObject<List<T>>(strconfig);
                    if (list.Count > 0)
                        return list[0];
                }
            }
            else
            {
                Debug.LogError($"配置文件不存在{fileName}");
            }
            return default(T);
        }

        private async CTask waitLoadComplate()
        {
            await CTask.WaitUntil(() => { return loadCount == loadedCount; });
        }
    }
}
