namespace Tools
{
    /// <summary>
    /// 中转Proto输出
    /// </summary>
    public class TransitProtoOut : BaseCodeOut
    {
        //生成Proto的配置目录
        public string ProtoType = string.Empty;

        /// <summary>客户端配置索引</summary>
        public int ClientIndex = -1;

        /// <summary>中转服配置索引</summary>
        public int TransitServerIndex = -1;

        /// <summary>服务端配置索引</summary>
        public int ServerIndex = -1;

        /// <summary>转发协议关联</summary>
        public string TransitFile = string.Empty;

        private string _RealityTransitFile;
        public string RealityTransitFile
        {
            get
            {
                if (_RealityTransitFile == null)
                    _RealityTransitFile = TransitFile.ToReality().Replace("$ProtoType$", ProtoType);
                return _RealityTransitFile;
            }
        }

    }
}
