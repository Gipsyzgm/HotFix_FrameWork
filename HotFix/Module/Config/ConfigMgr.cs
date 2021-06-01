/// <summary>
/// 工具生成，不要修改
/// </summary>
using System.Collections.Generic;
namespace HotFix
{
    public partial class ConfigMgr
    {
        private bool IsCSV = false;
        /// <summary> 物品配置</summary>
        public readonly Dictionary<object, ItemConfig> Item = new Dictionary<object, ItemConfig>();
        /// <summary> 道具礼包配置</summary>
        public readonly Dictionary<object, ItemPackConfig> ItemPack = new Dictionary<object, ItemPackConfig>();
        /// <summary> 怪物配置</summary>
        public readonly Dictionary<object, MonsterConfig> Monster = new Dictionary<object, MonsterConfig>();
        /// <summary> Boss出场配置</summary>
        public readonly Dictionary<object, BossEnterConfig> BossEnter = new Dictionary<object, BossEnterConfig>();
        /// <summary> 技能配置</summary>
        public readonly Dictionary<object, SkillConfig> Skill = new Dictionary<object, SkillConfig>();
        /// <summary> Buff配置</summary>
        public readonly Dictionary<object, BuffConfig> Buff = new Dictionary<object, BuffConfig>();
        /// <summary> 战斗掉落道具</summary>
        public readonly Dictionary<object, WarDropItemConfig> WarDropItem = new Dictionary<object, WarDropItemConfig>();
        /// <summary> 特效配置</summary>
        public readonly Dictionary<object, EffectConfig> Effect = new Dictionary<object, EffectConfig>();
        /// <summary> 每日任务</summary>
        public readonly Dictionary<object, TaskDayConfig> TaskDay = new Dictionary<object, TaskDayConfig>();
        /// <summary> 公用语言</summary>
        public readonly Dictionary<object, LanguageConfig> Language = new Dictionary<object, LanguageConfig>();

        /// <summary> 系统设置</summary>
        public SettingConfig Setting;

        public async CTask Initialize()
        {
            readConfig(Item,false).Run();
            readConfig(ItemPack,false).Run();
            readConfig(Monster,false).Run();
            readConfig(BossEnter,false).Run();
            readConfig(Skill,false).Run();
            readConfig(Buff,false).Run();
            readConfig(WarDropItem,false).Run();
            readConfig(Effect,false).Run();
            readConfig(TaskDay,false).Run();
            readConfig(Language,false).Run();

            //读取竖表配置
            Setting = await readConfigV<SettingConfig>(false);

            //等待全部加载完再执行自定义解析
            await waitLoadComplate();            
            customRead();
        }
    }
}
