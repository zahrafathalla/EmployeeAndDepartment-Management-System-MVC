using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection.Metadata;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(
            IUnitOfWork unitOfWork,
            ILogger<EmployeeController> logger,
            IMapper mapper ) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public IActionResult Index(string SearchValue="")
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> employeesViewModel;

            if (string.IsNullOrEmpty(SearchValue))
            { 
                employees = _unitOfWork.EmployeeRepository.GetAll();
                employeesViewModel= _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.Search(SearchValue);
                employeesViewModel = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }

            return View(employeesViewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(new EmployeeViewModel());
        
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {

           // ModelState["Department"].ValidationState = ModelValidationState.Valid;


            if (ModelState.IsValid)
            {              
                var employee = _mapper.Map<Employee>(employeeViewModel);

                employee.ImageUrl = Documentsetting.UploadFile(employeeViewModel.Image, "Images");

                _unitOfWork.EmployeeRepository.Add(employee);

                _unitOfWork.Complete();

                TempData["CreatedMessage"] = "Employee Added Successfully!";

                return RedirectToAction("Index");
            }
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(employeeViewModel);
            
        }
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var employee = _unitOfWork.EmployeeRepository.GetEntityById(id);
                var employeeViewModel =_mapper.Map<EmployeeViewModel>(employee);
                if (employee == null)
                    return BadRequest();
                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = _unitOfWork.EmployeeRepository.GetEntityById(id);

            if (employee == null)
                return NotFound();

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            string fileName = Path.GetFileName(employeeViewModel.ImageUrl);

            Documentsetting.DeleteFile(fileName, "Images");

            _unitOfWork.EmployeeRepository.Delete(employee);

            _unitOfWork.Complete();

            TempData["DeletedMessage"] = "Employee Deleted Successfully!";

            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var employee = _unitOfWork.EmployeeRepository.GetEntityById(id);
                var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
              
                if (employeeViewModel == null)
                    return BadRequest();
                ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();

                return View(employeeViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public IActionResult Update(int id, EmployeeViewModel employeeViewModel)
        {


            if (id != employeeViewModel.Id)
                return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    var employee = _mapper.Map<Employee>(employeeViewModel);

                    employee = _unitOfWork.EmployeeRepository.GetEntityById(id);

                    if (employeeViewModel.Image != null)
                    {
                        employee.ImageUrl = Documentsetting.UpdateFile(employeeViewModel.Image, "Images", employee.ImageUrl);
                    }
                    _unitOfWork.EmployeeRepository.Update(employee);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }

            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(employeeViewModel);
        }
    }
}
