using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TobeMvcPractise.Models;

namespace TobeMvcPractise.Controllers
{
    public class EmployeeController : Controller
    {
        List<Employee> employees = Employee.Employees;

        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<Employee> empls = from emp in employees
                                              where emp.Age >= 18
                                              orderby emp.Id
                                              select emp;
            return View(empls);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            Employee emp = employees.Single(m => m.Id == id);

            //try sending different content types eg json
            //return Content("{\"name\":\"Ade\",\"Age\":10}", "application/json");
            return View(emp);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]       //using Model Binding as opposed to FormCollection object
        public ActionResult Create(Employee newEmp) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string name = collection["Name"];
                    //string age = collection["Age"];
                    //string joiningDate = collection["JoiningDate"];
                    int id = employees.Max(e => e.Id) + 1;
                    newEmp.Id = id;
                    //Employee newEmp = new Employee { Id = id + 1, Name = name, Age = int.Parse(age), JoiningDate = DateTime.Parse(joiningDate) };
                    employees.Add(newEmp);

                    return RedirectToAction("Index");
                }
                return View(newEmp);        //returns the error message
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee emp = (from e in employees
                            where e.Id == id
                            select e).First();

            return View(emp);
        }

        // POST: Employee/Edit/5
        [HttpPost]       //using TryUpdateModel with FormCollection object
        public ActionResult Edit(int id, FormCollection collection)    
        {
            try
            {
                Employee emp = (from e in employees
                                where e.Id == id
                                select e).First();
                if (TryUpdateModel(emp))        //updating from collection, can work with model object
                {
                    //emp.Name = collection["Name"];
                    //emp.Age = int.Parse(collection["Age"]);
                    //emp.JoiningDate = DateTime.Parse(collection["JoiningDate"]);

                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee emp = (from e in employees
                            where e.Id == id
                            select e).First();

            return View(emp);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)  
        {
            try
            {
                Employee emp = (from e in employees
                                where e.Id == id
                                select e).First();
                employees.Remove(emp);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
