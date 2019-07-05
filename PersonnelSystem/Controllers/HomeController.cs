using Login.Extensions;
using PersonnelSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonnelSystem.Controllers
{
    public class HomeController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee employee)
        {
            int count = db.Database.SqlQuery<int>("select count(*) from Employee where Employee.Name = @Name and Employee.Phone = @Phone",
                new SqlParameter("@Name", employee.Name), new SqlParameter("@Phone", employee.Phone)).First();
            if(count <= 0)
                return HttpNotFound();

            string position = db.Database.SqlQuery<string>("select p.Position from Position p,Employee e where p.PositionId = e.PositionId and e.Name = '" + employee.Name + "' and e.Phone = '" + employee.Phone + "'").First();
            FormsAuthentication.SetAuthCookie(position, false);
            return Json("ok");
        }

        [UserAuthorize]
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


        /// <summary> 
        /// 退出系统 
        /// </summary> 
        /// <returns></returns> 
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}