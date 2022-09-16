using GerenciamentoMercadoria.Models;

namespace GerenciamentoEntrada.Repository.Interface
{
    public interface IEntradaRepository
    {
        Entrada Cadastrar(Entrada entrada);
        Entrada Atualizar(Entrada entrada);
        bool Deletar(int id);
        List<Entrada> Listar();
        Entrada CarregarId(int id);
        IEnumerable<Mercadoria> Mercadorias();
    }
}
