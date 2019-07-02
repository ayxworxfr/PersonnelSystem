using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelSystem.Vo
{
    public class EmployeeVo
    {
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public Nullable<System.DateTime> DateIntoCompany { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string IDCard { get; set; }
        public string School { get; set; }
        public string Remark { get; set; }
    }
}