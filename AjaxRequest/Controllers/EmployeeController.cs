using AjaxRequest.Data;
using AjaxRequest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxRequest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _Context;

        public EmployeeController(AppDbContext context)
        {
            this._Context = context;
        }

       

        [HttpGet]
        //public IActionResult Index(string? str)
        public IActionResult Index()
        {
            var employeeList = _Context.Employees.ToList();
           
            return View(employeeList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            _Context.Employees.Add(emp);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }


        /*HttpResponseMessage*/

        [HttpGet]
        public IActionResult  Details(int id)
        {
            var emp = _Context.Employees.Where(x => x.Id == id).FirstOrDefault();
            var CheckAjaxType = HttpContext.Request.Headers["X-Requested-With"].ToString();
            //bool 
            if (CheckAjaxType == "XMLHttpRequest")
            {
                
                return PartialView("EmployeeDetails", emp);
               
            }
            
            return View(emp);
            //return Redirect("~/Views/Employee/Details.cshtml");
        }


        //Content Negotiation Action
        [HttpGet]
        public IActionResult ContentNegotiation(Employee employee)
        {
            var emp = _Context.Employees.ToList();
            //var emp = _Context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return Ok(emp);
        }

        [HttpPost]
        public IActionResult ajax(int id)
        {
            //var course = _Context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return RedirectToAction("Details");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _Context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return View(course);
        }



        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            _Context.Employees.Update(model);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var employee = _Context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return View(employee);
        }

        

        [HttpPost]
        public IActionResult Delete(Employee model)
        {
            _Context.Employees.Remove(model);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
        //git
    }
}
