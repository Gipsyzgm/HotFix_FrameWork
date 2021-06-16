using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Module
{
    public class FightAttrItem
    {
        /// <summary>
        /// 计算类型 1值 2百分比值
        /// </summary>
        public FightAttrCalcuType CalculateType;
        /// <summary>
        /// 属性类型
        /// </summary>
        public FightAttrType AttrType;
        /// <summary>
        /// 属性值
        /// </summary>
        public int Value;
    }

}
