using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MVCCoreTutorial.Data;
using MVCCoreTutorial.Models;

namespace MVCCoreTutorial.Controllers
{
    public class EmployeeController : Controller
    {
        public IConfiguration _configuration { get; }
        DBContext _context;
        public EmployeeController(DBContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult Index()
        {

            List<Department> list = _context.department.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

            List<EmployeeViewModel> listEmp = _context.employee.Where(x => x.IsDeleted == false)
                .Select(x => new EmployeeViewModel
                {
                    Name = x.Name,
                    DepartmentName = x.Department.DepartmentName,
                    Address = x.Address,
                    EmployeeId = x.EmployeeId
                }).ToList();

            return View(listEmp);
        }
    }
}