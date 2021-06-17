using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    //可根据自己的需求扩展类型
    public class TItems : BaseTable
    {
        public List<TItemProp> PropList = new List<TItemProp>();
        public List<TItemEquip> EquipList = new List<TItemEquip>();
    }

    //道具模板
    public class TItemProp
    {
        /// <summary>模板Id</summary>
        public int TemplId;

        /// <summary>叠加数量</summary>
        public int Count;
    }

    //装备模板
    public class TItemEquip
    {
        /// <summary>模板Id</summary>
        public int TemplId;

        /// <summary>星级</summary>
        public int Star = 1;

        /// <summary>等级</summary>
        public int Level = 1;

        /// <summary>位置 0没穿戴 1穿戴位置1 2穿戴位置2</summary>
        public int Index;

        /// <summary>数量</summary>
        public int Count;
    }
}
