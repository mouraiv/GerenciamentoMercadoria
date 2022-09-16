using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciamentoMercadoria.Controllers
{
    public class MercadoriaController : Controller
    {
        private readonly IMercadoriaRepository _mercadoriaRepository;

        public MercadoriaController(IMercadoriaRepository mercadoriaRepository)
        {
            _mercadoriaRepository = mercadoriaRepository;
        }
        public IActionResult Index()
        {
            List<Mercadoria> mercadoria = _mercadoriaRepository.Listar();
            return View(mercadoria);
        }
        public IActionResult Inserir()
        {
            ViewData["CategoriaId"] =
                    new SelectList(_mercadoriaRepository.Categorias(), "Id", "Nome");

            ViewData["FabricanteId"] =
                new SelectList(_mercadoriaRepository.Fabricantes(), "Id", "Nome");
            return View();
        }
        public IActionResult Editar(int id)
        {
            Mercadoria mercadoria = _mercadoriaRepository.CarregarId(id);
            return View(mercadoria);
        }
        public IActionResult Confirmacao(int id)
        {
            Mercadoria mercadoria = _mercadoriaRepository.CarregarId(id);
            return View(mercadoria);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                _mercadoriaRepository.Deletar(id);
                TempData["Sucesso"] = "Mercadoria Excluída com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Excluir - {error.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Inserir(Mercadoria mercadoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_mercadoriaRepository.Listar().Any(p => p.Nome.Equals(mercadoria.Nome)))
                    {
                        TempData["Falha"] = $"A Mercadoria {mercadoria.Nome} já existe!";
                        return View(mercadoria);
                    }
                    else
                    {
                        _mercadoriaRepository.Cadastrar(mercadoria);
                        TempData["Sucesso"] = "Inserido com sucesso!.";
                        return RedirectToAction("Inserir");
                    }

                }
                ViewData["CategoriaId"] =
                    new SelectList(_mercadoriaRepository.Categorias(), "Id", "Nome");
         
                ViewData["FabricanteId"] =
                    new SelectList(_mercadoriaRepository.Fabricantes(), "Id", "Nome");
                return View(mercadoria);
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao inserir - {error.Message}";
                return View(mercadoria);
            }

        }
        [HttpPost]
        public IActionResult Editar(Mercadoria mercadoria)
        {
            try
            {
                if (_mercadoriaRepository.Listar().Any(p => p.Nome.Equals(mercadoria.Nome)))
                {
                    TempData["Falha"] = $"A Mercadoria {mercadoria.Nome} já existe!";
                    return View(mercadoria);
                }
                else
                {
                    _mercadoriaRepository.Atualizar(mercadoria);
                    TempData["Sucesso"] = "Atualizado com sucesso!.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Atualizar - O Campo nome precisa conter um valor.";
                return View(mercadoria);
            }
        }

    }
}