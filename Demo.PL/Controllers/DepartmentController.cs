using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;

        public DepartmentController(
            IUnitOfWork unitOfWork,
            ILogger<DepartmentController> logger,
            IMapper mapper)
        //IDepartmentRepository departmentRepository,

        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

            //_departmentRepository = departmentRepository;
        }

        public IActionResult Index(string SearchValue ="")
        {
            IEnumerable<Department> departments;
            IEnumerable<DepartmentViewModel> departmentsViewModel;

            if (string.IsNullOrEmpty(SearchValue))
            {
                 departments = _unitOfWork.DepartmentRepository.GetAll();
                departmentsViewModel=_mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            }
            else
            {
                departments = _unitOfWork.DepartmentRepository.Search(SearchValue);
                departmentsViewModel = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            }

            return View(departmentsViewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new DepartmentViewModel());
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(departmentViewModel);
                _unitOfWork.DepartmentRepository.Add(department);
                _unitOfWork.Complete();
                TempData["MessageTemp"] = "Department Added Successfully!";
                return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if(id == null) 
                    return BadRequest();
                var department = _unitOfWork.DepartmentRepository.GetEntityById(id);
                var departmetViewModel = _mapper.Map<DepartmentViewModel>(department);
                if (departmetViewModel == null)
                    return BadRequest();
                return View(departmetViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Update(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var department = _unitOfWork.DepartmentRepository.GetEntityById(id);
                var departmetViewModel = _mapper.Map<DepartmentViewModel>(department);
                if (departmetViewModel == null)
                    return BadRequest();
                return View(departmetViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public IActionResult Update(int id,DepartmentViewModel departmentViewModel)
        {
            if (id != departmentViewModel.Id)
                return NotFound();
            try
            {
                if(ModelState.IsValid)
                {
                    var department = _mapper.Map<Department>(departmentViewModel);
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return RedirectToAction("Error", "Home");

            }
            return View(departmentViewModel);

        }

        public IActionResult Delete(DepartmentViewModel departmentViewModel)
        {
            var department = _mapper.Map<Department>(departmentViewModel);
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
            TempData["DeletedMessage"] = "Department Deleted Successfully!";
            return RedirectToAction("Index");
        }



    }
}
