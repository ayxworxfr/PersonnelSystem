using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Login.Extensions;
using PersonnelSystem.Models;
using PersonnelSystem.Vo;

namespace PersonnelSystem.Controllers
{

    [UserAuthorize]
    public class EmployeeController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();
        static int page_now = 1;                        // 当前页
        static int page_total = 0;                      // 总页数
        static int page_pre = 0;                        // 上一页
        static int page_next = 2;                       // 下一页
        static int page_show = 2;                       // 一页展示数量
        static int page_nav_start = 1;                  // 导航页展示数量
        static int page_nav = 5;                        // 导航页展示数量


        public ActionResult FindEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindEmployee(string EmployeeName)
        {
            List<EmployeeVo> employees = null;

            if (String.IsNullOrEmpty(EmployeeName))
            {
                employees = db.Database.SqlQuery<EmployeeVo>(@"select EmployeeId, Name, Sex, DepartmentName, p.Position PositionName, DateIntoCompany, Phone, QQ, e.Address, Email, IDCard, School, Remark
                        from Employee e,Department d, Position p
                        where e.DepartmentId = d.DepartmentId and e.PositionId = p.PositionId").ToList();
            }
            else
            {
                employees = db.Database.SqlQuery<EmployeeVo>(@"select EmployeeId, Name, Sex, DepartmentName, p.Position PositionName, DateIntoCompany, Phone, QQ, e.Address, Email, IDCard, School, Remark
                        from Employee e,Department d, Position p
                        where e.DepartmentId = d.DepartmentId and e.PositionId = p.PositionId and e.Name = @EmployeeName",
                        new SqlParameter("@EmployeeName", EmployeeName)).ToList();
            }
            if (employees.Count == 0)
                return HttpNotFound();
            return Json(employees);
        }

        // GET: Employee
        public ActionResult Index(int? page_num)
        {
            if (page_num == null)
                page_num = 1;
            page_now = (int)page_num;
            page_pre = (int)page_now - 1;
            page_next = (int)page_now + 1;
            page_total = (int)Math.Floor((double)db.Contract.Count() / page_show) + 1;
            page_nav_start = page_now - page_now % page_nav + 1;
            page_nav_start = page_now % page_nav == 0 ? page_now - page_nav + 1 : page_nav_start;
            if (page_pre < 1) page_pre = 1;
            if (page_next > page_total) page_next = page_total;
            ViewBag.path = "/Employee/Index?page_num=";
            ViewBag.page_head = ViewBag.path + 1;
            ViewBag.page_pre = ViewBag.path + page_pre;
            ViewBag.page_next = ViewBag.path + page_next;
            ViewBag.page_tail = ViewBag.path + page_total;
            ViewBag.page_now = page_now;
            ViewBag.page_total = page_total;
            ViewBag.page_nav = page_nav;
            ViewBag.page_nav_start = page_nav_start;

            var employees = db.Employee.Include(e => e.Department).Include(e => e.Position).OrderBy(o => o.DepartmentId).Skip(((int)page_num - 1) * page_show).Take(page_show);
            return View("Index", employees.ToList());


            //var employee = db.Employee.Include(e => e.Department).Include(e => e.Position);
            //return View(employee.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName");
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1");
            return View();
        }

        // POST: Employee/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,Name,Sex,DepartmentId,PositionId,DateIntoCompany,Phone,QQ,Address,Email,IDCard,School,Remark")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1", employee.PositionId);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1", employee.PositionId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,Name,Sex,DepartmentId,PositionId,DateIntoCompany,Phone,QQ,Address,Email,IDCard,School,Remark")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1", employee.PositionId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
