using System.Collections.Generic;

namespace CommonLib.Comm
{
    public class MapConfig
    {
        //地图Id 和副本关卡Id保持一致
        public int id { get; set; }

        //type: 地图类型  0副本地图  1活动副本
        public int type { get; set; }

        //地图背景
        public string bg { get; set; }

        //怪物波数
        public List<List<MapMonster>> monster { get; set; }
    }

    public class MapMonster
    {
        ////怪物配置ID
        public int mId { get; set; }

        //place:怪物位置
        public int place { get; set; }

        //所在位置X偏移
        public int offX { get; set; }

        //所在位置Y偏移
        public int offY { get; set; }
    }
}