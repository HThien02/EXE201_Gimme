using EXE.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EXE.Controllers
{
    public class ChooseController : Controller
    {
        private readonly Exe201Context _exeContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ChooseController(Exe201Context exeContext, IWebHostEnvironment hostingEnvironment)
        {
            _exeContext = exeContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Choose([FromForm] int PaperType, [FromForm] int Size, [FromForm] int BookCuffColor, [FromForm] int NumberOfPages, [FromForm] string ImageFrontPath, [FromForm] string ImageBackPath)
        {
            var userSessionID = HttpContext.Session.GetInt32("UserSessionID");
            if (userSessionID != null)
            {
                var paperPrice = _exeContext.Papers.FirstOrDefault(p => p.PaperId == PaperType)?.Money ?? 0;
                var sizePrice = _exeContext.Sizes.FirstOrDefault(s => s.SizeId == Size)?.Money ?? 0;
                var numberPrice = _exeContext.NumberOfPages.FirstOrDefault(n => n.NumberId == NumberOfPages)?.Money ?? 0;
                var cuffPrice = _exeContext.Springs.FirstOrDefault(s => s.SpringId == BookCuffColor)?.Money ?? 0;

                var total = paperPrice + sizePrice + numberPrice + cuffPrice;
                var project = new Project
                {
                    UserId = userSessionID.Value,
                    PaperId = PaperType,
                    SizeId = Size,
                    SpringId = BookCuffColor,
                    NumberId = NumberOfPages,
                    Image = ImageFrontPath, // Thêm đường dẫn ảnh mặt trước vào project
                    // Image = ImageBackPath, // Thêm đường dẫn ảnh mặt sau vào project
                    Total = total,
                    // Name = NotebookName, // Thêm tên sổ vào project
                    // Describe = NotebookDescription // Thêm mô tả sổ vào project
                };

                _exeContext.Projects.Add(project);
                _exeContext.SaveChanges();

                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFrontImages(IFormFile image, [FromForm] string imageType)
        {
            var userSessionID = HttpContext.Session.GetInt32("UserSessionID");
            if (userSessionID == null)
            {
                return BadRequest("User session ID is not available.");
            }

            if (image != null && image.Length > 0)
            {
                try
                {
                    // Tạo thư mục người dùng
                    string userFolder = Path.Combine(_hostingEnvironment.WebRootPath, "image", userSessionID.ToString());
                    if (!Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder);
                    }

                    string fileNameFront = "textures1.png";
                    string filePathFront = Path.Combine(userFolder, fileNameFront);

                    // Kiểm tra và xóa file cũ nếu đã tồn tại
                    if (System.IO.File.Exists(filePathFront))
                    {
                        System.IO.File.Delete(filePathFront);
                    }

                    using (var stream = new FileStream(filePathFront, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    string imagePath = $"/image/{userSessionID}/{fileNameFront}";

                    return Ok(new { imagePath }); // Trả về imagePath
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("No image uploaded.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadBackImages(IFormFile image, [FromForm] string imageType)
        {
            var userSessionID = HttpContext.Session.GetInt32("UserSessionID");
            if (userSessionID == null)
            {
                return BadRequest("User session ID is not available.");
            }

            if (image != null && image.Length > 0)
            {
                try
                {
                    // Tạo thư mục người dùng
                    string userFolder = Path.Combine(_hostingEnvironment.WebRootPath, "image", userSessionID.ToString());
                    if (!Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder);
                    }

                    string fileNameBack = "textures2.png";
                    string filePathBack = Path.Combine(userFolder, fileNameBack);

                    // Kiểm tra và xóa file cũ nếu đã tồn tại
                    if (System.IO.File.Exists(filePathBack))
                    {
                        System.IO.File.Delete(filePathBack);
                    }

                    using (var stream = new FileStream(filePathBack, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    string imagePath = $"/image/{userSessionID}/{fileNameBack}";

                    return Ok(new { imagePath }); // Trả về imagePath
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("No image uploaded.");
            }
        }
    }
}
