using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TobeMvcPractise.Models;
using TobeMvcPractise.Services;

namespace TobeMvcPractise.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeServices employeeService = new EmployeeServices();

        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<Employee> empls = employeeService.GetAllEmployees();
            return View(empls);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee emp = employeeService.GetEmployee((int)id);

            if (emp == null)
            {
                return HttpNotFound();
            }

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
                    ////if collection object is being used
                    //string name = collection["Name"];
                    //string age = collection["Age"];
                    //string joiningDate = collection["JoiningDate"];
                    //Employee newEmp = new Employee { Id = id + 1, Name = name, Age = int.Parse(age), JoiningDate = DateTime.Parse(joiningDate) };
                    employeeService.AddEmployee(newEmp);

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
            Employee emp = employeeService.GetEmployee(id);

            return View(emp);
        }

        // POST: Employee/Edit/5
        [HttpPost]       //using TryUpdateModel with FormCollection object
        public ActionResult Edit(int id, FormCollection collection)    
        {
            try
            {
                //Employee emp = (from e in employees
                //                where e.Id == id
                //                select e).First();
                //if (TryUpdateModel(emp))        //updating from collection, can work with model object
                if(employeeService.UpdateEmployee(id,collection))
                {
                    //emp.Name = collection["Name"];
                    //emp.Age = int.Parse(collection["Age"]);
                    //emp.JoiningDate = DateTime.Parse(collection["JoiningDate"]);

                    return RedirectToAction("Index");
                }
                return View(employeeService.GetEmployee(id));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee emp = employeeService.GetEmployee(id);

            return View(emp);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)  
        {
            try
            {
                string token = collection["__RequestVerificationToken"];
                employeeService.RemoveEmployee(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
