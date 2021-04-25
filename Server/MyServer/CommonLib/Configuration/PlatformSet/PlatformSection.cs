using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Configuration
{
    public class PlatformSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public PlatformElementCollection Platforms
        {
            get
            {
                return (PlatformElementCollection)base[""];
            }

        }

    }
}
