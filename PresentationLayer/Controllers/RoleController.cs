using AutoMapper;
using BusinessLogicLayer.reposatories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper )  
        {
            this.roleManager = roleManager;
            _mapper = mapper;
        }



        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<RoleViewModel> mappedRoles;

            if (string.IsNullOrEmpty(searchValue))
            {
                // Get all roles and map to RoleViewModel
                var roles = await roleManager.Roles.ToListAsync();
                mappedRoles = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);
            }
            else
            {
                // Get a single role by name
                var role = await roleManager.FindByNameAsync(searchValue);

                if (role == null)
                {
                    // Role not found; return an empty list or handle as needed
                    mappedRoles = Enumerable.Empty<RoleViewModel>();
                }
                else
                {
                    // Map the single role to RoleViewModel and return as a list with one element
                    mappedRoles = new List<RoleViewModel>
            {
                _mapper.Map<RoleViewModel>(role)
            };
                }
            }

            return View(mappedRoles);
        }




        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel Role)
        {
            if (ModelState.IsValid)
            {
                var mappedRole =  _mapper.Map<RoleViewModel, IdentityRole>(Role);
                await roleManager.CreateAsync(mappedRole); 
                return RedirectToAction("Index");

            }
            return View();
        }


        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); // => 400 Client Error 
            }

            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();

            }
            var mappedRole = _mapper.Map<IdentityRole , RoleViewModel>(role);

            return View(ViewName, mappedRole);

        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel role, [FromRoute] string id)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await roleManager.FindByIdAsync(id);

                    Role.Id = role.Id;
                    Role.Name = role.RoleName ;
                    await roleManager.UpdateAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(role);
        }



        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleViewModel model, [FromRoute] string Id)
        {
            if (Id != model.Id)
            {
                return BadRequest();
            }
            try
            {
                var Role = await roleManager.FindByIdAsync(Id);  
                await roleManager.DeleteAsync(Role);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Error", "Home");
            }

        }



    }
}
