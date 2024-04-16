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

            var listaDeMesas = (from m in _restauranteContext.Mesas
                                select new
                                {
                                    nummesa = m.NumeroMesa
                                }).ToList();

            ViewData["listadoMesas"] = listaDeMesas;

            return View();


        }
        public IActionResult Detalle()
        {
            return View();
        }
    }
}
