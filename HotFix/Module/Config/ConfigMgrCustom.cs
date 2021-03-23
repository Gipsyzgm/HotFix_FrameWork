using System;
using System.Collections.Generic;

namespace HotFix
{
    /// <summary>需要自定义解析方法写在这里面</summary>
    public partial class ConfigMgr
    {
        /// <summary>
        /// 主题馆符号倍数表[主题馆ID,[符号ID,[数量(1-5)倍数]]]
        /// </summary>
        public Dictionary<int, Dictionary<int, List<int>>> dicSlotZonePayTabList = new Dictionary<int, Dictionary<int, List<int>>>();

        /// <summary>
        /// 随机名字[语种，[姓/名，内容]](男)
        /// </summary>
        public Dictionary<ELangType, Dictionary<int, List<string>>> dicRandomNameList_man = new Dictionary<ELangType, Dictionary<int, List<string>>>();
        /// <summary>
        /// 随机名字[语种，[姓/名，内容]](女)
        /// </summary>
        public Dictionary<ELangType, Dictionary<int, List<string>>> dicRandomNameList_women = new Dictionary<ELangType, Dictionary<int, List<string>>>();

        //public Dictionary<string, EffectConfig> dicEffectByName = new Dictionary<string, EffectConfig>();


        /// <summary>任务所在当前任务线的索引</summary>
        public Dictionary<int, int> dicTaskLineIndex = new Dictionary<int, int>();

        private void customRead()
        {
            //readEffect();
        }

        //public void readEffect()
        //{
        //    foreach (EffectConfig config in dicEffect.Values)
        //    {
        //        if (dicEffectByName.ContainsKey(config.res))
        //            CLog.Error("特效预制名有相同的：" + config.res);
        //        else
        //            dicEffectByName.Add(config.res, config);
        //    }
        //}
        //=====
    }
}
