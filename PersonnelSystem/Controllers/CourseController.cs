using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelSystem.Models;
using PersonnelSystem.Vo;

namespace PersonnelSystem.Controllers
{
    public class CourseController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();


        public ActionResult FindCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FindCourse(string CourseCode)
        {
            List<CourseVo> courses = null;
            if (String.IsNullOrEmpty(CourseCode))
            {
                courses = db.Database.SqlQuery<CourseVo>(@"select CourseCode, CourseName, DepartmentName, StudentType, Hours, StartTime, EndTime, AccruedCount, AttendedCount, CourseRemark
                from Course c, Department d
                where c.DepartmentId = d.DepartmentId").ToList();
                return Json(courses);
            }
            else
            {
                courses = db.Database.SqlQuery<CourseVo>(@"select CourseCode, CourseName, DepartmentName, StudentType, Hours, StartTime, EndTime, AccruedCount, AttendedCount, CourseRemark
                from Course c, Department d
                where c.DepartmentId = d.DepartmentId and CourseCode = " + CourseCode).ToList();
            }
            if (courses.Count == 0)
            {
                return HttpNotFound();
            }
            else
            {
                return Json(courses);
            }
        }

        // GET: Course
        public ActionResult Index()
        {
            var course = db.Course.Include(c => c.Department);
            return View(course.ToList());
        }

        // GET: Course/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Course/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseCode,CourseName,DepartmentId,StudentType,Hours,StartTime,EndTime,AccruedCount,AttendedCount,CourseRemark")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        // POST: Course/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseCode,CourseName,DepartmentId,StudentType,Hours,StartTime,EndTime,AccruedCount,AttendedCount,CourseRemark")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Course course = db.Course.Find(id);
            db.Course.Remove(course);
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
