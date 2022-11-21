using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using GerenciamentoMercadoria.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GerenciamentoMercadoria.Repository
{
    public class SaidaRepository : ISaidaRepository
    {
        private readonly AppDbContext _context;

        public SaidaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Saida Cadastrar(Saida saida)
        {
            _context.Saidas.Add(saida);
            saida.Tipo = "Saida";
            saida.DataHora = DateTime.Now;
            _context.SaveChanges();
            return saida;
        }
        public Saida Atualizar(Saida saida)
        {
            Saida db = CarregarId(saida.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Quantidade = saida.Quantidade;
            db.Rua = saida.Rua;
            db.Numero = saida.Numero;
            db.Bairro = saida.Bairro;
            db.Estado = saida.Estado;
            db.MercadoriaId = saida.MercadoriaId;

            _context.Saidas.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Saida db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Saidas.Remove(db);
            _context.SaveChanges();

            return true;
        }

        public IEnumerable<Saida> Listar(int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            return _context.Saidas
                .Include(p => p.mercadoria)
                    .ToList().ToPagedList(paginaNumero, paginaTamanho);
        }

        public IEnumerable<Saida> Pesquisar(DateTime data, int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            DateTime mesInicio = DateTime.Parse($"1/{data.Month}/{data.Year}");
            DateTime mesFim = DateTime.Parse($"{DateTime.DaysInMonth(data.Year, data.Month)}/{data.Month}/{data.Year}");

            var query = _context.Saidas.Include(p => p.mercadoria)
                .Where(p => p.DataHora >= mesInicio && p.DataHora <= mesFim)
                .ToPagedList(paginaNumero, paginaTamanho);

            return query;

        }

        public IEnumerable<Saida> Relatorio(DateTime data)
        {
            DateTime mesInicio = DateTime.Parse($"1/{data.Month}/{data.Year}");
            DateTime mesFim = DateTime.Parse($"{DateTime.DaysInMonth(data.Year, data.Month)}/{data.Month}/{data.Year}");

            var query = _context.Saidas.Include(p => p.mercadoria)
              .Where(p => p.DataHora >= mesInicio && p.DataHora <= mesFim).ToList();

            if (!data.Equals("01/01/0001 00:00:00"))
            {
                return query;
            }

            return _context.Saidas.Include(p => p.mercadoria).ToList();
        }

        public Saida CarregarId(int id)
        {
            return _context.Saidas.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Mercadoria> Mercadorias()
        {
            return _context.Mercadorias;
        }
    }
}




