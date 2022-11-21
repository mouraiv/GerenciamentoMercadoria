using FastReport.Export.PdfSimple;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            //TempData["Lista"] = JsonConvert.SerializeObject(saida);

            if (Request.IsHttps)
            {
                return PartialView("_Lista", saida);
            }
            return View(saida);
        }

        [HttpGet]
        public IActionResult Relatorio()
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"Reports\ReportMvc.frx");
            var freport = new FastReport.Report();

            //var saidaList = TempData["Lista"] == null ? _entradaRepository.Listar() : JsonConvert.DeserializeObject<IEnumerable<Entrada>>(TempData["Lista"].ToString());

            freport.Report.Load(caminhoReport);
            freport.Dictionary.RegisterBusinessObject(null, "saidaList", 10, true);
            freport.Prepare();

            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }

    }
}