using FastReport.Export.PdfSimple;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Rotativa.AspNetCore;

namespace GerenciamentoMercadoria.Controllers
{
    public class SaidaController : Controller
    {
        private readonly ISaidaRepository _saidaRepository;
        public readonly IWebHostEnvironment _webHostEnv;

        public SaidaController(IWebHostEnvironment webHostEnv, ISaidaRepository saidaRepository)
        {
            _saidaRepository = saidaRepository;
            _webHostEnv = webHostEnv;
        }
        public IActionResult Index(int? pagina)
        {
            IEnumerable<Saida> saida = _saidaRepository.Listar(pagina);
            return View(saida);
        }
        public IActionResult Inserir()
        {
            ViewData["MercadoriaId"] =
                new SelectList(_saidaRepository.Mercadorias(), "Id", "Nome");
            return View();
        }
        public IActionResult Editar(int id)
        {
            Saida Saida = _saidaRepository.CarregarId(id);

            ViewData["MercadoriaId"] =
                   new SelectList(_saidaRepository.Mercadorias(), "Id", "Nome");
            return View(Saida);
        }
        public IActionResult Confirmacao(int id)
        {
            Saida Saida = _saidaRepository.CarregarId(id);
            return View(Saida);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                _saidaRepository.Deletar(id);
                TempData["Sucesso"] = "Saida Excluída com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Excluir - {error.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Inserir(Saida Saida)
        {
            ViewData["MercadoriaId"] =
                new SelectList(_saidaRepository.Mercadorias(), "Id", "Nome");
            try
            {
                if (ModelState.IsValid)
                {
                    _saidaRepository.Cadastrar(Saida);
                    TempData["Sucesso"] = "Inserido com sucesso!.";
                    return RedirectToAction("Inserir");
                }
                return View(Saida);
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao inserir - {error.Message}";
                return View(Saida);
            }

        }
        [HttpPost]
        public IActionResult Editar(Saida saida)
        {
            ViewData["MercadoriaId"] =
                new SelectList(_saidaRepository.Mercadorias(), "Id", "Nome");

            try
            {
                _saidaRepository.Atualizar(saida);
                TempData["Sucesso"] = "Atualizado com sucesso!.";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Atualizar - O Campo nome precisa conter um valor.";
                return View(saida);
            }
        }
        [HttpPost]
        public IActionResult Index(DateTime seachData, int? pagina)
        {
            IEnumerable<Saida> saida = _saidaRepository.Pesquisar(seachData, pagina);
            IEnumerable<Saida> saidaRelatorio = _saidaRepository.Relatorio(seachData);

            TempData["Lista"] = JsonConvert.SerializeObject(saidaRelatorio);

            if (Request.IsHttps)
            {
                return PartialView("_Lista", saida);
            }
            return View(saida);
        }

        [HttpGet]
        public IActionResult Relatorio(DateTime seachData)
        {
            return View();
        }
        public IActionResult ExportPdf()
        {
            var data = DateTime.Parse($"1/{ DateTime.Now.Month}/{DateTime.Now.Year}");
            var saidaList = (TempData["Lista"] == null) ? _saidaRepository.Relatorio(data).ToList() : JsonConvert.DeserializeObject<IEnumerable<Saida>>(TempData["Lista"].ToString());

            var pdf = new ViewAsPdf
            {
                ViewName = "Relatorio",
                Model = saidaList
            };
            return pdf;
        }

    }
}