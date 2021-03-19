using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace Tools
{
    public partial class LogExportForm : Form
    {
        public LogExportForm()
        {
            InitializeComponent();
        }

        private void DBExportForm_Load(object sender, EventArgs e)
        {
            this.txtDBPath.Text = Glob.codeOutSetting.ServerLog.LogFile.ToReality();
            this.txtServerPath.Text = Glob.projectSetting.RealityServerDir;
        }
             

        /// <summary>
        /// 一键生成所有相关文件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
            Logger.Clean();
            ThreadPool.QueueUserWorkItem(callBack => LogExportHelper.CreateServerLogClassFile(this.txtDBPath.Text, this.txtServerPath.Text, generateLogAllCallback));
        
        }
        private void generateLogAllCallback()
        {
            Logger.LogAction("日志结构文件生成完成!!!!");
            this.btnExport.Enabled = true;
        }

        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.txtDBPath.Text);
        }
    }
}

