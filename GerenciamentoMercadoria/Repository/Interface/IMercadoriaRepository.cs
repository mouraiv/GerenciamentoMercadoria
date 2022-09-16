using GerenciamentoMercadoria.Models;

namespace GerenciamentoMercadoria.Repository.Interface
{
    public interface IMercadoriaRepository
    {
        Mercadoria Cadastrar(Mercadoria mercadoria);
        Mercadoria Atualizar(Mercadoria mercadoria);
        bool Deletar(int id);
        List<Mercadoria> Listar();
        Mercadoria CarregarId(int id);
        IEnumerable<Categoria> Categorias();
        IEnumerable<Fabricante> Fabricantes();
    }
}
