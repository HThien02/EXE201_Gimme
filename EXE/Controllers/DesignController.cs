using Microsoft.AspNetCore.Mvc;

namespace EXE.Controllers
{
    public class DesignController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
