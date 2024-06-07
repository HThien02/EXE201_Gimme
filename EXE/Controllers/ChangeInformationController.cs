using EXE.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using NuGet.Protocol.Plugins;
using System.Net.Mail;
using System.Net;
using static System.Net.WebRequestMethods;

namespace EXE.Controllers
{
    public class ChangeInformationController : Controller
    {
        private readonly Exe201Context _context;
        Random random = new Random();
        int otp;
        public ChangeInformationController(Exe201Context context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
        var sessionUser = HttpContext.Session.GetInt32("UserSessionID");

            if (sessionUser != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserId == sessionUser);
                if (user != null)
                {
                    TempData["UserUsername"] = user.Username;
                    TempData["UserPassword"] = user.Password;
                    TempData["UserAddress"] = user.Address;
                    TempData["UserGmail"] = user.Gmail;
                    return View("/Views/Login/Information.cshtml");
                }
            }
            return View("/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Index(User user) {
            if (HttpContext.Session.GetInt32("UserSessionID") != null) {
                TempData["UserUsername"] = user.Username;
                TempData["UserPassword"] = user.Password;
                TempData["UserAddress"] = user.Address;
                TempData["UserGmail"] = user.Gmail;

                otp = random.Next(100000, 1000000); //Random OTP
                HttpContext.Session.SetInt32("OTPChangeInformation", otp);
                SendOTPToChangeInformation(user.Gmail, otp.ToString());

                return RedirectToAction("OTP");
            }
            return View("/Views/Home/Index/cshtml");
        }

        public IActionResult OTP()
        {
            TempData["UserAction"] = "ChangeInformation";
            return View("/Views/Login/OTP.cshtml");
        }

        [HttpPost]
        public IActionResult OTP(string username, string password, string gmail, string address,string OTP)
        {
            var sessionOTP = HttpContext.Session.GetInt32("OTPChangeInformation");
            if (OTP == sessionOTP.ToString())
            {
                var sessionUserID = HttpContext.Session.GetInt32("UserSessionID");
                var user = _context.Users.FirstOrDefault(u => u.UserId == sessionUserID);
                if ( user != null)
                {
                    user.Password = password;
                    user.Address = address;
                    user.Gmail = gmail;
                }
                _context.Users.Update(user);
                _context.SaveChanges();
                HttpContext.Session.Remove("OTP");

                return View("/Views/Home/Index.cshtml");
            }
            return View("Views/Home/Index.cshtml");
        }



        private void SendOTPToChangeInformation(string email, string otp)
        {
            try
            {
                var fromAddress = new MailAddress("gimmehomee@gmail.com");
                var toAddress = new MailAddress(email);
                const string frompass = "cgzobrjclhcbmggz";
                const string subject = "Gimme's OTP Verification";
                string title = "Để thay đổi mật khẩu, hãy nhập mã OTP:\n";
                string body = otp;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, frompass),
                    Timeout = 200000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = title + body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
