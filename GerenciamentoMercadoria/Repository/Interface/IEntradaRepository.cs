using GerenciamentoMercadoria.Models;

namespace GerenciamentoEntrada.Repository.Interface
{
    public interface IEntradaRepository
    {
        Entrada Cadastrar(Entrada entrada);
        Entrada Atualizar(Entrada entrada);
        bool Deletar(int id);
        List<Entrada> Listar();
        List<Entrada> Listar(string day, string mes, string ano);
        Entrada CarregarId(int id);
        IEnumerable<Mercadoria> Mercadorias();
    }
}
