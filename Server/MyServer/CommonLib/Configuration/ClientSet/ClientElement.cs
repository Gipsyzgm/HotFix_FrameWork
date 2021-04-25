using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Configuration
{
    public class ClientElement : ConfigurationElement
    {

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get => (string)base["name"];
            set => base["name"] = value;
        }

        [ConfigurationProperty("ip", IsRequired = true)]
        public string ip
        {
            get => (string)base["ip"];
            set => base["ip"] = value;
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int port
        {
            get => (int)base["port"];
            set => base["port"] = value;
        }
    }
}
