using GerenciamentoMercadoria.Models;

namespace GerenciamentoMercadoria.Repository.Interface
{
    public interface IFabricanteRepository
    {
        Fabricante Cadastrar(Fabricante fabricante);
        Fabricante Atualizar(Fabricante fabricante);
        bool Deletar(int id);
        List<Fabricante> Listar();
        Fabricante CarregarId(int id);
    }
}
