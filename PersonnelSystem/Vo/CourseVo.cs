using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelSystem.Vo
{
    public class CourseVo
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string DepartmentName { get; set; }
        public string StudentType { get; set; }
        public Nullable<int> Hours { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> AccruedCount { get; set; }
        public Nullable<int> AttendedCount { get; set; }
        public string CourseRemark { get; set; }
    }
}