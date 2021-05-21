using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Http.Action.Msg
{
    /// <summary>
    /// 公告消息
    /// </summary>
    public class NoticeMsgData
    {



        public static NameValueCollection GetPostParams(string POSTStr)
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

    }
}
