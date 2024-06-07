using EXE.DataAccess;
using EXE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;

namespace EXE.Controllers
{
    public class LoginController : Controller
    {

        private readonly Exe201Context _context;
        int otp;
        Random random = new Random();
        public LoginController(Exe201Context context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {

            if (user == null)
            {
                return BadRequest("User object is null.");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var myUser = _context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);

            if (myUser != null)
            {
                byte[] avatarData;
                try
                {
                    avatarData = myUser.Avatar != null ? Convert.FromBase64String(myUser.Avatar) : new byte[0];
                }
                catch (FormatException ex)
                {

                    Console.WriteLine("Error converting avatar data from Base64: " + ex.Message);
                    avatarData = new byte[0];
                }
                HttpContext.Session.SetInt32("UserSessionID", myUser.UserId);
                HttpContext.Session.SetString("UserSession", myUser.Username);
                HttpContext.Session.SetString("UserSessionPass", myUser.Password);

                HttpContext.Session.Set("UserSessionAva", avatarData);
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View(user);
            }
        }



        public IActionResult Home()

        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.MySessionPass = HttpContext.Session.GetString("UserSessionPass").ToString();
                byte[] avatarData = HttpContext.Session.Get("UserSessionAva") as byte[];


                string avatarBase64 = avatarData != null ? Convert.ToBase64String(avatarData) : string.Empty;


                ViewBag.MySessionAva = avatarBase64;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View("Register", "Login");
        }


        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = 1;
                //await _context.Users.AddAsync(user);
                //await _context.SaveChangesAsync();
                //TempData["Success"] = "Registered successfully";

                otp = random.Next(100000, 1000000); //Random OTP
                HttpContext.Session.SetInt32("OTP", otp);
                SendOTPToEmail(user.Gmail, otp.ToString());

                TempData["UserGmail"] = user.Gmail;
                TempData["UserUsername"] = user.Username;
                TempData["UserPassword"] = user.Password;
                TempData["UserOTP"] = otp.ToString();

                return RedirectToAction("OTP", "Login");
            }

            return View("Register", "Login");
        }

        public IActionResult OTP()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OTP(string userPassword, string userGmail, string userUsername, string OTP)
        {
            var sessionOTP = HttpContext.Session.GetInt32("OTP");
            if (OTP == sessionOTP.ToString())
            {
                var newUser = new User
                {
                    Username = userUsername,
                    Gmail = userGmail,
                    Password = userPassword
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Register", "Login");
        }



        [HttpPost]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //Gửi OTP
        private void SendOTPToEmail(string email, string otp)
        {
            try
            {
                var fromAddress = new MailAddress("gimmehomee@gmail.com");
                var toAddress = new MailAddress(email);
                const string frompass = "cgzobrjclhcb mggz";
                const string subject = "Gimme's OTP Verification";
                string title = "Cảm ơn bạn đã quan tâm đến dịch vụ thiết kế sổ tay của Gimme, hãy nhập mã OTP này để đăng ký thành viên!\n";
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

        public IActionResult Forgot() { 
            return View();
        }

        [HttpPost]
        public IActionResult OTPForgot(string gmail) {

            if (gmail != null) {
                otp = random.Next(100000, 1000000); //Random OTP
                HttpContext.Session.SetInt32("OTPChangePassword", otp);
                SendOTPToChangePassword(gmail, otp.ToString());


            }



            return View("Index", "Login");
        }

        private void SendOTPToChangePassword(string email, string otp)
        {
            try
            {
                var fromAddress = new MailAddress("gimmehomee@gmail.com");
                var toAddress = new MailAddress(email);
                const string frompass = "cgzobrjclhcb mggz";
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
