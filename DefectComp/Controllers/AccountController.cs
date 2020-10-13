using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DefectComp.Models.ViewModels;
using DefectComp.Models.Users;

namespace DefectComp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

        public AccountController(UserManager<AppUser> userMgr,
            SignInManager<AppUser> signInMgr,
            IUserValidator<AppUser> userVldtr,
            IPasswordValidator<AppUser> pwdVldtr,
            IPasswordHasher<AppUser> pwdHasher)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            userValidator = userVldtr;
            passwordValidator = pwdVldtr;
            passwordHasher = pwdHasher;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnURL)
        {
            return View(new LoginModel() { ReturnUrl = returnURL});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(loginModel.Name);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    if((await signInManager.PasswordSignInAsync(user,loginModel.Pwd,false,false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }

            }
            ModelState.AddModelError("", "Неправильное имя или пароль!");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnURL="/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnURL);
        }

        // получить список пользователей
        public async Task<IActionResult> Index(int page = 1, string currentID = null, bool flagUpdate = false, string sort = "")
        {
            AppUserViewModel appUserViewModel = new AppUserViewModel()
            {
                AppUsers = userManager.Users,
                Page = page,
                Sort = sort,
                CurrentID = currentID
            };

            // фильтр по добавленной/отредактированной записи
            if (!string.IsNullOrEmpty(currentID) && flagUpdate)
            {
                AppUser user = await userManager.FindByIdAsync(currentID);
                if (user != null)
                {
                    appUserViewModel.AppUsers = userManager.Users.Where(p => p.Id == user.Id);
                }

            }

            return View(appUserViewModel);
        }

        // создание пользователя
        public ViewResult Create(int page = 1, string returnUrl = "", string sort = "")
        {
            ViewData["Title"] = "Пользователи: добавление";
            return View("Create",new AppUserCreateModel { Page = page, ReturnUrl = returnUrl, Sort = sort });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateModel createModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser(createModel.Name);
                IdentityResult result = await userManager.CreateAsync(user,createModel.Pwd);
                if(result.Succeeded)
                {
                    TempData["message"] = $"Запись \"{user.UserName}\" успешно добавлена!";
                    return RedirectToAction("Index", new { page = createModel.Page, sort = createModel.Sort, currentID = user.Id });
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(createModel);
        }

        // удаление пользователя
        [HttpPost]
        public async Task<IActionResult> Delete(string Id, int page = 1, string sort = "")
        {
            AppUser user = await userManager.FindByIdAsync(Id);
            if(user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    TempData["message"] = $"Запись \"{user.UserName}\" успешно удалена!";
                    return RedirectToAction("Index", new { page, sort });
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("","Пользователь не найден");
            }
            return RedirectToAction("Index", new { page, sort });
        }

        // редактирование пользователя
        public async Task<IActionResult> Edit(string Id, string returnUrl = "", int page = 1, string sort = "")
        {
            ViewData["Title"] = "Редактирование данных пользователя";

            AppUser user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                AppUserEditModel editModel = new AppUserEditModel()
                {
                    AppUser = user,
                    Page = page,
                    CurrentID = Id,
                    ReturnUrl = returnUrl,
                    Sort = sort
                };
                return View(editModel);
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
                return RedirectToAction("Index", new { page, sort});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string password, string returnUrl = "", int page = 1, string sort = "")
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                IdentityResult validPwd = null;
                if(!string.IsNullOrEmpty(password))
                {
                    validPwd = await passwordValidator.ValidateAsync(userManager,user,password);
                    if(validPwd.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,password);
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if(result.Succeeded)
                        {
                            TempData["message"] = $"Запись \"{user.UserName}\" сохранена!";
                            return RedirectToAction("Index", new { page, sort, currentID = user.Id });
                        }
                        else
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                    else
                    {
                        AddErrorsFromResult(validPwd);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Введен некорретный пароль!");
                }
            }
            else
            {
                ModelState.AddModelError("","Пользователь не найден");
            }

            AppUserEditModel editModel = new AppUserEditModel()
            {
                AppUser = user,
                Page = page,
                CurrentID = id,
                ReturnUrl = returnUrl,
                Sort = sort
            };
            return View(editModel);
        }

        // обработка ошибок
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
    }
}
