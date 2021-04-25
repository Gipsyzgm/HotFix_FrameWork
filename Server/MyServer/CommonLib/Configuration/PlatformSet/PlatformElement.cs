using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Configuration
{
    public class PlatformElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string name
        {
            get => (string)base["name"];
            set => base["name"] = value;

        }

        [ConfigurationProperty("appid", IsRequired = true)]
        public string appid
        {
            get => (string)base["appid"];
            set => base["appid"] = value;
        }

        [ConfigurationProperty("appkey", IsRequired = true)]
        public string appkey
        {
            get => (string)base["appkey"];
            set => base["appkey"] = value;
        }

        [ConfigurationProperty("paykey", IsRequired = true)]
        public string paykey
        {
            get => (string)base["paykey"];
            set => base["paykey"] = value;
        }

        [ConfigurationProperty("apiurl", IsRequired = false)]
        public string apiurl
        {
            get => (string)base["apiurl"];
            set => base["apiurl"] = value;
        }
        
    }
}
