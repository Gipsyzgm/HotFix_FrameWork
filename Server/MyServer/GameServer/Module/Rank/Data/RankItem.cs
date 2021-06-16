namespace GameServer.Module
{
    /// <summary>排行数据项</summary>
    public class RankItem
    {
        /// <summary>玩家或联盟ID</summary>
        public string ID { get; set; }

        /// <summary>玩家或联盟名</summary>
        public string Name { get; set; }

        /// <summary>玩家或联盟头像[头像、背景、角标](玩家头像是3个，联盟头像只有前2个)</summary>
        public int[] Icon { get; set; }

        /// <summary>玩家或联盟等级</summary>
        public int Level { get; set; }

        /// <summary>数值（个人竞技积分、联盟所有成员总竞技积分、联盟战争积分）</summary>
        public long NUM { get; set; }

        /// <summary>排名</summary>
        public int Rank { get; set; }
        
        /// <summary>联盟名或会长名</summary>
        public string ClubOrLeaderName { get; set; }

    }
}
