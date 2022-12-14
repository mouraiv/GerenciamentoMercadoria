using GerenciamentoEntrada.Repository.Interface;
using GerenciamentoMercadoria.Context;
using GerenciamentoMercadoria.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace GerenciamentoEntrada.Repository
{
    public class EntradaRepository : IEntradaRepository
    {
        private readonly AppDbContext _context;

        public EntradaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Entrada Cadastrar(Entrada entrada)
        {
               _context.Entradas.Add(entrada);
               entrada.Tipo = "Entrada";
               entrada.DataHora = DateTime.Now;
               _context.SaveChanges();
               return entrada;
        }
        public Entrada Atualizar(Entrada entrada)
        {
            Entrada db = CarregarId(entrada.Id);

            if (db == null) throw new Exception("Houve um erro na atualização");

            db.Quantidade = entrada.Quantidade;
            db.Rua = entrada.Rua;
            db.Numero = entrada.Numero;
            db.Bairro = entrada.Bairro;
            db.Estado = entrada.Estado;
            db.MercadoriaId = entrada.MercadoriaId;

            _context.Entradas.Update(db);
            _context.SaveChanges();

            return db;
        }
        public bool Deletar(int id)
        {
            Entrada db = CarregarId(id);

            if (db == null) throw new Exception("Houve um erro ao apagar");

            _context.Entradas.Remove(db);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Entrada> Listar(int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            return _context.Entradas
                .Include(p => p.mercadoria)
                    .ToList().ToPagedList(paginaNumero, paginaTamanho);
        }

        public Entrada CarregarId(int id)
        {
            return _context.Entradas.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Mercadoria> Mercadorias()
        {
            return _context.Mercadorias;
        }

        public IEnumerable<Entrada> Pesquisar(DateTime data, int? pagina)
        {
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            DateTime mesInicio = DateTime.Parse($"1/{data.Month}/{data.Year}");
            DateTime mesFim = DateTime.Parse($"{DateTime.DaysInMonth(data.Year, data.Month)}/{data.Month}/{data.Year}");

            var query = _context.Entradas.Include(p => p.mercadoria)
                .Where(p => p.DataHora >= mesInicio && p.DataHora <= mesFim)
                .ToPagedList(paginaNumero, paginaTamanho);
   
            return query;
             
        }

        public IEnumerable<Entrada> Relatorio(DateTime data)
        {
            DateTime mesInicio = DateTime.Parse($"1/{data.Month}/{data.Year}");
            DateTime mesFim = DateTime.Parse($"{DateTime.DaysInMonth(data.Year, data.Month)}/{data.Month}/{data.Year}");

            var query = _context.Entradas.Include(p => p.mercadoria)
              .Where(p => p.DataHora >= mesInicio && p.DataHora <= mesFim).ToList();

            if (!data.Equals("01/01/0001 00:00:00"))
            {
                return query;
            }

            return _context.Entradas.Include(p => p.mercadoria).ToList();
        }
    }
}

