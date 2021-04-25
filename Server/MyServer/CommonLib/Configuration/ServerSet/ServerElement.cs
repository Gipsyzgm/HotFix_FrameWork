using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Configuration
{
    public class ServerElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get => (string)base["name"];
            set => base["name"] = value;
        }

        [ConfigurationProperty("ip", IsRequired = true,DefaultValue ="Any")]
        public string ip
        {
            get => (string)base["ip"];
            set => base["ip"] = value;
        }

        [ConfigurationProperty("port", IsRequired = false)]
        public int port
        {
            get => (int)base["port"];
            set => base["port"] = value;
        }


        [ConfigurationProperty("netIP", IsRequired = false)]
        public string netIP
        {
            get 
            {
               string str =  (string)base["netIP"];
                if (str.ToLower() == "auto")
                    return StringHelper.GetLocalIP();
                return str;
            } 
            set => base["netIP"] = value;
        }

        [ConfigurationProperty("autoPort", IsRequired = false, DefaultValue = "false")]
        public bool autoPort
        {
            get => (bool)base["autoPort"];
            set => base["autoPort"] = value;
        }

        [ConfigurationProperty("maxConnection", IsRequired = false, DefaultValue = "300")]
        public int maxConnection
        {
            get => (int)base["maxConnection"];
            set => base["maxConnection"] = value;
        }

        [ConfigurationProperty("receiveBufferSize", IsRequired = false, DefaultValue = "1024")]
        public int receiveBufferSize
        {
            get => (int)base["receiveBufferSize"];
            set => base["receiveBufferSize"] = value;
        }
    }
}
