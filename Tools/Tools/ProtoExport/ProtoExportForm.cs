using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.ProtoExport;
using Tools.ProtoExport.ProtoCSharp;

namespace Tools
{
    public partial class ProtoExportForm : Form
    {
        public ProtoExportForm()
        {
            InitializeComponent();
        }

        private void ProtoExportForm_Load(object sender, EventArgs e)
        {           
            this.Refresh();
            setEditConfigBtnList();
        }

        public override void Refresh()
        {
            this.ProtoPathTxt.Text = Glob.projectSetting.RealityProtoDir;
            this.ServerPathTxt.Text = Glob.projectSetting.RealityServerDir;
            this.ClientPathTxt.Text = Glob.projectSetting.RealityClientDir;
            this.ClientHoxPathTxt.Text = Glob.projectSetting.RealityClientHotDir;
            GetProtoFolderFiles();
        }

        #region 打开文件和文件夹
        /// <summary>
        /// 双击打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void treeFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = this.treeFiles.GetNodeAt(e.Location);
            if (node.Parent == null)
                Utils.OpenDir(Path.Combine(this.ProtoPathTxt.Text, node.Text));
            else
                Utils.OpenFile(Path.Combine(this.ProtoPathTxt.Text, node.Parent.Text, node.Text));

        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelctFolder_ClickEvent(object sender, EventArgs e)
        {
            Utils.ButtonOpenDir(sender);
        }
        #endregion

        #region 获取目录下所有Proto文件
        /// <summary>
        /// 获取目录下所有Proto文件，都加入节点（TreeNode）
        /// </summary>
        private void GetProtoFolderFiles()
        {
            string path = this.ProtoPathTxt.Text;
            treeFiles.Nodes.Clear();
            if (path == "")
                return;
            //获取所有子文件夹
            DirectoryInfo[] dirs = new DirectoryInfo(path).GetDirectories();
            foreach (var dir in dirs)
            {
                TreeNode node = new TreeNode();
                node.Text = dir.Name;
                treeFiles.Nodes.Add(node);
                FileInfo[] files = dir.GetFiles("*.proto*");
                foreach (FileInfo fil in files)
                {
                    node.Nodes.Add(fil.Name);
                }
            }

        }
        #endregion
         
        #region 全部导出
        /// <summary>
        /// 一键生成所有相关文件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {          
            Logger.Clean();
            Logger.LogAction("正在生成["+ ToolsCookieHelper .GetDevName()+ "]Proto全部文件");
            this.btnExport.Enabled = false;
            List<Task> list = new List<Task>();
            List<ProtoExportBase> exportClients = new List<ProtoExportBase>();
            List<ProtoExportBase> exportServers = new List<ProtoExportBase>();
            List<ProtoExportTransit> transits = new List<ProtoExportTransit>();
            //全部客户端
            foreach (var config in Glob.codeOutSetting.ClientProtos)
            {
                if (config.CodeType == CodeType.CShap)
                    exportClients.Add(new ProtoExportClient(config));
            }
            //全部服务端
            foreach (var config in Glob.codeOutSetting.ServerProtos)
            {
                if (config.CodeType == CodeType.CShap)
                    exportServers.Add(new ProtoExportServer(config));
            }
            foreach (var config in Glob.codeOutSetting.TransitProtos)
            {
                if (config.CodeType == CodeType.CShap)
                {
                    var export = new ProtoExportTransit(config);
                    //追加到客户端
                    if (config.ClientIndex != -1 && config.ClientIndex < exportClients.Count)
                        exportClients[config.ClientIndex].TransitConfigList = export.ConfigList;

                    //追加到服务端
                    if (config.ServerIndex != -1 && config.ServerIndex < exportServers.Count)
                        exportServers[config.ServerIndex].TransitConfigList = export.ConfigList;                   
                    transits.Add(export);
                }
            }
            //开始执行导出
            foreach (var export in exportClients)
            {
                //专属客户端导出的，但不是客户端开发，跳过
                if (export.Config.IsClientDev && !ToolsCookieHelper.Config.IsClientDev)
                    continue;
                var task = Task.Factory.StartNew(() => { export.Generate(); });
                list.Add(task);
            }
            foreach (var export in exportServers)
            {
                if (!ToolsCookieHelper.Config.IsServerDev)
                    continue;
                var task = Task.Factory.StartNew(() => { export.Generate(); });
                list.Add(task);
            }
            foreach (var export in transits)
            {
                var task = Task.Factory.StartNew(() => { export.Generate(); });
                list.Add(task);
            }

            TaskFactory taskFactory = new TaskFactory();
            list.Add(taskFactory.ContinueWhenAll(list.ToArray(), tArray =>
            {
                this.btnExport.Enabled = true;
                Logger.LogAction("Proto生成完成");
            }));
        }


        #endregion

        #region 编辑_config.txt文件
        private void setEditConfigBtnList()
        {
            string path = this.ProtoPathTxt.Text;
            if (path == "")
                return;
            DirectoryInfo[] dirs = new DirectoryInfo(path).GetDirectories();
            int index = 0;
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].Name == "Common") continue;
                Button btn = new Button();
                btn.Text = dirs[i].Name;
                btn.Click += new EventHandler(this.openConfigFile_ClickEvent);
                btn.Size = new System.Drawing.Size(125, 30);
                btn.Location = new System.Drawing.Point(index % 3 * 153, index / 3 * 38);
                this.panelBtnList.Controls.Add(btn);
                index++;
            }
        }
        private void openConfigFile_ClickEvent(object sender, EventArgs e)
        {
            Utils.OpenFile(Path.Combine(this.ProtoPathTxt.Text,((Button)sender).Text, "_config.txt"));

        }
        #endregion

    }
}

