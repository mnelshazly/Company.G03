using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            
            return View(departments);
        }
    }
}
