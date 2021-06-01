using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace CommonLib.Comm
{
    /// <summary>公用语言</summary>
    public class LanguageConfig : BaseConfig
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public override object UniqueID => Id;
        /// <summary>
        /// id
        /// 例UI:UILogin.btnLoing
        /// 例表:Test/id或Test/字段/id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 中文
        /// </summary>
        public string Zh_cn { get; set; }
        /// <summary>
        /// 繁体
        /// </summary>
        public string Zh_tw { get; set; }
        /// <summary>
        /// 英语
        /// </summary>
        public string En { get; set; }
        /// <summary>
        /// 日语
        /// </summary>
        public string Ja { get; set; }
        /// <summary>
        /// 韩语
        /// </summary>
        public string Ko { get; set; }
    }
}
