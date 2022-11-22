using GerenciamentoMercadoria.Models;
using GerenciamentoEntrada.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Newtonsoft.Json;

namespace GerenciamentoEntrada.Controllers
{
    public class EntradaController : Controller
    {
        private readonly IEntradaRepository _entradaRepository;

        public EntradaController(IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
        }
        public IActionResult Index(int? pagina)
        {
            IEnumerable<Entrada> entrada = _entradaRepository.Listar(pagina);
       
            return View(entrada);
        
        }
        public IActionResult Inserir()
        {
            ViewData["MercadoriaId"] =
                new SelectList(_entradaRepository.Mercadorias(), "Id", "Nome");
            return View();
        }
        public IActionResult Editar(int id)
        {
            Entrada Entrada = _entradaRepository.CarregarId(id);

            ViewData["MercadoriaId"] =
                   new SelectList(_entradaRepository.Mercadorias(), "Id", "Nome");
            return View(Entrada);
        }
        public IActionResult Confirmacao(int id)
        {
            Entrada Entrada = _entradaRepository.CarregarId(id);
            return View(Entrada);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                _entradaRepository.Deletar(id);
                TempData["Sucesso"] = "Entrada Excluída com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Excluir - {error.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Inserir(Entrada entrada)
        {
            ViewData["MercadoriaId"] =
                new SelectList(_entradaRepository.Mercadorias(), "Id", "Nome");
            try
            {
                if (ModelState.IsValid)
                {
                  _entradaRepository.Cadastrar(entrada);
                  TempData["Sucesso"] = "Inserido com sucesso!.";
                  return RedirectToAction("Inserir");
                }
                return View(entrada);
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao inserir - {error.Message}";
                return View(entrada);
            }

        }
        [HttpPost]
        public IActionResult Editar(Entrada entrada)
        {
            ViewData["MercadoriaId"] =
                new SelectList(_entradaRepository.Mercadorias(), "Id", "Nome");

            try
            {
              _entradaRepository.Atualizar(entrada);
              TempData["Sucesso"] = "Atualizado com sucesso!.";
              return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Atualizar - O Campo nome precisa conter um valor. {error.Message}";
                return View(entrada);
            }
        }
        [HttpPost]
        public IActionResult Index(DateTime seachData, int? pagina) 
        {
            IEnumerable<Entrada> entrada = _entradaRepository.Pesquisar(seachData, pagina);
            IEnumerable<Entrada> saidaRelatorio = _entradaRepository.Relatorio(seachData);

            TempData["Lista"] = JsonConvert.SerializeObject(saidaRelatorio);

            if (Request.IsHttps)
            {
                return PartialView("_Lista",entrada);
            }
            return View(entrada);
        }

        [HttpGet]
        public IActionResult Relatorio()
        {
            return View();
        }
        public IActionResult ExportPdf()
        {
            var data = DateTime.Parse($"1/{DateTime.Now.Month}/{DateTime.Now.Year}");
            var entradaList = (TempData["Lista"] == null) ? _entradaRepository.Relatorio(data).ToList() : JsonConvert.DeserializeObject<IEnumerable<Entrada>>(TempData["Lista"].ToString());

            var pdf = new ViewAsPdf
            {
                ViewName = "Relatorio",
                Model = entradaList
            };
            return pdf;
        }
    }
}