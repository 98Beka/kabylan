using KabylanMVC.PL.Models;
using KabylanMVC.PL.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace KabylanMVC.PL.Controllers {
    public class AccountController : Controller {
        private UserContext db;

        RegisterConfirmSingleton registerConfirm;
        public AccountController(UserContext context) {
            registerConfirm = RegisterConfirmSingleton.getInstance();
            db = context;
        }
        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model) {
            if (ModelState.IsValid) {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null) {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterConfirm() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model) {
            if (ModelState.IsValid) {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null) {
                    registerConfirm.Code = new Random(10).Next().ToString();
                    registerConfirm.Email = model.Email;
                    registerConfirm.Password = model.Password;
                    SendEmail(registerConfirm.Code);

                    return RedirectToAction("RegisterConfirm");
                } else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterConfirm(string code) {
            if (code == registerConfirm.Code) {
                var user = new User { Email = registerConfirm.Email, Password = registerConfirm.Password };
                db.Users.Add(user);
                await db.SaveChangesAsync();

                await Authenticate(registerConfirm.Email);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        private void SendEmail(string text) {
            var from = "1998tursunkulov@mail.ru";
            var to = "tursunkulovbeka@gmail.com";

            var username = "1998tursunkulov@mail.ru"; // get from Mailtrap
            var password = "fvWhvDmFhsZtNas1gM9L"; // get from Mailtrap

            using (var client = new SmtpClient("smtp.mail.ru", 587) {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
        }) {
                client.Send(from, to, text, string.Empty);

            }


        }
        private async Task Authenticate(string userName) {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }

    class RegisterConfirmSingleton {
        private static RegisterConfirmSingleton instance;
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Не верный код")]
        public string Code { get; set; } = string.Empty;

        private RegisterConfirmSingleton() { }

        public static RegisterConfirmSingleton getInstance() {
            if (instance == null)
                instance = new RegisterConfirmSingleton();
            return instance;
        }
    }
}
