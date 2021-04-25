using System.Collections.Generic;
using System.Configuration;

namespace CommonLib.Configuration
{
    //获取并解析App.config里面platforms对应的属性
    //可通过以下方式配置,属性可通过PlatformElement扩展
    //<platforms>
    //	<platform name = "cysdk" appid="1590028813409" appkey="57d00545cd13465b9e96e33493b297d2" paykey="!@@#$$~123AbcdEf" apiurl="https://nsdk.gaming.com/" />
    //</platforms>
    public class PlatformSet
    {
        private readonly Dictionary<string, PlatformElement> platforms = new Dictionary<string, PlatformElement>();
        private PlatformSet()
        {
            var config = (PlatformSection) ConfigurationManager.GetSection("platforms");
            foreach (PlatformElement e in config.Platforms) platforms.Add(e.name, e);
        }
        public static PlatformSet Instance { get; } = new PlatformSet();
        /// <summary>
        /// 获取平台配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public PlatformElement GetConfig(string name)
        {
            PlatformElement rtn;
            platforms.TryGetValue(name, out rtn);
            return rtn;
        }
    }
}