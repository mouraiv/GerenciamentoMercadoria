using GerenciamentoMercadoria.Models;

namespace GerenciamentoMercadoria.Repository.Interface
{
    public interface ISaidaRepository
    {
        Saida Cadastrar(Saida saida);
        Saida Atualizar(Saida saida);
        bool Deletar(int id);
        List<Saida> Listar();
        Saida CarregarId(int id);
        IEnumerable<Mercadoria> Mercadorias();
    }
}
