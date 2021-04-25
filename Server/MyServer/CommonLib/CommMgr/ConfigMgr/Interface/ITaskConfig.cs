/// <summary>
/// 任务配置接口
/// </summary>
namespace CommonLib.Comm
{
    public interface ITaskConfig
    {
        /// <summary>
        /// id
        /// </summary>
        int id { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        int type { get; set; }

        /// <summary>
        /// 条件参数
        /// </summary>
        int[] condition { get; set; }
    }
}
