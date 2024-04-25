using Assighment.Models;
using Assighment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assighment.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        //Injection
        public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid == true)
            {
                //Check
                ApplicationUser usermodel = await userManager.FindByNameAsync(userVM.Username);
                if (usermodel != null)
                {
                  bool found = await  userManager.CheckPasswordAsync(usermodel, userVM.Password);
                    if (found)
                    {
                        //create cookie
                        //await signInManager.SignInAsync(usermodel, userVM.RemmeberMe);//default claims
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("Address", usermodel.Address));
                        await signInManager.SignInWithClaimsAsync(usermodel, userVM.RemmeberMe, claims);
                        return RedirectToAction("Index", "Student");
                    }
                }
                ModelState.AddModelError("", "UserName and password Invaild");
            }
            return View(userVM);
        }
        //registration
        [HttpGet] //<anchor tag>
       public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
    
        public async Task <IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if(ModelState.IsValid)
            {
                //Create Account
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;
                userModel.Address = newUserVM.Address;
               IdentityResult result= await userManager.CreateAsync(userModel,newUserVM.Password);// create : usermanager=>userstore=>context=>database
                if(result.Succeeded)
                {
                    //Create cookie
                  await signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return View(newUserVM);
        }
        public async Task< IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
        //Admin
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task< IActionResult> AddAdmin(RegisterUserViewModel newVM)
        {
            if (ModelState.IsValid)
            {
                //Create Account
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newVM.UserName;
                userModel.PasswordHash = newVM.Password;
                userModel.Address = newVM.Address;
                IdentityResult result = await userManager.CreateAsync(userModel, newVM.Password);// create : usermanager=>userstore=>context=>database
                if (result.Succeeded)
                {
                    //Assign toRole
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    //Create cookie
                    await signInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(newVM);
            
        }

    }
}
