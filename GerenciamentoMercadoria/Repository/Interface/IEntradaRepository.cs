using GerenciamentoMercadoria.Models;

namespace GerenciamentoEntrada.Repository.Interface
{
    public interface IEntradaRepository
    {
        Entrada Cadastrar(Entrada entrada);
        Entrada Atualizar(Entrada entrada);
        bool Deletar(int id);
        IEnumerable<Entrada> Listar();
        IEnumerable<Entrada> Pesquisar(DateTime data);
        Entrada CarregarId(int id);
        IEnumerable<Mercadoria> Mercadorias();
    }
}
