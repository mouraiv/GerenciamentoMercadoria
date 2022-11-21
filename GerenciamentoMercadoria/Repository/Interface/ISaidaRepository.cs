using GerenciamentoMercadoria.Models;

namespace GerenciamentoMercadoria.Repository.Interface
{
    public interface ISaidaRepository
    {
        Saida Cadastrar(Saida saida);
        Saida Atualizar(Saida saida);
        bool Deletar(int id);
        IEnumerable<Saida> Listar(int? pagina);
        IEnumerable<Saida> Relatorio(DateTime data);
        IEnumerable<Saida> Pesquisar(DateTime data, int? pagina);
        Saida CarregarId(int id);
        IEnumerable<Mercadoria> Mercadorias();
    }
}
