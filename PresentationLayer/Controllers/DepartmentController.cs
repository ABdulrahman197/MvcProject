using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.reposatories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
           
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Departments = await unitOfWork.DepartmentRepository.GetAll();
                var MappedDepartment = mapper.Map<IEnumerable<Department>, IEnumerable<Department>>(Departments);
                return View(MappedDepartment);
            }
            else
            {
                var Departments = unitOfWork.DepartmentRepository.GetDepartmentByName(SearchValue);
                var MappedDepartment = mapper.Map<IEnumerable<Department>, IEnumerable<Department>>(Departments);
                return View(MappedDepartment);
            }
        }

      
        public  IActionResult CreateDepartment()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateDepartmnet(Department department)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.DepartmentRepository.Add(department);
                int RowsAffected = await unitOfWork.Complete();
                if (RowsAffected > 0 ) 
                {
                    TempData["Message"] = "Department Is Created";
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }



        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); // => 400 Client Error 
            }
            var Department = await unitOfWork.DepartmentRepository.GetById(id.Value);
            if (Department == null)
            {
                return NotFound();

            }

            return View(Department);

        }


        [HttpGet]
        public  async Task<IActionResult> Edit(int? id)
        { 
            
            return await Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await unitOfWork.DepartmentRepository.Update(department);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int Id)
        {
            if (Id != department.Id)
            {
                return BadRequest();
            }
            try
            {
                await unitOfWork.DepartmentRepository.Delete(department);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }

        }

    }
}
