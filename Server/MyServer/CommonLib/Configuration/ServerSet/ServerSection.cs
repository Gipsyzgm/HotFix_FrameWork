using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Configuration
{
    public class ServerSection : ConfigurationSection
    {       
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ServerElementCollection Servers
        {
            get
            {
                return (ServerElementCollection)base[""];
            }

        }

    }
}
