﻿using DulceSabor_Modulo2.Models;
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

            var totalPlatos = (from p in _restauranteContext.Platos
                               select p).ToList();

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

            var totalPromos = (from p in _restauranteContext.Promociones
                               select p).ToList();

            var combos = (from m in _restauranteContext.DetalleCuentas
                          join p in _restauranteContext.Combos on m.IdCombo equals p.ComboId
                          where m.IdCombo != null
                          select new {
                              nombre = "Combo " + p.Nombre,
                              precio = m.Precio,
                              comentario = m.Comentario,
                              estado = m.Estado
                          }).ToList();

            var totalCombos = (from p in _restauranteContext.Combos
                               select p).ToList();

            Cuenta? c = (from m in _restauranteContext.Cuentas
                        where m.IdMesa == Id && m.Estado.Equals("ABIERTO")
                        select m).FirstOrDefault();

            ViewData["mesa"] = mesa;
            ViewData["total"] = platos.Sum(d => d.precio) + promociones.Sum(d => d.precio) + combos.Sum(d => d.precio);
            ViewData["cuenta"] = c;
            ViewData["platos"] = platos;
            ViewData["totalPlatos"] = totalPlatos;
            ViewData["promociones"] = promociones;
            ViewData["totalPromos"] = totalPromos;
            ViewData["combos"] = combos;
            ViewData["totalCombos"] = totalCombos;

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

        public IActionResult AgregarComida(AgregarComidaForm model)
        {
            int? IdMesa = _restauranteContext.Cuentas?.FirstOrDefault(c => c.IdCuenta == model.idCuenta)?.IdMesa;
            DetalleCuenta dc = new()
            {
                IdCuenta = model.idCuenta,
                IdPlato = model.platoId,
                IdCombo = model.comboId,
                IdPromocion = model.promoId,
                Precio = model.precio,
                Descuento = 0,
                Comentario = model.comentario,
                Estado = "SOLICITADO"
            };
            _restauranteContext.DetalleCuentas.Add(dc);
            _restauranteContext.SaveChanges();
            return RedirectToAction("Detalle", new { Id = IdMesa });
        }
    }

    public class AgregarClienteForm ()
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int idMesa { get; set; }
    }

    public class AgregarComidaForm
    {
        public string comentario { get; set; }
        public int idCuenta { get; set; }
        public int? platoId { get; set; }
        public int? promoId { get; set; }
        public int? comboId { get; set; }
        public decimal precio {  get; set; }
    }
}
