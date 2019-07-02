using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelSystem.Models;
using PersonnelSystem.Vo;

namespace PersonnelSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();


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
        public ActionResult Index()
        {
            var employee = db.Employee.Include(e => e.Department).Include(e => e.Position);
            return View(employee.ToList());
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
