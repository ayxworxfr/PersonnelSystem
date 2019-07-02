using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelSystem.Vo
{
    public class ContractVo
    {
        public long ContractId { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<System.DateTime> SignTime { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> RenewCount { get; set; }
        public Nullable<double> ProbationarySalary { get; set; }
        public Nullable<double> OfficialSalary { get; set; }
    }
}