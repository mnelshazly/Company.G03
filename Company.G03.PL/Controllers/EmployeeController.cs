using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Company.G03.PL.Dtos;
using Company.G03.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
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
        public async Task<IActionResult> Search(string? SearchInput)
        {
            var employees = await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);

            return PartialView("EmployeePartialView/EmployeesTablePartialView", employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
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

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await _unitOfWork.CompleteAsync();
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, string viewName = "Edit")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);

            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
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
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDto model)
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

                if (model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                }

                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Update(employee);

                var count = await _unitOfWork.CompleteAsync();

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            return await Edit(id, "Delete");
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
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //if (id != employee.Id) return BadRequest();
                var employee = _mapper.Map<Employee>(model);
                employee.Id = id;

                _unitOfWork.EmployeeRepository.Delete(employee);

                var count = await _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
