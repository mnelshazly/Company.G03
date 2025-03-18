using AutoMapper;
using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Repositories;
using Company.G03.DAL.Models;
using Company.G03.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet] // GET: /Department/Index
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid) //Server Side Validation
            {
                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = DateTime.Now,
                //};
                var department = _mapper.Map<Department>(model);
                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id: {id} was not found" });

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id: {id} was not found" });

            //var departmentDto = new CreateDepartmentDto()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = DateTime.Now,
            //};

            var departmentDto = _mapper.Map<CreateDepartmentDto>(department);

            return View(departmentDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                //if (id != department.Id) return BadRequest();

                //var department = new Department()
                //{
                //    Id = id,
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = DateTime.Now,
                //};

                var department = _mapper.Map<Department>(model);

                department.Id = id;

                _unitOfWork.DepartmentRepository.Update(department);

                var count = _unitOfWork.Complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            Code = model.Code,
        //            CreateAt = model.CreateAt
        //        };

        //        var count = _departmentRepository.Update(department);

        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }

        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("Invalid Id"); //400

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound(new { StatusCode = 404, message = $"Department with Id: {id} was not found" });

            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest();
                _unitOfWork.DepartmentRepository.Delete(department);
                var count = _unitOfWork.Complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }
    }
}
