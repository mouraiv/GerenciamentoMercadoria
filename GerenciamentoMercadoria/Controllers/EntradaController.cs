using GerenciamentoMercadoria.Models;
using GerenciamentoEntrada.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FastReport.Export.PdfSimple;
using Newtonsoft.Json;

namespace GerenciamentoEntrada.Controllers
{
    public class EntradaController : Controller
    {
        private readonly IEntradaRepository _entradaRepository;
        public readonly IWebHostEnvironment _webHostEnv;

        public EntradaController(IWebHostEnvironment webHostEnv, IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
            _webHostEnv = webHostEnv;
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
                TempData["Falha"] = $"Erro ao Atualizar - O Campo nome precisa conter um valor.";
                return View(entrada);
            }
        }
        [HttpPost]
        public IActionResult Index(DateTime seachData,string produto, int? pagina) 
        {
            IEnumerable<Entrada> entrada = _entradaRepository.Pesquisar(seachData, produto, pagina);
            //TempData["Lista"] = JsonConvert.SerializeObject(entrada);

            if (Request.IsHttps)
            {
                return PartialView("_Lista",entrada);
            }
            return View(entrada);
        }

        [HttpGet]
        public IActionResult Relatorio()
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"Reports\ReportMvc.frx");
            var freport = new FastReport.Report();

            //var entradaList = TempData["Lista"] == null ? _entradaRepository.Listar() : JsonConvert.DeserializeObject<IEnumerable<Entrada>>(TempData["Lista"].ToString());

            freport.Report.Load(caminhoReport);
            freport.Dictionary.RegisterBusinessObject(null, "entradaList", 10, true);
            freport.Prepare();
            
            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }   
    }
}