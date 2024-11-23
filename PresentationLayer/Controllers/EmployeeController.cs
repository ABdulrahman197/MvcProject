using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.reposatories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
//using PresentationLayer.Helpers;
using PresentationLayer.ViewModels;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

      

        public EmployeeController(IUnitOfWork unitOfWork,  IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var employees = await unitOfWork.EmployeeRepository.GetAll();
                var mappedEmployees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployees); // Use mappedEmployees here
            }
            else
            {
                var employees = unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
                var mappedEmployees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployees); // Map and return search result
            }
        }


        [HttpGet]
        public async Task<IActionResult> AddEmployee() 
        {
          ViewBag.departments =  await unitOfWork.DepartmentRepository.GetAll();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeVM) 
        {
            if (ModelState.IsValid)
            {
                //employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images"); 

                var MappedEmployee = mapper.Map<EmployeeViewModel , Employee>(employeeVM);   

                await unitOfWork.EmployeeRepository.Add(MappedEmployee);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index)); 
            }
            return View(employeeVM); 
        }


        public async Task<IActionResult> Details(int? id , string ViewName  = "Details" ) 
        {
            if (id is null )
            {
                return BadRequest();
            }
            var employee = await unitOfWork.EmployeeRepository.GetById(id.Value) ;

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> Edit(int?  id )
        {
            return  await Details(id, "Edit" );  
        }
           
           
           
           
        [HttpPost]
        [ValidateAntiForgeryToken]
           

           
           
           
           


        public async Task<IActionResult> Edit(Employee employee ,[FromRoute] int Id )  
        {
            if (Id != employee.Id)
            {
                return BadRequest();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    await unitOfWork.EmployeeRepository.Update(employee);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(System.Exception ex )
            {
                ModelState.AddModelError(string.Empty , ex.Message) ; 

            }
            
            return View(employee);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Employee employee , [FromRoute] int Id )
        {
            if (Id != employee.Id)
            {
                return BadRequest();
            }
            try
            {
                await unitOfWork.EmployeeRepository.Delete(employee);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex) 
            {
                ModelState.AddModelError (string.Empty , ex.Message) ;
                return View(employee);
            }

        }

    }
}
