using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoMercadoria.Controllers
{
    public class SaidaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
