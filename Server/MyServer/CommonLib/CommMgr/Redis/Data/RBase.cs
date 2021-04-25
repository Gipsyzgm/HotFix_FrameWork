using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommonLib.Redis
{
    /// <summary>
    /// Redis存储对象基类
    /// </summary>
    public class RBase
    {
        public string Key { get; set; }

        public RBase()
        { }


        public RBase(string key)
        {
            Key = key;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public virtual void Deserialize(string str)
        {
            JsonConvert.PopulateObject(str, this);
        }
    }
}
