using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace Tools
{
    public partial class DBExportForm : Form
    {
        public DBExportForm()
        {
            InitializeComponent();
        }

        private void DBExportForm_Load(object sender, EventArgs e)
        {
            this.DBPathTxt.Text = Glob.codeOutSetting.ServerDB.DBFile.ToReality();
            this.ServerPathTxt.Text = Glob.projectSetting.RealityServerDir;
        }
             

        /// <summary>
        /// 一键生成所有相关文件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.ExportBtn.Enabled = false;
            Logger.Clean();
            ThreadPool.QueueUserWorkItem(callBack => DBExportHelper.CreateServerDBClassFile(this.DBPathTxt.Text, this.ServerPathTxt.Text, GenerateDBAllCallback));
        
        }
        private void GenerateDBAllCallback()
        {
            Logger.LogAction("数据库表结构文件生成完成!!!!");
            this.ExportBtn.Enabled = true;
        }

        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(this.DBPathTxt.Text);
        }
    }
}

