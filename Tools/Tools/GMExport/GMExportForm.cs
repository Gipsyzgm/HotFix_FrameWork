using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tools.ExcelExport;

namespace Tools
{
    public partial class GMExportForm : Form
    {
        public GMExportForm()
        {
            InitializeComponent();
        }

        private void GMExportForm_Load(object sender, EventArgs e)
        {
            this.txtDBPath.Text = Glob.codeOutSetting.GMServerDB.DBFile.ToReality();
            this.txtAPIPath.Text = Glob.codeOutSetting.GMServerAPI.APIFile.ToReality();
            this.txtGameServerPath.Text = Glob.projectSetting.RealityGMServerDir;
        }

        /// <summary>
        /// 生成数据库
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
            Logger.Clean();
            ThreadPool.QueueUserWorkItem(callBack => GMExportHelper.CreateServerDBClassFile(this.txtDBPath.Text, this.txtGameServerPath.Text, generateDBAllCallback));            
        }
        private void generateDBAllCallback()
        {
            Logger.LogAction("GM数据库表结构文件生成完成!!!!");
            this.btnExport.Enabled = true;
        }


        /// <summary>
        /// 生成API
        /// </summary>
        private void btnExportAPI_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
            Logger.Clean();
            ThreadPool.QueueUserWorkItem(callBack => APIExportHelper.CreateAPIClassFile(this.txtAPIPath.Text, this.txtGameServerPath.Text, generateAPICallback));            
        }
        private void generateAPICallback()
        {
            Logger.LogAction("生成GM接口文件生成完成!!!!");
            this.btnExport.Enabled = true;
        }
        


      

        #region 选择文件夹操作
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }
        /// <summary>
        /// 选择文文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelctFile_ClickEvent(object sender, EventArgs e)
        {
            if (sender == this.btnSelectDBPath)
                System.Diagnostics.Process.Start(this.txtDBPath.Text);
            if (sender == this.btnSelectAPIPath)
                System.Diagnostics.Process.Start(this.txtAPIPath.Text);
        }
        #endregion

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.txtDBPath.Text);
        }

        private void btnOpenAPIFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.txtAPIPath.Text);
        }


        #region 生成GM配置文件
        /// <summary>
        /// 导出C#客户端配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportConfig_Click(object sender, EventArgs e)
        {
            //string csClientPath = this.txtGameServerPath.Text;
            //string configPath = Path.Combine(csClientPath, @"GameServer\bin\x64\Debug\DataConfig");
            //this.btnExportConfig.Enabled = false;
            //Logger.Clean();
            //Logger.LogAction("开始导出GM配置表文件");
            //ThreadPool.QueueUserWorkItem(callBack => ExcelExportHelper.GenerateGMServerExcel(ToolsConfigHelper.Config.ExcelPath, csClientPath, configPath, generateCSClientExcelCallback));
            this.btnExportConfig.Enabled = false;
            Logger.Clean();
            Logger.LogAction("开始导出GM配置表文件");

            Dictionary<string, ExcelSheet> dicSheet = ExcelExportParse.GetExcleSheet();
            if (dicSheet == null) return;
            List<ExcelSheet> serverSheet = ExcelExportParse.ExcleSheetFilter(dicSheet, 'G'); //获取服务器配置表

            ExcelExportBase exportServer = null;
            //导出服务端配置
            if (Glob.codeOutSetting.GMServerConfig.CodeType == CodeType.CShap)
                exportServer = new ExcelExport.ConfigCSharp.ExcelExportGMServer(Glob.codeOutSetting.GMServerConfig);
            ThreadPool.QueueUserWorkItem(cb => exportServer.Generate(serverSheet, () =>
            {
                Logger.LogAction("全部相关文件生成完成!!!!");
                this.btnExportConfig.Enabled = true;
            }));
        }
        #endregion

        private void btnGameDBExport_Click(object sender, EventArgs e)
        {
            this.btnGameDBExport.Enabled = false;
            Logger.Clean();
            ThreadPool.QueueUserWorkItem(callBack => GMExportHelper.CreateGameDBClassFile(Glob.codeOutSetting.ServerDB.DBFile.ToReality(), this.txtGameServerPath.Text, generateGameDBAllCallback));
            
        }
        private void generateGameDBAllCallback()
        {
            Logger.LogAction("游戏数据库表结构文件生成完成!!!!");
            this.btnGameDBExport.Enabled = true;
        }
    }
}

