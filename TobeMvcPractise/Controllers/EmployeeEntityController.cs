using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TobeMvcPractise.Models;

namespace TobeMvcPractise.Controllers
{
    public class EmployeeEntityController : Controller
    {
        private EmpDbContext db = EmpDbContext.EmpDbContextSingleton;   // new EmpDbContext();

        // GET: EmployeeEntity
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: EmployeeEntity/Details/5
        [OutputCache(Duration = 60, VaryByParam = "id")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: EmployeeEntity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(CacheProfile = "Cache60Secs")]     //configurations needed from web config
        public ActionResult Create([Bind(Include = "Id,Name,JoiningDate,Age")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                int id = db.Employees.Count() == 0 ? 1 : db.Employees.Max(e => e.Id) + 1;
                employee.Id = id;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: EmployeeEntity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EmployeeEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,JoiningDate,Age")] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("", "One or more errors occured");   //not necessary
                return View(employee);
            }

            Employee emp = db.Employees.Find(employee.Id);
            TryUpdateModel(emp);
            //db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: EmployeeEntity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EmployeeEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
