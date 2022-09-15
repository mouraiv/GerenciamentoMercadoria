using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoMercadoria.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly IFabricanteRepository _fabricanteRepository;

        public FabricanteController(IFabricanteRepository fabricanteRepository)
        {
            _fabricanteRepository = fabricanteRepository;
        }
        public IActionResult Index()
        {
            List<Fabricante> fabricante = _fabricanteRepository.Listar();
            return View(fabricante);
        }
        public IActionResult Inserir()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            Fabricante fabricante = _fabricanteRepository.CarregarId(id);
            return View(fabricante);
        }
        public IActionResult Confirmacao(int id)
        {
            Fabricante fabricante = _fabricanteRepository.CarregarId(id);
            return View(fabricante);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                _fabricanteRepository.Deletar(id);
                TempData["Sucesso"] = "Fabricante Excluída com sucesso.";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Excluir - {error.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Inserir(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_fabricanteRepository.Listar().Any(p => p.Nome.Equals(fabricante.Nome)))
                    {
                        TempData["Falha"] = $"o fabricante {fabricante.Nome} já existe!";
                        return View(fabricante);
                    }
                    else
                    {
                        _fabricanteRepository.Cadastrar(fabricante);
                        TempData["Sucesso"] = "Inserido com sucesso!.";
                        return RedirectToAction("Inserir");
                    }

                }
                return View(fabricante);
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao inserir - {error.Message}";
                return View(fabricante);
            }

        }
        [HttpPost]
        public IActionResult Editar(Fabricante fabricante)
        {
            try
            {
                if (_fabricanteRepository.Listar().Any(p => p.Nome.Equals(fabricante.Nome)))
                {
                    TempData["Falha"] = $"O fabricante {fabricante.Nome} já existe!";
                    return View(fabricante);
                }
                else
                {
                    _fabricanteRepository.Atualizar(fabricante);
                    TempData["Sucesso"] = "Atualizado com sucesso!.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception error)
            {
                TempData["Falha"] = $"Erro ao Atualizar - O Campo nome precisa conter um valor.";
                return View(fabricante);
            }
        }

    }
}