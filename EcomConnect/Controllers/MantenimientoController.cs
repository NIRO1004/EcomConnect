
using EcomConnect.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomConnect.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly EcomConnectContext _context;

        public MantenimientoController(EcomConnectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento" });
            }

            return View();
        }

        public async Task<IActionResult> PersonasClientes()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/PersonasClientes" });
            }

            ViewBag.Personas = await _context.Personas
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Clientes = await _context.Clientes
                .AsNoTracking()
                .ToListAsync();

            return View();
        }



        public async Task<IActionResult> ProductosBodegas()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/ProductosBodegas" });
            }

            ViewBag.Productos = await _context.Productos
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Bodegas = await _context.Bodegas
                .AsNoTracking()
                .ToListAsync();

            return View();
        }


        public async Task<IActionResult> CatalogosTipos()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/CatalogosTipos" });
            }

            ViewBag.Generos = await _context.CatalogoGeneros
                .AsNoTracking().ToListAsync();

            ViewBag.TiposPersona = await _context.TipoPersonas
                .AsNoTracking().ToListAsync();

            ViewBag.TiposCorreo = await _context.TipoCorreos
                .AsNoTracking().ToListAsync();

            ViewBag.TiposDireccion = await _context.TipoDireccions
                .AsNoTracking().ToListAsync();

            ViewBag.TiposTelefono = await _context.TipoTelefonos
                .AsNoTracking().ToListAsync();

            ViewBag.MetodosPago = await _context.MetodoPagos
                .AsNoTracking().ToListAsync();

            return View();
        }


        public async Task<IActionResult> PedidosRastreo()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/PedidosRastreo" });
            }

            ViewBag.Pedidos = await _context.Pedidos
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Rastreos = await _context.RastreoPedidos
                .AsNoTracking()
                .ToListAsync();

            ViewBag.EstadosPedido = await _context.EstadoPedidos
                .AsNoTracking()
                .ToListAsync();

            ViewBag.DetallesPedido = await _context.PedidoDetalles
                .AsNoTracking()
                .ToListAsync();

            return View();
        }


        public async Task<IActionResult> FacturacionPagos()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/FacturacionPagos" });
            }

            ViewBag.Facturas = await _context.Facturas
                .AsNoTracking()
                .ToListAsync();

            ViewBag.DetallesFactura = await _context.FacturaDetalles
                .AsNoTracking()
                .ToListAsync();

            ViewBag.MetodosPago = await _context.MetodoPagos
                .AsNoTracking()
                .ToListAsync();

            return View();
        }


        public async Task<IActionResult> PreferenciasSolicitudes()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/PreferenciasSolicitudes" }
                );
            }

            ViewBag.Preferencias = await _context.Preferencia
                .AsNoTracking()
                .ToListAsync();

            ViewBag.QuejasSolicitudes = await _context.QuejaSolicituds
                .AsNoTracking()
                .ToListAsync();

            return View();
        }


    }
}
