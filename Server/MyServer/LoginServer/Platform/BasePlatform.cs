using CommonLib;
using CommonLib.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Platform
{
    public interface IPlatform
    {
        /// <summary>插入到数据库</summary>
        void LoginVerify();
    }

    public abstract class BasePlatform
    {
        protected PlatformElement _config;

        protected string HttpPost(string url, NameValueCollection data)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadValues(url, data);//得到返回字符流  
            return Encoding.UTF8.GetString(responseData);//解码  
        }

        /// <summary>
        /// HTTP POST请求 带自定义header body为json字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string HttpPost(string url, Dictionary<string, string> headers, string data)
        {
            WebClient webClient = new WebClient();
            try
            {
                foreach (var kv in headers)
                    webClient.Headers.Add(kv.Key, kv.Value);
                webClient.Headers.Add("Content-Type", "application/json;charset=utf-8");
                string responseData = webClient.UploadString(url, data);//得到返回字符流  
                return responseData;
            }
            catch(Exception ex)
            {
                Logger.LogError(ex);
                return "";
            }
        }

        /// <summary>
        /// HTTP POST请求 form提交
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="parmStr"></param>
        /// <returns></returns>
        protected string HttpPostForm(string url, Dictionary<string, string> headers, string parmStr)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                foreach (var kv in headers)
                    request.Headers.Add(kv.Key, kv.Value);
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(parmStr);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();   //获取响应的字符串流
                StreamReader sr = new StreamReader(responseStream); //创建一个stream读取流
                var str = sr.ReadToEnd();
                sr.Close();
                responseStream.Close();
                return str.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return "";
            }
        }

        /// <summary>
        /// 当前时间戳
        /// </summary>
        /// <returns></returns>
        protected string timestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (DateTime.Now - startTime).TotalMilliseconds.ToString("f0");
            }
        }

        public virtual void PayOrder()
        {

        }

        public virtual bool LoginVerify()
        {
            return false;
        }

        /// <summary>
        /// 创建签名 参数首字母升序排列
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isSort">是否按首字母排序</param>
        /// <param name="otherStr">其它符加参数</param>
        /// <returns></returns>
        protected string CreateSign(NameValueCollection data, bool isSort = true, string otherStr = null)
        {
            string str = string.Empty;
            string[] key = data.AllKeys;
            if(isSort)
                Array.Sort(key);
            foreach (String s in key)
                str += s + "=" + data[s]+"&";
            str = str.TrimEnd('&');
            if (otherStr!=null)
                str += otherStr;
            Logger.LogWarning("Sign string:" + str);
            Logger.LogWarning("Sign string MD5:" + StringHelper.MD5(str));
            return StringHelper.MD5(str);
        }

        /// <summary>
        /// 解析post参数
        /// </summary>
        /// <param name="POSTStr">参数str</param>
        /// <returns></returns>
        protected NameValueCollection GetPostParams(string POSTStr)
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

        protected string HttpGet(string url, NameValueCollection data)
        {
            string urlstr = url + "?";
            foreach (string key in data.AllKeys)
            {
                urlstr += key + "=" + data[key] + "&";
            }
            urlstr = urlstr.Substring(0, urlstr.Length - 1);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlstr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Timeout = 20000;
            string responseContent = "";
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                responseContent = streamReader.ReadToEnd();
                httpWebResponse.Close();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message.ToString());
                return responseContent;
            }
            return responseContent;
        }

        public void SendResponse(HttpListenerResponse response, int ResultCode, string ResultMsg, string sign)
        {
            string result = $@"{{
                        ""AppID"":""{_config.appid}"",
                        ""ResultCode"":{ResultCode},
                        ""ResultMsg"":""{ResultMsg}"",
                        ""Sign"":""{sign}""
                    }}";
            Glob.http.ResponseOutput(response, result);
        }
    }

}
