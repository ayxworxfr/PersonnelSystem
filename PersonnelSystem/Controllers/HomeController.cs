using PersonnelSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelSystem.Controllers
{
    public class HomeController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();
        public ActionResult Index()
        {
            int EmployeeCount = db.Database.SqlQuery<int>("select count(*) from Employee").First();                             // 公司总人数
            int DepartmentCount = db.Database.SqlQuery<int>("select count(*) from Department").First();                         // 公司总部门数
            int CourseCount = db.Database.SqlQuery<int>("select count(*) from Course where EndTime > GETDATE()").First();       // 还未结束的课程数量
            int ContractCount = db.Database.SqlQuery<int>("select count(*) from Contract where EndTime > GETDATE()").First();   // 还未结束的合同数量
            ViewBag.EmployeeCount = EmployeeCount;
            ViewBag.DepartmentCount = DepartmentCount;
            ViewBag.CourseCount = CourseCount;
            ViewBag.ContractCount = ContractCount;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}