using DulceSabor_Modulo2.Models;
using Microsoft.AspNetCore.Mvc;

namespace DulceSabor_Modulo2.Controllers
{
    public class CuentasController : Controller
    {
        private readonly RestauranteDbContext _restauranteContext;

        public CuentasController(RestauranteDbContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        public IActionResult Index()
        {
            var abiertas = (from c in _restauranteContext.Cuentas
                            join m in _restauranteContext.Mesas on c.IdMesa equals m.Id
                            where c.Estado == "ABIERTO"
                            select new
                            {
                                OrdenId = c.IdCuenta,
                                Mesa = m.NumeroMesa,
                                Nombre = c.Nombre + " " + c.Apellido,
                                subtotal = c.DetalleCuenta.Sum(s => s.Precio)
                            }).ToList();

            var cerradas = (from c in _restauranteContext.Cuentas
                            join m in _restauranteContext.Mesas on c.IdMesa equals m.Id
                            where c.Estado == "CERRADO"
                            select new
                            {
                                OrdenId = c.IdCuenta,
                                Mesa = m.NumeroMesa,
                                Nombre = c.Nombre + " " + c.Apellido,
                                subtotal = c.DetalleCuenta.Sum(s => s.Precio)
                            }).ToList();

            ViewData["abiertas"] = abiertas;
            ViewData["cerradas"] = cerradas;
            return View();
        }

        public IActionResult Pagar(PagarFormModel model)
        {
            Cuenta? cuenta = _restauranteContext.Cuentas.FirstOrDefault(c => c.IdCuenta == model.cuentaId);
            if (cuenta != null)
            {
                Mesa? mesa = _restauranteContext.Mesas.FirstOrDefault(m => m.Id == cuenta.IdMesa);
                cuenta.Estado = "CERRADO";
                mesa.Estado = true;
                _restauranteContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

    public class PagarFormModel ()
    {
        public int cuentaId { get; set; }
    }
}
