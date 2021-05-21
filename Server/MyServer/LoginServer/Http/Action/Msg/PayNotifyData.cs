using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CommonLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoginServer.Http
{
    public class PayNotifyData
    {
        /// <summary>
        /// 对json订单信息一次base64加密后的值
        /// </summary>
        public string receipt;
        /// <summary>
        /// 数字签名
        /// </summary>
        public string sign;
        /// <summary>
        /// 透传参数(结构 PID_游戏订单号_商品Id_平台类型)
        /// </summary>
        public string pushInfo;

        /// <summary>
        /// receipt的json对象
        /// </summary>
        [JsonIgnore]
        private JObject jsonReceipt;

        public static PayNotifyData Deserialize(string dataStr)
        {
            NameValueCollection args = GetPostParams(dataStr);
            
            string receipt = args.Get("receipt");
            string pushInfo = args.Get("pushInfo");
            string sign = args.Get("sign");

            if (string.IsNullOrEmpty(receipt) || string.IsNullOrEmpty(pushInfo) || string.IsNullOrEmpty(sign))
                return null;

            PayNotifyData data = new PayNotifyData(receipt, pushInfo, sign);
            return data;
            //return JsonConvert.DeserializeObject<PayNotifyData>(dataStr);
        }

        /// <summary>
        /// 获取post参数
        /// </summary>
        /// <param name="POSTStr"></param>
        /// <returns></returns>
        protected static NameValueCollection GetPostParams(string POSTStr)
        {
            NameValueCollection list = new NameValueCollection();
            int index = POSTStr.IndexOf("&");
            string[] Arr = { };
            if (index != -1) //参数传递不只一项
            {
                Arr = POSTStr.Split('&');
                for (int i = 0; i < Arr.Length; i++)
                {
                    int equalindex = Arr[i].IndexOf('=');
                    string paramN = Arr[i].Substring(0, equalindex);
                    string paramV = Arr[i].Substring(equalindex + 1);
                    if (list.Get(paramN) == null || list.Get(paramN) == "") //避免用户传递相同参数
                    { list.Add(paramN, paramV); }
                    else //如果有相同的，一直删除取最后一个值为准
                    { list.Remove(paramN); list.Add(paramN, paramV); }
                }
            }
            else //参数少于或等于1项
            {
                int equalindex = POSTStr.IndexOf('=');
                if (equalindex != -1)
                { //参数是1项
                    string paramN = POSTStr.Substring(0, equalindex);
                    string paramV = POSTStr.Substring(equalindex + 1);
                    list.Add(paramN, paramV);

                }
                else //没有传递参数过来
                { list = null; }
            }
            return list;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public PayNotifyData(string _recp, string _pushinfo, string _sign)
        {
            receipt = _recp;
            pushInfo = _pushinfo;
            sign = _sign;
            jsonReceipt = GetJsonReceipt();
        }

        /// <summary>
        /// 验证支付回调签名
        /// </summary>
        /// <returns></returns>
        public bool VerifySign()
        {
            string recp = HttpUtility.UrlDecode(this.receipt);
            string sourceStr = $"pushInfo={pushInfo}&receipt={recp}";
            Logger.PayLog($"sourceStr：{sourceStr}");
            string md5Str = StringHelper.MD5(sourceStr);
            //string md5Str = Glob.platformMgr.pfCySdk.CyMD5(sourceStr);
            Logger.PayLog($"md5Str：{md5Str}");
            if (sign == md5Str)
                return true;
            return false;
        }

        /// <summary>
        /// 获取渠道号
        /// </summary>
        /// <returns></returns>
        public string GetChannelId()
        {
            if (jsonReceipt.TryGetValue("channelId", out JToken jToken))
                return jToken.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 获取平台订单号
        /// </summary>
        /// <returns></returns>
        public string GetPFOrderId()
        {
            if (jsonReceipt.TryGetValue("orderId", out JToken jToken))
                return jToken.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 获取游戏订单号
        /// </summary>
        /// <returns></returns>
        public string GetOrderId()
        {
            string[] strArr = pushInfo.Split('_');
            return strArr[1];
        }

        /// <summary>
        /// 获取商品Id
        /// </summary>
        /// <returns></returns>
        public int GetGoodsId()
        {
            string[] strArr = pushInfo.Split('_');
            return Convert.ToInt32(strArr[2]);
        }

        private JObject GetJsonReceipt()
        {
            string recp = HttpUtility.UrlDecode(this.receipt);
            recp = StringHelper.Base64Decode(recp);
            Logger.PayLog($"receipt json:{recp}");
            JObject jObject = (JObject)JsonConvert.DeserializeObject(recp);
            return jObject;
        }

        public string GetReceiptJsonStr()
        {
            string recp = HttpUtility.UrlDecode(this.receipt);
            return StringHelper.Base64Decode(recp);
        }
    }
}
