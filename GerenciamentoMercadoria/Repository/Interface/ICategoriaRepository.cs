using GerenciamentoMercadoria.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoMercadoria.Repository.Interface
{
    public interface ICategoriaRepository
    {
        Categoria Cadastrar(Categoria categoria);
        Categoria Atualizar(Categoria categoria);
        bool Deletar(int id);
        List<Categoria> Listar();
        Categoria CarregarId(int id);
    }
}
