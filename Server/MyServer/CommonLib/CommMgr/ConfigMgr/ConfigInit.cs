/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    public partial class ConfigMgr
    {
        protected virtual void configInit()
        {            
            loadConfig<ItemConfig>(); //物品配置
            loadConfig<ItemPackConfig>(); //道具礼包配置
            loadConfig<SettingConfig>(); //系统设置
            loadConfig<TaskDayConfig>(); //每日任务
            loadConfig<CDKeyConfig>(); //CDKey配置
            loadConfig<LanguageConfig>(); //公用语言
        }
    }
}
