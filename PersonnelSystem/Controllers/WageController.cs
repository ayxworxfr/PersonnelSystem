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
    public class WageController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();


        public ActionResult FindWage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindWage(string EmployeeName)
        {
            List<WageVo> Wages = null;

            if (String.IsNullOrEmpty(EmployeeName))
            {
                Wages = db.Database.SqlQuery<WageVo>(@"select Name EmployeeName, DepartmentName, p.Position PositionName, BasicWage, Subsidise, AwardMoney, FinedMoney, FinalWage
                        from Wage w, Employee e,Department d, Position p
                        where w.EmployeeId = e.EmployeeId and w.DepartmentId = d.DepartmentId and e.PositionId = p.PositionId").ToList();
            }
            else
            {
                Wages = db.Database.SqlQuery<WageVo>(@"select Name EmployeeName, DepartmentName, p.Position PositionName, BasicWage, Subsidise, AwardMoney, FinedMoney, FinalWage
                        from Wage w, Employee e,Department d, Position p
                        where w.EmployeeId = e.EmployeeId and w.DepartmentId = d.DepartmentId and e.PositionId = p.PositionId and e.Name = @EmployeeName",
                        new SqlParameter("@EmployeeName", EmployeeName)).ToList();
            }
            if (Wages.Count == 0)
                return HttpNotFound();
            return Json(Wages);
        }

        // GET: Wage
        public ActionResult Index()
        {
            var wage = db.Wage.Include(w => w.Department).Include(w => w.Employee).Include(w => w.Position);
            return View(wage.ToList());
        }

        // GET: Wage/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wage wage = db.Wage.Find(id);
            if (wage == null)
            {
                return HttpNotFound();
            }
            return View(wage);
        }

        // GET: Wage/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName");
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name");
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1");
            return View();
        }

        // POST: Wage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WageId,EmployeeId,DepartmentId,PositionId,BasicWage,Subsidise,AwardMoney,FinedMoney,FinalWage")] Wage wage)
        {
            if (ModelState.IsValid)
            {
                db.Wage.Add(wage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", wage.DepartmentId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name", wage.EmployeeId);
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1", wage.PositionId);
            return View(wage);
        }

        // GET: Wage/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wage wage = db.Wage.Find(id);
            if (wage == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", wage.DepartmentId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name", wage.EmployeeId);
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1", wage.PositionId);
            return View(wage);
        }

        // POST: Wage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WageId,EmployeeId,DepartmentId,PositionId,BasicWage,Subsidise,AwardMoney,FinedMoney,FinalWage")] Wage wage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", wage.DepartmentId);
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name", wage.EmployeeId);
            ViewBag.PositionId = new SelectList(db.Position, "PositionId", "Position1", wage.PositionId);
            return View(wage);
        }

        // GET: Wage/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wage wage = db.Wage.Find(id);
            if (wage == null)
            {
                return HttpNotFound();
            }
            return View(wage);
        }

        // POST: Wage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Wage wage = db.Wage.Find(id);
            db.Wage.Remove(wage);
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
