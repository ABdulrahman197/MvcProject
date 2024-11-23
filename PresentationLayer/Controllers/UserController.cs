using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.reposatories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper )
        {
            this.userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue) 
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var user = await userManager.Users.Select( U => new UserViewModel()
                {
                    Id = U.Id,
                    Fname = U.Fname,
                    Lname = U.Lname,

                    Email = U.Email,
                    PhoneNumber = U.PhoneNumber,
                    Roles = userManager.GetRolesAsync(U).Result,

                }).ToListAsync();  


                return View(user);
            }
            else
            {
                var User = await userManager.FindByEmailAsync(SearchValue); // Applicaton User 
                var MappedUser = new UserViewModel()
                {
                    Id = User.Id,
                    Fname = User.Fname,
                    Lname = User.Lname,

                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Roles = userManager.GetRolesAsync(User).Result,

                };
                return View(new List<UserViewModel> { MappedUser});
            }
            
        }


        public async Task<IActionResult> Details(string? id, string ViewName = "Details" )
        {
            if (id is null)
            {
                return BadRequest(); // => 400 Client Error 
            }

            var User = await userManager.FindByIdAsync(id) ;

            if (User == null)
            {
                return NotFound();

            }
            var mappedUser = _mapper.Map<ApplicationUser , UserViewModel>(User); 

            return View(ViewName , mappedUser);

        }

        public async Task<IActionResult> Edit(string? id)
        {
            return await Details( id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user, [FromRoute] string id)
        {
            if (id != user.Id) 
            {
                return BadRequest(); 
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var User = await userManager.FindByIdAsync(id);
                 
                    User.PhoneNumber = user.PhoneNumber;
                    User.Lname = user.Lname;
                    User.Fname = user.Fname;
                    await userManager.UpdateAsync(User);
                    return RedirectToAction(nameof(Index)); 
                }
                catch(System.Exception ex )
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(user); 
        }



        public async Task<IActionResult> Delete(string id ) 
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserViewModel model, [FromRoute] string Id)
        {
            if (Id != model.Id)
            {
                return BadRequest();
            }
            try
            {
                var User =await userManager.FindByIdAsync(Id); 
                await userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Error" , "Home");
            }

        }



    }
}
