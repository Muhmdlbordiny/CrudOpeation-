using Assighment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Assighment.Controllers
{
    [Authorize(Roles ="Admin")]//cookie -Role :Admin
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        //Create Role
        //Link
        [HttpGet]
       public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> New(RoleViewModel newroleVM)
        {
            if (ModelState.IsValid == true) 
            {
                IdentityRole role = new IdentityRole();
                role.Name = newroleVM.RoleName;
               IdentityResult result = await roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return View(new RoleViewModel());
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
             }

            return View(newroleVM);
        }
    }
}
