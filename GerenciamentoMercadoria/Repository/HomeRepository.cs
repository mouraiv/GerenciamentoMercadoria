using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models.ViewModels;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GerenciamentoMercadoria.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AppDbContext _context;

        public HomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EntradaSaida> Listar(int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            return _context.EntradaSaidas.AsNoTracking()
                        .Include(p => p.mercadoria).OrderBy(p => p.DataHora)
                            .ToList().ToPagedList(paginaNumero, paginaTamanho);
        }

        public IEnumerable<EntradaSaida> Listar()
        {
            return _context.EntradaSaidas.AsNoTracking().ToList();
        }

        public IEnumerable<EntradaSaida> Pesquisar(DateTime data, int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            DateTime mesInicio = DateTime.Parse($"1/{data.Month}/{data.Year}");
            DateTime mesFim = DateTime.Parse($"{DateTime.DaysInMonth(data.Year, data.Month)}/{data.Month}/{data.Year}");

            var query = _context.EntradaSaidas.AsNoTracking()
                            .Include(p => p.mercadoria).OrderBy(p => p.DataHora).
                                Where(p => p.DataHora >= mesInicio && p.DataHora <= mesFim)
                                    .ToPagedList(paginaNumero, paginaTamanho);

            return query;
        }
    }
}
