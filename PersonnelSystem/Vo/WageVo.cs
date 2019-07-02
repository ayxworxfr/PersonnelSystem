using System;

namespace PersonnelSystem.Vo
{
    public class WageVo
    {
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public Nullable<double> BasicWage { get; set; }
        public Nullable<double> Subsidise { get; set; }
        public Nullable<double> AwardMoney { get; set; }
        public Nullable<double> FinedMoney { get; set; }
        public Nullable<double> FinalWage { get; set; }
    }
}