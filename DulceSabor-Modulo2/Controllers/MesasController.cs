using DulceSabor_Modulo2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DulceSabor_Modulo2.Controllers
{
    public class MesasController : Controller
    {
        private readonly RestauranteDbContext _restauranteContext;

        public MesasController(RestauranteDbContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        public IActionResult Index()
        { 

            List<Mesa>? mesas = (from m in _restauranteContext.Mesas
                                    select m)?.ToList();
            List<Mesa> mesasDisponibles = mesas?.Where(m => m.Estado == true).ToList() ?? [];
            List<Mesa> mesasNoDisponibles = mesas?.Where(m => m.Estado == false).ToList() ?? [];

            ViewData["mesasDisponibles"] = mesasDisponibles;
            ViewData["mesasNoDisponibles"] = mesasNoDisponibles;
            return View();
        }
        public IActionResult Detalle(int Id)
        {
            Mesa? mesa = (from m in _restauranteContext.Mesas
                          where m.Id == Id
                          select m).FirstOrDefault();

            var platos = (from m in _restauranteContext.DetalleCuentas
                          join p in _restauranteContext.Platos on m.IdPlato equals p.PlatoId
                          where m.IdPlato != null
                          select new
                          {
                              nombre = p.Nombre,
                              precio = m.Precio,
                              comentario = m.Comentario,
                              estado = m.Estado
                          }).ToList();

            var promociones = (from m in _restauranteContext.DetalleCuentas
                          join p in _restauranteContext.Promociones on m.IdPromocion equals p.PromocionId
                          where m.IdPromocion != null
                               select new
                               {
                                   nombre = "Promo " + p.Nombre,
                                   precio = m.Precio,
                                   comentario = m.Comentario,
                                   estado = m.Estado
                               }).ToList();

            var combos = (from m in _restauranteContext.DetalleCuentas
                          join p in _restauranteContext.Combos on m.IdCombo equals p.ComboId
                          where m.IdCombo != null
                          select new {
                              nombre = "Combo " + p.Nombre,
                              precio = m.Precio,
                              comentario = m.Comentario,
                              estado = m.Estado
                          }).ToList();

            Cuenta? c = (from m in _restauranteContext.Cuentas
                        where m.IdMesa == Id && m.Estado.Equals("ABIERTO")
                        select m).FirstOrDefault();

            ViewData["mesa"] = mesa;
            ViewData["total"] = platos.Sum(d => d.precio) + promociones.Sum(d => d.precio) + combos.Sum(d => d.precio);
            ViewData["cuenta"] = c;
            ViewData["platos"] = platos;
            ViewData["promociones"] = promociones;
            ViewData["combos"] = combos;

            return View();
        }

        public IActionResult AgregarCliente(AgregarClienteForm model)
        {
            Cuenta cuenta = new()
            {
                Nombre = model.nombre,
                Apellido = model.apellido,
                IdMesa = model.idMesa,
                FechaApertura = DateTime.Now,
                Estado = "ABIERTO"
            };

            Mesa? m = _restauranteContext.Mesas.FirstOrDefault(m => m.Id == model.idMesa);
            m.Estado = false;

            _restauranteContext.Cuentas.Add(cuenta);
            _restauranteContext.SaveChanges();

            return RedirectToAction("Detalle", new { Id = model.idMesa });
        }
    }

    public class AgregarClienteForm ()
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int idMesa { get; set; }
    }
}
