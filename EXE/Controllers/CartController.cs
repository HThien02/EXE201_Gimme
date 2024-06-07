using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EXE.DataAccess;

namespace EXE.Controllers
{
    public class CartController : Controller
    {
        private readonly Exe201Context _exeContext;

        public CartController(Exe201Context exeContext)
        {
            _exeContext = exeContext;
        }

        public IActionResult Index()
        {
            var addressString = TempData["AddressString"] as string;

            // Lấy danh sách các project từ cơ sở dữ liệu và nạp dữ liệu liên kết
            var projects = _exeContext.Projects
                .Include(p => p.Paper)
                .Include(p => p.Size)
                .Include(p => p.Spring)
                .Include(p => p.Number)
                .ToList();

            // Truyền addressString và danh sách projects tới view
            ViewData["AddressString"] = addressString;
            return View("Index", projects);
        }

        public IActionResult InputAddress()
        {
            return View("InputAddress", "Cart");
        }

        [HttpPost]
        public IActionResult Address() // Thêm tham số để nhận dữ liệu từ form
        {
            var city = HttpContext.Request.Form["city"].FirstOrDefault();
            var district = HttpContext.Request.Form["district"].FirstOrDefault();
            var ward = HttpContext.Request.Form["ward"].FirstOrDefault();
            var numberAddress = HttpContext.Request.Form["numberAddress"].FirstOrDefault();
            var note = HttpContext.Request.Form["note"].FirstOrDefault();
            string addressString = $"{numberAddress}, {ward}, {district}, {city}";


            TempData["AddressString"] = addressString;

            return RedirectToAction("Index", "Cart");
        }
    }
}
