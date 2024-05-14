using EXE.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EXE.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var username = HttpContext.Request.Form["username"];
            var password = HttpContext.Request.Form["password"];

            if (ModelState.IsValid)
            {
                if (user.Username == username && user.Password == password) {
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ModelState.AddModelError(string.Empty, "Username hoặc Password không chính xác.");
                }
            }
            return View();
        }
    }
}
