
namespace GameServer.Module
{
    public partial class Player
    {
        ///// <summary>
        ///// 玩家全部建筑实体
        ///// </summary>
        //public DictionarySafe<int, Build> buildList = new DictionarySafe<int, Build>();

        ///// <summary>
        ///// 主城堡
        ///// </summary>
        //public BStrongHold StrongHold { get; set; }

        /// <summary>
        /// 建筑队列数量（购买了月卡且在有效期内，可以使用2个建筑队列）
        /// </summary>
        public int BuildQueueMax
        {
            get
            {
                if(payData != null)
                {
                    if (payData.MC > 0)//购买了月卡且在有效期内，可以使用2个建筑队列
                        return 2;
                }
                return 1;
            }
        }

        /// <summary>
        /// 食物上限
        /// </summary>
        public int FoodMax { get; set; }
        /// <summary>
        /// 石头上限
        /// </summary>
        public int StoneMax { get; set; }
        /// <summary>
        /// 人口上限
        /// </summary>
        public int PeopleMax { get; set; }
        
        
    }
}
