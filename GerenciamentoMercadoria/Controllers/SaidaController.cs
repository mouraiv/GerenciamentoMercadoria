using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoMercadoria.Controllers
{
    public class SaidaController : Controller
    {
        private readonly ISaidaRepository _saidaRepository;

        public SaidaController(ISaidaRepository saidaRepository)
        {
            _saidaRepository = saidaRepository;
        }
        public IActionResult Index()
        {
            List<Saida> saida = _saidaRepository.Listar();
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

    }
}