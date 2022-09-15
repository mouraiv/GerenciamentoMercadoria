using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoMercadoria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
