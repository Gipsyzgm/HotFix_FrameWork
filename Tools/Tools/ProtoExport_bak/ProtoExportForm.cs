using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tools.ProtoExport;

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
            this.listFiles.MouseDoubleClick += new MouseEventHandler(listFiles_MouseDoubleClick);
            this.Refresh();
        }

        public override void Refresh()
        {
            this.txtProtoPath.Text = Glob.projectSetting.RealityProtoDir;
            this.txtServerPath.Text = Glob.projectSetting.RealityServerDir;
            this.txtClientPath.Text = Glob.projectSetting.RealityClientDir;
            this.txtClientHoxPath.Text = Glob.projectSetting.RealityClientHotDir;
            GetProtoFolderFiles();
        }
        

        #region 打开文件和文件夹
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
                string path = Path.Combine(this.txtProtoPath.Text, this.listFiles.Items[index].ToString());
                System.Diagnostics.Process.Start(path);
                this.listFiles.ClearSelected();
                this.listFiles.SelectedIndex = index;
            }
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
        /// 获取目录下所有excel文件
        /// </summary>
        private void GetProtoFolderFiles()
        {
            string path = this.txtProtoPath.Text;
            this.listFiles.Items.Clear();
            if (path == "")
                return;

            if (!Directory.Exists(path))
                return;
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.proto*");            
            foreach (FileInfo fil in files)
            {
                if (fil.Extension.ToLower() == ".proto")
                    this.listFiles.Items.Add(fil.Name);
            }

        }
        #endregion
        
        
        
        #region 全部导出
        /// <summary>
        /// 一键生成所有相关文件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.btnExport.Enabled = false;
            Logger.Clean();
            ProtoExportBase exportServer = null;
            ProtoExportBase exportClient = null;
            //导出服务端配置
            if (Glob.codeOutSetting.ServerProto.CodeType == CodeType.CShap)
                exportServer = new ProtoExport.ProtoCSharp.ProtoExportServer();                     
            ThreadPool.QueueUserWorkItem(callBack => exportServer.Generate(()=> {
                //导出客户端配置
                if (Glob.codeOutSetting.ClientConfig.CodeType == CodeType.CShap)
                    exportClient = new ProtoExport.ProtoCSharp.ProtoExportClient();
                ThreadPool.QueueUserWorkItem(cb => exportClient.Generate(()=> {
                    Logger.LogAction("全部相关文件生成完成!!!!");
                    this.btnExport.Enabled = true;
                }
                , () =>
                {
                    //导出错误
                    this.btnExport.Enabled = true;
                }));
            }));

           
        }
        #endregion

        #region 编辑_config.txt文件
        private void btnEditConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Path.Combine(this.txtProtoPath.Text, "_config.txt"));
        }
        #endregion        
    }
}

