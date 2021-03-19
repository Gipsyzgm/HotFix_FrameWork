using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.ExcelExport;

namespace Tools
{
    public partial class ExcelExportForm : Form
    {
        public ExcelExportForm()
        {
            InitializeComponent();
        }

        private void ExcelExportForm_Load(object sender, EventArgs e)
        {
            this.listFiles.MouseDoubleClick += new MouseEventHandler(listFiles_MouseDoubleClick);
            this.Refresh();
        }

        public override void Refresh()
        {
            this.txtConfigDir.Text = Glob.projectSetting.RealityConfigDir;
            this.txtClientDir.Text = Glob.projectSetting.RealityClientDir;
            this.txtServerDir.Text = Glob.projectSetting.RealityServerDir;


            this.txtClientOutDir.Text = Glob.codeOutSetting.ClientConfigs[0].ConfigMgrDir.ToReality();
            this.txtServerOutDir.Text = Glob.codeOutSetting.ServerConfig.ConfigMgrDir.ToReality();

            this.txtVerLangFile.Text = Glob.codeOutSetting.ClientConfigs[0].VerLangFile.ToReality().Replace("$ConfigDir$", Glob.projectSetting.RealityConfigDir);
            this.txtVerLangOutFile.Text = Glob.codeOutSetting.ClientConfigs[0].VerLangOutFile.ToReality();

            GetExcelFolderFiles();
        }
        

        /// <summary>
        /// 双击打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void listFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listFiles.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                string path = Path.Combine(this.txtConfigDir.Text, this.listFiles.Items[index].ToString());
                System.Diagnostics.Process.Start(path); 
                this.listFiles.ClearSelected();
                this.listFiles.SelectedIndex = index;
            }
        }
        
        /// <summary>
        /// 一键生成所有相关文件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
            Logger.Clean();
            Logger.LogAction("开始导出[" + ToolsCookieHelper.GetDevName() + "]相关配置文件");

            Dictionary<string, ExcelSheet> dicSheet = ExcelExportParse.GetExcleSheet();
            if (dicSheet == null) return;
            //List<ExcelSheet> serverSheet = ExcelExportParse.ExcleSheetFilter(dicSheet, 'S'); //获取服务器配置表

            List<Task> list = new List<Task>();         

            //服务端
            if (Glob.codeOutSetting.ServerConfig.CodeType == CodeType.CShap && ToolsCookieHelper.Config.IsServerDev)
            {
                var task = Task.Factory.StartNew(() => {
                    var export = new ExcelExport.ConfigCSharp.ExcelExportServer(Glob.codeOutSetting.ServerConfig);
                    export.Generate(ExcelExportParse.ExcleSheetFilter(dicSheet, 'S'));
                });
                list.Add(task);
            }
            //客户端           
            if (ToolsCookieHelper.Config.IsClientDev)
            {
                List<ExcelSheet> clientSheet = ExcelExportParse.ExcleSheetFilter(dicSheet, 'C'); //获取客户端配置表
                foreach (var config in Glob.codeOutSetting.ClientConfigs)
                {
                    if (config.Disable) continue;//禁用
                    if (config.CodeType == CodeType.CShap)
                    {
                        var task = Task.Factory.StartNew(() =>
                        {
                            var export = new ExcelExport.ConfigCSharp.ExcelExportClient(config);
                            export.Generate(clientSheet);
                        });
                        list.Add(task);
                    }
                }
            }

            TaskFactory taskFactory = new TaskFactory();
            list.Add(taskFactory.ContinueWhenAll(list.ToArray(), tArray =>
            {
                exportMapConfig();
                this.btnExport.Enabled = true;
                Logger.LogAction("Excel导出完成");
            }));
        }        
        

        private void exportMapConfig()
        {
            //单个文件
            string mapSourceDir = Glob.projectSetting.RealityConfigDir + "/Maps/";
            if (!Directory.Exists(mapSourceDir)) return;
            FileInfo info = new FileInfo(mapSourceDir + "/MapConfig.txt");
            if (info.Exists)
            {
                if(ToolsCookieHelper.Config.IsServerDev)
                    info.CopyTo(Glob.codeOutSetting.ServerConfig.OutDataDir.ToReality() + "/MapConfig.txt", true);

                if (ToolsCookieHelper.Config.IsClientDev)
                {
                    foreach (var config in Glob.codeOutSetting.ClientConfigs)
                    {
                        if (config.Disable) continue;//禁用
                        if (!config.IsSqlite)  //不用SQlite直接复制
                            info.CopyTo(config.OutDataDir.ToReality() + "/MapConfig.txt", true);
                    }
                }
                if(ToolsCookieHelper.Config.IsServerDev || ToolsCookieHelper.Config.IsClientDev)
                Logger.LogAction("导出 MapConfig.txt!!!!");
            }
        }

        #region 打开文件夹     
        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }
        #endregion

        #region 获取目录下所有Excel文件
        /// <summary>
        /// 获取目录下所有excel文件
        /// </summary>
        private void GetExcelFolderFiles()
        {
            this.listFiles.Items.Clear();

            string path = this.txtConfigDir.Text;
            if (path == "")
                return;            
            if (!Directory.Exists(path))
                return;
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.xls*");           
            foreach (FileInfo fil in files)
            {
                if (fil.Name.StartsWith("~$"))
                    continue;
                if (fil.Name.StartsWith("_"))
                    continue;
                if (fil.Extension.ToLower() == ".xls" || fil.Extension.ToLower() == ".xlsx")
                    this.listFiles.Items.Add(fil.Name);
            }

        }

        /// <summary>
        /// 导出版本检测的文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportVerLang_Click(object sender, EventArgs e)
        {
            //导出客户端配置
            if (Glob.codeOutSetting.ClientConfigs[0].CodeType == CodeType.CShap)
            {
                ExcelExport.ConfigCSharp.ExcelExportClient exportClient = new ExcelExport.ConfigCSharp.ExcelExportClient(Glob.codeOutSetting.ClientConfigs[0]);
                exportClient.ExportVerLang(this.txtVerLangFile.Text, this.txtVerLangOutFile.Text);
            }
            Logger.LogAction("导出版本检测文字完成!!!!");
        }
        #endregion


        ///// <summary>
        ///// 导出C#客户端配置文件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnExportCSClient_Click(object sender, EventArgs e)
        //{
        //    string csClientPath = this.txtClientDir.Text.Substring(0, this.txtClientDir.Text.LastIndexOf('\\') + 1) + "GameClientTest";

        //    if (!Directory.Exists(csClientPath))
        //    {
        //        Logger.LogError("C#客户端测试目录不存", csClientPath);
        //        return;
        //    }

        //    string configPath = Path.Combine(csClientPath, @"GameClient\bin\Debug\DataConfig");

        //    this.btnExport.Enabled = false;
        //    this.btnExportSelectClient.Enabled = false;
        //    this.btnExportSelectServer.Enabled = false;
        //    this.btnExportCSClient.Enabled = false;
        //    Logger.Clean();
        //    Logger.LogAction("开始导出C#客户端配置文件");
        //    ThreadPool.QueueUserWorkItem(callBack => ExcelExportHelper.GenerateCSClientExcel(this.txtExcelPath.Text, csClientPath, configPath, generateCSClientExcelCallback));
        //}

        //private void generateCSClientExcelCallback()
        //{
        //    Logger.LogAction("导出C#客户端配置文件操作完成!!!!");
        //    this.btnExport.Enabled = true;
        //    this.btnExportSelectClient.Enabled = true;
        //    this.btnExportSelectServer.Enabled = true;
        //    this.btnExportCSClient.Enabled = true;
        //}
    }
}

