using GerenciamentoMercadoria.Models.ViewModels;

namespace GerenciamentoMercadoria.Repository.Interface
{
    public interface IHomeRepository
    {
        IEnumerable<EntradaSaida> Pesquisar(DateTime data, int? pagina);
        IEnumerable<EntradaSaida> Listar(int? pagina);
        IEnumerable<EntradaSaida> Listar();
    }
}
