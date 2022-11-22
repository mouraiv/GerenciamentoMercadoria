using GerenciamentoMercadoria.Models.ViewModels;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rotativa.AspNetCore;

namespace GerenciamentoMercadoria.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }
        public IActionResult Index(int? pagina)
        {
            IEnumerable<EntradaSaida> entradaSaida = _homeRepository.Listar(pagina);

            return View(entradaSaida); 
        }
        [HttpGet("EntradaSaida")]
        public IActionResult EntradaSaidaJson()
        {
            List<EntradaSaida> entradaSaida = _homeRepository.Listar().ToList();

            return Json(entradaSaida);
        }
        [HttpPost]
        public IActionResult Index(DateTime seachData, int? pagina)
        {
            IEnumerable<EntradaSaida> entradaSaida = _homeRepository.Pesquisar(seachData, pagina);
            IEnumerable<EntradaSaida> entradaRelatorio = _homeRepository.Relatorio(seachData);

            TempData["Lista"] = JsonConvert.SerializeObject(entradaRelatorio);

            if (Request.IsHttps)
            {
                return PartialView("_Lista", entradaSaida);
            }
            return View(entradaSaida);
        }
        [HttpGet]
        public IActionResult Relatorio()
        {
            return View();
        }
        public IActionResult ExportPdf()
        {
            var data = DateTime.Parse($"1/{DateTime.Now.Month}/{DateTime.Now.Year}");
            var entradaSaidaList = (TempData["Lista"] == null) ? _homeRepository.Relatorio(data).ToList() : JsonConvert.DeserializeObject<IEnumerable<EntradaSaida>>(TempData["Lista"].ToString());

            var pdf = new ViewAsPdf
            {
                ViewName = "Relatorio",
                Model = entradaSaidaList
            };
            return pdf;
        }

    }
}
