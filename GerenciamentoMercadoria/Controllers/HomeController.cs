using GerenciamentoMercadoria.Models.ViewModels;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GerenciamentoMercadoria.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;
        public readonly IWebHostEnvironment _webHostEnv;

        public HomeController(IWebHostEnvironment webHostEnv, IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
            _webHostEnv = webHostEnv;
        }
        public IActionResult Index(int? pagina)
        {
            IEnumerable<EntradaSaida> entradaSaida = _homeRepository.Listar(pagina);

            return View(entradaSaida); 
        }
        [HttpGet("EntradaSaida")]
        public IActionResult Index()
        {
            List<EntradaSaida> entradaSaida = _homeRepository.Listar().ToList();

            return Json(entradaSaida);
        }
        [HttpPost]
        public IActionResult Index(DateTime seachData, int? pagina)
        {
            IEnumerable<EntradaSaida> entradaSaida = _homeRepository.Pesquisar(seachData, pagina);
            //TempData["Lista"] = JsonConvert.SerializeObject(entrada);

            if (Request.IsHttps)
            {
                return PartialView("_Lista", entradaSaida);
            }
            return View(entradaSaida);
        }
       
    }
}
