using MCE_Electronic_Orders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MCE_Electronic_Orders.Controllers
{
    public class LoginController : Controller
    {
        private UserManager<AppUser> userManager;
        public LoginController(UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
        }

        [Authorize]
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = "Hello " + user.UserName;
            return View((object)message);
        }
    }
}
