using MCE_Electronic_Orders.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace MCE_Electronic_Orders.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;

        public ModelContext _context = new ModelContext();
        public AppIdentityDbContext _contextUsers = new AppIdentityDbContext();


        public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash,ModelContext Commdiv, AppIdentityDbContext iden)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
            _context = Commdiv;
            _contextUsers = iden;


        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            var _Companies2 = _context.CommdivViews.Select(o => new { o.CompName, o.AgrmntCompCode }).OrderBy(v => v.CompName).Distinct();
            ViewBag.CompNo = new SelectList(_Companies2, "AgrmntCompCode", "CompName");

            var _market1 = _context.OrdersMarkets.Select(o => new { o.MarketName, o.StoreCode }).OrderBy(v => v.MarketName).Distinct();
            ViewBag.CompName = new SelectList(_market1, "StoreCode", "MarketName" );

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var UserBranchNo = user.BRANCHID;
                if (UserBranchNo==null)
                {
                    UserBranchNo = "1";
                }
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    BRANCHID = UserBranchNo,
                    PHONENUM = user.PHONENUM,
                    USERTYPE = user.USERTYPE,
                   
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                // uncomment for email confirmation (link - https://www.yogihosting.com/aspnet-core-identity-email-confirmation/)
                /*if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("Index");
                    else
                    {
                        // log email failed 
                    }
                }*/

                if (result.Succeeded)
                {
                    RoleModification model = new RoleModification();
                    var userid1 = _contextUsers.Users.Select(o => new { o.Id,o.Email }).Where(m => m.Email== user.Email).SingleOrDefault();

                    AppUser user2 = await userManager.FindByIdAsync(userid1.Id);
                    if (user2 != null)
                    {
                        if (user.USERTYPE == 1)
                        {
                            result = await userManager.AddToRoleAsync(user2, "store");
                            if (!result.Succeeded)
                                Errors(result);

                        }
                        else if (user.USERTYPE == 2)
                        {
                            result = await userManager.AddToRoleAsync(user2, "mce");
                            if (!result.Succeeded)
                                Errors(result);

                        }
                        else if (user.USERTYPE == 3)
                        {
                            result = await userManager.AddToRoleAsync(user2, "comp");
                            if (!result.Succeeded)
                                Errors(result);
                        }
                        else if (user.USERTYPE == 4)
                        {
                            result = await userManager.AddToRoleAsync(user2, "admin");
                            if (!result.Succeeded)
                                Errors(result);
                        }
                       
                    }
                    //else
                    //{
                    //    result = await userManager.AddToRoleAsync(user2, "admin");
                    //    if (!result.Succeeded)
                    //        Errors(result);

                    //}

                    return RedirectToAction("Index", "Home");
                    //await userManager.AddToRolesAsync(user,"store");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);


                    var _Companies2 = _context.CommdivViews.Select(o => new { o.CompName, o.AgrmntCompCode }).OrderBy(v => v.CompName).Distinct();
                    ViewBag.CompNo = new SelectList(_Companies2, "AgrmntCompCode", "CompName");

                    var _market1 = _context.OrdersMarkets.Select(o => new { o.MarketName, o.StoreCode }).OrderBy(v => v.MarketName).Distinct();
                    ViewBag.CompName = new SelectList(_market1, "StoreCode", "MarketName");

                }
            }
            return View(user);
        }

        //public async Task<IActionResult> Update(RoleModification model)
        //{
        //    IdentityResult result;
        //    if (ModelState.IsValid)
        //    {
        //        foreach (string userId in model.AddIds ?? new string[] { })
        //        {
        //            AppUser user = await userManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                result = await userManager.AddToRoleAsync(user, model.RoleName);
        //                if (!result.Succeeded)
        //                    Errors(result);
        //            }
        //        }
        //        foreach (string userId in model.DeleteIds ?? new string[] { })
        //        {
        //            AppUser user = await userManager.FindByIdAsync(userId);
        //            if (user != null)
        //            {
        //                result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
        //                if (!result.Succeeded)
        //                    Errors(result);
        //            }
        //        }
        //    }

        //    if (ModelState.IsValid)
        //        return RedirectToAction(nameof(Index));
        //    else
        //        return await Update(model.RoleId);
        //}


        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }
        [AllowAnonymous]
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", userManager.Users);
        }
    }
}
