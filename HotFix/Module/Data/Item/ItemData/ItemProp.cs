using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix
{
    /// <summary>
    /// 道具
    /// </summary>
    public class ItemProp : BaseItemData
    {
        public TItemProp Data;

        /// <summary>道具子类</summary>
        public EItemSubTypeProp SubType;

        private int mCount = 0;
        /// <summary>数量</summary>
        public int Count
        {
            get => Data==null? mCount : Data.Count;
            set
            {
                if (Data == null)
                    mCount = value;
                else
                    Data.Count = value;
            }
        }
        /// <summary>排序规则 倒序排</summary>
        public int Sort
        {
            get
            {
                int sort = 0;
                sort += Config.id;
                return sort;
            }
        }

        /// <param name="tempId">物品模板ID</param>
        public ItemProp(int tempId) : base(tempId)
        {
            SubType = (EItemSubTypeProp)Config.subType;
        }

        public ItemProp(TItemProp data):base(data.TemplId)
        {
            Data = data;
            SubType = (EItemSubTypeProp)Config.subType;
        }
    }

}
