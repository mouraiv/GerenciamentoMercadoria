using GerenciamentoMercadoria.Models;
using GerenciamentoEntrada.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoEntrada.Controllers
{
    public class EntradaController : Controller
    {
        private readonly IEntradaRepository _entradaRepository;

        public EntradaController(IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
        }
        public IActionResult Index()
        {
            List<Entrada> Entrada = _entradaRepository.Listar();
            return View(Entrada);
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

    }
}