using AjaxRequest.Data;
using AjaxRequest.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxRequest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

       

        [HttpGet]
       
        public IActionResult Index()
        {
            var employeeList = _context.Employees.ToList();
           
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
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        /*HttpResponseMessage*/

        [HttpGet]
        public IActionResult  Details(int id)
        {
            var emp = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            var checkAjaxType = HttpContext.Request.Headers["X-Requested-With"].ToString();
           
            if (checkAjaxType == "XMLHttpRequest")
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
            var emp = _context.Employees.ToList();
           
            return Ok(emp);
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return View(employee);
        }



        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            _context.Employees.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return View(employee);
        }

        

        [HttpPost]
        public IActionResult Delete(Employee model)
        {
            _context.Employees.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
