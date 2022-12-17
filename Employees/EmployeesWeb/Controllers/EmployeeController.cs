using EmployeesWeb.Data;
using EmployeesWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> objEmployeeList = _db.Employees;
            return View(objEmployeeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee obj)
        {
            if (obj.EmployeeName == obj.Department)
            {
                ModelState.AddModelError("name", "The Department cannot exactly match the EmployeeName.");
            }
            if (ModelState.IsValid)
            {
                _db.Employees.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Employee created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var EmployeesFromDb = _db.Employees.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (EmployeesFromDb == null)
            {
                return NotFound();
            }

            return View(EmployeesFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee obje)
        {
            //if (obj.EmployeeName == obj.Department)
            //{
            //    ModelState.AddModelError("name", "The Department cannot exactly match the Employee Name.");
            //}
            if (ModelState.IsValid)
            {
                 _db.Employees.Update(obje);
                     _db.SaveChanges();
                TempData["success"] = "Employees updated successfully";
                return RedirectToAction("Index");
            }
            return View(obje);
        }
    


        public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var employeesFromDb = _db.Employees.Find(id);
			//var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

			if (employeesFromDb == null)
			{
				return NotFound();
			}

			return View(employeesFromDb);
		}

		//POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Employees.Find(id);
			if (obj == null)
			{
				return NotFound();
			}

			_db.Employees.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Employees deleted successfully";
			return RedirectToAction("Index");

		}

        //private bool EmployeeExists(int id)
        //{
        //    return (_db.Employees?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        //}

    }
}
