using System.Collections.Generic;
using System.Configuration;

namespace CommonLib.Configuration
{
    //获取并解析App.config里面servers对应的属性
    //可通过以下方式配置,属性可通过ServerElement扩展
    //<servers>
	//<server name = "LoginToCenterServer" ip="Any" port="13000" maxConnection="1" />
	//<server name = "GameToCenterServer" ip="Any" port="13001" maxConnection="100" />
    //<server name = "GMToCenterServer" ip="Any" port="13002" maxConnection="1" />
	//</servers>
    public class ServerSet
    {
        private readonly Dictionary<string, ServerElement> servers = new Dictionary<string, ServerElement>();

        private ServerSet()
        {
            var config = (ServerSection) ConfigurationManager.GetSection("servers");
            foreach (ServerElement e in config.Servers) servers.Add(e.Name, e);
        }

        public static ServerSet Instance { get; } = new ServerSet();
          
        public ServerElement GetConfig(string name)
        {
            ServerElement rtn;
            servers.TryGetValue(name, out rtn);
            return rtn;
        }
    }
}