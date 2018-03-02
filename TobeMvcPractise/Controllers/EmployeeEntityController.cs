using System.Linq;
using System.Net;
using System.Web.Mvc;
using TobeMvcPractise.Models;
using TobeMvcPractise.Services;

namespace TobeMvcPractise.Controllers
{
    public class EmployeeEntityController : Controller
    {
        private EmployeeEntityServices _employeeServices = new EmployeeEntityServices(new MyDbContext());

        // GET: EmployeeEntity
<<<<<<< HEAD
        //[OutputCache(CacheProfile = "Cache60Secs")]     //configurations set at web config (located at project root directory)
=======
>>>>>>> parent of fa1138b... Update
        public ActionResult Index()
        {
            return View(_employeeServices.GetAllEmployees());
        }

        // GET: EmployeeEntity/Details/5
        [OutputCache(Duration = 60, VaryByParam = "id")]
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Employee employee = _employeeServices.GetEmployee((int)id);
            if (employee == null) return HttpNotFound();

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
                //int id = db.Employees.Count() == 0 ? 1 : db.Employees.Max(e => e.Id) + 1;
                //employee.Id = id;             //not necessary, Id automatically updated by EF
                _employeeServices.AddEmployee(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: EmployeeEntity/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Employee employee = _employeeServices.GetEmployee((int)id);
            if (employee == null) return HttpNotFound();

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

            //Employee emp = db.Employees.Find(employee.Id);
            //TryUpdateModel(emp);
            ////db.Entry(employee).State = EntityState.Modified;
            //db.SaveChanges();
            _employeeServices.UpdateEmployee(employee);

            return RedirectToAction("Index");
        }

        // GET: EmployeeEntity/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Employee employee = _employeeServices.GetEmployee((int)id);
            if (employee == null) return HttpNotFound();

            return View(employee);
        }

        // POST: EmployeeEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _employeeServices.RemoveEmployee(id);
            return RedirectToAction("Index");
        }

        public ActionResult Put(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return RedirectToAction("Details", new { id = id });
        }

        private void Reflection()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();

            var methods = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type)) //filter controllers
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute), false))
                .Where(method => method.DeclaringType.Namespace == typeof(EmployeeEntityController).Namespace)
                .Where(method => !new System.Collections.Generic.List<System.Type> { typeof(AccountController), typeof(HomeController),
                typeof(ManageController)}.Contains(method.DeclaringType));

            var methds = typeof(EmployeeController).GetMethods()            //only public methods
                .Where(method => method.DeclaringType.Namespace == typeof(EmployeeController).Namespace);

            methds = typeof(EmployeeEntityController).GetMethods(   //all methods
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public 
                | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static)
                .Where(method => method.DeclaringType.Namespace == typeof(EmployeeController).Namespace);

            var baseDefinition = typeof(EmployeeEntityController).GetMethods(System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Instance)
                .Where(method => method.DeclaringType.Namespace == typeof(EmployeeEntityController).Namespace);
            //System.Reflection.BindingFlags.NonPublic for private?

            int count = baseDefinition.Count();
        }        
    }
}
