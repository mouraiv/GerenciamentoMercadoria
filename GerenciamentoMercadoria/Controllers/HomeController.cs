using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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
