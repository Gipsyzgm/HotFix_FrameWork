namespace Tools
{
    //所有配置的地址
    public class Glob
    {
        public static SettingMgr settingMgr;

        public static ProjectSetting projectSetting;

        public static CodeOutSetting codeOutSetting;

        public static void Initialize()
        {
            settingMgr = new SettingMgr();
            settingMgr.Initialize();
        }

        public static void SetProject(int projectId)
        {
            projectSetting = settingMgr.Select<ProjectSetting>(projectId);
            if (projectSetting != null)
            {
                Main.SetToolsTitle();
                codeOutSetting = settingMgr.Select<CodeOutSetting>(projectSetting.CodeOutId);
                if (codeOutSetting == null)
                    Logger.LogError("项目[" + projectId + "]代码导出配置不存在! Id:" + projectSetting.CodeOutId);
            }
            else
            {
                Logger.LogError("项目配置不存在! Id:" + projectId);
            }
        }
    }
}
