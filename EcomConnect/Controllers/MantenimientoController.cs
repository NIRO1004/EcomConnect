using EcomConnect.Data;
using EcomConnect.Models;
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

        [HttpGet]
        public async Task<IActionResult> CrearCliente()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/CrearCliente" });
            }

            ViewBag.Generos = await _context.CatalogoGeneros
                .AsNoTracking()
                .ToListAsync();

            ViewBag.TiposPersona = await _context.TipoPersonas
                .AsNoTracking()
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCliente(
    string Cedula,
    string Nombre,
    string Apellido1,
    string Apellido2,
    DateOnly FechaNacimiento,
    int CatalogoGeneroIdCatalogoGenero,
    int TipoPersonaIdtipoPersona,
    string MetodoPago,
    decimal Calificacion)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/CrearCliente" });
            }

            // Validar los datos recibidos.
            Cedula = Cedula?.Trim() ?? "";
            Nombre = Nombre?.Trim() ?? "";
            Apellido1 = Apellido1?.Trim() ?? "";
            Apellido2 = Apellido2?.Trim() ?? "";
            MetodoPago = MetodoPago?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(Cedula) ||
                string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Apellido1) ||
                string.IsNullOrWhiteSpace(Apellido2) ||
                string.IsNullOrWhiteSpace(MetodoPago))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
            }

            if (Cedula.Length > 10 ||
                Nombre.Length > 30 ||
                Apellido1.Length > 30 ||
                Apellido2.Length > 30 ||
                MetodoPago.Length > 50)
            {
                ModelState.AddModelError("", "Uno o más campos superan la longitud permitida.");
            }

            if (FechaNacimiento > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError(
                    nameof(FechaNacimiento),
                    "La fecha de nacimiento no puede ser futura.");
            }

            if (Calificacion < 0 || Calificacion > 9.99m)
            {
                ModelState.AddModelError(
                    nameof(Calificacion),
                    "La calificación debe estar entre 0 y 9.99.");
            }

            if (await _context.Personas.AnyAsync(p => p.Cedula == Cedula))
            {
                ModelState.AddModelError(
                    nameof(Cedula),
                    "Ya existe una persona registrada con esa cédula.");
            }

            bool generoExiste = await _context.CatalogoGeneros
                .AnyAsync(g => g.IdCatalogoGenero == CatalogoGeneroIdCatalogoGenero);

            bool tipoExiste = await _context.TipoPersonas
                .AnyAsync(t => t.IdtipoPersona == TipoPersonaIdtipoPersona);

            if (!generoExiste)
            {
                ModelState.AddModelError(
                    nameof(CatalogoGeneroIdCatalogoGenero),
                    "Seleccione un género válido.");
            }

            if (!tipoExiste)
            {
                ModelState.AddModelError(
                    nameof(TipoPersonaIdtipoPersona),
                    "Seleccione un tipo de persona válido.");
            }

            if (!ModelState.IsValid)
            {
                await CargarCatalogosCliente();
                return View();
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Registrar la persona.
                var persona = new Persona
                {
                    Cedula = Cedula,
                    Nombre = Nombre,
                    Apellido1 = Apellido1,
                    Apellido2 = Apellido2,
                    FechaNacimiento = FechaNacimiento,
                    CatalogoGeneroIdCatalogoGenero =
                        CatalogoGeneroIdCatalogoGenero,
                    TipoPersonaIdtipoPersona = TipoPersonaIdtipoPersona
                };

                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();

                // 2. Registrar al cliente asociado con esa persona.
                var cliente = new Cliente
                {
                    FechaAlta = DateOnly.FromDateTime(DateTime.Today),
                    FechaBaja = null,
                    MetodoPago = MetodoPago,
                    Calificacion = Calificacion,
                    PersonaCedula = persona.Cedula,
                    PersonaTipoPersonaIdtipoPersona =
                        persona.TipoPersonaIdtipoPersona
                };

                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["Mensaje"] = "El cliente se registró correctamente.";

                return RedirectToAction(nameof(PersonasClientes));
            }
            catch (DbUpdateException)
            {
                await transaction.RollbackAsync();

                ModelState.AddModelError(
                    "",
                    "No fue posible registrar el cliente. Verifique los datos e inténtelo nuevamente.");

                await CargarCatalogosCliente();
                return View();
            }
        }

        private async Task CargarCatalogosCliente()
        {
            ViewBag.Generos = await _context.CatalogoGeneros
                .AsNoTracking()
                .ToListAsync();

            ViewBag.TiposPersona = await _context.TipoPersonas
                .AsNoTracking()
                .ToListAsync();
        }


        [HttpGet]
        public async Task<IActionResult> EditarCliente(
            int idCliente, string cedula, int tipoPersonaId)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/PersonasClientes" });
            }

            var cliente = await _context.Clientes
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(c =>
                    c.IdCliente == idCliente &&
                    c.PersonaCedula == cedula &&
                    c.PersonaTipoPersonaIdtipoPersona == tipoPersonaId);

            if (cliente == null)
                return NotFound();

            ViewBag.Generos = await _context.CatalogoGeneros
                .AsNoTracking().ToListAsync();

            ViewBag.TiposPersona = await _context.TipoPersonas
                .AsNoTracking().ToListAsync();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCliente(
            int idCliente,
            string cedula,
            int tipoPersonaId,
            string Nombre,
            string Apellido1,
            string Apellido2,
            DateOnly FechaNacimiento,
            int CatalogoGeneroIdCatalogoGenero,
            string MetodoPago,
            decimal Calificacion)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/PersonasClientes" });
            }

            var cliente = await _context.Clientes
                .Include(c => c.Persona)
                .FirstOrDefaultAsync(c =>
                    c.IdCliente == idCliente &&
                    c.PersonaCedula == cedula &&
                    c.PersonaTipoPersonaIdtipoPersona == tipoPersonaId);

            if (cliente == null)
                return NotFound();

            Nombre = Nombre?.Trim() ?? "";
            Apellido1 = Apellido1?.Trim() ?? "";
            Apellido2 = Apellido2?.Trim() ?? "";
            MetodoPago = MetodoPago?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Apellido1) ||
                string.IsNullOrWhiteSpace(Apellido2) ||
                string.IsNullOrWhiteSpace(MetodoPago))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
            }

            if (Nombre.Length > 30 || Apellido1.Length > 30 ||
                Apellido2.Length > 30 || MetodoPago.Length > 50)
            {
                ModelState.AddModelError("", "Uno o más campos superan la longitud permitida.");
            }

            if (FechaNacimiento > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError(
                    nameof(FechaNacimiento),
                    "La fecha de nacimiento no puede ser futura.");
            }

            if (Calificacion < 0 || Calificacion > 9.99m)
            {
                ModelState.AddModelError(
                    nameof(Calificacion),
                    "La calificación debe estar entre 0 y 9.99.");
            }

            bool generoExiste = await _context.CatalogoGeneros
                .AnyAsync(g => g.IdCatalogoGenero == CatalogoGeneroIdCatalogoGenero);

            if (!generoExiste)
            {
                ModelState.AddModelError(
                    nameof(CatalogoGeneroIdCatalogoGenero),
                    "Seleccione un género válido.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Generos = await _context.CatalogoGeneros
                    .AsNoTracking().ToListAsync();

                ViewBag.TiposPersona = await _context.TipoPersonas
                    .AsNoTracking().ToListAsync();

                return View(cliente);
            }

            cliente.Persona.Nombre = Nombre;
            cliente.Persona.Apellido1 = Apellido1;
            cliente.Persona.Apellido2 = Apellido2;
            cliente.Persona.FechaNacimiento = FechaNacimiento;
            cliente.Persona.CatalogoGeneroIdCatalogoGenero =
                CatalogoGeneroIdCatalogoGenero;

            cliente.MetodoPago = MetodoPago;
            cliente.Calificacion = Calificacion;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "El cliente se actualizó correctamente.";

            return RedirectToAction(nameof(PersonasClientes));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarCliente(
    int idCliente, string cedula, int tipoPersonaId)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/PersonasClientes" });
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c =>
                    c.IdCliente == idCliente &&
                    c.PersonaCedula == cedula &&
                    c.PersonaTipoPersonaIdtipoPersona == tipoPersonaId);

            if (cliente == null)
            {
                TempData["MensajeError"] = "No se encontró el cliente.";
                return RedirectToAction(nameof(PersonasClientes));
            }

            // Verificar relaciones que podrían impedir la eliminación.
            bool tienePedidos = await _context.Pedidos
                .AnyAsync(p =>
                    p.ClienteIdCliente == cliente.IdCliente &&
                    p.ClientePersonaCedula == cliente.PersonaCedula &&
                    p.ClientePersonaTipoPersonaIdtipoPersona ==
                        cliente.PersonaTipoPersonaIdtipoPersona);

            if (tienePedidos)
            {
                TempData["MensajeError"] =
                    "No se puede eliminar el cliente porque tiene pedidos asociados.";
                return RedirectToAction(nameof(PersonasClientes));
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "El cliente se eliminó correctamente.";

            return RedirectToAction(nameof(PersonasClientes));
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


        [HttpGet]
        public async Task<IActionResult> HistorialCliente(
            int idCliente, string cedula, int tipoPersonaId)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction("Login", "Account",
                    new { returnUrl = "/Mantenimiento/PersonasClientes" });
            }

            var cliente = await _context.Clientes
                .Include(c => c.Persona)
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.IdCliente == idCliente &&
                    c.PersonaCedula == cedula &&
                    c.PersonaTipoPersonaIdtipoPersona == tipoPersonaId);

            if (cliente == null)
                return NotFound();

            var pedidos = await _context.Pedidos
                .AsNoTracking()
                .Where(p =>
                    p.ClienteIdCliente == idCliente &&
                    p.ClientePersonaCedula == cedula &&
                    p.ClientePersonaTipoPersonaIdtipoPersona == tipoPersonaId)
                .Include(p => p.PedidoDetalles)
                    .ThenInclude(d => d.Producto)
                .Include(p => p.RastreoPedidos)
                    .ThenInclude(r => r.EstadoPedidoIdEstadoPedidoNavigation)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();

            ViewBag.Cliente = cliente;
            return View(pedidos);
        }


        [HttpGet]
        public async Task<IActionResult> GestionSegmentos()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/GestionSegmentos" });
            }

            var segmentos = await _context.Segmentos
                .AsNoTracking()
                .Include(s => s.Cliente)
                    .ThenInclude(c => c.Persona)
                .OrderBy(s => s.Nombre)
                .ToListAsync();

            return View(segmentos);
        }


        [HttpGet]
        public async Task<IActionResult> ClasificacionClientes()
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/ClasificacionClientes" });
            }

            var clientes = await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Persona)
                .Include(c => c.Pedidos)
                .ToListAsync();

            var clasificaciones = clientes.Select(c =>
            {
                int cantidadPedidos = c.Pedidos.Count;
                decimal montoGastado = c.Pedidos.Sum(p => p.Total);

                return new ClasificacionClienteViewModel
                {
                    IdCliente = c.IdCliente,
                    Cedula = c.PersonaCedula,
                    TipoPersonaId = c.PersonaTipoPersonaIdtipoPersona,

                    NombreCompleto =
                        $"{c.Persona.Nombre} {c.Persona.Apellido1} {c.Persona.Apellido2}",

                    CantidadPedidos = cantidadPedidos,
                    MontoGastado = montoGastado,

                    ClasificacionFrecuencia = cantidadPedidos >= 5
                        ? "Cliente frecuente"
                        : "Cliente ocasional",

                    ClasificacionGasto = montoGastado >= 100000
                        ? "Alto gasto"
                        : "Gasto regular"
                };
            })
            .OrderByDescending(c => c.CantidadPedidos)
            .ToList();

            return View(clasificaciones);
        }



        [HttpGet]
        public async Task<IActionResult> CrearSegmento(string? clienteClave)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login", "Account",
                    new { returnUrl = "/Mantenimiento/CrearSegmento" });
            }

            await CargarClientesSegmento();

            if (!((List<Cliente>)ViewBag.Clientes).Any())
            {
                TempData["MensajeError"] =
                    "Debe existir al menos un cliente registrado para crear un segmento.";

                return RedirectToAction(nameof(GestionSegmentos));
            }

            if (!string.IsNullOrWhiteSpace(clienteClave))
            {
                var partes = clienteClave.Split('|');

                if (partes.Length == 3 &&
                    int.TryParse(partes[0], out int idCliente) &&
                    !string.IsNullOrWhiteSpace(partes[1]) &&
                    int.TryParse(partes[2], out int tipoPersonaId))
                {
                    string cedula = partes[1];

                    bool clienteExiste = await _context.Clientes.AnyAsync(c =>
                        c.IdCliente == idCliente &&
                        c.PersonaCedula == cedula &&
                        c.PersonaTipoPersonaIdtipoPersona == tipoPersonaId);

                    if (clienteExiste)
                    {
                        ViewBag.ClienteClaveSeleccionado =
                            $"{idCliente}|{cedula}|{tipoPersonaId}";
                    }
                }
            }

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearSegmento(
            string Nombre,
            string DescripcionSegmento,
            string Criterio,
            string Estado,
            string ClienteClave)
        {
            string? usuario = HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                return RedirectToAction(
                    "Login",
                    "Account",
                    new { returnUrl = "/Mantenimiento/CrearSegmento" });
            }

            Nombre = Nombre?.Trim() ?? "";
            DescripcionSegmento = DescripcionSegmento?.Trim() ?? "";
            Criterio = Criterio?.Trim() ?? "";

            int idCliente = 0;
            int tipoPersonaId = 0;
            string cedula = "";

            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length > 500)
            {
                ModelState.AddModelError(
                    nameof(Nombre),
                    "El nombre es obligatorio y admite hasta 500 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(DescripcionSegmento) ||
                DescripcionSegmento.Length > 255)
            {
                ModelState.AddModelError(
                    nameof(DescripcionSegmento),
                    "La descripción es obligatoria y admite hasta 255 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(Criterio) || Criterio.Length > 100)
            {
                ModelState.AddModelError(
                    nameof(Criterio),
                    "El criterio es obligatorio y admite hasta 100 caracteres.");
            }

            if (Estado != "1" && Estado != "0")
            {
                ModelState.AddModelError(
                    nameof(Estado),
                    "Seleccione un estado válido.");
            }

            var partes = (ClienteClave ?? "").Split('|');

            if (partes.Length != 3 ||
                !int.TryParse(partes[0], out idCliente) ||
                string.IsNullOrWhiteSpace(partes[1]) ||
                !int.TryParse(partes[2], out tipoPersonaId))
            {
                ModelState.AddModelError(
                    nameof(ClienteClave),
                    "Seleccione un cliente válido.");
            }
            else
            {
                cedula = partes[1];

                bool clienteExiste = await _context.Clientes.AnyAsync(c =>
                    c.IdCliente == idCliente &&
                    c.PersonaCedula == cedula &&
                    c.PersonaTipoPersonaIdtipoPersona == tipoPersonaId);

                if (!clienteExiste)
                {
                    ModelState.AddModelError(
                        nameof(ClienteClave),
                        "El cliente seleccionado no existe.");
                }
            }

            if (!ModelState.IsValid)
            {
                await CargarClientesSegmento();
                return View();
            }

            var segmento = new Segmento
            {
                Nombre = Nombre,
                DescripcionSegmento = DescripcionSegmento,
                Criterio = Criterio,
                Estado = System.Text.Encoding.UTF8.GetBytes(Estado),
                ClienteIdCliente = idCliente,
                ClientePersonaCedula = cedula,
                ClientePersonaTipoPersonaIdtipoPersona = tipoPersonaId
            };

            try
            {
                _context.Segmentos.Add(segmento);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] =
                    "El segmento se registró correctamente.";

                return RedirectToAction(nameof(GestionSegmentos));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(
                    "",
                    "No se pudo guardar el segmento. Verifique los datos e inténtelo nuevamente.");

                await CargarClientesSegmento();
                return View();
            }
        }

        private async Task CargarClientesSegmento()
        {
            ViewBag.Clientes = await _context.Clientes
                .AsNoTracking()
                .Include(c => c.Persona)
                .OrderBy(c => c.Persona.Nombre)
                .ToListAsync();
        }

    }
}
