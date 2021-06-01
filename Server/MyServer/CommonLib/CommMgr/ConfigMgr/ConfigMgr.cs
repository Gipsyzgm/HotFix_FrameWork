/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;

namespace CommonLib.Comm
{
    public partial class ConfigMgr
    {
        public static ConfigMgr I { get; protected set; }
        /// <summary> 物品配置</summary>
        public readonly Dictionary<object, ItemConfig> dicItem = new Dictionary<object, ItemConfig>();
        /// <summary> 道具礼包配置</summary>
        public readonly Dictionary<object, ItemPackConfig> dicItemPack = new Dictionary<object, ItemPackConfig>();
        /// <summary> 每日任务</summary>
        public readonly Dictionary<object, TaskDayConfig> dicTaskDay = new Dictionary<object, TaskDayConfig>();
        /// <summary> CDKey配置</summary>
        public readonly Dictionary<object, CDKeyConfig> dicCDKey = new Dictionary<object, CDKeyConfig>();
        /// <summary> 公用语言</summary>
        public readonly Dictionary<object, LanguageConfig> dicLanguage = new Dictionary<object, LanguageConfig>();

        /// <summary> 系统设置</summary>
        public readonly SettingConfig settingConfig;

        public ConfigMgr()
        {
            I = this;
            Logger.Sys("开始读取所有配置表文件...");
            configInit();
            readConfig(dicItem);
            readConfig(dicItemPack);
            readConfig(dicTaskDay);
            readConfig(dicCDKey);
            readConfig(dicLanguage);

            //读取竖表配置
            readConfigV(ref settingConfig);

            customRead();
            Logger.Sys("读取配置表文件完成");
        }

        protected virtual void reloadAll()
        {
            copyDictionary(dicItem, reloadConfig<ItemConfig>());
            copyDictionary(dicItemPack, reloadConfig<ItemPackConfig>());
            copyDictionary(dicTaskDay, reloadConfig<TaskDayConfig>());
            copyDictionary(dicCDKey, reloadConfig<CDKeyConfig>());
            copyDictionary(dicLanguage, reloadConfig<LanguageConfig>());

            copyClassValue(settingConfig, reloadConfigV<SettingConfig>());

        }
    }
}
