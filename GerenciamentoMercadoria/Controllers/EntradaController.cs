using GerenciamentoMercadoria.Models;
using GerenciamentoEntrada.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GerenciamentoMercadoria.Models.ViewModel;
using System.Globalization;
using FastReport.Export.PdfSimple;
using System.Diagnostics;

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
        public IActionResult Index()
        {
            var model = new EntradaViewModel();
            model.Entrada = _entradaRepository.Listar();
       
            return View(model);
        
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
        public IActionResult Index(EntradaViewModel entradaView) 
        {
            var diafim = DateTime.DaysInMonth(entradaView.DataMes.Year, entradaView.DataMes.Month).ToString("d2");
            var mes = entradaView.DataMes.Month.ToString("d2");
            var ano = entradaView.DataMes.Year.ToString();

            entradaView.Entrada = _entradaRepository.Listar(diafim, mes, ano);

            //TempData["listaEntrada"] = entradaView.Entrada;
            //TempData["mes"] = diafim;
            //TempData["ano"] = diafim;

            if (Request.IsHttps)
            {
                return PartialView("_Lista",entradaView);
            }
            return View(entradaView);
        }
        [HttpGet]
        public IActionResult Relatorio()
        {
            var caminhoReport = Path.Combine(_webHostEnv.WebRootPath, @"Reports\ReportMvc.frx");

            var freport = new FastReport.Report();
            //var diafim = TempData["listaEntrada"] as list;
            Debug.WriteLine("---------------");
            var entradaList = _entradaRepository.Listar();

            freport.Report.Load(caminhoReport);
            freport.Dictionary.RegisterBusinessObject(entradaList, "entradaList", 10, true);
            freport.Prepare();

            var pdfExport = new PDFSimpleExport();

            using MemoryStream ms = new MemoryStream();
            pdfExport.Export(freport, ms);
            ms.Flush();

            return File(ms.ToArray(), "application/pdf");
        }

    }
}