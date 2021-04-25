using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Configuration
{
    public class ClientSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ClientElementCollection Clients
        {
            get
            {
                return (ClientElementCollection)base[""];
            }

        }

    }
}
