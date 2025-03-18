using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Company.G03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }

            // Dictionary: 3 Properties

            // 1. ViewData: Transfer Extra Information From Controller (Action) to View
            // ViewData["Message"] = "Hello From ViewData";

            // 2. ViewBag: Transfer Extra Information From Controller (Action) to View
            // ViewBag.Message = "Hello From ViewBag";

            // 3. TempData



            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Email = model.Email,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    HiringDate = model.HiringDate,
                //    CreateAt = model.CreateAt,
                //    DepartmentId = model.DepartmentId,
                //};
                var employee = _mapper.Map<Employee>(model);
                _unitOfWork.EmployeeRepository.Add(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee has been created";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        //[HttpGet]
        //public IActionResult Details(int? id, string viewName = "Details")
        //{
        //    if (id is null) return BadRequest("Invalid Id"); //400

        //    var employee = _employeeRepository.Get(id.Value);

        //    if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with Id: {id} was not found" });

        //    return View(viewName, employee);
        //}

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Employee with Id: {id} was not found" });

            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(dto);
        }

        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    var departments = _departmentRepository.GetAll();
        //    ViewData["departments"] = departments;

        //    if (id is null) return BadRequest("Invalid Id"); //400

        //    var employee = _employeeRepository.Get(id.Value);

        //    if (employee is null) return NotFound(new { StatusCode = 404, message = $"Department with Id: {id} was not found" });

        //    //var employeeDto = new CreateEmployeeDto()
        //    //{
        //    //    Name = employee.Name,
        //    //    Address = employee.Address,
        //    //    Age = employee.Age,
        //    //    Email = employee.Email,
        //    //    Phone = employee.Phone,
        //    //    Salary = employee.Salary,
        //    //    IsActive = employee.IsActive,
        //    //    IsDeleted = employee.IsDeleted,
        //    //    HiringDate = employee.HiringDate,
        //    //    CreateAt = employee.CreateAt,
        //    //    DepartmentId = employee.DepartmentId,
        //    //};

        //    var employeeDto = _mapper.Map<CreateEmployeeDto>(employee);

        //    return View(employeeDto);
        //}

        [HttpGet]
        public IActionResult Edit(int? id, string viewName = "Edit")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;

            if (employee is null) return NotFound(new { StatusCode = 404, message = $"Department with Id: {id} was not found" });

            //var employeeDto = new CreateEmployeeDto()
            //{
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    Email = employee.Email,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    IsDeleted = employee.IsDeleted,
            //    HiringDate = employee.HiringDate,
            //    CreateAt = employee.CreateAt,
            //    DepartmentId = employee.DepartmentId,
            //};

            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(viewName, dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //if (id != employee.Id) return BadRequest();
                //var employee = new Employee()
                //{
                //    Id = id,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Email = model.Email,
                //    Phone = model.Phone,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    HiringDate = model.HiringDate,
                //    CreateAt = model.CreateAt,
                //    DepartmentId = model.DepartmentId,
                //};

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Update(employee);

                var count = _unitOfWork.Complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    return Details(id, "Delete");
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Edit(id, "Delete");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete([FromRoute] int id, Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != employee.Id) return BadRequest();
        //        var count = _employeeRepository.Delete(employee);

        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View(employee);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //if (id != employee.Id) return BadRequest();
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Delete(employee);

                var count = _unitOfWork.Complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
