using Microsoft.AspNetCore.Mvc;

namespace DulceSabor_Modulo2.Controllers
{
    public class MesasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
