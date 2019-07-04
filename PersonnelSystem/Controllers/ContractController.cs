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
    public class ContractController : Controller
    {
        private PersonnelSystemEntities db = new PersonnelSystemEntities();
        static int page_now = 1;                        // 当前页
        static int page_total = 0;                      // 总页数
        static int page_pre = 0;                        // 上一页
        static int page_next = 2;                       // 下一页
        static int page_show = 2;                       // 一页展示数量
        static int page_nav_start = 1;                  // 导航页展示数量
        static int page_nav = 2;                        // 导航页展示数量


        public ActionResult FindContract()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindContract(string EmployeeName)
        {
            List<ContractVo> contracts = null;
            
            if(String.IsNullOrEmpty(EmployeeName))
            {
                contracts = db.Database.SqlQuery<ContractVo>(@"select ContractId, ContractId, e.Name EmployeeName, SignTime, StartTime, EndTime, RenewCount, ProbationarySalary, OfficialSalary
                        from Contract c, Employee e").ToList();
            }
            else
            {
                contracts = db.Database.SqlQuery<ContractVo>(@"select ContractId, ContractId, e.Name EmployeeName, SignTime, StartTime, EndTime, RenewCount, ProbationarySalary, OfficialSalary
                        from Contract c, Employee e
                        where c.EmployeeId = e.EmployeeId and e.Name = @EmployeeName",
                        new SqlParameter("@EmployeeName", EmployeeName)).ToList();
            }
            if (contracts.Count == 0)
                return HttpNotFound();
            return Json(contracts);
        }

        // GET: Contract
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
            ViewBag.path = "/Contract/Index?page_num=";
            ViewBag.page_head = ViewBag.path + 1;
            ViewBag.page_pre = ViewBag.path + page_pre;
            ViewBag.page_next = ViewBag.path + page_next;
            ViewBag.page_tail = ViewBag.path + page_total;
            ViewBag.page_now = page_now;
            ViewBag.page_total = page_total;
            ViewBag.page_nav = page_nav;
            ViewBag.page_nav_start = page_nav_start;

            var contracts = db.Contract.Include(c => c.Employee).OrderBy(o => o.EndTime).Skip(((int)page_num - 1) * page_show).Take(page_show);
            return View("Index", contracts.ToList());
            //var contract = db.Contract.Include(c => c.Employee);
            //return View(contract.ToList());
        }

        // GET: Contract/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contract/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name");
            return View();
        }

        // POST: Contract/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractId,EmployeeId,SignTime,StartTime,EndTime,RenewCount,ProbationarySalary,OfficialSalary")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Contract.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name", contract.EmployeeId);
            return View(contract);
        }

        // GET: Contract/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name", contract.EmployeeId);
            return View(contract);
        }

        // POST: Contract/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContractId,EmployeeId,SignTime,StartTime,EndTime,RenewCount,ProbationarySalary,OfficialSalary")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee, "EmployeeId", "Name", contract.EmployeeId);
            return View(contract);
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contract.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Contract contract = db.Contract.Find(id);
            db.Contract.Remove(contract);
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
