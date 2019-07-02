using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelSystem.Vo
{
    public class PositionVo
    {
        public long PositionId { get; set; }
        public string PositionName { get; set; }
        public string Info { get; set; }
        public Nullable<long> Number { get; set; }
    }
}