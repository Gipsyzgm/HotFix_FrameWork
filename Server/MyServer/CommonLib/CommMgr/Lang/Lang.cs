
namespace CommonLib.Comm
{
    public class Lang
    {        
        public string key { get; set; }
        public Lang(string _key)
        {
            key = _key;
        }

        /// <summary>
        /// 系统黓认语言
        /// </summary>
        public string Value
        {
            get
            {
                return LangMgr.I.Get(key);
            }
        }

        /// <summary>
        /// 跟据语言类型获取值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetValue(ELangType type)
        {
            return LangMgr.I.Get(key, type);
        }

    }
}
