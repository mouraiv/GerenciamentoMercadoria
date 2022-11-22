using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoMercadoria.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index()
        {
            List<Categoria> categoria = _categoriaRepository.Listar();
            return View(categoria);
        }
        public IActionResult Inserir()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            Categoria categoria = _categoriaRepository.CarregarId(id);
            return View(categoria);
        }
        public IActionResult Confirmacao(int id)
        {
            Categoria categoria = _categoriaRepository.CarregarId(id);
            return View(categoria);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                _categoriaRepository.Deletar(id);
                TempData["Sucesso"] = "Categoria Excluída com sucesso.";
                return RedirectToAction("Index");
            }
            catch(Exception error)
            {
                TempData["Falha"] = $"Erro ao Excluir - {error.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Inserir(Categoria categoria)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    if (_categoriaRepository.Listar().Any(p => p.Nome.Equals(categoria.Nome)))
                    {
                        TempData["Falha"] = $"A categoria {categoria.Nome} já existe!";
                        return View(categoria);
                    }
                    else
                    {
                        _categoriaRepository.Cadastrar(categoria);
                        TempData["Sucesso"] = "Inserido com sucesso!.";
                        return RedirectToAction("Inserir");
                    }
                    
                }
                return View(categoria);
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao inserir - {error.Message}";
                return View(categoria);
            }
            
        }
        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            try
            {
                    if (_categoriaRepository.Listar().Any(p => p.Nome.Equals(categoria.Nome)))
                    {
                        TempData["Falha"] = $"A categoria {categoria.Nome} já existe!";
                        return View(categoria);
                    }
                    else
                    {
                        _categoriaRepository.Atualizar(categoria);
                        TempData["Sucesso"] = "Atualizado com sucesso!.";
                        return RedirectToAction("Index");
                    }
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Atualizar - O Campo nome precisa conter um valor. {error.Message}";
                return View(categoria);
            }
        }

    }
}
