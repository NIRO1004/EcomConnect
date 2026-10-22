
using EcomConnect.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcomConnectContext _context;

        public AccountController(EcomConnectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(
            string email,
            string password,
            string? returnUrl = null)
        {
            const string correoPrueba = "prueba@ecomconnect.com";
            const string contrasenaPrueba = "Laboratorio01";

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Ingresa tu correo y contraseña.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            bool correoExiste = await _context.Correos
                .AnyAsync(c => c.Correo1 == correoPrueba);

            bool credencialesValidas =
                string.Equals(email.Trim(), correoPrueba,
                    StringComparison.OrdinalIgnoreCase)
                && password == contrasenaPrueba
                && correoExiste;

            if (!credencialesValidas)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            HttpContext.Session.SetString("Usuario", correoPrueba);

            if (!string.IsNullOrEmpty(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ProbarCorreo()
        {
            bool existe = await _context.Correos
                .AnyAsync(c => c.Correo1 == "prueba@ecomconnect.com");

            return Content(existe
                ? "Correcto: ASP.NET Core encontró el correo en SQL Server."
                : "No se encontró el correo en la base de datos.");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
