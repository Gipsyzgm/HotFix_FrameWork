using System.Collections.Generic;
using System.Configuration;

namespace CommonLib.Configuration
{
    //获取并解析App.config里面clients对应的属性
    //可通过以下方式配置,属性可通过ClientElement扩展
    //<clients>	
    //	 <client name = "LoginToCenterClient" ip="127.0.0.1" port="13000" />
    //</clients>
    public class ClientSet
    {
        private readonly Dictionary<string, ClientElement> platforms = new Dictionary<string, ClientElement>();
        private ClientSet()
        {
            var config = (ClientSection) ConfigurationManager.GetSection("clients");
            foreach (ClientElement e in config.Clients) platforms.Add(e.Name, e);
        }

        public static ClientSet Instance { get; } = new ClientSet();
               
        public ClientElement GetConfig(string name)
        {
            ClientElement rtn;
            platforms.TryGetValue(name, out rtn);
            return rtn;
        }
    }
}